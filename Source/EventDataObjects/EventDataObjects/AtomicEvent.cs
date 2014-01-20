using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EventOntology
{
    public class AtomicEvent
    {

        public string triggerType { get; private set; }
        public int triggerId { get; private set; }
        public string triggerName { get; private set; }
        public string alternativeName { get; private set; }
        public DateTime created { get; private set; }
        public int createdBy { get; private set; }
        public string description { get; private set; }
        public int dsInstance { get; private set; }
        public string internalSource { get; private set; }
        public List<string> returnValues { get; private set; }
        public string triggerSyntax { get; private set; }

	    public AtomicEvent(string triggerType, string triggername, string alternativename, int userId, string description, 
            int dsInstance, string internalSource, string returnValues, string triggerSyntax)
        {
            this.triggerType = triggerType;
            this.triggerSyntax = triggerSyntax;
            this.triggerName = triggername;
            this.returnValues = returnValues.Replace(";", ",").Replace(" ", "").Split(',').ToList();
            this.internalSource = internalSource;
            this.dsInstance = dsInstance;
            this.description = description;
            this.createdBy = createdBy;
            this.created = DateTime.Now;
            this.alternativeName = alternativename;
	    }

        public AtomicEvent(ref DataTable table, int rowIndex)
        {
            DataRow row = table.Rows[rowIndex];
            this.alternativeName = row.ItemArray[table.Columns[Constants.altName].Ordinal].ToString();
            this.created = DateTime.Parse(row.ItemArray[table.Columns[Constants.created].Ordinal].ToString());
            this.createdBy = (int)(row.ItemArray[table.Columns[Constants.createdBy].Ordinal]);
            this.description = row.ItemArray[table.Columns[Constants.description].Ordinal].ToString();
            this.dsInstance = (int)(row.ItemArray[table.Columns[Constants.dsInstance].Ordinal]);
            this.triggerId = (int)(row.ItemArray[table.Columns[Constants.trigID].Ordinal]);
            this.internalSource = row.ItemArray[table.Columns[Constants.intSource].Ordinal].ToString();
            this.triggerName = row.ItemArray[table.Columns[Constants.trigName].Ordinal].ToString();
            this.returnValues = row.ItemArray[table.Columns[Constants.trigReturnVals].Ordinal].ToString().Replace(";", ",").Replace(" ", "").Split(',').ToList();
            this.triggerSyntax = row.ItemArray[table.Columns[Constants.trigStatement].Ordinal].ToString();
            this.triggerType = row.ItemArray[table.Columns[Constants.trigType].Ordinal].ToString();
        }
    }
}
