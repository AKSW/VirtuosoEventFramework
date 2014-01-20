using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EventOntology;
using System.Text.RegularExpressions;

namespace EventFrameworkControl
{
    public partial class ParameterMapping : Form
    {
        private int stageNr;
        private int actionNr;
        private int ceid;
        private int rtbCount = 0;
        private EventAction action;
        private List<Control> controls = new List<Control>();
        public List<ParameterMap> Map { get; set; }
        private bool addParams = true; 
        private List<InitialStage> stages;
        private int lastMappingIndex = -1;

        public ParameterMapping(EventAction action, int ceid, List<InitialStage> stages, int stageNr, int actionNr, List<ParameterMap> map = null)
        {
            InitializeComponent();
            this.stages = stages;
            this.stageNr = stageNr;
            this.ceid = ceid;
            this.action = action;
            this.actionNr = actionNr;
            
            if (map != null)
            {
                this.Map = map;
                addParams = false;
            }
            else
                this.Map = new List<ParameterMap>();

            if (action.ParamDescription == null || action.ParamDescription.Count == 0)
            {
                labelLA.Visible = false;
                textboxTB.Visible = false;
            }
            else
            {
                labelLA.Text = "parameter 1: " + action.ParamDescription[0] + " (Type: " + action.ParamTypes[0] + ")";
                if (action.ParamTypes[0].Contains("String"))
                {
                    textboxTB.Visible = false;
                    richTextBoxRTB.Visible = true;
                    rtbCount++;
                    if (!addParams && map[0].StaticValue != null)
                        richTextBoxRTB.Text = map[0].StaticValue.ToString();
                    if (!addParams && Map[0].EventValueMap != null)
                        richTextBoxRTB.Text = Map[0].EventValueMap;

                    this.controlPanel.Controls.Remove(textboxTB);
                    controls.Add(labelLA);
                    controls.Add(richTextBoxRTB);
                    if (addParams)
                        Map.Add(new ParameterMap(stageNr, action.ID, actionNr, 0, action.Description));
                }
                else
                {
                    this.controlPanel.Controls.Remove(richTextBoxRTB);
                    if (!addParams && map[0].StaticValue != null)
                        textboxTB.Text = map[0].StaticValue.ToString();
                    if (!addParams && Map[0].EventValueMap != null)
                        textboxTB.Text = Map[0].EventValueMap;

                    controls.Add(labelLA);
                    controls.Add(textboxTB);
                    if (addParams)
                        Map.Add(new ParameterMap(stageNr, action.ID, actionNr, 0, action.Description));

                    if (action.ParamDescription[0].ToString() == "controlID" && action.DSInstance > -1)
                    {
                        textboxTB.Text = getControlID(action.DSInstance);
                        textboxTB_Validated(textboxTB, new EventArgs());
                    }
                }
                controls.Add(queryBT);
                controls.Add(mappingBT);
            }

            if (action.ParamDescription != null && action.ParamDescription.Count > 1)
            {
                generateParamControls();
            }

            ScrollBar vScrollBar1 = new VScrollBar();
            vScrollBar1.Dock = DockStyle.Right;
            vScrollBar1.Height = controlPanel.Height;
            vScrollBar1.Scroll += (sender, e) => { controlPanel.VerticalScroll.Value = vScrollBar1.Value; };
            controlPanel.Controls.Add(vScrollBar1);
        }

        private TreeNode loadComplexTV(List<InitialStage> sta, int staNr)
        {
            if (staNr < 0)
                staNr = sta.Count - 1;
            TreeNode root = new TreeNode("ComplexEvent" + ceid.ToString());
            root.Tag = ceid;
            for (int i = 0; i <= stageNr; i++)
            {
                TreeNode acNode = new TreeNode("STAGE" + i.ToString() + " OP: " + (((sta[i] as Stage).InitialEventSet as EventSet).Operator.ToString()));
                acNode.Tag = i;
                acNode.Nodes.AddRange(loadEventSetTV(stages[i].InitialEventSet));
                root.Nodes.Add(acNode);
            }
            return root;
        }

