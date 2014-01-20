using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EventOntology
{
    class InternalConditionQuery
    {
        private int id;
        private int createdBy; //UserID
        private string description;
        private string internalQuery;
        private List<string> paramTypes;
        private List<string> paramDescription;
        private string returnType;
        private string returnDescription;
        

        public InternalConditionQuery(ref DataTable table, int rowIndex)
        {
            DataRow row = table.Rows[rowIndex];
            this.createdBy = (int)(row.ItemArray[table.Columns[Constants.createdBy].Ordinal]);
            this.description = row.ItemArray[table.Columns[Constants.description].Ordinal].ToString();
            this.id = (int)(row.ItemArray[table.Columns[Constants.actionID].Ordinal]);
            this.internalQuery = row.ItemArray[table.Columns[Constants.internalQuery].Ordinal].ToString();
            this.paramDescription = row.ItemArray[table.Columns[Constants.actParams].Ordinal].ToString().Replace(";", ",").Replace(" ", "").Split(',').ToList();
            this.paramTypes = row.ItemArray[table.Columns[Constants.actParamTypes].Ordinal].ToString().Replace(";", ",").Replace(" ", "").Split(',').ToList();
            this.returnDescription = row.ItemArray[table.Columns[Constants.actReturn].Ordinal].ToString();
            this.returnType = row.ItemArray[table.Columns[Constants.actReturnType].Ordinal].ToString();
        }

        public InternalConditionQuery(string description, string internalStatement, List<string> paramTypes = null, List<string> paramDescription = null, string returnType = null, string returnDescription = null)
        {
            this.description = description;
            this.paramDescription = paramDescription;
            this.paramTypes = paramTypes;
            this.returnDescription = returnDescription;
            this.returnType = returnType;
            this.internalQuery = internalStatement;
        }

        public object InvokeInternalQuery(object[] parameters)
        {
            return null;
        }

        public int ID
        {
            get { return id; }
        }

        public int CreatedBy
        {
            get { return createdBy; }
        }

        public string InternalQuery
        {
            get { return internalQuery; }
        }

        public string Description
        {
            get { return description; }
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
    }
}
