using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Data;
using EventOntology;
using System.Web.Configuration;
using System.ServiceModel;
using System.Data.Odbc;
using System.Net.Mail;

namespace VirtuosoEventService
{
    /// <summary>
    /// Summary description for VirtuosoExtentionService
    /// </summary>
    public class VirtuosoExtentionService : EventOntology.IVirtuosoExtentionService
    {
        //private static bool consoleDbg = bool.Parse(WebConfigurationManager.AppSettings["writeDbgMessages"].ToString());
        private DistributedVirtuosoEventService eventService = new DistributedVirtuosoEventService();

        /// <summary>
        /// constructor: cennects to specific Virtuoso-DB 
        /// </summary>
        public VirtuosoExtentionService()
        {
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(StaticHelper.CurrentDomain_UnhandledException);
        }

public void CallForAction(int controlID, int actionID, int eventInstance, string[] parameters)
{
    new System.Threading.Tasks.Task(() =>
    {
        DataTable t = eventService.GetActionById(actionID);
        StaticHelper.writeToConsole(t.Rows.Count.ToString());
        if ((int)(eventService.GetControlId(1)) == controlID && t.Rows.Count > 0)
        {
            Regex valMapCheck = new Regex("##CE[0-9]+\\/STAGE[0-9]+(\\/((MS[0-9]+\\/[0-9]+(\\/CE[0-9]+\\/STAGE[0-9])?)|ES[0-9]+|(CE[0-9]+\\/STAGE[0-9]+)))*\\/AE[0-9]+\\/VALUE\\([0-9a-zA-Z]+\\)##");
            EventAction action = new EventAction(t, 0);
            StaticHelper.writeToConsole(action.Description.ToString());
            string[] forwardParams = new string[parameters.Length / 2];

            for (int i = 0; i < parameters.Length; i++)
            {
                if (i % 2 == 0)
                {
                    if (parameters[i] == "query")
                    {
                        DataTable q = eventService.GetActionById(int.Parse(parameters[i + 1].ToString()));
                        EventAction query = new EventAction(q, 0);
                        if (query.SparqlQuery.Contains("##"))
                        {
                            MatchCollection matches = valMapCheck.Matches(query.SparqlQuery);
                            foreach (Match mat in matches.OfType<Match>().OrderByDescending(x => x.Index))
                            {
                                query.SparqlQuery = query.SparqlQuery.Remove(mat.Index, mat.Length);
                                query.SparqlQuery = query.SparqlQuery.Insert(mat.Index, evaluateValueMap(eventInstance, mat.Value));
                            }
                        }
                        object zw = query.InvokeSparqlQuery();
                        forwardParams[i / 2] = zw.ToString();
                    }
                    else if (parameters[i] == "valueMap")
                    {
                        MatchCollection matches = valMapCheck.Matches(parameters[i + 1].ToString());
                        string res = parameters[i + 1];
                        foreach (Match mat in matches.OfType<Match>().OrderByDescending(x => x.Index))
                        {
                            res = res.Remove(mat.Index, mat.Length);
                            res = res.Insert(mat.Index, evaluateValueMap(eventInstance, mat.Value));
                        }

                        forwardParams[i / 2] = res;
                    }
                    else  //static value
                        forwardParams[i / 2] = parameters[i + 1];
                }
            }

            try
            {
                StaticHelper.writeToConsole(StaticHelper.writeArrayToString(forwardParams));
                action.InvokeRemoteMethode(forwardParams);
            }
            catch (InvalidOperationException io)
            {
                if (io.Message.ToLower().Contains("sequence"))
                    StaticHelper.unhandledExceptionLog(io, "please check if endpoint is available for actionID: " + action.ID.ToString() + "endpoint: " + action.EndpointAddress);
            }
            catch (Exception ex)
            {
                StaticHelper.unhandledExceptionLog(ex, "exception at invoking action, for actionID: " + action.ID.ToString());
            }
        }
    }).Start();
}

