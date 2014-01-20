namespace EventFrameworkControl
{
    partial class CreateActionCondition
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
            this.createActionBT = new System.Windows.Forms.Button();
            this.editActionBT = new System.Windows.Forms.Button();
            this.methodeCB = new System.Windows.Forms.ComboBox();
            this.wsdlTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.descriptionRTB = new System.Windows.Forms.RichTextBox();
            this.browseCertificateBT = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.x509PasswordTB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.x509CertificateTB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.usernameTB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.titleLA = new System.Windows.Forms.Label();
            this.testActionBT = new System.Windows.Forms.Button();
            this.wsdlPanel = new System.Windows.Forms.Panel();
            this.databasesCB = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.proxyEndpointCB = new System.Windows.Forms.ComboBox();
            this.sparqlPanel = new System.Windows.Forms.Panel();
            this.useOtherSparqlCB = new System.Windows.Forms.CheckBox();
            this.sparqlEndpointCB = new System.Windows.Forms.ComboBox();
            this.paramDescrBT = new System.Windows.Forms.Button();
            this.sparqlX509CertBrowseBT = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.sparqlPasswordTB = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.sparqlLA = new System.Windows.Forms.Label();
            this.sparqlRTB = new System.Windows.Forms.RichTextBox();
            this.sparqlCreateActionBT = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.sparqlTestActionBT = new System.Windows.Forms.Button();
            this.sparqlEditActionBT = new System.Windows.Forms.Button();
            this.sparqlDescrRTB = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.sparqlEndpointTB = new System.Windows.Forms.TextBox();
            this.sparqlX509PassTB = new System.Windows.Forms.TextBox();
            this.endpointLA = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.sparqlX509CertTB = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.sparqlUserNameTB = new System.Windows.Forms.TextBox();
            this.paramPanel = new System.Windows.Forms.Panel();
            this.paramDGV = new System.Windows.Forms.DataGridView();
            this.nrCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.decrCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.welcomPanel = new System.Windows.Forms.Panel();
            this.sparqlBT = new System.Windows.Forms.Button();
            this.soapBT = new System.Windows.Forms.Button();
            this.wsdlPanel.SuspendLayout();
            this.sparqlPanel.SuspendLayout();
            this.paramPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paramDGV)).BeginInit();
            this.welcomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // createActionBT
            // 
            this.createActionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.createActionBT.Location = new System.Drawing.Point(392, 534);
            this.createActionBT.Name = "createActionBT";
            this.createActionBT.Size = new System.Drawing.Size(131, 32);
            this.createActionBT.TabIndex = 42;
            this.createActionBT.Text = "create new action";
            this.createActionBT.UseVisualStyleBackColor = true;
            this.createActionBT.Click += new System.EventHandler(this.createActionBT_Click);
            // 
            // editActionBT
            // 
            this.editActionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editActionBT.Location = new System.Drawing.Point(11, 534);
            this.editActionBT.Name = "editActionBT";
            this.editActionBT.Size = new System.Drawing.Size(140, 32);
            this.editActionBT.TabIndex = 43;
            this.editActionBT.Text = "edit action";
            this.editActionBT.UseVisualStyleBackColor = true;
            this.editActionBT.Click += new System.EventHandler(this.editActionBT_Click);
            // 
            // methodeCB
            // 
            this.methodeCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.methodeCB.FormattingEnabled = true;
            this.methodeCB.Location = new System.Drawing.Point(10, 192);
            this.methodeCB.Name = "methodeCB";
            this.methodeCB.Size = new System.Drawing.Size(513, 21);
            this.methodeCB.TabIndex = 58;
            this.methodeCB.DropDown += new System.EventHandler(this.methodeCB_DropDown);
            // 
            // wsdlTB
            // 
            this.wsdlTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wsdlTB.Location = new System.Drawing.Point(10, 116);
            this.wsdlTB.Name = "wsdlTB";
            this.wsdlTB.Size = new System.Drawing.Size(513, 20);
            this.wsdlTB.TabIndex = 52;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 57;
            this.label4.Text = "description (recommended)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "enter wsdl address:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "select endpoint address:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(290, 13);
            this.label5.TabIndex = 55;
            this.label5.Text = "select methode name:  (the dropdown might take a second!)";
            // 
            // descriptionRTB
            // 
            this.descriptionRTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionRTB.Location = new System.Drawing.Point(10, 234);
            this.descriptionRTB.Name = "descriptionRTB";
            this.descriptionRTB.Size = new System.Drawing.Size(513, 43);
            this.descriptionRTB.TabIndex = 56;
            this.descriptionRTB.Text = "";
            // 
            // browseCertificateBT
            // 
            this.browseCertificateBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.browseCertificateBT.Location = new System.Drawing.Point(392, 422);
            this.browseCertificateBT.Name = "browseCertificateBT";
            this.browseCertificateBT.Size = new System.Drawing.Size(131, 20);
            this.browseCertificateBT.TabIndex = 68;
            this.browseCertificateBT.Text = "browse";
            this.browseCertificateBT.UseVisualStyleBackColor = true;
            this.browseCertificateBT.Click += new System.EventHandler(this.x509BrowseBT_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 445);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 67;
            this.label8.Text = "X509 password:";
            // 
            // x509PasswordTB
            // 
            this.x509PasswordTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.x509PasswordTB.Location = new System.Drawing.Point(10, 461);
            this.x509PasswordTB.Name = "x509PasswordTB";
            this.x509PasswordTB.PasswordChar = '*';
            this.x509PasswordTB.Size = new System.Drawing.Size(376, 20);
            this.x509PasswordTB.TabIndex = 66;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 328);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(208, 18);
            this.label9.TabIndex = 59;
            this.label9.Text = "security settings (optional)";
            // 
            // x509CertificateTB
            // 
            this.x509CertificateTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.x509CertificateTB.Location = new System.Drawing.Point(10, 422);
            this.x509CertificateTB.Name = "x509CertificateTB";
            this.x509CertificateTB.Size = new System.Drawing.Size(376, 20);
            this.x509CertificateTB.TabIndex = 65;
            this.x509CertificateTB.TextChanged += new System.EventHandler(this.x509CertificateTB_TextChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 406);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 13);
            this.label7.TabIndex = 64;
            this.label7.Text = "X509Certificate filepath";
            // 
            // passwordTB
            // 
            this.passwordTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTB.Location = new System.Drawing.Point(392, 364);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.PasswordChar = '*';
            this.passwordTB.Size = new System.Drawing.Size(131, 20);
            this.passwordTB.TabIndex = 63;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(396, 348);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 62;
            this.label6.Text = "password:";
            // 
            // usernameTB
            // 
            this.usernameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameTB.Location = new System.Drawing.Point(10, 364);
            this.usernameTB.Name = "usernameTB";
            this.usernameTB.Size = new System.Drawing.Size(376, 20);
            this.usernameTB.TabIndex = 61;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 350);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 60;
            this.label10.Text = "username: ";
            // 
            // titleLA
            // 
            this.titleLA.AutoSize = true;
            this.titleLA.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLA.Location = new System.Drawing.Point(12, 9);
            this.titleLA.Name = "titleLA";
            this.titleLA.Size = new System.Drawing.Size(339, 18);
            this.titleLA.TabIndex = 69;
            this.titleLA.Text = "create an action for a remote soap-endpoint";
            // 
            // testActionBT
            // 
            this.testActionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.testActionBT.Location = new System.Drawing.Point(157, 534);
            this.testActionBT.Name = "testActionBT";
            this.testActionBT.Size = new System.Drawing.Size(85, 32);
            this.testActionBT.TabIndex = 70;
            this.testActionBT.Text = "test";
            this.testActionBT.UseVisualStyleBackColor = true;
            this.testActionBT.Visible = false;
            this.testActionBT.Click += new System.EventHandler(this.testActionBT_Click);
            // 
            // wsdlPanel
            // 
            this.wsdlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wsdlPanel.Controls.Add(this.databasesCB);
            this.wsdlPanel.Controls.Add(this.label13);
            this.wsdlPanel.Controls.Add(this.label11);
            this.wsdlPanel.Controls.Add(this.proxyEndpointCB);
            this.wsdlPanel.Controls.Add(this.titleLA);
            this.wsdlPanel.Controls.Add(this.testActionBT);
            this.wsdlPanel.Controls.Add(this.createActionBT);
            this.wsdlPanel.Controls.Add(this.editActionBT);
            this.wsdlPanel.Controls.Add(this.descriptionRTB);
            this.wsdlPanel.Controls.Add(this.browseCertificateBT);
            this.wsdlPanel.Controls.Add(this.label5);
            this.wsdlPanel.Controls.Add(this.label8);
            this.wsdlPanel.Controls.Add(this.x509PasswordTB);
            this.wsdlPanel.Controls.Add(this.label3);
            this.wsdlPanel.Controls.Add(this.label9);
            this.wsdlPanel.Controls.Add(this.label1);
            this.wsdlPanel.Controls.Add(this.x509CertificateTB);
            this.wsdlPanel.Controls.Add(this.label4);
            this.wsdlPanel.Controls.Add(this.label7);
            this.wsdlPanel.Controls.Add(this.wsdlTB);
            this.wsdlPanel.Controls.Add(this.passwordTB);
            this.wsdlPanel.Controls.Add(this.methodeCB);
            this.wsdlPanel.Controls.Add(this.label6);
            this.wsdlPanel.Controls.Add(this.label10);
            this.wsdlPanel.Controls.Add(this.usernameTB);
            this.wsdlPanel.Location = new System.Drawing.Point(1, 0);
            this.wsdlPanel.Name = "wsdlPanel";
            this.wsdlPanel.Size = new System.Drawing.Size(531, 566);
            this.wsdlPanel.TabIndex = 71;
            // 
            // databasesCB
            // 
            this.databasesCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databasesCB.FormattingEnabled = true;
            this.databasesCB.Location = new System.Drawing.Point(10, 64);
            this.databasesCB.Name = "databasesCB";
            this.databasesCB.Size = new System.Drawing.Size(513, 21);
            this.databasesCB.TabIndex = 75;
            this.databasesCB.SelectedIndexChanged += new System.EventHandler(this.databasesCB_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 74;
            this.label13.Text = " - or - ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(117, 13);
            this.label11.TabIndex = 72;
            this.label11.Text = "select satellit database:";
            // 
            // proxyEndpointCB
            // 
            this.proxyEndpointCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.proxyEndpointCB.FormattingEnabled = true;
            this.proxyEndpointCB.Location = new System.Drawing.Point(9, 152);
            this.proxyEndpointCB.Name = "proxyEndpointCB";
            this.proxyEndpointCB.Size = new System.Drawing.Size(513, 21);
            this.proxyEndpointCB.TabIndex = 71;
            this.proxyEndpointCB.DropDown += new System.EventHandler(this.proxyEndpointCB_DropDown);
            // 
            // sparqlPanel
            // 
            this.sparqlPanel.Controls.Add(this.useOtherSparqlCB);
            this.sparqlPanel.Controls.Add(this.sparqlEndpointCB);
            this.sparqlPanel.Controls.Add(this.paramDescrBT);
            this.sparqlPanel.Controls.Add(this.sparqlX509CertBrowseBT);
            this.sparqlPanel.Controls.Add(this.label21);
            this.sparqlPanel.Controls.Add(this.sparqlPasswordTB);
            this.sparqlPanel.Controls.Add(this.label18);
            this.sparqlPanel.Controls.Add(this.label20);
            this.sparqlPanel.Controls.Add(this.label15);
            this.sparqlPanel.Controls.Add(this.sparqlLA);
            this.sparqlPanel.Controls.Add(this.sparqlRTB);
            this.sparqlPanel.Controls.Add(this.sparqlCreateActionBT);
            this.sparqlPanel.Controls.Add(this.label2);
            this.sparqlPanel.Controls.Add(this.sparqlTestActionBT);
            this.sparqlPanel.Controls.Add(this.sparqlEditActionBT);
            this.sparqlPanel.Controls.Add(this.sparqlDescrRTB);
            this.sparqlPanel.Controls.Add(this.label12);
            this.sparqlPanel.Controls.Add(this.sparqlEndpointTB);
            this.sparqlPanel.Controls.Add(this.sparqlX509PassTB);
            this.sparqlPanel.Controls.Add(this.endpointLA);
            this.sparqlPanel.Controls.Add(this.label14);
            this.sparqlPanel.Controls.Add(this.sparqlX509CertTB);
            this.sparqlPanel.Controls.Add(this.label16);
            this.sparqlPanel.Controls.Add(this.label17);
            this.sparqlPanel.Controls.Add(this.label19);
            this.sparqlPanel.Controls.Add(this.sparqlUserNameTB);
            this.sparqlPanel.Controls.Add(this.paramPanel);
            this.sparqlPanel.Location = new System.Drawing.Point(0, 0);
            this.sparqlPanel.Name = "sparqlPanel";
            this.sparqlPanel.Size = new System.Drawing.Size(532, 566);
            this.sparqlPanel.TabIndex = 72;
            // 
            // useOtherSparqlCB
            // 
            this.useOtherSparqlCB.AutoSize = true;
            this.useOtherSparqlCB.Location = new System.Drawing.Point(368, 41);
            this.useOtherSparqlCB.Name = "useOtherSparqlCB";
            this.useOtherSparqlCB.Size = new System.Drawing.Size(160, 17);
            this.useOtherSparqlCB.TabIndex = 81;
            this.useOtherSparqlCB.Text = "use an other sparql endpoint";
            this.useOtherSparqlCB.UseVisualStyleBackColor = true;
            this.useOtherSparqlCB.CheckedChanged += new System.EventHandler(this.useOtherSparqlCB_CheckedChanged);
            // 
            // sparqlEndpointCB
            // 
            this.sparqlEndpointCB.FormattingEnabled = true;
            this.sparqlEndpointCB.Location = new System.Drawing.Point(12, 60);
            this.sparqlEndpointCB.Name = "sparqlEndpointCB";
            this.sparqlEndpointCB.Size = new System.Drawing.Size(517, 21);
            this.sparqlEndpointCB.TabIndex = 80;
            this.sparqlEndpointCB.Text = "select a database";
            this.sparqlEndpointCB.SelectedIndexChanged += new System.EventHandler(this.endpointCB_SelectedIndexChanged);
            // 
            // paramDescrBT
            // 
            this.paramDescrBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.paramDescrBT.Location = new System.Drawing.Point(457, 311);
            this.paramDescrBT.Name = "paramDescrBT";
            this.paramDescrBT.Size = new System.Drawing.Size(71, 39);
            this.paramDescrBT.TabIndex = 77;
            this.paramDescrBT.Text = "parameter descriptions";
            this.paramDescrBT.UseVisualStyleBackColor = true;
            this.paramDescrBT.Click += new System.EventHandler(this.paramDescrBT_Click);
            // 
            // sparqlX509CertBrowseBT
            // 
            this.sparqlX509CertBrowseBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sparqlX509CertBrowseBT.Location = new System.Drawing.Point(361, 445);
            this.sparqlX509CertBrowseBT.Name = "sparqlX509CertBrowseBT";
            this.sparqlX509CertBrowseBT.Size = new System.Drawing.Size(163, 19);
            this.sparqlX509CertBrowseBT.TabIndex = 68;
            this.sparqlX509CertBrowseBT.Text = "browse";
            this.sparqlX509CertBrowseBT.UseVisualStyleBackColor = true;
            this.sparqlX509CertBrowseBT.Click += new System.EventHandler(this.x509BrowseBT_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(12, 324);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(306, 13);
            this.label21.TabIndex = 76;
            this.label21.Text = "parameter-mappings will be added in the complex event context";
            // 
            // sparqlPasswordTB
            // 
            this.sparqlPasswordTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sparqlPasswordTB.Location = new System.Drawing.Point(361, 386);
            this.sparqlPasswordTB.Name = "sparqlPasswordTB";
            this.sparqlPasswordTB.PasswordChar = '*';
            this.sparqlPasswordTB.Size = new System.Drawing.Size(163, 20);
            this.sparqlPasswordTB.TabIndex = 63;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(365, 371);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(55, 13);
            this.label18.TabIndex = 62;
            this.label18.Text = "password:";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(195, 337);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(256, 13);
            this.label20.TabIndex = 75;
            this.label20.Text = "please give a short description for every parameter ->";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 311);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(281, 13);
            this.label15.TabIndex = 74;
            this.label15.Text = "to parameterize this query use the positional parameter \'??\'";
            // 
            // sparqlLA
            // 
            this.sparqlLA.AutoSize = true;
            this.sparqlLA.Location = new System.Drawing.Point(12, 149);
            this.sparqlLA.Name = "sparqlLA";
            this.sparqlLA.Size = new System.Drawing.Size(64, 13);
            this.sparqlLA.TabIndex = 73;
            this.sparqlLA.Text = "sparql query";
            // 
            // sparqlRTB
            // 
            this.sparqlRTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sparqlRTB.Location = new System.Drawing.Point(10, 165);
            this.sparqlRTB.Name = "sparqlRTB";
            this.sparqlRTB.Size = new System.Drawing.Size(518, 143);
            this.sparqlRTB.TabIndex = 72;
            this.sparqlRTB.Text = "";
            // 
            // sparqlCreateActionBT
            // 
            this.sparqlCreateActionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sparqlCreateActionBT.Location = new System.Drawing.Point(384, 534);
            this.sparqlCreateActionBT.Name = "sparqlCreateActionBT";
            this.sparqlCreateActionBT.Size = new System.Drawing.Size(140, 32);
            this.sparqlCreateActionBT.TabIndex = 71;
            this.sparqlCreateActionBT.Text = "create new action";
            this.sparqlCreateActionBT.UseVisualStyleBackColor = true;
            this.sparqlCreateActionBT.Click += new System.EventHandler(this.sparqlCreateActionBT_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(348, 18);
            this.label2.TabIndex = 69;
            this.label2.Text = "create an action for a remote sparql-endpoint";
            // 
            // sparqlTestActionBT
            // 
            this.sparqlTestActionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sparqlTestActionBT.Location = new System.Drawing.Point(157, 534);
            this.sparqlTestActionBT.Name = "sparqlTestActionBT";
            this.sparqlTestActionBT.Size = new System.Drawing.Size(85, 32);
            this.sparqlTestActionBT.TabIndex = 70;
            this.sparqlTestActionBT.Text = "test";
            this.sparqlTestActionBT.UseVisualStyleBackColor = true;
            this.sparqlTestActionBT.Visible = false;
            this.sparqlTestActionBT.Click += new System.EventHandler(this.sparqlTestActionBT_Click);
            // 
            // sparqlEditActionBT
            // 
            this.sparqlEditActionBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sparqlEditActionBT.Location = new System.Drawing.Point(11, 534);
            this.sparqlEditActionBT.Name = "sparqlEditActionBT";
            this.sparqlEditActionBT.Size = new System.Drawing.Size(140, 32);
            this.sparqlEditActionBT.TabIndex = 43;
            this.sparqlEditActionBT.Text = "edit action";
            this.sparqlEditActionBT.UseVisualStyleBackColor = true;
            this.sparqlEditActionBT.Click += new System.EventHandler(this.sparqlEditActionBT_Click);
            // 
            // sparqlDescrRTB
            // 
            this.sparqlDescrRTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sparqlDescrRTB.Location = new System.Drawing.Point(10, 103);
            this.sparqlDescrRTB.Name = "sparqlDescrRTB";
            this.sparqlDescrRTB.Size = new System.Drawing.Size(518, 43);
            this.sparqlDescrRTB.TabIndex = 56;
            this.sparqlDescrRTB.Text = "";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 467);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 13);
            this.label12.TabIndex = 67;
            this.label12.Text = "X509 password:";
            // 
            // sparqlEndpointTB
            // 
            this.sparqlEndpointTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sparqlEndpointTB.Location = new System.Drawing.Point(10, 61);
            this.sparqlEndpointTB.Name = "sparqlEndpointTB";
            this.sparqlEndpointTB.Size = new System.Drawing.Size(519, 20);
            this.sparqlEndpointTB.TabIndex = 54;
            this.sparqlEndpointTB.Visible = false;
            // 
            // sparqlX509PassTB
            // 
            this.sparqlX509PassTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sparqlX509PassTB.Location = new System.Drawing.Point(10, 483);
            this.sparqlX509PassTB.Name = "sparqlX509PassTB";
            this.sparqlX509PassTB.PasswordChar = '*';
            this.sparqlX509PassTB.Size = new System.Drawing.Size(345, 20);
            this.sparqlX509PassTB.TabIndex = 66;
            // 
            // endpointLA
            // 
            this.endpointLA.AutoSize = true;
            this.endpointLA.Location = new System.Drawing.Point(12, 45);
            this.endpointLA.Name = "endpointLA";
            this.endpointLA.Size = new System.Drawing.Size(122, 13);
            this.endpointLA.TabIndex = 53;
            this.endpointLA.Text = "sparql endpoint address:";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(9, 346);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(208, 18);
            this.label14.TabIndex = 59;
            this.label14.Text = "security settings (optional)";
            // 
            // sparqlX509CertTB
            // 
            this.sparqlX509CertTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sparqlX509CertTB.Location = new System.Drawing.Point(10, 444);
            this.sparqlX509CertTB.Name = "sparqlX509CertTB";
            this.sparqlX509CertTB.Size = new System.Drawing.Size(345, 20);
            this.sparqlX509CertTB.TabIndex = 65;
            this.sparqlX509CertTB.TextChanged += new System.EventHandler(this.x509CertificateTB_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 87);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(134, 13);
            this.label16.TabIndex = 57;
            this.label16.Text = "description (recommended)";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 428);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(116, 13);
            this.label17.TabIndex = 64;
            this.label17.Text = "X509Certificate filepath";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(12, 372);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 13);
            this.label19.TabIndex = 60;
            this.label19.Text = "username: ";
            // 
            // sparqlUserNameTB
            // 
            this.sparqlUserNameTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sparqlUserNameTB.Location = new System.Drawing.Point(10, 386);
            this.sparqlUserNameTB.Name = "sparqlUserNameTB";
            this.sparqlUserNameTB.Size = new System.Drawing.Size(345, 20);
            this.sparqlUserNameTB.TabIndex = 61;
            // 
            // paramPanel
            // 
            this.paramPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paramPanel.Controls.Add(this.paramDGV);
            this.paramPanel.Location = new System.Drawing.Point(0, 350);
            this.paramPanel.Name = "paramPanel";
            this.paramPanel.Size = new System.Drawing.Size(532, 216);
            this.paramPanel.TabIndex = 78;
            this.paramPanel.Visible = false;
            // 
            // paramDGV
            // 
            this.paramDGV.AllowUserToAddRows = false;
            this.paramDGV.AllowUserToDeleteRows = false;
            this.paramDGV.AllowUserToResizeColumns = false;
            this.paramDGV.AllowUserToResizeRows = false;
            this.paramDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paramDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.paramDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nrCol,
            this.decrCol});
            this.paramDGV.Location = new System.Drawing.Point(0, 0);
            this.paramDGV.Name = "paramDGV";
            this.paramDGV.RowHeadersVisible = false;
            this.paramDGV.Size = new System.Drawing.Size(529, 216);
            this.paramDGV.TabIndex = 0;
            this.paramDGV.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.paramDGV_CellValidated);
            // 
            // nrCol
            // 
            this.nrCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.nrCol.HeaderText = "position";
            this.nrCol.Name = "nrCol";
            this.nrCol.ReadOnly = true;
            this.nrCol.Width = 68;
            // 
            // decrCol
            // 
            this.decrCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.decrCol.HeaderText = "description";
            this.decrCol.Name = "decrCol";
            // 
            // welcomPanel
            // 
            this.welcomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.welcomPanel.Controls.Add(this.sparqlBT);
            this.welcomPanel.Controls.Add(this.soapBT);
            this.welcomPanel.Location = new System.Drawing.Point(0, 0);
            this.welcomPanel.Name = "welcomPanel";
            this.welcomPanel.Size = new System.Drawing.Size(531, 566);
            this.welcomPanel.TabIndex = 74;
            // 
            // sparqlBT
            // 
            this.sparqlBT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sparqlBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sparqlBT.Location = new System.Drawing.Point(268, 234);
            this.sparqlBT.Name = "sparqlBT";
            this.sparqlBT.Size = new System.Drawing.Size(152, 53);
            this.sparqlBT.TabIndex = 1;
            this.sparqlBT.Text = "create action for sparql endpoints";
            this.sparqlBT.UseVisualStyleBackColor = true;
            this.sparqlBT.Click += new System.EventHandler(this.sparqlBT_Click);
            // 
            // soapBT
            // 
            this.soapBT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.soapBT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.soapBT.Location = new System.Drawing.Point(110, 234);
            this.soapBT.Name = "soapBT";
            this.soapBT.Size = new System.Drawing.Size(152, 53);
            this.soapBT.TabIndex = 0;
            this.soapBT.Text = "create action for soap endpoints";
            this.soapBT.UseVisualStyleBackColor = true;
            this.soapBT.Click += new System.EventHandler(this.soapBT_Click);
            // 
            // CreateActionCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 574);
            this.Controls.Add(this.welcomPanel);
            this.Controls.Add(this.wsdlPanel);
            this.Controls.Add(this.sparqlPanel);
            this.Name = "CreateActionCondition";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "CreateAction";
            this.wsdlPanel.ResumeLayout(false);
            this.wsdlPanel.PerformLayout();
            this.sparqlPanel.ResumeLayout(false);
            this.sparqlPanel.PerformLayout();
            this.paramPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paramDGV)).EndInit();
            this.welcomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button createActionBT;
        private System.Windows.Forms.Button editActionBT;
        private System.Windows.Forms.ComboBox methodeCB;
        private System.Windows.Forms.TextBox wsdlTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox descriptionRTB;
        private System.Windows.Forms.Button browseCertificateBT;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox x509PasswordTB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox x509CertificateTB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox usernameTB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label titleLA;
        private System.Windows.Forms.Button testActionBT;
        private System.Windows.Forms.Panel wsdlPanel;
        private System.Windows.Forms.Panel sparqlPanel;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label sparqlLA;
        private System.Windows.Forms.RichTextBox sparqlRTB;
        private System.Windows.Forms.Button sparqlCreateActionBT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sparqlTestActionBT;
        private System.Windows.Forms.Button sparqlEditActionBT;
        private System.Windows.Forms.RichTextBox sparqlDescrRTB;
        private System.Windows.Forms.Button sparqlX509CertBrowseBT;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox sparqlX509PassTB;
        private System.Windows.Forms.Label endpointLA;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox sparqlX509CertTB;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox sparqlPasswordTB;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox sparqlUserNameTB;
        private System.Windows.Forms.Panel welcomPanel;
        private System.Windows.Forms.Button sparqlBT;
        private System.Windows.Forms.Button soapBT;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button paramDescrBT;
        private System.Windows.Forms.Panel paramPanel;
        private System.Windows.Forms.DataGridView paramDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn nrCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn decrCol;
        private System.Windows.Forms.ComboBox sparqlEndpointCB;
        private System.Windows.Forms.ComboBox proxyEndpointCB;
        private System.Windows.Forms.CheckBox useOtherSparqlCB;
        private System.Windows.Forms.TextBox sparqlEndpointTB;
        private System.Windows.Forms.ComboBox databasesCB;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
    }
}