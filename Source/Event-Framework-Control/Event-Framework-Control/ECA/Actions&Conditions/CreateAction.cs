using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using EventOntology;

namespace EventFrameworkControl
{
    public partial class CreateActionCondition : Form
    {
        public int QueryID { get; private set; }
        private Activity act;
        private bool x509Changed = false;
        private DataGridViewRow source;
        private EventAction actcond;
        private DataTable databases = null;

        public CreateActionCondition(Activity act)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            editActionBT.Visible = false;
            this.act = act;
            manageActivity(act);

            if (act == Activity.Action)
                actcond = new EventAction();
            else
                actcond = new ConditionQuery();

            databases = StaticHelper.ClientProxy.GetDatabases("Virtuoso");

            databasesCB.Items.Clear();
            databasesCB.Items.Add("");
            object[] items = new object[databases.Rows.Count];
            for (int i = 0; i < databases.Rows.Count; i++)
                items[i] = databases.Rows[i]["DSInstance"].ToString() + " - " + databases.Rows[i]["DSName"].ToString() + " - " + databases.Rows[i]["ProcedureEndpoint"].ToString();
            databasesCB.Items.AddRange(items);

            updateSparqlEndpoints();
        }

        private void updateSparqlEndpoints()
        {
            sparqlEndpointCB.Items.Clear();
            object[] items = new object[databases.Rows.Count];
            for (int i = 0; i < databases.Rows.Count; i++)
                items[i] = databases.Rows[i]["SparqlEndpointAddress"];
            sparqlEndpointCB.Items.AddRange(items);
        }

        public CreateActionCondition(DataGridViewRow row, Activity act)
        {
            InitializeComponent();
            this.source = row;
            this.testActionBT.Visible = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.act = act;

            if (act == Activity.Action)
            {
                actcond = new EventAction(row);
            }
            else
            {
                actcond = new ConditionQuery(row);
            }
            updateSparqlEndpoints();

            manageActivity(act);

            descriptionRTB.Text = actcond.Description;
            wsdlTB.Text = actcond.WsdlAddress;
            proxyEndpointCB.SelectedItem = actcond.EndpointAddress;
            methodeCB.Text = actcond.MethodeName;
            if (!string.IsNullOrEmpty(actcond.X509Password))
                x509PasswordTB.Text = "--a password was provided (not shown)--";
            if (!string.IsNullOrEmpty(actcond.X509Certificate))
                x509CertificateTB.Text = "--a certificate was stored (not shown)--";
            if (!string.IsNullOrEmpty(actcond.ServicePassword))
                this.passwordTB.Text = "--a password was provided (not shown)--";
            if (!string.IsNullOrEmpty(actcond.ServiceUserName))
                this.usernameTB.Text = "--a service-username was provided (not shown)--";

            createActionBT.Visible = false;
        }

        private void manageActivity(Activity activety)
        {
            try
            {
                foreach (Control panel in StaticHelper.GetOffsprings(this).Where(x => x is Panel))
                {
                    foreach (Control cont in StaticHelper.GetOffsprings(panel).Where(x => x is Button))
                        (cont as Button).Text = (cont as Button).Text.Replace("action", activety.ToString().ToLower());
                    foreach (Control cont in StaticHelper.GetOffsprings(panel).Where(x => x is Label))
                        (cont as Label).Text = (cont as Label).Text.Replace("action", activety.ToString().ToLower());
                }
            }
            catch (Exception)
            {
            }

            if(activety == Activity.Action)
                titleLA.Text = "create an action on a remote endpoint";
            else if (activety == Activity.Condition)
            {
                titleLA.Text = "create a condition for a remote endpoint";
                sparqlLA.Text = "sparql query (only ASK-queries are supported by conditions!)";
            }
            else if (activety == Activity.Query)
                titleLA.Text = "create a query on a remote endpoint";

        }

