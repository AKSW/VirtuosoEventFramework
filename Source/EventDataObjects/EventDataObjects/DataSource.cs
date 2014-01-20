using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EventOntology
{
    public class DataSource
    {

        public int DsInstance { get; private set; }
        public int ControlId { get; private set; }
        public string DsName { get; private set; }
        public string DsType { get; private set; }
        public string Description { get; private set; }
        public string ProcedureEndpoint { get; private set; }
        public string RdfGraphs { get; private set; }
        public string SparqlEndpoint { get; private set; }

        public DataSource(string DsName ,string DsType ,string Description ,string ProcedureEndpoint = null ,string RdfGraphs = null ,string SparqlEndpoint = null)
        {
            this.DsName = DsName;
            this.DsType = DsType;
            this.Description = Description;
            this.ProcedureEndpoint = ProcedureEndpoint;
            this.RdfGraphs = RdfGraphs;
            this.SparqlEndpoint = SparqlEndpoint;
        }

        DataSource(ref DataTable table, int rowIndex)
        {
            DataRow row = table.Rows[rowIndex];
            this.ControlId = (int)(row.ItemArray[table.Columns[Constants.controlID].Ordinal]);
            this.Description = row.ItemArray[table.Columns[Constants.description].Ordinal].ToString();
            this.DsInstance = (int)(row.ItemArray[table.Columns[Constants.dsInstance].Ordinal]);
            this.DsName = row.ItemArray[table.Columns[Constants.dsName].Ordinal].ToString();
            this.DsType = row.ItemArray[table.Columns[Constants.dsType].Ordinal].ToString();
            this.ProcedureEndpoint = row.ItemArray[table.Columns[Constants.procEndpoint].Ordinal].ToString();
            this.RdfGraphs = row.ItemArray[table.Columns[Constants.rdfGraphs].Ordinal].ToString();
            this.SparqlEndpoint = row.ItemArray[table.Columns[Constants.sparqlEndpoint].Ordinal].ToString();
        }

    }
}