        public object CheckCondition(int controlID, int actionID, int eventInstance, string[] parameters)
        {
            DataTable t = eventService.GetActionById(actionID);
            if ((int)(eventService.GetControlId(1)) == controlID && t.Rows.Count > 0)
            {
                Regex valMapCheck = new Regex("##CE[0-9]+\\/STAGE[0-9]+(\\/((MS[0-9]+\\/[0-9]+(\\/CE[0-9]+\\/STAGE[0-9])?)|ES[0-9]+|(CE[0-9]+\\/STAGE[0-9]+)))*\\/AE[0-9]+\\/VALUE\\([0-9a-zA-Z]+\\)##");
                EventAction action = new EventAction(t, 0);
                string[] forwardParams = new string[parameters.Length / 2];

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        if (parameters[i] == "query")
                        {
                            DataTable q = eventService.GetActionById(int.Parse(parameters[i + 1].ToString()));
                            EventAction query = new EventAction(q, 0);
                            if (query.SparqlQuery.Contains("##"))
                            {
                                MatchCollection matches = valMapCheck.Matches(query.SparqlQuery);
                                foreach (Match mat in matches.OfType<Match>().OrderByDescending(x => x.Index))
                                {
                                    query.SparqlQuery = query.SparqlQuery.Remove(mat.Index, mat.Length);
                                    query.SparqlQuery = query.SparqlQuery.Insert(mat.Index, evaluateValueMap(eventInstance, mat.Value));
                                }
                            }
                            object zw = query.InvokeSparqlQuery();
                            forwardParams[i / 2] = zw.ToString();
                        }
                        else if (parameters[i] == "valueMap")
                        {
                            MatchCollection matches = valMapCheck.Matches(parameters[i + 1].ToString());
                            string res = parameters[i + 1];
                            foreach (Match mat in matches.OfType<Match>().OrderByDescending(x => x.Index))
                            {
                                res = res.Remove(mat.Index, mat.Length);
                                res = res.Insert(mat.Index, evaluateValueMap(eventInstance, mat.Value));
                            }

                            forwardParams[i / 2] = res;
                        }
                        else  //static value
                            forwardParams[i / 2] = parameters[i + 1];
                    }
                }
                object ret = null;
                try
                {
                    ret = action.InvokeRemoteMethode(forwardParams);
                    return Convert.ToInt32(ret);
                }
                catch (InvalidOperationException io)
                {
                    if (io.Message.ToLower().Contains("sequence"))
                        StaticHelper.unhandledExceptionLog(io, "please check if endpoint is available for actionID: " + action.ID.ToString() + "endpoint: " + action.EndpointAddress);
                    return 0;
                }
                catch (Exception ex)
                {
                    StaticHelper.unhandledExceptionLog(ex, "exception at invoking action, for actionID: " + action.ID.ToString());
                    return 0;
                }

            }
            else
            {
                StaticHelper.writeToConsole("CallForAction: controlID or actionID was not found");
                return 0;
            }
        }

        private string evaluateValueMap(int eventInstance, string match)
        {
            int continuePosCE = 0;
            string returnVal = null;
            match = match.Replace("##", "");
            string[] parts = match.Split('/');
            Regex numbers = new Regex("[0-9]+");
            string ceid = numbers.Match(parts[0]).Value;
            string stage = numbers.Match(parts[1]).Value;
            IDbCommand dbcmd = StaticHelper.dbcon.CreateCommand();
            string sql = "SELECT TOP 1 events.EventID, mapping.InstanceID, mapping.StageNr, TriggerID, events.CEID, events.CeInstance " +
                "FROM EventFrameworkEventToInstanceMapping as mapping INNER JOIN EventFrameworkEvents as events ON mapping.EventID = events.EventID ";

            for (int i = 2; i < parts.Length - 1; i++)
            {
                if (parts[i].StartsWith("MS"))
                {
                    int recurrence = int.Parse(numbers.Match(parts[i + 1]).Value);
                    if (parts[i + 2].StartsWith("AE"))
                    {
                        sql = "SELECT TOP 1," + recurrence + " events.EventID, mapping.InstanceID, mapping.StageNr, TriggerID, events.CEID, events.CeInstance " +
                            "FROM EventFrameworkEventToInstanceMapping as mapping INNER JOIN EventFrameworkEvents as events ON mapping.EventID = events.EventID " +
                            "WHERE mapping.CEID = " + ceid + " AND InstanceID = " + eventInstance.ToString() + " AND mapping.StageNr = " + stage + " AND TriggerID = " +
                            numbers.Match(parts[i + 2]).Value + " ORDER BY events.Occurence";
                        break;
                    }
                    if (parts[i + 2].StartsWith("CE"))
                    {
                        sql = "SELECT TOP 1," + recurrence + " events.EventID, mapping.InstanceID, mapping.StageNr, TriggerID, events.CEID, events.CeInstance " +
                            "FROM EventFrameworkEventToInstanceMapping as mapping INNER JOIN EventFrameworkEvents as events ON mapping.EventID = events.EventID " +
                            "WHERE mapping.CEID = " + ceid + " AND InstanceID = " + eventInstance.ToString() + " AND mapping.StageNr = " + stage + " AND events.CEID = " +
                            numbers.Match(parts[i + 2]).Value + " ORDER BY events.Occurence";
                        continuePosCE = i;
                        break;
                    }
                }
                else if (parts[i].StartsWith("CE"))
                {
                    sql += "WHERE mapping.CEID = " + ceid + " AND InstanceID = " + eventInstance.ToString() + " AND mapping.StageNr = " + stage + " AND events.CEID = " +
                            numbers.Match(parts[i]).Value + " ORDER BY events.Occurence";
                    continuePosCE = i;
                    break;
                }
                else if (parts[i].StartsWith("AE"))
                {
                    sql += "WHERE mapping.CEID = " + ceid + " AND InstanceID = " + eventInstance.ToString() + " AND mapping.StageNr = " + stage + " AND events.TriggerID = " +
                            numbers.Match(parts[i]).Value + " ORDER BY events.Occurence";
                    break;
                }
            }


            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            reader.Read();

            if (continuePosCE > 0)
            {
                string zw = "";
                for (int j = continuePosCE; j < parts.Length; j++)
                    zw += "/" + parts[j];
                return evaluateValueMap((int)reader["CeInstance"], zw.Substring(1));
            }
            else
            {
                string valueIndex = numbers.Match(parts[parts.Length - 1]).Value;
                sql = "SELECT Row[" + valueIndex + "] FROM DB.DBA.EventFrameworkEvents WHERE EventID = " + reader[0].ToString();
                reader.Close();
                reader = null;
                dbcmd.Dispose();
                dbcmd = null;
                dbcmd = StaticHelper.dbcon.CreateCommand();
                dbcmd.CommandText = sql;
                object zw = dbcmd.ExecuteScalar();
                if (zw != null)
                    returnVal = zw.ToString();
                else
                    return "NULL";
            }

            dbcmd.Dispose();
            dbcmd = null;
            return returnVal;
        }

        public string ReceiveNewEvent(int controlID, int TriggerID, object rowVector)
        {
            Console.WriteLine("here we are");
            if ((int)(eventService.GetControlId(1)) == controlID)
            {
                //TODO atomic event handeling
                int zz = TriggerID;
                return "delivered";
            }
            else
                return "wrong controlID!";
        }
        
        public void SendSmtpMail(string smtpHost, int port, string username, string password, string from, string to, string subject, string body)
        {
            new System.Threading.Tasks.Task(() =>
            {
                try
                {
                    SmtpClient client = new SmtpClient();
                    client.Port = port;
                    client.Host = smtpHost;
                    client.EnableSsl = true;
                    client.Timeout = 10000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    //aclient.EnableSsl = false;
                    client.Credentials = new System.Net.NetworkCredential(username, password);

                    MailMessage mm = new MailMessage(from, to, subject, body);
                    //mm.BodyEncoding = UTF8Encoding.UTF8;
                    //mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    client.Send(mm);
                }
                catch (Exception)
                {
                }
            }).Start();
        }
    }
}
