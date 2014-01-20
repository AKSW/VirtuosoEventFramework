using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using WcfSamples.DynamicProxy;
using System.ServiceModel.Description;
using System.Security.Cryptography.X509Certificates;

namespace EventOntology
{
    public class Action : IRemoteAction
    {
        private int id;
        private int createdBy;  //UserID
        private string description;
        private string wsdlAddress;
        private string endpointAddress;
        private string methodeName;
        private List<string> paramTypes;
        private List<string> paramDescription;
        private string returnType;
        private string returnDescription;
        private string serviceUserName;
        private string servicePassword;
        private string x509Certificate;
        private string x509Password;

        public Action(ref DataTable table, int rowIndex)
        {
            DataRow row = table.Rows[rowIndex];
            this.createdBy = (int)(row.ItemArray[table.Columns[Constants.createdBy].Ordinal]);
            this.description = row.ItemArray[table.Columns[Constants.description].Ordinal].ToString();
            this.endpointAddress = row.ItemArray[table.Columns[Constants.actEndpoint].Ordinal].ToString();
            this.id = (int)(row.ItemArray[table.Columns[Constants.actionID].Ordinal]);
            this.methodeName = row.ItemArray[table.Columns[Constants.actMethode].Ordinal].ToString();
            this.paramDescription = row.ItemArray[table.Columns[Constants.actParams].Ordinal].ToString().Replace(";", ",").Replace(" ", "").Split(',').ToList();
            this.paramTypes = row.ItemArray[table.Columns[Constants.actParamTypes].Ordinal].ToString().Replace(";", ",").Replace(" ", "").Split(',').ToList();
            this.returnDescription = row.ItemArray[table.Columns[Constants.actReturn].Ordinal].ToString();
            this.returnType = row.ItemArray[table.Columns[Constants.actReturnType].Ordinal].ToString();
            this.servicePassword = row.ItemArray[table.Columns[Constants.actServicePassword].Ordinal].ToString();
            this.serviceUserName = row.ItemArray[table.Columns[Constants.actServiceUserName].Ordinal].ToString();
            this.wsdlAddress = row.ItemArray[table.Columns[Constants.actWsdl].Ordinal].ToString();
            this.x509Certificate  = row.ItemArray[table.Columns[Constants.actX509Cert].Ordinal].ToString();
            this.x509Password = row.ItemArray[table.Columns[Constants.actX509Pass].Ordinal].ToString();
        }

        public Action(string wsdlAddress, string endpointAddress, string methodeName, string description, List<string> paramTypes = null, List<string> paramDescription = null, string returnType = null,
            string returnDescription = null, string serviceUserName = null, string servicePassword = null, string x509Certificate = null, string x509Password = null)
        {
            this.description = description;
            this.endpointAddress = endpointAddress;
            this.methodeName = methodeName;
            this.paramDescription = paramDescription;
            this.paramTypes = paramTypes;
            this.returnDescription = returnDescription;
            this.returnType = returnType;
            this.servicePassword = servicePassword;
            this.serviceUserName = serviceUserName;
            this.wsdlAddress = wsdlAddress;
            this.x509Certificate = x509Certificate;
            this.x509Password = x509Password;
        }
        
        public object InvokeRemoteMethode(object[] parameter)
        {
            try
            {
                byte[] cert = System.Text.Encoding.Default.GetBytes(this.x509Certificate);
                DynamicProxyFactory factory = new DynamicProxyFactory(this.wsdlAddress);
                DynamicProxy proxy = factory.CreateProxy(factory.Endpoints.Where(x => x.Address.Uri.AbsoluteUri.ToLower().Trim() == this.endpointAddress.ToLower().Trim()).First());
                (proxy.GetProperty("ClientCredentials") as ClientCredentials).UserName.UserName = serviceUserName;
                (proxy.GetProperty("ClientCredentials") as ClientCredentials).UserName.Password = servicePassword;
                if (!string.IsNullOrEmpty(x509Password) && cert != null && cert.Count() != 0)
                {
                    System.Security.Cryptography.X509Certificates.X509Certificate2 cert2 = new System.Security.Cryptography.X509Certificates.X509Certificate2(cert, x509Password, X509KeyStorageFlags.MachineKeySet
                        | X509KeyStorageFlags.PersistKeySet
                        | X509KeyStorageFlags.Exportable);
                    (proxy.GetProperty("ClientCredentials") as ClientCredentials).ClientCertificate.Certificate = cert2;
                }
                object ret = proxy.CallMethod(methodeName, parameter);
                proxy.Close();
                return ret;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public int ID
        {
            get { return id; }
        }

        public int CreatedBy
        {
            get { return createdBy; }
        }
        
        public string Description
        {
            get { return description; }
        }

        public string WsdlAddress
        {
            get { return wsdlAddress; }
        }

        public string EndpointAddress
        {
            get { return endpointAddress; }
        }

        public string MethodeName
        {
            get { return methodeName; }
        }

        public List<string> ParamTypes
        {
            get { return paramTypes; }
        }

        public List<string> ParamDescription
        {
            get { return paramDescription; }
        }

        public string ReturnType
        {
            get { return returnType; }
        }

        public string ReturnDescription
        {
            get { return returnDescription; }
        }

        public string ServiceUserName
        {
            get { return serviceUserName; }
        }

        public string ServicePassword
        {
            get { return servicePassword; }
        }

        public string X509Certificate
        {
            get { return x509Certificate; }
        }

        public string X509Password
        {
            get { return x509Password; }
        }
    }
}
