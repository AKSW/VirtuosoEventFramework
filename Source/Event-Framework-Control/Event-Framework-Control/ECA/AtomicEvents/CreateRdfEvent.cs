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
    public partial class CreateRdfEvent : Form
    {
        private int fromGraphs = 0;
        private int dataSource = 0;

        public CreateRdfEvent(int dataSourceInstance)
        {
            InitializeComponent();
            view2Panel.Visible = false;
            this.dataSource = dataSourceInstance;

            DataTable source = new DataTable();
            source.Columns.Add("selectGraphCol", typeof(bool));
            source.Columns.Add("prefixCol", typeof(string));
            source.Columns.Add("graphUriCol", typeof(string));
            source.Columns.Add("separatorCol", typeof(string));
            graphDGV.AutoGenerateColumns = false;
            graphDGV.Columns.Add(new DataGridViewCheckBoxColumn());
            graphDGV.Columns.Add(new DataGridViewTextBoxColumn());
            graphDGV.Columns.Add(new DataGridViewTextBoxColumn());
            graphDGV.Columns.Add(new DataGridViewTextBoxColumn());
            graphDGV.Columns[0].HeaderText = "Select Graphs to query";
            graphDGV.Columns[0].DataPropertyName = "selectGraphCol";
            graphDGV.Columns[1].HeaderText = "Enter prefix abrivation";
            graphDGV.Columns[1].DataPropertyName = "prefixCol";
            graphDGV.Columns[2].HeaderText = "Graph uri";
            graphDGV.Columns[2].DataPropertyName = "graphUriCol";
            graphDGV.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            graphDGV.Columns[3].HeaderText = "separator";
            graphDGV.Columns[3].DataPropertyName = "separatorCol";
            
            string[] zw = StaticHelper.ClientProxy.GetGraphs(dataSourceInstance);
            foreach (string uri in zw)
                source.Rows.Add(new object[] { false, "", uri.Substring(0, uri.Length - 1) + uri.Substring(uri.Length - 1).Replace("/", ""), "/" });
            this.graphDGV.DataSource = source;

            this.graphDGV.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.graphDGV_CellValueChanged);
        }

        private void graphSelectBT_Click(object sender, EventArgs e)
        {
            view1Panel.Visible = false;
            view2Panel.Visible = true;
        }

        private void graphBackBT_Click(object sender, EventArgs e)
        {
            view2Panel.Visible = false;
            view1Panel.Visible = true;
        }

        private void graphDGV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if ((bool)((graphDGV.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell).Value) == true)
                {
                    if (fromGraphs == 0)
                        graphDGV.Rows[e.RowIndex].Cells[1].Value = ":";
                    else
                        graphDGV.Rows[e.RowIndex].Cells[1].Value = ((char)((int)('a')-1 + fromGraphs)).ToString() + ":";
                    fromGraphs++;
                }
            }
        }

        private void graphConfirmBT_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.graphDGV.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    if (((row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "True") || row.Cells[1].Value.ToString().Length > 0) && (row.Cells[2].Value == null || row.Cells[2].Value.ToString().Length < 6))
                    {
                        MessageBox.Show("you have to enter a graph-name at row: " + row.Index.ToString());
                        return;
                    }
                    if (row.Cells[1].Value.ToString().Length > 0 && !row.Cells[1].Value.ToString().EndsWith(":")) //!not
                    {
                        MessageBox.Show("a prefix has to end with ':'");
                        return;
                    }
                    if (row.Cells[3].Value == null || row.Cells[3].Value.ToString().Length == 0)
                    {
                        MessageBox.Show("please enter separator (at row " + row.Index.ToString() + ") !");
                        return;
                    }
                    else if (row.Cells[3].Value.ToString().Length > 1 || Char.IsLetterOrDigit(row.Cells[3].Value.ToString()[0]))
                    {
                        MessageBox.Show("separator-length is not 1 or separator is a letter \nor separator is a digit (at row " + row.Index.ToString() + ") !");
                        return;
                    }
                }
            }
            view2Panel.Visible = false;
            view1Panel.Visible = true;
        }

        private string generateSyntax(bool ask)
        {
            string graphs = "} FILTER(?graph IN (";
            string syntax = "sparql ";
            string froms = "";

            foreach (DataGridViewRow row in graphDGV.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    if (row.Cells[1].Value.ToString().Length > 0)
                        syntax += "PREFIX " + row.Cells[1].Value.ToString() + " <" + row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString() + ">\n";
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == "True")
                    {
                        froms += "FROM <" + row.Cells[2].Value.ToString() + ">\n";
                        graphs += "<" + row.Cells[2].Value.ToString() + ">, ";
                    } 
                }
            }

            if(ask)
                syntax += "ASK ";
            else
                syntax += "SELECT ?graph ?subj ?pred ?obj \n";

            syntax += froms;

            if (graphs.Contains(','))
            {
                graphs = graphs.Remove(graphs.LastIndexOf(','));
                graphs = graphs + "))";
            }
            else
                graphs = "";

            if (conditionRTB.Text.Trim().Length > 10 && conditionRTB.Text.Trim().Substring(0, 5).ToLower() == "where" && conditionRTB.Text.Trim().ToLower().Replace("  ", " ").Contains("?subj ?pred ?obj"))
            {
                try
                {
                    string condition = conditionRTB.Text.Trim();
                    if(graphs.Length > 0)
                        condition = condition.Insert(condition.IndexOf('{') + 1, "graph ?graph{");

                    syntax += condition;

                    syntax = syntax.Insert(syntax.LastIndexOf('}'), graphs);

                    if (!ask)
                        syntax += "\nLIMIT 1000";
                }
                catch (Exception)
                {
                    MessageBox.Show("please revise your syntax!");
                }
            }
            else
            {
                MessageBox.Show("please enter a valid syntax, starting with 'WHERE{'.\nalso: the triple ?subj ?pred ?obj is obligatory \ncomplexer problems have to be modeled with the complex event feature ");
                return null;
            }

            return syntax;
        }

        private void executeQuerryBT_Click(object sender, EventArgs e)
        {
            string syntax = generateSyntax(false);
            object zw = null;
            try
            {
                if(syntax != null)
                    zw = StaticHelper.ClientProxy.ExecuteTestSqlQuery(syntax, StaticHelper.DataSource);
            }
            catch (Exception ex)
            {
                MessageBox.Show("an exception occured: " + ex.Message);
            }

            List<List<string>> lala = new List<List<string>>();
            DataTable sourceDT = new DataTable();

            if (zw != null && zw != DBNull.Value)
            {
                for (int j = 0; j < ((zw as object[][])[0] as string[]).Count(); j++)
                    sourceDT.Columns.Add(((zw as object[][])[0] as string[])[j]);

                for (int i = 1; i < (zw as object[][]).Count(); i++)
                {
                    sourceDT.Rows.Add(((zw as object[][])[i] as string[]));
                }
                resultDGV.DataSource = sourceDT;
            }
            else
                MessageBox.Show("this query has a faulty syntax!");
        }

        private void confirmTriggerBT_Click(object sender, EventArgs e)
        {
            string syntax = generateSyntax(true);

            if (this.triggerTypeCB.SelectedItem == null)
            {
                MessageBox.Show("please select the trigger-type");
                return;
            }
            if (conditionRTB.Text.Trim().Length < 9)
            {
                MessageBox.Show("please enter a condition");
                return;
            }
            try
            {
                if (syntax != null)
                {
                    AtomicEvent ev = new AtomicEvent(StaticHelper.CurrentUser, descriptionRTB.Text, "TripleSore", "", dataSource, triggerTypeCB.SelectedItem.ToString(), "subject, predicate, object", syntax);
                    MessageBox.Show(StaticHelper.ClientProxy.SetNewRdfTrigger(ev));    //not sure to provide the triggers off option with rdf events!?
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("an exception occured: " + ex.Message);
            }
        }
    }
}
