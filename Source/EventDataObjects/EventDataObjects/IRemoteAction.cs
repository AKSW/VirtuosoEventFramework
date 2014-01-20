using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventOntology
{
    public interface IRemoteAction
    {
        public string WsdlAddress { get; private set; }
        public string EndpointAddress { get; private set; }
        public string MethodeName { get; private set; }
        public List<Type> ParamTypes { get; private set; }
        public List<string> ParamDescription { get; private set; }
        public Type ReturnType {get; private set;}
        public string ReturnDescription { get; private set; }
        public string ServiceUserName  { get; private set; }
        public string ServicePassword { get; private set; }
        public string X509Certificate { get; private set; }
        public string X509Password { get; private set; }

        public object InvokeRemoteMethode(object[] parameter);
    }
}
