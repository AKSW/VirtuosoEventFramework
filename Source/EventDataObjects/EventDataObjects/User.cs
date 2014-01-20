using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EventOntology
{
    public class User
    {

        public int UserID { get; private set; }
        public DateTime Created { get; private set; }
        public bool EcaDefRight { get; private set; }
        public DateTime LastLogIn { get; private set; }
        public string Name { get; private set; }
        public string Pass { get; private set; }
        public int SessionNr { get; private set; }
        public bool UserAccRight { get; private set; }

        public User(string name, string pass, bool accountRights, bool ecaRights)
        {
            this.Name = name;
            this.Pass = pass;
            this.UserAccRight = accountRights;
            this.EcaDefRight = ecaRights;
            this.Created = DateTime.Now;
        }

        public User(ref DataTable table, int rowIndex)
        {
            DataRow row = table.Rows[rowIndex];
            this.Created = DateTime.Parse(row.ItemArray[table.Columns[Constants.created].Ordinal].ToString());
            this.EcaDefRight = Convert.ToBoolean(row.ItemArray[table.Columns[Constants.ecaRights].Ordinal]);
            this.UserAccRight = Convert.ToBoolean(row.ItemArray[table.Columns[Constants.accRights].Ordinal]);
            this.UserID = (int)(row.ItemArray[table.Columns[Constants.userID].Ordinal]);
            this.SessionNr = (int)(row.ItemArray[table.Columns[Constants.session].Ordinal]);
            this.Pass = row.ItemArray[table.Columns[Constants.userPass].Ordinal].ToString();
            this.Name = row.ItemArray[table.Columns[Constants.userName].Ordinal].ToString();
            this.LastLogIn = DateTime.Parse(row.ItemArray[table.Columns[Constants.lastLogIn].Ordinal].ToString());
        }
        
    }
}
