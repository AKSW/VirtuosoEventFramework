namespace EventFrameworkControl
{
    partial class EventSetControl
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
            this.eventSetTabControl = new System.Windows.Forms.TabControl();
            this.atomicEventsTab = new System.Windows.Forms.TabPage();
            this.atomicEventsDGV = new System.Windows.Forms.DataGridView();
            this.complexEventsTab = new System.Windows.Forms.TabPage();
            this.complexEventsDGV = new System.Windows.Forms.DataGridView();
            this.selectedEventsDGV = new System.Windows.Forms.DataGridView();
            this.typeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descrColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.maxCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.finishBT = new System.Windows.Forms.Button();
            this.parentSetBT = new System.Windows.Forms.Button();
            this.insertSetsBT = new System.Windows.Forms.Button();
            this.removeSetBT = new System.Windows.Forms.Button();
            this.notCB = new System.Windows.Forms.CheckBox();
            this.xorCB = new System.Windows.Forms.CheckBox();
            this.andCB = new System.Windows.Forms.CheckBox();
            this.orCB = new System.Windows.Forms.CheckBox();
            this.eventSyntaxTB = new System.Windows.Forms.TextBox();
            this.insertMultiSetBT = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cancelBT = new System.Windows.Forms.Button();
            this.eventSetTabControl.SuspendLayout();
            this.atomicEventsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.atomicEventsDGV)).BeginInit();
            this.complexEventsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.complexEventsDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedEventsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // eventSetTabControl
            // 
            this.eventSetTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.eventSetTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventSetTabControl.Controls.Add(this.atomicEventsTab);
            this.eventSetTabControl.Controls.Add(this.complexEventsTab);
            this.eventSetTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventSetTabControl.Location = new System.Drawing.Point(-1, -1);
            this.eventSetTabControl.Multiline = true;
            this.eventSetTabControl.Name = "eventSetTabControl";
            this.eventSetTabControl.Padding = new System.Drawing.Point(70, 5);
            this.eventSetTabControl.SelectedIndex = 0;
            this.eventSetTabControl.Size = new System.Drawing.Size(971, 252);
            this.eventSetTabControl.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.eventSetTabControl.TabIndex = 0;
            this.eventSetTabControl.SelectedIndexChanged += new System.EventHandler(this.eventSetTabControl_SelectedIndexChanged);
            // 
            // atomicEventsTab
            // 
            this.atomicEventsTab.Controls.Add(this.atomicEventsDGV);
            this.atomicEventsTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.atomicEventsTab.Location = new System.Drawing.Point(4, 4);
            this.atomicEventsTab.Name = "atomicEventsTab";
            this.atomicEventsTab.Padding = new System.Windows.Forms.Padding(3);
            this.atomicEventsTab.Size = new System.Drawing.Size(963, 220);
            this.atomicEventsTab.TabIndex = 0;
            this.atomicEventsTab.Text = "         Atomic Events           ";
            this.atomicEventsTab.UseVisualStyleBackColor = true;
            // 
            // atomicEventsDGV
            // 
            this.atomicEventsDGV.AllowUserToAddRows = false;
            this.atomicEventsDGV.AllowUserToDeleteRows = false;
            this.atomicEventsDGV.AllowUserToResizeRows = false;
            this.atomicEventsDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.atomicEventsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.atomicEventsDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.atomicEventsDGV.Location = new System.Drawing.Point(3, 3);
            this.atomicEventsDGV.Name = "atomicEventsDGV";
            this.atomicEventsDGV.RowHeadersVisible = false;
            this.atomicEventsDGV.Size = new System.Drawing.Size(957, 214);
            this.atomicEventsDGV.TabIndex = 0;
            this.atomicEventsDGV.DragOver += new System.Windows.Forms.DragEventHandler(this.dgv_DragOver);
            this.atomicEventsDGV.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseDown);
            this.atomicEventsDGV.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseMove);
            // 
            // complexEventsTab
            // 
            this.complexEventsTab.Controls.Add(this.complexEventsDGV);
            this.complexEventsTab.Location = new System.Drawing.Point(4, 4);
            this.complexEventsTab.Name = "complexEventsTab";
            this.complexEventsTab.Padding = new System.Windows.Forms.Padding(3);
            this.complexEventsTab.Size = new System.Drawing.Size(963, 220);
            this.complexEventsTab.TabIndex = 1;
            this.complexEventsTab.Text = "          Complex Events         ";
            this.complexEventsTab.UseVisualStyleBackColor = true;
            // 
            // complexEventsDGV
            // 
            this.complexEventsDGV.AllowUserToAddRows = false;
            this.complexEventsDGV.AllowUserToDeleteRows = false;
            this.complexEventsDGV.AllowUserToResizeRows = false;
            this.complexEventsDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.complexEventsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.complexEventsDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.complexEventsDGV.Location = new System.Drawing.Point(3, 3);
            this.complexEventsDGV.Name = "complexEventsDGV";
            this.complexEventsDGV.Size = new System.Drawing.Size(957, 214);
            this.complexEventsDGV.TabIndex = 0;
            this.complexEventsDGV.DragOver += new System.Windows.Forms.DragEventHandler(this.dgv_DragOver);
            this.complexEventsDGV.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseDown);
            this.complexEventsDGV.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseMove);
            // 
            // selectedEventsDGV
            // 
            this.selectedEventsDGV.AllowDrop = true;
            this.selectedEventsDGV.AllowUserToAddRows = false;
            this.selectedEventsDGV.AllowUserToOrderColumns = true;
            this.selectedEventsDGV.AllowUserToResizeRows = false;
            this.selectedEventsDGV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedEventsDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.selectedEventsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.selectedEventsDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.typeColumn,
            this.idColumn,
            this.nameColumn,
            this.descrColumn,
            this.minCol,
            this.maxCol});
            this.selectedEventsDGV.Location = new System.Drawing.Point(151, 273);
            this.selectedEventsDGV.Name = "selectedEventsDGV";
            this.selectedEventsDGV.RowHeadersWidth = 10;
            this.selectedEventsDGV.Size = new System.Drawing.Size(553, 219);
            this.selectedEventsDGV.TabIndex = 1;
            this.selectedEventsDGV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.selectedEventsDGV_CellDoubleClick);
            this.selectedEventsDGV.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.selectedEventsDGV_CellValidating);
            this.selectedEventsDGV.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.selectedEventsDGV_EditingControlShowing);
            this.selectedEventsDGV.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.selectedEventsDGV_RowsAdded);
            this.selectedEventsDGV.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgv_DragDrop);
            this.selectedEventsDGV.DragOver += new System.Windows.Forms.DragEventHandler(this.dgv_DragOver);
            // 
            // typeColumn
            // 
            this.typeColumn.HeaderText = "Type";
            this.typeColumn.Name = "typeColumn";
            this.typeColumn.ReadOnly = true;
            this.typeColumn.Width = 56;
            // 
            // idColumn
            // 
            this.idColumn.HeaderText = "ID";
            this.idColumn.Name = "idColumn";
            this.idColumn.ReadOnly = true;
            this.idColumn.Width = 43;
            // 
            // nameColumn
            // 
            this.nameColumn.HeaderText = "Name";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            this.nameColumn.Width = 60;
            // 
            // descrColumn
            // 
            this.descrColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descrColumn.HeaderText = "Description";
            this.descrColumn.Name = "descrColumn";
            this.descrColumn.ReadOnly = true;
            // 
            // minCol
            // 
            this.minCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.minCol.HeaderText = "min Cardinality";
            this.minCol.Name = "minCol";
            this.minCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.minCol.Visible = false;
            // 
            // maxCol
            // 
            this.maxCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.maxCol.HeaderText = "max. Cardinality";
            this.maxCol.Name = "maxCol";
            this.maxCol.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 254);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "drag events or sets into the table below:";
            // 
            // finishBT
            // 
            this.finishBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.finishBT.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.finishBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finishBT.Location = new System.Drawing.Point(837, 485);
            this.finishBT.Name = "finishBT";
            this.finishBT.Size = new System.Drawing.Size(126, 24);
            this.finishBT.TabIndex = 8;
            this.finishBT.Text = "finish";
            this.finishBT.UseVisualStyleBackColor = false;
            this.finishBT.Click += new System.EventHandler(this.finishBT_Click);
            // 
            // parentSetBT
            // 
            this.parentSetBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.parentSetBT.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.parentSetBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parentSetBT.Location = new System.Drawing.Point(710, 449);
            this.parentSetBT.Name = "parentSetBT";
            this.parentSetBT.Size = new System.Drawing.Size(253, 30);
            this.parentSetBT.TabIndex = 10;
            this.parentSetBT.Text = "back to parent-set";
            this.parentSetBT.UseVisualStyleBackColor = false;
            this.parentSetBT.Click += new System.EventHandler(this.parentSetBT_Click);
            // 
            // insertSetsBT
            // 
            this.insertSetsBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.insertSetsBT.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.insertSetsBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.insertSetsBT.Location = new System.Drawing.Point(710, 315);
            this.insertSetsBT.Name = "insertSetsBT";
            this.insertSetsBT.Size = new System.Drawing.Size(167, 29);
            this.insertSetsBT.TabIndex = 11;
            this.insertSetsBT.Text = "insert new Event Set";
            this.insertSetsBT.UseVisualStyleBackColor = false;
            this.insertSetsBT.Click += new System.EventHandler(this.insertSetsBT_Click);
            // 
            // removeSetBT
            // 
            this.removeSetBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeSetBT.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.removeSetBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeSetBT.Location = new System.Drawing.Point(710, 281);
            this.removeSetBT.Name = "removeSetBT";
            this.removeSetBT.Size = new System.Drawing.Size(167, 28);
            this.removeSetBT.TabIndex = 12;
            this.removeSetBT.Text = "remove item";
            this.removeSetBT.UseVisualStyleBackColor = false;
            this.removeSetBT.Click += new System.EventHandler(this.removeSetBT_Click);
            // 
            // notCB
            // 
            this.notCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.notCB.Appearance = System.Windows.Forms.Appearance.Button;
            this.notCB.AutoSize = true;
            this.notCB.Checked = true;
            this.notCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.notCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notCB.Image = global::EventFrameworkControl.Properties.Resources.button;
            this.notCB.Location = new System.Drawing.Point(4, 458);
            this.notCB.Name = "notCB";
            this.notCB.Size = new System.Drawing.Size(141, 51);
            this.notCB.TabIndex = 16;
            this.notCB.Text = "NOT";
            this.notCB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.notCB.UseVisualStyleBackColor = true;
            this.notCB.Click += new System.EventHandler(this.notCB_Click);
            // 
            // xorCB
            // 
            this.xorCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xorCB.Appearance = System.Windows.Forms.Appearance.Button;
            this.xorCB.AutoSize = true;
            this.xorCB.Checked = true;
            this.xorCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xorCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xorCB.Image = global::EventFrameworkControl.Properties.Resources.button;
            this.xorCB.Location = new System.Drawing.Point(4, 398);
            this.xorCB.Name = "xorCB";
            this.xorCB.Size = new System.Drawing.Size(141, 51);
            this.xorCB.TabIndex = 15;
            this.xorCB.Text = "XOR";
            this.xorCB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.xorCB.UseVisualStyleBackColor = true;
            this.xorCB.Click += new System.EventHandler(this.xorCB_Click);
            // 
            // andCB
            // 
            this.andCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.andCB.Appearance = System.Windows.Forms.Appearance.Button;
            this.andCB.AutoSize = true;
            this.andCB.Checked = true;
            this.andCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.andCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.andCB.Image = global::EventFrameworkControl.Properties.Resources.button;
            this.andCB.Location = new System.Drawing.Point(4, 335);
            this.andCB.Name = "andCB";
            this.andCB.Size = new System.Drawing.Size(141, 51);
            this.andCB.TabIndex = 14;
            this.andCB.Text = "AND";
            this.andCB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.andCB.UseVisualStyleBackColor = true;
            this.andCB.Click += new System.EventHandler(this.andCB_Click);
            // 
            // orCB
            // 
            this.orCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.orCB.Appearance = System.Windows.Forms.Appearance.Button;
            this.orCB.AutoSize = true;
            this.orCB.Checked = true;
            this.orCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.orCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orCB.Image = global::EventFrameworkControl.Properties.Resources.button;
            this.orCB.Location = new System.Drawing.Point(4, 273);
            this.orCB.Name = "orCB";
            this.orCB.Size = new System.Drawing.Size(141, 51);
            this.orCB.TabIndex = 13;
            this.orCB.Text = "OR";
            this.orCB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.orCB.UseVisualStyleBackColor = true;
            this.orCB.Click += new System.EventHandler(this.orCB_Click);
            // 
            // eventSyntaxTB
            // 
            this.eventSyntaxTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventSyntaxTB.Location = new System.Drawing.Point(151, 492);
            this.eventSyntaxTB.Name = "eventSyntaxTB";
            this.eventSyntaxTB.Size = new System.Drawing.Size(553, 20);
            this.eventSyntaxTB.TabIndex = 18;
            // 
            // insertMultiSetBT
            // 
            this.insertMultiSetBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.insertMultiSetBT.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.insertMultiSetBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.insertMultiSetBT.Location = new System.Drawing.Point(710, 351);
            this.insertMultiSetBT.Name = "insertMultiSetBT";
            this.insertMultiSetBT.Size = new System.Drawing.Size(167, 26);
            this.insertMultiSetBT.TabIndex = 19;
            this.insertMultiSetBT.Text = "insert a Multi-Set";
            this.insertMultiSetBT.UseVisualStyleBackColor = false;
            this.insertMultiSetBT.Click += new System.EventHandler(this.insertMultiSetBT_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(449, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(255, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "* this ID is preliminary and will different after reloading";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(710, 417);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(190, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "edit EventSets by double-clicking them";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(710, 430);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(247, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "use the buttom below to navigate to the Parent-Set";
            // 
            // cancelBT
            // 
            this.cancelBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBT.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.cancelBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBT.Location = new System.Drawing.Point(710, 485);
            this.cancelBT.Name = "cancelBT";
            this.cancelBT.Size = new System.Drawing.Size(121, 24);
            this.cancelBT.TabIndex = 23;
            this.cancelBT.Text = "cancel";
            this.cancelBT.UseVisualStyleBackColor = false;
            this.cancelBT.Click += new System.EventHandler(this.cancelBT_Click);
            // 
            // EventSetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cancelBT);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.insertMultiSetBT);
            this.Controls.Add(this.eventSyntaxTB);
            this.Controls.Add(this.notCB);
            this.Controls.Add(this.xorCB);
            this.Controls.Add(this.andCB);
            this.Controls.Add(this.orCB);
            this.Controls.Add(this.removeSetBT);
            this.Controls.Add(this.insertSetsBT);
            this.Controls.Add(this.parentSetBT);
            this.Controls.Add(this.finishBT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.selectedEventsDGV);
            this.Controls.Add(this.eventSetTabControl);
            this.Name = "EventSetControl";
            this.Size = new System.Drawing.Size(969, 512);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.dgv_DragOver);
            this.eventSetTabControl.ResumeLayout(false);
            this.atomicEventsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.atomicEventsDGV)).EndInit();
            this.complexEventsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.complexEventsDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedEventsDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl eventSetTabControl;
        private System.Windows.Forms.TabPage atomicEventsTab;
        private System.Windows.Forms.DataGridView atomicEventsDGV;
        private System.Windows.Forms.TabPage complexEventsTab;
        private System.Windows.Forms.DataGridView complexEventsDGV;
        private System.Windows.Forms.DataGridView selectedEventsDGV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button finishBT;
        private System.Windows.Forms.Button parentSetBT;
        private System.Windows.Forms.Button insertSetsBT;
        private System.Windows.Forms.Button removeSetBT;
        private System.Windows.Forms.CheckBox orCB;
        private System.Windows.Forms.CheckBox andCB;
        private System.Windows.Forms.CheckBox xorCB;
        private System.Windows.Forms.CheckBox notCB;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descrColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn minCol;
        private System.Windows.Forms.DataGridViewComboBoxColumn maxCol;
        private System.Windows.Forms.Button insertMultiSetBT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cancelBT;
    }
}