        private TreeNode loadAtomicTV(AtomicEvent ae)
        {
            TreeNode aeRoot = new TreeNode("AtomicEvent" + ae.triggerId.ToString());
            aeRoot.Tag = ae.triggerId;
            string[] values = ae.returnValues.Replace(" ", "").Replace(";", ",").Replace(".", ",").Split(',');
            foreach (string val in values)
                aeRoot.Nodes.Add("Value: " + val);
            return aeRoot;
        }

        private TreeNode[] loadEventSetTV(InitialEventSet set)
        {
            List<TreeNode> nodes = new List<TreeNode>();

            if (set is MultiSet)
            {
                nodes.Add(new TreeNode("MultiEventSet" + (set as MultiSet).SetID.ToString() + " : " + (set as MultiSet).MinCardinality.ToString() + "-" + (set as MultiSet).MaxCardinality.ToString()));
                nodes.Last().Tag = (set as MultiSet).SetID;
                for (int i = (set as MultiSet).MinCardinality; i < (set as MultiSet).MaxCardinality; i++)
                {
                    if ((set as MultiSet).Events[0] is AtomicEvent)
                    {
                        nodes.Last().Nodes.Add(loadAtomicTV((set as MultiSet).Events[0] as AtomicEvent));
                        nodes.Last().Nodes[nodes.Last().Nodes.Count - 1].Text = i.ToString() + " : " + nodes.Last().Nodes[nodes.Last().Nodes.Count - 1].Text;
                    }
                    else if ((set as MultiSet).Events[0] is ComplexEvent)
                    {
                        nodes.Last().Nodes.Add(loadComplexTV(StaticHelper.LoadComplexEvent(ceid).Stages, -1));
                        nodes.Last().Nodes[nodes.Last().Nodes.Count - 1].Text = i.ToString() + " : " + nodes.Last().Nodes[nodes.Last().Nodes.Count - 1].Text;
                    }
                }
            }
            else if (set is EventSet)
            {
                foreach (IEventSetMember memb in set.Events)
                {
                    if (memb is AtomicEvent)
                        nodes.Add(loadAtomicTV(memb as AtomicEvent));
                    else if (memb is ComplexEvent)
                        nodes.Add(loadComplexTV((memb as ComplexEvent).Stages, -1));
                    else if (memb is MultiSet)
                        nodes.AddRange(loadEventSetTV(memb as MultiSet));
                    else
                    {
                        nodes.Add(new TreeNode("EventSet" + (memb as EventSet).SetID.ToString() + " OP: " + (memb as EventSet).Operator.ToString()));
                        nodes.Last().Tag = (memb as EventSet).SetID;
                        nodes.Last().Nodes.AddRange(loadEventSetTV(memb as EventSet));
                    }
                }
            }

            return nodes.ToArray();
        }

        private string getControlID(int dsInstance)
        {
            return StaticHelper.ClientProxy.getControlId(dsInstance).ToString();
        }

