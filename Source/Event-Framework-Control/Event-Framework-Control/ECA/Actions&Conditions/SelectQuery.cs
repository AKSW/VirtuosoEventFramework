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
    public partial class SelectQuery : Form
    {
        public ConditionsQueryControl coControl { get; private set; }

        public SelectQuery(List<InitialStage> stages, int stageNr, int ceid)
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(SelectQuery_FormClosing);

            coControl = new ConditionsQueryControl(stages, stageNr, EventOntology.Activity.Query, ceid);
            coControl.Size = this.queryPanel.ClientSize;
            coControl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.queryPanel.Controls.Add(coControl);

            coControl.VisibleChanged += new EventHandler(coControl_VisibleChanged);
        }

        void coControl_VisibleChanged(object sender, EventArgs e)
        {
            if(!coControl.Visible)
                this.Hide();
        }

        void SelectQuery_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
