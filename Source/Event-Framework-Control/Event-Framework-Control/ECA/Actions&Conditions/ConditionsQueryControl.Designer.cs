namespace EventFrameworkControl
{
    partial class ConditionsQueryControl
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
            this.mappingBT = new System.Windows.Forms.Button();
            this.removeBT = new System.Windows.Forms.Button();
            this.confirmBT = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.selectedConditionDGV = new System.Windows.Forms.DataGridView();
            this.orderCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndpointAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MethodeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParamDescr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReturnDescr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conditionsDGV = new System.Windows.Forms.DataGridView();
            this.label16 = new System.Windows.Forms.Label();
            this.testConditionBT = new System.Windows.Forms.Button();
            this.editConditionBT = new System.Windows.Forms.Button();
            this.addConditionBT = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.selectedConditionDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.conditionsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // mappingBT
            // 
            this.mappingBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mappingBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mappingBT.Location = new System.Drawing.Point(761, 348);
            this.mappingBT.Name = "mappingBT";
            this.mappingBT.Size = new System.Drawing.Size(151, 43);
            this.mappingBT.TabIndex = 39;
            this.mappingBT.Text = "define parameter-mapping";
            this.mappingBT.UseVisualStyleBackColor = true;
            this.mappingBT.Click += new System.EventHandler(this.mappingBT_Click);
            // 
            // removeBT
            // 
            this.removeBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeBT.Location = new System.Drawing.Point(761, 297);
            this.removeBT.Name = "removeBT";
            this.removeBT.Size = new System.Drawing.Size(151, 45);
            this.removeBT.TabIndex = 38;
            this.removeBT.Text = "remove selected condition";
            this.removeBT.UseVisualStyleBackColor = true;
            this.removeBT.Click += new System.EventHandler(this.removeBT_Click);
            // 
            // confirmBT
            // 
            this.confirmBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmBT.Location = new System.Drawing.Point(761, 439);
            this.confirmBT.Name = "confirmBT";
            this.confirmBT.Size = new System.Drawing.Size(151, 43);
            this.confirmBT.TabIndex = 37;
            this.confirmBT.Text = "confirm";
            this.confirmBT.UseVisualStyleBackColor = true;
            this.confirmBT.Click += new System.EventHandler(this.confirmBT_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(405, 16);
            this.label1.TabIndex = 36;
            this.label1.Text = "Drag conditions from the table above into the table below:";
            // 
            // selectedConditionDGV
            // 
            this.selectedConditionDGV.AllowDrop = true;
            this.selectedConditionDGV.AllowUserToAddRows = false;
            this.selectedConditionDGV.AllowUserToDeleteRows = false;
            this.selectedConditionDGV.AllowUserToResizeRows = false;
            this.selectedConditionDGV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedConditionDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.selectedConditionDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.selectedConditionDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.orderCol,
            this.ActionID,
            this.EndpointAddress,
            this.MethodeName,
            this.ParamDescr,
            this.ReturnDescr,
            this.Description});
            this.selectedConditionDGV.Location = new System.Drawing.Point(0, 297);
            this.selectedConditionDGV.Name = "selectedConditionDGV";
            this.selectedConditionDGV.RowHeadersWidth = 10;
            this.selectedConditionDGV.Size = new System.Drawing.Size(755, 188);
            this.selectedConditionDGV.TabIndex = 35;
            this.selectedConditionDGV.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgv_DragDrop);
            this.selectedConditionDGV.DragOver += new System.Windows.Forms.DragEventHandler(this.dgv_DragOver);
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
            // conditionsDGV
            // 
            this.conditionsDGV.AllowDrop = true;
            this.conditionsDGV.AllowUserToAddRows = false;
            this.conditionsDGV.AllowUserToDeleteRows = false;
            this.conditionsDGV.AllowUserToResizeRows = false;
            this.conditionsDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.conditionsDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.conditionsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conditionsDGV.Location = new System.Drawing.Point(0, 25);
            this.conditionsDGV.Name = "conditionsDGV";
            this.conditionsDGV.RowHeadersWidth = 10;
            this.conditionsDGV.Size = new System.Drawing.Size(755, 250);
            this.conditionsDGV.TabIndex = 34;
            this.conditionsDGV.DragOver += new System.Windows.Forms.DragEventHandler(this.dgv_DragOver);
            this.conditionsDGV.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseDown);
            this.conditionsDGV.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseMove);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 5);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(443, 16);
            this.label16.TabIndex = 33;
            this.label16.Text = "Select conditions which have to be met in addition to all events:";
            // 
            // testConditionBT
            // 
            this.testConditionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testConditionBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testConditionBT.Location = new System.Drawing.Point(761, 127);
            this.testConditionBT.Name = "testConditionBT";
            this.testConditionBT.Size = new System.Drawing.Size(151, 45);
            this.testConditionBT.TabIndex = 42;
            this.testConditionBT.Text = "test selected condition";
            this.testConditionBT.UseVisualStyleBackColor = true;
            this.testConditionBT.Click += new System.EventHandler(this.testConditionBT_Click);
            // 
            // editConditionBT
            // 
            this.editConditionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editConditionBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editConditionBT.Location = new System.Drawing.Point(761, 76);
            this.editConditionBT.Name = "editConditionBT";
            this.editConditionBT.Size = new System.Drawing.Size(151, 45);
            this.editConditionBT.TabIndex = 41;
            this.editConditionBT.Text = "edit selected condition";
            this.editConditionBT.UseVisualStyleBackColor = true;
            this.editConditionBT.Click += new System.EventHandler(this.editConditionBT_Click);
            // 
            // addConditionBT
            // 
            this.addConditionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addConditionBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addConditionBT.Location = new System.Drawing.Point(761, 25);
            this.addConditionBT.Name = "addConditionBT";
            this.addConditionBT.Size = new System.Drawing.Size(151, 45);
            this.addConditionBT.TabIndex = 40;
            this.addConditionBT.Text = "add a new condition";
            this.addConditionBT.UseVisualStyleBackColor = true;
            this.addConditionBT.Click += new System.EventHandler(this.addConditionBT_Click);
            // 
            // ConditionsQueryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.testConditionBT);
            this.Controls.Add(this.editConditionBT);
            this.Controls.Add(this.addConditionBT);
            this.Controls.Add(this.mappingBT);
            this.Controls.Add(this.removeBT);
            this.Controls.Add(this.confirmBT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectedConditionDGV);
            this.Controls.Add(this.conditionsDGV);
            this.Controls.Add(this.label16);
            this.Name = "ConditionsQueryControl";
            this.Size = new System.Drawing.Size(916, 485);
            ((System.ComponentModel.ISupportInitialize)(this.selectedConditionDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.conditionsDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mappingBT;
        private System.Windows.Forms.Button removeBT;
        private System.Windows.Forms.Button confirmBT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView selectedConditionDGV;
        private System.Windows.Forms.DataGridView conditionsDGV;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button testConditionBT;
        private System.Windows.Forms.Button editConditionBT;
        private System.Windows.Forms.Button addConditionBT;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndpointAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn MethodeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParamDescr;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReturnDescr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}
