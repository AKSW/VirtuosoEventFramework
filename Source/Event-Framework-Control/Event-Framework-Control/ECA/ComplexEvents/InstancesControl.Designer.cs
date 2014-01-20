namespace EventFrameworkControl
{
    partial class InstancesControl
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
            this.instancesDGV = new System.Windows.Forms.DataGridView();
            this.refreshBT = new System.Windows.Forms.Button();
            this.resetBT = new System.Windows.Forms.Button();
            this.abortBT = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.filterCeidTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.filterCeidBT = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.instancesDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // instancesDGV
            // 
            this.instancesDGV.AllowUserToAddRows = false;
            this.instancesDGV.AllowUserToDeleteRows = false;
            this.instancesDGV.AllowUserToResizeRows = false;
            this.instancesDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.instancesDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.instancesDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.instancesDGV.Location = new System.Drawing.Point(187, 36);
            this.instancesDGV.Name = "instancesDGV";
            this.instancesDGV.RowHeadersVisible = false;
            this.instancesDGV.Size = new System.Drawing.Size(772, 498);
            this.instancesDGV.TabIndex = 0;
            this.instancesDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.instancesDGV_CellClick);
            // 
            // refreshBT
            // 
            this.refreshBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshBT.Location = new System.Drawing.Point(782, 3);
            this.refreshBT.Name = "refreshBT";
            this.refreshBT.Size = new System.Drawing.Size(177, 27);
            this.refreshBT.TabIndex = 1;
            this.refreshBT.Text = "refresh";
            this.refreshBT.UseVisualStyleBackColor = true;
            this.refreshBT.Click += new System.EventHandler(this.refreshBT_Click);
            // 
            // resetBT
            // 
            this.resetBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetBT.Enabled = false;
            this.resetBT.Location = new System.Drawing.Point(4, 252);
            this.resetBT.Name = "resetBT";
            this.resetBT.Size = new System.Drawing.Size(177, 27);
            this.resetBT.TabIndex = 2;
            this.resetBT.Text = "reset to first stage";
            this.resetBT.UseVisualStyleBackColor = true;
            this.resetBT.Click += new System.EventHandler(this.resetBT_Click);
            // 
            // abortBT
            // 
            this.abortBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.abortBT.Enabled = false;
            this.abortBT.Location = new System.Drawing.Point(4, 189);
            this.abortBT.Name = "abortBT";
            this.abortBT.Size = new System.Drawing.Size(177, 27);
            this.abortBT.TabIndex = 3;
            this.abortBT.Text = "abort instance";
            this.abortBT.UseVisualStyleBackColor = true;
            this.abortBT.Click += new System.EventHandler(this.abortBT_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(374, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "currently running instances of complex events";
            // 
            // filterCeidTB
            // 
            this.filterCeidTB.Location = new System.Drawing.Point(4, 62);
            this.filterCeidTB.Name = "filterCeidTB";
            this.filterCeidTB.Size = new System.Drawing.Size(108, 20);
            this.filterCeidTB.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "filter by CEID:";
            // 
            // filterCeidBT
            // 
            this.filterCeidBT.Location = new System.Drawing.Point(119, 62);
            this.filterCeidBT.Name = "filterCeidBT";
            this.filterCeidBT.Size = new System.Drawing.Size(62, 20);
            this.filterCeidBT.TabIndex = 7;
            this.filterCeidBT.Text = "filter";
            this.filterCeidBT.UseVisualStyleBackColor = true;
            this.filterCeidBT.Click += new System.EventHandler(this.filterCeidBT_Click);
            // 
            // InstancesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.filterCeidBT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filterCeidTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.abortBT);
            this.Controls.Add(this.resetBT);
            this.Controls.Add(this.refreshBT);
            this.Controls.Add(this.instancesDGV);
            this.Name = "InstancesControl";
            this.Size = new System.Drawing.Size(962, 534);
            ((System.ComponentModel.ISupportInitialize)(this.instancesDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView instancesDGV;
        private System.Windows.Forms.Button refreshBT;
        private System.Windows.Forms.Button resetBT;
        private System.Windows.Forms.Button abortBT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox filterCeidTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button filterCeidBT;
    }
}
