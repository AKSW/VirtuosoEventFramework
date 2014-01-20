namespace EventFrameworkControl
{
    partial class atomicEventControl
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
            this.atomicEventsDGV = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.deleteTriggerBT = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chooseExternalCB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.createRdfTriggerBT = new System.Windows.Forms.Button();
            this.registerEventBT = new System.Windows.Forms.Button();
            this.chooseDbCB = new System.Windows.Forms.ComboBox();
            this.createTableTriggerBT = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.atomicEventsDGV)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // atomicEventsDGV
            // 
            this.atomicEventsDGV.AllowUserToAddRows = false;
            this.atomicEventsDGV.AllowUserToDeleteRows = false;
            this.atomicEventsDGV.AllowUserToResizeRows = false;
            this.atomicEventsDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.atomicEventsDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.atomicEventsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.atomicEventsDGV.Location = new System.Drawing.Point(0, 24);
            this.atomicEventsDGV.Name = "atomicEventsDGV";
            this.atomicEventsDGV.RowHeadersVisible = false;
            this.atomicEventsDGV.RowHeadersWidth = 10;
            this.atomicEventsDGV.Size = new System.Drawing.Size(1042, 420);
            this.atomicEventsDGV.TabIndex = 0;
            this.atomicEventsDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.atomicEventsDGV_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "registered atomic events";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "atomic event details";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel1.Controls.Add(this.deleteTriggerBT);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(0, 450);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 180);
            this.panel1.TabIndex = 3;
            // 
            // deleteTriggerBT
            // 
            this.deleteTriggerBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteTriggerBT.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.deleteTriggerBT.Location = new System.Drawing.Point(7, 137);
            this.deleteTriggerBT.Name = "deleteTriggerBT";
            this.deleteTriggerBT.Size = new System.Drawing.Size(176, 35);
            this.deleteTriggerBT.TabIndex = 3;
            this.deleteTriggerBT.Text = "delete this trigger";
            this.deleteTriggerBT.UseVisualStyleBackColor = false;
            this.deleteTriggerBT.Click += new System.EventHandler(this.deleteTriggerBT_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.MenuBar;
            this.panel2.Controls.Add(this.chooseExternalCB);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.createRdfTriggerBT);
            this.panel2.Controls.Add(this.registerEventBT);
            this.panel2.Controls.Add(this.chooseDbCB);
            this.panel2.Controls.Add(this.createTableTriggerBT);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(393, 450);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(649, 180);
            this.panel2.TabIndex = 4;
            // 
            // chooseExternalCB
            // 
            this.chooseExternalCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chooseExternalCB.FormattingEnabled = true;
            this.chooseExternalCB.Location = new System.Drawing.Point(383, 59);
            this.chooseExternalCB.Name = "chooseExternalCB";
            this.chooseExternalCB.Size = new System.Drawing.Size(263, 21);
            this.chooseExternalCB.TabIndex = 12;
            this.chooseExternalCB.Text = "choose the event-source where this event occurs";
            this.chooseExternalCB.DropDown += new System.EventHandler(this.chooseExternalCB_DropDown);
            this.chooseExternalCB.SelectedIndexChanged += new System.EventHandler(this.chooseExternalCB_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(380, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "or register external events";
            // 
            // createRdfTriggerBT
            // 
            this.createRdfTriggerBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.createRdfTriggerBT.BackColor = System.Drawing.SystemColors.Control;
            this.createRdfTriggerBT.Location = new System.Drawing.Point(181, 106);
            this.createRdfTriggerBT.Name = "createRdfTriggerBT";
            this.createRdfTriggerBT.Size = new System.Drawing.Size(186, 35);
            this.createRdfTriggerBT.TabIndex = 10;
            this.createRdfTriggerBT.Text = "create event for virtuoso triple-store";
            this.createRdfTriggerBT.UseVisualStyleBackColor = false;
            this.createRdfTriggerBT.Click += new System.EventHandler(this.createRdfTriggerBT_Click);
            // 
            // registerEventBT
            // 
            this.registerEventBT.BackColor = System.Drawing.SystemColors.Control;
            this.registerEventBT.Location = new System.Drawing.Point(383, 106);
            this.registerEventBT.Name = "registerEventBT";
            this.registerEventBT.Size = new System.Drawing.Size(263, 35);
            this.registerEventBT.TabIndex = 9;
            this.registerEventBT.Text = "register external event";
            this.registerEventBT.UseVisualStyleBackColor = false;
            this.registerEventBT.Click += new System.EventHandler(this.registerEventBT_Click);
            // 
            // chooseDbCB
            // 
            this.chooseDbCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chooseDbCB.FormattingEnabled = true;
            this.chooseDbCB.Location = new System.Drawing.Point(6, 59);
            this.chooseDbCB.Name = "chooseDbCB";
            this.chooseDbCB.Size = new System.Drawing.Size(361, 21);
            this.chooseDbCB.TabIndex = 7;
            this.chooseDbCB.Text = "choose the database where this event occurs";
            this.chooseDbCB.DropDown += new System.EventHandler(this.chooseDbCB_DropDown);
            this.chooseDbCB.SelectedIndexChanged += new System.EventHandler(this.chooseDbCB_SelectedIndexChanged);
            // 
            // createTableTriggerBT
            // 
            this.createTableTriggerBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.createTableTriggerBT.BackColor = System.Drawing.SystemColors.Control;
            this.createTableTriggerBT.Location = new System.Drawing.Point(6, 106);
            this.createTableTriggerBT.Name = "createTableTriggerBT";
            this.createTableTriggerBT.Size = new System.Drawing.Size(169, 35);
            this.createTableTriggerBT.TabIndex = 5;
            this.createTableTriggerBT.Text = "create event for db-table";
            this.createTableTriggerBT.UseVisualStyleBackColor = false;
            this.createTableTriggerBT.Click += new System.EventHandler(this.createEventBT_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(345, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "create new atomic events on satellite databases:";
            // 
            // atomicEventControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.atomicEventsDGV);
            this.Name = "atomicEventControl";
            this.Size = new System.Drawing.Size(1042, 633);
            ((System.ComponentModel.ISupportInitialize)(this.atomicEventsDGV)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView atomicEventsDGV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button registerEventBT;
        private System.Windows.Forms.ComboBox chooseDbCB;
        private System.Windows.Forms.Button createTableTriggerBT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button deleteTriggerBT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button createRdfTriggerBT;
        private System.Windows.Forms.ComboBox chooseExternalCB;


    }
}
