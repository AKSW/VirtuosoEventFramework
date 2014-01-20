using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Data;
using System.Diagnostics;
using EventOntology;
using System.Xml;
using System.IO;
using System.ServiceModel.Channels;
using System.Reflection;

namespace RdfEventClientServiceLib
{
    public static class ServiceProvider
    {
        public static IVirtuosoEventService ServiceInstance;
        public static IVirtuosoExtentionService ServiceExtention;

        public static void SetPrivatePropertyValue<T>(this object obj, string propName, T val)
        {
            Type t = obj.GetType();
            if (t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) == null)
                throw new ArgumentOutOfRangeException("propName",
                                                      string.Format("Property {0} was not found in Type {1}", propName,
                                                                    obj.GetType().FullName));
            t.InvokeMember(propName,
                           BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty |
                           BindingFlags.Instance, null, obj, new object[] { val });
        }

        private static CustomBinding CreateCustomBinding(bool useHttps = false, bool textEncoding = false)
        {
            BindingElement security;
            BindingElement encoding;
            BindingElement transport;
            //if (useHttps)
            //{
            //    var seq = SecurityBindingElement.CreateUserNameOverTransportBindingElement();
            //    seq.MessageSecurityVersion =
            //        MessageSecurityVersion.
            //            WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
            //    seq.SecurityHeaderLayout = SecurityHeaderLayout.Lax;
            //    seq.DefaultAlgorithmSuite = SecurityAlgorithmSuite.Default;

            //    security = seq;
            //    transport = new HttpsTransportBindingElement
            //    {
            //        MaxBufferPoolSize = 2147483647,
            //        MaxBufferSize = 2147483647,
            //        MaxReceivedMessageSize = 2147483647,
            //    };
            //}
            //else
            //{
            security = SecurityBindingElement.CreateUserNameOverTransportBindingElement();

            SetPrivatePropertyValue(security, "AllowInsecureTransport", true);
           
            //PropertyInfo[] infos = security.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //PropertyInfo highlightedItemProperty = infos.Single(pi => pi.Name == "AllowInsecureTransport");
            //bool allowInsecureTransport = (bool)(highlightedItemProperty.GetValue(security, null));
            //allowInsecureTransport = true;

                transport = new HttpTransportBindingElement
                {
                    MaxBufferPoolSize = 2147483647,
                    MaxBufferSize = 2147483647,
                    MaxReceivedMessageSize = 2147483647,
                };
            //}

            //if (textEncoding)
            //    encoding = new TextMessageEncodingBindingElement
            //    {
            //        MaxReadPoolSize = 64,
            //        MaxWritePoolSize = 16,
            //        MessageVersion = MessageVersion.Soap11,
            //        WriteEncoding = System.Text.Encoding.UTF8
            //    };
            //else
                encoding = new MtomMessageEncodingBindingElement
                {
                    MaxReadPoolSize = 64,
                    MaxWritePoolSize = 16,
                    MaxBufferSize = 2147483647,
                    MessageVersion = MessageVersion.Soap11,
                    WriteEncoding = System.Text.Encoding.UTF8
                };

            var customBinding = new CustomBinding();

            customBinding.Elements.Add(security);
            customBinding.Elements.Add(encoding);
            customBinding.Elements.Add(transport);

            return customBinding;
        }

