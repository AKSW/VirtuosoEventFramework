using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EventOntology;

namespace EventFrameworkControl
{
    public partial class TestActionCondition : Form
    {
        private List<Control> controls = new List<Control>();
        private DataGridViewRow row;
        private EventAction action;
        private bool sparqlAction = false;

        public TestActionCondition(DataGridViewRow row)
        {
            InitializeComponent();
            int rtbCount = 0;
            action = new EventAction(row);

            if (!string.IsNullOrEmpty(action.SparqlQuery))
                sparqlAction = true;

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
                    controls.Add(labelLA);
                    controls.Add(richTextBoxRTB);
                }
                else
                {
                    controls.Add(labelLA);
                    controls.Add(textboxTB);
                }
            }

            if (action.ParamDescription != null && action.ParamDescription.Count > 1)
            {
                for (int i = 1; i < action.ParamDescription.Count(); i++)
                {
                    Label la = new Label();
                    la.Size = new System.Drawing.Size(this.textboxTB.Width, labelLA.Height);
                    la.Location = new Point(labelLA.Location.X, labelLA.Location.Y + i * 40 + rtbCount * 30);
                    la.Font = (labelLA.Font.Clone() as Font);
                    this.Controls.Add(la);
                    controls.Add(la);
                    string mama = "parameter " + (i + 1).ToString() + ": " + action.ParamDescription[i] + " (Type: " + action.ParamTypes[i] + ")";
                    la.Text = mama;

                    if (action.ParamTypes[i].Contains("String"))
                    {
                        RichTextBox rtb = new RichTextBox();
                        rtb.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                        rtb.Size = richTextBoxRTB.Size;
                        rtb.Location = new Point(textboxTB.Location.X, textboxTB.Location.Y + i * 40 + rtbCount * 30);
                        this.Controls.Add(rtb);
                        controls.Add(rtb);
                        rtbCount++;
                    }
                    else
                    {
                        TextBox tb = new TextBox();
                        tb.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                        tb.Size = textboxTB.Size;
                        tb.Location = new Point(textboxTB.Location.X, textboxTB.Location.Y + i * 40 + rtbCount * 30);
                        this.Controls.Add(tb);
                        controls.Add(tb);
                    }
                }
            }

        }

        private void confirmBT_Click(object sender, EventArgs e)
        {
            string[] paramArray = new string[0];
            if (action.ParamTypes != null && action.ParamTypes.Count > 0)
            {
                paramArray = new string[action.ParamTypes.Count()];
                List<TextBoxBase> boxes = this.controls.Where(x => x is TextBoxBase).Cast<TextBoxBase>().ToList();

                for (int i = 0; i < boxes.Count(); i++)
                    if (boxes[i] is TextBox)
                        if (string.IsNullOrEmpty((boxes[i] as TextBox).Text))
                            paramArray[i] = null;
                        else
                            paramArray[i] = boxes[i].Text.Trim();
                    else
                        paramArray[i] = boxes[i].Text.Trim();
            }

            if (sparqlAction)
            {
                DataTable retObj = null;
                try
                {
                    retObj = action.InvokeSparqlQuery(paramArray);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("an error occured: " + ex.Message);
                    return;
                }
                if (retObj != null && retObj.Rows.Count > 0)
                    MessageBox.Show(retObj.Rows[0][0].ToString());
            }
            else
            {
                object retObj = null;
                //try
                //{
                    retObj = action.InvokeRemoteMethode(paramArray);
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("an error occured: " + ex.Message);
                //    return;
                //}
                    if (retObj != null && (retObj is string[][]) && (retObj as string[][]).Count() > 1)
                    MessageBox.Show((retObj as string[][])[1][0].ToString());
                    else
                        MessageBox.Show(retObj.ToString());
            }
            this.Close();
        }
    }
}
