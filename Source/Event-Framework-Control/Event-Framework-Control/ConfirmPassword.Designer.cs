namespace EventFrameworkControl
{
    partial class ConfirmPassword
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
            this.passTB = new System.Windows.Forms.TextBox();
            this.passBT = new System.Windows.Forms.Button();
            this.newPassTB = new System.Windows.Forms.TextBox();
            this.newPassLA = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please enter your pasword to confirm your action.";
            // 
            // passTB
            // 
            this.passTB.Location = new System.Drawing.Point(12, 53);
            this.passTB.Name = "passTB";
            this.passTB.Size = new System.Drawing.Size(206, 20);
            this.passTB.TabIndex = 1;
            this.passTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.passTB_KeyDown);
            // 
            // passBT
            // 
            this.passBT.Location = new System.Drawing.Point(241, 53);
            this.passBT.Name = "passBT";
            this.passBT.Size = new System.Drawing.Size(75, 20);
            this.passBT.TabIndex = 2;
            this.passBT.Text = "OK";
            this.passBT.UseVisualStyleBackColor = true;
            this.passBT.Click += new System.EventHandler(this.passBT_Click);
            // 
            // newPassTB
            // 
            this.newPassTB.Location = new System.Drawing.Point(96, 6);
            this.newPassTB.Name = "newPassTB";
            this.newPassTB.Size = new System.Drawing.Size(122, 20);
            this.newPassTB.TabIndex = 3;
            // 
            // newPassLA
            // 
            this.newPassLA.AutoSize = true;
            this.newPassLA.Location = new System.Drawing.Point(12, 9);
            this.newPassLA.Name = "newPassLA";
            this.newPassLA.Size = new System.Drawing.Size(78, 13);
            this.newPassLA.TabIndex = 4;
            this.newPassLA.Text = "new password:";
            // 
            // ConfirmPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 77);
            this.Controls.Add(this.newPassLA);
            this.Controls.Add(this.newPassTB);
            this.Controls.Add(this.passBT);
            this.Controls.Add(this.passTB);
            this.Controls.Add(this.label1);
            this.Name = "ConfirmPassword";
            this.Text = "ConfirmPassword";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passTB;
        private System.Windows.Forms.Button passBT;
        private System.Windows.Forms.TextBox newPassTB;
        private System.Windows.Forms.Label newPassLA;
    }
}