namespace EventFrameworkControl
{
    partial class ActionSelectControl
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
            this.label16 = new System.Windows.Forms.Label();
            this.actionsDGV = new System.Windows.Forms.DataGridView();
            this.selectedActionsDGV = new System.Windows.Forms.DataGridView();
            this.orderCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndpointAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MethodeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParamDescr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReturnDescr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.confirmBT = new System.Windows.Forms.Button();
            this.removeBT = new System.Windows.Forms.Button();
            this.mappingBT = new System.Windows.Forms.Button();
            this.addActionBT = new System.Windows.Forms.Button();
            this.editActionBT = new System.Windows.Forms.Button();
            this.testActionBT = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.actionsDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedActionsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(452, 16);
            this.label16.TabIndex = 26;
            this.label16.Text = "Select all actions to execute after completion of the current state:";
            // 
            // actionsDGV
            // 
            this.actionsDGV.AllowDrop = true;
            this.actionsDGV.AllowUserToAddRows = false;
            this.actionsDGV.AllowUserToDeleteRows = false;
            this.actionsDGV.AllowUserToResizeRows = false;
            this.actionsDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.actionsDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.actionsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.actionsDGV.Location = new System.Drawing.Point(0, 29);
            this.actionsDGV.Name = "actionsDGV";
            this.actionsDGV.RowHeadersWidth = 10;
            this.actionsDGV.Size = new System.Drawing.Size(835, 332);
            this.actionsDGV.TabIndex = 27;
            this.actionsDGV.DragOver += new System.Windows.Forms.DragEventHandler(this.dgv_DragOver);
            this.actionsDGV.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseDown);
            this.actionsDGV.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseMove);
            // 
            // selectedActionsDGV
            // 
            this.selectedActionsDGV.AllowDrop = true;
            this.selectedActionsDGV.AllowUserToAddRows = false;
            this.selectedActionsDGV.AllowUserToDeleteRows = false;
            this.selectedActionsDGV.AllowUserToResizeRows = false;
            this.selectedActionsDGV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedActionsDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.selectedActionsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.selectedActionsDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.orderCol,
            this.ActionID,
            this.EndpointAddress,
            this.MethodeName,
            this.ParamDescr,
            this.ReturnDescr,
            this.Description});
            this.selectedActionsDGV.Location = new System.Drawing.Point(0, 382);
            this.selectedActionsDGV.Name = "selectedActionsDGV";
            this.selectedActionsDGV.RowHeadersWidth = 10;
            this.selectedActionsDGV.Size = new System.Drawing.Size(835, 188);
            this.selectedActionsDGV.TabIndex = 28;
            this.selectedActionsDGV.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgv_DragDrop);
            this.selectedActionsDGV.DragOver += new System.Windows.Forms.DragEventHandler(this.dgv_DragOver);
            // 
            // orderCol
            // 
            this.orderCol.HeaderText = "Order";
            this.orderCol.Name = "orderCol";
            this.orderCol.Width = 58;
            // 
            // ActionID
            // 
            this.ActionID.HeaderText = "ID";
            this.ActionID.Name = "ActionID";
            this.ActionID.Width = 43;
            // 
            // EndpointAddress
            // 
            this.EndpointAddress.HeaderText = "Endpoint";
            this.EndpointAddress.Name = "EndpointAddress";
            this.EndpointAddress.Width = 74;
            // 
            // MethodeName
            // 
            this.MethodeName.HeaderText = "Methodename";
            this.MethodeName.Name = "MethodeName";
            // 
            // ParamDescr
            // 
            this.ParamDescr.HeaderText = "Parameters";
            this.ParamDescr.Name = "ParamDescr";
            this.ParamDescr.Width = 85;
            // 
            // ReturnDescr
            // 
            this.ReturnDescr.HeaderText = "Returns";
            this.ReturnDescr.Name = "ReturnDescr";
            this.ReturnDescr.Width = 69;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 364);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(384, 16);
            this.label1.TabIndex = 29;
            this.label1.Text = "Drag actions from the table above into the table below:";
            // 
            // confirmBT
            // 
            this.confirmBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmBT.Location = new System.Drawing.Point(842, 526);
            this.confirmBT.Name = "confirmBT";
            this.confirmBT.Size = new System.Drawing.Size(150, 41);
            this.confirmBT.TabIndex = 30;
            this.confirmBT.Text = "save action-list";
            this.confirmBT.UseVisualStyleBackColor = true;
            this.confirmBT.Click += new System.EventHandler(this.confirmBT_Click);
            // 
            // removeBT
            // 
            this.removeBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeBT.Location = new System.Drawing.Point(841, 382);
            this.removeBT.Name = "removeBT";
            this.removeBT.Size = new System.Drawing.Size(151, 45);
            this.removeBT.TabIndex = 31;
            this.removeBT.Text = "remove selected action from your collection";
            this.removeBT.UseVisualStyleBackColor = true;
            this.removeBT.Click += new System.EventHandler(this.removeBT_Click);
            // 
            // mappingBT
            // 
            this.mappingBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mappingBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mappingBT.Location = new System.Drawing.Point(841, 433);
            this.mappingBT.Name = "mappingBT";
            this.mappingBT.Size = new System.Drawing.Size(151, 43);
            this.mappingBT.TabIndex = 32;
            this.mappingBT.Text = "define parameter-mapping";
            this.mappingBT.UseVisualStyleBackColor = true;
            this.mappingBT.Click += new System.EventHandler(this.mappingBT_Click);
            // 
            // addActionBT
            // 
            this.addActionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addActionBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addActionBT.Location = new System.Drawing.Point(841, 29);
            this.addActionBT.Name = "addActionBT";
            this.addActionBT.Size = new System.Drawing.Size(151, 45);
            this.addActionBT.TabIndex = 33;
            this.addActionBT.Text = "add a new action";
            this.addActionBT.UseVisualStyleBackColor = true;
            this.addActionBT.Click += new System.EventHandler(this.addActionBT_Click);
            // 
            // editActionBT
            // 
            this.editActionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editActionBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editActionBT.Location = new System.Drawing.Point(841, 80);
            this.editActionBT.Name = "editActionBT";
            this.editActionBT.Size = new System.Drawing.Size(151, 45);
            this.editActionBT.TabIndex = 35;
            this.editActionBT.Text = "edit selected action";
            this.editActionBT.UseVisualStyleBackColor = true;
            this.editActionBT.Click += new System.EventHandler(this.editActionBT_Click);
            // 
            // testActionBT
            // 
            this.testActionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testActionBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testActionBT.Location = new System.Drawing.Point(841, 131);
            this.testActionBT.Name = "testActionBT";
            this.testActionBT.Size = new System.Drawing.Size(151, 45);
            this.testActionBT.TabIndex = 36;
            this.testActionBT.Text = "test selected action";
            this.testActionBT.UseVisualStyleBackColor = true;
            this.testActionBT.Click += new System.EventHandler(this.testActionBT_Click);
            // 
            // ActionSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.testActionBT);
            this.Controls.Add(this.editActionBT);
            this.Controls.Add(this.addActionBT);
            this.Controls.Add(this.mappingBT);
            this.Controls.Add(this.removeBT);
            this.Controls.Add(this.confirmBT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectedActionsDGV);
            this.Controls.Add(this.actionsDGV);
            this.Controls.Add(this.label16);
            this.Name = "ActionSelectControl";
            this.Size = new System.Drawing.Size(995, 570);
            ((System.ComponentModel.ISupportInitialize)(this.actionsDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedActionsDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridView actionsDGV;
        private System.Windows.Forms.DataGridView selectedActionsDGV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button confirmBT;
        private System.Windows.Forms.Button removeBT;
        private System.Windows.Forms.Button mappingBT;
        private System.Windows.Forms.Button addActionBT;
        private System.Windows.Forms.Button editActionBT;
        private System.Windows.Forms.Button testActionBT;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndpointAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn MethodeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamDescr;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReturnDescr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}