        private void createActionBT_Click(object sender, EventArgs e)
        {
            if (x509CertificateTB.Text.Length > 6 && x509CertificateTB.Text[0] != '-' && x509PasswordTB.Text.Length > 2 && File.Exists(x509CertificateTB.Text))
            {
                byte[] buffer = File.ReadAllBytes(x509CertificateTB.Text);
                actcond.X509Certificate = System.Text.Encoding.Default.GetString(buffer);
            }

            actcond.WsdlAddress = wsdlTB.Text.Trim();
            actcond.EndpointAddress = proxyEndpointCB.SelectedItem.ToString().Trim();
            actcond.Description = descriptionRTB.Text;
            if (usernameTB.Text.Length > 2 && usernameTB.Text.Trim()[0] != '-')
                actcond.ServiceUserName = usernameTB.Text.Trim();
            if (passwordTB.Text.Length > 2 && passwordTB.Text.Trim()[0] != '-')
                actcond.ServicePassword = passwordTB.Text.Trim();
            if (x509PasswordTB.Text.Length > 2 && x509PasswordTB.Text.Trim()[0] != '-')
                actcond.X509Password = x509PasswordTB.Text.Trim();
            if (methodeCB.SelectedItem == null)
                actcond.MethodeName = methodeCB.Text.Trim();
            else
                actcond.MethodeName = methodeCB.SelectedItem.ToString();

            actcond.GetMethodParameterNamesAndTypes();
            
            if (databasesCB.SelectedIndex > 0)
                actcond.DSInstance = int.Parse(databasesCB.SelectedItem.ToString().Substring(0, databasesCB.SelectedItem.ToString().IndexOf(' ')));

            try
            {
                int QueryID = 0;
                if (this.act == Activity.Action)
                {
                    QueryID = StaticHelper.ClientProxy.InsertNewAction(actcond);
                }
                else if (this.act == Activity.Condition)
                {
                    QueryID = StaticHelper.ClientProxy.InsertNewCondition(actcond as ConditionQuery, Activity.Condition);
                }
                else if (this.act == Activity.Query)
                    QueryID = StaticHelper.ClientProxy.InsertNewCondition(actcond as ConditionQuery, Activity.Query);
                if (QueryID > 0)
                    MessageBox.Show("created");
                else
                    MessageBox.Show("an error occured");
            }
            catch (Exception)
            {
            }

            this.Close();
        }

