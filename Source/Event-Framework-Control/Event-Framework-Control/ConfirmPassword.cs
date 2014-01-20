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
    public partial class ConfirmPassword : Form
    {
        public string adminPassword = "";
        public string newPassword = "";
        private bool showNewPasswordBox = false;

        public ConfirmPassword(bool showNewPasswordBox)
        {
            this.showNewPasswordBox = showNewPasswordBox;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            InitializeComponent();

            if (!showNewPasswordBox)
            {
                newPassTB.Visible = false;
                newPassLA.Visible = false;
            }
        }

        private void passBT_Click(object sender, EventArgs e)
        {
            if (showNewPasswordBox && !string.IsNullOrEmpty(newPassTB.Text)) //!not
            {
                newPassword = newPassTB.Text;
            }
            adminPassword = passTB.Text;
            this.Hide();
        }

        private void passTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                passBT_Click(new object(), new EventArgs());
        }
    }
}
