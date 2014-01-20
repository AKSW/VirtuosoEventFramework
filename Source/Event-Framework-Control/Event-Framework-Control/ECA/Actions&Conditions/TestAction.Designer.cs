namespace EventFrameworkControl
{
    partial class TestActionCondition
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
            this.labelLA = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textboxTB = new System.Windows.Forms.TextBox();
            this.confirmBT = new System.Windows.Forms.Button();
            this.richTextBoxRTB = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // labelLA
            // 
            this.labelLA.AutoSize = true;
            this.labelLA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLA.Location = new System.Drawing.Point(13, 51);
            this.labelLA.Name = "labelLA";
            this.labelLA.Size = new System.Drawing.Size(41, 15);
            this.labelLA.TabIndex = 0;
            this.labelLA.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(321, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Enter Parameters and confirm the test.";
            // 
            // textboxTB
            // 
            this.textboxTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxTB.Location = new System.Drawing.Point(16, 70);
            this.textboxTB.Name = "textboxTB";
            this.textboxTB.Size = new System.Drawing.Size(793, 20);
            this.textboxTB.TabIndex = 2;
            // 
            // confirmBT
            // 
            this.confirmBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmBT.Location = new System.Drawing.Point(675, 615);
            this.confirmBT.Name = "confirmBT";
            this.confirmBT.Size = new System.Drawing.Size(133, 30);
            this.confirmBT.TabIndex = 3;
            this.confirmBT.Text = "confirm";
            this.confirmBT.UseVisualStyleBackColor = true;
            this.confirmBT.Click += new System.EventHandler(this.confirmBT_Click);
            // 
            // richTextBoxRTB
            // 
            this.richTextBoxRTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxRTB.Location = new System.Drawing.Point(16, 70);
            this.richTextBoxRTB.Name = "richTextBoxRTB";
            this.richTextBoxRTB.Size = new System.Drawing.Size(795, 50);
            this.richTextBoxRTB.TabIndex = 4;
            this.richTextBoxRTB.Text = "";
            this.richTextBoxRTB.Visible = false;
            // 
            // TestActionCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 657);
            this.Controls.Add(this.richTextBoxRTB);
            this.Controls.Add(this.confirmBT);
            this.Controls.Add(this.textboxTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelLA);
            this.Name = "TestActionCondition";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "TestAction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLA;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textboxTB;
        private System.Windows.Forms.Button confirmBT;
        private System.Windows.Forms.RichTextBox richTextBoxRTB;
    }
}