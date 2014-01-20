using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Channels;
using EventOntology;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace EventFrameworkControl
{
    public partial class AdminForm : Form
    {

        private User selectedUser = null;
        private int lastSelectedRow = -1;

        //DEBUG stuff
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public AdminForm()
        {
            InitializeComponent();
            StaticHelper.InitStatics();
            usersDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                                
            //DEBUG STUFF
            usernameTB.Text = "Admin";
            passwordTB.Text = "admin";

            deleteDsBT.Enabled = false;

            atomicEventControl eventControl = new atomicEventControl();
            eventControl.Size = atomicEventsTab.ClientSize;
            eventControl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.atomicEventsTab.Controls.Add(eventControl);

            ActionsControl actionControl = new ActionsControl(Activity.Action);
            actionControl.Size = actionsTab.ClientSize;
            actionControl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.actionsTab.Controls.Add(actionControl);

            ActionsControl conditionControl = new ActionsControl(Activity.Condition);
            conditionControl.Size = conditionsTab.ClientSize;
            conditionControl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.conditionsTab.Controls.Add(conditionControl);
            
            ComplexEventControl ceControl = new ComplexEventControl();
            ceControl.Size = this.complexEventTab.ClientSize;
            ceControl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            ceControl.Disposed += new EventHandler(ceControl_Disposed);
            this.complexEventTab.Controls.Add(ceControl);

            InstancesControl inst = new InstancesControl();
            inst.Size = this.instancesTab.ClientSize;
            inst.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.instancesTab.Controls.Add(inst);

            //DEBUG stuff
            timer.Interval = 5000;
            timer.Tick += new EventHandler(timer_Tick);
            timer_Tick(new object(), new EventArgs());
            timer.Start();

            //loginBT_Click(new object(), new EventArgs());
        }

        void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = (Exception )e.Exception;
            unhandledExceptionLog(ex);
        }

        /// <summary>
        /// reicht unhandled Exception an Log-writer weiter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception )e.ExceptionObject;
            unhandledExceptionLog(ex);
        }
        
        /// <summary>
        /// fängt alle unbehandelten Exceptions ab, speichert den StackTrace und Fehlermeldung in einer bestimmten Txt-Datei, beendet das Programm
        /// </summary>
        static void unhandledExceptionLog(Exception e)
        {
            if (e is System.ServiceModel.FaultException)
            {
                if (e.Message.Contains("no endpoint listening"))
                    MessageBox.Show("This datasource seems to be offline. Please check your connectivity.");
                else
                    MessageBox.Show("an unknowen database-error occurred: " + e.Message);
                StaticHelper.InitializeClientProxy();
                return;
            }
            else if (e is TargetInvocationException)
            {
                MessageBox.Show("An error while executing a remote-procedure occured.: " + e.Message);
                return;
            }
            else if (e is TimeoutException)
            {
                MessageBox.Show("The request timed out, please try again." + e.Message);
                return;
            }
            else if (e is CommunicationObjectFaultedException)
            {
                StaticHelper.InitializeClientProxy();
            }
            else
            {
                string errorTxtDateiPfad = Application.StartupPath + "\\errorLog.txt";
                MessageBox.Show("an unhandled exception occured:\n" + e.Message + "\n\nthis error be logged in the 'errorLog.txt' file");

                try
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(errorTxtDateiPfad, true))
                    {
                        file.WriteLine("-------------------------------------------------------------------");
                        file.WriteLine(DateTime.Now.ToString());
                        file.WriteLine(System.Environment.MachineName.ToString());
                        file.WriteLine(e.Message);
                        file.WriteLine(e.StackTrace.ToString());
                    }
                }
                catch (FileNotFoundException)
                {
                }
                Process.GetCurrentProcess().Kill();
            }
        }

        void ceControl_Disposed(object sender, EventArgs e)
        {
            ComplexEventControl ceControl = new ComplexEventControl();
            ceControl.Size = this.complexEventTab.ClientSize;
            ceControl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            ceControl.Disposed += new EventHandler(ceControl_Disposed);
            this.complexEventTab.Controls.Add(ceControl);
        }
        /// <summary>
        /// DEBUG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            //try{
                DataTable tb = StaticHelper.ClientProxy.GetEvents(1000);
                BindingSource bind = new BindingSource(tb, "");
                bind.Sort = "Occurence DESC";
                tripleDGV.DataSource = bind;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("an exception occured: " + ex.Message);
            //}
            foreach (DataGridViewColumn col in tripleDGV.Columns)
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            timer.Start();
        }

        private void loginBT_Click(object sender, EventArgs e)
        {
            StaticHelper.CurrentUser = new User(usernameTB.Text.Trim(), passwordTB.Text.Trim());
            try
            {
                StaticHelper.CurrentUser = StaticHelper.ClientProxy.LogIn(StaticHelper.CurrentUser);
                StaticHelper.CurrentUser.Pass = passwordTB.Text.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("an exception occured: " + ex.Message);
            }
            //if (returnStr == "000")
            //    MessageBox.Show("LogIn is not correct");
            //if (returnStr.Substring(0, 1) == "1")
            //    StaticHelper.UserAccountRights = true;                           //x = UserAccountRights (as 0,1) 
            //if (returnStr.Substring(1, 1) == "1")
            //    StaticHelper.EventDefinitionRights = true;                       //y = EventDefinitionRights
            //StaticHelper.LoggedIn = true;
            //StaticHelper.Username = usernameTB.Text;

        }

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainTabControl.SelectedTab != LogInTab && (StaticHelper.CurrentUser == null || StaticHelper.CurrentUser.SessionNr < 1))   //not!
            {
                MessageBox.Show("Please Log In first.");
                mainTabControl.SelectedTab = LogInTab;
            }
            else
            {
                if (mainTabControl.SelectedTab == this.EcaDefTab && !StaticHelper.CurrentUser.EcaDefRight) //not!
                {
                    mainTabControl.SelectedTab = LogInTab;
                    MessageBox.Show("You have no right to view this conetent.");
                }
                if (mainTabControl.SelectedTab == databaseTab)
                {
                    updateDataSourceCBs();
                    this.datasourceDGV.DataSource = StaticHelper.ClientProxy.GetDatabases("");
                }
                if (mainTabControl.SelectedTab == UserAccTab)
                {
                    this.usersDGV.DataSource = StaticHelper.ClientProxy.GetUsers("");
                }
            }
        }

        private void refreshBT_Click(object sender, EventArgs e)
        {
            DataTable tb = StaticHelper.ClientProxy.GetUsers("");
            tb.Columns.Add("HasEcaRights", typeof(bool));
            tb.Columns.Add("HasAccountRights", typeof(bool));
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                if (int.Parse(tb.Rows[i][StaticHelper.accRights].ToString()) == 1)
                    tb.Rows[i]["HasAccountRights"] = true;
                else
                    tb.Rows[i]["HasAccountRights"] = false;
                if (int.Parse(tb.Rows[i][StaticHelper.ecaRights].ToString()) == 1)
                    tb.Rows[i]["HasEcaRights"] = true;
                else
                    tb.Rows[i]["HasEcaRights"] = false;
            }
            
            usersDGV.DataSource = tb;

            //usersDGV.Columns["Pass"].Visible = false;
            usersDGV.Columns[StaticHelper.accRights].Visible = false;
            usersDGV.Columns[StaticHelper.ecaRights].Visible = false;
        }

        private void usersDGV_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (lastSelectedRow != e.RowIndex)
            {
                lastSelectedRow = e.RowIndex;
                if (e.RowIndex >= 0)
                {
                    selectedUser = new User(usersDGV.Rows[e.RowIndex]);
                    DataTable tb = StaticHelper.ClientProxy.GetDatabases("");
                    tb.Columns.Add("HasRight", typeof(bool));
                    string[] sources = selectedUser.Datasources.Replace(";", ",").Split(',');
                    for (int i = 0; i < tb.Rows.Count; i++)
                        if (sources.Contains("all") || sources.Contains(tb.Rows[i].ItemArray[0].ToString().Trim())) //!not
                            tb.Rows[i]["HasRight"] = true;
                        else
                            tb.Rows[i]["HasRight"] = false;

                    assignDatabasesDGV.DataSource = tb;
                    assignDatabasesDGV.Columns["HasRight"].DisplayIndex = 0;

                    foreach (DataGridViewColumn col in assignDatabasesDGV.Columns)
                        if (col.Name != "HasRight" && col.Name != "DSInstance" && col.Name != "DSName" && col.Name != "DSType" && col.Name != "Description")
                            col.Visible = false;
                }
                else
                {
                    selectedUser = null;
                    assignDatabasesDGV.DataSource = null;
                }
            }
        }

        private void newAccBT_Click(object sender, EventArgs e)
        {
            NewAccount acc = new NewAccount();
            acc.ShowDialog();
            try{
            if (StaticHelper.ClientProxy.CreateNewAccount(new User(acc.username, acc.password)))
                refreshBT_Click(new object(), new EventArgs());
            else
                MessageBox.Show("An unknown error has occured.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("an exception occured: " + ex.Message);
            }
            acc.Dispose();
        }


        private void resetPassBT_Click(object sender, EventArgs e)
        {
            if (selectedUser != null)
            {
                ConfirmPassword passWind = new ConfirmPassword(true);
                passWind.ShowDialog();

                try{
                    if (StaticHelper.ClientProxy.ResetUserpassword(selectedUser.Name, passWind.newPassword))
                    refreshBT_Click(new object(), new EventArgs());
                else
                    MessageBox.Show("It seems you entered a wrong password.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("an exception occured: " + ex.Message);
                }
                passWind.Dispose();
            }
            else
                MessageBox.Show("Please select an account to do this.");
        }

        private void databaseRefreshBT_Click(object sender, EventArgs e)
        {
            try{
                datasourceDGV.DataSource = StaticHelper.ClientProxy.GetDatabases("");
            }
            catch (Exception ex)
            {
                MessageBox.Show("an exception occured: " + ex.Message);
            }
            foreach (DataGridViewColumn col in datasourceDGV.Columns)
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string remote = remoteEndpointTB.Text.Trim();
            if (!StaticHelper.ClientProxy.CheckIfDsExists(remote))
            {
                object zw = "";
                try
                {
                    try
                    {
                        DataSource source = new DataSource(1, dbNameTB.Text, dbTypeCB.SelectedItem.ToString(), dbDescribtionRTB.Text, remote, sparqlAddressTB.Text.Trim());
                        zw = StaticHelper.ClientProxy.RegisterNewRemoteDataSource(source);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("an exception occured: " + ex.Message);
                    }

                    if (int.Parse(zw.ToString()) > 0)
                    {
                        if (dbTypeCB.SelectedItem.ToString().Contains("Virtuoso"))
                        {
                            EventAction insertTTL = new EventAction(remote + "/services.wsdl", remote, "EVENT_FRAMEWORK_INSERT_TTL_DATA", "insert rdf-data in Turtle-Syntax (optional: load ttl-file)",
                                new List<string>() { "System.Int32", "System.String", "System.String", "System.String", "System.Int32", "System.Int32" },
                                new List<string>() { "controlID", "optional: the rdf-data as ttl syntax", "optional: the graph to insert in", "optional: the file-path on the client to load rdf-data from", "optional: read file from position", "optional: read file to position" }, "", "", "", "", "", "", int.Parse(zw.ToString()));
                            StaticHelper.ClientProxy.InsertNewAction(insertTTL);
                            EventAction executeSPARQL = new EventAction(remote + "/services.wsdl", remote, "EVENT_FRAMEWORK_INTERNAL_SPARQL", "execute any query as action",
                                new List<string>() { "System.Int32", "System.String" },
                                new List<string>() { "controlID", "the query (start with SPARQL for sparql-queries)" },  "", "", "", "", "", "", int.Parse(zw.ToString()));
                            StaticHelper.ClientProxy.InsertNewAction(executeSPARQL);
                            ConditionQuery query = new ConditionQuery(remote + "/services.wsdl", remote, "EVENT_FRAMEWORK_INTERNAL_SPARQL", "execute any query",
                                new List<string>() { "System.Int32", "System.String" },
                                new List<string>() { "controlID", "the query (start with SPARQL for sparql-queries)" }, "", "", "", "", "", "", int.Parse(zw.ToString()));
                            StaticHelper.ClientProxy.InsertNewCondition(query, Activity.Query);
                            ConditionQuery cond = new ConditionQuery(remote + "/services.wsdl", remote, "EVENT_FRAMEWORK_INTERNAL_SPARQL", "execute a query as condition (only queries which return 0 or 1 can be used as a condition!)",
                                new List<string>() { "System.Int32", "System.String" },
                                new List<string>() { "controlID", "the query (start with SPARQL for sparql-queries)" }, "", "", "", "", "", "", int.Parse(zw.ToString()));
                            StaticHelper.ClientProxy.InsertNewCondition(cond, Activity.Condition);
                        }
                        MessageBox.Show("registration succeded");
                    }
                    refreshBT_Click(new object(), new EventArgs());
                    return;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("primary key"))
                        zw = "This DB may have been registered before. Please clear the Table EventFrameworkConstants and try again";
                }

                MessageBox.Show("an error occured: " + zw);
            }
            else
                MessageBox.Show("this procedure-endpoint is already in use");
        }

        private void submitNewDsBT_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dsNameTB.Text))
            {
                string zw = "";
                try
                {
                    try
                    {
                        DataSource source = new DataSource(1, dsNameTB.Text, "external event source", descriptionRTB.Text, "", sparqlAddressTB.Text);
                        zw = StaticHelper.ClientProxy.RegisterNewRemoteDataSource(source);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("an exception occured: " + ex.Message);
                    }

                    if (int.Parse(zw.ToString()) > 0)
                        MessageBox.Show("registration succeded");
                    refreshBT_Click(new object(), new EventArgs());
                    return;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("primary key"))
                        MessageBox.Show("This DB may have been registered before. Please clear the Table EventFrameworkConstants and try again");
                    else
                        MessageBox.Show("an exception occured: " + ex.Message);
                }
            }
            else
                MessageBox.Show("enter a name for this event source");
        }

        private void updateDataSourceCBs()
        {
            string[] dss = null;

            try
            {
                dss = StaticHelper.ClientProxy.GetSupportedDBs().Replace(";", ",").Replace(" ", "").Split(',');
            }
            catch (Exception ex)
            {
                MessageBox.Show("an exception occured: " + ex.Message);
            }
                foreach (string str in dss)
                    if (str.EndsWith("SQL"))
                        dbTypeCB.Items.Add(str.Replace("-SQL", ""));            
        }

        private void changeDatabasesBT_Click(object sender, EventArgs e)
        {
            if (selectedUser != null && selectedUser.Name != "Admin")
            {
                string zw = "";
                foreach (DataGridViewRow row in assignDatabasesDGV.Rows)
                    if (bool.Parse((row.Cells["HasRight"] as DataGridViewCheckBoxCell).Value.ToString()))
                        zw += "," + row.Cells["DSInstance"].Value.ToString();
                if (zw.Length > 0)
                    selectedUser.Datasources = zw.Substring(1);

                StaticHelper.ClientProxy.UpdateUserAccount(selectedUser); 
            }
        }

        private void usersDGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (selectedUser != null && selectedUser.Name != "Admin")
            {
                if (e.RowIndex >= 0 && usersDGV.Columns[e.ColumnIndex].Name == usersDGV.Columns["HasAccountRights"].Name)
                {
                    selectedUser.UserAccRight = (bool)(usersDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    usersDGV.Rows[e.RowIndex].Cells[StaticHelper.accRights].Value = Convert.ToInt32(usersDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                }
                else if (e.RowIndex >= 0 && usersDGV.Columns[e.ColumnIndex].Name == usersDGV.Columns["HasEcaRights"].Name)
                {
                    selectedUser.EcaDefRight = (bool)(usersDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    usersDGV.Rows[e.RowIndex].Cells[StaticHelper.ecaRights].Value = Convert.ToInt32(usersDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                }
            }
            else
                MessageBox.Show("You can't change these values for the 'Admin' account");
        }

        private void datasourceDGV_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.RowIndex >= 0)
                this.deleteDsBT.Enabled = true;
        }

        private void deleteDsBT_Click(object sender, EventArgs e)
        {
            if (this.datasourceDGV.SelectedCells.Count >0 && 1 < int.Parse(this.datasourceDGV.Rows[this.datasourceDGV.SelectedCells[0].RowIndex].Cells["DSInstance"].Value.ToString())
                && DialogResult.OK == MessageBox.Show("remove selected datasource and all dependant events?", "delete datasource", MessageBoxButtons.OKCancel)) 
                //TODO: check for dependant events - file under 'future stuff'
            {
                MessageBox.Show(StaticHelper.ClientProxy.DeleteDataSource(int.Parse(this.datasourceDGV.Rows[this.datasourceDGV.SelectedCells[0].RowIndex].Cells["DSInstance"].Value.ToString())));
                this.datasourceDGV.DataSource = StaticHelper.ClientProxy.GetDatabases("");
            }
        }

        private void deactivateDbBT_Click(object sender, EventArgs e)
        {
            if (datasourceDGV.SelectedCells.Count > 0 && !datasourceDGV.Rows[datasourceDGV.SelectedCells[0].RowIndex].Cells["DSType"].Value.ToString().Contains("external")) //is not an external event source
            {
                if (int.Parse(datasourceDGV.Rows[datasourceDGV.SelectedCells[0].RowIndex].Cells[0].Value.ToString()) > 1
                    && DialogResult.Yes == MessageBox.Show("(de)activate all atomic events on this database?", "deactivate", MessageBoxButtons.YesNo))
                {
                    DataSource ds = new DataSource(datasourceDGV.Rows[datasourceDGV.SelectedCells[0].RowIndex]);
                    if (ds.Active == 1)
                        ds.Active = 0;
                    else
                        ds.Active = 1;
                    MessageBox.Show(StaticHelper.ClientProxy.DeActivateDB(ds));
                }
                else if (int.Parse(datasourceDGV.Rows[datasourceDGV.SelectedCells[0].RowIndex].Cells[0].Value.ToString()) == 1
                    && DialogResult.Yes == MessageBox.Show("(de)activate central DB and the entire Event-Framework?\n(all occuring events will be retransmitted on activation)", "deactivate", MessageBoxButtons.YesNo))
                {
                    DataSource ds = new DataSource(datasourceDGV.Rows[datasourceDGV.SelectedCells[0].RowIndex]);
                    if (ds.Active == 1)
                        ds.Active = 0;
                    else
                        ds.Active = 1;
                    MessageBox.Show(StaticHelper.ClientProxy.DeActivateDB(ds));
                }
            }
            else
                MessageBox.Show("external event-sources can not be deactivated!");
        }
    }
}
