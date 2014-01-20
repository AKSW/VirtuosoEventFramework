using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WcfSamples.DynamicProxy;
using System.Reflection;
using System.ServiceModel.Description;
using System.Windows.Forms;
using EventOntology;
using System.ServiceModel;
using System.Data;
using System.Xml;

namespace EventFrameworkControl
{
    public static class StaticHelper
    {
        public static IEventClient ClientProxy { get; set; }
        public static int DataSource { get; set; }
        public static string CentralDbEndpoint { get; set; }
        public static User CurrentUser { get; set; }
        public static List<string> ColumnsToUse { get; private set; }
        public static string ecaRights= "ECADefRight";
        public static string accRights = "UserAccRight";
        public static DataTable currentAE { get; set; }
        public static DataTable currentCE { get; set; }
        public static DataTable currentAC { get; set; }
        public static DataTable currentCO { get; set; }

        public static void InitStatics()
        {
            InitializeClientProxy();
            UpdateDataSources();
            CentralDbEndpoint = ClientProxy.GetDatabases("").Rows.Cast<DataRow>().Single(x => x["DSInstance"].ToString() == "1")["ProcedureEndpoint"].ToString();
            DataSource = 1;
            ColumnsToUse = new List<string>();
            ColumnsToUse.AddRange(new List<string>() { "ActionID", "CreatedBy", "WsdlAddress", "EndpointAddress", "MethodeName", "Description", 
                "ParamTypes", "ParamDescr", "ReturnType", "ReturnDescr"});
        }

        public static void UpdateDataSources()
        {
            currentAE = ClientProxy.GetAllTriggers("");
            currentCE = ClientProxy.GetComplexEvents();
            currentAC = ClientProxy.GetActionsOrConditions(Activity.Action);
            currentCO = ClientProxy.GetActionsOrConditions(Activity.Condition);
        }

        public static void InitializeClientProxy()
        {
            //NetTcpBinding bind = new NetTcpBinding();
            NetNamedPipeBinding bind = new NetNamedPipeBinding();
            bind.MaxBufferPoolSize = 1000000;
            bind.MaxBufferSize = 1000000;
            bind.MaxReceivedMessageSize = 1000000;
            bind.SendTimeout = new TimeSpan(0, 0, 30);
            XmlDictionaryReaderQuotas myReaderQuotas = new XmlDictionaryReaderQuotas();
            myReaderQuotas.MaxStringContentLength = 1000000;
            myReaderQuotas.MaxArrayLength = 1000000;
            myReaderQuotas.MaxBytesPerRead = 1000000;
            myReaderQuotas.MaxDepth = 1000000;
            myReaderQuotas.MaxNameTableCharCount = 1000000;
            bind.GetType().GetProperty("ReaderQuotas").SetValue(bind, myReaderQuotas, null);
            ChannelFactory<IEventClient> clientFactory = new ChannelFactory<IEventClient>(bind, Properties.Settings.Default.netPipeAddress);
            ClientProxy = clientFactory.CreateChannel();
        }

        public static DataRow getDataBoundItem(DataGridViewRow row)
        {
            return (row.DataBoundItem as DataRowView).Row;
        }

        public static IEnumerable<Control> GetOffsprings(this Control @this)
        {
            foreach (Control child in @this.Controls)
            {
                yield return child;
                foreach (var offspring in GetOffsprings(child))
                    yield return offspring;
            }
        }

        static public string BeautifyXml(string input)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(input);
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";
            settings.NewLineChars = "\r\n";
            settings.NewLineHandling = NewLineHandling.Replace;
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }

        public static ComplexEvent LoadComplexEvent(int ceid)
        {
            bool firstStage = true;
            int stageCount = 0;
            int rowNr = StaticHelper.currentCE.Rows.IndexOf(StaticHelper.currentCE.Rows.Cast<DataRow>().Where(x => x["CEID"].ToString() == ceid.ToString()).First());
            ComplexEvent ev = new ComplexEvent(currentCE, rowNr);
            ev.Stages = new List<InitialStage>();
            DataTable sets;
            DataTable paramMappings = StaticHelper.ClientProxy.GetParamMappings(ev.CeId);

            List<EventAction> actions = new List<EventAction>();

            while (true)
            {

                sets = null;
                sets = StaticHelper.ClientProxy.GetStagesAndSets(ev.InitialStagUri, stageCount);
                if (sets == null)
                    break;

                List<ConditionQuery> conditions = new List<ConditionQuery>();
                List<ParameterMap> mapList = new List<ParameterMap>();

                foreach (DataRow row in paramMappings.Rows.Cast<DataRow>().Where(x => (int)(x[Constants.getParamMappings_CEID])
                    == ev.CeId && (int)(x[Constants.getParamMappings_StageNr]) == stageCount))
                {
                    if (Convert.ToInt32(row[Constants.getParamMappings_Condition]) == 1 && conditions.Where(y => y.ID == Convert.ToInt32(row[Constants.getParamMappings_ActionID])).Count() == 0)
                        conditions.Add(new ConditionQuery(currentCO, currentCO.Rows.IndexOf(currentCO.Rows.Cast<DataRow>().Where(z => Convert.ToInt32(z[Constants.getParamMappings_ActionID]) ==
                        Convert.ToInt32(row[Constants.getParamMappings_ActionID])).First())));
                    if (Convert.ToInt32(row[Constants.getParamMappings_Condition]) == 0 && actions.Where(y => y.ID == Convert.ToInt32(row[Constants.getParamMappings_ActionID])).Count() == 0)
                        actions.Add(new EventAction(currentAC, currentAC.Rows.IndexOf(currentAC.Rows.Cast<DataRow>().Where(z => Convert.ToInt32(z[Constants.getParamMappings_ActionID]) ==
                        Convert.ToInt32(row[Constants.getParamMappings_ActionID])).First())));
                    object zw = null;
                    if (row[Constants.getParamMappings_StaticValue].ToString() != "NULL" && row[Constants.getParamMappings_StaticValueType].ToString() != "NULL")
                    {
                        zw = Convert.ChangeType(row[Constants.getParamMappings_StaticValue], Type.GetType(row[Constants.getParamMappings_StaticValueType].ToString()));
                        mapList.Add(new ParameterMap((int)(row[Constants.getParamMappings_StageNr]), (int)(row[Constants.getParamMappings_ActionID]), (int)(row[Constants.getParamMappings_ActionNr]),
                            (int)(row[Constants.getParamMappings_ParamNr]), row[Constants.getParamMappings_Description].ToString(), zw, ev.CeId));
                    }
                    string ttt = row[Constants.getParamMappings_EventValueMap].ToString();
                    if (!string.IsNullOrEmpty(row[Constants.getParamMappings_EventValueMap].ToString()))
                    {
                        mapList.Add(new ParameterMap((int)(row[Constants.getParamMappings_StageNr]), (int)(row[Constants.getParamMappings_ActionID]), (int)(row[Constants.getParamMappings_ActionNr]),
                             (int)(row[Constants.getParamMappings_ParamNr]), row[Constants.getParamMappings_Description].ToString(), null, ev.CeId, 0, row[Constants.getParamMappings_EventValueMap].ToString()));
                    }
                    if ((int)row[Constants.getParamMappings_ConditionQuery] > 0)
                    {
                        mapList.Add(new ParameterMap((int)(row[Constants.getParamMappings_StageNr]), (int)(row[Constants.getParamMappings_ActionID]), (int)(row[Constants.getParamMappings_ActionNr]),
                            (int)(row[Constants.getParamMappings_ParamNr]), row[Constants.getParamMappings_Description].ToString(), null, ev.CeId, (int)row[Constants.getParamMappings_ConditionQuery]));
                    }
                }

                string firstSet = sets.Rows[0][Constants.getStagesFromEvents_Parent].ToString();

                EventSet set = createEventSetFromDatatable(ref sets, firstSet);
                if (firstStage)
                {
                    ev.Stages.Add(new Stage(set, StaticHelper.ClientProxy.GetStageUriFromEvent(firstSet)));
                    firstStage = false;
                }
                else
                    ev.Stages.Add(new Stage(set, StaticHelper.ClientProxy.GetStageUriFromEvent(firstSet)));

                (ev.Stages.Last() as InitialStage).TimeRestriction = StaticHelper.ClientProxy.GetTimeOfStage(ev.InitialStagUri, stageCount);
                ev.Stages.Last().ConditionQuerys = conditions;
                ev.Stages.Last().ParameterMappings = mapList;
                stageCount++;
            }
            try
            {
                ev.Stages.Last().Actions = actions;
            }
            catch (InvalidOperationException)
            {

            }

            return ev;
        }

        private static EventSet createEventSetFromDatatable(ref DataTable source, string setName, EventSet parentSet = null)
        {
            EventSet outSet;
            int id = int.Parse(setName.Substring(setName.LastIndexOf("EventSet") + 8));
            if (setName.Contains("MultiEventSet"))
                outSet = new MultiSet(id, parentSet, null);
            else
                outSet = new EventSet(id, parentSet, null);
            string operatorUri = source.Rows.Cast<DataRow>().Where(x => x[Constants.getStagesFromEvents_Parent].ToString().ToLower().Trim()
                == setName.ToLower().Trim()).First()[Constants.getStagesFromEvents_Operator].ToString();
            outSet.Operator = (Operator)(Enum.Parse(typeof(Operator), operatorUri.Substring(operatorUri.LastIndexOf('/') + 1)));


            foreach (DataRow row in source.Rows.Cast<DataRow>().Where(x => x[Constants.getStagesFromEvents_Parent].ToString().ToLower().Trim() == setName.ToLower().Trim()))
            {
                if (row[Constants.getStagesFromEvents_Parent].ToString().Contains("MultiEventSet"))
                {
                    (outSet as MultiSet).MaxCardinality = int.Parse(row[Constants.getStagesFromEvents_MaxRec].ToString());
                    (outSet as MultiSet).MinCardinality = int.Parse(row[Constants.getStagesFromEvents_MinRec].ToString());
                }
                if (row[Constants.getStagesFromEvents_Event].ToString().Contains("AtomicEvent"))
                    outSet.Events.Add(new AtomicEvent(
                        currentAE, currentAE.Rows.IndexOf(currentAE.Rows.Cast<DataRow>().Where(y => y[Constants.trigID].ToString() ==
                            row[Constants.getStagesFromEvents_Event].ToString().Substring(row[Constants.getStagesFromEvents_Event].ToString().LastIndexOf("AtomicEvent") + 11)).First())));
                else if (row[Constants.getStagesFromEvents_Event].ToString().Contains("ComplexEvent"))
                    outSet.Events.Add(new ComplexEvent(
                       currentCE, currentCE.Rows.IndexOf(currentCE.Rows.Cast<DataRow>().Where(y => y[Constants.ceid].ToString() ==
                           row[Constants.getStagesFromEvents_Event].ToString().Substring(row[Constants.getStagesFromEvents_Event].ToString().LastIndexOf("ComplexEvent") + 12)).First())));
                else if (row[Constants.getStagesFromEvents_Event].ToString().Contains("EventSet"))
                    outSet.Events.Add(createEventSetFromDatatable(ref source, row[Constants.getStagesFromEvents_Event].ToString(), outSet));
            }
            return outSet;
        }
    }
}
