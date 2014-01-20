using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Net.Security;
using System.Data;
using VirtuosoEventService.VirtuosoCentral;
using System.Configuration;
using System.Web.Configuration;
using EventOntology;
using System.Text.RegularExpressions;
using System.Data.Odbc;
using System.IO;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Disposables;

namespace VirtuosoEventService
{
    public class DistributedVirtuosoEventService : EventOntology.IVirtuosoEventService
    {
        private static Dictionary<string, List<string>> graphDict = new Dictionary<string, List<string>>();
        private string insertGraph = "http://EventFramework/Stages";
        private BasicHttpBinding bind = new BasicHttpBinding();

        /// <summary>
        /// constructor: cennects to specific Virtuoso-DB 
        /// </summary>
        /// <param name="Connections">a list of connection-strings for all used Virtuoso instances including their passwords</param>
        public DistributedVirtuosoEventService()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(StaticHelper.CurrentDomain_UnhandledException);

            bind.MaxBufferPoolSize = 2147483647;
            bind.MaxBufferSize = 2147483647;
            bind.MaxReceivedMessageSize = 2147483647;
        }
        
        public int GetControlId(int dsInstance)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            dbcmd.CommandText = "SELECT ControlID FROM EventFrameworkDataSources WHERE DSInstance =" + dsInstance.ToString();
            int zw =  (int)dbcmd.ExecuteScalar();
            dbcmd.Dispose();
            dbcmd = null;
            return zw;
        }

        private int getNextTriggerId()
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            dbcmd.CommandText = "SELECT CASE WHEN MAX(TriggerID) IS NULL THEN 1 ELSE MAX(TriggerID)+1 END FROM EventFrameworkTriggers";
            int zw =  (int)dbcmd.ExecuteScalar();
            dbcmd.Dispose();
            dbcmd = null;
            return zw;
        }


        public string GetRemoteProcedureEndpoint(int dsInstance)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            dbcmd.CommandText = "SELECT ProcedureEndpoint FROM EventFrameworkDataSources WHERE DSInstance =" + dsInstance.ToString();
            string zw = dbcmd.ExecuteScalar().ToString(); 
            dbcmd.Dispose();
            dbcmd = null;
            return zw;
        }
        
        public DataTable GetEvents(int minutes)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            string sql = "SELECT \"EventID\", \"TriggerID\", \"CEID\", nullableDateString(\"Occurence\") AS Occurence, \"DSInstance\", \"InternalSource\", DUMP_VEC(\"Row\") AS RowValue FROM  EventFrameworkEvents WHERE datediff('minute', Occurence, getdate()) < " + minutes.ToString();
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            DataTable table = new DataTable();
            table.TableName = "AtomicEvents";
            DataTable schema = reader.GetSchemaTable();

            foreach (DataRow col in schema.Rows)
            {
                Type type = (col["DataType"] as Type);
                table.Columns.Add(col["ColumnName"].ToString(), type);
            }

            while (reader.Read())
            {
                object[] zw = new object[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)

                            zw[i] = reader[i];
                table.Rows.Add(zw);
            }
            // clean up
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            return table;
        }

        public DataTable GetUsers(string searchString)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            string sql = "SELECT UserID, Name, nullableDateString(Created) AS Created, CASE WHEN LastLogIn IS NULL THEN NULL ELSE nullableDateString(LastLogIn) END AS LastLogIn, SessionNr, UserAccRight, ECADefRight, Datasources FROM EventFrameworkUsers WHERE Name LIKE '%" + searchString + "%'";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            DataTable table = new DataTable();
            table.TableName = "Users";
            DataTable schema = reader.GetSchemaTable();

            foreach (DataRow col in schema.Rows)
            {
                Type type = (col["DataType"] as Type);
                table.Columns.Add(col["ColumnName"].ToString(), type);
            }

            while (reader.Read())
            {
                object[] zw = new object[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)

                    zw[i] = reader[i];
                table.Rows.Add(zw);
            }
            // clean up
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            return table;
        }
        
        public User LogIn(User currentUser)
        {

                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                string sql = "UPDATE EventFrameworkUsers SET LastLogIn = GETDATE(), SessionNr = EVENT_FRAMEWORK_GET_SESSION_NR () " +
                    "WHERE      (Name = '" + currentUser.Name + "') AND (Pass = subseq(pwd_magic_calc(Name,'" + currentUser.Pass + "'),1)) ";// +
                    
                dbcmd.CommandText = sql;
                dbcmd.ExecuteNonQuery();

                sql = "SELECT \"UserID\",\"Name\",nullableDateString(\"Created\") as Created,nullableDateString(\"LastLogIn\") as LastLogIn,\"SessionNr\",\"Datasources\",\"UserAccRight\",\"ECADefRight\" " +
                    "FROM  EventFrameworkUsers WHERE (Name = '" + currentUser.Name + "') AND (Pass = subseq(pwd_magic_calc(Name, '" + currentUser.Pass + "'),1))";
                dbcmd.CommandText = sql;
                IDataReader reader = dbcmd.ExecuteReader();
                DataTable table = new DataTable();
                table.TableName = "Users";
                DataTable schema = reader.GetSchemaTable();

                foreach (DataRow col in schema.Rows)
                {
                    Type type = (col["DataType"] as Type);
                    table.Columns.Add(col["ColumnName"].ToString(), type);
                }

                while (reader.Read())
                {
                    object[] zw = new object[table.Columns.Count];
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        zw[i] = reader[i];
                    }
                    table.Rows.Add(zw);
                }
                // clean up
                reader.Close();
                reader = null;
                dbcmd.Dispose();
                dbcmd = null;
                currentUser = new User(ref table, 0);
            
            return currentUser;
        }

        public bool CreateNewAccount(User newUser, User admin)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            string sql = "SELECT    CASE WHEN COUNT(*) > 0 THEN 'True' ELSE 'False' END FROM EventFrameworkUsers WHERE NAME = '" + newUser.Name + "'";
            dbcmd.CommandText = sql;
            object zw = dbcmd.ExecuteScalar();
            if (admin.UserAccRight && admin.SessionNr > 0 && !bool.Parse(zw.ToString()))
            {      //not!
                dbcmd.CommandText = "INSERT INTO EventFrameworkUsers(Name, Pass, Created, UserAccRight, ECADefRight) VALUES ('" + newUser.Name + "',subseq(pwd_magic_calc('" + newUser.Name + "','" + newUser.Pass + "'),1), GETDATE(), 0, 0)	";
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                dbcmd = null;
                return true;
            }
            else
            {
                dbcmd.Dispose();
                dbcmd = null;
                return false;
            }
        }
        
        public bool ResetUserpassword(string userName, string newPass, User admin)
        {
            if (admin.UserAccRight)
            {
                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                string sql = "UPDATE    EventFrameworkUsers SET  Pass = subseq(pwd_magic_calc('" + userName + "','" + newPass + "'),1) WHERE     (Name = '" + userName + "')";
                dbcmd.CommandText = sql;
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                dbcmd = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateUserAccount(User user, User admin)
        {
            if (user.UserAccRight)
            {
                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                string sql = "UPDATE    EventFrameworkUsers SET \"Datasources\" = '" + user.Datasources + "',\"UserAccRight\" = " + Convert.ToInt32(user.UserAccRight).ToString() + 
                    ", \"ECADefRight\" = " + Convert.ToInt32(user.EcaDefRight).ToString() + " WHERE (Name = '" + user.Name + "')";
                dbcmd.CommandText = sql;
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                dbcmd = null;
                return true;
            }
            else
            {
                return false;
            }
        }

        public string RegisterTrigger(AtomicEvent trigger, User admin)
        {
            if (trigger.triggerName.Contains('.'))
            {
                trigger.triggerName = trigger.triggerName.Substring(trigger.triggerName.LastIndexOf('.') + 1);
            }
            
            if (admin.SessionNr > 0 && admin.EcaDefRight)
            {
                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                dbcmd.CommandText = "SELECT CASE WHEN EXISTS(SELECT * FROM EventFrameworkTriggers WHERE TriggerName = '" + trigger.triggerName + "' OR AlternativeName = '" + trigger.triggerName  + "') THEN 'True' ELSE 'False' END";
                object zw = dbcmd.ExecuteScalar();
                dbcmd.Dispose();
                dbcmd = null;
                if (bool.Parse(zw.ToString()))
                {
                    dbcmd = StaticHelper.dbcon.CreateCommand();
                    int nextTrigger = getNextTriggerId();
                    string internalTriggerName = trigger.triggerType + "TRIGGER_" + trigger.dsInstance.ToString() + "_" + trigger.internalSource + "_" + nextTrigger.ToString();
                    //try
                    //{
                        dbcmd.CommandText = "INSERT INTO EventFrameworkTriggers (TriggerType, TriggerName, AlternativeName, DSInstance, \"InternalSource\", \"Values\", Created, \"CreatedBy\", \"Description\") " +
                            "VALUES('" + trigger.triggerType + "','" + internalTriggerName + "','" + trigger.triggerName + "'," + trigger.dsInstance + ",'" + trigger.internalSource + "','" +
                            trigger.returnValues + "',UTC_TIME()," + trigger.createdBy + ",'" + trigger.description + "')";
                        dbcmd.ExecuteNonQuery();
                        dbcmd.Dispose();
                        dbcmd = null;
                    //}
                    //catch (Exception)
                    //{
                    //    throw;
                    //}
                    return "registered as: " + internalTriggerName;
                }
                else
                    return "this event-name is taken";
            }
            else
                return "you have no right to do this";
        }

        public string SetNewSqlTrigger(AtomicEvent trigger,  User admin)
        {

            if (admin.SessionNr > 0 && admin.EcaDefRight)
            {
                if (trigger.triggerName.Contains('.'))
                {
                    trigger.triggerName = trigger.triggerName.Substring(trigger.triggerName.LastIndexOf('.') + 1);
                }
                if (trigger.internalSource.Contains('.'))
                {
                    trigger.internalSource = trigger.internalSource.Substring(trigger.internalSource.LastIndexOf('.') + 1);
                }

                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                EndpointAddress addr = new EndpointAddress(GetRemoteProcedureEndpoint(trigger.dsInstance));
                EventFrameworkProceduresDocLiteralPortTypeClient virtuosoSoapClient = new EventFrameworkProceduresDocLiteralPortTypeClient(bind, addr);
                string[] allColumns = null;

                allColumns = GetColumnsOfRemoteTable(trigger.dsInstance, trigger.internalSource);
                int nextTrigger = getNextTriggerId();
                string internalTriggerName = trigger.triggerType.ToString() + "TRIGGER_" + trigger.dsInstance.ToString() + "_" + trigger.internalSource;
                string TriggerOption = "";
                string optionShort = "";
                string beforeAfter = "";

                if (trigger.triggerType == "INSERT")
                {
                    TriggerOption = " new as N ";
                    optionShort = "N.";
                    beforeAfter = " AFTER ";
                }
                else if (trigger.triggerType == "UPDATE")
                {
                    TriggerOption = " old as O, new as N ";
                    optionShort = "N.";
                    beforeAfter = " BEFORE ";
                }
                else if (trigger.triggerType == "DELETE")
                {
                    TriggerOption = " old as O ";
                    optionShort = "O.";
                    beforeAfter = " BEFORE ";
                }
                else
                    return "wrong trigger-type supplied";

                string Triggercondition = " " + trigger.condition.Replace("\"", "") + " ";
                string rowVector = " rowVector:= make_array(" + allColumns.Where(x=> !(x.Contains("_IDN"))).Count().ToString() + ", 'any' ); ";

                List<MatchCollection> matchesList = new List<MatchCollection>();
                List<Match> matches = new List<Match>();
                for (int i = 0; i < allColumns.Length;i++ )
                    if (!(allColumns[i].Contains("_IDN")) && !string.IsNullOrEmpty(allColumns[i])) //not!
                    {
                        rowVector += "rowVector[" + i.ToString() + "] := " + optionShort + "\"" + allColumns[i].Trim() + "\"; ";
                        if (Triggercondition.ToLower().Contains(allColumns[i].ToLower()))
                        {
                            Regex colReg = new Regex("(?<=[\\.\\[\\]\\(\\)\\{\\}';,\\=\\+\\- ])(" + allColumns[i] + ")(?=[\\.\\[\\]\\(\\)\\{\\}';,\\=\\+\\- ])");
                            matchesList.Add(colReg.Matches(Triggercondition));
                            Triggercondition = colReg.Replace(Triggercondition, " ? ");
                        }
                    }
                foreach (MatchCollection matchis in matchesList)
                    foreach (Match match in matchis)
                        matches.Add(match);
                matches = matches.OrderBy(x => x.Index).ToList();
                string paramList = "";
                for (int i = 0; i < matches.Count; i++)
                {
                    if (i != 0)
                        paramList += ",";
                    paramList += allColumns.ToList().IndexOf(allColumns.Where(x => x == matches[i].Value).First()).ToString();
                }

                Regex tableReg = new Regex("(?<=[\\.\\[\\]\\(\\)\\{\\}';,\\=\\+\\- ])(" + trigger.internalSource + ")(?=[\\.\\[\\]\\(\\)\\{\\}';,\\=\\+\\- ])");
                Triggercondition = tableReg.Replace(Triggercondition, "\"" + trigger.internalSource + "\"");
                Triggercondition = "SELECT CASE WHEN (" + Triggercondition + ") THEN 1 ELSE 0 END";
                //Triggercondition = Triggercondition.Replace("'", "\\'");

                string syntax = "create trigger " + internalTriggerName + beforeAfter + trigger.triggerType + " on \"" +
                    trigger.internalSource + "\" REFERENCING " + TriggerOption + "{ declare aq, res, rowVector, i, state, msg, descs, trigg any; ";
                
                syntax += rowVector;

                syntax += "EXEC('SELECT TriggerName, Condition, ParamArray FROM EventFrameworkTriggerConditions WHERE TableName = \\'" + trigger.internalSource + "\\' AND TriggerType = \\'" + trigger.triggerType + "\\'' , state, msg, vector(), 10000, descs, trigg); ";
                syntax += "	if (internal_type_name(internal_type(trigg)) <> 'INTEGER' AND LENGTH(trigg)>0){	for(i:=0;i<LENGTH(trigg);i:=i+1){";
                syntax += " aq := async_queue (1); res:=aq_request (aq, 'EVENT_FRAMEWORK_CHECK_TRIGGER_CONDITION', vector ('" + trigger.internalSource + "', UTC_TIME(), trigg[i][0], trigg[i][1], trigg[i][2], rowVector, 1)); }}}";
                string zw = null;

                internalTriggerName = internalTriggerName + "_" + nextTrigger.ToString();
                zw = virtuosoSoapClient.EVENT_FRAMEWORK_SET_NEW_TRIGGER(GetControlId(trigger.dsInstance), syntax, trigger.internalSource, internalTriggerName, trigger.triggerType, Triggercondition, paramList);

                bool result = false;
                if (bool.TryParse(zw, out result) && result)
                {
                    dbcmd.CommandText = "INSERT INTO EventFrameworkTriggers (TriggerType, TriggerName, AlternativeName, DSInstance, \"InternalSource\", \"Values\", Created, \"CreatedBy\", \"Description\", Statement) " +
                        "VALUES('" + trigger.triggerType.ToString() + "','" + internalTriggerName + "','" + trigger.alternativeName + "'," + trigger.dsInstance + ",'" + trigger.internalSource + "','" +
                        trigger.returnValues + "',UTC_TIME()," + trigger.createdBy + ",'" + trigger.description.Replace("'", "\\'") + "','" + trigger.condition.Replace("'", "\\'") + "')";
                    dbcmd.ExecuteNonQuery();
                }
                else
                {
                    virtuosoSoapClient.Close();
                    virtuosoSoapClient = null;
                    dbcmd.Dispose();
                    dbcmd = null;
                    return "An error occured: " + zw;
                }

                virtuosoSoapClient.Close();
                virtuosoSoapClient = null;
                dbcmd.Dispose();
                dbcmd = null;
                return "Atomic event has been created";
            }
            else
                return "you have no right to do this";
        }


        public string SetNewRdfTrigger(AtomicEvent trigger,User admin)       //(string UserName, string Description, string TriggerType, string Querysyntax, int dsInstance = 1)
        {
            if (admin.SessionNr > 0 && admin.EcaDefRight)
            {
                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                EndpointAddress addr = new EndpointAddress(GetRemoteProcedureEndpoint(trigger.dsInstance));
                EventFrameworkProceduresDocLiteralPortTypeClient virtuosoSoapClient = new EventFrameworkProceduresDocLiteralPortTypeClient(bind, addr);

                int nextTrigger = getNextTriggerId();
                string internalTriggerName = trigger.triggerType.ToString() + "TRIGGER_" + trigger.dsInstance.ToString() + "_RDF_" + nextTrigger.ToString();

                
                string triggerCondition = trigger.condition;
                int graphFilterPos = triggerCondition.LastIndexOf("?graph IN");
                string graphFilter = triggerCondition.Substring(graphFilterPos);
                graphFilter = graphFilter.Replace("<", "\"").Replace(">", "\"");
                graphFilter = graphFilter.Replace("?graph IN", "\"??\" IN");
                triggerCondition = triggerCondition.Substring(0, graphFilterPos) + graphFilter;
                int last = triggerCondition.LastIndexOf('}');
                triggerCondition = triggerCondition.Insert(last, " FILTER(str(?subj) = \"??\") FILTER(str(?pred) = \"??\") FILTER(str(?obj) = \"??\")");
                if (triggerCondition.Trim().Substring(0, 6).ToLower() != "sparql")
                    triggerCondition = "sparql " + triggerCondition;

                string zw = null;
                    zw = virtuosoSoapClient.EVENT_FRAMEWORK_SET_NEW_TRIGGER((int)(GetControlId(trigger.dsInstance)), "", "VirtuosoTripleStore", internalTriggerName, trigger.triggerType, triggerCondition, null);

                bool result = false;
                if (bool.TryParse(zw, out result) && result)
                {
                try
                {
                    dbcmd.CommandText = "INSERT INTO EventFrameworkTriggers (TriggerType, TriggerName, AlternativeName, DSInstance, \"InternalSource\", \"Values\", Created, \"CreatedBy\", \"Description\", Statement) " +
                        "VALUES('" + trigger.triggerType.ToString() + "','" + internalTriggerName + "','" + trigger.alternativeName + "'," + trigger.dsInstance + ",'VirtuosoTripleStore','graph, subject, predicate, object',UTC_TIME()," +
                        trigger.createdBy + ",'" + trigger.description + "','" + trigger.condition + "')";
                    dbcmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    virtuosoSoapClient.Close();
                    virtuosoSoapClient = null;
                    dbcmd.Dispose();
                    dbcmd = null;
                    return "An error occured";
                }
                }
                else
                {
                    virtuosoSoapClient.Close();
                    virtuosoSoapClient = null;
                    dbcmd.Dispose();
                    dbcmd = null;
                    return "An error occured: " + zw;
                }

                dbcmd.Dispose();
                dbcmd = null;
                virtuosoSoapClient.Close();
                virtuosoSoapClient = null;
                return "Atomic event has been created";
            }
            return "you have no right to do this";
        }

        public string[] GetSchemaTables(int dsInstance = 1)
        {
            EndpointAddress addr = new EndpointAddress(GetRemoteProcedureEndpoint(dsInstance));

            EventFrameworkProceduresDocLiteralPortTypeClient virtuosoSoapClient = new EventFrameworkProceduresDocLiteralPortTypeClient(bind, addr);
            string[] zw = null;

                zw = virtuosoSoapClient.EVENT_FRAMEWORK_GET_SCHEMA_TABLES((int)(GetControlId(dsInstance)));

            virtuosoSoapClient.Close();
            virtuosoSoapClient = null;
            return zw;
        }


        public DataTable GetDatabases(string type)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            string sql = "SELECT DSInstance, Active, DSName, DSType, ControlID, Description, ProcedureEndpoint, SparqlEndpointAddress FROM EventFrameworkDataSources WHERE DSType LIKE '%" + type + "%' ORDER BY DSInstance DESC";
            dbcmd.CommandText = sql;
            //Console.WriteLine(sql);
            IDataReader reader = dbcmd.ExecuteReader();
            //Console.WriteLine("reader");
            DataTable table = new DataTable();
            table.TableName = "DataSources";
            DataTable schema = reader.GetSchemaTable();
            //Console.WriteLine("schema");
            foreach (DataRow col in schema.Rows)
                table.Columns.Add(col["ColumnName"].ToString(), (col["DataType"] as Type));

            while (reader.Read())
            {
                object[] zw = new object[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)
                    zw[i] = reader[i];
                table.Rows.Add(zw);
            }
            // clean up
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            return table;
        }

        public string[] GetTriggersOfTable(string tableName, int dsInstance = 1)
        {
            EndpointAddress addr = new EndpointAddress(GetRemoteProcedureEndpoint(dsInstance));

            EventFrameworkProceduresDocLiteralPortTypeClient virtuosoSoapClient = new EventFrameworkProceduresDocLiteralPortTypeClient(bind, addr);
            string[] zw = null;

                zw = virtuosoSoapClient.EVENT_FRAMEWORK_GET_TRIGGERS_OF_TABLE((int)(GetControlId(dsInstance)), tableName);

            virtuosoSoapClient.Close();
            virtuosoSoapClient = null;
            return zw;
        }

        public string GetSupportedDBs()
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            dbcmd.CommandText = "SELECT \"Value\" FROM EventFrameworkConstants  WHERE \"Key\" = 'supportedSources'";
            string zw = dbcmd.ExecuteScalar().ToString();
            dbcmd.Dispose();
            dbcmd = null;
            return zw;
        }

        public string RegisterNewRemoteDataSource(DataSource ds, User admin)
        {
            if (admin.SessionNr > 0 && admin.EcaDefRight)
            {
                object zw;
                try
                {
                    IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                    dbcmd.CommandText = "SELECT EVENT_FRAMEWORK_REGISTER_REMOTE_DB('" + ds.DsName + "','" + ds.DsType + "','" + ds.Description + "','" + ds.ProcedureEndpoint + "','" + ds.SparqlEndpoint + "')";
                    zw = dbcmd.ExecuteScalar();
                    dbcmd.Dispose();
                    dbcmd = null;
                    return zw.ToString();
                }
                catch (FaultException ex)
                {//TODO
                    if (ex.Message.Contains("controlid") || ex.Message.Contains("unique"))
                        return "If this datasource has been registered before, make sure to clear the table 'EventFrameworkConstants' on the client.";
                    return "an exception occurd: " + ex.Message;
                }
            }
            else
                return "you have no right to do this";
        }

        public string[][] ExecuteTestSqlQuery(string querryString, int dbInstance = 1)
        {
            EndpointAddress addr = new EndpointAddress(GetRemoteProcedureEndpoint(dbInstance));

            EventFrameworkProceduresDocLiteralPortTypeClient virtuosoSoapClient = new EventFrameworkProceduresDocLiteralPortTypeClient(bind, addr);
            string[][] zw = null;

                zw = virtuosoSoapClient.EVENT_FRAMEWORK_TEST_SQL_CONDITION(querryString);

            virtuosoSoapClient.Close();
            virtuosoSoapClient = null;
            return zw;
        }

        public string[] GetColumnsOfRemoteTable(int dbInstance, string tableName)
        {
            EndpointAddress addr = new EndpointAddress(GetRemoteProcedureEndpoint(dbInstance));

            EventFrameworkProceduresDocLiteralPortTypeClient virtuosoSoapClient = new EventFrameworkProceduresDocLiteralPortTypeClient(bind, addr);
            string[] zw = null;

                zw = virtuosoSoapClient.EVENT_FRAMEWORK_GET_COLUMNS_OF_TABLE((int)(GetControlId(dbInstance)), tableName.Substring(tableName.LastIndexOf('.') + 1));

            if(zw != null)
                zw = zw.ToList().Where(x => !(x.Contains("_IDN"))).ToArray();
            virtuosoSoapClient.Close();
            virtuosoSoapClient = null;
            return zw;
        }
        
        public int InsertNewAction(EventOntology.EventAction action, User admin)
        {
            if (admin.SessionNr >0 && admin.EcaDefRight)
            {
                if (string.IsNullOrEmpty(action.ServiceUserName) || string.IsNullOrEmpty(action.ServicePassword))
                {
                    action.ServiceUserName = "default";
                    action.ServicePassword = "default";
                }
                if (string.IsNullOrEmpty(action.X509Password))
                {
                    action.X509Password = "default";
                }

                try
                {
                    string pTypes = "";
                    string pDescr = "";
                    if (action.ParamTypes != null)
                    {
                        for (int i = 0; i < action.ParamTypes.Count; i++)
                        {
                            if (i != 0)
                            {
                                pTypes += ", ";
                                pDescr += ", ";
                            }
                            pTypes += action.ParamTypes[i];
                            pDescr += action.ParamDescription[i];
                        } 
                    }
                    IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                    dbcmd.CommandText = "INSERT INTO EventFrameworkActions(\"Condition\",\"Description\",  \"CreatedBy\",  " +
                        "\"WsdlAddress\",\"EndpointAddress\",  \"MethodeName\",  \"ParamTypes\",  \"ParamDescr\", " +
                        "\"ReturnType\",\"ReturnDescr\",\"SparqlQuery\",  \"UserName\",  \"Password\",  \"X509Password\",\"X509Certificate\", \"DSInstance\") " +
                        "VALUES (0,'" + (action.Description ?? "") + "'," + admin.UserID.ToString() + ",'" + (action.WsdlAddress ?? "") + "','" + 
                        action.EndpointAddress + "','" + (action.MethodeName ?? "") + "','" + pTypes + "','" + pDescr + "','" + action.ReturnType + 
                        "','" + action.ReturnDescription + "','" +  (action.SparqlQuery ?? "") + "','" + (action.ServiceUserName ?? "") + 
                        "', subseq(pwd_magic_calc('" + (action.ServiceUserName ?? "") + "', '" + (action.ServicePassword ?? "") + "'),1),subseq(pwd_magic_calc('" + 
                        (action.ServiceUserName ?? "") + "', '" + (action.X509Password ?? "") + "'),1),'" + (action.X509Certificate ?? "") + "'," + action.DSInstance.ToString() + ")";

                    dbcmd.ExecuteNonQuery();
                    dbcmd.Dispose();
                    dbcmd = null;
                    dbcmd = StaticHelper.dbcon.CreateCommand();
                    dbcmd.CommandText = "SELECT MAX(ActionID) FROM EventFrameworkActions";
                    int zw = (int)(dbcmd.ExecuteScalar());
                    dbcmd.Dispose();
                    dbcmd = null;
                    return zw;
                }
                catch (Exception)
                {
                    return -1;
                } 
            }
            return -1;
        }
        
        public DataTable GetAllTriggers(string likeName)
        {
            //return tableAdapter.GetTriggers().DefaultView.ToTable();

            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            string sql = "SELECT \"TriggerID\",\"TriggerType\",\"TriggerName\",\"AlternativeName\",\"DSInstance\",\"InternalSource\",\"Values\",nullableDateString(\"Created\") as Created,\"CreatedBy\",\"Description\",\"Statement\"" +
                "FROM EventFrameworkTriggers WHERE TriggerName LIKE '%" + likeName + "%' ORDER BY Created DESC";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            DataTable table = new DataTable();
            table.TableName = "Users";
            DataTable schema = reader.GetSchemaTable();
            foreach (DataRow col in schema.Rows)
                table.Columns.Add(col["ColumnName"].ToString(), (col["DataType"] as Type));

            while (reader.Read())
            {
                object[] zw = new object[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)
                    zw[i] = reader[i];
                table.Rows.Add(zw);
            }
            // clean up
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            return table;
        }


        public string[] GetGraphs(int dbInstance)
        {
            EndpointAddress addr = new EndpointAddress(GetRemoteProcedureEndpoint(dbInstance));

            EventFrameworkProceduresDocLiteralPortTypeClient virtuosoSoapClient = new EventFrameworkProceduresDocLiteralPortTypeClient(bind, addr);
            string[] zw = null;

                zw = virtuosoSoapClient.EVENT_FRAMEWORK_GET_GRAPHS((int)(GetControlId(dbInstance)));

            virtuosoSoapClient.Close();
            virtuosoSoapClient = null;
            return zw;
        }

        public DataTable GetActionsOrConditions(Activity act)
        {
            int condition = 2;
            if (act == Activity.Action)
                condition = 0;
            else if (act == Activity.Condition)
                condition = 1;

            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            string sql = "SELECT \"ActionID\",Description,CreatedBy,WsdlAddress,EndpointAddress,MethodeName,SparqlQuery,ParamTypes,ParamDescr,ReturnType,ReturnDescr,UserName,InternalQuery," +
                "subseq(pwd_magic_calc(UserName, \"Password\"),1) AS \"Password\",subseq(pwd_magic_calc(UserName, \"X509Password\"),1) AS \"X509Password\", \"X509Certificate\", \"DSInstance\"" +
                "FROM EventFrameworkActions WHERE     \"Condition\" = " + condition.ToString();
            //Console.WriteLine(sql);
            IDataReader reader = null;
            dbcmd.CommandText = sql;
            try
            {
                reader = dbcmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
            }
            DataTable table = new DataTable();
            table.TableName = "Actions";
            DataTable schema = reader.GetSchemaTable();
            foreach (DataRow col in schema.Rows)
                table.Columns.Add(col["ColumnName"].ToString(), (col["DataType"] as Type));

            while (reader.Read())
            {
                object[] zw = new object[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)
                    zw[i] = reader[i];
                table.Rows.Add(zw);
            }
            // clean up
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            return table;
        }

        public string UpdateAction(EventOntology.EventAction action, User admin)
        {
            if (admin.SessionNr >0 && admin.EcaDefRight)
            {
                if (string.IsNullOrEmpty(action.ServiceUserName) || string.IsNullOrEmpty(action.ServicePassword))
                {
                    action.ServiceUserName = "default";
                    action.ServicePassword = "default";
                }
                if (string.IsNullOrEmpty(action.X509Password))
                {
                    action.X509Password = "default";
                }
                string pTypes = "";
                string pDescr = "";
                if (action.ParamTypes != null)
                {
                    for (int i = 0; i < action.ParamTypes.Count; i++)
                    {
                        if (i != 0)
                        {
                            pTypes += ", ";
                            pDescr += ", ";
                        }
                        pTypes += action.ParamTypes[i];
                        pDescr += action.ParamDescription[i];
                    } 
                }
                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                dbcmd.CommandText = "UPDATE EventFrameworkActions set \"Description\" = '" + action.Description ?? "" + 
                    "', \"WsdlAddress\" = '" + action.WsdlAddress ?? "" + "',\"EndpointAddress\" = '" + action.EndpointAddress +
                    "',  \"MethodeName\" = '" + action.MethodeName ?? "" + "',  \"ParamTypes\" = '" + pTypes + "',  \"ParamDescr\" = '" + 
                    pDescr + "', \"ReturnType\" = '" + action.ReturnType + "',\"ReturnDescr\" = '" + action.ReturnDescription +
                    "',\"SparqlQuery\" = '" + action.SparqlQuery ?? "" + "',  \"UserName\" = '" + action.ServiceUserName ?? "" + 
                    "',  \"Password\" = subseq(pwd_magic_calc('" + action.ServiceUserName ?? "" + "', '" + action.ServicePassword ?? "" +
                    "'),1),  \"X509Password\" = subseq(pwd_magic_calc('" + action.ServiceUserName ?? "" + "', '" + action.X509Password ?? "" + 
                    "'),1), \"X509Certificate\" = '" + action.X509Certificate ?? "" + "' WHERE ActionID = " + action.ID.ToString();
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                dbcmd = null;
                //tableAdapter.UpdateActionByID(action.Description ?? "", action.CreatedBy, action.WsdlAddress, action.EndpointAddress, action.MethodeName, pTypes, pDescr, action.ReturnType,
                //    action.ReturnDescription, action.SparqlQuery ?? "", action.ServiceUserName ?? "", action.ServiceUserName ?? "", action.ServicePassword ?? "", action.ServiceUserName ?? "", action.X509Password ?? "", action.X509Certificate ?? "", action.ID);
                return "done";
             }
            return "you have no right to do this";
        }

        public int InsertNewCondition(ConditionQuery condition, User admin, Activity act)
        {
            if (admin.SessionNr >0 && admin.EcaDefRight)
            {
                if (string.IsNullOrEmpty(condition.ServiceUserName) || string.IsNullOrEmpty(condition.ServicePassword))
                {
                    condition.ServiceUserName = "default";
                    condition.ServicePassword = "default";
                }
                if (string.IsNullOrEmpty(condition.X509Password))
                {
                    condition.X509Password = "default";
                }
                try
                {
                    string pTypes = "";
                    string pDescr = "";
                    if (condition.ParamTypes != null)
                    {
                        for (int i = 0; i < condition.ParamTypes.Count; i++)
                        {
                            if (i != 0)
                            {
                                pTypes += ", ";
                                pDescr += ", ";
                            }
                            pTypes += condition.ParamTypes[i];
                            pDescr += condition.ParamDescription[i];
                        } 
                    }
                    IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                    if(act == Activity.Condition)
                        dbcmd.CommandText = "INSERT INTO EventFrameworkActions(\"Condition\",\"Description\",  \"CreatedBy\",  " +
                        "\"WsdlAddress\",\"EndpointAddress\",  \"MethodeName\",  \"ParamTypes\",  \"ParamDescr\", " +
                        "\"ReturnType\",\"ReturnDescr\",\"SparqlQuery\",  \"UserName\",  \"Password\",  \"X509Password\",\"X509Certificate\", \"DSInstance\") " +
                        "VALUES (1,'" + (condition.Description ?? "") + "'," + admin.UserID.ToString() + ",'" + (condition.WsdlAddress ?? "") + "','" +
                        condition.EndpointAddress + "','" + (condition.MethodeName ?? "") + "','" + pTypes + "','" + pDescr + "','" + condition.ReturnType +
                        "','" + condition.ReturnDescription + "','" + (condition.SparqlQuery ?? "") + "','" + (condition.ServiceUserName ?? "") +
                        "', subseq(pwd_magic_calc('" + (condition.ServiceUserName ?? "") + "', '" + (condition.ServicePassword ?? "") + "'),1),subseq(pwd_magic_calc('" +
                        (condition.ServiceUserName ?? "") + "', '" + (condition.X509Password ?? "") + "'),1),'" + (condition.X509Certificate ?? "") + "'," + condition.DSInstance.ToString() + ")";
                    else if(act == Activity.Query)
                        dbcmd.CommandText = "INSERT INTO EventFrameworkActions(\"Condition\",\"Description\",  \"CreatedBy\",  " +
                        "\"WsdlAddress\",\"EndpointAddress\",  \"MethodeName\",  \"ParamTypes\",  \"ParamDescr\", " +
                        "\"ReturnType\",\"ReturnDescr\",\"SparqlQuery\",  \"UserName\",  \"Password\",  \"X509Password\",\"X509Certificate\", \"DSInstance\") " +
                        "VALUES (2,'" + (condition.Description ?? "") + "'," + admin.UserID.ToString() + ",'" + (condition.WsdlAddress ?? "") + "','" +
                        condition.EndpointAddress + "','" + (condition.MethodeName ?? "") + "','" + pTypes + "','" + pDescr + "','" + condition.ReturnType +
                        "','" + condition.ReturnDescription + "','" + (condition.SparqlQuery ?? "") + "','" + (condition.ServiceUserName ?? "") +
                        "', subseq(pwd_magic_calc('" + (condition.ServiceUserName ?? "") + "', '" + (condition.ServicePassword ?? "") + "'),1),subseq(pwd_magic_calc('" +
                        (condition.ServiceUserName ?? "") + "', '" + (condition.X509Password ?? "") + "'),1),'" + (condition.X509Certificate ?? "") + "'," + condition.DSInstance.ToString() + ")";
                    else
                        return 0;

                    dbcmd.ExecuteNonQuery();
                    dbcmd.Dispose();
                    dbcmd = null;
                    dbcmd = StaticHelper.dbcon.CreateCommand();
                    dbcmd.CommandText = "SELECT MAX(ActionID) FROM EventFrameworkActions";
                    int zw = (int)(dbcmd.ExecuteScalar());
                    dbcmd.Dispose();
                    dbcmd = null;
                    return zw;
                }
                catch (Exception ex)
                {
                    return -1;
                } 
            }
            return -1;
        }
        
        public DataTable GetEventsBetween(DateTime From, DateTime To)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            string sql = "SELECT \"EventID\", \"TriggerID\", \"CEID\", nullableDateString(\"Occurence\"), \"DSInstance\", \"InternalSource\",  DUMP_VEC(\"Row\") AS RowValue " +
                "FROM  EventFrameworkEvents WHERE Occurence BETWEEN stringdate('" + From.ToString("yyyy-MM-dd' 'HH:mm:ss") + "') AND stringdate('" + To.ToString("yyyy-MM-dd' 'HH:mm:ss") + "');";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            DataTable table = new DataTable();
            table.TableName = "AtomicEvents";
            DataTable schema = reader.GetSchemaTable();
            foreach (DataRow col in schema.Rows)
                table.Columns.Add(col["ColumnName"].ToString(), (col["DataType"] as Type));

            while (reader.Read())
            {
                object[] zw = new object[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)
                    zw[i] = reader[i];
                table.Rows.Add(zw);
            }
            // clean up
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            return table;
        }


        public string DropTrigger(AtomicEvent trigger, User admin)
        {
            if (admin.SessionNr >0 && admin.EcaDefRight)
            {
                EndpointAddress addr = new EndpointAddress(GetRemoteProcedureEndpoint(trigger.dsInstance));

                EventFrameworkProceduresDocLiteralPortTypeClient virtuosoSoapClient = new EventFrameworkProceduresDocLiteralPortTypeClient(bind, addr);
                string zw = null;

                    zw = virtuosoSoapClient.EVENT_FRAMEWORK_DROP_TRIGGER(GetControlId(trigger.dsInstance), trigger.triggerName);

                virtuosoSoapClient.Close();
                virtuosoSoapClient = null;

                if (bool.Parse(zw))
                {
                    IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                    dbcmd.CommandText = "DELETE FROM EventFrameworkTriggers WHERE TriggerName = '" + trigger.triggerName + "'";
                    dbcmd.ExecuteNonQuery();
                    dbcmd.Dispose();
                    dbcmd = null;
                    return "done";
                }
                return " an error occured";
            }
            return "you have no right to do this";
        }

        public DataTable GetActionById(int actionID)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            string sql = "SELECT ActionID,Description,CreatedBy,WsdlAddress,EndpointAddress,MethodeName,SparqlQuery,ParamTypes," + 
                "ParamDescr,ReturnType,ReturnDescr,UserName,InternalQuery, subseq(pwd_magic_calc(UserName, \"Password\"),1) AS \"Password\"," +
                "subseq(pwd_magic_calc(UserName, \"X509Password\"),1) AS \"X509Password\", \"X509Certificate\", \"DSInstance\" FROM EventFrameworkActions  WHERE ActionID = " + actionID.ToString();
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            DataTable table = new DataTable();
            table.TableName = "zzw";
            DataTable schema = reader.GetSchemaTable();
            foreach (DataRow col in schema.Rows)
                table.Columns.Add(col["ColumnName"].ToString(), (col["DataType"] as Type));

            while (reader.Read())
            {
                object[] zw = new object[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)
                    zw[i] = reader[i];
                table.Rows.Add(zw);
            }
            // clean up
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            return table;
        }

        private string intro = "@prefix shma: <http://EventFramework/Schema/> .\n @prefix link: <http://EventFramework/LinkedData/> .\n " +
                "@prefix : <http://EventFramework/Stages/> . \n";

        public DataTable GetComplexEvents(string likeName = "", int ceid = 0)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            string sql = "SELECT \"CEID\",\"Name\",\"InitialStage\",\"CreatedBy\",\"Recurrences\",\"Period\",nullableDateString(\"InitializeAt\") as InitializeAt," +
                "\"Description\",\"IsActive\",\"IsOverlapping\" FROM EventFrameworkComplexEvents WHERE Name LIKE '%" + likeName + "%'";
            if(ceid > 0)
                sql += " AND CEID = " + ceid.ToString();
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            DataTable table = new DataTable();
            table.TableName = "ConolexEvents";
            DataTable schema = reader.GetSchemaTable();
            foreach (DataRow col in schema.Rows)
                table.Columns.Add(col["ColumnName"].ToString(), (col["DataType"] as Type));

            while (reader.Read())
            {
                object[] zw = new object[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)
                    zw[i] = reader[i];
                table.Rows.Add(zw);
            }
            // clean up
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            return table;
        }

        private string insertNextInstance(string setType, string idType)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            dbcmd.CommandText = "SELECT EVENT_FRAMEWORK_INSERT_NEXT_INSTANCE('" + setType + "','" + idType + "')";
            string zw = dbcmd.ExecuteScalar().ToString();
            dbcmd.Dispose();
            dbcmd = null;
            return zw;
        }

        private string insertNewEventSetReturnId (InitialEventSet set)
        {
            string syn = intro;
            string setId = null;
            string internalSet = null;
            string internalEvent = null;
            if (set is MultiSet)
            {
                //insert new instance of 'EventSet'
                setId = ":MultiEventSet" + insertNextInstance("MultiEventSet", "setId");
                syn += setId + "   ";
                syn += "shma:operator   shma:" + (set as EventSet).Operator.ToString() + "; ";
                syn += "shma:minRecurrence   \"" + (set as MultiSet).MinCardinality.ToString() + "\"^^xsd:integer; ";
                syn += "shma:maxRecurrence   \"" + (set as MultiSet).MaxCardinality.ToString() + "\"^^xsd:integer; ";
                internalSet = ":InitialEventSet" + insertNextInstance("InitialEventSet", "setId");
                syn += "shma:operands   " + internalSet + " . ";

                if ((set as EventSet).Events.Count > 1)
                {
                    throw new Exception("MultiSet has more than 1 operand!");
                }
            }
            else if (set is EventSet)
            {
                if (setId == null)
                {
                    setId = ":EventSet" + insertNextInstance("EventSet", "setId");
                    syn += setId + "   ";
                    syn += "shma:operator   shma:" + (set as EventSet).Operator.ToString() + "; ";
                }
                
                internalSet = ":InitialEventSet" + insertNextInstance("InitialEventSet", "setId");
                syn += "shma:operands   " + internalSet + " . ";

            }
            if (setId != null)
            {
                Regex zahl = new Regex("[0-9]+");
                set.PreSetID = set.SetID;
                set.SetID = int.Parse(zahl.Match(setId).Value);
            }

            if (set.Events != null)
            {
                syn += internalSet + "   ";
                foreach (IEventSetMember member in set.Events)
                {
                    if (member is AtomicEvent)
                        syn += "shma:initialOr  link:AtomicEvent" + (member as AtomicEvent).triggerId.ToString() + "; ";
                    else if (member is ComplexEvent)
                        syn += "shma:initialOr  link:ComplexEvent" + (member as ComplexEvent).CeId.ToString() + "; ";
                    else if (member is EventSet)
                    {
                        internalEvent = insertNewEventSetReturnId((member as EventSet));
                        syn += "shma:initialOr   " + internalEvent + "; ";
                    }
                    else if (member is InitialEventSet)
                    {
                        internalEvent = insertNewEventSetReturnId((member as InitialEventSet));
                        syn += "shma:initialOr   " + internalEvent + "; ";
                    }
                } 
            }
            int lastSemi = syn.LastIndexOf(';');
            if (lastSemi > 10)
            {
                    IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                    dbcmd.CommandText = "SELECT TTLP_MT ('" + syn.Substring(0, lastSemi) + "." + "', '','" + insertGraph + "')";
                    dbcmd.ExecuteNonQuery();
                    dbcmd.Dispose();
                    dbcmd = null;

                    return setId;
            }
            return null;
        }

        private string insertNewStageReturnUri(InitialStage stage)
        {
            string syn = intro;
            string stageId = null;
            string nextStage =null;
            string setId = null;

            if (stage is Stage)
            {
                setId = insertNewEventSetReturnId((stage as Stage).InitialEventSet);
                stageId = insertNextInstance("Stage", "stageID");
                if ((stage as Stage).TimeRestriction != null)
                {
                    syn += "_:y1	a		shma:Time; " +
                        "shma:waitTillEnd		\"" + Convert.ToInt32((stage as Stage).TimeRestriction.WaitTillEnd).ToString() + "\"^^xsd:integer; " +
                        "shma:timeDuration      \"" + Time.TimeSpanToXsdDuration((stage as Stage).TimeRestriction.Duration) + "\"^^xsd:dayTimeDuration. ";
                }
                syn += ":Stage" + stageId + "   shma:initialEventSet    " + setId + "; ";

                if ((stage as Stage).TimeRestriction != null)
                    syn += "shma:timeRestriction     _:y1;   ";
            }
            else if (stage is InitialStage)
            {
                setId = insertNewEventSetReturnId((stage as InitialStage).InitialEventSet);
                stageId = insertNextInstance("InitialStage", "stageID");

                syn += ":InitialStage" + stageId.ToString() + "       shma:initialEventSet      " + setId + "; ";
            }
            if (stage.Actions != null && stage.NextStage == null)
            {
                foreach (EventAction act in stage.Actions)
                {
                    syn += " shma:takeAction   link:Action" + act.ID.ToString() + "; ";
                } 
            }
            if (stage.ConditionQuerys != null)
            {
                foreach (ConditionQuery query in stage.ConditionQuerys)
                {
                    syn += " shma:hasCondition    link:ConditionQuery" + query.ID.ToString() + "; ";
                } 
            }
            if (stage.NextStage != null && (stage.Actions == null || stage.Actions.Count == 0))
            {
                nextStage = insertNewStageReturnUri(stage.NextStage);
                nextStage = nextStage.Substring(nextStage.LastIndexOf('e') + 1);
                syn += " shma:transition      :Stage" + nextStage + "; ";
            }
            int lastSemi = syn.LastIndexOf(';');
            if (lastSemi > 0)
            {
                syn = syn.Remove(lastSemi, 1).Insert(lastSemi, ".");
            }
            else
                return null;

            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            dbcmd.CommandText = "SELECT TTLP_MT ('" + syn + "', '','" + insertGraph + "')";
            dbcmd.ExecuteNonQuery();
            dbcmd.Dispose();
            dbcmd = null;
            string zw = stage.GetType().ToString();
            zw = zw.Substring(zw.LastIndexOf('.') + 1);
            return "http://EventFramework/Stages/" + zw + stageId;
            
        }

        public string InsertComplexEvent(User admin, ComplexEvent ev)
        {
            if (admin.SessionNr > 0 && admin.EcaDefRight)
            {
                string stageId = insertNewStageReturnUri(ev.InStage);
                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();

                try
                {
                    dbcmd.CommandText = "INSERT INTO EventFrameworkComplexEvents (\"Name\",\"InitialStage\",\"CreatedBy\",\"Description\",\"IsActive\",\"IsOverlapping\",\"Recurrences\", \"Period\", \"InitializeAt\")" +
                        "VALUES ('" + ev.Name + "', '" + stageId + "', " + admin.UserID.ToString() + ",'" + ev.Description + "', COALESCE(" + Convert.ToInt32(ev.IsActive).ToString() + ", 0)," +
                        "COALESCE(" + Convert.ToInt32(ev.IsOverlapping).ToString() + ",0), " + ev.Recurrence.ToString() + ", '" + ev.Period + "', stringdate('" + ev.InitializeAt.ToString("yyyy-MM-dd' 'HH:mm:ss") + "'))";
                    dbcmd.ExecuteNonQuery();

                    //tableAdapter.InsertComplexEvent(ev.Name, stageId, admin.UserID, ev.Description, 
                    //    Convert.ToInt32(ev.IsActive), Convert.ToInt32(ev.IsOverlapping), ev.Recurrence, ev.Period, ev.InitializeAt);

                    dbcmd.CommandText = "SELECT TOP 1 CEID FROM EventFrameworkComplexEvents WHERE InitialStage = '" + stageId + "'";
                    object ceid = dbcmd.ExecuteScalar();

                    if (ev.Period.Trim() != "PT" && ev.InitializeAt != null && (DateTime.UtcNow).Add(new TimeSpan(0, 1, 0)) < ev.InitializeAt)
                    {
                        TimeSpan ts = Time.XsdDurationToTimeSpan(ev.Period);
                        if (ts > TimeSpan.Zero)
                        {
                            //
                            //UTC_TOME oder now() ???
                            //
                            dbcmd.CommandText = "INSERT INTO SYS_SCHEDULED_EVENT (SE_NAME, SE_SQL, SE_START, SE_INTERVAL) VALUES ('EventFrameworkPeriod_" + stageId.Substring(stageId.LastIndexOf('/') + 1) +
                                "','EVENT_FRAMEWORK_INSERT_EVENT_INSTANCE(\'" + stageId + "\', 0, 0, DATEADD(\'minute\', (CAST((select cfg_item_value (virtuoso_ini_path (),\'Parameters\',\'SchedulerInterval\')) as INTEGER)" +
                                " - DATEDIFF(\'minute\', stringdate(\''|| CAST(UTC_TIME() as varchar) ||'\'), UTC_TIME())), UTC_TIME()), 1)', DATEADD('minute', " +
                                " (CAST((select cfg_item_value (virtuoso_ini_path (),'Parameters','SchedulerInterval')) as INTEGER)*(-1)), stringdate('" + ev.InitializeAt.ToString("yyyy-MM-dd' 'HH:mm:ss") + "')), " + ((int)(ts.TotalMinutes)).ToString() + ")";
                            dbcmd.ExecuteNonQuery();
                            //tableAdapter.InsertInstancePeriod(stageId.Substring(stageId.LastIndexOf('/') + 1), stageId, ev.InitializeAt, (int)(ts.TotalMinutes));
                        }
                    }
                    else if (ev.IsActive)
                    {
                        InsertCeInstance(admin, (int)ceid, stageId, 0, ev.IsOverlapping);
                    }

                    ev.CeId = (int)ceid;
                    updateParamMapping(admin, ev);
                }
                catch (Exception ex )
                {
                    dbcmd.Dispose();
                    dbcmd = null;
                    if (ex.Message.ToLower().Contains("uniqu"))
                        return "This name is already taken, please enter a new one.";
                    else
                        return "An unknowen DB-Error occured: \n" + ex.Message;
                }
                dbcmd.Dispose();
                dbcmd = null;
                return "Event has been added.";
            }
            return "you have no right to do this";
        }

        private bool hasEventSetChanged(InitialEventSet n, InitialEventSet o)
        {
            if (n.Events == null)
            {
                if (o.Events == null)
                    return false;
                else
                    return true;
            }
            else
                if (o.Events == null)
                    return true;

            if(!(n.GetType() != o.GetType()))
                return true;

            if (n is MultiSet)
                if ((n as MultiSet).MaxCardinality != (o as MultiSet).MaxCardinality || (n as MultiSet).MinCardinality != (o as MultiSet).MinCardinality)
                    return true;
            if (n is EventSet)
                if ((n as EventSet).Operator != (o as EventSet).Operator)
                    return true;
            if (n.Events.Count != o.Events.Count)
                return true;

            for (int i = 0; i < n.Events.Count; i++)
            {
                if (n.Events[i] is AtomicEvent)
                {
                    if (o.Events.Where(x => x is AtomicEvent).Cast<AtomicEvent>().Where(y => y.triggerId == (n.Events[i] as AtomicEvent).triggerId).Count() != 1)
                        return true;
                }
                else if (n.Events[i] is ComplexEvent)
                {
                    if (o.Events.Where(x => x is ComplexEvent).Cast<ComplexEvent>().Where(y => y.CeId == (n.Events[i] as ComplexEvent).CeId).Count() != 1)
                        return true;
                }
                else if (n.Events[i] is EventSet)
                {
                    EventSet old = o.Events.Where(x => x is EventSet).Cast<EventSet>().Where(y => y.SetID == (n.Events[i] as EventSet).SetID).First();
                    return hasEventSetChanged((n.Events[i] as EventSet), old);
                }
            }
            return false;
        }

        private bool haveActionsChanged(List<EventAction> n, List<EventAction> o, Activity acttivity)
        {
            if (n == null)
            {
                if (o == null)
                    return false;
                else
                    return true;
            }
            else
                if (o == null)
                    return true;

            if(n.Count != o.Count)
                return true;
                foreach(EventAction act in n)
                {
                    if (o.Where(x=>x.ID == act.ID).Count() != n.Where(y=>y.ID == act.ID).Count())
                        return true;
                }
            
            return false;
        }

        private bool haveStagesChanged(ComplexEvent newEv, ComplexEvent oldEv)
        {
            if (newEv.Stages.Count != oldEv.Stages.Count)
                return true;
            for (int i = 0; i < newEv.Stages.Count; i++)
            {
                if (newEv.Stages[i].TimeRestriction != null)
                    if (newEv.Stages[i].TimeRestriction.WaitTillEnd != oldEv.Stages[i].TimeRestriction.WaitTillEnd || newEv.Stages[i].TimeRestriction.Duration.TotalSeconds != oldEv.Stages[i].TimeRestriction.Duration.TotalSeconds)
                        return true;
                if (hasEventSetChanged(newEv.Stages[i].InitialEventSet, oldEv.Stages[i].InitialEventSet))
                    return true;
            }
            return false;
        }

        private bool hasParamMappingChanged(List<ParameterMap> n, List<ParameterMap> o)
        {
            if (n.Count != o.Count)
                return true;
            foreach (ParameterMap map in n)
            {
                if (o.Where(x => x.ActionId == map.ActionId && x.ActionNr == map.ActionNr && x.Ceid == map.Ceid).Count() != 1)
                {
                    if(map.StaticValue != null)
                        if(n.Where(x=>x.StaticValue == map.StaticValue).Count() != o.Where(y=>y.StaticValue == map.StaticValue).Count())
                            return true;
                    if(map.ConditionaQuery > 0)
                        if (n.Where(x => x.ConditionaQuery == map.ConditionaQuery).Count() != o.Where(y => y.ConditionaQuery == map.ConditionaQuery).Count())
                            return true;
                    if(!string.IsNullOrEmpty(map.EventValueMap))
                        if (n.Where(x => x.EventValueMap == map.EventValueMap).Count() != o.Where(y => y.EventValueMap == map.EventValueMap).Count())
                            return true;
                }
            }
            return false;
        }

        public string UpdateComplexEvents(User admin, ComplexEvent newEv, ComplexEvent oldEv)
        {
            bool updated = false;
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            if (admin.SessionNr > 0 && admin.EcaDefRight)
            {
                string initialStageUri = null;
                if (haveStagesChanged(newEv, oldEv))
                {
                    updated = true;
                    deleteEventStages(oldEv.Stages);
                    initialStageUri = insertNewStageReturnUri(newEv.InStage);
                }
                else
                    initialStageUri = newEv.InitialStagUri;

                if (!updated)
                    for (int i = 0; i < newEv.Stages.Count; i++)
                        if (haveActionsChanged(newEv.Stages[i].Actions, oldEv.Stages[i].Actions, Activity.Action)
                            || haveActionsChanged(newEv.Stages[i].ConditionQuerys.Cast<EventAction>().ToList(), oldEv.Stages[i].ConditionQuerys.Cast<EventAction>().ToList(), Activity.Condition))
                        {
                            deleteEventStages(oldEv.Stages);
                            initialStageUri = insertNewStageReturnUri(newEv.InStage);
                            break;
                        }

                if (newEv.Stages.Count == oldEv.Stages.Count)
                {
                    for (int i = 0; i < newEv.Stages.Count; i++)
                        if (hasParamMappingChanged(newEv.Stages[i].ParameterMappings, oldEv.Stages[i].ParameterMappings))
                        {
                            updateParamMapping(admin, newEv);
                            break;
                        }
                }
                else
                    updateParamMapping(admin, newEv);


                dbcmd.CommandText = "UPDATE EventFrameworkComplexEvents SET \"Name\" = '" + newEv.Name + "',\"InitialStage\" = '" + initialStageUri + "', \"Description\" = '" + newEv.Description +
                    "', \"IsActive\" = " + Convert.ToInt16(newEv.IsActive).ToString() + ", \"IsOverlapping\" = " + Convert.ToInt16(newEv.IsOverlapping).ToString() + ",\"Recurrences\" = " +
                    newEv.Recurrence.ToString() + ",\"Period\" = '" + newEv.Period + "',\"InitializeAt\" = stringdate('" + newEv.InitializeAt.ToString("yyyy-MM-dd' 'HH:mm:ss") + "')  WHERE CEID = " + newEv.CeId.ToString();

                dbcmd.ExecuteNonQuery();
                return "updated!";
            }
            return "you have no right to do this";
        }

        private List<InitialEventSet> getEventSets(InitialEventSet set)
        {
            List<InitialEventSet> list = new List<InitialEventSet>();
            list.Add(set);
            foreach (IEventSetMember memb in set.Events)
                if ((memb is InitialEventSet))
                    list.AddRange(getEventSets(memb as InitialEventSet));
            return list;
        }

        private void updateParamMapping(User admin, ComplexEvent newEv)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            dbcmd.CommandText = "DELETE FROM EventFrameworkParameterMappings WHERE CEID = " + newEv.CeId.ToString();
            dbcmd.ExecuteNonQuery(); 
            //tableAdapter.DeleteParamMappings(newEv.CeId);

            for (int i = 0; i < newEv.Stages.Count; i++)
            {
                if ((newEv.Stages[i] as InitialStage).ParameterMappings != null)
                {
                    foreach (object obj in (newEv.Stages[i] as InitialStage).ParameterMappings)
                    {
                        ParameterMap map = (obj as ParameterMap);
                        List<InitialEventSet> allSets = new List<InitialEventSet>(getEventSets(newEv.Stages[i].InitialEventSet));
                        //if((allSets.Where(x => x.PreSetID != null && x.PreSetID > 0).Count() > 0))
                        //{
                            if (map.EventValueMap != null)
                            {
                                map.EventValueMap = overhaulEventValueMap(map.EventValueMap, allSets, newEv.CeId);
                            }
                            if(map.ConditionaQuery > 0)
                            {
                                DataTable q = GetActionById(map.ConditionaQuery);
                                ConditionQuery query = new ConditionQuery(q, 0);
                                query.SparqlQuery = overhaulEventValueMap(query.SparqlQuery, allSets, newEv.CeId);
                                UpdateAction(query, admin);
                            }
                        //}
                        string value = "", type = "";
                        if (map.StaticValue != null)
                        {
                            type = map.StaticValue.GetType().ToString();
                            if (type.Contains("String"))
                                value = map.StaticValue.ToString().Replace("'", "\\'");
                            else
                                value = map.StaticValue.ToString();
                        }
                        else
                        {
                            value = "NULL";
                            type = "NULL";
                        }
                        dbcmd.CommandText = "INSERT INTO EventFrameworkParameterMappings VALUES(" + newEv.CeId.ToString() + "," + i.ToString() + "," +
                            map.ActionId.ToString() + "," + map.ActionNr.ToString() + "," + map.ParamNr.ToString() +
                            ",'" + map.Description + "','" + value + "','" + type + "'," + map.ConditionaQuery.ToString() + "," + ((map.EventValueMap == null) ? "NULL" : "'" 
                            + map.EventValueMap.Replace("'", "\\'") + "'") + ")";
                        dbcmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private string overhaulEventValueMap(string originalMap, List<InitialEventSet> sets, int ceid)
        {
            Regex setEx = new Regex("(CE|MS|ES)[0-9]+");
            MatchCollection matches = setEx.Matches(originalMap);
            foreach (InitialEventSet set in sets)
                if (set.PreSetID != null && set.PreSetID > 0)
                {
                    IEnumerable<Match> zw = matches.Cast<Match>().Where(x => int.Parse(x.Value.Substring(2)) == set.PreSetID && !x.Value.Contains("CE")).OrderByDescending(y => y.Index);
                    for (int j = 0; j < zw.Count(); j++)
                    {
                        originalMap = originalMap.Remove(zw.ElementAt(j).Index, zw.ElementAt(j).Length);
                        originalMap = originalMap.Insert(zw.ElementAt(j).Index, zw.ElementAt(j).Value.Substring(0, 2) + set.SetID.ToString());
                    }
                }
            if (matches.Cast<Match>().Where(x => x.Value == "CE0").Count() == 1)
            {
                Match zw = matches.Cast<Match>().Where(x => x.Value == "CE0").First();
                originalMap = originalMap.Remove(zw.Index, zw.Length);
                originalMap = originalMap.Insert(zw.Index, zw.Value.Substring(0, 2) + ceid.ToString());
            }
            return originalMap;
        }
        
        public int GetNextUriNr(string Property)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            dbcmd.CommandText = "SELECT CASE WHEN \"zz\"  IS NULL THEN 1 ELSE \"zz\" END FROM( SELECT  MIN(\"s_19_2\".\"zz\") AS \"zz\"  FROM (SELECT ( CAST ( " + 
                "regexp_match ( '[[:digit:]*]' , __rdf_strsqlval ( \"s_19_1_t0\".\"O\" , 0)) AS INTEGER) + 1) AS \"zz\" FROM DB.DBA.RDF_QUAD AS \"s_19_1_t0\" " +
	            "WHERE \"s_19_1_t0\".\"G\" = __i2idn ( __bft( 'http://EventFramework/Stages' , 1)) AND \"s_19_1_t0\".\"P\" = __i2idn ( __bft( 'http://EventFramework/Schema/" + Property + "' , 1)) " +
		        "OPTION (QUIETCAST) ) AS \"s_19_2\" WHERE \"s_19_2\".zz not in (SELECT * FROM( SELECT CAST ( regexp_match ( '[[:digit:]*]' , __rdf_strsqlval ( \"s_29_4_t1\".\"O\" , 0)) AS INTEGER) AS \"zz\" " +
		        "FROM DB.DBA.RDF_QUAD AS \"s_29_4_t1\" WHERE \"s_29_4_t1\".\"G\" = __i2idn ( __bft( 'http://EventFramework/Stages' , 1)) AND \"s_29_4_t1\".\"P\" = " +
                "__i2idn ( __bft( 'http://EventFramework/Schema/" + Property + "' , 1)) OPTION (QUIETCAST) ) AS \"s_29_5\" ) OPTION (QUIETCAST)) AS jj ";
            object zw = dbcmd.ExecuteScalar();
            dbcmd.Dispose();
            dbcmd = null;
            return (int)zw;
        }
        
        public DataTable GetStagesAndSets(string initialStageUri, int stageNr)
        {
            //Console.WriteLine(initialStageUri);
            //Console.WriteLine(stageNr.ToString());
            DataTable table = GetSetsOfStage(initialStageUri);
            try
            {
                //Console.WriteLine(table.Rows[0][0].ToString());
                table = getEventsFromStage(table.Rows.Cast<DataRow>().Where(x => int.Parse(x[Constants.getSetsOfStage_Distance].ToString()) == stageNr).First()[Constants.getSetsOfStage_Stage].ToString());
            }
            catch (InvalidOperationException)
            {
                return null;
            }
            if (table == null || table.Rows.Count < 1)
                return null;
            //if (stageNr > int.Parse(table.Rows[0][0].ToString()))
            //    return null;
            return table;
        }

        private DataTable getEventsFromStage(string stageUri)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            DataTable table = new DataTable();
            try
            {
                dbcmd.CommandText = "CALL EVENT_FRAMEWORK_GET_EVENTS_FROM_STAGE('" + stageUri + "', 0,0,1)";
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("sequence"))
                    return null;
            }
            IDataReader reader = dbcmd.ExecuteReader();
            table.TableName = "StageEvents";
            DataTable schema = reader.GetSchemaTable();
            if (schema == null)
                return table;

            foreach (DataRow col in schema.Rows)
                table.Columns.Add(col["ColumnName"].ToString(), (col["DataType"] as Type));

            while (reader.Read())
            {
                object[] zw = new object[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)
                    zw[i] = reader[i];
                table.Rows.Add(zw);
            }

            // clean up
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            return table;
        }

        public DataTable GetSetsOfStage(string initialStageUri)
        {
            string sql = "CALL EVENT_FRAMEWORK_GET_SETS_OF_STAGE('" + initialStageUri + "', 1) ";

            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            dbcmd.CommandText = sql;
            //(StaticHelper.dbcon as SqlConnection).Open();
            IDataReader reader = dbcmd.ExecuteReader();
            DataTable table = new DataTable();
            DataTable schema = reader.GetSchemaTable();
            //Console.WriteLine(schema.Rows.Count.ToString());
            foreach (DataRow col in schema.Rows)
            {
                table.Columns.Add(col["ColumnName"].ToString(), (col["DataType"] as Type));
                //Console.WriteLine(col["ColumnName"].ToString());
            }

            while (reader.Read())
            {
                //Console.WriteLine(reader["dist"].ToString());
                object[] zw = new object[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    //Console.WriteLine(reader[i].ToString());
                    zw[i] = reader[i];
                }
                table.Rows.Add(zw);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            return table;
        }


        public string InsertCeInstance(User admin, int ceid, string initialStageUri, int startAtStage = 0, bool isOverlapping = false)
        {
            if (admin.SessionNr > 0 && admin.EcaDefRight)
            {
                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                dbcmd.CommandText = "SELECT EVENT_FRAMEWORK_INSERT_EVENT_INSTANCE('" + initialStageUri + "'," + ceid.ToString() + "," + startAtStage.ToString() 
                    + ",stringdate('" + DateTime.UtcNow.ToString("yyyy-MM-dd' 'HH:mm:ss") + "')," + Convert.ToInt16(isOverlapping) + ")";
                object zw = dbcmd.ExecuteScalar();
                dbcmd.Dispose();
                dbcmd = null;
                return zw.ToString();
                //return tableAdapter.InsertNewEventInstance(initialStageUri, ceid, startAtStage, DateTime.Now, 0).ToString();
            }
            return "you have no right to do this";
        }


        public string DeleteComplexEvent(User admin, ComplexEvent ev)
        {
            if (admin.SessionNr > 0 && admin.EcaDefRight)
            {
                deleteEventStages(ev.Stages);
                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                dbcmd.CommandText = "DELETE FROM EventFrameworkParameterMappings WHERE CEID = " + ev.CeId.ToString();
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                dbcmd = null;
                dbcmd = StaticHelper.dbcon.CreateCommand();
                dbcmd.CommandText = "DELETE FROM EventFrameworkComplexEvents WHERE CEID = " + ev.CeId.ToString();
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                dbcmd = null;
                dbcmd = StaticHelper.dbcon.CreateCommand();
                dbcmd.CommandText = "SELECT CASE WHEN EXISTS (SELECT * FROM EventFrameworkComplexEvents WHERE CEID = " + ev.CeId.ToString() + ") THEN 0 ELSE 1 END";
                object zw = dbcmd.ExecuteScalar();
                dbcmd.Dispose();
                dbcmd = null;
                return zw.ToString();
            }
            return "you have no right to do this";
        }

        private void deleteEventStages(List<InitialStage> stages)
        {
            List<string> subjects = new List<string>();
            for (int i=0;i<stages.Count;i++)
            {
                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                DataTable stag = GetSetsOfStage(stages[i].StageUri);
                subjects.Add(stag.Rows.Cast<DataRow>().Where(x => x["dist"].ToString() == "0").First()["stage"].ToString());
                subjects.Add(stag.Rows.Cast<DataRow>().Where(x => x["dist"].ToString() == "0").First()["initialSet"].ToString());

                DataTable sets = getEventsFromStage(stages[i].StageUri);
                foreach (DataRow set in sets.Rows) 
                {
                    if (DBNull.Value != set["initialEventSet"])
                    {
                        subjects.Add(set["initialEventSet"].ToString());
                    }
                    subjects.Add(set["event"].ToString());
                }

                foreach (string subj in subjects)
                {
                    dbcmd.Dispose();
                    dbcmd = null;
                    dbcmd = StaticHelper.dbcon.CreateCommand();
                    dbcmd.CommandText = "SELECT EVENT_FRAMEWORK_DELETE_STAGES_EVENTS('" + subj + "')";
                    dbcmd.ExecuteScalar();
                }
                dbcmd.Dispose();
                dbcmd = null;
            }
        }

        public string ActivateComplexEvent(User admin, ComplexEvent ev)
        {
            if (admin.SessionNr > 0 && admin.EcaDefRight)
            {
                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                dbcmd.CommandText = "UPDATE EventFrameworkComplexEvents SET \"Name\" = '" + ev.Name + "',\"InitialStage\" = '" + ev.InitialStagUri + "', \"Description\" = '" + ev.Description +
                    "', \"IsActive\" = " + Convert.ToInt16(ev.IsActive).ToString() + ", \"IsOverlapping\" = " + Convert.ToInt16(ev.IsOverlapping).ToString() + ",\"Recurrences\" = " +
                    ev.Recurrence.ToString() + ",\"Period\" = '" + ev.Period + "',\"InitializeAt\" = stringdate('" + ev.InitializeAt.ToString("yyyy-MM-dd' 'HH:mm:ss") + "')  WHERE CEID = " + ev.CeId.ToString();
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                dbcmd = null;

                InsertCeInstance(admin, ev.CeId, ev.InitialStagUri, 0, ev.IsOverlapping);

                return "activated";
            }
            return "you have no right to do this";
        }
        
        public DataTable GetParamMappings(int ceid)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            string sql = "SELECT CEID,(SELECT Condition FROM EventFrameworkActions as dd WHERE dd.ActionID = vv.ActionID) AS Condition,StageNr,ActionID,ActionNr,ParamNr,Description" +
                ",StaticValue,StaticValueType,ConditionQuery,EventValueMap  FROM EventFrameworkParameterMappings as vv WHERE CEID = " + ceid.ToString();
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            DataTable table = new DataTable();
            table.TableName = "Mappings";
            DataTable schema = reader.GetSchemaTable();
            foreach (DataRow col in schema.Rows)
                table.Columns.Add(col["ColumnName"].ToString(), (col["DataType"] as Type));

            while (reader.Read())
            {
                object[] zw = new object[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)
                    zw[i] = reader[i];
                table.Rows.Add(zw);
            }
            // clean up
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            return table;
        }
        
        public string DeActivateComplexEvent(User admin, ComplexEvent ev)
        {
            if (admin.SessionNr > 0 && admin.EcaDefRight)
            {
                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                dbcmd.CommandText = "UPDATE EventFrameworkComplexEvents SET \"Name\" = '" + ev.Name + "',\"InitialStage\" = '" + ev.InitialStagUri + "', \"Description\" = '" + ev.Description +
                    "', \"IsActive\" = " + Convert.ToInt16(ev.IsActive).ToString() + ", \"IsOverlapping\" = " + Convert.ToInt16(ev.IsOverlapping).ToString() + ",\"Recurrences\" = " +
                    ev.Recurrence.ToString() + ",\"Period\" = " + ev.Period + ",\"InitializeAt\" = stringdate('" + ev.InitializeAt.ToString("yyyy-MM-dd' 'HH:mm:ss") + "')  WHERE CEID = " + ev.CeId.ToString();
                dbcmd.ExecuteNonQuery();

                dbcmd.CommandText = "DELETE FROM EventFrameworkComplexEventInstances WHERE CEID = " + ev.CeId.ToString();
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                dbcmd = null;
                return "event-instance was deactivated";
            }
            return "you have no right to do this";
        }

        public DataTable GetComplexEventInstances(int ceid = 0)
        {
            string sql = "";
            if (ceid == 0)
                sql = " SELECT EventID,CEID,EventUri,FirstStageUri,CurrentStage,nullableDateString(Started) AS Started, nullableDateString(Finished) AS Finished FROM EventFrameworkComplexEventInstances " + 
                    "WHERE CEID IN (SELECT CEID FROM  EventFrameworkComplexEventInstances WHERE CEID IS NOT NULL AND CEID >= 0 AND CEID <= 2000000000)";   
            else
                sql = " SELECT EventID,CEID,EventUri,FirstStageUri,CurrentStage,nullableDateString(Started) AS Started,nullableDateString(Finished) AS Finished FROM EventFrameworkComplexEventInstances " + 
                    "WHERE CEID IN (SELECT CEID FROM  EventFrameworkComplexEventInstances WHERE CEID IS NOT NULL AND CEID >= " + ceid.ToString() + " AND CEID <= " + ceid.ToString() + ")";

            StaticHelper.writeToConsole(sql);
            
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            DataTable table = new DataTable();
            table.TableName = "CeInstance";
            DataTable schema = reader.GetSchemaTable();
            foreach (DataRow col in schema.Rows)
                    table.Columns.Add(col["ColumnName"].ToString(), (col["DataType"] as Type));

            while (reader.Read())
            {
                object[] zw = new object[table.Columns.Count];
                for (int i = 0; i < table.Columns.Count; i++)
                    zw[i] = reader[i];
                table.Rows.Add(zw);
            }
            // clean up
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            return table;
        }
        
        public string DeleteDataSource(User admin, int instanceID)
        {
            if (admin.SessionNr > 0 && admin.EcaDefRight)
            {
                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                dbcmd.CommandText = "DELETE FROM EventFrameworkDataSources WHERE DSInstance = " + instanceID.ToString();
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                dbcmd = null;
                dbcmd = StaticHelper.dbcon.CreateCommand();
                dbcmd.CommandText = "DELETE FROM EventFrameworkActions WHERE DSInstance = " + instanceID.ToString();
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                dbcmd = null;
                return "datasource and all dependant events and actions have been removed";
            }
            return "you have no right to do this";
        }

        public string AbortComplexEventInstances(int ceid)
        {
                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                dbcmd.CommandText = "DELETE FROM EventFrameworkComplexEventInstances WHERE CEID = " + ceid.ToString();
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                dbcmd = null;
                return "instance aborted";
        }
        
        public Time GetTimeOfStage(string initialStageUri, int stageNr)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            string sql = "CALL EVENT_FRAMEWORK_GET_DURATION_OF_STAGE('" + initialStageUri + "'," + stageNr.ToString() + ",1,0)";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            reader.Read();
            Time time = null;
            object dura = reader[Constants.getDurationOfStage_Duration];
            object wait = reader[Constants.getDurationOfStage_Wait];
            try
            {
                if (dura.ToString().StartsWith("P"))
                    time = new Time(dura.ToString(), Convert.ToBoolean(int.Parse(wait.ToString())));
                else
                    time = new Time(0, 0, 0, int.Parse(dura.ToString()), Convert.ToBoolean(int.Parse(wait.ToString())));
            }
            catch (Exception)
            {
                dbcmd.Dispose();
                dbcmd = null;
                return null;
            }
            dbcmd.Dispose();
            dbcmd = null;
            return time;
        }
        
        public string GetStageUriFromEvent(string eventUri)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            dbcmd.CommandText = "sparql PREFIX shma: <http://EventFramework/Schema/> PREFIX : <http://EventFramework/Stages/> Select DISTINCT ?stage FROM <http://EventFramework/Stages> " + 
                "WHERE {{?stage shma:initialEventSet ?set.} UNION {?stage shma:initialEventSet/shma:operands ?set.} FILTER(?set = <" + eventUri + ">)}";
            object zw = dbcmd.ExecuteScalar();
            dbcmd.Dispose();
            dbcmd = null;
            return zw.ToString();
        }
        
        public string DeActivateDB(User admin, DataSource ds)
        {
            if (admin.SessionNr > 0 && admin.EcaDefRight)
            {
                EndpointAddress addr = new EndpointAddress(GetRemoteProcedureEndpoint(ds.DsInstance));
                EventFrameworkProceduresDocLiteralPortTypeClient virtuosoSoapClient = new EventFrameworkProceduresDocLiteralPortTypeClient(bind, addr);
                string[][] zw = null;

                    virtuosoSoapClient.EVENT_FRAMEWORK_INSERT_CONSTANT((int)(GetControlId(ds.DsInstance)), "active", ds.Active.ToString());
                    zw = virtuosoSoapClient.EVENT_FRAMEWORK_INTERNAL_SPARQL((int)(GetControlId(ds.DsInstance)), "SELECT \"Value\" FROM EventFrameworkConstants WHERE \"Key\" = 'active'");

                virtuosoSoapClient.Close();
                virtuosoSoapClient = null;

                if (zw != null)
                {
                    IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                    dbcmd.CommandText = "UPDATE EventFrameworkDatasources SET Active = " + ds.Active.ToString() + " WHERE DSInstance = " + ds.DsInstance.ToString();
                    dbcmd.ExecuteNonQuery();
                    dbcmd.Dispose();
                    dbcmd = null;
                    return "database-status: " + zw[1][0];
                }
                else
                    return "an error occured";
            }
            return "you have no right to do this";
        }
        
        public bool CheckIfDsExists(string endpoint)
        {
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            dbcmd.CommandText = "SELECT CASE WHEN EXISTS(SELECT * FROM EventFrameworkDatasources WHERE ProcedureEndpoint = '" + endpoint + "') THEN 'True' ELSE 'False' END";
            object zw = dbcmd.ExecuteScalar();
            dbcmd.Dispose();
            dbcmd = null;
            return Convert.ToBoolean(zw);
        }


        public string DeleteActionsOrConditions(User admin, int actionID)
        {
            if (admin.SessionNr > 0 && admin.EcaDefRight)
            {
                IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
                dbcmd.CommandText = "DELETE FROM EventFrameworkActions WHERE ActionID = " + actionID.ToString();
                object zw = dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                dbcmd = null;
                return "deleted";
            }
            else
                return "you have no right to do this";
        }
    }
}