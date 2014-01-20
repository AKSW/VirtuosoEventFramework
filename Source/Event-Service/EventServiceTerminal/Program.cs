using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using VirtuosoEventService;
using EventOntology;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Xml;

namespace EventServiceTerminal
{
    class Program
    {
        static void Main(string[] args)
        {
            StaticHelper.InitializeDbConnection();
            ServiceHost eventServiceHost = new ServiceHost(typeof(DistributedVirtuosoEventService), new Uri(System.Configuration.ConfigurationManager.AppSettings["baseAddressEvService"].ToString()));
            ServiceHost extentionServiceHost = new ServiceHost(typeof(VirtuosoExtentionService), new Uri(System.Configuration.ConfigurationManager.AppSettings["baseAddressExService"].ToString()));
            ServicePointManager.ServerCertificateValidationCallback =
               delegate(object sender, X509Certificate certificate, X509Chain chain,
                   SslPolicyErrors sslPolicyErrors) { return true; };

            //try
            //{
                WSHttpBinding wsBind = new WSHttpBinding(SecurityMode.None);
                BasicHttpBinding baBind = new BasicHttpBinding();

                wsBind.MaxBufferPoolSize = 2147483647;
                wsBind.MaxReceivedMessageSize = 2147483647;
                baBind.MaxBufferPoolSize = 2147483647;
                baBind.MaxBufferSize = 2147483647;
                baBind.MaxReceivedMessageSize = 2147483647;
                XmlDictionaryReaderQuotas wsReaderQuotas = new XmlDictionaryReaderQuotas();
                wsReaderQuotas.MaxStringContentLength = 2147483647;
                wsReaderQuotas.MaxArrayLength = 2147483647;
                wsReaderQuotas.MaxBytesPerRead = 2147483647;
                wsReaderQuotas.MaxDepth = 2147483647;
                wsReaderQuotas.MaxNameTableCharCount = 2147483647;
                wsBind.GetType().GetProperty("ReaderQuotas").SetValue(wsBind, wsReaderQuotas, null);
                baBind.ReaderQuotas.MaxArrayLength = 2147483647;
                baBind.ReaderQuotas.MaxBytesPerRead = 2147483647;
                baBind.ReaderQuotas.MaxDepth = 2147483647;
                baBind.ReaderQuotas.MaxNameTableCharCount = 2147483647;
                baBind.ReaderQuotas.MaxStringContentLength = 2147483647;

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();

                eventServiceHost.AddServiceEndpoint(typeof(EventOntology.IVirtuosoEventService),wsBind,System.Configuration.ConfigurationManager.AppSettings["serviceEndpoint"].ToString());
                ContractDescription ff = ContractDescription.GetContract(typeof(EventOntology.IVirtuosoExtentionService));
                ServiceEndpoint extentionPoint = new ServiceEndpoint(ff, baBind, new EndpointAddress(System.Configuration.ConfigurationManager.AppSettings["baseAddressExService"].ToString() + "/" + System.Configuration.ConfigurationManager.AppSettings["virtExtEndpoint"].ToString()));
                extentionPoint.Behaviors.Add(new HookServiceBehaviour());
                extentionServiceHost.AddServiceEndpoint(extentionPoint);
                ServiceDebugBehavior dbgev = eventServiceHost.Description.Behaviors.Find<ServiceDebugBehavior>();
                ServiceDebugBehavior dbgex = extentionServiceHost.Description.Behaviors.Find<ServiceDebugBehavior>();
                

                smb.HttpGetEnabled = true;
//#if DEBUG
                if (dbgev == null)
                    eventServiceHost.Description.Behaviors.Add(
                         new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
                else
                    // make sure setting is turned ON
                    if (!dbgev.IncludeExceptionDetailInFaults)
                        dbgev.IncludeExceptionDetailInFaults = true;
            if(dbgex == null)
                    extentionServiceHost.Description.Behaviors.Add(
                         new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            else
                // make sure setting is turned ON
                if (!dbgex.IncludeExceptionDetailInFaults)
                    dbgex.IncludeExceptionDetailInFaults = true;
//#endif

                eventServiceHost.Description.Behaviors.Add(smb);
                //extentionServiceHost.Description.Behaviors.Add(smb);
                // Step 5 of the hosting procedure: Start (and then stop) the service.

                eventServiceHost.Open();
                extentionServiceHost.Open();
                Console.WriteLine(eventServiceHost.ChannelDispatchers[0].Listener.Uri);
                Console.WriteLine(extentionServiceHost.ChannelDispatchers[0].Listener.Uri);
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.WriteLine(baBind.MaxReceivedMessageSize.ToString() + baBind.ReaderQuotas.MaxDepth.ToString());
                Console.ReadLine();

                // Close the ServiceHostBase to shutdown the service.

                StaticHelper.CloseDbConnection();
                extentionServiceHost.Close();
                eventServiceHost.Close();

            //}
            //catch (CommunicationException ce)
            //{
            //    Console.WriteLine("An exception occurred: {0}", ce.Message);
            //    StaticHelper.CloseDbConnection();
            //    eventServiceHost.Abort();
            //    extentionServiceHost.Abort();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("An exception occurred: {0}", ex.Message);
            //    StaticHelper.CloseDbConnection();
            //    eventServiceHost.Abort();
            //    extentionServiceHost.Abort();
            //}
        }
    }
}
