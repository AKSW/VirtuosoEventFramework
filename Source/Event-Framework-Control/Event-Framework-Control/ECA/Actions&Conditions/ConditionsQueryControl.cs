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
    public partial class ConditionsQueryControl :UserControl
    {
        public int LastQueryAddedID { get; private set; }
        public Dictionary<int, Tuple<ConditionQuery, List<ParameterMap>>> ActionDict { get; private set; }

        private List<InitialStage> stages;
        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;
        private int stageNr;
        private int ceid;
        private Activity condQuery;

        private List<ConditionQuery> conditions = new List<ConditionQuery>();

        public ConditionsQueryControl(List<InitialStage> stages, int stageNr, Activity condQuery = Activity.Condition, int ceid = 0)
        {
            InitializeComponent();

            ActionDict = new Dictionary<int, Tuple<ConditionQuery, List<ParameterMap>>>();
            this.stages = stages;
            this.stageNr = stageNr;
            this.ceid = ceid;
            this.condQuery = condQuery;
            updateActionDataSource();

            if (stages[stageNr].ConditionQuerys != null && stages[stageNr].ConditionQuerys.Count > 0 && stages[stageNr].ParameterMappings != null && stages[stageNr].ParameterMappings.Count > 0)
            {
                foreach (ConditionQuery act in stages[stageNr].ConditionQuerys)
                {
                    if (stages[stageNr].ParameterMappings.Where(x => x.ActionId == act.ID && !ActionDict.Keys.Contains(x.ActionId)).Count() > 0)
                    {
                        int order = stages[stageNr].ParameterMappings.Where(x => x.ActionId == act.ID && !ActionDict.Keys.Contains(x.ActionId)).First().ActionNr;
                        ActionDict.Add(order, new Tuple<ConditionQuery, List<ParameterMap>>(act, stages[stageNr].ParameterMappings.Where(y => y.ActionId == act.ID && y.ActionNr == order).ToList()));
                        object[] newRow = new object[selectedConditionDGV.Columns.Count];
                        newRow[0] = order;
                        newRow[1] = act.ID;
                        newRow[2] = act.EndpointAddress;
                        newRow[3] = act.MethodeName;
                        newRow[4] = act.ParamDescription;
                        newRow[5] = act.ReturnDescription;
                        selectedConditionDGV.Rows.Add(newRow);
                    }
                }
            }

            if (condQuery == Activity.Query)
            {
                foreach (Control cont in StaticHelper.GetOffsprings(this))
                {
                    cont.Text.Replace("condition", "query");
                }
            }
        }

        private void updateActionDataSource()
        {
            this.conditionsDGV.DataSource = StaticHelper.ClientProxy.GetActionsOrConditions(Activity.Condition);
            List<string> cols = Properties.Settings.Default.actionViewCols.Replace(";", ",").Replace(" ", "").Split(',').ToList();

            foreach (DataGridViewColumn col in this.conditionsDGV.Columns)
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
            if (selectedConditionDGV.SelectedCells.Count > 0
                    && DialogResult.OK == MessageBox.Show("remove condition: " + selectedConditionDGV.Rows[selectedConditionDGV.SelectedCells[0].RowIndex].Cells["ActionID"].Value.ToString()))
            {
                //ActionDict.Remove(selectedConditionDGV.SelectedCells[0].RowIndex);    
                ActionDict.Remove(ActionDict.Where(x => x.Value.Item2.First().ActionNr == (int)(selectedConditionDGV.Rows[selectedConditionDGV.SelectedCells[0].RowIndex].Cells["orderCol"].Value)).First().Key);
                this.selectedConditionDGV.Rows.RemoveAt(selectedConditionDGV.SelectedCells[0].RowIndex);
            }
        }

        private void dgv_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    conditionsDGV.DoDragDrop(conditionsDGV.Rows[rowIndexFromMouseDown], DragDropEffects.All);
                }
            }
        }

        private void dgv_MouseDown(object sender, MouseEventArgs e)
        {
            rowIndexFromMouseDown = conditionsDGV.HitTest(e.X, e.Y).RowIndex;

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
            Point clientPoint = this.selectedConditionDGV.PointToClient(new Point(e.X, e.Y));

            rowIndexOfItemUnderMouseToDrop = this.selectedConditionDGV.HitTest(clientPoint.X, clientPoint.Y).RowIndex;
            // If the drag operation was a move then remove and insert the sourceRow.
            if (rowIndexOfItemUnderMouseToDrop < 0)
                rowIndexOfItemUnderMouseToDrop = this.selectedConditionDGV.Rows.Count;

            if (e.Effect == DragDropEffects.Move && (condQuery == Activity.Condition || selectedConditionDGV.Rows.Count == 0))
            {
                DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

                object[] newRow = new object[selectedConditionDGV.Columns.Count + 1];
                int order = 0;
                if(this.selectedConditionDGV.Rows.Count > 0)
                    order = (int)(this.selectedConditionDGV.Rows[this.selectedConditionDGV.Rows.Count - 1].Cells[0].Value) + 1;
                newRow[0] = order;
                foreach (DataGridViewColumn col in selectedConditionDGV.Columns)
                {
                    if (col.Name != "orderCol")
                    {
                        newRow[col.DisplayIndex] = rowToMove.Cells[conditionsDGV.Columns.Cast<DataGridViewColumn>()
                    .Where(x => x.HeaderText == col.Name).First().Name].Value.ToString(); 
                    }
                }
                this.selectedConditionDGV.Rows.Add(newRow);

                ConditionQuery act = new ConditionQuery(rowToMove);
                LastQueryAddedID = act.ID;
                ParameterMapping map = null;
                if (act.ParamDescription != null && act.ParamDescription.Count > 0 && act.ParamDescription[0].Trim() != "")
                {
                    map = new ParameterMapping(act, ceid, stages, stageNr, this.selectedConditionDGV.Rows.Count - 1);
                    map.ShowDialog();
                    ActionDict.Add(order, new Tuple<ConditionQuery, List<ParameterMap>>(act, map.Map));
                    map.Dispose();
                }
                else
                {
                    ActionDict.Add(order, new Tuple<ConditionQuery, List<ParameterMap>>(act, new List<ParameterMap>() { new ParameterMap(stageNr, act.ID, order, 0, "alibi") }));
                }
            }
        }

        private void confirmBT_Click(object sender, EventArgs e)
        {
            Control ceControl = this;
            while (!(ceControl is ComplexEventControl) && ceControl != null) //not! 
                ceControl = ceControl.Parent;

            if (!(ceControl is ComplexEventControl))
            {
                this.Hide();
                return;
            }

            List<ConditionQuery> conditions = new List<ConditionQuery>();
            List<ParameterMap> mappings = new List<ParameterMap>();
            foreach (KeyValuePair<int, Tuple<ConditionQuery, List<ParameterMap>>> map in ActionDict)
            {
                conditions.Add(map.Value.Item1);
                mappings.AddRange(map.Value.Item2);
            }
            (ceControl as ComplexEventControl).Stages[(ceControl as ComplexEventControl).SelectedStage].ConditionQuerys = conditions;
            //(ceControl as ComplexEventControl).Mappings[(ceControl as ComplexEventControl).SelectedStage] = new Tuple<List<ParameterMap>, 
            //    List<ParameterMap>>((ceControl as ComplexEventControl).Mappings[(ceControl as ComplexEventControl).SelectedStage].Item1, mappings);
            (ceControl as ComplexEventControl).Stages[(ceControl as ComplexEventControl).SelectedStage].ParameterMappings.AddRange(mappings);
            this.Parent.SendToBack();
            this.SendToBack();
            this.Parent.Parent.BringToFront();
            (this.Parent.Parent as ComplexEventControl).initializeStatePanel();
        }

        private void addConditionBT_Click(object sender, EventArgs e)
        {
            CreateActionCondition act = new CreateActionCondition(condQuery);
            act.ShowDialog();
            updateActionDataSource();
        }

        private void editConditionBT_Click(object sender, EventArgs e)
        {
            CreateActionCondition act = new CreateActionCondition(this.conditionsDGV.Rows[this.conditionsDGV.SelectedCells[0].RowIndex], condQuery);
            act.ShowDialog();
            updateActionDataSource();
        }

        private void testConditionBT_Click(object sender, EventArgs e)
        {
            TestActionCondition test = new TestActionCondition(this.conditionsDGV.Rows[this.conditionsDGV.SelectedCells[0].RowIndex]);
            test.ShowDialog();
        }

        private void mappingBT_Click(object sender, EventArgs e)
        {
            if (this.selectedConditionDGV.SelectedCells.Count > 0)
            {
                ConditionQuery act = ActionDict[this.selectedConditionDGV.SelectedCells[0].RowIndex].Item1;

                if (act.ParamDescription != null && act.ParamDescription.Count > 0 && act.ParamDescription[0].Trim() != "")
                {
                    if (ActionDict.Values.ElementAt(this.selectedConditionDGV.SelectedCells[0].RowIndex).Item2 == null)
                    {
                        ParameterMapping map = new ParameterMapping(act, ceid, stages, stageNr, this.selectedConditionDGV.SelectedCells[0].RowIndex);

                        map.ShowDialog();
                        ActionDict.Remove(selectedConditionDGV.SelectedCells[0].RowIndex);
                        ActionDict.Add(selectedConditionDGV.SelectedCells[0].RowIndex, new Tuple<ConditionQuery, List<ParameterMap>>(act, map.Map));
                        map.Dispose();
                    }
                    else
                    {
                        ParameterMapping map = new ParameterMapping(act, ceid, stages, stageNr, this.selectedConditionDGV.SelectedCells[0].RowIndex,
                            ActionDict.Values.ElementAt(this.selectedConditionDGV.SelectedCells[0].RowIndex).Item2);

                        map.ShowDialog();
                        ActionDict.Remove(selectedConditionDGV.SelectedCells[0].RowIndex);
                        ActionDict.Add(selectedConditionDGV.SelectedCells[0].RowIndex, new Tuple<ConditionQuery, List<ParameterMap>>(act, map.Map));
                        map.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("no parameters needed");
                }
            }
        }

    }
}