        private void x509BrowseBT_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "X509Certificates|*.pfx";
            dialog.ShowDialog();
            x509CertificateTB.Text = dialog.FileName;
            dialog.Dispose();
        }

        private void x509CertificateTB_TextChanged(object sender, EventArgs e)
        {
            x509Changed = true;
        }

        private void methodeCB_DropDown(object sender, EventArgs e)
        {
            methodeCB.Items.Clear();
            if (wsdlTB.Text.Length > 5 && proxyEndpointCB.SelectedItem != null)
                methodeCB.Items.AddRange(DynamicSoapClientStatics.GetEndpointMethods(wsdlTB.Text.Trim(), proxyEndpointCB.SelectedItem.ToString().Trim()));
        }

        private void testActionBT_Click(object sender, EventArgs e)
        {
                TestActionCondition test = new TestActionCondition(source);
                test.ShowDialog();
        }

        private void editActionBT_Click(object sender, EventArgs e)
        {
            string certificate = actcond.X509Certificate;
            if (x509Changed && x509CertificateTB.Text.Length > 6 && x509PasswordTB.Text.Length > 2 && File.Exists(x509CertificateTB.Text))
            {
                byte[] buffer = File.ReadAllBytes(x509CertificateTB.Text);
                certificate = System.Text.Encoding.Default.GetString(buffer);
            }

            if (methodeCB.SelectedItem == null)
                actcond.MethodeName = methodeCB.Text.Trim();
            else
                actcond.MethodeName = methodeCB.SelectedItem.ToString();

            actcond.WsdlAddress = wsdlTB.Text.Trim();
            actcond.EndpointAddress = proxyEndpointCB.SelectedItem.ToString().Trim();
            actcond.Description = descriptionRTB.Text;
            if (usernameTB.Text.Length > 2 && usernameTB.Text.Trim()[0] != '-')
                actcond.ServiceUserName = usernameTB.Text.Trim();
            if (passwordTB.Text.Length > 2 && passwordTB.Text.Trim()[0] != '-')
                actcond.ServicePassword = passwordTB.Text.Trim();
            if (x509PasswordTB.Text.Length > 2 && x509PasswordTB.Text.Trim()[0] != '-')
                actcond.X509Password = x509PasswordTB.Text.Trim();
            actcond.GetMethodParameterNamesAndTypes();

            StaticHelper.ClientProxy.UpdateAction(actcond);
            this.Close();
        }

        private void sparqlCreateActionBT_Click(object sender, EventArgs e)
        {

            if (this.sparqlX509CertTB.Text.Length > 6 && sparqlX509CertTB.Text[0] != '-' && this.sparqlX509PassTB.Text.Length > 2 && File.Exists(sparqlX509CertTB.Text))
            {
                byte[] buffer = File.ReadAllBytes(sparqlX509CertTB.Text);
                actcond.X509Certificate = System.Text.Encoding.Default.GetString(buffer);
            }

            actcond.ReturnDescription = "a DataTable";
            actcond.ReturnType = typeof(DataTable).ToString();
            actcond.EndpointAddress = this.sparqlEndpointTB.Text.Trim();
            actcond.Description = this.sparqlDescrRTB.Text;

            if (databasesCB.SelectedIndex > 0)
                actcond.DSInstance = int.Parse(databasesCB.SelectedItem.ToString().Substring(0, databasesCB.SelectedItem.ToString().IndexOf(' ')));

            if (act == Activity.Condition && !sparqlRTB.Text.ToLower().Trim().Contains("ask")) //!NOT
            {
                MessageBox.Show("only ASK-queries are supported as conditions-queries!");
                return;
            }
            else
                actcond.SparqlQuery = sparqlRTB.Text;

            if (this.sparqlUserNameTB.Text.Length > 2 && this.sparqlUserNameTB.Text.Trim()[0] != '-')
                actcond.ServiceUserName = this.sparqlUserNameTB.Text.Trim();
            if (this.sparqlPasswordTB.Text.Length > 2 && sparqlPasswordTB.Text.Trim()[0] != '-')
                actcond.ServicePassword = sparqlPasswordTB.Text.Trim();
            if (this.sparqlX509PassTB.Text.Length > 2 && sparqlX509PassTB.Text.Trim()[0] != '-')
                actcond.X509Password = sparqlX509PassTB.Text.Trim();

            try
            {
                int QueryID = 0;
                if (this.act == Activity.Action)
                {
                    QueryID = StaticHelper.ClientProxy.InsertNewAction(actcond);
                }
                else if (this.act == Activity.Condition)
                {
                    QueryID = StaticHelper.ClientProxy.InsertNewCondition(actcond as ConditionQuery, Activity.Condition);
                }
                else if (this.act == Activity.Query)
                    QueryID = StaticHelper.ClientProxy.InsertNewCondition(actcond as ConditionQuery, Activity.Query);
                if (QueryID > 0)
                    MessageBox.Show("created");
                else
                    MessageBox.Show("an error occured");
            }
            catch (Exception)
            {
            }

            if (act == Activity.Query)
                this.Hide();
            else
                this.Close();
        }

        private void sparqlEditActionBT_Click(object sender, EventArgs e)
        {
            string certificate = actcond.X509Certificate;
            if (x509Changed && this.sparqlX509CertTB.Text.Length > 6 && sparqlX509CertTB.Text[0] != '-' && this.sparqlX509PassTB.Text.Length > 2 && File.Exists(sparqlX509CertTB.Text))
            {
                byte[] buffer = File.ReadAllBytes(sparqlX509CertTB.Text);
                actcond.X509Certificate = System.Text.Encoding.Default.GetString(buffer);
            }

            actcond.EndpointAddress = this.sparqlEndpointTB.Text.Trim();
            actcond.Description = this.sparqlDescrRTB.Text;
            if (this.sparqlUserNameTB.Text.Length > 2 && this.sparqlUserNameTB.Text.Trim()[0] != '-')
                actcond.ServiceUserName = this.sparqlUserNameTB.Text.Trim();
            if (this.sparqlPasswordTB.Text.Length > 2 && sparqlPasswordTB.Text.Trim()[0] != '-')
                actcond.ServicePassword = sparqlPasswordTB.Text.Trim();
            if (this.sparqlX509PassTB.Text.Length > 2 && sparqlX509PassTB.Text.Trim()[0] != '-')
                actcond.X509Password = sparqlX509PassTB.Text.Trim();

            StaticHelper.ClientProxy.UpdateAction(actcond);
            this.Close();
        }

        private void paramDescrBT_Click(object sender, EventArgs e)
        {
            paramPanel.BringToFront();
            if (paramPanel.Visible == true)
                paramPanel.Visible = false;
            else
                paramPanel.Visible = true;

            for (int i = 0; i < sparqlRTB.Text.Select((c, j) => sparqlRTB.Text.Substring(j)).Count(x => x.StartsWith("??")); i++)
            {
                if (actcond.ParamDescription != null && actcond.ParamDescription.Count > i)
                    paramDGV.Rows.Add(new object[] { i, actcond.ParamDescription[i] });
                else
                    paramDGV.Rows.Add(new object[] { i, "" });
            }
        }

        private void paramDGV_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            actcond.ParamDescription[e.RowIndex] = e.FormattedValue.ToString();
        }

        private void sparqlTestActionBT_Click(object sender, EventArgs e)
        {
            TestActionCondition test = new TestActionCondition(source);
            test.ShowDialog();
        }

        private void soapBT_Click(object sender, EventArgs e)
        {
            welcomPanel.Visible = false;
            sparqlPanel.Visible = false;
            wsdlPanel.Visible = true;
            wsdlPanel.BringToFront();
        }

        private void sparqlBT_Click(object sender, EventArgs e)
        {
            wsdlPanel.Visible = false;
            welcomPanel.Visible = false;
            sparqlPanel.Visible = true;

            sparqlEndpointCB.Visible = true;
            sparqlPanel.BringToFront();
        }

        private void paramDGV_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (actcond.ParamDescription == null || actcond.ParamDescription.Count != this.paramDGV.Rows.Count)
            {
                actcond.ParamTypes = new List<string>();
                actcond.ParamDescription = new List<string>();
                for (int i = 0; i < paramDGV.Rows.Count; i++)
                {
                    actcond.ParamDescription.Add(paramDGV.Rows[i].Cells[1].Value.ToString());
                    actcond.ParamTypes.Add(typeof(string).ToString());
                }
            }
            else
            {
                actcond.ParamDescription[e.RowIndex] = paramDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                actcond.ParamTypes[e.RowIndex] = typeof(string).ToString();
            }
        }

        private void endpointCB_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void proxyEndpointCB_DropDown(object sender, EventArgs e)
        {
            proxyEndpointCB.Items.Clear();
            if (wsdlTB.Text.Length > 5)
                proxyEndpointCB.Items.AddRange(DynamicSoapClientStatics.GetEndpoints(wsdlTB.Text.Trim()));
        }

        private void useOtherSparqlCB_CheckedChanged(object sender, EventArgs e)
        {
            if (useOtherSparqlCB.Checked)
            {
                sparqlEndpointCB.Visible = false;
                sparqlEndpointTB.Visible = true;

            }
            else
            {
                sparqlEndpointCB.Visible = true;
                sparqlEndpointTB.Visible = false;
                updateSparqlEndpoints();
            }
        }

        private void databasesCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (databasesCB.SelectedIndex > 0)
            {
                DataRow row = databases.Rows.Cast<DataRow>().Where(x => x.ItemArray[0].ToString() == databasesCB.SelectedItem.ToString().Substring(0, databasesCB.SelectedItem.ToString().IndexOf(' '))).First();
                if(row["DSType"].ToString() == "Virtuoso")
                    wsdlTB.Text = row["ProcedureEndpoint"].ToString() + "/services.wsdl";
                wsdlTB.Enabled = false;
            }
            else
                wsdlTB.Enabled = true;
        }
    }
}
