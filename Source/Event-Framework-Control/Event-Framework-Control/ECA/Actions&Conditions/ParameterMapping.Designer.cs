namespace EventFrameworkControl
{
    partial class ParameterMapping
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.closeBT = new System.Windows.Forms.Button();
            this.textboxTB = new System.Windows.Forms.TextBox();
            this.labelLA = new System.Windows.Forms.Label();
            this.queryBT = new System.Windows.Forms.Button();
            this.mappingBT = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.richTextBoxRTB = new System.Windows.Forms.RichTextBox();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.mappingPanel = new System.Windows.Forms.Panel();
            this.valueMappingTV = new System.Windows.Forms.TreeView();
            this.closeMappingPanelBT = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.controlPanel.SuspendLayout();
            this.mappingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(373, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "you can map parameters to different sources:";
            // 
            // closeBT
            // 
            this.closeBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBT.Location = new System.Drawing.Point(584, 588);
            this.closeBT.Name = "closeBT";
            this.closeBT.Size = new System.Drawing.Size(125, 36);
            this.closeBT.TabIndex = 2;
            this.closeBT.Text = "save and close";
            this.closeBT.UseVisualStyleBackColor = true;
            this.closeBT.Click += new System.EventHandler(this.closeBT_Click);
            // 
            // textboxTB
            // 
            this.textboxTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxTB.Location = new System.Drawing.Point(3, 60);
            this.textboxTB.Name = "textboxTB";
            this.textboxTB.Size = new System.Drawing.Size(488, 20);
            this.textboxTB.TabIndex = 4;
            this.textboxTB.Validated += new System.EventHandler(this.textboxTB_Validated);
            // 
            // labelLA
            // 
            this.labelLA.AutoSize = true;
            this.labelLA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLA.Location = new System.Drawing.Point(0, 42);
            this.labelLA.Name = "labelLA";
            this.labelLA.Size = new System.Drawing.Size(41, 15);
            this.labelLA.TabIndex = 3;
            this.labelLA.Text = "label1";
            // 
            // queryBT
            // 
            this.queryBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.queryBT.Location = new System.Drawing.Point(497, 60);
            this.queryBT.Name = "queryBT";
            this.queryBT.Size = new System.Drawing.Size(82, 50);
            this.queryBT.TabIndex = 5;
            this.queryBT.Text = "query";
            this.queryBT.UseVisualStyleBackColor = true;
            this.queryBT.Click += new System.EventHandler(this.queryBT_Click);
            // 
            // mappingBT
            // 
            this.mappingBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mappingBT.Location = new System.Drawing.Point(585, 60);
            this.mappingBT.Name = "mappingBT";
            this.mappingBT.Size = new System.Drawing.Size(82, 50);
            this.mappingBT.TabIndex = 6;
            this.mappingBT.Text = "mapping";
            this.mappingBT.UseVisualStyleBackColor = true;
            this.mappingBT.Click += new System.EventHandler(this.mappingBT_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "3 options: a) enter a static value";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(485, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "b) design a query";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(574, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "c) map value to event";
            // 
            // richTextBoxRTB
            // 
            this.richTextBoxRTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxRTB.Location = new System.Drawing.Point(3, 60);
            this.richTextBoxRTB.Name = "richTextBoxRTB";
            this.richTextBoxRTB.Size = new System.Drawing.Size(488, 50);
            this.richTextBoxRTB.TabIndex = 10;
            this.richTextBoxRTB.Text = "";
            this.richTextBoxRTB.Visible = false;
            this.richTextBoxRTB.Validated += new System.EventHandler(this.textboxTB_Validated);
            // 
            // controlPanel
            // 
            this.controlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlPanel.Controls.Add(this.richTextBoxRTB);
            this.controlPanel.Controls.Add(this.labelLA);
            this.controlPanel.Controls.Add(this.label5);
            this.controlPanel.Controls.Add(this.textboxTB);
            this.controlPanel.Controls.Add(this.label4);
            this.controlPanel.Controls.Add(this.queryBT);
            this.controlPanel.Controls.Add(this.label3);
            this.controlPanel.Controls.Add(this.mappingBT);
            this.controlPanel.Location = new System.Drawing.Point(6, 76);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(703, 506);
            this.controlPanel.TabIndex = 11;
            // 
            // mappingPanel
            // 
            this.mappingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mappingPanel.Controls.Add(this.label2);
            this.mappingPanel.Controls.Add(this.closeMappingPanelBT);
            this.mappingPanel.Controls.Add(this.valueMappingTV);
            this.mappingPanel.Location = new System.Drawing.Point(6, 76);
            this.mappingPanel.Name = "mappingPanel";
            this.mappingPanel.Size = new System.Drawing.Size(703, 506);
            this.mappingPanel.TabIndex = 12;
            // 
            // valueMappingTV
            // 
            this.valueMappingTV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueMappingTV.Location = new System.Drawing.Point(10, 32);
            this.valueMappingTV.Name = "valueMappingTV";
            this.valueMappingTV.Size = new System.Drawing.Size(547, 464);
            this.valueMappingTV.TabIndex = 0;
            this.valueMappingTV.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.valueMappingTV_NodeMouseDoubleClick);
            // 
            // closeMappingPanelBT
            // 
            this.closeMappingPanelBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeMappingPanelBT.Location = new System.Drawing.Point(575, 460);
            this.closeMappingPanelBT.Name = "closeMappingPanelBT";
            this.closeMappingPanelBT.Size = new System.Drawing.Size(125, 36);
            this.closeMappingPanelBT.TabIndex = 13;
            this.closeMappingPanelBT.Text = "go back";
            this.closeMappingPanelBT.UseVisualStyleBackColor = true;
            this.closeMappingPanelBT.Click += new System.EventHandler(this.closeMappingPanelBT_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(237, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "select a Value by double-clicking";
            // 
            // ParameterMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 631);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closeBT);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.mappingPanel);
            this.Name = "ParameterMapping";
            this.ShowInTaskbar = false;
            this.Text = "ParameterMapping";
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            this.mappingPanel.ResumeLayout(false);
            this.mappingPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closeBT;
        private System.Windows.Forms.TextBox textboxTB;
        private System.Windows.Forms.Label labelLA;
        private System.Windows.Forms.Button queryBT;
        private System.Windows.Forms.Button mappingBT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox richTextBoxRTB;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Panel mappingPanel;
        private System.Windows.Forms.TreeView valueMappingTV;
        private System.Windows.Forms.Button closeMappingPanelBT;
        private System.Windows.Forms.Label label2;
    }
}