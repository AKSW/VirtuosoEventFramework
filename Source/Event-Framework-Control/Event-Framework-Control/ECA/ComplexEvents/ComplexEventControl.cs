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
    public partial class ComplexEventControl : UserControl
    {
        public List<InitialStage> Stages {get; set; }
        //public List<Tuple<List<ParameterMap>, List<ParameterMap>>> Mappings { get; set; }
        public int SelectedStage { get; private set; }
        private bool isNewEvent = true;
        private List<CheckBox> checkBoxes = new List<CheckBox>();
        private ActionSelectControl actionSelection;
        private ConditionsQueryControl conditionsSelection;
        private List<EventSetControl> EventSetControls = new List<EventSetControl>();
        private ComplexEvent ev;

        public ComplexEventControl()
        {
            InitializeComponent();
            SelectedStage = 0;
            Stages = new List<InitialStage>();
            //Mappings = new List<Tuple<List<ParameterMap>, List<ParameterMap>>>();
            //Stages.Add(new InitialStage(new InitialEventSet(0, null)));
            Stages.Add(new Stage(new InitialEventSet(0, null)));
            //Mappings.Add(new Tuple<List<ParameterMap>, List<ParameterMap>>(null, null));
            initialCB.CheckState = CheckState.Checked;
            checkBoxes.Add(initialCB);
            initializeStatePanel();

            foreach (Control cont in StaticHelper.GetOffsprings(this))
                cont.BringToFront();
            this.Controls.Add(actionsPanel);
            this.Controls.Add(conditionsPanel);
            this.Controls.Add(eventSetPanel);
            this.actionsPanel.SendToBack();
            this.conditionsPanel.SendToBack();
            this.eventSetPanel.SendToBack();

            this.repeaterCB.SelectedIndex = 1;
            this.initTimePicker.Value = DateTime.Now;
            this.initDatePicker.Value = DateTime.Now;
            updateDataSources();
        }

        private void updateDataSources()
        {
            StaticHelper.UpdateDataSources();
            this.complexEventsDGV.DataSource = StaticHelper.currentCE;

            List<string> cols = Properties.Settings.Default.complexEvViewCols.Replace(";", ",").Replace(" ", "").Split(',').ToList();

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

        private void saveCeBT_Click(object sender, EventArgs e)
        {
            updateDataSources();
            if (nameTB.Text.Length < 3)
            {
                MessageBox.Show("please enter a longer name");
                return;
            }
            if ((Stages[0].InitialEventSet).Events == null || (Stages[0].InitialEventSet as EventSet).Events.Count < 1)
            {
                MessageBox.Show("please define the first event set");
                return;
            }

            int repeatThis = 0;
            if (repeaterCB.SelectedIndex == 0)
                repeatThis = -1;
            else
                repeatThis = repeaterCB.SelectedIndex;

            TimeSpan span = TimeSpan.Zero;
            if(!string.IsNullOrEmpty(days1TB.Text)) //!NOT
                span = new TimeSpan(int.Parse(days1TB.Text), int.Parse(hours1TB.Text), int.Parse(minutes1TB.Text), 0);

            DateTime initAt = initDatePicker.Value.Date.AddHours(initTimePicker.Value.Hour).AddMinutes(initTimePicker.Value.Minute).AddSeconds(initTimePicker.Value.Second);
            if (activCB.Checked || !isNewEvent && initAt.Subtract(DateTime.Now).Add(new TimeSpan(0, 1, 0)) < TimeSpan.Zero)
                initAt = DateTime.Now;

            for (int i = 0; i < Stages.Count; i++)
            {
                if (i < Stages.Count - 1)
                    Stages[i].NextStage = (Stages[i + 1] as Stage);
                //Stages[i].ParameterMappings = new List<ParameterMap>();
                //if(Mappings[i].Item1 != null)
                //    Stages[i].ParameterMappings.AddRange(Mappings[i].Item1);
                //if (Mappings[i].Item2 != null)
                //    Stages[i].ParameterMappings.AddRange(Mappings[i].Item2);
            }            
            
            string zw="";
            if (isNewEvent)
            {
                ComplexEvent thisEvent = new ComplexEvent(StaticHelper.CurrentUser, nameTB.Text, Stages, repeatThis, span, initAt, descriptionRTB.Text, activCB.Checked, overlappingCB.Checked);
                zw = StaticHelper.ClientProxy.InsertComplexEvent(thisEvent);
            }
            else
            {
                ev.Stages = Stages;
                ev.InStage = Stages[0];
                ev.Description = descriptionRTB.Text;
                ev.InitializeAt = initAt;
                ev.IsActive = activCB.Checked;
                ev.IsOverlapping = overlappingCB.Checked;
                ev.Period = Time.TimeSpanToXsdDuration(span);
                ev.Recurrence = repeatThis;
                zw = StaticHelper.ClientProxy.UpdateComplexEvents(ev, StaticHelper.LoadComplexEvent(ev.CeId));
            }
            MessageBox.Show(zw);

            if (zw.Contains("added"))
                this.Dispose();
        }

        private void nextStageBT_Click(object sender, EventArgs e)
        {
            bool next = false;

            if (Stages.Count - 1 == SelectedStage)
            {
                if (SelectedStage == 0 && (Stages[SelectedStage].InitialEventSet.Events == null || Stages[SelectedStage].InitialEventSet.Events.Count == 0))
                {
                    MessageBox.Show("An Initial Stage must have an Event Set with at least one atomic- or complex event!");
                    return;
                }
                else if (SelectedStage > 0
                    && (Stages[SelectedStage].InitialEventSet.Events == null || Stages[SelectedStage].InitialEventSet.Events.Count == 0)
                    && DialogResult.OK == MessageBox.Show("Event-Set has no events. Are you sure to create a blank stage?", "Blank Stage", MessageBoxButtons.OKCancel))
                {
                    if ((Stages[SelectedStage] as Stage).TimeRestriction != null && (Stages[SelectedStage] as Stage).TimeRestriction.Duration.TotalSeconds > 0)
                        next = true;
                    else
                        MessageBox.Show("A blank stage needs a time-duration > 0!");
                }
                else
                    next = true;

                //TODO event-set validation!
                //else if(


                if (next)
                {
                    Stages.Add(new Stage(new EventSet(0, null, null)));
                    //Mappings.Add(new Tuple<List<ParameterMap>, List<ParameterMap>>(null, null));
                    Stages[SelectedStage].NextStage = (Stages[SelectedStage + 1] as Stage);
                    SelectedStage++;
                    
                    addNewStageBT();
                    initializeStatePanel();
                }
            }

            else if (Stages.Count - 1 != SelectedStage)
            {
                SelectedStage++;
            }
            initializeStatePanel();
        }

        private void addNewStageBT()
        {
            CheckBox bt = new CheckBox();
            bt.Appearance = Appearance.Button;
            bt.Image = Properties.Resources.buttonState;
            bt.TextAlign = ContentAlignment.MiddleCenter;
            bt.Size = initialCB.Size;
            bt.Name = SelectedStage.ToString();
            string text = "Satge " + SelectedStage.ToString();
            int zw = (31 - text.Length) / 2;
            for (int i = 0; i < zw; i++)
                text = " " + text + " ";
            bt.Text = text;
            this.stagePanel.Controls.Add(bt);
            this.checkBoxes.Add(bt);
            bt.Location = new Point(initialCB.Location.X, initialCB.Location.Y + (SelectedStage * 35));
            bt.Click += new EventHandler(checkBoxes_Click);
            bt.BringToFront();
        }

        public void initializeStatePanel()
        {
            foreach (CheckBox chk in checkBoxes)
                chk.Checked = false;
            checkBoxes[SelectedStage].Checked = true;

            if ((Stages[SelectedStage] as InitialStage).TimeRestriction == null || (Stages[SelectedStage] as InitialStage).TimeRestriction.Duration.TotalSeconds == 0)
            {
                hoursTB.Enabled = false;
                daysTB.Enabled = false;
                minutesTB.Enabled = false;
                secondsTB.Enabled = false;
                waitTillEndCB.Enabled = false;
                restrictionCB.Checked = false;
                hoursTB.Text = "";
                daysTB.Text = "";
                minutesTB.Text = "";
                secondsTB.Text = "";
                waitTillEndCB.Checked = false;
            }
            else
            {
                TimeSpan duration = (Stages[SelectedStage] as Stage).TimeRestriction.Duration;
                hoursTB.Enabled = true;
                hoursTB.Text = duration.Hours.ToString();
                daysTB.Enabled = true;
                daysTB.Text = duration.Days.ToString();
                minutesTB.Enabled = true;
                minutesTB.Text = duration.Minutes.ToString();
                secondsTB.Enabled = true;
                secondsTB.Text = duration.Seconds.ToString();
                waitTillEndCB.Enabled = true;
                waitTillEndCB.Checked = (Stages[SelectedStage] as Stage).TimeRestriction.WaitTillEnd;
                restrictionCB.Checked = true;
            }

            deleteStageBT.Visible = true;
            this.timePanel.Visible = true;

            if (Stages[SelectedStage].Actions != null && Stages[SelectedStage].Actions.Count > 0)
                nextStageBT.Visible = false;
            if(Stages.Count-1 > SelectedStage)
            {
                    actionsBT.Visible = false;
                    actionsLA.Visible = false;
                    nextStageBT.Visible = true;
            }
            if (SelectedStage > 0 && (Stages[SelectedStage] as Stage).TimeRestriction != null)
            {
                this.daysTB.Text = (Stages[SelectedStage] as Stage).TimeRestriction.Duration.Days.ToString();
                this.hoursTB.Text = (Stages[SelectedStage] as Stage).TimeRestriction.Duration.Hours.ToString();
                this.minutesTB.Text = (Stages[SelectedStage] as Stage).TimeRestriction.Duration.Minutes.ToString();
                this.secondsTB.Text = (Stages[SelectedStage] as Stage).TimeRestriction.Duration.Seconds.ToString();
                this.waitTillEndCB.Checked = (Stages[SelectedStage] as Stage).TimeRestriction.WaitTillEnd;
            }
            if (Stages.Count - 1 == SelectedStage)
            {
                deleteStageBT.Visible = true;
                actionsBT.Visible = true;
            }
            else
            {
                deleteStageBT.Visible = false;
                actionsBT.Visible = false;
            }

        }

        private void hoursTB_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length > 0 && Char.IsDigit((sender as TextBox).Text[(sender as TextBox).Text.Length - 1]))
            {
                foreach (Control tb in StaticHelper.GetOffsprings(timePanel))
                {
                    if (tb is TextBox && (tb as TextBox).Text == "")
                        (tb as TextBox).Text = "0";
                }
                (Stages[SelectedStage] as Stage).TimeRestriction = new Time(int.Parse(daysTB.Text), int.Parse(hoursTB.Text),
                    int.Parse(minutesTB.Text), int.Parse(secondsTB.Text), waitTillEndCB.Checked);
            }
            else
                (sender as TextBox).Text = (sender as TextBox).Text.Substring((sender as TextBox).Text.Length);
        }
        
        private void hours1TB_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length > 0 && Char.IsDigit((sender as TextBox).Text[(sender as TextBox).Text.Length - 1]))
            {
                foreach (Control tb in StaticHelper.GetOffsprings(this))
                {
                    if (tb is TextBox && tb.Name != "nameTB" && (tb as TextBox).Text == "")
                        (tb as TextBox).Text = "0";
                }
            }
            else
                (sender as TextBox).Text = (sender as TextBox).Text.Substring((sender as TextBox).Text.Length);
        }

        private void regulateVisibility(Control cont)
        {
            cont.Parent.BringToFront();
            cont.BringToFront();
        }

        private void eventSetBT_Click(object sender, EventArgs e)
        {
            if (EventSetControls.Count - 1 < SelectedStage)
            {
                EventSetControl compEventControl = null;
                if (SelectedStage == 0)
                    compEventControl = new EventSetControl("RootSet", Stages[SelectedStage].InitialEventSet, null, SetType.InitialEventSet);
                else
                    compEventControl = new EventSetControl("RootSet", Stages[SelectedStage].InitialEventSet, null, SetType.EventSet);
                EventSetControls.Add(compEventControl);

                compEventControl.Size = new Size(this.eventSetPanel.ClientSize.Width, this.eventSetPanel.ClientSize.Height);
                compEventControl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                this.eventSetPanel.Controls.Add(compEventControl);
                regulateVisibility(compEventControl);
            }
            else
            {
                eventSetPanel.BringToFront();
                EventSetControls[SelectedStage].BringToFront();
            }
        }

        private void actionsBT_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (ev != null && ev.CeId > 0)
                id = ev.CeId;
            if (this.actionsPanel.Controls.Cast<ActionSelectControl>().Count() == 0)
            {
                actionSelection = new ActionSelectControl(Stages, SelectedStage, id);
                actionSelection.Size = new Size(this.actionsPanel.ClientSize.Width, this.actionsPanel.ClientSize.Height);
                actionSelection.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                //this.SendToBack();
                this.actionsPanel.Controls.Add(actionSelection);
                actionsPanel.BringToFront();
                regulateVisibility(actionSelection);
                initializeStatePanel();
            }
            else
            {
                actionsPanel.BringToFront();
                regulateVisibility(actionSelection);
                initializeStatePanel();
            }
        }

        private void conditionsBT_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (ev != null && ev.CeId > 0)
                id = ev.CeId;
