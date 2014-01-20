using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EventOntology;
using System.IO;

namespace EventFrameworkControl
{
    public partial class ActionsControl : UserControl
    {
        private Activity act;
        private bool x509Changed = false;
        private string initialCertPath = "";
        private EventAction actcond;

        public ActionsControl(Activity act)
        {
            InitializeComponent();

            changeActionBT.Visible = false;
            this.act = act;
            if (act == Activity.Action)
            {
                actcond = new EventAction();
                updateActionDataSource();
            }
            else
            {
                actcond = new ConditionQuery();
                updateActionDataSource();
                titleLA.Text = "conditions";
                this.changeActionBT.Text = this.changeActionBT.Text.Replace("action", "condition");
                this.deleteActionBT.Text = this.deleteActionBT.Text.Replace("action", "condition");
                    this.insertActionBT.Text = this.insertActionBT.Text.Replace("action", "condition");
                    this.testActionBT.Text = this.testActionBT.Text.Replace("action", "condition");
            }
        }

        private void updateActionDataSource()
        {
            this.actionsDGV.DataSource = StaticHelper.ClientProxy.GetActionsOrConditions(act);
            List<string> cols = Properties.Settings.Default.actionViewCols.Replace(";", ",").Replace(" ", "").Split(',').ToList();

            foreach (DataGridViewColumn col in this.actionsDGV.Columns)
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

        private void insertActionBT_Click(object sender, EventArgs e)
        {
            CreateActionCondition activ = new CreateActionCondition(act);
            activ.ShowDialog();
            updateActionDataSource();
        }
        
        private void refreshBT_Click(object sender, EventArgs e)
        {
            updateActionDataSource();
        }

        private void actionsDGV_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            deleteActionBT.Visible = true;
            changeActionBT.Visible = true;
        }

        private void deleteActionBT_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("delete item: " + this.actionsDGV.Rows[this.actionsDGV.SelectedCells[0].RowIndex].Cells[0].Value.ToString(),
                "delete action/condition", MessageBoxButtons.OKCancel))
            {
                StaticHelper.ClientProxy.DeleteActionsOrConditions(int.Parse(this.actionsDGV.Rows[this.actionsDGV.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
            }
        }

        private void changeActionBT_Click(object sender, EventArgs e)
        {
            CreateActionCondition cond = new CreateActionCondition(act);
            cond.ShowDialog();
            updateActionDataSource();
        }

        private void testActionBT_Click(object sender, EventArgs e)
        {
            if (actionsDGV.SelectedCells.Count > 0)
            {
                TestActionCondition test = new TestActionCondition(actionsDGV.Rows[actionsDGV.SelectedCells[0].RowIndex]);
                test.ShowDialog();
            }
        }
    }
}
