using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EventOntology;
using System.Collections.ObjectModel;

namespace EventFrameworkControl
{
    public partial class ActionSelectControl : UserControl
    {
        private List<InitialStage> stages;
        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;
        private int stageNr;
        private int ceid;
        private Dictionary<int, Tuple<EventAction, List<ParameterMap>>> actionDict = new Dictionary<int, Tuple<EventAction, List<ParameterMap>>>();

        public ActionSelectControl(List<InitialStage> stages, int stageNr, int ceid = 0)
        {
            InitializeComponent();
            this.stages = stages;
            this.stageNr = stageNr;
            this.ceid = ceid;

            updateActionDataSource();

            if (stages[stageNr].Actions != null && stages[stageNr].Actions.Count > 0 && stages[stageNr].ParameterMappings != null && stages[stageNr].ParameterMappings.Count > 0)
            {
                foreach (EventAction act in stages[stageNr].Actions)
                {
                    if (stages[stageNr].ParameterMappings.Where(x => x.ActionId == act.ID && !actionDict.Keys.Contains(x.ActionNr)).Count() > 0)
                    {
                        int order = stages[stageNr].ParameterMappings.Where(x => x.ActionId == act.ID && !actionDict.Keys.Contains(x.ActionNr)).First().ActionNr;
                        actionDict.Add(order, new Tuple<EventAction, List<ParameterMap>>(act, stages[stageNr].ParameterMappings.Where(y => y.ActionId == act.ID && y.ActionNr == order).ToList()));
                        object[] newRow = new object[selectedActionsDGV.Columns.Count];
                        newRow[0] = order;
                        newRow[1] = act.ID;
                        newRow[2] = act.EndpointAddress;
                        newRow[3] = act.MethodeName;
                        newRow[4] = act.ParamDescription;
                        newRow[5] = act.ReturnDescription;
                        selectedActionsDGV.Rows.Add(newRow);
                    }

                }
            }
        }

