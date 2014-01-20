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
    public enum SetType
    {
        InitialEventSet,
        EventSet,
        MultiSet
    }
    public partial class EventSetControl : UserControl
    {
        private TextBox eventSyntaxTB;
        private SetType style;
        private static int esCount = 0;
        private DataGridView gridView;
        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;
        private InitialEventSet thisSet;
        private Operator op = Operator.Or;
        public string Name { get; private set; }

        public EventSetControl(string Name, InitialEventSet thisSet = null, InitialEventSet ParentSet = null, SetType style = SetType.EventSet)
        {
            InitializeComponent();
            this.Name = Name;
            this.style = style;
            eventSyntaxTB.HideSelection = false;
            eventSyntaxTB.GotFocus += new EventHandler(eventSyntaxTB_GotFocus);
            if (thisSet != null)
                this.thisSet = thisSet;
            else
                this.thisSet = new InitialEventSet(0, ParentSet);

            this.thisSet.UserControl = this;

            if(this.thisSet.Events == null)
                this.thisSet.Events = new List<IEventSetMember>();
            orCB.Checked = true;
            gridView = this.atomicEventsDGV;
            updateDataSources();

            if (thisSet.Events != null)
            {
                if (thisSet.Events.Where(a => a is EventSet).Count() > 0)
                    esCount = thisSet.Events.Where(a => a is EventSet).Cast<EventSet>().GroupBy(x => x.SetID > 0).Select(y => new { MaxID = y.Max(z => z.SetID) }).First().MaxID;
                loadEventSet();
            }

            SetStyle();
        }

        private void updateDataSources()
        {
            this.atomicEventsDGV.DataSource = StaticHelper.ClientProxy.GetAllTriggers("");
            this.complexEventsDGV.DataSource = StaticHelper.ClientProxy.GetComplexEvents("");
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
            cols = Properties.Settings.Default.complexEvViewCols.Replace(";", ",").Replace(" ", "").Split(',').ToList();
            foreach (DataGridViewColumn col in this.complexEventsDGV.Columns)
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

        void eventSyntaxTB_GotFocus(object sender, EventArgs e)
        {
            this.selectedEventsDGV.Focus();
        }

        private void loadEventSet()
        {
            this.op = Operator.Or;
            if (thisSet is EventSet)
                this.op = (thisSet as EventSet).Operator;

            foreach (IEventSetMember memb in thisSet.Events.Cast<IEventSetMember>())
            {
                if (thisSet is MultiSet)
                {
                    if (memb is AtomicEvent)
                        this.selectedEventsDGV.Rows.Add(new object[] { "AE", "AE" + (memb as AtomicEvent).triggerId.ToString(), (memb as AtomicEvent).triggerName, (memb as AtomicEvent).description, (thisSet as MultiSet).MinCardinality.ToString(), (thisSet as MultiSet).MaxCardinality.ToString() });
                    else if (memb is ComplexEvent)
                        this.selectedEventsDGV.Rows.Add(new object[] { "CE", "CE" + (memb as ComplexEvent).CeId.ToString(), (memb as ComplexEvent).Name, (memb as ComplexEvent).Description, (thisSet as MultiSet).MinCardinality.ToString(), (thisSet as MultiSet).MaxCardinality.ToString() });
                }
                else
                {
                    if (memb is AtomicEvent)
                        this.selectedEventsDGV.Rows.Add(new object[] { "AE", "AE" + (memb as AtomicEvent).triggerId.ToString(), (memb as AtomicEvent).triggerName, (memb as AtomicEvent).description });
                    else if (memb is ComplexEvent)
                        this.selectedEventsDGV.Rows.Add(new object[] { "CE", "CE" + (memb as ComplexEvent).CeId.ToString(), (memb as ComplexEvent).Name, (memb as ComplexEvent).Description });

                    else if (memb is MultiSet)
                    {
                        this.selectedEventsDGV.Rows.Add(new object[] { "MS", "MS" + (memb as MultiSet).SetID.ToString(), GenerateSyntax((memb as MultiSet)) });
                        EventSetControl control = new EventSetControl("MS" + (memb as MultiSet).SetID.ToString(), (memb as MultiSet), thisSet, SetType.MultiSet);
                        (memb as MultiSet).UserControl = control;
                        control.Size = this.ClientSize;
                        control.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                        this.Controls.Add(control);
                    }
                    else if (memb is EventSet)
                    {
                        this.selectedEventsDGV.Rows.Add(new object[] { "ES", "ES" + (memb as EventSet).SetID.ToString(), GenerateSyntax((memb as EventSet)) });
                        EventSetControl control = new EventSetControl("ES" + (memb as EventSet).SetID.ToString(), (memb as EventSet), thisSet, SetType.EventSet);
                        (memb as EventSet).UserControl = control;
                        control.Size = this.ClientSize;
                        control.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                        this.Controls.Add(control);
                    }
                }
            }
            SetStyle();
        }

        public string GenerateSyntax(InitialEventSet set = null)
        {

            if (set == null)
            {
                set = this.thisSet;
                while (set.ParentSet != null && set.ParentSet.Events != null && set.ParentSet.UserControl != null)
                {
                    set = set.ParentSet;
                } 
            }

            EventSetControl cont = (set.UserControl as EventSetControl);
            string syn = "";
            if (set.Events != null)
            {

                foreach (IEventSetMember memb in set.Events)
                {
                    if (memb is AtomicEvent)
                        syn += ",AE" + (memb as AtomicEvent).triggerId.ToString();
                    else if (memb is ComplexEvent)
                        syn += ",CE" + (memb as ComplexEvent).CeId.ToString();
                    //else if (memb is MultiSet)
                    //    syn += "," + GenerateSyntax((memb as MultiSet));
                    else if (memb is EventSet)
                        syn += "," + GenerateSyntax(memb as EventSet);
                    else if (memb is InitialEventSet)
                        syn += "," + GenerateSyntax((memb as InitialEventSet));

                    if (set is MultiSet)
                        syn += "," + (set as MultiSet).MinCardinality.ToString() + "," + (set as MultiSet).MaxCardinality.ToString();
                    if (memb is EventSet)
                        if ((memb as EventSet).SetID > esCount)
                            esCount = (memb as EventSet).SetID;
                    }
            }

                if (syn.Length > 0)
                    syn = "(" + syn.Substring(1) + ")";
                else
                    syn = "()";
                if (cont != null)
                {
                    if (cont.op != Operator.Or)
                        syn = "(" + cont.op.ToString().ToUpper() + syn + ")";

                    cont.eventSyntaxTB.Text = syn;
                }

          
            return syn;
        }

        public EventSet Finish(EventSetControl cont = null)
        {
            if (cont == null)
            {
                InitialEventSet zw = thisSet;
                while (true)
                {
                    if (zw.ParentSet != null)
                        zw = zw.ParentSet;
                    else
                    {
                        if (zw is EventSet && (zw as EventSet).ParentSet != null)
                            zw = (zw as EventSet).ParentSet;
                        else
                            break;
                    }
                }
                cont = (zw.UserControl as EventSetControl);
            }

            for (int i = 0; i < cont.thisSet.Events.Count; i++)
            {
                if ((cont.thisSet.Events[i] is InitialEventSet) && !(cont.thisSet.Events[i] is EventSet)) //!NOT
                    cont.thisSet.Events[i] = Finish((cont.thisSet.Events[i] as InitialEventSet).UserControl as EventSetControl);
                //else if((cont.thisSet.Events[i] is InitialEventSet))
                //    cont.thisSet.Events[i] = Finish((cont.thisSet.Events[i] as EventSet).UserControl as EventSetControl);
            }


            if (cont.thisSet.ParentSet == null)
            {
                (cont.Parent.Parent as ComplexEventControl).Stages[(cont.Parent.Parent as ComplexEventControl).SelectedStage].InitialEventSet
                    = new EventSet(thisSet.SetID, null, cont.thisSet.Events, cont.op);
                cont.Parent.SendToBack();
                cont.SendToBack();
                cont.Parent.Parent.BringToFront();
                (cont.Parent.Parent as ComplexEventControl).initializeStatePanel();
                return null;
            }
            else
            {
                return new EventSet(thisSet.SetID, cont.thisSet.ParentSet, cont.thisSet.Events, cont.op);
            }
            return null;
        }

        public void SetStyle()
        {
            if (this.style == SetType.MultiSet)
            {
                andCB.Enabled = false;
                xorCB.Enabled = false;
                notCB.Enabled = false;
                orCB.Enabled = false;
                insertSetsBT.Enabled = false;
                insertMultiSetBT.Enabled = false;
                this.selectedEventsDGV.Columns["minCol"].Visible = true;
                this.selectedEventsDGV.Columns["maxCol"].Visible = true;
            }
            else if (this.style == SetType.InitialEventSet)
            {
                andCB.Enabled = false;
                xorCB.Enabled = false;
                notCB.Enabled = false;
                orCB.Enabled = true;
                orCB.Checked = true;
                insertSetsBT.Enabled = false;
                insertMultiSetBT.Enabled = false;
                this.selectedEventsDGV.Columns["minCol"].Visible = false;
                this.selectedEventsDGV.Columns["maxCol"].Visible = false;

            }
            else if (this.style == SetType.EventSet)
            {
                andCB.Enabled = true;
                xorCB.Enabled = true;
                notCB.Enabled = true;
                orCB.Enabled = true;
                orCB.Checked = false;
                xorCB.Checked = false;
                andCB.Checked = false;
                notCB.Checked = false;
                insertSetsBT.Enabled = true;
                insertMultiSetBT.Enabled = true;
                this.selectedEventsDGV.Columns["minCol"].Visible = false;
                this.selectedEventsDGV.Columns["maxCol"].Visible = false;

                switch (op)
                {
                    case Operator.Or: 
                        orCB.Checked = true;
                        break;
                    case Operator.Xor:
                        xorCB.Checked = true;
                        break;
                    case Operator.And:
                        andCB.Checked = true;
                        break;
                    case Operator.Not:
                        notCB.Checked = true;
                        break;
                }

            }
            if (thisSet.ParentSet != null && thisSet.ParentSet.UserControl != null)
            {
                finishBT.Enabled = false;
                parentSetBT.Enabled = true;
            }
            else
            {
                finishBT.Enabled = true;
                parentSetBT.Enabled = false;
            }

            for (int i = 0; i < selectedEventsDGV.Rows.Count; i++)
            {
                List<IEventSetMember> zw = null;

                if (thisSet.Events != null)
                    zw = thisSet.Events;
                else
                    zw = (thisSet as EventSet).Events;
                if (zw[i] is MultiSet)
                {
                    selectedEventsDGV.Rows[i].Cells["descrColumn"].Value = GenerateSyntax(zw[i] as MultiSet);
                    selectedEventsDGV.Rows[i].DefaultCellStyle.BackColor = Color.Honeydew;
                }
                else if (zw[i] is EventSet)
                {
                    selectedEventsDGV.Rows[i].Cells["descrColumn"].Value = GenerateSyntax(zw[i] as EventSet);
                    selectedEventsDGV.Rows[i].DefaultCellStyle.BackColor = Color.Goldenrod;
                }
                else if (zw[i] is InitialEventSet)
                {
                    selectedEventsDGV.Rows[i].Cells["descrColumn"].Value = GenerateSyntax(zw[i] as InitialEventSet);
                    selectedEventsDGV.Rows[i].DefaultCellStyle.BackColor = Color.Goldenrod;
                }

            }

            GenerateSyntax();
        }

        private void dgv_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                        gridView.DoDragDrop(gridView.Rows[rowIndexFromMouseDown], DragDropEffects.All);
            }
        }

        private void dgv_MouseDown(object sender, MouseEventArgs e)
        {
            rowIndexFromMouseDown = gridView.HitTest(e.X, e.Y).RowIndex;

            if (rowIndexFromMouseDown != -1)
            {
                Size dragSize = SystemInformation.DragSize;
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),e.Y - (dragSize.Height / 2)),dragSize);
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

            if (this.style != SetType.MultiSet || this.selectedEventsDGV.Rows.Count == 0)
            {
                //lastMemberStrings = InternalSyntax.ToList();
                Point clientPoint = this.selectedEventsDGV.PointToClient(new Point(e.X, e.Y));

                rowIndexOfItemUnderMouseToDrop = this.selectedEventsDGV.HitTest(clientPoint.X, clientPoint.Y).RowIndex;
                // If the drag operation was a move then remove and insert the sourceRow.
                if (rowIndexOfItemUnderMouseToDrop < 0)
                    rowIndexOfItemUnderMouseToDrop = this.selectedEventsDGV.Rows.Count;

                if (e.Effect == DragDropEffects.Move)
                {
                    DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
                    string type = "";
                    if (this.eventSetTabControl.SelectedTab == this.atomicEventsTab)
                        type = "AE";
                    if (this.eventSetTabControl.SelectedTab == this.complexEventsTab)
                        type = "CE";


                    if (selectedEventsDGV.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["idColumn"].Value.ToString()
                        == type + rowToMove.Cells.Cast<DataGridViewTextBoxCell>().Where(z => z.ColumnIndex == gridView.Columns
                            .Cast<DataGridViewColumn>().Where(y => y.Name.ToLower().Contains("id")).First().Index)
                            .First().Value.ToString()).Count() == 0)  //!not
                    {
                        if (this.eventSetTabControl.SelectedTab == this.atomicEventsTab)
                        {
                            this.thisSet.Events.Add(new AtomicEvent(rowToMove));
                        }
                        if (this.eventSetTabControl.SelectedTab == this.complexEventsTab)
                        {
                            this.thisSet.Events.Add(new ComplexEvent(rowToMove));
                        }

                        this.selectedEventsDGV.Rows.Add(new object[]{ type,
                    type + rowToMove.Cells.Cast<DataGridViewTextBoxCell>().Where(x => x.ColumnIndex==gridView.Columns.Cast<DataGridViewColumn>().Where(y => y.Name.ToLower().Contains("id")).First().Index).First().Value.ToString()
                    , rowToMove.Cells.Cast<DataGridViewTextBoxCell>().Where(x => x.ColumnIndex==gridView.Columns.Cast<DataGridViewColumn>().Where(y => y.Name.ToLower().Contains("name")).First().Index).First().Value.ToString()
                    ,rowToMove.Cells.Cast<DataGridViewTextBoxCell>().Where(x => x.ColumnIndex==gridView.Columns.Cast<DataGridViewColumn>().Where(y => y.Name.ToLower().Contains("description")).First().Index).First().Value.ToString()});


                        GenerateSyntax();
                    }
                    else
                        MessageBox.Show("use a 'MultiSet'!");
                } 
            }
        }

        private void eventSetTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridView = this.eventSetTabControl.SelectedTab.Controls.Cast<DataGridView>().First();
            if (eventSetTabControl.SelectedTab == atomicEventsTab)
                gridView.DataSource = StaticHelper.ClientProxy.GetAllTriggers("");
            else if (eventSetTabControl.SelectedTab == complexEventsTab)
                gridView.DataSource = StaticHelper.ClientProxy.GetComplexEvents();
            //TODO
            //else if  (eventSetTabControl.SelectedTab == atomicEventsTab)
            //     gridView.DataSource = StaticHelper.ClientProxy
            gridView.AllowDrop = true;
        }

        private void finishBT_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void andCB_Click(object sender, EventArgs e)
        {
            andCB.CheckState = CheckState.Checked;
            notCB.CheckState = CheckState.Unchecked;
            xorCB.CheckState = CheckState.Unchecked;
            orCB.CheckState = CheckState.Unchecked;
            op = Operator.And;
            (thisSet as EventSet).Operator = Operator.And;
            GenerateSyntax();
     }

        private void orCB_Click(object sender, EventArgs e)
        {
            orCB.CheckState = CheckState.Checked;
            notCB.CheckState = CheckState.Unchecked;
            xorCB.CheckState = CheckState.Unchecked;
            andCB.CheckState = CheckState.Unchecked;
            op = Operator.Or;
            (thisSet as EventSet).Operator = Operator.Or;
            GenerateSyntax();
      }

        private void xorCB_Click(object sender, EventArgs e)
        {
            xorCB.CheckState = CheckState.Checked;
            notCB.CheckState = CheckState.Unchecked;
            orCB.CheckState = CheckState.Unchecked;
            andCB.CheckState = CheckState.Unchecked;
            op = Operator.Xor;
            (thisSet as EventSet).Operator = Operator.Xor;
            GenerateSyntax();
          }

        private void notCB_Click(object sender, EventArgs e)
        {
            notCB.CheckState = CheckState.Checked;
            xorCB.CheckState = CheckState.Unchecked;
            orCB.CheckState = CheckState.Unchecked;
            andCB.CheckState = CheckState.Unchecked;
            op = Operator.Not;
            (thisSet as EventSet).Operator = Operator.Not;
            GenerateSyntax();
 }

        private void insertSetsBT_Click(object sender, EventArgs e)
        {
            esCount++;
            //lastMemberStrings = InternalSyntax.ToList();
            this.selectedEventsDGV.Rows.Add(new object[] { "ES", "ES" + esCount.ToString() + "*", "", "new Event-Set" });
            if(thisSet != null)
                thisSet.Events.Add(new EventSet(esCount, this.thisSet, null));
            //InternalSyntax.Add("ESnew" + esCount.ToString());

            GenerateSyntax();
     }

        private void removeSetBT_Click(object sender, EventArgs e)
        {
            if (selectedEventsDGV.SelectedCells.Count > 0
                && DialogResult.OK == MessageBox.Show("delete item: " + selectedEventsDGV.Rows[selectedEventsDGV.SelectedCells[0].RowIndex].Cells["idColumn"].Value.ToString()))
            {
                this.thisSet.Events.RemoveAt(selectedEventsDGV.SelectedCells[0].RowIndex);
                this.selectedEventsDGV.Rows.RemoveAt(selectedEventsDGV.SelectedCells[0].RowIndex);
            }
            GenerateSyntax();
        }
        
        private void selectedEventsDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && this.selectedEventsDGV.Rows[e.RowIndex].Cells["typeColumn"].Value.ToString() == "ES")
            {
                if ((thisSet.Events[e.RowIndex] as EventSet).UserControl == null)
                {
                    EventSetControl control = new EventSetControl(selectedEventsDGV.Rows[e.RowIndex].Cells["idColumn"].Value.ToString(), (thisSet.Events.ElementAt(e.RowIndex) as InitialEventSet), thisSet, SetType.EventSet);
                    (thisSet.Events[e.RowIndex] as EventSet).UserControl = control;
                    control.Size = this.ClientSize;
                    control.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    this.Controls.Add(control);
                    control.BringToFront();
                }
                else if ((thisSet.Events[e.RowIndex] as EventSet) != null)
                {
                    this.SendToBack();
                    (thisSet.Events[e.RowIndex] as EventSet).UserControl.BringToFront();
                }
            }
            else if (e.RowIndex >= 0 && this.selectedEventsDGV.Rows[e.RowIndex].Cells["typeColumn"].Value.ToString() == "MS")
            {
                if ((thisSet.Events[e.RowIndex] as EventSet).UserControl == null)
                {
                    EventSetControl control = new EventSetControl(selectedEventsDGV.Rows[e.RowIndex].Cells["idColumn"].Value.ToString(), (thisSet.Events.ElementAt(e.RowIndex) as InitialEventSet), thisSet, SetType.MultiSet);
                    (thisSet.Events[e.RowIndex] as MultiSet).UserControl = control;
                    control.Size = this.ClientSize;
                    control.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    this.Controls.Add(control);
                    control.BringToFront();
                }
                else if ((thisSet.Events[e.RowIndex] as EventSet) != null)
                {
                    this.SendToBack();
                    (thisSet.Events[e.RowIndex] as EventSet).UserControl.BringToFront();
                }
            }
        }

        private void parentSetBT_Click(object sender, EventArgs e)
        {
            if (this.style == SetType.MultiSet && selectedEventsDGV.Rows.Count > 0)
            {
                if (int.Parse(selectedEventsDGV.Rows[0].Cells["maxCol"].Value.ToString()) >= int.Parse(selectedEventsDGV.Rows[0].Cells["minCol"].Value.ToString()))
                {
                    (this.thisSet as MultiSet).MaxCardinality = int.Parse(selectedEventsDGV.Rows[0].Cells["maxCol"].Value.ToString());
                    (this.thisSet as MultiSet).MinCardinality = int.Parse(selectedEventsDGV.Rows[0].Cells["minCol"].Value.ToString());
                }
                else
                {
                    MessageBox.Show("please select a max cardinality greater or equal to the min cardinality");
                    return;
                }
            }

            if (thisSet.ParentSet != null && thisSet.ParentSet.UserControl != null)
            {
                thisSet.ParentSet.UserControl.BringToFront();
                (thisSet.ParentSet.UserControl as EventSetControl).SetStyle();
                this.SendToBack();
            }
            SetStyle();
        }

        private void selectedEventsDGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (this.style == SetType.MultiSet)
            {
                List<object> zw = new List<object>();
                for (int i = 0; i < 101; i++)
                        zw.Add((i).ToString());

                if ((selectedEventsDGV.Rows[0].Cells["maxCol"] as DataGridViewComboBoxCell).Value != null
                    && int.Parse((selectedEventsDGV.Rows[0].Cells["maxCol"] as DataGridViewComboBoxCell).Value.ToString()) > 100)
                    (selectedEventsDGV.Rows[0].Cells["maxCol"] as DataGridViewComboBoxCell).Items.Add((selectedEventsDGV.Rows[0].Cells["maxCol"] as DataGridViewComboBoxCell).Value);
                if ((selectedEventsDGV.Rows[0].Cells["minCol"] as DataGridViewComboBoxCell).Value != null
                    && int.Parse((selectedEventsDGV.Rows[0].Cells["minCol"] as DataGridViewComboBoxCell).Value.ToString()) > 100)
                    (selectedEventsDGV.Rows[0].Cells["minCol"] as DataGridViewComboBoxCell).Items.Add((selectedEventsDGV.Rows[0].Cells["minCol"] as DataGridViewComboBoxCell).Value);

                (selectedEventsDGV.Rows[0].Cells["minCol"] as DataGridViewComboBoxCell).Items.AddRange(zw.ToArray());
                (selectedEventsDGV.Rows[0].Cells["maxCol"] as DataGridViewComboBoxCell).Items.AddRange(zw.ToArray());
                if((selectedEventsDGV.Rows[0].Cells["maxCol"] as DataGridViewComboBoxCell).Value == null)
                    (selectedEventsDGV.Rows[0].Cells["maxCol"] as DataGridViewComboBoxCell).Value = "1";
                if ((selectedEventsDGV.Rows[0].Cells["minCol"] as DataGridViewComboBoxCell).Value == null)
                    (selectedEventsDGV.Rows[0].Cells["minCol"] as DataGridViewComboBoxCell).Value = "1";

                    
            }
        }

        private void insertMultiSetBT_Click(object sender, EventArgs e)
        {
            esCount++;
            this.selectedEventsDGV.Rows.Add(new object[] { "MS", "MS" + esCount.ToString() + "*", "", "empty MultiSet" });
            if (thisSet != null)
                thisSet.Events.Add(new MultiSet(esCount, this.thisSet, null));

            GenerateSyntax();
        }

        private void selectedEventsDGV_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox)
            {
                (e.Control as ComboBox).DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        private void selectedEventsDGV_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex > 2)
            {
                if (this.selectedEventsDGV.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewComboBoxCell)
                {
                    if (!(this.selectedEventsDGV.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell).Items.Contains(e.FormattedValue)) //!not
                    {
                        (this.selectedEventsDGV.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell).Items.Add(e.FormattedValue);
                    }
                    (this.selectedEventsDGV.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell).Value =
                        (this.selectedEventsDGV.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell).Items[
                        (this.selectedEventsDGV.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell).Items.Count - 1];
                }
            }
        }

        private void cancelBT_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("return without saving?", "cancel", MessageBoxButtons.YesNo))
            {
                this.Parent.SendToBack();
                this.SendToBack();
                Control cont = this;
                while (!(cont is ComplexEventControl))
                {
                    cont.SendToBack();
                    cont = cont.Parent;
                }
                cont.BringToFront();
                (cont as ComplexEventControl).initializeStatePanel();
            }
        }
    }
}
