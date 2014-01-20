namespace EventFrameworkControl
{
    partial class ActionsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.actionsDGV = new System.Windows.Forms.DataGridView();
            this.insertActionBT = new System.Windows.Forms.Button();
            this.testActionBT = new System.Windows.Forms.Button();
            this.changeActionBT = new System.Windows.Forms.Button();
            this.refreshBT = new System.Windows.Forms.Button();
            this.deleteActionBT = new System.Windows.Forms.Button();
            this.titleLA = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.actionsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // actionsDGV
            // 
            this.actionsDGV.AllowUserToAddRows = false;
            this.actionsDGV.AllowUserToDeleteRows = false;
            this.actionsDGV.AllowUserToResizeRows = false;
            this.actionsDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actionsDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.actionsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.actionsDGV.Location = new System.Drawing.Point(-1, 29);
            this.actionsDGV.Name = "actionsDGV";
            this.actionsDGV.RowHeadersVisible = false;
            this.actionsDGV.Size = new System.Drawing.Size(1116, 415);
            this.actionsDGV.TabIndex = 0;
            this.actionsDGV.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.actionsDGV_CellMouseClick);
            // 
            // insertActionBT
            // 
            this.insertActionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.insertActionBT.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.insertActionBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.insertActionBT.Location = new System.Drawing.Point(912, 535);
            this.insertActionBT.Name = "insertActionBT";
            this.insertActionBT.Size = new System.Drawing.Size(198, 28);
            this.insertActionBT.TabIndex = 13;
            this.insertActionBT.Text = "create new action";
            this.insertActionBT.UseVisualStyleBackColor = false;
            this.insertActionBT.Click += new System.EventHandler(this.insertActionBT_Click);
            // 
            // testActionBT
            // 
            this.testActionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.testActionBT.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.testActionBT.Location = new System.Drawing.Point(708, 501);
            this.testActionBT.Name = "testActionBT";
            this.testActionBT.Size = new System.Drawing.Size(198, 28);
            this.testActionBT.TabIndex = 15;
            this.testActionBT.Text = "test action";
            this.testActionBT.UseVisualStyleBackColor = false;
            this.testActionBT.Click += new System.EventHandler(this.testActionBT_Click);
            // 
            // changeActionBT
            // 
            this.changeActionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.changeActionBT.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.changeActionBT.Location = new System.Drawing.Point(912, 501);
            this.changeActionBT.Name = "changeActionBT";
            this.changeActionBT.Size = new System.Drawing.Size(198, 28);
            this.changeActionBT.TabIndex = 16;
            this.changeActionBT.Text = "edit this action";
            this.changeActionBT.UseVisualStyleBackColor = false;
            this.changeActionBT.Click += new System.EventHandler(this.changeActionBT_Click);
            // 
            // refreshBT
            // 
            this.refreshBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshBT.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.refreshBT.Location = new System.Drawing.Point(912, 4);
            this.refreshBT.Name = "refreshBT";
            this.refreshBT.Size = new System.Drawing.Size(198, 20);
            this.refreshBT.TabIndex = 17;
            this.refreshBT.Text = "refresh";
            this.refreshBT.UseVisualStyleBackColor = false;
            this.refreshBT.Click += new System.EventHandler(this.refreshBT_Click);
            // 
            // deleteActionBT
            // 
            this.deleteActionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteActionBT.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.deleteActionBT.Location = new System.Drawing.Point(912, 450);
            this.deleteActionBT.Name = "deleteActionBT";
            this.deleteActionBT.Size = new System.Drawing.Size(198, 28);
            this.deleteActionBT.TabIndex = 51;
            this.deleteActionBT.Text = "delete this action";
            this.deleteActionBT.UseVisualStyleBackColor = false;
            this.deleteActionBT.Visible = false;
            this.deleteActionBT.Click += new System.EventHandler(this.deleteActionBT_Click);
            // 
            // titleLA
            // 
            this.titleLA.AutoSize = true;
            this.titleLA.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLA.Location = new System.Drawing.Point(4, 4);
            this.titleLA.Name = "titleLA";
            this.titleLA.Size = new System.Drawing.Size(67, 20);
            this.titleLA.TabIndex = 52;
            this.titleLA.Text = "actions";
            // 
            // ActionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.titleLA);
            this.Controls.Add(this.deleteActionBT);
            this.Controls.Add(this.refreshBT);
            this.Controls.Add(this.changeActionBT);
            this.Controls.Add(this.testActionBT);
            this.Controls.Add(this.insertActionBT);
            this.Controls.Add(this.actionsDGV);
            this.Name = "ActionsControl";
            this.Size = new System.Drawing.Size(1114, 566);
            ((System.ComponentModel.ISupportInitialize)(this.actionsDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView actionsDGV;
        private System.Windows.Forms.Button insertActionBT;
        private System.Windows.Forms.Button testActionBT;
        private System.Windows.Forms.Button changeActionBT;
        private System.Windows.Forms.Button refreshBT;
        private System.Windows.Forms.Button deleteActionBT;
        private System.Windows.Forms.Label titleLA;

    }
}
