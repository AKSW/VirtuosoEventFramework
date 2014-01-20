namespace EventFrameworkControl
{
    partial class CreateRegisterEvents
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
            this.triggerTypeCB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.conditionRTB = new System.Windows.Forms.RichTextBox();
            this.executeQuerryBT = new System.Windows.Forms.Button();
            this.resultDGV = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.confirmTriggerBT = new System.Windows.Forms.Button();
            this.newDbEventTitleLA = new System.Windows.Forms.Label();
            this.chooseTableCB = new System.Windows.Forms.ComboBox();
            this.chooseColumnsTB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.successLA = new System.Windows.Forms.Label();
            this.descriptionRTB = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.resultDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(661, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "2. construct a simple SELECT FROM WHERE querry which returns exactly those rows, " +
    "the new event should be triggered on in the future. ";
            // 
            // triggerTypeCB
            // 
            this.triggerTypeCB.FormattingEnabled = true;
            this.triggerTypeCB.Items.AddRange(new object[] {
            "INSERT",
            "DELETE"});
            this.triggerTypeCB.Location = new System.Drawing.Point(12, 34);
            this.triggerTypeCB.Name = "triggerTypeCB";
            this.triggerTypeCB.Size = new System.Drawing.Size(411, 21);
            this.triggerTypeCB.TabIndex = 2;
            this.triggerTypeCB.Text = "1. choose the type of data-manipulation the new event should be triggered on";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(412, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "---------------------------------------------------------------------------------" +
    "------------------------------------------------------";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "SELECT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "FROM";
            // 
            // conditionRTB
            // 
            this.conditionRTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionRTB.Location = new System.Drawing.Point(25, 155);
            this.conditionRTB.Name = "conditionRTB";
            this.conditionRTB.Size = new System.Drawing.Size(750, 96);
            this.conditionRTB.TabIndex = 7;
            this.conditionRTB.Text = "WHERE ";
            // 
            // executeQuerryBT
            // 
            this.executeQuerryBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.executeQuerryBT.Location = new System.Drawing.Point(779, 111);
            this.executeQuerryBT.Name = "executeQuerryBT";
            this.executeQuerryBT.Size = new System.Drawing.Size(53, 140);
            this.executeQuerryBT.TabIndex = 8;
            this.executeQuerryBT.Text = "execute";
            this.executeQuerryBT.UseVisualStyleBackColor = true;
            this.executeQuerryBT.Click += new System.EventHandler(this.executeQuerryBT_Click);
            // 
            // resultDGV
            // 
            this.resultDGV.AllowUserToAddRows = false;
            this.resultDGV.AllowUserToDeleteRows = false;
            this.resultDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.resultDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultDGV.Location = new System.Drawing.Point(2, 257);
            this.resultDGV.Name = "resultDGV";
            this.resultDGV.Size = new System.Drawing.Size(830, 220);
            this.resultDGV.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(581, 486);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(170, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "4. confirm the new atomic event ->";
            // 
            // confirmTriggerBT
            // 
            this.confirmTriggerBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmTriggerBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmTriggerBT.Location = new System.Drawing.Point(584, 522);
            this.confirmTriggerBT.Name = "confirmTriggerBT";
            this.confirmTriggerBT.Size = new System.Drawing.Size(248, 27);
            this.confirmTriggerBT.TabIndex = 11;
            this.confirmTriggerBT.Text = "create event and insert new trigger";
            this.confirmTriggerBT.UseVisualStyleBackColor = true;
            this.confirmTriggerBT.Click += new System.EventHandler(this.confirmTriggerBT_Click);
            // 
            // newDbEventTitleLA
            // 
            this.newDbEventTitleLA.AutoSize = true;
            this.newDbEventTitleLA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newDbEventTitleLA.Location = new System.Drawing.Point(9, 9);
            this.newDbEventTitleLA.Name = "newDbEventTitleLA";
            this.newDbEventTitleLA.Size = new System.Drawing.Size(339, 16);
            this.newDbEventTitleLA.TabIndex = 12;
            this.newDbEventTitleLA.Text = "Create a new atomic event for a database-table:";
            // 
            // chooseTableCB
            // 
            this.chooseTableCB.FormattingEnabled = true;
            this.chooseTableCB.Items.AddRange(new object[] {
            "AFTER INSERT",
            "BEFORE INSERT",
            "AFTER UPDATE",
            "BEFORE UPDATE",
            "AFTER DELETE",
            "BEFORE DELETE"});
            this.chooseTableCB.Location = new System.Drawing.Point(83, 131);
            this.chooseTableCB.Name = "chooseTableCB";
            this.chooseTableCB.Size = new System.Drawing.Size(692, 21);
            this.chooseTableCB.TabIndex = 13;
            this.chooseTableCB.Text = " choose the table on which the event will occure";
            this.chooseTableCB.SelectedIndexChanged += new System.EventHandler(this.chooseTableCB_SelectedIndexChanged);
            // 
            // chooseColumnsTB
            // 
            this.chooseColumnsTB.Location = new System.Drawing.Point(83, 111);
            this.chooseColumnsTB.Name = "chooseColumnsTB";
            this.chooseColumnsTB.Size = new System.Drawing.Size(692, 20);
            this.chooseColumnsTB.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(748, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "a) select a table (FROM), b) selct the columns which should be returned (SELECT)," +
    " c) add conditions (WHERE...) stick to the syntax of the selected database";
            // 
            // successLA
            // 
            this.successLA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.successLA.AutoSize = true;
            this.successLA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.successLA.Location = new System.Drawing.Point(341, 531);
            this.successLA.Name = "successLA";
            this.successLA.Size = new System.Drawing.Size(0, 13);
            this.successLA.TabIndex = 20;
            // 
            // descriptionRTB
            // 
            this.descriptionRTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.descriptionRTB.Location = new System.Drawing.Point(189, 483);
            this.descriptionRTB.Name = "descriptionRTB";
            this.descriptionRTB.Size = new System.Drawing.Size(382, 66);
            this.descriptionRTB.TabIndex = 21;
            this.descriptionRTB.Text = "";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 486);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "3. enter a short description:";
            // 
            // CreateRegisterEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 553);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.descriptionRTB);
            this.Controls.Add(this.successLA);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chooseColumnsTB);
            this.Controls.Add(this.chooseTableCB);
            this.Controls.Add(this.newDbEventTitleLA);
            this.Controls.Add(this.confirmTriggerBT);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.resultDGV);
            this.Controls.Add(this.executeQuerryBT);
            this.Controls.Add(this.conditionRTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.triggerTypeCB);
            this.Controls.Add(this.label1);
            this.Name = "CreateRegisterEvents";
            this.ShowInTaskbar = false;
            this.Text = "CreateRegisterEvents";
            ((System.ComponentModel.ISupportInitialize)(this.resultDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox triggerTypeCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox conditionRTB;
        private System.Windows.Forms.Button executeQuerryBT;
        private System.Windows.Forms.DataGridView resultDGV;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button confirmTriggerBT;
        private System.Windows.Forms.Label newDbEventTitleLA;
        private System.Windows.Forms.ComboBox chooseTableCB;
        private System.Windows.Forms.TextBox chooseColumnsTB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label successLA;
        private System.Windows.Forms.RichTextBox descriptionRTB;
        private System.Windows.Forms.Label label2;
    }
}