        public static void InitService(string endpoint)
        {
            if (ServiceInstance == null)
            {
                //BasicHttpBinding baBind = new BasicHttpBinding();
                //baBind.MaxBufferPoolSize = 2147483647;
                //baBind.MaxBufferSize = 2147483647;
                //baBind.MaxReceivedMessageSize = 2147483647;
                //baBind.ReaderQuotas.MaxArrayLength = 2147483647;
                //baBind.ReaderQuotas.MaxBytesPerRead = 2147483647;
                //baBind.ReaderQuotas.MaxDepth = 2147483647;
                //baBind.ReaderQuotas.MaxNameTableCharCount = 2147483647;
                //baBind.ReaderQuotas.MaxStringContentLength = 2147483647;
                //ChannelFactory<IVirtuosoExtentionService> extentionFactory = new ChannelFactory<IVirtuosoExtentionService>(baBind, "http://WIN-N26JO1512TH:8000/VirtuosoExtentionService/VirtuosoExtentionEndpoint");


                WSHttpBinding bind = new WSHttpBinding(SecurityMode.None);

                bind.MaxBufferPoolSize = 2147483647;
                bind.MaxReceivedMessageSize = 2147483647;
                XmlDictionaryReaderQuotas myReaderQuotas = new XmlDictionaryReaderQuotas();
                myReaderQuotas.MaxStringContentLength = 1000000;
                myReaderQuotas.MaxArrayLength = 1000000;
                myReaderQuotas.MaxBytesPerRead = 1000000;
                myReaderQuotas.MaxDepth = 1000000;
                myReaderQuotas.MaxNameTableCharCount = 1000000;
                bind.GetType().GetProperty("ReaderQuotas").SetValue(bind, myReaderQuotas, null);

                //CustomBinding bind = CreateCustomBinding();
                ChannelFactory<IVirtuosoEventService> clientFactory = new ChannelFactory<IVirtuosoEventService>(bind, endpoint);

                ServiceInstance = clientFactory.CreateChannel();
                //ServiceExtention = extentionFactory.CreateChannel();
            }
        }
    }

    public class EventClientNetTcp : IEventClient
    {
        private User currentUser { get; set; }
        private string remoteProcedureEndpoint { get; set; }


        public EventClientNetTcp()
        {
            this.remoteProcedureEndpoint = (System.Configuration.ConfigurationManager.AppSettings["remoteEndpoint"].ToString());
            ServiceProvider.InitService(remoteProcedureEndpoint);
            
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            unhandledExceptionLog(ex);

        }

        static void unhandledExceptionLog(Exception e)
        {
            string errorTxtDateiPfad = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\errorLog.txt";

            try
            {
                using (StreamWriter file = new System.IO.StreamWriter(errorTxtDateiPfad, true))
                {
                    file.WriteLine("---------------------------new unhandled Exception -------------------------------");
                    file.WriteLine(DateTime.Now.ToString());
                    file.WriteLine(System.Environment.MachineName.ToString());
                    file.WriteLine(e.Message);
                    file.WriteLine(e.StackTrace.ToString());
                }
            }
            catch (FileNotFoundException)
            {
            }
        }

        public User LogIn(EventOntology.User user)
        {
            currentUser = ServiceProvider.ServiceInstance.LogIn(user);
           return currentUser;
        }

        public bool CreateNewAccount(User newUser)
        {
            bool zw = false;
            if (currentUser.UserAccRight)
            {
                zw = ServiceProvider.ServiceInstance.CreateNewAccount(newUser, currentUser);
            }
            return zw;
        }

        public bool ResetUserpassword(string userName, string newPass)
        {
            bool zw = false;
            if (currentUser.SessionNr > 0 && currentUser.UserAccRight)
                zw = ServiceProvider.ServiceInstance.ResetUserpassword(userName, newPass, currentUser);
            return zw;
        }

        public bool UpdateUserAccount(User user)
        {
            return ServiceProvider.ServiceInstance.UpdateUserAccount(user, currentUser);
        }

        public DataTable GetUsers(string namePart)
        {
            return ServiceProvider.ServiceInstance.GetUsers(namePart);
        }

        public string[] GetSchemaTables(int dbInstance)
        {
            return ServiceProvider.ServiceInstance.GetSchemaTables(dbInstance);
        }

        public DataTable GetDatabases(string type)
        {
            return ServiceProvider.ServiceInstance.GetDatabases(type);
        }
        public string RegisterTrigger(AtomicEvent trigger)
        {
            return ServiceProvider.ServiceInstance.RegisterTrigger(trigger, currentUser);
        }

        public string[] GetTriggersOfTable(string tableName, int dbInstance)
        {
            return ServiceProvider.ServiceInstance.GetTriggersOfTable(tableName, dbInstance);
        }
        public DataTable GetEvents(int minutes)
        {
            return ServiceProvider.ServiceInstance.GetEvents(minutes);
        }

        public string GetSupportedDBs()
        {
            return ServiceProvider.ServiceInstance.GetSupportedDBs();
        }

        public string GetRemoteProcedureEndpoint(int dsInstance)
        {
            return ServiceProvider.ServiceInstance.GetRemoteProcedureEndpoint(dsInstance).ToString();
        }

        public string RegisterNewRemoteDataSource(DataSource ds)
        {
            return ServiceProvider.ServiceInstance.RegisterNewRemoteDataSource(ds, currentUser);
        }

        public string[][] ExecuteTestSqlQuery(string querryString, int dbInstance)
        {
            return ServiceProvider.ServiceInstance.ExecuteTestSqlQuery(querryString, dbInstance);
        }

        public string SetNewSqlTrigger(AtomicEvent trigger)
        {
            return ServiceProvider.ServiceInstance.SetNewSqlTrigger(trigger, currentUser);
        }

        public string[] GetColumnsOfRemoteTable(int dbInstance, string tableName)
        {
            return ServiceProvider.ServiceInstance.GetColumnsOfRemoteTable(dbInstance, tableName);
        }

        public int InsertNewAction(EventAction action)
        {
            return ServiceProvider.ServiceInstance.InsertNewAction(action, currentUser);
        }

        public DataTable GetAllTriggers(string likeName)
        {
            DataTable t = ServiceProvider.ServiceInstance.GetAllTriggers(likeName);
            return t;
        }

        public string[] GetGraphs(int dbInstance)
        {
            return ServiceProvider.ServiceInstance.GetGraphs(dbInstance);
        }

        public string SetNewRdfTrigger(AtomicEvent trigger)
        {
            return ServiceProvider.ServiceInstance.SetNewRdfTrigger(trigger, currentUser);
        }

        public DataTable GetActionsOrConditions(Activity act)
        {
            return ServiceProvider.ServiceInstance.GetActionsOrConditions(act);
        }

        //public object CallActionMethode(int ActionID, object[] Parameters)
        //{
        //    return ServiceProvider.ServiceInstance.CallActionMethode(ActionID, Parameters, currentUser);
        //}

        public string ExecuteSomething(int ActionID, string a, string b, string c)
        {
            Process.Start("calc.exe");
            return "it started";
        }

        public void UpdateAction(EventAction action)
        {
            ServiceProvider.ServiceInstance.UpdateAction(action, currentUser);
        }

        public DataTable GetEventsBetween(DateTime From, DateTime To)
        {
            return ServiceProvider.ServiceInstance.GetEventsBetween(From, To);
        }

        public int InsertNewCondition(ConditionQuery condition, Activity act)
        {
            return ServiceProvider.ServiceInstance.InsertNewCondition(condition, currentUser, act);
        }


        public string DropTrigger(AtomicEvent trigger)
        {
            return ServiceProvider.ServiceInstance.DropTrigger(trigger, currentUser);
        }

        public DateTime StartCalc(int inInt, string aString)
        {
            System.Diagnostics.Process.Start("calc.exe");
            return DateTime.Now;
        }

        public DataTable GetComplexEvents(string likeName = "", int ceid = 0)
        {
            return ServiceProvider.ServiceInstance.GetComplexEvents(likeName, ceid);
        }

        public int GetNextUriNr(string IriClass)
        {
            return ServiceProvider.ServiceInstance.GetNextUriNr(IriClass);
        }

        public string InsertComplexEvent(ComplexEvent ev)
        {
            return ServiceProvider.ServiceInstance.InsertComplexEvent(currentUser, ev);
        }

        public string UpdateComplexEvents(ComplexEvent newEv, ComplexEvent oldEv)
        {
            return ServiceProvider.ServiceInstance.UpdateComplexEvents(currentUser, newEv, oldEv);
        }

        public DataTable GetStagesAndSets(string initialStageUri, int stageNr)
        {
            return ServiceProvider.ServiceInstance.GetStagesAndSets(initialStageUri, stageNr);
        }

        public string InsertCeInstance(int ceid, string initialStageUri, int startAtStage = 0, bool isOverlapping = false)
        {
            return ServiceProvider.ServiceInstance.InsertCeInstance(currentUser, ceid, initialStageUri, startAtStage, isOverlapping);
        }

        public string ActivateComplexEvent(ComplexEvent ev)
        {
            return ServiceProvider.ServiceInstance.ActivateComplexEvent(currentUser, ev);
        }

        public string DeleteComplexEvent(ComplexEvent ev)
        {
            return ServiceProvider.ServiceInstance.DeleteComplexEvent(currentUser, ev);
        }

        string IEventClient.UpdateAction(EventAction action)
        {
            return ServiceProvider.ServiceInstance.UpdateAction(action, currentUser);
        }

        public DataTable GetParamMappings(int ceid)
        {
            return ServiceProvider.ServiceInstance.GetParamMappings(ceid);
        }

        public int CheckThis(int inInt)
        {
            if (inInt > 10)
                return 1;
            else
                return 0;
        }

        public string DeActivateComplexEvent(ComplexEvent ev)
        {
            return ServiceProvider.ServiceInstance.DeActivateComplexEvent(currentUser, ev);
        }

        public DataTable GetComplexEventInstances(int ceid = 0)
        {

            DataTable ll = ServiceProvider.ServiceInstance.GetComplexEventInstances(ceid);
            return ll;
        }
        
        public string DeleteDataSource(int instanceID)
        {
            return ServiceProvider.ServiceInstance.DeleteDataSource(currentUser, instanceID);
        }

        public string AbortComplexEventInstances(int ceid)
        {
            return ServiceProvider.ServiceInstance.AbortComplexEventInstances(ceid);
        }
        
        public Time GetTimeOfStage(string initialStageUri, int stageNr)
        {
            return ServiceProvider.ServiceInstance.GetTimeOfStage(initialStageUri, stageNr);
        }
        
        public string GetStageUriFromEvent(string eventUri)
        {
            return ServiceProvider.ServiceInstance.GetStageUriFromEvent(eventUri);
        }

        public string DeActivateDB(DataSource ds)
        {
            return ServiceProvider.ServiceInstance.DeActivateDB(currentUser, ds);
        }
        
        public bool CheckIfDsExists(string endpoint)
        {
            return ServiceProvider.ServiceInstance.CheckIfDsExists(endpoint);
        }
        
        public int getControlId(int dsInstance)
        {
            return ServiceProvider.ServiceInstance.GetControlId(dsInstance);
        }

        public string DeleteActionsOrConditions(int actionID)
        {
            return ServiceProvider.ServiceInstance.DeleteActionsOrConditions(currentUser, actionID);
        }
    }
}