//
            if (this.conditionsPanel.Controls.Cast<ConditionsQueryControl>().Count() == 0)
            {
                conditionsSelection = new ConditionsQueryControl(Stages, SelectedStage, Activity.Condition, id);
                conditionsSelection.Size = new Size(this.conditionsPanel.ClientSize.Width, this.conditionsPanel.ClientSize.Height);
                conditionsSelection.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                this.SendToBack();
                this.conditionsPanel.Controls.Add(conditionsSelection);
                conditionsPanel.BringToFront();
                regulateVisibility(conditionsSelection);
                initializeStatePanel();
            }
            else
            {
                conditionsPanel.BringToFront();
                regulateVisibility(conditionsSelection);
                initializeStatePanel();
            }
 
        }

        private void checkBoxes_Click(object sender, EventArgs e)
        {

            if ((sender as CheckBox).Text.Trim() == initialCB.Text.Trim())
                SelectedStage = 0;
            else
                SelectedStage = int.Parse((sender as CheckBox).Name);

            initializeStatePanel();
        }

        private void waitTillEndCB_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control tb in StaticHelper.GetOffsprings(timePanel))
            {
                if (tb is TextBox && (tb as TextBox).Text == "")
                    (tb as TextBox).Text = "0";
            }
                (Stages[SelectedStage] as Stage).TimeRestriction = new Time(int.Parse(daysTB.Text), int.Parse(hoursTB.Text),
                    int.Parse(minutesTB.Text), int.Parse(secondsTB.Text), waitTillEndCB.Checked);
        }

        private void newCeBT_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Do you want to save this event?", "save event", MessageBoxButtons.OKCancel))
            {
                saveCeBT_Click(sender, e);
            }

            this.Dispose();
            this.isNewEvent = true;
        }

        private void activCB_CheckedChanged(object sender, EventArgs e)
        {
            if (activCB.Checked == true)
            {
                initTimePicker.Enabled = false;
                initDatePicker.Enabled = false;
            }
            else
            {
                initTimePicker.Enabled = true;
                initDatePicker.Enabled = true;
            }
        }

        private void complexEventsDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!string.IsNullOrEmpty(nameTB.Text) &&
                DialogResult.OK == MessageBox.Show("save the current event?", "save", MessageBoxButtons.OKCancel))
                this.saveCeBT_Click(new object(), new EventArgs());

            this.isNewEvent = false;
            List<InitialStage> stages = new List<InitialStage>();

            ev = StaticHelper.LoadComplexEvent(int.Parse(this.complexEventsDGV.Rows[e.RowIndex].Cells[0].Value.ToString()));
            //Mappings.Add(new Tuple<List<ParameterMap>, List<ParameterMap>>(null, null));

            TimeSpan duration = Time.XsdDurationToTimeSpan(ev.Period);
            this.days1TB.Text = duration.Days.ToString();
            this.hours1TB.Text = duration.Hours.ToString();
            this.minutes1TB.Text = duration.Minutes.ToString();

            this.initDatePicker.Value = ev.InitializeAt.Date;
            this.initTimePicker.Value = ev.InitializeAt;

            this.nameTB.Text = ev.Name;
            //this.nameTB.Enabled = false;
            this.descriptionRTB.Text = ev.Description;
            if (ev.Recurrence == -1)
                this.repeaterCB.SelectedIndex = 0;
            else
                this.repeaterCB.SelectedIndex = ev.Recurrence;
            this.activCB.Checked = ev.IsActive;
            this.overlappingCB.Checked = ev.IsOverlapping;
            EventSetControls.Clear();
            this.Stages = ev.Stages.Cast<InitialStage>().ToList();
            for (int i = checkBoxes.Count-1; i > 0; i--)
            {
                this.Controls.Remove(checkBoxes[i]);
                checkBoxes[i].Dispose();
                checkBoxes.RemoveAt(i);
            }
            //if (this.checkBoxes.Count > 1)
            //    this.checkBoxes.RemoveRange(1, checkBoxes.Count - 2);
            SelectedStage = 0;
            for (int i = 1; i < this.Stages.Count;i++ )
            {
                SelectedStage++;
                addNewStageBT();
            }
            SelectedStage = 0;
            checkBoxes_Click(checkBoxes[0], new EventArgs());
            initializeStatePanel();
        }

        //private ComplexEvent loadComplexEvent(ref DataTable ces, int rowNr)
        //{
        //    bool firstStage = true;
        //    int stageCount = 0;
        //    ComplexEvent ev = new ComplexEvent(ref ces, rowNr);
        //    ev.Stages = new List<InitialStage>();
        //    DataTable sets;
        //    DataTable paramMappings = StaticHelper.ClientProxy.GetParamMappings(ev.CeId);
        //    updateDataSources();

        //    List<EventAction> actions = new List<EventAction>();

        //    while (true)
        //    {

        //        sets = null;
        //        sets = StaticHelper.ClientProxy.GetStagesAndSets(ev.InitialStagUri, stageCount);
        //        if (sets == null)
        //            break;
                                
        //        List<ConditionQuery> conditions = new List<ConditionQuery>();
        //        List<ParameterMap> mapList = new List<ParameterMap>();
        //        foreach (DataRow row in paramMappings.Rows.Cast<DataRow>().Where(x => (int)(x[Constants.getParamMappings_CEID]) 
        //            == ev.CeId && (int)(x[Constants.getParamMappings_StageNr]) == stageCount))
        //        {
        //            if (Convert.ToInt32(row[Constants.getParamMappings_Condition]) == 1 && conditions.Where(y => y.ID == Convert.ToInt32(row[Constants.getParamMappings_ActionID])).Count() == 0)
        //                conditions.Add(new ConditionQuery(ref currentCO, currentCO.Rows.IndexOf(currentCO.Rows.Cast<DataRow>().Where(z => Convert.ToInt32(z[Constants.getParamMappings_ActionID]) == 
        //                Convert.ToInt32(row[Constants.getParamMappings_ActionID])).First())));
        //            if (Convert.ToInt32(row[Constants.getParamMappings_Condition]) == 0 && actions.Where(y => y.ID == Convert.ToInt32(row[Constants.getParamMappings_ActionID])).Count() == 0)
        //                actions.Add(new EventAction(ref currentAC, currentAC.Rows.IndexOf(currentAC.Rows.Cast<DataRow>().Where(z => Convert.ToInt32(z[Constants.getParamMappings_ActionID]) == 
        //                Convert.ToInt32(row[Constants.getParamMappings_ActionID])).First())));
        //            object zw = null;
        //            if (row[Constants.getParamMappings_StaticValue].ToString() != "NULL" && row[Constants.getParamMappings_StaticValueType].ToString() != "NULL")
        //            {
        //                zw = Convert.ChangeType(row[Constants.getParamMappings_StaticValue], Type.GetType(row[Constants.getParamMappings_StaticValueType].ToString()));
        //                mapList.Add(new ParameterMap((int)(row[Constants.getParamMappings_StageNr]), (int)(row[Constants.getParamMappings_ActionID]), (int)(row[Constants.getParamMappings_ActionNr]),
        //                    (int)(row[Constants.getParamMappings_ParamNr]), row[Constants.getParamMappings_Description].ToString(), zw, ev.CeId));
        //            }
        //            string ttt = row[Constants.getParamMappings_EventValueMap].ToString();
        //            if (!string.IsNullOrEmpty(row[Constants.getParamMappings_EventValueMap].ToString()))
        //            {
        //               mapList.Add(new ParameterMap((int)(row[Constants.getParamMappings_StageNr]), (int)(row[Constants.getParamMappings_ActionID]), (int)(row[Constants.getParamMappings_ActionNr]),
        //                    (int)(row[Constants.getParamMappings_ParamNr]), row[Constants.getParamMappings_Description].ToString(), null, ev.CeId, 0, row[Constants.getParamMappings_EventValueMap].ToString()));
        //            }
        //            if ((int)row[Constants.getParamMappings_ConditionQuery] > 0)
        //            {
        //                mapList.Add(new ParameterMap((int)(row[Constants.getParamMappings_StageNr]), (int)(row[Constants.getParamMappings_ActionID]), (int)(row[Constants.getParamMappings_ActionNr]),
        //                    (int)(row[Constants.getParamMappings_ParamNr]), row[Constants.getParamMappings_Description].ToString(), null, ev.CeId, (int)row[Constants.getParamMappings_ConditionQuery]));
        //            }
        //        }

        //        string firstSet = sets.Rows[0][Constants.getStagesFromEvents_Parent].ToString();

        //        EventSet set = createEventSetFromDatatable(sets, firstSet);
        //        if (firstStage)
        //        {
        //            ev.Stages.Add(new Stage(set, StaticHelper.ClientProxy.GetStageUriFromEvent(firstSet)));
        //            firstStage = false;
        //        }
        //        else
        //            ev.Stages.Add(new Stage(set, StaticHelper.ClientProxy.GetStageUriFromEvent(firstSet)));

        //        Mappings.Add(new Tuple<List<ParameterMap>, List<ParameterMap>>(null, null));
        //        (ev.Stages.Last() as InitialStage).TimeRestriction = StaticHelper.ClientProxy.GetTimeOfStage(ev.InitialStagUri, stageCount);
        //        ev.Stages.Last().ConditionQuerys = conditions;
        //        ev.Stages.Last().ParameterMappings = mapList;
        //        stageCount++;
        //    }
        //    try
        //    {
        //        ev.Stages.Last().Actions = actions;
        //    }
        //    catch (InvalidOperationException)
        //    {
                
        //    }

        //    return ev;
        //}

        //private EventSet createEventSetFromDatatable(DataTable source, string setName, EventSet parentSet = null)
        //{
        //    EventSet outSet;
        //    int id = int.Parse(setName.Substring(setName.LastIndexOf("EventSet") + 8));
        //    if (setName.Contains("MultiEventSet"))
        //        outSet = new MultiSet(id, parentSet, null);
        //    else
        //        outSet = new EventSet(id, parentSet, null);
        //    string operatorUri = source.Rows.Cast<DataRow>().Where(x => x[Constants.getStagesFromEvents_Parent].ToString().ToLower().Trim() 
        //        == setName.ToLower().Trim()).First()[Constants.getStagesFromEvents_Operator].ToString();
        //    outSet.Operator = (Operator)(Enum.Parse(typeof(Operator), operatorUri.Substring(operatorUri.LastIndexOf('/') + 1)));


        //    foreach (DataRow row in source.Rows.Cast<DataRow>().Where(x => x[Constants.getStagesFromEvents_Parent].ToString().ToLower().Trim() == setName.ToLower().Trim()))
        //    {
        //        if (row[Constants.getStagesFromEvents_Parent].ToString().Contains("MultiEventSet"))
        //        {
        //            (outSet as MultiSet).MaxCardinality = int.Parse(row[Constants.getStagesFromEvents_MaxRec].ToString());
        //            (outSet as MultiSet).MinCardinality = int.Parse(row[Constants.getStagesFromEvents_MinRec].ToString());
        //        }
        //        if (row[Constants.getStagesFromEvents_Event].ToString().Contains("AtomicEvent"))
        //            outSet.Events.Add(new AtomicEvent(
        //                ref currentAE, currentAE.Rows.IndexOf(currentAE.Rows.Cast<DataRow>().Where(y => y[Constants.trigID].ToString() ==
        //                    row[Constants.getStagesFromEvents_Event].ToString().Substring(row[Constants.getStagesFromEvents_Event].ToString().LastIndexOf("AtomicEvent") + 11)).First())));
        //        else if (row[Constants.getStagesFromEvents_Event].ToString().Contains("ComplexEvent"))
        //            outSet.Events.Add(new ComplexEvent(
        //               ref currentCE, currentCE.Rows.IndexOf(currentCE.Rows.Cast<DataRow>().Where(y => y[Constants.ceid].ToString() ==
        //                   row[Constants.getStagesFromEvents_Event].ToString().Substring(row[Constants.getStagesFromEvents_Event].ToString().LastIndexOf("ComplexEvent") + 12)).First())));
        //        else if (row[Constants.getStagesFromEvents_Event].ToString().Contains("EventSet"))
        //            outSet.Events.Add(createEventSetFromDatatable(source, row[Constants.getStagesFromEvents_Event].ToString(), outSet));
        //    }
        //    return outSet;
        //}

        private void activatrBT_Click(object sender, EventArgs e)
        {
            if (this.complexEventsDGV.SelectedCells.Count > 0)
            {
                int indexOfRow = StaticHelper.currentCE.Rows.IndexOf(StaticHelper.getDataBoundItem(complexEventsDGV.Rows[complexEventsDGV.SelectedCells[0].RowIndex]));
                if (indexOfRow < 0)
                {
                    updateDataSources();
                    MessageBox.Show("could not activate right now, please try again");
                }

                updateDataSources();
                ComplexEvent ev = StaticHelper.LoadComplexEvent(int.Parse(this.complexEventsDGV.Rows[indexOfRow].Cells[0].Value.ToString()));
                ev.IsActive = true;
                MessageBox.Show(StaticHelper.ClientProxy.ActivateComplexEvent(ev));
            }
            else
                MessageBox.Show("select an event first!");
        }

        private void editCeBT_Click(object sender, EventArgs e)
        {
            if (this.complexEventsDGV.SelectedCells.Count > 0)
            {
                this.complexEventsDGV_CellDoubleClick(sender, new DataGridViewCellEventArgs(complexEventsDGV.SelectedCells[0].ColumnIndex, complexEventsDGV.SelectedCells[0].RowIndex));
            }
            else
                MessageBox.Show("select an event first!");
        }

        private void deleteCeBT_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("are you sure to delete Complex Event " + 
                complexEventsDGV.Rows[complexEventsDGV.SelectedCells[0].RowIndex].Cells["Name"].Value.ToString() + "?", "delete complex event", MessageBoxButtons.OKCancel))
            {
                if (this.complexEventsDGV.SelectedCells.Count > 0)
                {
                    if ("1" == StaticHelper.ClientProxy.DeleteComplexEvent(StaticHelper.LoadComplexEvent(int.Parse(this.complexEventsDGV.Rows[complexEventsDGV.SelectedCells[0].RowIndex].Cells[0].Value.ToString()))))    
                        MessageBox.Show("event was deleted");
                }
                else
                    MessageBox.Show("select an event first!"); 
            }
        }

        private void restrictionCB_CheckedChanged(object sender, EventArgs e)
        {
            if (restrictionCB.Checked)
            {
                hoursTB.Enabled = true;
                daysTB.Enabled = true;
                minutesTB.Enabled = true;
                secondsTB.Enabled = true;
                waitTillEndCB.Enabled = true;
            }
            else
            {
                hoursTB.Enabled = false;
                daysTB.Enabled = false;
                minutesTB.Enabled = false;
                secondsTB.Enabled = false;
                waitTillEndCB.Enabled = false;
            }
        }

        private void deleteStageBT_Click(object sender, EventArgs e)
        {
            this.Stages.Remove(this.Stages.Last());
            //this.Mappings.Remove(this.Mappings.Last());
            this.checkBoxes.Last().Dispose();
            this.checkBoxes.Remove(this.checkBoxes.Last());
            SelectedStage--;
            Stages[SelectedStage].NextStage = null;
            initializeStatePanel();
        }
    }
}
