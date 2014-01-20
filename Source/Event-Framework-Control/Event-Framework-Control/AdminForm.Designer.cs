namespace EventFrameworkControl
{
    partial class AdminForm
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
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.LogInTab = new System.Windows.Forms.TabPage();
            this.tripleDGV = new System.Windows.Forms.DataGridView();
            this.loginBT = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.usernameTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UserAccTab = new System.Windows.Forms.TabPage();
            this.deleteAccBT = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.assignDatabasesDGV = new System.Windows.Forms.DataGridView();
            this.saveChangesBT = new System.Windows.Forms.Button();
            this.resetPassBT = new System.Windows.Forms.Button();
            this.newAccBT = new System.Windows.Forms.Button();
            this.refreshBT = new System.Windows.Forms.Button();
            this.usersDGV = new System.Windows.Forms.DataGridView();
            this.databaseTab = new System.Windows.Forms.TabPage();
            this.deactivateDbBT = new System.Windows.Forms.Button();
            this.deleteDsBT = new System.Windows.Forms.Button();
            this.datasourcesTC = new System.Windows.Forms.TabControl();
            this.regDBTab = new System.Windows.Forms.TabPage();
            this.dbNameTB = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dbTypeCB = new System.Windows.Forms.ComboBox();
            this.remoteEndpointTB = new System.Windows.Forms.TextBox();
            this.submitNewDbBT = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.dbDescribtionRTB = new System.Windows.Forms.RichTextBox();
            this.sparqlEndpointTB = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.regDSTab = new System.Windows.Forms.TabPage();
            this.dsNameTB = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.submitNewDsBT = new System.Windows.Forms.Button();
            this.descriptionRTB = new System.Windows.Forms.RichTextBox();
            this.sparqlAddressTB = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.databaseRefreshBT = new System.Windows.Forms.Button();
            this.datasourceDGV = new System.Windows.Forms.DataGridView();
            this.EcaDefTab = new System.Windows.Forms.TabPage();
            this.atomicEventTC = new System.Windows.Forms.TabControl();
            this.atomicEventsTab = new System.Windows.Forms.TabPage();
            this.actionsTab = new System.Windows.Forms.TabPage();
            this.conditionsTab = new System.Windows.Forms.TabPage();
            this.complexEventTab = new System.Windows.Forms.TabPage();
            this.instancesTab = new System.Windows.Forms.TabPage();
            this.ecaDefPanel = new System.Windows.Forms.Panel();
            this.mainTabControl.SuspendLayout();
            this.LogInTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tripleDGV)).BeginInit();
            this.UserAccTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.assignDatabasesDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersDGV)).BeginInit();
            this.databaseTab.SuspendLayout();
            this.datasourcesTC.SuspendLayout();
            this.regDBTab.SuspendLayout();
            this.regDSTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datasourceDGV)).BeginInit();
            this.EcaDefTab.SuspendLayout();
            this.atomicEventTC.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.LogInTab);
            this.mainTabControl.Controls.Add(this.UserAccTab);
            this.mainTabControl.Controls.Add(this.databaseTab);
            this.mainTabControl.Controls.Add(this.EcaDefTab);
            this.mainTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabControl.Location = new System.Drawing.Point(0, 2);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.Padding = new System.Drawing.Point(50, 6);
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1151, 709);
            this.mainTabControl.TabIndex = 1;
            this.mainTabControl.SelectedIndexChanged += new System.EventHandler(this.mainTabControl_SelectedIndexChanged);
            // 
            // LogInTab
            // 
            this.LogInTab.Controls.Add(this.tripleDGV);
            this.LogInTab.Controls.Add(this.loginBT);
            this.LogInTab.Controls.Add(this.label3);
            this.LogInTab.Controls.Add(this.label2);
            this.LogInTab.Controls.Add(this.passwordTB);
            this.LogInTab.Controls.Add(this.usernameTB);
            this.LogInTab.Controls.Add(this.label1);
            this.LogInTab.Location = new System.Drawing.Point(4, 30);
            this.LogInTab.Name = "LogInTab";
            this.LogInTab.Padding = new System.Windows.Forms.Padding(3);
            this.LogInTab.Size = new System.Drawing.Size(1143, 675);
            this.LogInTab.TabIndex = 0;
            this.LogInTab.Text = "           LogIn            ";
            this.LogInTab.UseVisualStyleBackColor = true;
            // 
            // tripleDGV
            // 
            this.tripleDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tripleDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tripleDGV.Location = new System.Drawing.Point(236, 3);
            this.tripleDGV.Name = "tripleDGV";
            this.tripleDGV.RowHeadersVisible = false;
            this.tripleDGV.Size = new System.Drawing.Size(817, 819);
            this.tripleDGV.TabIndex = 9;
            // 
            // loginBT
            // 
            this.loginBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginBT.Location = new System.Drawing.Point(155, 101);
            this.loginBT.Name = "loginBT";
            this.loginBT.Size = new System.Drawing.Size(75, 23);
            this.loginBT.TabIndex = 5;
            this.loginBT.Text = "Log In";
            this.loginBT.UseVisualStyleBackColor = true;
            this.loginBT.Click += new System.EventHandler(this.loginBT_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Username:";
            // 
            // passwordTB
            // 
            this.passwordTB.Location = new System.Drawing.Point(92, 75);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.PasswordChar = '*';
            this.passwordTB.Size = new System.Drawing.Size(138, 21);
            this.passwordTB.TabIndex = 2;
            // 
            // usernameTB
            // 
            this.usernameTB.Location = new System.Drawing.Point(92, 48);
            this.usernameTB.Name = "usernameTB";
            this.usernameTB.Size = new System.Drawing.Size(138, 21);
            this.usernameTB.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Log In:";
            // 
            // UserAccTab
            // 
            this.UserAccTab.Controls.Add(this.deleteAccBT);
            this.UserAccTab.Controls.Add(this.label4);
            this.UserAccTab.Controls.Add(this.assignDatabasesDGV);
            this.UserAccTab.Controls.Add(this.saveChangesBT);
            this.UserAccTab.Controls.Add(this.resetPassBT);
            this.UserAccTab.Controls.Add(this.newAccBT);
            this.UserAccTab.Controls.Add(this.refreshBT);
            this.UserAccTab.Controls.Add(this.usersDGV);
            this.UserAccTab.Location = new System.Drawing.Point(4, 30);
            this.UserAccTab.Name = "UserAccTab";
            this.UserAccTab.Padding = new System.Windows.Forms.Padding(3);
            this.UserAccTab.Size = new System.Drawing.Size(1143, 675);
            this.UserAccTab.TabIndex = 1;
            this.UserAccTab.Text = "     User Accounts     ";
            this.UserAccTab.UseVisualStyleBackColor = true;
            // 
            // deleteAccBT
            // 
            this.deleteAccBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteAccBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.deleteAccBT.Location = new System.Drawing.Point(3, 598);
            this.deleteAccBT.Name = "deleteAccBT";
            this.deleteAccBT.Size = new System.Drawing.Size(131, 35);
            this.deleteAccBT.TabIndex = 12;
            this.deleteAccBT.Text = "delete account";
            this.deleteAccBT.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(389, 500);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "assign rights for datasources:";
            // 
            // assignDatabasesDGV
            // 
            this.assignDatabasesDGV.AllowDrop = true;
            this.assignDatabasesDGV.AllowUserToAddRows = false;
            this.assignDatabasesDGV.AllowUserToDeleteRows = false;
            this.assignDatabasesDGV.AllowUserToResizeRows = false;
            this.assignDatabasesDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.assignDatabasesDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.assignDatabasesDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.assignDatabasesDGV.Location = new System.Drawing.Point(392, 516);
            this.assignDatabasesDGV.Name = "assignDatabasesDGV";
            this.assignDatabasesDGV.RowHeadersVisible = false;
            this.assignDatabasesDGV.Size = new System.Drawing.Size(645, 159);
            this.assignDatabasesDGV.TabIndex = 10;
            // 
            // saveChangesBT
            // 
            this.saveChangesBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveChangesBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveChangesBT.Location = new System.Drawing.Point(3, 639);
            this.saveChangesBT.Name = "saveChangesBT";
            this.saveChangesBT.Size = new System.Drawing.Size(131, 35);
            this.saveChangesBT.TabIndex = 9;
            this.saveChangesBT.Text = "save all changes";
            this.saveChangesBT.UseVisualStyleBackColor = true;
            this.saveChangesBT.Click += new System.EventHandler(this.changeDatabasesBT_Click);
            // 
            // resetPassBT
            // 
            this.resetPassBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resetPassBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.resetPassBT.Location = new System.Drawing.Point(3, 557);
            this.resetPassBT.Name = "resetPassBT";
            this.resetPassBT.Size = new System.Drawing.Size(131, 35);
            this.resetPassBT.TabIndex = 5;
            this.resetPassBT.Text = "reset password";
            this.resetPassBT.UseVisualStyleBackColor = true;
            this.resetPassBT.Click += new System.EventHandler(this.resetPassBT_Click);
            // 
            // newAccBT
            // 
            this.newAccBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.newAccBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.newAccBT.Location = new System.Drawing.Point(3, 516);
            this.newAccBT.Name = "newAccBT";
            this.newAccBT.Size = new System.Drawing.Size(131, 35);
            this.newAccBT.TabIndex = 2;
            this.newAccBT.Text = "add new account";
            this.newAccBT.UseVisualStyleBackColor = true;
            this.newAccBT.Click += new System.EventHandler(this.newAccBT_Click);
            // 
            // refreshBT
            // 
            this.refreshBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.refreshBT.Location = new System.Drawing.Point(1043, 517);
            this.refreshBT.Name = "refreshBT";
            this.refreshBT.Size = new System.Drawing.Size(98, 35);
            this.refreshBT.TabIndex = 1;
            this.refreshBT.Text = "refresh";
            this.refreshBT.UseVisualStyleBackColor = true;
            this.refreshBT.Click += new System.EventHandler(this.refreshBT_Click);
            // 
            // usersDGV
            // 
            this.usersDGV.AllowUserToAddRows = false;
            this.usersDGV.AllowUserToDeleteRows = false;
            this.usersDGV.AllowUserToResizeColumns = false;
            this.usersDGV.AllowUserToResizeRows = false;
            this.usersDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usersDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.usersDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.usersDGV.Location = new System.Drawing.Point(2, 2);
            this.usersDGV.Name = "usersDGV";
            this.usersDGV.RowHeadersWidth = 10;
            this.usersDGV.Size = new System.Drawing.Size(1141, 495);
            this.usersDGV.TabIndex = 0;
            this.usersDGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.usersDGV_CellEndEdit);
            this.usersDGV.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.usersDGV_CellMouseDown);
            // 
            // databaseTab
            // 
            this.databaseTab.Controls.Add(this.deactivateDbBT);
            this.databaseTab.Controls.Add(this.deleteDsBT);
            this.databaseTab.Controls.Add(this.datasourcesTC);
            this.databaseTab.Controls.Add(this.label13);
            this.databaseTab.Controls.Add(this.databaseRefreshBT);
            this.databaseTab.Controls.Add(this.datasourceDGV);
            this.databaseTab.Location = new System.Drawing.Point(4, 30);
            this.databaseTab.Name = "databaseTab";
            this.databaseTab.Padding = new System.Windows.Forms.Padding(3);
            this.databaseTab.Size = new System.Drawing.Size(1143, 675);
            this.databaseTab.TabIndex = 4;
            this.databaseTab.Text = "         Event- & Datasources         ";
            this.databaseTab.UseVisualStyleBackColor = true;
            // 
            // deactivateDbBT
            // 
            this.deactivateDbBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deactivateDbBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.deactivateDbBT.Location = new System.Drawing.Point(179, 646);
            this.deactivateDbBT.Name = "deactivateDbBT";
            this.deactivateDbBT.Size = new System.Drawing.Size(233, 26);
            this.deactivateDbBT.TabIndex = 24;
            this.deactivateDbBT.Text = "deactivate selected database";
            this.deactivateDbBT.UseVisualStyleBackColor = true;
            this.deactivateDbBT.Click += new System.EventHandler(this.deactivateDbBT_Click);
            // 
            // deleteDsBT
            // 
            this.deleteDsBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteDsBT.Location = new System.Drawing.Point(3, 646);
            this.deleteDsBT.Name = "deleteDsBT";
            this.deleteDsBT.Size = new System.Drawing.Size(170, 26);
            this.deleteDsBT.TabIndex = 23;
            this.deleteDsBT.Text = "delete datasource";
            this.deleteDsBT.UseVisualStyleBackColor = true;
            this.deleteDsBT.Click += new System.EventHandler(this.deleteDsBT_Click);
            // 
            // datasourcesTC
            // 
            this.datasourcesTC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datasourcesTC.Controls.Add(this.regDBTab);
            this.datasourcesTC.Controls.Add(this.regDSTab);
            this.datasourcesTC.Location = new System.Drawing.Point(749, 0);
            this.datasourcesTC.Name = "datasourcesTC";
            this.datasourcesTC.SelectedIndex = 0;
            this.datasourcesTC.Size = new System.Drawing.Size(391, 675);
            this.datasourcesTC.TabIndex = 22;
            // 
            // regDBTab
            // 
            this.regDBTab.Controls.Add(this.dbNameTB);
            this.regDBTab.Controls.Add(this.label15);
            this.regDBTab.Controls.Add(this.label7);
            this.regDBTab.Controls.Add(this.dbTypeCB);
            this.regDBTab.Controls.Add(this.remoteEndpointTB);
            this.regDBTab.Controls.Add(this.submitNewDbBT);
            this.regDBTab.Controls.Add(this.label8);
            this.regDBTab.Controls.Add(this.dbDescribtionRTB);
            this.regDBTab.Controls.Add(this.sparqlEndpointTB);
            this.regDBTab.Controls.Add(this.label12);
            this.regDBTab.Controls.Add(this.label9);
            this.regDBTab.Location = new System.Drawing.Point(4, 24);
            this.regDBTab.Name = "regDBTab";
            this.regDBTab.Padding = new System.Windows.Forms.Padding(3);
            this.regDBTab.Size = new System.Drawing.Size(383, 653);
            this.regDBTab.TabIndex = 0;
            this.regDBTab.Text = "register new satellite-database";
            this.regDBTab.UseVisualStyleBackColor = true;
            // 
            // dbNameTB
            // 
            this.dbNameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbNameTB.Location = new System.Drawing.Point(6, 75);
            this.dbNameTB.Name = "dbNameTB";
            this.dbNameTB.Size = new System.Drawing.Size(375, 21);
            this.dbNameTB.TabIndex = 19;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(4, 119);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(339, 16);
            this.label15.TabIndex = 20;
            this.label15.Text = "insert the endpoint address of the new database";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(209, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "choose the type of database:";
            // 
            // dbTypeCB
            // 
            this.dbTypeCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbTypeCB.FormattingEnabled = true;
            this.dbTypeCB.Location = new System.Drawing.Point(6, 23);
            this.dbTypeCB.Name = "dbTypeCB";
            this.dbTypeCB.Size = new System.Drawing.Size(375, 23);
            this.dbTypeCB.TabIndex = 4;
            // 
            // remoteEndpointTB
            // 
            this.remoteEndpointTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remoteEndpointTB.Location = new System.Drawing.Point(4, 138);
            this.remoteEndpointTB.Name = "remoteEndpointTB";
            this.remoteEndpointTB.Size = new System.Drawing.Size(377, 21);
            this.remoteEndpointTB.TabIndex = 9;
            // 
            // submitNewDbBT
            // 
            this.submitNewDbBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.submitNewDbBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitNewDbBT.Location = new System.Drawing.Point(210, 622);
            this.submitNewDbBT.Name = "submitNewDbBT";
            this.submitNewDbBT.Size = new System.Drawing.Size(170, 28);
            this.submitNewDbBT.TabIndex = 18;
            this.submitNewDbBT.Text = "submit new database";
            this.submitNewDbBT.UseVisualStyleBackColor = true;
            this.submitNewDbBT.Click += new System.EventHandler(this.button3_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(218, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "enter a name for this database";
            // 
            // dbDescribtionRTB
            // 
            this.dbDescribtionRTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dbDescribtionRTB.Location = new System.Drawing.Point(5, 189);
            this.dbDescribtionRTB.Name = "dbDescribtionRTB";
            this.dbDescribtionRTB.Size = new System.Drawing.Size(376, 71);
            this.dbDescribtionRTB.TabIndex = 17;
            this.dbDescribtionRTB.Text = "";
            // 
            // sparqlEndpointTB
            // 
            this.sparqlEndpointTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sparqlEndpointTB.Location = new System.Drawing.Point(6, 294);
            this.sparqlEndpointTB.Name = "sparqlEndpointTB";
            this.sparqlEndpointTB.Size = new System.Drawing.Size(377, 21);
            this.sparqlEndpointTB.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 170);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(290, 16);
            this.label12.TabIndex = 16;
            this.label12.Text = "enter a short description of this database";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 275);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(305, 15);
            this.label9.TabIndex = 12;
            this.label9.Text = "enter the SPARQL-entpoint-address: (optional)";
            // 
            // regDSTab
            // 
            this.regDSTab.Controls.Add(this.dsNameTB);
            this.regDSTab.Controls.Add(this.label20);
            this.regDSTab.Controls.Add(this.submitNewDsBT);
            this.regDSTab.Controls.Add(this.descriptionRTB);
            this.regDSTab.Controls.Add(this.sparqlAddressTB);
            this.regDSTab.Controls.Add(this.label16);
            this.regDSTab.Controls.Add(this.label17);
            this.regDSTab.Location = new System.Drawing.Point(4, 24);
            this.regDSTab.Name = "regDSTab";
            this.regDSTab.Padding = new System.Windows.Forms.Padding(3);
            this.regDSTab.Size = new System.Drawing.Size(383, 647);
            this.regDSTab.TabIndex = 1;
            this.regDSTab.Text = "register external event source";
            this.regDSTab.UseVisualStyleBackColor = true;
            // 
            // dsNameTB
            // 
            this.dsNameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dsNameTB.Location = new System.Drawing.Point(6, 33);
            this.dsNameTB.Name = "dsNameTB";
            this.dsNameTB.Size = new System.Drawing.Size(371, 21);
            this.dsNameTB.TabIndex = 37;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(3, 14);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(98, 16);
            this.label20.TabIndex = 36;
            this.label20.Text = "enter a name";
            // 
            // submitNewDsBT
            // 
            this.submitNewDsBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.submitNewDsBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitNewDsBT.Location = new System.Drawing.Point(186, 622);
            this.submitNewDsBT.Name = "submitNewDsBT";
            this.submitNewDsBT.Size = new System.Drawing.Size(197, 28);
            this.submitNewDsBT.TabIndex = 33;
            this.submitNewDsBT.Text = "register new event source";
            this.submitNewDsBT.UseVisualStyleBackColor = true;
            this.submitNewDsBT.Click += new System.EventHandler(this.submitNewDsBT_Click);
            // 
            // descriptionRTB
            // 
            this.descriptionRTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionRTB.Location = new System.Drawing.Point(6, 103);
            this.descriptionRTB.Name = "descriptionRTB";
            this.descriptionRTB.Size = new System.Drawing.Size(371, 71);
            this.descriptionRTB.TabIndex = 32;
            this.descriptionRTB.Text = "";
            // 
            // sparqlAddressTB
            // 
            this.sparqlAddressTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sparqlAddressTB.Location = new System.Drawing.Point(6, 216);
            this.sparqlAddressTB.Name = "sparqlAddressTB";
            this.sparqlAddressTB.Size = new System.Drawing.Size(371, 21);
            this.sparqlAddressTB.TabIndex = 26;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 84);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(313, 16);
            this.label16.TabIndex = 31;
            this.label16.Text = "enter a short description of this event source";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(4, 198);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(338, 15);
            this.label17.TabIndex = 27;
            this.label17.Text = "enter the SPARQL-entpoint-address here: (optional)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(3, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(162, 16);
            this.label13.TabIndex = 19;
            this.label13.Text = "knowen event-sources";
            // 
            // databaseRefreshBT
            // 
            this.databaseRefreshBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.databaseRefreshBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.databaseRefreshBT.Location = new System.Drawing.Point(550, 5);
            this.databaseRefreshBT.Name = "databaseRefreshBT";
            this.databaseRefreshBT.Size = new System.Drawing.Size(184, 23);
            this.databaseRefreshBT.TabIndex = 1;
            this.databaseRefreshBT.Text = "refresh";
            this.databaseRefreshBT.UseVisualStyleBackColor = true;
            this.databaseRefreshBT.Click += new System.EventHandler(this.databaseRefreshBT_Click);
            // 
            // datasourceDGV
            // 
            this.datasourceDGV.AllowUserToAddRows = false;
            this.datasourceDGV.AllowUserToDeleteRows = false;
            this.datasourceDGV.AllowUserToResizeRows = false;
            this.datasourceDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datasourceDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.datasourceDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datasourceDGV.Location = new System.Drawing.Point(0, 34);
            this.datasourceDGV.Name = "datasourceDGV";
            this.datasourceDGV.RowHeadersVisible = false;
            this.datasourceDGV.Size = new System.Drawing.Size(747, 606);
            this.datasourceDGV.TabIndex = 0;
            this.datasourceDGV.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.datasourceDGV_CellMouseClick);
            // 
            // EcaDefTab
            // 
            this.EcaDefTab.Controls.Add(this.atomicEventTC);
            this.EcaDefTab.Controls.Add(this.ecaDefPanel);
            this.EcaDefTab.Location = new System.Drawing.Point(4, 30);
            this.EcaDefTab.Name = "EcaDefTab";
            this.EcaDefTab.Padding = new System.Windows.Forms.Padding(3);
            this.EcaDefTab.Size = new System.Drawing.Size(1143, 675);
            this.EcaDefTab.TabIndex = 2;
            this.EcaDefTab.Text = "           ECA-Rules             ";
            this.EcaDefTab.UseVisualStyleBackColor = true;
            // 
            // atomicEventTC
            // 
            this.atomicEventTC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.atomicEventTC.Controls.Add(this.atomicEventsTab);
            this.atomicEventTC.Controls.Add(this.actionsTab);
            this.atomicEventTC.Controls.Add(this.conditionsTab);
            this.atomicEventTC.Controls.Add(this.complexEventTab);
            this.atomicEventTC.Controls.Add(this.instancesTab);
            this.atomicEventTC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.atomicEventTC.Location = new System.Drawing.Point(-4, 3);
            this.atomicEventTC.Name = "atomicEventTC";
            this.atomicEventTC.Padding = new System.Drawing.Point(30, 6);
            this.atomicEventTC.SelectedIndex = 0;
            this.atomicEventTC.Size = new System.Drawing.Size(1151, 677);
            this.atomicEventTC.TabIndex = 1;
            // 
            // atomicEventsTab
            // 
            this.atomicEventsTab.Location = new System.Drawing.Point(4, 28);
            this.atomicEventsTab.Name = "atomicEventsTab";
            this.atomicEventsTab.Padding = new System.Windows.Forms.Padding(3);
            this.atomicEventsTab.Size = new System.Drawing.Size(1143, 645);
            this.atomicEventsTab.TabIndex = 0;
            this.atomicEventsTab.Text = "Atomic Events";
            this.atomicEventsTab.UseVisualStyleBackColor = true;
            // 
            // actionsTab
            // 
            this.actionsTab.Location = new System.Drawing.Point(4, 28);
            this.actionsTab.Name = "actionsTab";
            this.actionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.actionsTab.Size = new System.Drawing.Size(1143, 651);
            this.actionsTab.TabIndex = 3;
            this.actionsTab.Text = "Actions";
            this.actionsTab.UseVisualStyleBackColor = true;
            // 
            // conditionsTab
            // 
            this.conditionsTab.Location = new System.Drawing.Point(4, 28);
            this.conditionsTab.Name = "conditionsTab";
            this.conditionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.conditionsTab.Size = new System.Drawing.Size(1143, 651);
            this.conditionsTab.TabIndex = 4;
            this.conditionsTab.Text = "Conditions";
            this.conditionsTab.UseVisualStyleBackColor = true;
            // 
            // complexEventTab
            // 
            this.complexEventTab.Location = new System.Drawing.Point(4, 28);
            this.complexEventTab.Name = "complexEventTab";
            this.complexEventTab.Padding = new System.Windows.Forms.Padding(3);
            this.complexEventTab.Size = new System.Drawing.Size(1143, 651);
            this.complexEventTab.TabIndex = 2;
            this.complexEventTab.Text = "Complex Events";
            this.complexEventTab.UseVisualStyleBackColor = true;
            // 
            // instancesTab
            // 
            this.instancesTab.Location = new System.Drawing.Point(4, 28);
            this.instancesTab.Name = "instancesTab";
            this.instancesTab.Padding = new System.Windows.Forms.Padding(3);
            this.instancesTab.Size = new System.Drawing.Size(1143, 651);
            this.instancesTab.TabIndex = 5;
            this.instancesTab.Text = "Running Instances";
            this.instancesTab.UseVisualStyleBackColor = true;
            // 
            // ecaDefPanel
            // 
            this.ecaDefPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ecaDefPanel.Location = new System.Drawing.Point(3, 3);
            this.ecaDefPanel.Name = "ecaDefPanel";
            this.ecaDefPanel.Size = new System.Drawing.Size(1137, 669);
            this.ecaDefPanel.TabIndex = 0;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 710);
            this.Controls.Add(this.mainTabControl);
            this.Name = "AdminForm";
            this.Text = "Event-Framework-Control";
            this.mainTabControl.ResumeLayout(false);
            this.LogInTab.ResumeLayout(false);
            this.LogInTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tripleDGV)).EndInit();
            this.UserAccTab.ResumeLayout(false);
            this.UserAccTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.assignDatabasesDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersDGV)).EndInit();
            this.databaseTab.ResumeLayout(false);
            this.databaseTab.PerformLayout();
            this.datasourcesTC.ResumeLayout(false);
            this.regDBTab.ResumeLayout(false);
            this.regDBTab.PerformLayout();
            this.regDSTab.ResumeLayout(false);
            this.regDSTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datasourceDGV)).EndInit();
            this.EcaDefTab.ResumeLayout(false);
            this.atomicEventTC.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage LogInTab;
        private System.Windows.Forms.TabPage UserAccTab;
        private System.Windows.Forms.TabPage EcaDefTab;
        private System.Windows.Forms.Button loginBT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.TextBox usernameTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button refreshBT;
        private System.Windows.Forms.DataGridView usersDGV;
        private System.Windows.Forms.Button resetPassBT;
        private System.Windows.Forms.Button newAccBT;
        private System.Windows.Forms.DataGridView tripleDGV;
        private System.Windows.Forms.TabPage databaseTab;
        private System.Windows.Forms.Button databaseRefreshBT;
        private System.Windows.Forms.DataGridView datasourceDGV;
        private System.Windows.Forms.Button submitNewDbBT;
        private System.Windows.Forms.RichTextBox dbDescribtionRTB;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox sparqlEndpointTB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox remoteEndpointTB;
        private System.Windows.Forms.ComboBox dbTypeCB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel ecaDefPanel;
        private System.Windows.Forms.TabControl atomicEventTC;
        private System.Windows.Forms.TabPage atomicEventsTab;
        private System.Windows.Forms.TabControl datasourcesTC;
        private System.Windows.Forms.TabPage regDBTab;
        private System.Windows.Forms.TabPage regDSTab;
        private System.Windows.Forms.Button submitNewDsBT;
        private System.Windows.Forms.RichTextBox descriptionRTB;
        private System.Windows.Forms.TextBox sparqlAddressTB;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TabPage complexEventTab;
        private System.Windows.Forms.TabPage actionsTab;
        private System.Windows.Forms.TextBox dbNameTB;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox dsNameTB;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TabPage conditionsTab;
        private System.Windows.Forms.DataGridView assignDatabasesDGV;
        private System.Windows.Forms.Button saveChangesBT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button deleteAccBT;
        private System.Windows.Forms.TabPage instancesTab;
        private System.Windows.Forms.Button deleteDsBT;
        private System.Windows.Forms.Button deactivateDbBT;
    }
}