        private void updateActionDataSource()
        {
            this.actionsDGV.DataSource = StaticHelper.ClientProxy.GetActionsOrConditions(Activity.Action);
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

        private void removeBT_Click(object sender, EventArgs e)
        {
            if (selectedActionsDGV.SelectedCells.Count > 0
                    && DialogResult.OK == MessageBox.Show("remove action: " + selectedActionsDGV.Rows[selectedActionsDGV.SelectedCells[0].RowIndex].Cells["ActionID"].Value.ToString()))
            {
                //ActionDict.Remove(selectedActionsDGV.SelectedCells[0].RowIndex);    
                actionDict.Remove(actionDict.Where(x => x.Value.Item2.First().ActionNr == (int)(selectedActionsDGV.Rows[selectedActionsDGV.SelectedCells[0].RowIndex].Cells["orderCol"].Value)).First().Key);
                this.selectedActionsDGV.Rows.RemoveAt(selectedActionsDGV.SelectedCells[0].RowIndex);
            }
        }

        private void dgv_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    actionsDGV.DoDragDrop(actionsDGV.Rows[rowIndexFromMouseDown], DragDropEffects.All);
                }
            }
        }

        private void dgv_MouseDown(object sender, MouseEventArgs e)
        {
            rowIndexFromMouseDown = actionsDGV.HitTest(e.X, e.Y).RowIndex;

            if (rowIndexFromMouseDown != -1)
            {
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void dgv_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dgv_DragDrop(object sender, DragEventArgs e)
        {
            Point clientPoint = this.selectedActionsDGV.PointToClient(new Point(e.X, e.Y));

            rowIndexOfItemUnderMouseToDrop = this.selectedActionsDGV.HitTest(clientPoint.X, clientPoint.Y).RowIndex;
            // If the drag operation was a move then remove and insert the sourceRow.
            if (rowIndexOfItemUnderMouseToDrop < 0)
                rowIndexOfItemUnderMouseToDrop = this.selectedActionsDGV.Rows.Count;

            if (e.Effect == DragDropEffects.Move)
            {
                DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

                object[] newRow = new object[selectedActionsDGV.Columns.Count+1];
                int order = 0;
                if (this.selectedActionsDGV.Rows.Count > 0)
                    order = (int)(this.selectedActionsDGV.Rows[this.selectedActionsDGV.Rows.Count - 1].Cells[0].Value) + 1;
                newRow[0] = order;
                foreach (DataGridViewColumn col in selectedActionsDGV.Columns)
                {
                    if (col.Name != "orderCol")
                    {
                        newRow[col.DisplayIndex] = rowToMove.Cells[actionsDGV.Columns.Cast<DataGridViewColumn>()
                            .Where(x => x.HeaderText == col.Name).First().Name].Value.ToString();
                    }
                }
                this.selectedActionsDGV.Rows.Add(newRow);

                EventAction act = new EventAction(rowToMove);
                ParameterMapping map = null;
                if (act.ParamDescription != null && act.ParamDescription.Count > 0 && act.ParamDescription[0].Trim() != "")
                {
                    map = new ParameterMapping(act, ceid, stages, stageNr, this.selectedActionsDGV.Rows.Count - 1);
                    map.ShowDialog();
                    actionDict.Add(order, new Tuple<EventAction, List<ParameterMap>>(act, map.Map));
                    map.Dispose();
                }
                else
                {
                    actionDict.Add(order, new Tuple<EventAction, List<ParameterMap>>(act, new List<ParameterMap>(){new ParameterMap(stageNr, act.ID, order, 0, "alibi")}));
                }
            }
        }

        private void confirmBT_Click(object sender, EventArgs e)
        {
            ComplexEventControl ceControl = (this.Parent.Parent as ComplexEventControl);
            List<EventAction> actions = new List<EventAction>();
            List<ParameterMap> mappings = new List<ParameterMap>();
            foreach (KeyValuePair<int, Tuple<EventAction, List<ParameterMap>>> map in actionDict)
            {
                actions.Add(map.Value.Item1);
                mappings.AddRange(map.Value.Item2);
            }
            ceControl.Stages[ceControl.SelectedStage].Actions = actions;
            //ceControl.Mappings[ceControl.SelectedStage] = new Tuple<List<ParameterMap>,List<ParameterMap>>(mappings, ceControl.Mappings[ceControl.SelectedStage].Item2);
            ceControl.Stages[ceControl.SelectedStage].ParameterMappings.AddRange(mappings);
            this.Parent.SendToBack();
            this.SendToBack();
            this.Parent.Parent.BringToFront();
            (this.Parent.Parent as ComplexEventControl).initializeStatePanel();
        }

        private void mappingBT_Click(object sender, EventArgs e)
        {
            if (selectedActionsDGV.SelectedCells.Count > 0)
            {
                EventAction act = actionDict[this.selectedActionsDGV.SelectedCells[0].RowIndex].Item1;

                if (act.ParamDescription != null && act.ParamDescription.Count > 0 && act.ParamDescription[0].Trim() != "")
                {
                    if (actionDict.Values.ElementAt(this.selectedActionsDGV.SelectedCells[0].RowIndex).Item2 == null)
                    {
                        ParameterMapping map = new ParameterMapping(act, ceid, stages, stageNr, this.selectedActionsDGV.SelectedCells[0].RowIndex);

                        map.ShowDialog();
                        actionDict.Remove(selectedActionsDGV.SelectedCells[0].RowIndex);
                        actionDict.Add(selectedActionsDGV.SelectedCells[0].RowIndex, new Tuple<EventAction, List<ParameterMap>>(act, map.Map));
                        map.Dispose();
                    }
                    else
                    {
                        ParameterMapping map = new ParameterMapping(act, ceid, stages, stageNr, this.selectedActionsDGV.SelectedCells[0].RowIndex, 
                            actionDict.Values.ElementAt(this.selectedActionsDGV.SelectedCells[0].RowIndex).Item2);

                        map.ShowDialog();
                        actionDict.Remove(selectedActionsDGV.SelectedCells[0].RowIndex);
                        actionDict.Add(selectedActionsDGV.SelectedCells[0].RowIndex, new Tuple<EventAction, List<ParameterMap>>(act, map.Map));
                        map.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("no parameters needed");
                }
            }
        }

        private void addActionBT_Click(object sender, EventArgs e)
        {
            CreateActionCondition act = new CreateActionCondition(Activity.Action);
            act.ShowDialog();
            updateActionDataSource();
        }

        private void editActionBT_Click(object sender, EventArgs e)
        {
            CreateActionCondition act = new CreateActionCondition(this.actionsDGV.Rows[this.actionsDGV.SelectedCells[0].RowIndex], Activity.Action);
            act.ShowDialog();
            updateActionDataSource();
        }

        private void testActionBT_Click(object sender, EventArgs e)
        {
            TestActionCondition test = new TestActionCondition(this.actionsDGV.Rows[this.actionsDGV.SelectedCells[0].RowIndex]);
            test.ShowDialog();
        }
    }
}
