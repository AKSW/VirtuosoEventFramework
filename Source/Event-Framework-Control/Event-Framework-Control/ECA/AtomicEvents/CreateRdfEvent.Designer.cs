namespace EventFrameworkControl
{
    partial class CreateRdfEvent
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.newDbEventTitleLA = new System.Windows.Forms.Label();
            this.triggerTypeCB = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.view1Panel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.fromLA = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.descriptionRTB = new System.Windows.Forms.RichTextBox();
            this.successLA = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.confirmTriggerBT = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.resultDGV = new System.Windows.Forms.DataGridView();
            this.executeQuerryBT = new System.Windows.Forms.Button();
            this.conditionRTB = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.graphSelectBT = new System.Windows.Forms.Button();
            this.view2Panel = new System.Windows.Forms.Panel();
            this.graphBackBT = new System.Windows.Forms.Button();
            this.graphConfirmBT = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.graphDGV = new System.Windows.Forms.DataGridView();
            this.label13 = new System.Windows.Forms.Label();
            this.view1Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultDGV)).BeginInit();
            this.view2Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(535, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(346, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "At this stage only Virtuoso-Triplesstores are supported by this framework.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(653, 13);
            this.label9.TabIndex = 38;
            this.label9.Text = "a) select a graph (FROM), b) select additional prefixes, c) ad a WHERE-statement " +
    "(stick to the SPARQL-dialect of your triplestore-provider";
            // 
            // newDbEventTitleLA
            // 
            this.newDbEventTitleLA.AutoSize = true;
            this.newDbEventTitleLA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newDbEventTitleLA.Location = new System.Drawing.Point(12, 9);
            this.newDbEventTitleLA.Name = "newDbEventTitleLA";
            this.newDbEventTitleLA.Size = new System.Drawing.Size(323, 16);
            this.newDbEventTitleLA.TabIndex = 33;
            this.newDbEventTitleLA.Text = "Create a new graph based events for Virtuoso";
            // 
            // triggerTypeCB
            // 
            this.triggerTypeCB.FormattingEnabled = true;
            this.triggerTypeCB.Items.AddRange(new object[] {
            "INSERT",
            "DELETE"});
            this.triggerTypeCB.Location = new System.Drawing.Point(10, 34);
            this.triggerTypeCB.Name = "triggerTypeCB";
            this.triggerTypeCB.Size = new System.Drawing.Size(411, 21);
            this.triggerTypeCB.TabIndex = 24;
            this.triggerTypeCB.Text = "1. choose the type of data-manipulation the new event should be triggered on";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(663, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "2. construct a simple SELECT FROM WHERE query which returns exactly those triples" +
    ", the new event should be triggered on in the future. ";
            // 
            // view1Panel
            // 
            this.view1Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.view1Panel.Controls.Add(this.label2);
            this.view1Panel.Controls.Add(this.fromLA);
            this.view1Panel.Controls.Add(this.label4);
            this.view1Panel.Controls.Add(this.descriptionRTB);
            this.view1Panel.Controls.Add(this.successLA);
            this.view1Panel.Controls.Add(this.label6);
            this.view1Panel.Controls.Add(this.confirmTriggerBT);
            this.view1Panel.Controls.Add(this.label7);
            this.view1Panel.Controls.Add(this.resultDGV);
            this.view1Panel.Controls.Add(this.executeQuerryBT);
            this.view1Panel.Controls.Add(this.conditionRTB);
            this.view1Panel.Controls.Add(this.label5);
            this.view1Panel.Controls.Add(this.label10);
            this.view1Panel.Controls.Add(this.label11);
            this.view1Panel.Controls.Add(this.graphSelectBT);
            this.view1Panel.Location = new System.Drawing.Point(0, 87);
            this.view1Panel.Name = "view1Panel";
            this.view1Panel.Size = new System.Drawing.Size(909, 493);
            this.view1Panel.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 13);
            this.label2.TabIndex = 60;
            this.label2.Text = "(the triple: ?subj ?pred ?obj. is obligatory!)";
            // 
            // fromLA
            // 
            this.fromLA.AutoSize = true;
            this.fromLA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromLA.Location = new System.Drawing.Point(231, 41);
            this.fromLA.Name = "fromLA";
            this.fromLA.Size = new System.Drawing.Size(0, 16);
            this.fromLA.TabIndex = 59;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 458);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 13);
            this.label4.TabIndex = 56;
            this.label4.Text = "3. enter a short description:";
            // 
            // descriptionRTB
            // 
            this.descriptionRTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionRTB.Location = new System.Drawing.Point(148, 437);
            this.descriptionRTB.Name = "descriptionRTB";
            this.descriptionRTB.Size = new System.Drawing.Size(326, 54);
            this.descriptionRTB.TabIndex = 55;
            this.descriptionRTB.Text = "";
            // 
            // successLA
            // 
            this.successLA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.successLA.AutoSize = true;
            this.successLA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.successLA.Location = new System.Drawing.Point(323, 435);
            this.successLA.Name = "successLA";
            this.successLA.Size = new System.Drawing.Size(0, 13);
            this.successLA.TabIndex = 54;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(246, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(354, 13);
            this.label6.TabIndex = 53;
            this.label6.Text = "(note: the ?graph will match the From-statements, no graph-query needed)";
            // 
            // confirmTriggerBT
            // 
            this.confirmTriggerBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmTriggerBT.Location = new System.Drawing.Point(661, 437);
            this.confirmTriggerBT.Name = "confirmTriggerBT";
            this.confirmTriggerBT.Size = new System.Drawing.Size(241, 54);
            this.confirmTriggerBT.TabIndex = 51;
            this.confirmTriggerBT.Text = "create event and insert new trigger in table";
            this.confirmTriggerBT.UseVisualStyleBackColor = true;
            this.confirmTriggerBT.Click += new System.EventHandler(this.confirmTriggerBT_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(490, 458);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(170, 13);
            this.label7.TabIndex = 50;
            this.label7.Text = "4. confirm the new atomic event ->";
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
            this.resultDGV.Location = new System.Drawing.Point(10, 240);
            this.resultDGV.Name = "resultDGV";
            this.resultDGV.Size = new System.Drawing.Size(892, 191);
            this.resultDGV.TabIndex = 49;
            // 
            // executeQuerryBT
            // 
            this.executeQuerryBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.executeQuerryBT.Location = new System.Drawing.Point(849, 68);
            this.executeQuerryBT.Name = "executeQuerryBT";
            this.executeQuerryBT.Size = new System.Drawing.Size(53, 151);
            this.executeQuerryBT.TabIndex = 48;
            this.executeQuerryBT.Text = "execute";
            this.executeQuerryBT.UseVisualStyleBackColor = true;
            this.executeQuerryBT.Click += new System.EventHandler(this.executeQuerryBT_Click);
            // 
            // conditionRTB
            // 
            this.conditionRTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionRTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conditionRTB.Location = new System.Drawing.Point(10, 68);
            this.conditionRTB.Name = "conditionRTB";
            this.conditionRTB.Size = new System.Drawing.Size(833, 151);
            this.conditionRTB.TabIndex = 47;
            this.conditionRTB.Text = "WHERE {?subj ?pred ?obj. ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 16);
            this.label5.TabIndex = 46;
            this.label5.Text = "FROM";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(210, 16);
            this.label10.TabIndex = 45;
            this.label10.Text = "SELECT  ?graph ?subj ?pred ?obj";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(412, 13);
            this.label11.TabIndex = 44;
            this.label11.Text = "---------------------------------------------------------------------------------" +
    "------------------------------------------------------";
            // 
            // graphSelectBT
            // 
            this.graphSelectBT.Location = new System.Drawing.Point(71, 43);
            this.graphSelectBT.Name = "graphSelectBT";
            this.graphSelectBT.Size = new System.Drawing.Size(147, 24);
            this.graphSelectBT.TabIndex = 58;
            this.graphSelectBT.Text = "select graphs and prefixes";
            this.graphSelectBT.UseVisualStyleBackColor = true;
            this.graphSelectBT.Click += new System.EventHandler(this.graphSelectBT_Click);
            // 
            // view2Panel
            // 
            this.view2Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.view2Panel.Controls.Add(this.graphBackBT);
            this.view2Panel.Controls.Add(this.graphConfirmBT);
            this.view2Panel.Controls.Add(this.label8);
            this.view2Panel.Controls.Add(this.graphDGV);
            this.view2Panel.Location = new System.Drawing.Point(0, 87);
            this.view2Panel.Name = "view2Panel";
            this.view2Panel.Size = new System.Drawing.Size(909, 493);
            this.view2Panel.TabIndex = 59;
            // 
            // graphBackBT
            // 
            this.graphBackBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.graphBackBT.Location = new System.Drawing.Point(10, 454);
            this.graphBackBT.Name = "graphBackBT";
            this.graphBackBT.Size = new System.Drawing.Size(182, 36);
            this.graphBackBT.TabIndex = 3;
            this.graphBackBT.Text = "back without changes";
            this.graphBackBT.UseVisualStyleBackColor = true;
            this.graphBackBT.Click += new System.EventHandler(this.graphBackBT_Click);
            // 
            // graphConfirmBT
            // 
            this.graphConfirmBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.graphConfirmBT.Location = new System.Drawing.Point(714, 454);
            this.graphConfirmBT.Name = "graphConfirmBT";
            this.graphConfirmBT.Size = new System.Drawing.Size(182, 36);
            this.graphConfirmBT.TabIndex = 2;
            this.graphConfirmBT.Text = "confirm";
            this.graphConfirmBT.UseVisualStyleBackColor = true;
            this.graphConfirmBT.Click += new System.EventHandler(this.graphConfirmBT_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(551, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Select graphs if their recources will be used in your query. Enter e prefix-abriv" +
    "ation to shorten your query (e.g. \' tag: \')";
            // 
            // graphDGV
            // 
            this.graphDGV.AllowUserToDeleteRows = false;
            this.graphDGV.AllowUserToResizeRows = false;
            this.graphDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.graphDGV.DefaultCellStyle = dataGridViewCellStyle6;
            this.graphDGV.Location = new System.Drawing.Point(10, 37);
            this.graphDGV.Name = "graphDGV";
            this.graphDGV.RowHeadersVisible = false;
            this.graphDGV.RowHeadersWidth = 4;
            this.graphDGV.Size = new System.Drawing.Size(887, 411);
            this.graphDGV.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(535, 34);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(276, 13);
            this.label13.TabIndex = 60;
            this.label13.Text = "Please use external events for other triple-store providers.";
            // 
            // CreateRdfEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 584);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.newDbEventTitleLA);
            this.Controls.Add(this.triggerTypeCB);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.view1Panel);
            this.Controls.Add(this.view2Panel);
            this.Name = "CreateRdfEvent";
            this.Text = "CreateRdfEvent";
            this.view1Panel.ResumeLayout(false);
            this.view1Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultDGV)).EndInit();
            this.view2Panel.ResumeLayout(false);
            this.view2Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label newDbEventTitleLA;
        private System.Windows.Forms.ComboBox triggerTypeCB;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel view1Panel;
        private System.Windows.Forms.Panel view2Panel;
        private System.Windows.Forms.Button graphConfirmBT;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView graphDGV;
        private System.Windows.Forms.Button graphSelectBT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox descriptionRTB;
        private System.Windows.Forms.Label successLA;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button confirmTriggerBT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView resultDGV;
        private System.Windows.Forms.Button executeQuerryBT;
        private System.Windows.Forms.RichTextBox conditionRTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button graphBackBT;
        private System.Windows.Forms.Label fromLA;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label2;
    }
}