        private void generateParamControls()
        {
            for (int i = 1; i < action.ParamDescription.Count(); i++)
            {
                if(addParams)
                    Map.Add(new ParameterMap(stageNr, action.ID, actionNr, i, action.Description));

                Label la = new Label();
                la.Size = new System.Drawing.Size(this.textboxTB.Width, labelLA.Height);
                la.Location = new Point(labelLA.Location.X, labelLA.Location.Y + i * (this.mappingBT.Height + 20));
                la.Font = (labelLA.Font.Clone() as Font);
                la.Text = "parameter " + (i+1).ToString() + ": " + action.ParamDescription[i] + " (Type: " + action.ParamTypes[i] + ")";
                controls.Add(la);
                this.controlPanel.Controls.Add(la);

                if (action.ParamTypes[i].Contains("String"))
                {
                    RichTextBox rtb = new RichTextBox();
                    rtb.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    rtb.Size = richTextBoxRTB.Size;
                    rtb.Location = new Point(textboxTB.Location.X, textboxTB.Location.Y + i *(this.mappingBT.Height + 20) );
                    if (!addParams && Map[i].StaticValue != null)
                        rtb.Text = Map[i].StaticValue.ToString();
                    if (!addParams && Map[i].EventValueMap != null)
                        rtb.Text = Map[i].EventValueMap;
                    rtb.Validated += new EventHandler(textboxTB_Validated);
                    this.controlPanel.Controls.Add(rtb);
                    controls.Add(rtb);
                    rtbCount++;
                }
                else
                {
                    TextBox tb = new TextBox();
                    tb.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    tb.Size = textboxTB.Size;
                    tb.Location = new Point(textboxTB.Location.X, textboxTB.Location.Y + i * (this.mappingBT.Height + 20));
                    if (!addParams && Map[i].StaticValue != null)
                        tb.Text = Map[i].StaticValue.ToString();
                    if (!addParams && Map[i].EventValueMap != null)
                        tb.Text = Map[i].EventValueMap;
                    tb.Validated += new EventHandler(textboxTB_Validated);
                    this.controlPanel.Controls.Add(tb);
                    controls.Add(tb);
                }

                Button qbt = new Button();
                qbt.Size = queryBT.Size;
                qbt.Anchor = queryBT.Anchor;
                qbt.Location = new Point(queryBT.Location.X, queryBT.Location.Y + i * (this.mappingBT.Height + 20));
                qbt.Click += new EventHandler(queryBT_Click);
                qbt.Text = queryBT.Text;
                controls.Add(qbt);
                this.controlPanel.Controls.Add(qbt);

                Button mbt = new Button();
                mbt.Size = mappingBT.Size;
                mbt.Anchor = mappingBT.Anchor;
                mbt.Location = new Point(mappingBT.Location.X, mappingBT.Location.Y + i * (this.mappingBT.Height + 20));
                mbt.Click += new EventHandler(mappingBT_Click);
                mbt.Text = mappingBT.Text;
                controls.Add(mbt);
                this.controlPanel.Controls.Add(mbt);
            }
        }

        private void closeBT_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void queryBT_Click(object sender, EventArgs e)
        {
            SelectQuery query = new SelectQuery(stages, stageNr, ceid);
            query.ShowDialog();
            if (query.coControl.LastQueryAddedID > 0)
            {
                int paramNr = -1;
                List<Control> zz = controls.Where(x => x is Button && x.Text.Contains("query")).ToList();
                paramNr = zz.IndexOf((sender as Button));
                Map[paramNr].ConditionaQuery = query.coControl.LastQueryAddedID;
                Map.AddRange(query.coControl.ActionDict.Last().Value.Item2);
            }

            query.Dispose();
        }

        private void mappingBT_Click(object sender, EventArgs e)
        {
            this.SendToBack();
            this.controlPanel.SendToBack();
            this.mappingPanel.BringToFront();
            this.valueMappingTV.Nodes.Clear();
            this.valueMappingTV.Nodes.Add(loadComplexTV(stages, stageNr));
            this.valueMappingTV.ExpandAll();
            lastMappingIndex = this.controls.IndexOf(sender as Control);
        }

