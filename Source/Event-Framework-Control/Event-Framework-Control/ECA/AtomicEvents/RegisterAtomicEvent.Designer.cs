namespace EventFrameworkControl
{
    partial class RegisterAtomicEvent
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
            this.selectedEventSourceLA = new System.Windows.Forms.Label();
            this.altNameTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.descriptionRTB = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.messageRTB = new System.Windows.Forms.RichTextBox();
            this.registerBT = new System.Windows.Forms.Button();
            this.returnValuesTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // selectedEventSourceLA
            // 
            this.selectedEventSourceLA.AutoSize = true;
            this.selectedEventSourceLA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedEventSourceLA.Location = new System.Drawing.Point(12, 16);
            this.selectedEventSourceLA.Name = "selectedEventSourceLA";
            this.selectedEventSourceLA.Size = new System.Drawing.Size(213, 16);
            this.selectedEventSourceLA.TabIndex = 12;
            this.selectedEventSourceLA.Text = "register ne external event for: ";
            // 
            // altNameTB
            // 
            this.altNameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.altNameTB.Location = new System.Drawing.Point(15, 60);
            this.altNameTB.Name = "altNameTB";
            this.altNameTB.Size = new System.Drawing.Size(807, 20);
            this.altNameTB.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 16);
            this.label1.TabIndex = 20;
            this.label1.Text = "enter a name for this event:";
            // 
            // descriptionRTB
            // 
            this.descriptionRTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionRTB.Location = new System.Drawing.Point(15, 144);
            this.descriptionRTB.Name = "descriptionRTB";
            this.descriptionRTB.Size = new System.Drawing.Size(807, 73);
            this.descriptionRTB.TabIndex = 21;
            this.descriptionRTB.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 16);
            this.label3.TabIndex = 22;
            this.label3.Text = "enter a short description for this event:";
            // 
            // messageRTB
            // 
            this.messageRTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageRTB.Enabled = false;
            this.messageRTB.Location = new System.Drawing.Point(15, 252);
            this.messageRTB.Name = "messageRTB";
            this.messageRTB.Size = new System.Drawing.Size(807, 299);
            this.messageRTB.TabIndex = 24;
            this.messageRTB.Text = "";
            // 
            // registerBT
            // 
            this.registerBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.registerBT.Location = new System.Drawing.Point(694, 226);
            this.registerBT.Name = "registerBT";
            this.registerBT.Size = new System.Drawing.Size(128, 23);
            this.registerBT.TabIndex = 25;
            this.registerBT.Text = "register event";
            this.registerBT.UseVisualStyleBackColor = true;
            this.registerBT.Click += new System.EventHandler(this.registerBT_Click);
            // 
            // returnValuesTB
            // 
            this.returnValuesTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.returnValuesTB.Location = new System.Drawing.Point(15, 102);
            this.returnValuesTB.Name = "returnValuesTB";
            this.returnValuesTB.Size = new System.Drawing.Size(807, 20);
            this.returnValuesTB.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(332, 16);
            this.label5.TabIndex = 27;
            this.label5.Text = "enter names of retuned values as comma-list (optional)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(568, 16);
            this.label2.TabIndex = 28;
            this.label2.Text = "use the SOAP-message below as a tamplete for sending event messages from a event-" +
    "source";
            // 
            // RegisterAtomicEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 563);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.returnValuesTB);
            this.Controls.Add(this.registerBT);
            this.Controls.Add(this.messageRTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.descriptionRTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.altNameTB);
            this.Controls.Add(this.selectedEventSourceLA);
            this.Name = "RegisterAtomicEvent";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "RegisterAtomicEvent";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label selectedEventSourceLA;
        private System.Windows.Forms.TextBox altNameTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox descriptionRTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox messageRTB;
        private System.Windows.Forms.Button registerBT;
        private System.Windows.Forms.TextBox returnValuesTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
    }
}