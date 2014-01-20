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
    public partial class RegisterAtomicEvent : Form
    {
        private int dsInstance;
        public RegisterAtomicEvent(int dsInstance)
        {
            InitializeComponent();
            this.dsInstance = dsInstance;
        }

        private void registerBT_Click(object sender, EventArgs e)
        {
            if (altNameTB.Text.Length > 0 && descriptionRTB.Text.Length > 0)
            {
                string retunValues = returnValuesTB.Text.Trim().Replace(";",",");
                string[] retunValuesArray = retunValues.Split(',');
                AtomicEvent ev = new AtomicEvent(StaticHelper.CurrentUser, this.descriptionRTB.Text, "", this.altNameTB.Text.Trim(), dsInstance, "EXTERN", retunValues);
                string zw = StaticHelper.ClientProxy.RegisterTrigger(ev);

                if (zw.Contains("registered"))
                {
                    this.messageRTB.Enabled = true;
                    this.messageRTB.Text = Properties.Resources.newEventSoapRequest.Replace("##endpoint##", StaticHelper.CentralDbEndpoint);
                    Regex dsI = new Regex("(?<=<dsInstance>)[^<]*(?=<\\/dsInstance>)");
                    Regex name = new Regex("(?<=<name>)[^<]*(?=<\\/name>)");
                    this.messageRTB.Text = dsI.Replace(this.messageRTB.Text, dsInstance.ToString());
                    this.messageRTB.Text = name.Replace(this.messageRTB.Text, zw.Substring(zw.LastIndexOf(' ') + 1));

                    if (returnValuesTB.Text.Trim().Length > 0)
                    {
                        Regex rowVector = new Regex("(?<=<rowVector>).*?(?=<\\/rowVector>)", RegexOptions.Singleline);
                        string rowReplace = "\r\n";
                        foreach (string val in retunValuesArray)
                        {
                            rowReplace += "<variant>enter a value for " + val + "</variant>\r\n";
                        }
                        this.messageRTB.Text = rowVector.Replace(this.messageRTB.Text, rowReplace);
                    }
                    this.messageRTB.Text = StaticHelper.BeautifyXml(this.messageRTB.Text);

                    MessageBox.Show("A SOAP-message template has been created. \nCopy this template and use it to send event-messages to this framework. \nMake changes to this template only at the annotated places.");
                }
                else
                    MessageBox.Show(zw);
            }
            else
                MessageBox.Show("please select a datasource and enter a description");
        }
    }
}