        private void textboxTB_Validated(object sender, EventArgs e)
        {
            int paramNr = -1;

                List<Control> zz = controls.Where(x => x is TextBoxBase).ToList();
                paramNr = zz.IndexOf((sender as TextBoxBase));

            if ((sender as TextBoxBase).Text.Contains("##"))
            {
                Regex valMapCheck = new Regex("##(CE[0-9]+\\/)?STAGE[0-9]+(\\/((MS[0-9]+\\/[0-9]+(\\/CE[0-9]+\\/STAGE[0-9])?)|ES[0-9]+|(CE[0-9]+\\/STAGE[0-9]+)))*\\/AE[0-9]+\\/VALUE\\([0-9a-zA-Z]+\\)##");
                string zw = (sender as TextBoxBase).Text;
                MatchCollection matches = valMapCheck.Matches(zw);
                foreach(Match mat in matches)
                {
                    if(!mat.Value.StartsWith("##CE")) //not!
                    {
                        zw = zw.Remove(mat.Index, mat.Length);
                        zw = zw.Insert(mat.Index, mat.Value.Replace(mat.Value, "##CE" + ceid.ToString() + "/" + mat.Value.Substring(2)));
                    }
                }
                if (false) //TODO
                {
                    MessageBox.Show("this Value-Map has an invalid syntax!\nExample: ##STAGE8/ES77/MS6/5/CE21/STAGE5/ES101/AE32/VALUE(2)##\n always start with: ##STAGE\\d, (or ##CE\\d/STAGE\\d) end with /AE\\d/VALUE(\\d)##");
                    return;
                }
                Map[paramNr].EventValueMap = zw;
                return;
            }

            string type = action.ParamTypes[paramNr].Trim();
            if(!type.StartsWith("System."))
                type = "System." + type;
            object statVal = null;
            try
            {
                statVal = Convert.ChangeType((sender as TextBoxBase).Text, Type.GetType(type));
                Map[paramNr].StaticValue = statVal;
            }
            catch (FormatException)
            {
                (sender as TextBoxBase).Text = "";
                MessageBox.Show("this value can't be cast as " + type);
            }
        }

        private void closeMappingPanelBT_Click(object sender, EventArgs e)
        {
            this.mappingPanel.SendToBack();
            this.controlPanel.BringToFront();
        }

        private void valueMappingTV_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text.StartsWith("Value") && e.Node.Text.Length>7)
            {
                string warningMsg = "";
                string valueMap = "/VALUE(" + e.Node.Text.Substring(e.Node.Text.IndexOf(' ')+1) + ")##";
                TreeNode currentNode = e.Node;
                while (currentNode.Parent != null)
                {
                    currentNode = currentNode.Parent;
                    if (currentNode.Text.StartsWith("Complex"))
                    {
                        valueMap = "CE" + currentNode.Tag.ToString() + valueMap;
                        if (currentNode.Parent == null)
                            valueMap = "##" + valueMap;
                        else
                            valueMap = "/" + valueMap;
                    }
                    else if (currentNode.Text.StartsWith("Atomic"))
                        valueMap = "/AE" + currentNode.Tag.ToString() + valueMap;
                    else if (currentNode.Text.StartsWith("Multi"))
                        valueMap = "/MS" + currentNode.Tag.ToString() + "/" + e.Node.Text.Substring(0, e.Node.Text.IndexOf(' ')) + valueMap;
                    else if (currentNode.Text.StartsWith("STAGE"))
                    {
                        valueMap = "/STAGE" + currentNode.Tag.ToString() + valueMap;
                        warningMsg = getWarningMsg(currentNode) ?? warningMsg;
                    }
                    else if (currentNode.Text.StartsWith("Event"))
                    {
                        valueMap = "/ES" + currentNode.Tag.ToString() + valueMap;
                        warningMsg = getWarningMsg(currentNode) ?? warningMsg;
                    }
                }
                (controls[this.lastMappingIndex-2] as TextBoxBase).Text = valueMap;
                if (warningMsg.Length > 0)
                    MessageBox.Show(warningMsg);
                this.mappingPanel.SendToBack();
                this.BringToFront();
                this.controlPanel.BringToFront(); 
            }
        }

        private string getWarningMsg(TreeNode node)
        {
            string warningMsg = "The chosen value might not be available, since it \nis under one of the following operators: {NOT,OR,XOR}. \nThis ";
            if (action is ConditionQuery)
                warningMsg += "condition is threfore optional.";
            else
                warningMsg += "action is threfore optional.";
            if(node.Text.ToUpper().EndsWith("NOT"))
                return warningMsg;
            if(node.Text.ToUpper().EndsWith("OR") && node.Nodes.Count > 1)
                return warningMsg;
            return null;
        }
    }
}
