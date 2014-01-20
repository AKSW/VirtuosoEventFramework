using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using System.Web.Configuration;

namespace VirtuosoEventService
{
    public class CustomMessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message message, IClientChannel channel, InstanceContext context)
        {
            if (bool.Parse(WebConfigurationManager.AppSettings["writeDbgMessages"].ToString()))
            {
                MemoryStream ms = new MemoryStream();
                XmlWriter xw = XmlWriter.Create(ms);
                message.WriteMessage(xw);
                xw.Flush();
                string envelope = Encoding.UTF8.GetString(ms.ToArray());
                //StaticHelper.writeToConsole(envelope);
                xw.Close();
                Regex bodyCut = new Regex("<[a-zA-Z]+:Body.*\\/[a-zA-Z]+:Body>", RegexOptions.Singleline);
                string body = bodyCut.Match(envelope).Value.Replace("item>", "a:string>");
                if (body == null || body.Length == 0)
                {
                    bodyCut = new Regex("<SOAP:Body.*\\/SOAP:Body>", RegexOptions.Singleline);
                }
                //StaticHelper.writeToConsole(body);
                string output = body.Substring(0, body.IndexOf('>', body.IndexOf('>') + 1) + 1);
                body = body.Substring(body.IndexOf('>', body.IndexOf('>') + 1) + 1);
                Regex cut = new Regex("(?<=(<[^\\s>]*))\\s[^>]*");
                MatchCollection coll = cut.Matches(body);
                foreach (Match match in coll.OfType<Match>().OrderByDescending(x => x.Index))
                {
                    if (match.Value.Contains("SOAP-ENC:Array"))
                    {
                        body = body.Remove(match.Index, match.Length);
                        body = body.Insert(match.Index, " xmlns:a=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"");
                    }
                    else
                        body = body.Remove(match.Index, match.Length);
                }
                output += body;
                envelope = bodyCut.Replace(envelope, output.Replace(":cli", "").Replace("cli:", ""));
                envelope = envelope.Substring(39).Replace("SOAP:", "s:").Replace(":SOAP", ":s");
                ms = new MemoryStream(Encoding.UTF8.GetBytes(envelope));
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.CheckCharacters = false;
                XmlReader xdr = XmlReader.Create(ms, settings);
                Message newMessage = Message.CreateMessage(xdr, int.MaxValue, message.Version);
                newMessage.Properties.CopyProperties(message.Properties);
                MessageBuffer buffer = newMessage.CreateBufferedCopy(int.MaxValue);
                message = buffer.CreateMessage();

                //ms.Position = 0;
                //string separator = "\\";
                //if (StaticHelper.IsLinux)
                //    separator = "/";

                //using (var fileStream = new FileStream(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + separator + "soapMsg.xml", FileMode.Append))
                //{
                //    ms.CopyTo(fileStream);
                //} 
            }
            
            return null;
        }

        public void BeforeSendReply(ref Message message, object instance)
        {
        }
    }


    public class HookServiceBehaviour : IEndpointBehavior
    {
        #region Implementation of IEndpointBehavior

        public void Validate(ServiceEndpoint endpoint) { }

        public void AddBindingParameters(ServiceEndpoint endpoint,
            BindingParameterCollection bindingParameters) { }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint,
            EndpointDispatcher endpointDispatcher) 
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new CustomMessageInspector());
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint,
            ClientRuntime clientRuntime)
        {
        }

        #endregion
    }
}