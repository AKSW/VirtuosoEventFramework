using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EventFrameworkControl
{
    public partial class NewAccount : Form
    {
        public string username = "";
        public string password = "";

        public NewAccount()
        {
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            InitializeComponent();
                    }

        private void okBT_Click(object sender, EventArgs e)
        {
            if (userTB.Text.Count() > 5 && passTB.Text.Count() > 5)
            {
                username = userTB.Text;
                password = passTB.Text;
                this.Hide();
            }
            else
                MessageBox.Show("Please enter username und password with at least 6 letters");
        }
    }
}
