namespace App
{
    partial class FormNewMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewMain));
            this.pnlHeader = new App.Controls.SectionPanel();
            this.singleLineLabel2 = new App.Controls.SingleLineLabel();
            this.singleLineLabel1 = new App.Controls.SingleLineLabel();
            this.iconLogo = new App.Controls.IconBox();
            this.lblOrganization = new App.Controls.SingleLineLabel();
            this.sepHeaderLeft = new App.Controls.BufferedPanel();
            this.lblAppTitle = new App.Controls.SingleLineLabel();
            this.sepHeaderMonth = new App.Controls.BufferedPanel();
            this.lblProcessingMonth = new App.Controls.SingleLineLabel();
            this.cmbProcessingMonth = new App.Controls.BorderedComboBox();
            this.sepHeaderRight = new App.Controls.BufferedPanel();
            this.lblLoginUser = new App.Controls.SingleLineLabel();
            this.pnlHeaderBorder = new App.Controls.BufferedPanel();
            this.pnlToolbar = new App.Controls.SectionPanel();
            this.btnDataSave = new App.Controls.ToolbarButton();
            this.sepToolbar1 = new App.Controls.BufferedPanel();
            this.btnDataRestore = new App.Controls.ToolbarButton();
            this.sepToolbar2 = new App.Controls.BufferedPanel();
            this.btnRemoteSupport = new App.Controls.ToolbarButton();
            this.sepToolbar3 = new App.Controls.BufferedPanel();
            this.btnLogout = new App.Controls.ToolbarButton();
            this.pnlToolbarBorder = new App.Controls.BufferedPanel();
            this.pnlContent = new App.Controls.BufferedPanel();
            this.tblSections = new App.Controls.BufferedTableLayoutPanel();
            this.secDaily = new App.Controls.SectionPanel();
            this.tblDaily = new App.Controls.BufferedTableLayoutPanel();
            this.stpPrepare = new App.Controls.StepHeaderLabel();
            this.stpEntry = new App.Controls.StepHeaderLabel();
            this.stpOutput = new App.Controls.StepHeaderLabel();
            this.tblPrepare = new App.Controls.BufferedTableLayoutPanel();
            this.btnSelectMonth = new App.Controls.CardTileButton();
            this.btnStaffEntry = new App.Controls.CardTileButton();
            this.btnCalendarSetting = new App.Controls.CardTileButton();
            this.sepDailyColumn1 = new App.Controls.BufferedPanel();
            this.tblEntry = new App.Controls.BufferedTableLayoutPanel();
            this.btnWorkDataEntry = new App.Controls.CardTileButton();
            this.btnWorkDataCsvImport = new App.Controls.CardTileButton();
            this.btnWorkCheckList = new App.Controls.CardTileButton();
            this.btnAllowanceCheckList = new App.Controls.CardTileButton();
            this.sepDailyColumn2 = new App.Controls.BufferedPanel();
            this.tblOutput = new App.Controls.BufferedTableLayoutPanel();
            this.btnPayrollDataCreate = new App.Controls.CardTileButton();
            this.btnPayslipPrint = new App.Controls.CardTileButton();
            this.btnFbDataCreate = new App.Controls.CardTileButton();
            this.secYearEnd = new App.Controls.SectionPanel();
            this.tblYearEnd = new App.Controls.BufferedTableLayoutPanel();
            this.btnWithholdingSlipPrint = new App.Controls.CardTileButton();
            this.btnETaxCsvExport = new App.Controls.CardTileButton();
            this.secMaster = new App.Controls.SectionPanel();
            this.tblMaster = new App.Controls.BufferedTableLayoutPanel();
            this.btnMasterBasicInfo = new App.Controls.CardTileButton();
            this.btnMasterStaffInCharge = new App.Controls.CardTileButton();
            this.btnMasterJob = new App.Controls.CardTileButton();
            this.btnMasterBusinessCategory = new App.Controls.CardTileButton();
            this.btnMasterWorkUnitPrice = new App.Controls.CardTileButton();
            this.btnMasterAllowanceUnitPrice = new App.Controls.CardTileButton();
            this.btnMasterWithholdingTax = new App.Controls.CardTileButton();
            this.pnlHeading = new App.Controls.BufferedPanel();
            this.icoHeading = new App.Controls.IconBox();
            this.lblHeading = new App.Controls.SingleLineLabel();
            this.lblHeadingNote = new App.Controls.SingleLineLabel();
            this.pnlHeadingLineAccent = new App.Controls.BufferedPanel();
            this.pnlHeadingLine = new App.Controls.BufferedPanel();
            this.pnlHeader.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.tblSections.SuspendLayout();
            this.secDaily.SuspendLayout();
            this.tblDaily.SuspendLayout();
            this.tblPrepare.SuspendLayout();
            this.tblEntry.SuspendLayout();
            this.tblOutput.SuspendLayout();
            this.secYearEnd.SuspendLayout();
            this.tblYearEnd.SuspendLayout();
            this.secMaster.SuspendLayout();
            this.tblMaster.SuspendLayout();
            this.pnlHeading.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.BorderColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.singleLineLabel2);
            this.pnlHeader.Controls.Add(this.singleLineLabel1);
            this.pnlHeader.Controls.Add(this.iconLogo);
            this.pnlHeader.Controls.Add(this.lblOrganization);
            this.pnlHeader.Controls.Add(this.sepHeaderLeft);
            this.pnlHeader.Controls.Add(this.lblAppTitle);
            this.pnlHeader.Controls.Add(this.sepHeaderMonth);
            this.pnlHeader.Controls.Add(this.lblProcessingMonth);
            this.pnlHeader.Controls.Add(this.cmbProcessingMonth);
            this.pnlHeader.Controls.Add(this.sepHeaderRight);
            this.pnlHeader.Controls.Add(this.lblLoginUser);
            this.pnlHeader.Controls.Add(this.pnlHeaderBorder);
            this.pnlHeader.CornerRadius = 0;
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlHeader.HeaderHeight = 0;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1294, 66);
            this.pnlHeader.TabIndex = 1;
            // 
            // singleLineLabel2
            // 
            this.singleLineLabel2.Font = new System.Drawing.Font("BIZ UDPゴシック", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineLabel2.ForeColor = System.Drawing.Color.LightSlateGray;
            this.singleLineLabel2.Location = new System.Drawing.Point(307, 8);
            this.singleLineLabel2.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineLabel2.Name = "singleLineLabel2";
            this.singleLineLabel2.Size = new System.Drawing.Size(359, 53);
            this.singleLineLabel2.TabIndex = 11;
            this.singleLineLabel2.TabStop = false;
            this.singleLineLabel2.Text = "手当管理システム";
            // 
            // singleLineLabel1
            // 
            this.singleLineLabel1.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(60)))), ((int)(((byte)(84)))));
            this.singleLineLabel1.Location = new System.Drawing.Point(93, 6);
            this.singleLineLabel1.Name = "singleLineLabel1";
            this.singleLineLabel1.Size = new System.Drawing.Size(100, 23);
            this.singleLineLabel1.TabIndex = 10;
            this.singleLineLabel1.TabStop = false;
            this.singleLineLabel1.Text = "公益社団法人";
            // 
            // iconLogo
            // 
            this.iconLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(84)))), ((int)(((byte)(145)))));
            this.iconLogo.CornerRadius = 10;
            this.iconLogo.ForeColor = System.Drawing.Color.White;
            this.iconLogo.IconKind = App.Controls.MenuIconKind.OrganizationLogo;
            this.iconLogo.LineWeight = 0.85F;
            this.iconLogo.Location = new System.Drawing.Point(33, 9);
            this.iconLogo.Margin = new System.Windows.Forms.Padding(4);
            this.iconLogo.Name = "iconLogo";
            this.iconLogo.Padding = new System.Windows.Forms.Padding(7);
            this.iconLogo.Size = new System.Drawing.Size(48, 48);
            this.iconLogo.TabIndex = 0;
            this.iconLogo.TabStop = false;
            // 
            // lblOrganization
            // 
            this.lblOrganization.Font = new System.Drawing.Font("BIZ UDPゴシック", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOrganization.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(60)))), ((int)(((byte)(84)))));
            this.lblOrganization.Location = new System.Drawing.Point(92, 30);
            this.lblOrganization.Margin = new System.Windows.Forms.Padding(4);
            this.lblOrganization.Name = "lblOrganization";
            this.lblOrganization.Size = new System.Drawing.Size(122, 27);
            this.lblOrganization.TabIndex = 1;
            this.lblOrganization.TabStop = false;
            this.lblOrganization.Text = "藤沢医師会";
            // 
            // sepHeaderLeft
            // 
            this.sepHeaderLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(96)))), ((int)(((byte)(112)))), ((int)(((byte)(134)))));
            this.sepHeaderLeft.Location = new System.Drawing.Point(222, 13);
            this.sepHeaderLeft.Margin = new System.Windows.Forms.Padding(4);
            this.sepHeaderLeft.Name = "sepHeaderLeft";
            this.sepHeaderLeft.Size = new System.Drawing.Size(1, 40);
            this.sepHeaderLeft.TabIndex = 2;
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.Font = new System.Drawing.Font("BIZ UDPゴシック", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblAppTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(38)))), ((int)(((byte)(66)))));
            this.lblAppTitle.Location = new System.Drawing.Point(246, 6);
            this.lblAppTitle.Margin = new System.Windows.Forms.Padding(4);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(359, 53);
            this.lblAppTitle.TabIndex = 3;
            this.lblAppTitle.TabStop = false;
            this.lblAppTitle.Text = "給与";
            // 
            // sepHeaderMonth
            // 
            this.sepHeaderMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sepHeaderMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(96)))), ((int)(((byte)(112)))), ((int)(((byte)(134)))));
            this.sepHeaderMonth.Location = new System.Drawing.Point(806, 18);
            this.sepHeaderMonth.Margin = new System.Windows.Forms.Padding(4);
            this.sepHeaderMonth.Name = "sepHeaderMonth";
            this.sepHeaderMonth.Size = new System.Drawing.Size(1, 31);
            this.sepHeaderMonth.TabIndex = 4;
            // 
            // lblProcessingMonth
            // 
            this.lblProcessingMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProcessingMonth.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblProcessingMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(60)))), ((int)(((byte)(84)))));
            this.lblProcessingMonth.Location = new System.Drawing.Point(828, 12);
            this.lblProcessingMonth.Margin = new System.Windows.Forms.Padding(4);
            this.lblProcessingMonth.Name = "lblProcessingMonth";
            this.lblProcessingMonth.Size = new System.Drawing.Size(84, 41);
            this.lblProcessingMonth.TabIndex = 5;
            this.lblProcessingMonth.TabStop = false;
            this.lblProcessingMonth.Text = "処理月度";
            // 
            // cmbProcessingMonth
            // 
            this.cmbProcessingMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProcessingMonth.BackColor = System.Drawing.Color.White;
            this.cmbProcessingMonth.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(206)))), ((int)(((byte)(220)))));
            this.cmbProcessingMonth.CornerRadius = 7;
            this.cmbProcessingMonth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbProcessingMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProcessingMonth.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbProcessingMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProcessingMonth.Font = new System.Drawing.Font("游ゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbProcessingMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(38)))), ((int)(((byte)(66)))));
            this.cmbProcessingMonth.FormatString = "yyyy年M月";
            this.cmbProcessingMonth.FormattingEnabled = true;
            this.cmbProcessingMonth.ItemHeight = 29;
            this.cmbProcessingMonth.Location = new System.Drawing.Point(920, 14);
            this.cmbProcessingMonth.Margin = new System.Windows.Forms.Padding(4);
            this.cmbProcessingMonth.Name = "cmbProcessingMonth";
            this.cmbProcessingMonth.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(232)))), ((int)(((byte)(248)))));
            this.cmbProcessingMonth.Size = new System.Drawing.Size(171, 35);
            this.cmbProcessingMonth.TabIndex = 6;
            // 
            // sepHeaderRight
            // 
            this.sepHeaderRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sepHeaderRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(96)))), ((int)(((byte)(112)))), ((int)(((byte)(134)))));
            this.sepHeaderRight.Location = new System.Drawing.Point(1114, 18);
            this.sepHeaderRight.Margin = new System.Windows.Forms.Padding(4);
            this.sepHeaderRight.Name = "sepHeaderRight";
            this.sepHeaderRight.Size = new System.Drawing.Size(1, 31);
            this.sepHeaderRight.TabIndex = 7;
            // 
            // lblLoginUser
            // 
            this.lblLoginUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoginUser.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLoginUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(60)))), ((int)(((byte)(84)))));
            this.lblLoginUser.Location = new System.Drawing.Point(1137, 12);
            this.lblLoginUser.Margin = new System.Windows.Forms.Padding(4);
            this.lblLoginUser.Name = "lblLoginUser";
            this.lblLoginUser.Size = new System.Drawing.Size(147, 41);
            this.lblLoginUser.TabIndex = 8;
            this.lblLoginUser.TabStop = false;
            this.lblLoginUser.Text = "ログイン：管理者";
            // 
            // pnlHeaderBorder
            // 
            this.pnlHeaderBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.pnlHeaderBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlHeaderBorder.Location = new System.Drawing.Point(0, 65);
            this.pnlHeaderBorder.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeaderBorder.Name = "pnlHeaderBorder";
            this.pnlHeaderBorder.Size = new System.Drawing.Size(1294, 1);
            this.pnlHeaderBorder.TabIndex = 9;
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.Transparent;
            this.pnlToolbar.BorderColor = System.Drawing.Color.Transparent;
            this.pnlToolbar.Controls.Add(this.btnDataSave);
            this.pnlToolbar.Controls.Add(this.sepToolbar1);
            this.pnlToolbar.Controls.Add(this.btnDataRestore);
            this.pnlToolbar.Controls.Add(this.sepToolbar2);
            this.pnlToolbar.Controls.Add(this.btnRemoteSupport);
            this.pnlToolbar.Controls.Add(this.sepToolbar3);
            this.pnlToolbar.Controls.Add(this.btnLogout);
            this.pnlToolbar.Controls.Add(this.pnlToolbarBorder);
            this.pnlToolbar.CornerRadius = 0;
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlToolbar.Font = new System.Drawing.Font("游ゴシック", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pnlToolbar.HeaderHeight = 0;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 66);
            this.pnlToolbar.Margin = new System.Windows.Forms.Padding(4);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(1294, 41);
            this.pnlToolbar.TabIndex = 2;
            // 
            // btnDataSave
            // 
            this.btnDataSave.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDataSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.btnDataSave.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(246)))));
            this.btnDataSave.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(96)))), ((int)(((byte)(160)))));
            this.btnDataSave.IconKind = App.Controls.MenuIconKind.FileDown;
            this.btnDataSave.IconLineWeight = 0.85F;
            this.btnDataSave.IconSize = 19;
            this.btnDataSave.Location = new System.Drawing.Point(31, 5);
            this.btnDataSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnDataSave.Name = "btnDataSave";
            this.btnDataSave.Size = new System.Drawing.Size(139, 31);
            this.btnDataSave.TabIndex = 0;
            this.btnDataSave.Text = "データ保存";
            // 
            // sepToolbar1
            // 
            this.sepToolbar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(96)))), ((int)(((byte)(112)))), ((int)(((byte)(134)))));
            this.sepToolbar1.Location = new System.Drawing.Point(186, 11);
            this.sepToolbar1.Margin = new System.Windows.Forms.Padding(4);
            this.sepToolbar1.Name = "sepToolbar1";
            this.sepToolbar1.Size = new System.Drawing.Size(1, 19);
            this.sepToolbar1.TabIndex = 1;
            // 
            // btnDataRestore
            // 
            this.btnDataRestore.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnDataRestore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.btnDataRestore.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(246)))));
            this.btnDataRestore.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(96)))), ((int)(((byte)(160)))));
            this.btnDataRestore.IconKind = App.Controls.MenuIconKind.Upload;
            this.btnDataRestore.IconLineWeight = 0.85F;
            this.btnDataRestore.IconSize = 19;
            this.btnDataRestore.Location = new System.Drawing.Point(202, 5);
            this.btnDataRestore.Margin = new System.Windows.Forms.Padding(4);
            this.btnDataRestore.Name = "btnDataRestore";
            this.btnDataRestore.Size = new System.Drawing.Size(139, 31);
            this.btnDataRestore.TabIndex = 2;
            this.btnDataRestore.Text = "データ復旧";
            // 
            // sepToolbar2
            // 
            this.sepToolbar2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(96)))), ((int)(((byte)(112)))), ((int)(((byte)(134)))));
            this.sepToolbar2.Location = new System.Drawing.Point(357, 11);
            this.sepToolbar2.Margin = new System.Windows.Forms.Padding(4);
            this.sepToolbar2.Name = "sepToolbar2";
            this.sepToolbar2.Size = new System.Drawing.Size(1, 19);
            this.sepToolbar2.TabIndex = 3;
            // 
            // btnRemoteSupport
            // 
            this.btnRemoteSupport.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnRemoteSupport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.btnRemoteSupport.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(246)))));
            this.btnRemoteSupport.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(96)))), ((int)(((byte)(160)))));
            this.btnRemoteSupport.IconKind = App.Controls.MenuIconKind.Laptop;
            this.btnRemoteSupport.IconLineWeight = 0.85F;
            this.btnRemoteSupport.IconSize = 19;
            this.btnRemoteSupport.Location = new System.Drawing.Point(374, 5);
            this.btnRemoteSupport.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoteSupport.Name = "btnRemoteSupport";
            this.btnRemoteSupport.Size = new System.Drawing.Size(156, 31);
            this.btnRemoteSupport.TabIndex = 4;
            this.btnRemoteSupport.Text = "遠隔サポート";
            // 
            // sepToolbar3
            // 
            this.sepToolbar3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(96)))), ((int)(((byte)(112)))), ((int)(((byte)(134)))));
            this.sepToolbar3.Location = new System.Drawing.Point(545, 11);
            this.sepToolbar3.Margin = new System.Windows.Forms.Padding(4);
            this.sepToolbar3.Name = "sepToolbar3";
            this.sepToolbar3.Size = new System.Drawing.Size(1, 19);
            this.sepToolbar3.TabIndex = 5;
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.btnLogout.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(246)))));
            this.btnLogout.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(96)))), ((int)(((byte)(160)))));
            this.btnLogout.IconKind = App.Controls.MenuIconKind.LogOut;
            this.btnLogout.IconLineWeight = 0.85F;
            this.btnLogout.IconSize = 19;
            this.btnLogout.Location = new System.Drawing.Point(562, 5);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(139, 31);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "ログアウト";
            // 
            // pnlToolbarBorder
            // 
            this.pnlToolbarBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.pnlToolbarBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlToolbarBorder.Location = new System.Drawing.Point(0, 40);
            this.pnlToolbarBorder.Margin = new System.Windows.Forms.Padding(4);
            this.pnlToolbarBorder.Name = "pnlToolbarBorder";
            this.pnlToolbarBorder.Size = new System.Drawing.Size(1294, 1);
            this.pnlToolbarBorder.TabIndex = 7;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlContent.Controls.Add(this.tblSections);
            this.pnlContent.Controls.Add(this.pnlHeading);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 107);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(4);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(29, 0, 26, 24);
            this.pnlContent.Size = new System.Drawing.Size(1294, 738);
            this.pnlContent.TabIndex = 3;
            // 
            // tblSections
            // 
            this.tblSections.ColumnCount = 2;
            this.tblSections.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73F));
            this.tblSections.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tblSections.Controls.Add(this.secDaily, 0, 0);
            this.tblSections.Controls.Add(this.secYearEnd, 1, 0);
            this.tblSections.Controls.Add(this.secMaster, 0, 1);
            this.tblSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblSections.Location = new System.Drawing.Point(29, 100);
            this.tblSections.Margin = new System.Windows.Forms.Padding(4);
            this.tblSections.Name = "tblSections";
            this.tblSections.RowCount = 2;
            this.tblSections.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSections.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tblSections.Size = new System.Drawing.Size(1239, 614);
            this.tblSections.TabIndex = 1;
            // 
            // secDaily
            // 
            this.secDaily.BackColor = System.Drawing.Color.Transparent;
            this.secDaily.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secDaily.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secDaily.Controls.Add(this.tblDaily);
            this.secDaily.CornerRadius = 10;
            this.secDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secDaily.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(190)))), ((int)(((byte)(212)))), ((int)(((byte)(240)))));
            this.secDaily.Font = new System.Drawing.Font("BIZ UDPゴシック", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.secDaily.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(42)))), ((int)(((byte)(100)))), ((int)(((byte)(166)))));
            this.secDaily.HeaderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(84)))), ((int)(((byte)(146)))), ((int)(((byte)(200)))));
            this.secDaily.HeaderHeight = 42;
            this.secDaily.HeaderIcon = App.Controls.MenuIconKind.Calendar;
            this.secDaily.HeaderIconLeft = 22;
            this.secDaily.HeaderIconLineWeight = 0.8F;
            this.secDaily.HeaderIconSize = 24;
            this.secDaily.Location = new System.Drawing.Point(0, 0);
            this.secDaily.Margin = new System.Windows.Forms.Padding(0, 0, 17, 24);
            this.secDaily.Name = "secDaily";
            this.secDaily.Padding = new System.Windows.Forms.Padding(14, 17, 13, 10);
            this.secDaily.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secDaily.ShadowSize = 10;
            this.secDaily.Size = new System.Drawing.Size(887, 415);
            this.secDaily.TabIndex = 0;
            this.secDaily.Title = "日常処理";
            // 
            // tblDaily
            // 
            this.tblDaily.ColumnCount = 5;
            this.tblDaily.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblDaily.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblDaily.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblDaily.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblDaily.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblDaily.Controls.Add(this.stpPrepare, 0, 0);
            this.tblDaily.Controls.Add(this.stpEntry, 2, 0);
            this.tblDaily.Controls.Add(this.stpOutput, 4, 0);
            this.tblDaily.Controls.Add(this.tblPrepare, 0, 1);
            this.tblDaily.Controls.Add(this.sepDailyColumn1, 1, 1);
            this.tblDaily.Controls.Add(this.tblEntry, 2, 1);
            this.tblDaily.Controls.Add(this.sepDailyColumn2, 3, 1);
            this.tblDaily.Controls.Add(this.tblOutput, 4, 1);
            this.tblDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDaily.Font = new System.Drawing.Font("游ゴシック", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tblDaily.Location = new System.Drawing.Point(18, 71);
            this.tblDaily.Margin = new System.Windows.Forms.Padding(4);
            this.tblDaily.Name = "tblDaily";
            this.tblDaily.RowCount = 2;
            this.tblDaily.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tblDaily.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDaily.Size = new System.Drawing.Size(852, 322);
            this.tblDaily.TabIndex = 0;
            // 
            // stpPrepare
            // 
            this.tblDaily.SetColumnSpan(this.stpPrepare, 2);
            this.stpPrepare.ConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(46)))), ((int)(((byte)(109)))), ((int)(((byte)(154)))));
            this.stpPrepare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stpPrepare.Font = new System.Drawing.Font("BIZ UDPゴシック", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.stpPrepare.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(42)))), ((int)(((byte)(74)))));
            this.stpPrepare.Location = new System.Drawing.Point(2, 0);
            this.stpPrepare.Margin = new System.Windows.Forms.Padding(2, 0, 7, 12);
            this.stpPrepare.Name = "stpPrepare";
            this.stpPrepare.Size = new System.Drawing.Size(287, 41);
            this.stpPrepare.StepText = "01";
            this.stpPrepare.TabIndex = 100;
            this.stpPrepare.TabStop = false;
            this.stpPrepare.Text = "準備";
            // 
            // stpEntry
            // 
            this.stpEntry.BadgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(112)))), ((int)(((byte)(140)))));
            this.tblDaily.SetColumnSpan(this.stpEntry, 2);
            this.stpEntry.ConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(40)))), ((int)(((byte)(122)))), ((int)(((byte)(138)))));
            this.stpEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stpEntry.Font = new System.Drawing.Font("BIZ UDPゴシック", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.stpEntry.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(42)))), ((int)(((byte)(74)))));
            this.stpEntry.Location = new System.Drawing.Point(298, 0);
            this.stpEntry.Margin = new System.Windows.Forms.Padding(2, 0, 7, 12);
            this.stpEntry.Name = "stpEntry";
            this.stpEntry.Size = new System.Drawing.Size(287, 41);
            this.stpEntry.StepText = "02";
            this.stpEntry.TabIndex = 102;
            this.stpEntry.TabStop = false;
            this.stpEntry.Text = "入力・確認";
            // 
            // stpOutput
            // 
            this.stpOutput.BadgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(128)))), ((int)(((byte)(120)))));
            this.stpOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stpOutput.Font = new System.Drawing.Font("BIZ UDPゴシック", 13.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.stpOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(42)))), ((int)(((byte)(74)))));
            this.stpOutput.Location = new System.Drawing.Point(594, 0);
            this.stpOutput.Margin = new System.Windows.Forms.Padding(2, 0, 2, 12);
            this.stpOutput.Name = "stpOutput";
            this.stpOutput.Size = new System.Drawing.Size(256, 41);
            this.stpOutput.StepText = "03";
            this.stpOutput.TabIndex = 104;
            this.stpOutput.TabStop = false;
            this.stpOutput.Text = "計算・出力";
            // 
            // tblPrepare
            // 
            this.tblPrepare.ColumnCount = 1;
            this.tblPrepare.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPrepare.Controls.Add(this.btnSelectMonth, 0, 0);
            this.tblPrepare.Controls.Add(this.btnStaffEntry, 0, 1);
            this.tblPrepare.Controls.Add(this.btnCalendarSetting, 0, 2);
            this.tblPrepare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPrepare.Location = new System.Drawing.Point(0, 53);
            this.tblPrepare.Margin = new System.Windows.Forms.Padding(0);
            this.tblPrepare.Name = "tblPrepare";
            this.tblPrepare.RowCount = 4;
            this.tblPrepare.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tblPrepare.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tblPrepare.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tblPrepare.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPrepare.Size = new System.Drawing.Size(260, 269);
            this.tblPrepare.TabIndex = 0;
            // 
            // btnSelectMonth
            // 
            this.btnSelectMonth.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSelectMonth.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnSelectMonth.CornerRadius = 9;
            this.btnSelectMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectMonth.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSelectMonth.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSelectMonth.Font = new System.Drawing.Font("BIZ UDPゴシック", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSelectMonth.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(206)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.btnSelectMonth.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(86)))), ((int)(((byte)(150)))));
            this.btnSelectMonth.IconKind = App.Controls.MenuIconKind.Calendar;
            this.btnSelectMonth.IconLineWeight = 0.75F;
            this.btnSelectMonth.IconSize = 26;
            this.btnSelectMonth.Location = new System.Drawing.Point(0, 0);
            this.btnSelectMonth.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btnSelectMonth.Name = "btnSelectMonth";
            this.btnSelectMonth.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnSelectMonth.ShadowSize = 6;
            this.btnSelectMonth.Size = new System.Drawing.Size(260, 71);
            this.btnSelectMonth.TabIndex = 0;
            this.btnSelectMonth.Text = "処理月度の選択";
            this.btnSelectMonth.TextGap = 15;
            // 
            // btnStaffEntry
            // 
            this.btnStaffEntry.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnStaffEntry.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnStaffEntry.CornerRadius = 9;
            this.btnStaffEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStaffEntry.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnStaffEntry.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnStaffEntry.Font = new System.Drawing.Font("BIZ UDPゴシック", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnStaffEntry.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(206)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.btnStaffEntry.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(86)))), ((int)(((byte)(150)))));
            this.btnStaffEntry.IconKind = App.Controls.MenuIconKind.Users;
            this.btnStaffEntry.IconLineWeight = 0.75F;
            this.btnStaffEntry.IconSize = 26;
            this.btnStaffEntry.Location = new System.Drawing.Point(0, 77);
            this.btnStaffEntry.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btnStaffEntry.Name = "btnStaffEntry";
            this.btnStaffEntry.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnStaffEntry.ShadowSize = 6;
            this.btnStaffEntry.Size = new System.Drawing.Size(260, 71);
            this.btnStaffEntry.TabIndex = 1;
            this.btnStaffEntry.Text = "スタッフ登録";
            this.btnStaffEntry.TextGap = 15;
            // 
            // btnCalendarSetting
            // 
            this.btnCalendarSetting.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCalendarSetting.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnCalendarSetting.CornerRadius = 9;
            this.btnCalendarSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCalendarSetting.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCalendarSetting.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCalendarSetting.Font = new System.Drawing.Font("BIZ UDPゴシック", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCalendarSetting.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(206)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.btnCalendarSetting.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(86)))), ((int)(((byte)(150)))));
            this.btnCalendarSetting.IconKind = App.Controls.MenuIconKind.Calendar;
            this.btnCalendarSetting.IconLineWeight = 0.75F;
            this.btnCalendarSetting.IconSize = 26;
            this.btnCalendarSetting.Location = new System.Drawing.Point(0, 154);
            this.btnCalendarSetting.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btnCalendarSetting.Name = "btnCalendarSetting";
            this.btnCalendarSetting.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnCalendarSetting.ShadowSize = 6;
            this.btnCalendarSetting.Size = new System.Drawing.Size(260, 71);
            this.btnCalendarSetting.TabIndex = 2;
            this.btnCalendarSetting.Text = "カレンダー設定";
            this.btnCalendarSetting.TextGap = 15;
            // 
            // sepDailyColumn1
            // 
            this.sepDailyColumn1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.sepDailyColumn1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.sepDailyColumn1.Location = new System.Drawing.Point(277, 55);
            this.sepDailyColumn1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 10);
            this.sepDailyColumn1.Name = "sepDailyColumn1";
            this.sepDailyColumn1.Size = new System.Drawing.Size(1, 257);
            this.sepDailyColumn1.TabIndex = 105;
            // 
            // tblEntry
            // 
            this.tblEntry.ColumnCount = 1;
            this.tblEntry.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEntry.Controls.Add(this.btnWorkDataEntry, 0, 0);
            this.tblEntry.Controls.Add(this.btnWorkDataCsvImport, 0, 1);
            this.tblEntry.Controls.Add(this.btnWorkCheckList, 0, 2);
            this.tblEntry.Controls.Add(this.btnAllowanceCheckList, 0, 3);
            this.tblEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblEntry.Location = new System.Drawing.Point(296, 53);
            this.tblEntry.Margin = new System.Windows.Forms.Padding(0);
            this.tblEntry.Name = "tblEntry";
            this.tblEntry.RowCount = 5;
            this.tblEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tblEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tblEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tblEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tblEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEntry.Size = new System.Drawing.Size(260, 269);
            this.tblEntry.TabIndex = 1;
            // 
            // btnWorkDataEntry
            // 
            this.btnWorkDataEntry.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnWorkDataEntry.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnWorkDataEntry.CornerRadius = 9;
            this.btnWorkDataEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWorkDataEntry.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnWorkDataEntry.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnWorkDataEntry.Font = new System.Drawing.Font("BIZ UDPゴシック", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnWorkDataEntry.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(205)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.btnWorkDataEntry.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(104)))), ((int)(((byte)(133)))));
            this.btnWorkDataEntry.IconKind = App.Controls.MenuIconKind.Laptop;
            this.btnWorkDataEntry.IconLineWeight = 0.75F;
            this.btnWorkDataEntry.IconSize = 26;
            this.btnWorkDataEntry.Location = new System.Drawing.Point(0, 0);
            this.btnWorkDataEntry.Margin = new System.Windows.Forms.Padding(0);
            this.btnWorkDataEntry.Name = "btnWorkDataEntry";
            this.btnWorkDataEntry.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnWorkDataEntry.ShadowSize = 6;
            this.btnWorkDataEntry.Size = new System.Drawing.Size(260, 62);
            this.btnWorkDataEntry.TabIndex = 0;
            this.btnWorkDataEntry.Text = "勤務データ入力";
            this.btnWorkDataEntry.TextGap = 15;
            // 
            // btnWorkDataCsvImport
            // 
            this.btnWorkDataCsvImport.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnWorkDataCsvImport.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnWorkDataCsvImport.CornerRadius = 9;
            this.btnWorkDataCsvImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWorkDataCsvImport.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnWorkDataCsvImport.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnWorkDataCsvImport.Font = new System.Drawing.Font("BIZ UDPゴシック", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnWorkDataCsvImport.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(205)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.btnWorkDataCsvImport.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(104)))), ((int)(((byte)(133)))));
            this.btnWorkDataCsvImport.IconKind = App.Controls.MenuIconKind.FileDown;
            this.btnWorkDataCsvImport.IconLineWeight = 0.75F;
            this.btnWorkDataCsvImport.IconSize = 26;
            this.btnWorkDataCsvImport.Location = new System.Drawing.Point(0, 62);
            this.btnWorkDataCsvImport.Margin = new System.Windows.Forms.Padding(0);
            this.btnWorkDataCsvImport.Name = "btnWorkDataCsvImport";
            this.btnWorkDataCsvImport.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnWorkDataCsvImport.ShadowSize = 6;
            this.btnWorkDataCsvImport.Size = new System.Drawing.Size(260, 62);
            this.btnWorkDataCsvImport.TabIndex = 1;
            this.btnWorkDataCsvImport.Text = "勤務データCSV取込";
            this.btnWorkDataCsvImport.TextGap = 15;
            // 
            // btnWorkCheckList
            // 
            this.btnWorkCheckList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnWorkCheckList.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnWorkCheckList.CornerRadius = 9;
            this.btnWorkCheckList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWorkCheckList.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnWorkCheckList.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnWorkCheckList.Font = new System.Drawing.Font("BIZ UDPゴシック", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnWorkCheckList.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(205)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.btnWorkCheckList.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(104)))), ((int)(((byte)(133)))));
            this.btnWorkCheckList.IconKind = App.Controls.MenuIconKind.ListChecks;
            this.btnWorkCheckList.IconLineWeight = 0.75F;
            this.btnWorkCheckList.IconSize = 26;
            this.btnWorkCheckList.Location = new System.Drawing.Point(0, 124);
            this.btnWorkCheckList.Margin = new System.Windows.Forms.Padding(0);
            this.btnWorkCheckList.Name = "btnWorkCheckList";
            this.btnWorkCheckList.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnWorkCheckList.ShadowSize = 6;
            this.btnWorkCheckList.Size = new System.Drawing.Size(260, 62);
            this.btnWorkCheckList.TabIndex = 2;
            this.btnWorkCheckList.Text = "勤務チェック一覧表";
            this.btnWorkCheckList.TextGap = 15;
            // 
            // btnAllowanceCheckList
            // 
            this.btnAllowanceCheckList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnAllowanceCheckList.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnAllowanceCheckList.CornerRadius = 9;
            this.btnAllowanceCheckList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAllowanceCheckList.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnAllowanceCheckList.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnAllowanceCheckList.Font = new System.Drawing.Font("BIZ UDPゴシック", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnAllowanceCheckList.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(205)))), ((int)(((byte)(233)))), ((int)(((byte)(240)))));
            this.btnAllowanceCheckList.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(104)))), ((int)(((byte)(133)))));
            this.btnAllowanceCheckList.IconKind = App.Controls.MenuIconKind.ListChecks;
            this.btnAllowanceCheckList.IconLineWeight = 0.75F;
            this.btnAllowanceCheckList.IconSize = 26;
            this.btnAllowanceCheckList.Location = new System.Drawing.Point(0, 186);
            this.btnAllowanceCheckList.Margin = new System.Windows.Forms.Padding(0);
            this.btnAllowanceCheckList.Name = "btnAllowanceCheckList";
            this.btnAllowanceCheckList.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnAllowanceCheckList.ShadowSize = 6;
            this.btnAllowanceCheckList.Size = new System.Drawing.Size(260, 62);
            this.btnAllowanceCheckList.TabIndex = 3;
            this.btnAllowanceCheckList.Text = "手当チェック一覧表";
            this.btnAllowanceCheckList.TextGap = 15;
            // 
            // sepDailyColumn2
            // 
            this.sepDailyColumn2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.sepDailyColumn2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.sepDailyColumn2.Location = new System.Drawing.Point(573, 55);
            this.sepDailyColumn2.Margin = new System.Windows.Forms.Padding(0, 2, 0, 10);
            this.sepDailyColumn2.Name = "sepDailyColumn2";
            this.sepDailyColumn2.Size = new System.Drawing.Size(1, 257);
            this.sepDailyColumn2.TabIndex = 106;
            // 
            // tblOutput
            // 
            this.tblOutput.ColumnCount = 1;
            this.tblOutput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblOutput.Controls.Add(this.btnPayrollDataCreate, 0, 0);
            this.tblOutput.Controls.Add(this.btnPayslipPrint, 0, 1);
            this.tblOutput.Controls.Add(this.btnFbDataCreate, 0, 2);
            this.tblOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblOutput.Location = new System.Drawing.Point(592, 53);
            this.tblOutput.Margin = new System.Windows.Forms.Padding(0);
            this.tblOutput.Name = "tblOutput";
            this.tblOutput.RowCount = 4;
            this.tblOutput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tblOutput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tblOutput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tblOutput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblOutput.Size = new System.Drawing.Size(260, 269);
            this.tblOutput.TabIndex = 2;
            // 
            // btnPayrollDataCreate
            // 
            this.btnPayrollDataCreate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPayrollDataCreate.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnPayrollDataCreate.CornerRadius = 9;
            this.btnPayrollDataCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPayrollDataCreate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPayrollDataCreate.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPayrollDataCreate.Font = new System.Drawing.Font("BIZ UDPゴシック", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnPayrollDataCreate.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(230)))));
            this.btnPayrollDataCreate.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(122)))), ((int)(((byte)(116)))));
            this.btnPayrollDataCreate.IconKind = App.Controls.MenuIconKind.Calculator;
            this.btnPayrollDataCreate.IconLineWeight = 0.75F;
            this.btnPayrollDataCreate.IconSize = 26;
            this.btnPayrollDataCreate.Location = new System.Drawing.Point(0, 0);
            this.btnPayrollDataCreate.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btnPayrollDataCreate.Name = "btnPayrollDataCreate";
            this.btnPayrollDataCreate.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnPayrollDataCreate.ShadowSize = 6;
            this.btnPayrollDataCreate.Size = new System.Drawing.Size(260, 71);
            this.btnPayrollDataCreate.TabIndex = 0;
            this.btnPayrollDataCreate.Text = "給与計算データ作成";
            this.btnPayrollDataCreate.TextGap = 15;
            // 
            // btnPayslipPrint
            // 
            this.btnPayslipPrint.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPayslipPrint.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnPayslipPrint.CornerRadius = 9;
            this.btnPayslipPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPayslipPrint.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPayslipPrint.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPayslipPrint.Font = new System.Drawing.Font("BIZ UDPゴシック", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnPayslipPrint.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(230)))));
            this.btnPayslipPrint.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(122)))), ((int)(((byte)(116)))));
            this.btnPayslipPrint.IconKind = App.Controls.MenuIconKind.Printer;
            this.btnPayslipPrint.IconLineWeight = 0.75F;
            this.btnPayslipPrint.IconSize = 26;
            this.btnPayslipPrint.Location = new System.Drawing.Point(0, 77);
            this.btnPayslipPrint.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btnPayslipPrint.Name = "btnPayslipPrint";
            this.btnPayslipPrint.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnPayslipPrint.ShadowSize = 6;
            this.btnPayslipPrint.Size = new System.Drawing.Size(260, 71);
            this.btnPayslipPrint.TabIndex = 1;
            this.btnPayslipPrint.Text = "明細書印刷";
            this.btnPayslipPrint.TextGap = 15;
            // 
            // btnFbDataCreate
            // 
            this.btnFbDataCreate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnFbDataCreate.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnFbDataCreate.CornerRadius = 9;
            this.btnFbDataCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFbDataCreate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnFbDataCreate.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnFbDataCreate.Font = new System.Drawing.Font("BIZ UDPゴシック", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFbDataCreate.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(204)))), ((int)(((byte)(236)))), ((int)(((byte)(230)))));
            this.btnFbDataCreate.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(122)))), ((int)(((byte)(116)))));
            this.btnFbDataCreate.IconKind = App.Controls.MenuIconKind.Bank;
            this.btnFbDataCreate.IconLineWeight = 0.75F;
            this.btnFbDataCreate.IconSize = 26;
            this.btnFbDataCreate.Location = new System.Drawing.Point(0, 154);
            this.btnFbDataCreate.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btnFbDataCreate.Name = "btnFbDataCreate";
            this.btnFbDataCreate.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnFbDataCreate.ShadowSize = 6;
            this.btnFbDataCreate.Size = new System.Drawing.Size(260, 71);
            this.btnFbDataCreate.TabIndex = 2;
            this.btnFbDataCreate.Text = "FBデータ作成";
            this.btnFbDataCreate.TextGap = 15;
            // 
            // secYearEnd
            // 
            this.secYearEnd.BackColor = System.Drawing.Color.Transparent;
            this.secYearEnd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secYearEnd.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secYearEnd.Controls.Add(this.tblYearEnd);
            this.secYearEnd.CornerRadius = 10;
            this.secYearEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secYearEnd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(192)))), ((int)(((byte)(226)))), ((int)(((byte)(208)))));
            this.secYearEnd.Font = new System.Drawing.Font("BIZ UDPゴシック", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.secYearEnd.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(112)))), ((int)(((byte)(210)))), ((int)(((byte)(142)))));
            this.secYearEnd.HeaderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(124)))), ((int)(((byte)(208)))), ((int)(((byte)(138)))));
            this.secYearEnd.HeaderHeight = 42;
            this.secYearEnd.HeaderIcon = App.Controls.MenuIconKind.FileText;
            this.secYearEnd.HeaderIconLeft = 22;
            this.secYearEnd.HeaderIconLineWeight = 0.8F;
            this.secYearEnd.HeaderIconSize = 24;
            this.secYearEnd.Location = new System.Drawing.Point(904, 0);
            this.secYearEnd.Margin = new System.Windows.Forms.Padding(0, 0, 0, 24);
            this.secYearEnd.Name = "secYearEnd";
            this.secYearEnd.Padding = new System.Windows.Forms.Padding(14, 17, 14, 10);
            this.secYearEnd.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secYearEnd.ShadowSize = 10;
            this.secYearEnd.Size = new System.Drawing.Size(335, 415);
            this.secYearEnd.TabIndex = 1;
            this.secYearEnd.Title = "年調処理";
            // 
            // tblYearEnd
            // 
            this.tblYearEnd.ColumnCount = 1;
            this.tblYearEnd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblYearEnd.Controls.Add(this.btnWithholdingSlipPrint, 0, 0);
            this.tblYearEnd.Controls.Add(this.btnETaxCsvExport, 0, 1);
            this.tblYearEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblYearEnd.Font = new System.Drawing.Font("游ゴシック", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tblYearEnd.Location = new System.Drawing.Point(18, 71);
            this.tblYearEnd.Margin = new System.Windows.Forms.Padding(4);
            this.tblYearEnd.Name = "tblYearEnd";
            this.tblYearEnd.RowCount = 3;
            this.tblYearEnd.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tblYearEnd.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tblYearEnd.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblYearEnd.Size = new System.Drawing.Size(299, 322);
            this.tblYearEnd.TabIndex = 0;
            // 
            // btnWithholdingSlipPrint
            // 
            this.btnWithholdingSlipPrint.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnWithholdingSlipPrint.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnWithholdingSlipPrint.CornerRadius = 9;
            this.btnWithholdingSlipPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWithholdingSlipPrint.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnWithholdingSlipPrint.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnWithholdingSlipPrint.Font = new System.Drawing.Font("BIZ UDPゴシック", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnWithholdingSlipPrint.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(208)))), ((int)(((byte)(238)))), ((int)(((byte)(222)))));
            this.btnWithholdingSlipPrint.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(118)))), ((int)(((byte)(84)))));
            this.btnWithholdingSlipPrint.IconKind = App.Controls.MenuIconKind.Printer;
            this.btnWithholdingSlipPrint.IconLineWeight = 0.75F;
            this.btnWithholdingSlipPrint.IconSize = 26;
            this.btnWithholdingSlipPrint.Location = new System.Drawing.Point(0, 0);
            this.btnWithholdingSlipPrint.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.btnWithholdingSlipPrint.Name = "btnWithholdingSlipPrint";
            this.btnWithholdingSlipPrint.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnWithholdingSlipPrint.ShadowSize = 6;
            this.btnWithholdingSlipPrint.Size = new System.Drawing.Size(299, 71);
            this.btnWithholdingSlipPrint.TabIndex = 0;
            this.btnWithholdingSlipPrint.Text = "源泉徴収票印刷";
            this.btnWithholdingSlipPrint.TextGap = 15;
            // 
            // btnETaxCsvExport
            // 
            this.btnETaxCsvExport.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnETaxCsvExport.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnETaxCsvExport.CornerRadius = 9;
            this.btnETaxCsvExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnETaxCsvExport.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnETaxCsvExport.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnETaxCsvExport.Font = new System.Drawing.Font("BIZ UDPゴシック", 11.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnETaxCsvExport.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(208)))), ((int)(((byte)(238)))), ((int)(((byte)(222)))));
            this.btnETaxCsvExport.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(118)))), ((int)(((byte)(84)))));
            this.btnETaxCsvExport.IconKind = App.Controls.MenuIconKind.ExternalLink;
            this.btnETaxCsvExport.IconLineWeight = 0.75F;
            this.btnETaxCsvExport.IconSize = 26;
            this.btnETaxCsvExport.Location = new System.Drawing.Point(0, 79);
            this.btnETaxCsvExport.Margin = new System.Windows.Forms.Padding(0);
            this.btnETaxCsvExport.Name = "btnETaxCsvExport";
            this.btnETaxCsvExport.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnETaxCsvExport.ShadowSize = 6;
            this.btnETaxCsvExport.Size = new System.Drawing.Size(299, 86);
            this.btnETaxCsvExport.TabIndex = 1;
            this.btnETaxCsvExport.Text = "e-Tax / eLTAX\r\nCSV出力";
            this.btnETaxCsvExport.TextGap = 15;
            // 
            // secMaster
            // 
            this.secMaster.BackColor = System.Drawing.Color.Transparent;
            this.secMaster.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secMaster.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.tblSections.SetColumnSpan(this.secMaster, 2);
            this.secMaster.Controls.Add(this.tblMaster);
            this.secMaster.CornerRadius = 10;
            this.secMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secMaster.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(200)))), ((int)(((byte)(212)))), ((int)(((byte)(230)))));
            this.secMaster.Font = new System.Drawing.Font("BIZ UDPゴシック", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.secMaster.HeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(118)))), ((int)(((byte)(140)))), ((int)(((byte)(166)))));
            this.secMaster.HeaderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(84)))), ((int)(((byte)(106)))), ((int)(((byte)(132)))));
            this.secMaster.HeaderHeight = 38;
            this.secMaster.HeaderIcon = App.Controls.MenuIconKind.Sliders;
            this.secMaster.HeaderIconLeft = 22;
            this.secMaster.HeaderIconLineWeight = 0.8F;
            this.secMaster.HeaderIconSize = 24;
            this.secMaster.Location = new System.Drawing.Point(0, 439);
            this.secMaster.Margin = new System.Windows.Forms.Padding(0);
            this.secMaster.Name = "secMaster";
            this.secMaster.Padding = new System.Windows.Forms.Padding(11, 13, 11, 6);
            this.secMaster.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secMaster.ShadowSize = 10;
            this.secMaster.Size = new System.Drawing.Size(1239, 175);
            this.secMaster.TabIndex = 2;
            this.secMaster.Title = "マスター設定";
            // 
            // tblMaster
            // 
            this.tblMaster.ColumnCount = 7;
            this.tblMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblMaster.Controls.Add(this.btnMasterBasicInfo, 0, 0);
            this.tblMaster.Controls.Add(this.btnMasterStaffInCharge, 1, 0);
            this.tblMaster.Controls.Add(this.btnMasterJob, 2, 0);
            this.tblMaster.Controls.Add(this.btnMasterBusinessCategory, 3, 0);
            this.tblMaster.Controls.Add(this.btnMasterWorkUnitPrice, 4, 0);
            this.tblMaster.Controls.Add(this.btnMasterAllowanceUnitPrice, 5, 0);
            this.tblMaster.Controls.Add(this.btnMasterWithholdingTax, 6, 0);
            this.tblMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMaster.Font = new System.Drawing.Font("游ゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tblMaster.Location = new System.Drawing.Point(15, 63);
            this.tblMaster.Margin = new System.Windows.Forms.Padding(4);
            this.tblMaster.Name = "tblMaster";
            this.tblMaster.RowCount = 1;
            this.tblMaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMaster.Size = new System.Drawing.Size(1209, 94);
            this.tblMaster.TabIndex = 0;
            // 
            // btnMasterBasicInfo
            // 
            this.btnMasterBasicInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterBasicInfo.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnMasterBasicInfo.ContentLayout = App.Controls.CardTileLayout.Vertical;
            this.btnMasterBasicInfo.CornerRadius = 30;
            this.btnMasterBasicInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMasterBasicInfo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterBasicInfo.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterBasicInfo.Font = new System.Drawing.Font("BIZ UDPゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnMasterBasicInfo.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(212)))), ((int)(((byte)(224)))), ((int)(((byte)(240)))));
            this.btnMasterBasicInfo.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(70)))), ((int)(((byte)(112)))));
            this.btnMasterBasicInfo.IconKind = App.Controls.MenuIconKind.Gear;
            this.btnMasterBasicInfo.IconLineWeight = 0.75F;
            this.btnMasterBasicInfo.IconSize = 24;
            this.btnMasterBasicInfo.Location = new System.Drawing.Point(4, 0);
            this.btnMasterBasicInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnMasterBasicInfo.Name = "btnMasterBasicInfo";
            this.btnMasterBasicInfo.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnMasterBasicInfo.ShadowSize = 6;
            this.btnMasterBasicInfo.Size = new System.Drawing.Size(164, 94);
            this.btnMasterBasicInfo.TabIndex = 0;
            this.btnMasterBasicInfo.Text = "基本情報";
            this.btnMasterBasicInfo.TextGap = 5;
            // 
            // btnMasterStaffInCharge
            // 
            this.btnMasterStaffInCharge.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterStaffInCharge.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnMasterStaffInCharge.ContentLayout = App.Controls.CardTileLayout.Vertical;
            this.btnMasterStaffInCharge.CornerRadius = 30;
            this.btnMasterStaffInCharge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMasterStaffInCharge.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterStaffInCharge.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterStaffInCharge.Font = new System.Drawing.Font("BIZ UDPゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnMasterStaffInCharge.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(212)))), ((int)(((byte)(224)))), ((int)(((byte)(240)))));
            this.btnMasterStaffInCharge.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(70)))), ((int)(((byte)(112)))));
            this.btnMasterStaffInCharge.IconKind = App.Controls.MenuIconKind.User;
            this.btnMasterStaffInCharge.IconLineWeight = 0.75F;
            this.btnMasterStaffInCharge.IconSize = 24;
            this.btnMasterStaffInCharge.Location = new System.Drawing.Point(176, 0);
            this.btnMasterStaffInCharge.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnMasterStaffInCharge.Name = "btnMasterStaffInCharge";
            this.btnMasterStaffInCharge.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnMasterStaffInCharge.ShadowSize = 6;
            this.btnMasterStaffInCharge.Size = new System.Drawing.Size(164, 94);
            this.btnMasterStaffInCharge.TabIndex = 1;
            this.btnMasterStaffInCharge.Text = "担当者マスタ";
            this.btnMasterStaffInCharge.TextGap = 5;
            // 
            // btnMasterJob
            // 
            this.btnMasterJob.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterJob.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnMasterJob.ContentLayout = App.Controls.CardTileLayout.Vertical;
            this.btnMasterJob.CornerRadius = 30;
            this.btnMasterJob.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMasterJob.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterJob.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterJob.Font = new System.Drawing.Font("BIZ UDPゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnMasterJob.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(212)))), ((int)(((byte)(224)))), ((int)(((byte)(240)))));
            this.btnMasterJob.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(70)))), ((int)(((byte)(112)))));
            this.btnMasterJob.IconKind = App.Controls.MenuIconKind.User;
            this.btnMasterJob.IconLineWeight = 0.75F;
            this.btnMasterJob.IconSize = 24;
            this.btnMasterJob.Location = new System.Drawing.Point(348, 0);
            this.btnMasterJob.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnMasterJob.Name = "btnMasterJob";
            this.btnMasterJob.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnMasterJob.ShadowSize = 6;
            this.btnMasterJob.Size = new System.Drawing.Size(164, 94);
            this.btnMasterJob.TabIndex = 2;
            this.btnMasterJob.Text = "職務マスタ";
            this.btnMasterJob.TextGap = 5;
            // 
            // btnMasterBusinessCategory
            // 
            this.btnMasterBusinessCategory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterBusinessCategory.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnMasterBusinessCategory.ContentLayout = App.Controls.CardTileLayout.Vertical;
            this.btnMasterBusinessCategory.CornerRadius = 30;
            this.btnMasterBusinessCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMasterBusinessCategory.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterBusinessCategory.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterBusinessCategory.Font = new System.Drawing.Font("BIZ UDPゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnMasterBusinessCategory.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(212)))), ((int)(((byte)(224)))), ((int)(((byte)(240)))));
            this.btnMasterBusinessCategory.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(70)))), ((int)(((byte)(112)))));
            this.btnMasterBusinessCategory.IconKind = App.Controls.MenuIconKind.Tag;
            this.btnMasterBusinessCategory.IconLineWeight = 0.75F;
            this.btnMasterBusinessCategory.IconSize = 24;
            this.btnMasterBusinessCategory.Location = new System.Drawing.Point(520, 0);
            this.btnMasterBusinessCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnMasterBusinessCategory.Name = "btnMasterBusinessCategory";
            this.btnMasterBusinessCategory.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnMasterBusinessCategory.ShadowSize = 6;
            this.btnMasterBusinessCategory.Size = new System.Drawing.Size(164, 94);
            this.btnMasterBusinessCategory.TabIndex = 3;
            this.btnMasterBusinessCategory.Text = "事業区分マスタ";
            this.btnMasterBusinessCategory.TextGap = 5;
            // 
            // btnMasterWorkUnitPrice
            // 
            this.btnMasterWorkUnitPrice.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterWorkUnitPrice.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnMasterWorkUnitPrice.ContentLayout = App.Controls.CardTileLayout.Vertical;
            this.btnMasterWorkUnitPrice.CornerRadius = 30;
            this.btnMasterWorkUnitPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMasterWorkUnitPrice.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterWorkUnitPrice.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterWorkUnitPrice.Font = new System.Drawing.Font("BIZ UDPゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnMasterWorkUnitPrice.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(212)))), ((int)(((byte)(224)))), ((int)(((byte)(240)))));
            this.btnMasterWorkUnitPrice.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(70)))), ((int)(((byte)(112)))));
            this.btnMasterWorkUnitPrice.IconKind = App.Controls.MenuIconKind.Calculator;
            this.btnMasterWorkUnitPrice.IconLineWeight = 0.75F;
            this.btnMasterWorkUnitPrice.IconSize = 24;
            this.btnMasterWorkUnitPrice.Location = new System.Drawing.Point(692, 0);
            this.btnMasterWorkUnitPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnMasterWorkUnitPrice.Name = "btnMasterWorkUnitPrice";
            this.btnMasterWorkUnitPrice.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnMasterWorkUnitPrice.ShadowSize = 6;
            this.btnMasterWorkUnitPrice.Size = new System.Drawing.Size(164, 94);
            this.btnMasterWorkUnitPrice.TabIndex = 4;
            this.btnMasterWorkUnitPrice.Text = "勤務単価マスタ";
            this.btnMasterWorkUnitPrice.TextGap = 5;
            // 
            // btnMasterAllowanceUnitPrice
            // 
            this.btnMasterAllowanceUnitPrice.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterAllowanceUnitPrice.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnMasterAllowanceUnitPrice.ContentLayout = App.Controls.CardTileLayout.Vertical;
            this.btnMasterAllowanceUnitPrice.CornerRadius = 30;
            this.btnMasterAllowanceUnitPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMasterAllowanceUnitPrice.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterAllowanceUnitPrice.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterAllowanceUnitPrice.Font = new System.Drawing.Font("BIZ UDPゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnMasterAllowanceUnitPrice.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(212)))), ((int)(((byte)(224)))), ((int)(((byte)(240)))));
            this.btnMasterAllowanceUnitPrice.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(70)))), ((int)(((byte)(112)))));
            this.btnMasterAllowanceUnitPrice.IconKind = App.Controls.MenuIconKind.Coins;
            this.btnMasterAllowanceUnitPrice.IconLineWeight = 0.75F;
            this.btnMasterAllowanceUnitPrice.IconSize = 24;
            this.btnMasterAllowanceUnitPrice.Location = new System.Drawing.Point(864, 0);
            this.btnMasterAllowanceUnitPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnMasterAllowanceUnitPrice.Name = "btnMasterAllowanceUnitPrice";
            this.btnMasterAllowanceUnitPrice.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnMasterAllowanceUnitPrice.ShadowSize = 6;
            this.btnMasterAllowanceUnitPrice.Size = new System.Drawing.Size(164, 94);
            this.btnMasterAllowanceUnitPrice.TabIndex = 5;
            this.btnMasterAllowanceUnitPrice.Text = "手当単価マスタ";
            this.btnMasterAllowanceUnitPrice.TextGap = 5;
            // 
            // btnMasterWithholdingTax
            // 
            this.btnMasterWithholdingTax.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterWithholdingTax.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(176)))), ((int)(((byte)(190)))), ((int)(((byte)(208)))));
            this.btnMasterWithholdingTax.ContentLayout = App.Controls.CardTileLayout.Vertical;
            this.btnMasterWithholdingTax.CornerRadius = 30;
            this.btnMasterWithholdingTax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMasterWithholdingTax.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterWithholdingTax.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnMasterWithholdingTax.Font = new System.Drawing.Font("BIZ UDPゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnMasterWithholdingTax.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(212)))), ((int)(((byte)(224)))), ((int)(((byte)(240)))));
            this.btnMasterWithholdingTax.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(70)))), ((int)(((byte)(112)))));
            this.btnMasterWithholdingTax.IconKind = App.Controls.MenuIconKind.Tax;
            this.btnMasterWithholdingTax.IconLineWeight = 0.75F;
            this.btnMasterWithholdingTax.IconSize = 24;
            this.btnMasterWithholdingTax.Location = new System.Drawing.Point(1036, 0);
            this.btnMasterWithholdingTax.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnMasterWithholdingTax.Name = "btnMasterWithholdingTax";
            this.btnMasterWithholdingTax.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.btnMasterWithholdingTax.ShadowSize = 6;
            this.btnMasterWithholdingTax.Size = new System.Drawing.Size(169, 94);
            this.btnMasterWithholdingTax.TabIndex = 6;
            this.btnMasterWithholdingTax.Text = "源泉徴収税額";
            this.btnMasterWithholdingTax.TextGap = 5;
            // 
            // pnlHeading
            // 
            this.pnlHeading.Controls.Add(this.icoHeading);
            this.pnlHeading.Controls.Add(this.lblHeading);
            this.pnlHeading.Controls.Add(this.lblHeadingNote);
            this.pnlHeading.Controls.Add(this.pnlHeadingLineAccent);
            this.pnlHeading.Controls.Add(this.pnlHeadingLine);
            this.pnlHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeading.Location = new System.Drawing.Point(29, 0);
            this.pnlHeading.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeading.Name = "pnlHeading";
            this.pnlHeading.Size = new System.Drawing.Size(1239, 100);
            this.pnlHeading.TabIndex = 0;
            // 
            // icoHeading
            // 
            this.icoHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(100)))), ((int)(((byte)(161)))));
            this.icoHeading.IconKind = App.Controls.MenuIconKind.DoubleSlash;
            this.icoHeading.LineWeight = 0.9F;
            this.icoHeading.Location = new System.Drawing.Point(2, 29);
            this.icoHeading.Margin = new System.Windows.Forms.Padding(4);
            this.icoHeading.Name = "icoHeading";
            this.icoHeading.Size = new System.Drawing.Size(36, 36);
            this.icoHeading.TabIndex = 0;
            this.icoHeading.TabStop = false;
            // 
            // lblHeading
            // 
            this.lblHeading.Font = new System.Drawing.Font("BIZ UDPゴシック", 20.03478F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(42)))), ((int)(((byte)(74)))));
            this.lblHeading.Location = new System.Drawing.Point(46, 21);
            this.lblHeading.Margin = new System.Windows.Forms.Padding(4);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(213, 53);
            this.lblHeading.TabIndex = 1;
            this.lblHeading.TabStop = false;
            this.lblHeading.Text = "業務メニュー";
            // 
            // lblHeadingNote
            // 
            this.lblHeadingNote.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHeadingNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(116)))), ((int)(((byte)(134)))));
            this.lblHeadingNote.Location = new System.Drawing.Point(273, 24);
            this.lblHeadingNote.Margin = new System.Windows.Forms.Padding(4);
            this.lblHeadingNote.Name = "lblHeadingNote";
            this.lblHeadingNote.Size = new System.Drawing.Size(383, 49);
            this.lblHeadingNote.TabIndex = 2;
            this.lblHeadingNote.TabStop = false;
            this.lblHeadingNote.Text = "毎月の業務を、左から順に";
            // 
            // pnlHeadingLineAccent
            // 
            this.pnlHeadingLineAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(100)))), ((int)(((byte)(161)))));
            this.pnlHeadingLineAccent.Location = new System.Drawing.Point(46, 74);
            this.pnlHeadingLineAccent.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeadingLineAccent.Name = "pnlHeadingLineAccent";
            this.pnlHeadingLineAccent.Size = new System.Drawing.Size(54, 3);
            this.pnlHeadingLineAccent.TabIndex = 3;
            // 
            // pnlHeadingLine
            // 
            this.pnlHeadingLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeadingLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.pnlHeadingLine.Location = new System.Drawing.Point(46, 75);
            this.pnlHeadingLine.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeadingLine.Name = "pnlHeadingLine";
            this.pnlHeadingLine.Size = new System.Drawing.Size(1189, 1);
            this.pnlHeadingLine.TabIndex = 4;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(115F, 115F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1294, 845);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(34)))), ((int)(((byte)(56)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "給与手当管理システム";
            this.pnlHeader.ResumeLayout(false);
            this.pnlToolbar.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.tblSections.ResumeLayout(false);
            this.secDaily.ResumeLayout(false);
            this.tblDaily.ResumeLayout(false);
            this.tblPrepare.ResumeLayout(false);
            this.tblEntry.ResumeLayout(false);
            this.tblOutput.ResumeLayout(false);
            this.secYearEnd.ResumeLayout(false);
            this.tblYearEnd.ResumeLayout(false);
            this.secMaster.ResumeLayout(false);
            this.tblMaster.ResumeLayout(false);
            this.pnlHeading.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private App.Controls.SectionPanel pnlHeader;
        private App.Controls.IconBox iconLogo;
        private App.Controls.SingleLineLabel lblOrganization;
        private App.Controls.BufferedPanel sepHeaderLeft;
        private App.Controls.SingleLineLabel lblAppTitle;
        private App.Controls.BufferedPanel sepHeaderMonth;
        private App.Controls.SingleLineLabel lblProcessingMonth;
        private App.Controls.BorderedComboBox cmbProcessingMonth;
        private App.Controls.BufferedPanel sepHeaderRight;
        private App.Controls.SingleLineLabel lblLoginUser;
        private App.Controls.BufferedPanel pnlHeaderBorder;
        private App.Controls.SectionPanel pnlToolbar;
        private App.Controls.ToolbarButton btnDataSave;
        private App.Controls.BufferedPanel sepToolbar1;
        private App.Controls.ToolbarButton btnDataRestore;
        private App.Controls.BufferedPanel sepToolbar2;
        private App.Controls.ToolbarButton btnRemoteSupport;
        private App.Controls.BufferedPanel sepToolbar3;
        private App.Controls.ToolbarButton btnLogout;
        private App.Controls.BufferedPanel pnlToolbarBorder;
        private App.Controls.BufferedPanel pnlContent;
        private App.Controls.BufferedTableLayoutPanel tblSections;
        private App.Controls.SectionPanel secDaily;
        private App.Controls.BufferedTableLayoutPanel tblDaily;
        private App.Controls.StepHeaderLabel stpPrepare;
        private App.Controls.StepHeaderLabel stpEntry;
        private App.Controls.StepHeaderLabel stpOutput;
        private App.Controls.BufferedTableLayoutPanel tblPrepare;
        private App.Controls.CardTileButton btnSelectMonth;
        private App.Controls.CardTileButton btnStaffEntry;
        private App.Controls.CardTileButton btnCalendarSetting;
        private App.Controls.BufferedPanel sepDailyColumn1;
        private App.Controls.BufferedTableLayoutPanel tblEntry;
        private App.Controls.CardTileButton btnWorkDataEntry;
        private App.Controls.CardTileButton btnWorkDataCsvImport;
        private App.Controls.CardTileButton btnWorkCheckList;
        private App.Controls.CardTileButton btnAllowanceCheckList;
        private App.Controls.BufferedPanel sepDailyColumn2;
        private App.Controls.BufferedTableLayoutPanel tblOutput;
        private App.Controls.CardTileButton btnPayrollDataCreate;
        private App.Controls.CardTileButton btnPayslipPrint;
        private App.Controls.CardTileButton btnFbDataCreate;
        private App.Controls.SectionPanel secYearEnd;
        private App.Controls.BufferedTableLayoutPanel tblYearEnd;
        private App.Controls.CardTileButton btnWithholdingSlipPrint;
        private App.Controls.CardTileButton btnETaxCsvExport;
        private App.Controls.SectionPanel secMaster;
        private App.Controls.BufferedTableLayoutPanel tblMaster;
        private App.Controls.CardTileButton btnMasterBasicInfo;
        private App.Controls.CardTileButton btnMasterStaffInCharge;
        private App.Controls.CardTileButton btnMasterJob;
        private App.Controls.CardTileButton btnMasterBusinessCategory;
        private App.Controls.CardTileButton btnMasterWorkUnitPrice;
        private App.Controls.CardTileButton btnMasterAllowanceUnitPrice;
        private App.Controls.CardTileButton btnMasterWithholdingTax;
        private App.Controls.BufferedPanel pnlHeading;
        private App.Controls.IconBox icoHeading;
        private App.Controls.BufferedPanel pnlHeadingLineAccent;
        private App.Controls.BufferedPanel pnlHeadingLine;
        private App.Controls.SingleLineLabel lblHeading;
        private App.Controls.SingleLineLabel lblHeadingNote;
        private Controls.SingleLineLabel singleLineLabel1;
        private Controls.SingleLineLabel singleLineLabel2;
    }
}
