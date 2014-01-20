using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfSamples.DynamicProxy;
using System.ServiceModel.Description;
using System.Security.Cryptography.X509Certificates;

namespace VirtuosoEventService
{
    public class DynamicSoapClient
    {
        private string wsdlUrl = null;
        private DynamicProxyFactory factory;

        public DynamicSoapClient(string wsdlUrl)
        {
            try
            {
                this.wsdlUrl = wsdlUrl;
                DynamicProxyFactoryOptions options = new DynamicProxyFactoryOptions();
                this.factory = new DynamicProxyFactory(wsdlUrl);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public object PerformSoapCall(string Endpoint, string Methodename, string Username, string Password, byte[]x509certificate, string x509Password, object[] Parameters)
        {
            //try
            //{
            WcfSamples.DynamicProxy.DynamicProxy proxy = factory.CreateProxy(factory.Endpoints.Where(x => x.Address.Uri.AbsoluteUri.ToLower().Trim() == Endpoint.ToLower().Trim()).First());
                (proxy.GetProperty("ClientCredentials") as ClientCredentials).UserName.UserName = Username;
                (proxy.GetProperty("ClientCredentials") as ClientCredentials).UserName.Password = Password;
                if (!string.IsNullOrEmpty(x509Password) && x509certificate != null && x509certificate.Count() != 0)
                {
                    System.Security.Cryptography.X509Certificates.X509Certificate2 cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(x509certificate, x509Password, X509KeyStorageFlags.MachineKeySet
    | X509KeyStorageFlags.PersistKeySet
    | X509KeyStorageFlags.Exportable);
                    (proxy.GetProperty("ClientCredentials") as ClientCredentials).ClientCertificate.Certificate = cert;
                }
                object ret = proxy.CallMethod(Methodename, Parameters);
                proxy.Close();
                return ret;
            //}
            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}
        }
    }
}