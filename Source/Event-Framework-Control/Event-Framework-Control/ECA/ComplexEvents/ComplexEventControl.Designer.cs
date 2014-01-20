namespace EventFrameworkControl
{
    partial class ComplexEventControl
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
            this.overlappingCB = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.eventSetPanel = new System.Windows.Forms.Panel();
            this.actionsPanel = new System.Windows.Forms.Panel();
            this.conditionsPanel = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.days1TB = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.minutes1TB = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.hours1TB = new System.Windows.Forms.TextBox();
            this.deleteStageBT = new System.Windows.Forms.Button();
            this.nextStageBT = new System.Windows.Forms.Button();
            this.actionsBT = new System.Windows.Forms.Button();
            this.conditionsBT = new System.Windows.Forms.Button();
            this.eventSetBT = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.initialCB = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.timePanel = new System.Windows.Forms.Panel();
            this.restrictionCB = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.daysTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.hoursTB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.minutesTB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.secondsTB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.waitTillEndCB = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.actionsLA = new System.Windows.Forms.Label();
            this.complexEventsDGV = new System.Windows.Forms.DataGridView();
            this.label17 = new System.Windows.Forms.Label();
            this.activatrBT = new System.Windows.Forms.Button();
            this.repeaterCB = new System.Windows.Forms.ComboBox();
            this.newCeBT = new System.Windows.Forms.Button();
            this.saveCeBT = new System.Windows.Forms.Button();
            this.activCB = new System.Windows.Forms.CheckBox();
            this.descriptionRTB = new System.Windows.Forms.RichTextBox();
            this.nameTB = new System.Windows.Forms.TextBox();
            this.deleteCeBT = new System.Windows.Forms.Button();
            this.editCeBT = new System.Windows.Forms.Button();
            this.initDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.initTimePicker = new System.Windows.Forms.DateTimePicker();
            this.stagePanel = new System.Windows.Forms.Panel();
            this.eventSetPanel.SuspendLayout();
            this.actionsPanel.SuspendLayout();
            this.timePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.complexEventsDGV)).BeginInit();
            this.stagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // overlappingCB
            // 
            this.overlappingCB.AutoSize = true;
            this.overlappingCB.Location = new System.Drawing.Point(163, 195);
            this.overlappingCB.Name = "overlappingCB";
            this.overlappingCB.Size = new System.Drawing.Size(117, 17);
            this.overlappingCB.TabIndex = 13;
            this.overlappingCB.Text = "events can overlap";
            this.overlappingCB.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Description:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "New Complex Event";
            // 
            // eventSetPanel
            // 
            this.eventSetPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventSetPanel.Controls.Add(this.actionsPanel);
            this.eventSetPanel.Location = new System.Drawing.Point(0, 0);
            this.eventSetPanel.Name = "eventSetPanel";
            this.eventSetPanel.Size = new System.Drawing.Size(1193, 686);
            this.eventSetPanel.TabIndex = 15;
            // 
            // actionsPanel
            // 
            this.actionsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actionsPanel.Controls.Add(this.conditionsPanel);
            this.actionsPanel.Location = new System.Drawing.Point(0, 0);
            this.actionsPanel.Name = "actionsPanel";
            this.actionsPanel.Size = new System.Drawing.Size(1193, 686);
            this.actionsPanel.TabIndex = 17;
            // 
            // conditionsPanel
            // 
            this.conditionsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionsPanel.Location = new System.Drawing.Point(0, 0);
            this.conditionsPanel.Name = "conditionsPanel";
            this.conditionsPanel.Size = new System.Drawing.Size(1193, 686);
            this.conditionsPanel.TabIndex = 16;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 557);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(121, 13);
            this.label11.TabIndex = 86;
            this.label11.Text = "or double-click on event";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(4, 310);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 13);
            this.label21.TabIndex = 24;
            this.label21.Text = "days";
            // 
            // days1TB
            // 
            this.days1TB.Enabled = false;
            this.days1TB.Location = new System.Drawing.Point(4, 326);
            this.days1TB.Name = "days1TB";
            this.days1TB.Size = new System.Drawing.Size(60, 20);
            this.days1TB.TabIndex = 25;
            this.days1TB.TextChanged += new System.EventHandler(this.hours1TB_TextChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(4, 297);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(99, 13);
            this.label20.TabIndex = 84;
            this.label20.Text = "repeat event every:";
            // 
            // minutes1TB
            // 
            this.minutes1TB.Enabled = false;
            this.minutes1TB.Location = new System.Drawing.Point(163, 326);
            this.minutes1TB.Name = "minutes1TB";
            this.minutes1TB.Size = new System.Drawing.Size(60, 20);
            this.minutes1TB.TabIndex = 21;
            this.minutes1TB.TextChanged += new System.EventHandler(this.hours1TB_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(160, 310);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(43, 13);
            this.label23.TabIndex = 20;
            this.label23.Text = "minutes";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(81, 310);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(33, 13);
            this.label22.TabIndex = 18;
            this.label22.Text = "hours";
            // 
            // hours1TB
            // 
            this.hours1TB.Enabled = false;
            this.hours1TB.Location = new System.Drawing.Point(82, 326);
            this.hours1TB.Name = "hours1TB";
            this.hours1TB.Size = new System.Drawing.Size(60, 20);
            this.hours1TB.TabIndex = 19;
            this.hours1TB.TextChanged += new System.EventHandler(this.hours1TB_TextChanged);
            // 
            // deleteStageBT
            // 
            this.deleteStageBT.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.deleteStageBT.Location = new System.Drawing.Point(575, 313);
            this.deleteStageBT.Name = "deleteStageBT";
            this.deleteStageBT.Size = new System.Drawing.Size(170, 29);
            this.deleteStageBT.TabIndex = 58;
            this.deleteStageBT.Text = "Remove Stage";
            this.deleteStageBT.UseVisualStyleBackColor = false;
            this.deleteStageBT.Visible = false;
            this.deleteStageBT.Click += new System.EventHandler(this.deleteStageBT_Click);
            // 
            // nextStageBT
            // 
            this.nextStageBT.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.nextStageBT.Location = new System.Drawing.Point(210, 313);
            this.nextStageBT.Name = "nextStageBT";
            this.nextStageBT.Size = new System.Drawing.Size(170, 29);
            this.nextStageBT.TabIndex = 57;
            this.nextStageBT.Text = "Next Stage";
            this.nextStageBT.UseVisualStyleBackColor = false;
            this.nextStageBT.Click += new System.EventHandler(this.nextStageBT_Click);
            // 
            // actionsBT
            // 
            this.actionsBT.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.actionsBT.Location = new System.Drawing.Point(210, 225);
            this.actionsBT.Name = "actionsBT";
            this.actionsBT.Size = new System.Drawing.Size(170, 29);
            this.actionsBT.TabIndex = 55;
            this.actionsBT.Text = "Actions";
            this.actionsBT.UseVisualStyleBackColor = false;
            this.actionsBT.Click += new System.EventHandler(this.actionsBT_Click);
            // 
            // conditionsBT
            // 
            this.conditionsBT.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.conditionsBT.Location = new System.Drawing.Point(210, 138);
            this.conditionsBT.Name = "conditionsBT";
            this.conditionsBT.Size = new System.Drawing.Size(170, 29);
            this.conditionsBT.TabIndex = 53;
            this.conditionsBT.Text = "Conditions";
            this.conditionsBT.UseVisualStyleBackColor = false;
            this.conditionsBT.Click += new System.EventHandler(this.conditionsBT_Click);
            // 
            // eventSetBT
            // 
            this.eventSetBT.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.eventSetBT.Location = new System.Drawing.Point(210, 40);
            this.eventSetBT.Name = "eventSetBT";
            this.eventSetBT.Size = new System.Drawing.Size(170, 29);
            this.eventSetBT.TabIndex = 51;
            this.eventSetBT.Text = "Event-Set";
            this.eventSetBT.UseVisualStyleBackColor = false;
            this.eventSetBT.Click += new System.EventHandler(this.eventSetBT_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(12, 11);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(117, 20);
            this.label16.TabIndex = 49;
            this.label16.Text = "Event Stages";
            // 
            // initialCB
            // 
            this.initialCB.Appearance = System.Windows.Forms.Appearance.Button;
            this.initialCB.AutoSize = true;
            this.initialCB.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.initialCB.FlatAppearance.BorderSize = 0;
            this.initialCB.Location = new System.Drawing.Point(9, 40);
            this.initialCB.Name = "initialCB";
            this.initialCB.Size = new System.Drawing.Size(126, 23);
            this.initialCB.TabIndex = 66;
            this.initialCB.Text = "         Initial Stage         ";
            this.initialCB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.initialCB.UseVisualStyleBackColor = false;
            this.initialCB.Click += new System.EventHandler(this.checkBoxes_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(185, 21);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(211, 16);
            this.label15.TabIndex = 61;
            this.label15.Text = "1. Define events of this stage:";
            // 
            // timePanel
            // 
            this.timePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timePanel.Controls.Add(this.restrictionCB);
            this.timePanel.Controls.Add(this.label25);
            this.timePanel.Controls.Add(this.label12);
            this.timePanel.Controls.Add(this.daysTB);
            this.timePanel.Controls.Add(this.label4);
            this.timePanel.Controls.Add(this.label5);
            this.timePanel.Controls.Add(this.label6);
            this.timePanel.Controls.Add(this.label7);
            this.timePanel.Controls.Add(this.hoursTB);
            this.timePanel.Controls.Add(this.label8);
            this.timePanel.Controls.Add(this.minutesTB);
            this.timePanel.Controls.Add(this.label9);
            this.timePanel.Controls.Add(this.secondsTB);
            this.timePanel.Controls.Add(this.label10);
            this.timePanel.Controls.Add(this.waitTillEndCB);
            this.timePanel.Location = new System.Drawing.Point(413, 11);
            this.timePanel.Name = "timePanel";
            this.timePanel.Size = new System.Drawing.Size(332, 175);
            this.timePanel.TabIndex = 65;
            // 
            // restrictionCB
            // 
            this.restrictionCB.AutoSize = true;
            this.restrictionCB.Location = new System.Drawing.Point(236, 9);
            this.restrictionCB.Name = "restrictionCB";
            this.restrictionCB.Size = new System.Drawing.Size(57, 17);
            this.restrictionCB.TabIndex = 19;
            this.restrictionCB.Text = "restrict";
            this.restrictionCB.UseVisualStyleBackColor = true;
            this.restrictionCB.CheckedChanged += new System.EventHandler(this.restrictionCB_CheckedChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(4, 148);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(286, 13);
            this.label25.TabIndex = 18;
            this.label25.Text = "(check entire time-span for events which should not occur!)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 77);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "days";
            // 
            // daysTB
            // 
            this.daysTB.Location = new System.Drawing.Point(7, 93);
            this.daysTB.Name = "daysTB";
            this.daysTB.Size = new System.Drawing.Size(60, 20);
            this.daysTB.TabIndex = 17;
            this.daysTB.TextChanged += new System.EventHandler(this.hoursTB_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "optional: use time restrictions";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "duration: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(257, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "(time in which all events in this Event Set must occur)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(92, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "hours";
            // 
            // hoursTB
            // 
            this.hoursTB.Location = new System.Drawing.Point(95, 93);
            this.hoursTB.Name = "hoursTB";
            this.hoursTB.Size = new System.Drawing.Size(60, 20);
            this.hoursTB.TabIndex = 5;
            this.hoursTB.TextChanged += new System.EventHandler(this.hoursTB_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(177, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "minutes";
            // 
            // minutesTB
            // 
            this.minutesTB.Location = new System.Drawing.Point(180, 93);
            this.minutesTB.Name = "minutesTB";
            this.minutesTB.Size = new System.Drawing.Size(60, 20);
            this.minutesTB.TabIndex = 7;
            this.minutesTB.TextChanged += new System.EventHandler(this.hoursTB_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(264, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "seconds";
            // 
            // secondsTB
            // 
            this.secondsTB.Location = new System.Drawing.Point(267, 93);
            this.secondsTB.Name = "secondsTB";
            this.secondsTB.Size = new System.Drawing.Size(60, 20);
            this.secondsTB.TabIndex = 9;
            this.secondsTB.TextChanged += new System.EventHandler(this.hoursTB_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 126);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(307, 15);
            this.label10.TabIndex = 12;
            this.label10.Text = "wait untill the end of duration before transition:";
            // 
            // waitTillEndCB
            // 
            this.waitTillEndCB.AutoSize = true;
            this.waitTillEndCB.Location = new System.Drawing.Point(312, 128);
            this.waitTillEndCB.Name = "waitTillEndCB";
            this.waitTillEndCB.Size = new System.Drawing.Size(15, 14);
            this.waitTillEndCB.TabIndex = 14;
            this.waitTillEndCB.UseVisualStyleBackColor = true;
            this.waitTillEndCB.CheckedChanged += new System.EventHandler(this.waitTillEndCB_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(185, 109);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(222, 16);
            this.label14.TabIndex = 62;
            this.label14.Text = "2. Select additional Conditions:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(185, 294);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(197, 16);
            this.label13.TabIndex = 64;
            this.label13.Text = "3b. or - create a new Stage:";
            // 
            // actionsLA
            // 
            this.actionsLA.AutoSize = true;
            this.actionsLA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actionsLA.Location = new System.Drawing.Point(185, 200);
            this.actionsLA.Name = "actionsLA";
            this.actionsLA.Size = new System.Drawing.Size(534, 16);
            this.actionsLA.TabIndex = 63;
            this.actionsLA.Text = "3a. Select actions which will be taken after all events and conditions are met:";
            // 
            // complexEventsDGV
            // 
            this.complexEventsDGV.AllowUserToAddRows = false;
            this.complexEventsDGV.AllowUserToDeleteRows = false;
            this.complexEventsDGV.AllowUserToResizeColumns = false;
            this.complexEventsDGV.AllowUserToResizeRows = false;
            this.complexEventsDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.complexEventsDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.complexEventsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.complexEventsDGV.Location = new System.Drawing.Point(153, 402);
            this.complexEventsDGV.Name = "complexEventsDGV";
            this.complexEventsDGV.RowHeadersVisible = false;
            this.complexEventsDGV.Size = new System.Drawing.Size(1040, 284);
            this.complexEventsDGV.TabIndex = 67;
            this.complexEventsDGV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.complexEventsDGV_CellDoubleClick);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(4, 262);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(145, 13);
            this.label17.TabIndex = 70;
            this.label17.Text = "times event can be repeated:";
            // 
            // activatrBT
            // 
            this.activatrBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.activatrBT.Location = new System.Drawing.Point(4, 470);
            this.activatrBT.Name = "activatrBT";
            this.activatrBT.Size = new System.Drawing.Size(143, 31);
            this.activatrBT.TabIndex = 76;
            this.activatrBT.Text = "activate event now";
            this.activatrBT.UseVisualStyleBackColor = true;
            this.activatrBT.Click += new System.EventHandler(this.activatrBT_Click);
            // 
            // repeaterCB
            // 
            this.repeaterCB.Enabled = false;
            this.repeaterCB.FormattingEnabled = true;
            this.repeaterCB.Items.AddRange(new object[] {
            "continuously",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100",
            "200",
            "300",
            "400",
            "500",
            "600",
            "700",
            "800",
            "900",
            "1000",
            "2000",
            "3000",
            "4000",
            "5000",
            "6000",
            "7000",
            "8000",
            "9000",
            "10000"});
            this.repeaterCB.Location = new System.Drawing.Point(163, 260);
            this.repeaterCB.Name = "repeaterCB";
            this.repeaterCB.Size = new System.Drawing.Size(130, 21);
            this.repeaterCB.TabIndex = 77;
            // 
            // newCeBT
            // 
            this.newCeBT.BackColor = System.Drawing.Color.Orange;
            this.newCeBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newCeBT.Location = new System.Drawing.Point(153, 368);
            this.newCeBT.Name = "newCeBT";
            this.newCeBT.Size = new System.Drawing.Size(140, 28);
            this.newCeBT.TabIndex = 75;
            this.newCeBT.Text = "new event";
            this.newCeBT.UseVisualStyleBackColor = false;
            this.newCeBT.Click += new System.EventHandler(this.newCeBT_Click);
            // 
            // saveCeBT
            // 
            this.saveCeBT.BackColor = System.Drawing.Color.Orange;
            this.saveCeBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveCeBT.Location = new System.Drawing.Point(4, 367);
            this.saveCeBT.Name = "saveCeBT";
            this.saveCeBT.Size = new System.Drawing.Size(143, 29);
            this.saveCeBT.TabIndex = 74;
            this.saveCeBT.Text = "save event";
            this.saveCeBT.UseVisualStyleBackColor = false;
            this.saveCeBT.Click += new System.EventHandler(this.saveCeBT_Click);
            // 
            // activCB
            // 
            this.activCB.AutoSize = true;
            this.activCB.Location = new System.Drawing.Point(4, 196);
            this.activCB.Name = "activCB";
            this.activCB.Size = new System.Drawing.Size(87, 17);
            this.activCB.TabIndex = 73;
            this.activCB.Text = "activate now";
            this.activCB.UseVisualStyleBackColor = true;
            this.activCB.CheckedChanged += new System.EventHandler(this.activCB_CheckedChanged);
            // 
            // descriptionRTB
            // 
            this.descriptionRTB.Location = new System.Drawing.Point(4, 118);
            this.descriptionRTB.Name = "descriptionRTB";
            this.descriptionRTB.Size = new System.Drawing.Size(289, 71);
            this.descriptionRTB.TabIndex = 72;
            this.descriptionRTB.Text = "";
            // 
            // nameTB
            // 
            this.nameTB.Location = new System.Drawing.Point(4, 64);
            this.nameTB.Name = "nameTB";
            this.nameTB.Size = new System.Drawing.Size(289, 20);
            this.nameTB.TabIndex = 71;
            // 
            // deleteCeBT
            // 
            this.deleteCeBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteCeBT.Location = new System.Drawing.Point(4, 601);
            this.deleteCeBT.Name = "deleteCeBT";
            this.deleteCeBT.Size = new System.Drawing.Size(143, 31);
            this.deleteCeBT.TabIndex = 78;
            this.deleteCeBT.Text = "delete this event";
            this.deleteCeBT.UseVisualStyleBackColor = true;
            this.deleteCeBT.Click += new System.EventHandler(this.deleteCeBT_Click);
            // 
            // editCeBT
            // 
            this.editCeBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editCeBT.Location = new System.Drawing.Point(4, 520);
            this.editCeBT.Name = "editCeBT";
            this.editCeBT.Size = new System.Drawing.Size(143, 31);
            this.editCeBT.TabIndex = 79;
            this.editCeBT.Text = "edit event";
            this.editCeBT.UseVisualStyleBackColor = true;
            this.editCeBT.Click += new System.EventHandler(this.editCeBT_Click);
            // 
            // initDatePicker
            // 
            this.initDatePicker.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.initDatePicker.Enabled = false;
            this.initDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.initDatePicker.Location = new System.Drawing.Point(98, 223);
            this.initDatePicker.Name = "initDatePicker";
            this.initDatePicker.Size = new System.Drawing.Size(119, 20);
            this.initDatePicker.TabIndex = 80;
            this.initDatePicker.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(8, 437);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(139, 15);
            this.label18.TabIndex = 81;
            this.label18.Text = "manage existing events:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(5, 228);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 13);
            this.label19.TabIndex = 82;
            this.label19.Text = "initialize event at:";
            // 
            // initTimePicker
            // 
            this.initTimePicker.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.initTimePicker.Enabled = false;
            this.initTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.initTimePicker.Location = new System.Drawing.Point(223, 223);
            this.initTimePicker.Name = "initTimePicker";
            this.initTimePicker.ShowUpDown = true;
            this.initTimePicker.Size = new System.Drawing.Size(70, 20);
            this.initTimePicker.TabIndex = 83;
            this.initTimePicker.Value = new System.DateTime(2013, 10, 11, 0, 0, 0, 0);
            // 
            // stagePanel
            // 
            this.stagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stagePanel.BackColor = System.Drawing.Color.DarkKhaki;
            this.stagePanel.Controls.Add(this.conditionsBT);
            this.stagePanel.Controls.Add(this.label16);
            this.stagePanel.Controls.Add(this.eventSetBT);
            this.stagePanel.Controls.Add(this.actionsBT);
            this.stagePanel.Controls.Add(this.actionsLA);
            this.stagePanel.Controls.Add(this.nextStageBT);
            this.stagePanel.Controls.Add(this.label13);
            this.stagePanel.Controls.Add(this.deleteStageBT);
            this.stagePanel.Controls.Add(this.label14);
            this.stagePanel.Controls.Add(this.timePanel);
            this.stagePanel.Controls.Add(this.label15);
            this.stagePanel.Controls.Add(this.initialCB);
            this.stagePanel.Location = new System.Drawing.Point(315, 3);
            this.stagePanel.Name = "stagePanel";
            this.stagePanel.Size = new System.Drawing.Size(875, 393);
            this.stagePanel.TabIndex = 85;
            // 
            // ComplexEventControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.stagePanel);
            this.Controls.Add(this.days1TB);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.initTimePicker);
            this.Controls.Add(this.minutes1TB);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.initDatePicker);
            this.Controls.Add(this.hours1TB);
            this.Controls.Add(this.editCeBT);
            this.Controls.Add(this.deleteCeBT);
            this.Controls.Add(this.activatrBT);
            this.Controls.Add(this.repeaterCB);
            this.Controls.Add(this.newCeBT);
            this.Controls.Add(this.saveCeBT);
            this.Controls.Add(this.activCB);
            this.Controls.Add(this.descriptionRTB);
            this.Controls.Add(this.nameTB);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.complexEventsDGV);
            this.Controls.Add(this.overlappingCB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.eventSetPanel);
            this.Name = "ComplexEventControl";
            this.Size = new System.Drawing.Size(1193, 686);
            this.eventSetPanel.ResumeLayout(false);
            this.actionsPanel.ResumeLayout(false);
            this.timePanel.ResumeLayout(false);
            this.timePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.complexEventsDGV)).EndInit();
            this.stagePanel.ResumeLayout(false);
            this.stagePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox overlappingCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel eventSetPanel;
        private System.Windows.Forms.Panel conditionsPanel;
        private System.Windows.Forms.Panel actionsPanel;
        private System.Windows.Forms.Button deleteStageBT;
        private System.Windows.Forms.Button nextStageBT;
        private System.Windows.Forms.Button actionsBT;
        private System.Windows.Forms.Button conditionsBT;
        private System.Windows.Forms.Button eventSetBT;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox initialCB;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel timePanel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox daysTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox hoursTB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox minutesTB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox secondsTB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox waitTillEndCB;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label actionsLA;
        private System.Windows.Forms.DataGridView complexEventsDGV;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button activatrBT;
        private System.Windows.Forms.ComboBox repeaterCB;
        private System.Windows.Forms.Button newCeBT;
        private System.Windows.Forms.Button saveCeBT;
        private System.Windows.Forms.CheckBox activCB;
        private System.Windows.Forms.RichTextBox descriptionRTB;
        private System.Windows.Forms.TextBox nameTB;
        private System.Windows.Forms.Button deleteCeBT;
        private System.Windows.Forms.Button editCeBT;
        private System.Windows.Forms.DateTimePicker initDatePicker;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker initTimePicker;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox days1TB;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox hours1TB;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox minutes1TB;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel stagePanel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox restrictionCB;
    }
}
