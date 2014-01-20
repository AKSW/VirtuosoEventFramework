using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EventOntology
{
    public class ComplexEvent
    {

        public int CeId { get; private set; }
        public int CreatedBy { get; private set; }
        public string Description { get; private set; }
        public InitialStage InStage { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsOverlapping { get; private set; }
        public string Name { get; private set; }
        public List<Action> Actions { get; private set; }

        public ComplexEvent(string Name , InitialStage stage, int CreatedBy, string Description = null, bool IsActive = false, bool IsOverlapping = false)
        {
            this.CreatedBy = CreatedBy;
            this.Name = Name;
            this.InStage = stage;
            this.Description = Description;
            this.IsActive = IsActive;
            this.IsOverlapping = IsOverlapping;

            InitialStage zw = stage;
            while (zw.Actions == null && zw.NextStage != null)
            {
                zw = zw.NextStage;
            }
            if (zw.NextStage != null)
                this.Actions = zw.Actions;

        }

        public ComplexEvent(ref DataTable table , int rowIndex)
        {
            DataRow row = table.Rows[rowIndex];
            this.CreatedBy = (int)(row.ItemArray[table.Columns[Constants.createdBy].Ordinal]);
            this.CeId = (int)(row.ItemArray[table.Columns[Constants.ceid].Ordinal]);
            this.Description = row.ItemArray[table.Columns[Constants.description].Ordinal].ToString();
            //this.InStage = new InitialStage(new InitialEventSet(), new Stage(new EventSet(new InitialEventSet(new List<AtomicEvent>(){new AtomicEvent(), new AtomicEvent()}), Operator.And)) );
        }

    }
}
