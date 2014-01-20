using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using EventOntology;

namespace EventFrameworkControl
{
    public partial class InstancesControl : UserControl
    {
        public InstancesControl()
        {
            InitializeComponent();

            this.instancesDGV.DataSource = StaticHelper.ClientProxy.GetComplexEventInstances();
        }

        private void refreshBT_Click(object sender, EventArgs e)
        {
            this.instancesDGV.DataSource = StaticHelper.ClientProxy.GetComplexEventInstances();
        }

        private void filterCeidBT_Click(object sender, EventArgs e)
        {
            Regex ceidReg = new Regex("[0-9]+");
            if (ceidReg.IsMatch(filterCeidTB.Text.Trim()))
                this.instancesDGV.DataSource = StaticHelper.ClientProxy.GetComplexEventInstances(int.Parse(filterCeidTB.Text.Trim()));
            else
                MessageBox.Show("please enter only digits");
        }

        private void abortBT_Click(object sender, EventArgs e)
        {
            if (this.instancesDGV.SelectedCells.Count > 0)
            {
                StaticHelper.ClientProxy.AbortComplexEventInstances(int.Parse(this.instancesDGV.Rows[instancesDGV.SelectedCells[0].RowIndex].Cells["CEID"].Value.ToString()));
                refreshBT_Click(sender, e);
            }
        }

        private void instancesDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.abortBT.Enabled = true;
                this.resetBT.Enabled = true;
            }
        }

        private void resetBT_Click(object sender, EventArgs e)
        {
            if (this.instancesDGV.SelectedCells.Count > 0)
            {
                int ceid = int.Parse(this.instancesDGV.Rows[instancesDGV.SelectedCells[0].RowIndex].Cells["CEID"].Value.ToString());
                StaticHelper.ClientProxy.AbortComplexEventInstances(ceid);
                DataTable events = StaticHelper.ClientProxy.GetComplexEvents("", ceid);
                ComplexEvent ev = new ComplexEvent(events, 0);
                StaticHelper.ClientProxy.ActivateComplexEvent(ev);
                refreshBT_Click(sender, e);
            }
        }
    }
}
