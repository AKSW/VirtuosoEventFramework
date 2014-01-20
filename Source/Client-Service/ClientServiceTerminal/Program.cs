using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using RdfEventClientServiceLib;
using System.ServiceModel.Description;
using EventOntology;
using System.Data;

namespace RdfEventClientConcoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri[] baseAddress = new Uri[] { new Uri(System.Configuration.ConfigurationManager.AppSettings["netPipeAddress"].ToString())
                , new Uri(System.Configuration.ConfigurationManager.AppSettings["netTcpAddress"].ToString()) };

                ServiceHost host = new ServiceHost(typeof(EventClientNetTcp), baseAddress);

                try
                {
                    //NetTcpBinding netBind = new NetTcpBinding(SecurityMode.Message);
                    //Encoding enc = new UTF8Encoding(false);
                    //wsBind.TextEncoding = enc; 
                    //netBind.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
                    //wsBind.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;

                    host.AddServiceEndpoint(typeof(IEventClient),
                    new NetTcpBinding(),
                    System.Configuration.ConfigurationManager.AppSettings["serviceEndpoint"].ToString());

                    host.AddServiceEndpoint(typeof(IEventClient),
                        new NetNamedPipeBinding(),
                        System.Configuration.ConfigurationManager.AppSettings["serviceEndpoint"].ToString());

                    ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                    smb.HttpGetUrl = new Uri(System.Configuration.ConfigurationManager.AppSettings["httpAddress"].ToString());
                    smb.HttpGetEnabled = true;
                    host.Description.Behaviors.Add(smb);
                    
                    // Step 5 of the hosting procedure: Start (and then stop) the service.
                    
                    host.Open();

                    //EventClientNetTcp tcp = new EventClientNetTcp();
                    //ServiceProvider.ServiceExtention.CallForAction(380869745, 6, 26, new string[] { "renjgj", "23", "fdhsdfsgg", "rjhksruiggh" });
                    //ServiceProvider.ServiceExtention.CallForAction(1692216257, 7, 33, new string[] { "renjgj", "23", "fdhsdfsgg", "rjhksruiggh" });
                    //tcp.LogIn( new User("Admin", "admin"));
                    //foreach (DataRow row in t.Rows)
                    //{
                    //    string zw = "";
                    //    foreach (object obj in row.ItemArray)
                    //        zw += obj.ToString() + ", ";
                    //    Console.WriteLine(zw.ToString());
                    //}

                    //foreach(System.ServiceModel.Dispatcher.ChannelDispatcher disp in host.ChannelDispatchers)
                    //    Console.WriteLine(disp.Listener.Uri);
                    Console.WriteLine("Press <ENTER> to terminate service.");
                    Console.WriteLine();
                    Console.ReadLine();

                    // Close the ServiceHostBase to shutdown the service.
                    host.Close();

                }
                catch (CommunicationException ce)
                {
                    Console.WriteLine("An exception occurred: {0}", ce.Message);
                    host.Abort();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception occurred: {0}", ex.Message);
                    host.Abort();
                }
            
        }
    }
}
