using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EventOntology;

namespace EventFrameworkControl
{
    public partial class atomicEventControl : UserControl
    {
        private DataTable databases;

        public atomicEventControl()

        {
            InitializeComponent();
            createTableTriggerBT.Enabled = false;
            createRdfTriggerBT.Enabled = false;
            registerEventBT.Enabled = false;
            deleteTriggerBT.Enabled = false;
            updateTriggerDataSource();
        }

        private void updateTriggerDataSource()
        {
            this.atomicEventsDGV.DataSource = StaticHelper.ClientProxy.GetAllTriggers("");
            List<string> cols = Properties.Settings.Default.atomicEvViewCols.Replace(";", ",").Replace(" ", "").Split(',').ToList();

            foreach (DataGridViewColumn col in this.atomicEventsDGV.Columns)
            {
                if (cols.Contains(col.Name))
                {
                    col.DisplayIndex = cols.IndexOf(col.Name);
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
                else
                    col.Visible = false;
            }
        }

        private void createEventBT_Click(object sender, EventArgs e)
        {
            if (chooseDbCB.SelectedItem!=null)
            {
                    CreateRegisterEvents createRegister = new CreateRegisterEvents(databases.Rows.Cast<DataRow>()
                        .Where(x => x.ItemArray[0].ToString() == chooseDbCB.SelectedItem.ToString()
                            .Substring(0, chooseDbCB.SelectedItem.ToString().IndexOf('-')).Trim()).First());
                    createRegister.ShowDialog();
                    updateTriggerDataSource();
            };
        }

        private void registerEventBT_Click(object sender, EventArgs e)
        {
            RegisterAtomicEvent reg = new RegisterAtomicEvent(int.Parse(this.chooseExternalCB.SelectedItem.ToString().Substring(0, chooseExternalCB.SelectedItem.ToString().IndexOf('-')).Trim()));
            reg.ShowDialog();
            updateTriggerDataSource();
        }

        private void deleteTriggerBT_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Discard selected event?", "discard event", MessageBoxButtons.OKCancel))
            {
                AtomicEvent ev = new AtomicEvent(this.atomicEventsDGV.Rows[atomicEventsDGV.SelectedCells[0].RowIndex]);
                StaticHelper.ClientProxy.DropTrigger(ev);
            }
            updateTriggerDataSource();
        }

        private void atomicEventsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                deleteTriggerBT.Enabled = true;
        }

        private void createRdfTriggerBT_Click(object sender, EventArgs e)
        {
            if (this.chooseDbCB.SelectedItem != null)
            {
                CreateRdfEvent createRdf = new CreateRdfEvent(int.Parse(this.chooseDbCB.SelectedItem.ToString().Substring(0, chooseDbCB.SelectedItem.ToString().IndexOf('-')).Trim()));
                createRdf.ShowDialog();

                updateTriggerDataSource();
            }
        }

        private void chooseDbCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            createTableTriggerBT.Enabled = true;
            createRdfTriggerBT.Enabled = true;
        }

        private void chooseDbCB_DropDown(object sender, EventArgs e)
        {
            databases = StaticHelper.ClientProxy.GetDatabases("Virtuoso");
            List<string> zw = new List<string>();
            for (int i = 0; i < databases.Rows.Count; i++)
                zw.Add(databases.Rows[i].ItemArray[0].ToString() + " - " + databases.Rows[i].ItemArray[1].ToString() + " - " + databases.Rows[i].ItemArray[2].ToString() + " - " + databases.Rows[i].ItemArray[3].ToString());
            this.chooseDbCB.Items.Clear();
            this.chooseDbCB.Items.AddRange(zw.ToArray());
        }

        private void chooseExternalCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            registerEventBT.Enabled = true;
        }

        private void chooseExternalCB_DropDown(object sender, EventArgs e)
        {
            databases = StaticHelper.ClientProxy.GetDatabases("external");
            List<string> zw = new List<string>();
            for (int i = 0; i < databases.Rows.Count; i++)
                zw.Add(databases.Rows[i].ItemArray[0].ToString() + " - " + databases.Rows[i].ItemArray[1].ToString() + " - " + databases.Rows[i].ItemArray[2].ToString()); // + " - " + databases.Rows[i].ItemArray[3].ToString());
            this.chooseExternalCB.Items.Clear();
            this.chooseExternalCB.Items.AddRange(zw.ToArray());
        }
    }
}