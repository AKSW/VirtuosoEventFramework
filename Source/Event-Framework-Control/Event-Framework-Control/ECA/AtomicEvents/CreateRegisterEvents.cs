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
    public partial class CreateRegisterEvents : Form
    {
        private  int database = 0;
        private string endpointAddress = "";
        private string[] columnList = null;

        public CreateRegisterEvents(DataRow dbRow)
        {
            InitializeComponent();
            this.database = (int)dbRow.ItemArray[0];
            this.endpointAddress = dbRow.ItemArray[3].ToString();
            this.chooseTableCB.Items.Clear();

                string[] zw = StaticHelper.ClientProxy.GetSchemaTables(database);
                foreach (string str in zw)
                    if (!str.Contains("SYS_") && !str.Contains("RDF_") && !str.Contains("ADMIN_") && !str.Contains("EventFramework"))
                        chooseTableCB.Items.Add(str);

        }

        private void executeQuerryBT_Click(object sender, EventArgs e)
        {
            string condition = conditionRTB.Text.Trim();
            if (condition.Length >8 && condition.Substring(0, 6).ToLower() != "where ")
                condition = "WHERE " + condition;

            condition = condition.Replace("'", "\'");

            object zw = null;
            if (chooseTableCB.SelectedItem != null)
            {
                if (condition.Length == 0 || condition.Length > 8)
                {
                    condition = condition.Replace("\"", "");
                    foreach (string col in columnList)
                        if (!string.IsNullOrEmpty(col) && condition.ToLower().Contains(col.ToLower())) //not!
                        {
                            int index = 0;
                            while (condition.ToLower().Substring(index).IndexOf(col.ToLower()) >= 0)
                            {
                                index += condition.ToLower().Substring(index).IndexOf(col.ToLower());
                                if (index > 0 && !char.IsLetter(condition[index - 1]) && !char.IsLetter(condition[index + col.Length]))   //!not
                                {
                                    condition = condition.Insert(index, "\"");
                                    condition = condition.Insert(index + col.Length + 1, "\"");
                                    index = index + 2;
                                }
                                index = index + col.Length;
                            }
                        }
                    try
                    {
                        Regex noBr = new Regex("\\s?\\([^\\)]*\\)");
                        
                        zw = StaticHelper.ClientProxy.ExecuteTestSqlQuery("SELECT TOP 100 " + noBr.Replace(chooseColumnsTB.Text, "") + " FROM \"" + chooseTableCB.SelectedItem.ToString() + "\" " + condition, database);

                    }
                    catch (Exception ex)
                    {
                            MessageBox.Show("an exception occured: " + ex.Message);
                    }
                }
            }
            else
                MessageBox.Show("please select a table");

            List<List<string>> lala = new List<List<string>>();
            DataTable sourceDT = new DataTable();

            try
            {
                if (zw != DBNull.Value && zw != DBNull.Value)
                {
                    for (int j = 0; j < ((zw as object[][])[0] as string[]).Count(); j++)
                        sourceDT.Columns.Add(((zw as object[][])[0] as string[])[j]);

                    for (int i = 1; i < (zw as object[][]).Count(); i++)
                    {
                        sourceDT.Rows.Add(((zw as object[][])[i] as string[]));
                    }
                    resultDGV.DataSource = sourceDT;
                }
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("please check the syntax of your query");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("please check the syntax of your query");
            }
        }

        private void confirmTriggerBT_Click(object sender, EventArgs e)
        {
            if (this.triggerTypeCB.SelectedItem == null)
            {
                MessageBox.Show("please select the trigger-type");
                return;
            }
            if (this.chooseTableCB.SelectedItem == null)
            {
                MessageBox.Show("please select a data-table");
                return;
            }
            if (conditionRTB.Text.Trim().Length < 9)
            {
                MessageBox.Show("please enter a condition");
                return;
            }

            if (conditionRTB.Text.Length > 6)
            {

                string condition = conditionRTB.Text.Trim().Substring(6);

                for (int i =0; i< columnList.Count(); i++)
                    if (!chooseColumnsTB.Text.Contains(columnList[i])) //not!
                        columnList[i] = null;

                try{
                    string returnCols ="";
                    foreach(string str in columnList)
                        returnCols += ", " + str;
                    returnCols = returnCols.Substring(2);

                    AtomicEvent trigger = new AtomicEvent(StaticHelper.CurrentUser, descriptionRTB.Text, chooseTableCB.SelectedItem.ToString(), "",database, triggerTypeCB.SelectedItem.ToString(), returnCols, condition);
                    MessageBox.Show(StaticHelper.ClientProxy.SetNewSqlTrigger(trigger));  
                }
                catch (Exception ex)
                {
                    MessageBox.Show("an exception occured: " + ex.Message);
                }
                this.Close();
            }

        }

        private void chooseTableCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chooseTableCB.SelectedItem != null)
            {
                try{
                    this.columnList = StaticHelper.ClientProxy.GetColumnsOfRemoteTable(database, chooseTableCB.SelectedItem.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("an exception occured: " + ex.Message);
                }
                string outPut = "";
                if (columnList != null && columnList.Count() >0)
                {
                    foreach (string col in columnList)
                        outPut += ", \"" + col + "\"";
                    chooseColumnsTB.Text = outPut.Substring(2);
                }
            }
        }
    }
}
