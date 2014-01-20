using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventOntology
{
    public enum Operator
    {
        And,
        Or,
        Xor,
        Not
    }

    public static class Constants
    {
        public static string dsInstance = "DSInstance";
        public static string dsName = "DSName";
        public static string dsType = "DSType";
        public static string controlID = "ControlID";
        public static string description = "Description";
        public static string procEndpoint = "ProcedureEndpoint";
        public static string sparqlEndpoint = "SparqlEndpointAddress";
        public static string rdfGraphs = "RdfGraphs";
        public static string userID = "UserID";
        public static string userName = "Name";
        public static string userPass = "Pass";
        public static string created = "Created";
        public static string lastLogIn = "LastLogIn";
        public static string session = "SessionNr";
        public static string accRights = "UserAccRight";
        public static string ecaRights = "ECADefRight";
        public static string trigID = "TriggerID";
        public static string trigType = "TriggerType";
        public static string trigName = "TriggerName";
        public static string altName = "AlternativeName" ;
        public static string intSource = "InternalSource";
        public static string trigReturnVals = "Values";
        public static string createdBy = "CreatedBy";
        public static string trigStatement = "Statement";
        public static string constKey = "Key";
        public static string constValue = "Value";
        public static string actionID = "ActionID";
        public static string condition = "Condition";
        public static string internalQuery = "InternalQuery";
        public static string actWsdl = "WsdlAddress";
        public static string actEndpoint = "EndpointAddress";
        public static string actMethode = "MethodeName";
        public static string actParamTypes = "ParamTypes";
        public static string actParams = "ParamDescr";
        public static string actReturnType = "ReturnType";
        public static string actReturn = "ReturnDescr";
        public static string actServiceUserName = "UserName";
        public static string actServicePassword = "Password";
        public static string actX509Cert = "X509Certificate";
        public static string actX509Pass = "X509Password";
        public static string ceid = "CEID";
        public static string ceName = "Name";
        public static string ceInternalStage = "InitialStageID";
        public static string ceActive = "IsActive";
        public static string ceOverlapping = "IsOverlapping";
        public static string eventID = "EventID";
        public static string eventOccurence = "Occurence";
        public static string eventValues = "Row";
        public static string eventValidUntil = "Until";

    }
}
