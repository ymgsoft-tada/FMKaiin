
namespace App
{
    partial class FormMasterIryoKikan2_DlgEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMasterIryoKikan2_DlgEntry));
            this.funckey = new App.Controls.FunctionKeyBar();
            this.icoHeading = new App.Controls.IconBox();
            this.lblHeading = new App.Controls.SLabel();
            this.lblLastUpdate = new App.Controls.SLabel();
            this.oLastUpdate = new App.Controls.SLabel();
            this.oLastUpdateUser = new App.Controls.SLabel();
            this.secBody = new App.Controls.SectionPanel();
            this.lblCode = new App.Controls.SLabel();
            this.iCode = new App.Controls.STextBox();
            this.lblShisetsuName = new App.Controls.SLabel();
            this.iShisetsuName = new App.Controls.STextBox();
            this.lblShisetsuKana = new App.Controls.SLabel();
            this.iShisetsuKana = new App.Controls.STextBox();
            this.lblShisetsuTsusho = new App.Controls.SLabel();
            this.iShisetsuTsusho = new App.Controls.STextBox();
            this.lblShisetsuTsushoKana = new App.Controls.SLabel();
            this.iShisetsuTsushoKana = new App.Controls.STextBox();
            this.lblPost = new App.Controls.SLabel();
            this.iPost1 = new App.Controls.STextBox();
            this.lblPostDash = new App.Controls.SLabel();
            this.iPost2 = new App.Controls.STextBox();
            this.lblAddr = new App.Controls.SLabel();
            this.iAddr1 = new App.Controls.STextBox();
            this.iAddr2 = new App.Controls.STextBox();
            this.lblTel1 = new App.Controls.SLabel();
            this.iTel1_1 = new App.Controls.STextBox();
            this.lblTel1Dash1 = new App.Controls.SLabel();
            this.iTel1_2 = new App.Controls.STextBox();
            this.lblTel1Dash2 = new App.Controls.SLabel();
            this.iTel1_3 = new App.Controls.STextBox();
            this.lblTel2 = new App.Controls.SLabel();
            this.iTel2_1 = new App.Controls.STextBox();
            this.lblTel2Dash1 = new App.Controls.SLabel();
            this.iTel2_2 = new App.Controls.STextBox();
            this.lblTel2Dash2 = new App.Controls.SLabel();
            this.iTel2_3 = new App.Controls.STextBox();
            this.lblKaisetsushutai = new App.Controls.SLabel();
            this.iKaisetsushutai = new App.Controls.STableComboBox();
            this.lblByoshoUmu = new App.Controls.SLabel();
            this.iByoshoUmu = new App.Controls.SComboBox();
            this.lblKyokabyosho = new App.Controls.SLabel();
            this.iKyokabyosho = new App.Controls.STextBox();
            this.lblHeisetsu = new App.Controls.SLabel();
            this.chkKaigo = new App.Controls.SCheckBox();
            this.chkEtc = new App.Controls.SCheckBox();
            this.iHeisetsu = new App.Controls.STextBox();
            this.lblKumiCode = new App.Controls.SLabel();
            this.iKumiCode = new App.Controls.STableComboBox();
            this.lblTaikaiKbn = new App.Controls.SLabel();
            this.iTaikaiKbn = new App.Controls.SComboBox();
            this.lblHyoboKamoku = new App.Controls.SLabel();
            this.btnKamokuAdd = new App.Controls.SButton();
            this.btnKamokuDel = new App.Controls.SButton();
            this.iHyoboKamoku = new App.Controls.STableComboBox();
            this.iShinryoKamokuChk = new App.Controls.SCheckBox();
            this.grid_Sinryo = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.secBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid_Sinryo)).BeginInit();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 604);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(890, 66);
            this.funckey.TabIndex = 1;
            // 
            // icoHeading
            // 
            this.icoHeading.BackColor = System.Drawing.Color.Transparent;
            this.icoHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(100)))), ((int)(((byte)(161)))));
            this.icoHeading.IconKind = App.Controls.MenuIconKind.DoubleSlash;
            this.icoHeading.LineWeight = 0.9F;
            this.icoHeading.Location = new System.Drawing.Point(16, 13);
            this.icoHeading.Name = "icoHeading";
            this.icoHeading.Size = new System.Drawing.Size(24, 24);
            this.icoHeading.TabIndex = 2;
            this.icoHeading.TabStop = false;
            // 
            // lblHeading
            // 
            this.lblHeading.BackColor = System.Drawing.Color.Transparent;
            this.lblHeading.Font = new System.Drawing.Font("BIZ UDPゴシック", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(42)))), ((int)(((byte)(74)))));
            this.lblHeading.Location = new System.Drawing.Point(46, 10);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(300, 30);
            this.lblHeading.TabIndex = 3;
            this.lblHeading.TabStop = false;
            this.lblHeading.Text = "医療機関情報";
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.BackColor = System.Drawing.Color.Transparent;
            this.lblLastUpdate.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLastUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblLastUpdate.Location = new System.Drawing.Point(640, 6);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(106, 18);
            this.lblLastUpdate.TabIndex = 100;
            this.lblLastUpdate.TabStop = false;
            this.lblLastUpdate.Text = "最終更新:";
            this.lblLastUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oLastUpdate
            // 
            this.oLastUpdate.BackColor = System.Drawing.Color.Transparent;
            this.oLastUpdate.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.oLastUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.oLastUpdate.Location = new System.Drawing.Point(750, 6);
            this.oLastUpdate.Name = "oLastUpdate";
            this.oLastUpdate.Size = new System.Drawing.Size(130, 18);
            this.oLastUpdate.TabIndex = 101;
            this.oLastUpdate.TabStop = false;
            this.oLastUpdate.Text = "YYYY/MM/DD hh:mm";
            this.oLastUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oLastUpdateUser
            // 
            this.oLastUpdateUser.BackColor = System.Drawing.Color.Transparent;
            this.oLastUpdateUser.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.oLastUpdateUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.oLastUpdateUser.Location = new System.Drawing.Point(570, 24);
            this.oLastUpdateUser.Name = "oLastUpdateUser";
            this.oLastUpdateUser.Size = new System.Drawing.Size(310, 18);
            this.oLastUpdateUser.TabIndex = 102;
            this.oLastUpdateUser.TabStop = false;
            this.oLastUpdateUser.Text = "name";
            this.oLastUpdateUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblCode);
            this.secBody.Controls.Add(this.iCode);
            this.secBody.Controls.Add(this.lblShisetsuName);
            this.secBody.Controls.Add(this.iShisetsuName);
            this.secBody.Controls.Add(this.lblShisetsuKana);
            this.secBody.Controls.Add(this.iShisetsuKana);
            this.secBody.Controls.Add(this.lblShisetsuTsusho);
            this.secBody.Controls.Add(this.iShisetsuTsusho);
            this.secBody.Controls.Add(this.lblShisetsuTsushoKana);
            this.secBody.Controls.Add(this.iShisetsuTsushoKana);
            this.secBody.Controls.Add(this.lblPost);
            this.secBody.Controls.Add(this.iPost1);
            this.secBody.Controls.Add(this.lblPostDash);
            this.secBody.Controls.Add(this.iPost2);
            this.secBody.Controls.Add(this.lblAddr);
            this.secBody.Controls.Add(this.iAddr1);
            this.secBody.Controls.Add(this.iAddr2);
            this.secBody.Controls.Add(this.lblTel1);
            this.secBody.Controls.Add(this.iTel1_1);
            this.secBody.Controls.Add(this.lblTel1Dash1);
            this.secBody.Controls.Add(this.iTel1_2);
            this.secBody.Controls.Add(this.lblTel1Dash2);
            this.secBody.Controls.Add(this.iTel1_3);
            this.secBody.Controls.Add(this.lblTel2);
            this.secBody.Controls.Add(this.iTel2_1);
            this.secBody.Controls.Add(this.lblTel2Dash1);
            this.secBody.Controls.Add(this.iTel2_2);
            this.secBody.Controls.Add(this.lblTel2Dash2);
            this.secBody.Controls.Add(this.iTel2_3);
            this.secBody.Controls.Add(this.lblKaisetsushutai);
            this.secBody.Controls.Add(this.iKaisetsushutai);
            this.secBody.Controls.Add(this.lblByoshoUmu);
            this.secBody.Controls.Add(this.iByoshoUmu);
            this.secBody.Controls.Add(this.lblKyokabyosho);
            this.secBody.Controls.Add(this.iKyokabyosho);
            this.secBody.Controls.Add(this.lblHeisetsu);
            this.secBody.Controls.Add(this.chkKaigo);
            this.secBody.Controls.Add(this.chkEtc);
            this.secBody.Controls.Add(this.iHeisetsu);
            this.secBody.Controls.Add(this.lblKumiCode);
            this.secBody.Controls.Add(this.iKumiCode);
            this.secBody.Controls.Add(this.lblTaikaiKbn);
            this.secBody.Controls.Add(this.iTaikaiKbn);
            this.secBody.Controls.Add(this.lblHyoboKamoku);
            this.secBody.Controls.Add(this.btnKamokuAdd);
            this.secBody.Controls.Add(this.btnKamokuDel);
            this.secBody.Controls.Add(this.iHyoboKamoku);
            this.secBody.Controls.Add(this.iShinryoKamokuChk);
            this.secBody.Controls.Add(this.grid_Sinryo);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(870, 538);
            this.secBody.TabIndex = 0;
            // 
            // lblCode
            // 
            this.lblCode.BackColor = System.Drawing.Color.Transparent;
            this.lblCode.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblCode.Location = new System.Drawing.Point(26, 24);
            this.lblCode.Name = "lblCode";
            this.lblCode.Required = true;
            this.lblCode.Size = new System.Drawing.Size(118, 24);
            this.lblCode.TabIndex = 103;
            this.lblCode.TabStop = false;
            this.lblCode.Text = "コード";
            // 
            // iCode
            // 
            this.iCode.Location = new System.Drawing.Point(150, 24);
            this.iCode.Name = "iCode";
            this.iCode.PlaceholderText = "入力必須";
            this.iCode.Size = new System.Drawing.Size(80, 24);
            this.iCode.TabIndex = 0;
            // 
            // lblShisetsuName
            // 
            this.lblShisetsuName.BackColor = System.Drawing.Color.Transparent;
            this.lblShisetsuName.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShisetsuName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblShisetsuName.Location = new System.Drawing.Point(26, 55);
            this.lblShisetsuName.Name = "lblShisetsuName";
            this.lblShisetsuName.Size = new System.Drawing.Size(118, 24);
            this.lblShisetsuName.TabIndex = 104;
            this.lblShisetsuName.TabStop = false;
            this.lblShisetsuName.Text = "施設正式名";
            // 
            // iShisetsuName
            // 
            this.iShisetsuName.Location = new System.Drawing.Point(150, 55);
            this.iShisetsuName.Name = "iShisetsuName";
            this.iShisetsuName.Size = new System.Drawing.Size(350, 24);
            this.iShisetsuName.TabIndex = 1;
            // 
            // lblShisetsuKana
            // 
            this.lblShisetsuKana.BackColor = System.Drawing.Color.Transparent;
            this.lblShisetsuKana.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShisetsuKana.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblShisetsuKana.Location = new System.Drawing.Point(26, 84);
            this.lblShisetsuKana.Name = "lblShisetsuKana";
            this.lblShisetsuKana.Size = new System.Drawing.Size(118, 24);
            this.lblShisetsuKana.TabIndex = 105;
            this.lblShisetsuKana.TabStop = false;
            this.lblShisetsuKana.Text = "施設カナ";
            // 
            // iShisetsuKana
            // 
            this.iShisetsuKana.Location = new System.Drawing.Point(150, 84);
            this.iShisetsuKana.Name = "iShisetsuKana";
            this.iShisetsuKana.Size = new System.Drawing.Size(350, 24);
            this.iShisetsuKana.TabIndex = 2;
            // 
            // lblShisetsuTsusho
            // 
            this.lblShisetsuTsusho.BackColor = System.Drawing.Color.Transparent;
            this.lblShisetsuTsusho.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShisetsuTsusho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblShisetsuTsusho.Location = new System.Drawing.Point(26, 115);
            this.lblShisetsuTsusho.Name = "lblShisetsuTsusho";
            this.lblShisetsuTsusho.Size = new System.Drawing.Size(118, 24);
            this.lblShisetsuTsusho.TabIndex = 106;
            this.lblShisetsuTsusho.TabStop = false;
            this.lblShisetsuTsusho.Text = "施設漢字通称";
            // 
            // iShisetsuTsusho
            // 
            this.iShisetsuTsusho.Location = new System.Drawing.Point(150, 115);
            this.iShisetsuTsusho.Name = "iShisetsuTsusho";
            this.iShisetsuTsusho.Size = new System.Drawing.Size(280, 24);
            this.iShisetsuTsusho.TabIndex = 3;
            // 
            // lblShisetsuTsushoKana
            // 
            this.lblShisetsuTsushoKana.BackColor = System.Drawing.Color.Transparent;
            this.lblShisetsuTsushoKana.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.765218F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShisetsuTsushoKana.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblShisetsuTsushoKana.Location = new System.Drawing.Point(26, 144);
            this.lblShisetsuTsushoKana.Name = "lblShisetsuTsushoKana";
            this.lblShisetsuTsushoKana.Size = new System.Drawing.Size(118, 24);
            this.lblShisetsuTsushoKana.TabIndex = 107;
            this.lblShisetsuTsushoKana.TabStop = false;
            this.lblShisetsuTsushoKana.Text = "施設通称カナ";
            // 
            // iShisetsuTsushoKana
            // 
            this.iShisetsuTsushoKana.Location = new System.Drawing.Point(150, 144);
            this.iShisetsuTsushoKana.Name = "iShisetsuTsushoKana";
            this.iShisetsuTsushoKana.Size = new System.Drawing.Size(280, 24);
            this.iShisetsuTsushoKana.TabIndex = 4;
            // 
            // lblPost
            // 
            this.lblPost.BackColor = System.Drawing.Color.Transparent;
            this.lblPost.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblPost.Location = new System.Drawing.Point(26, 175);
            this.lblPost.Name = "lblPost";
            this.lblPost.Size = new System.Drawing.Size(118, 24);
            this.lblPost.TabIndex = 108;
            this.lblPost.TabStop = false;
            this.lblPost.Text = "郵便番号";
            // 
            // iPost1
            // 
            this.iPost1.Location = new System.Drawing.Point(150, 175);
            this.iPost1.Name = "iPost1";
            this.iPost1.Size = new System.Drawing.Size(50, 24);
            this.iPost1.TabIndex = 5;
            // 
            // lblPostDash
            // 
            this.lblPostDash.BackColor = System.Drawing.Color.Transparent;
            this.lblPostDash.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPostDash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblPostDash.Location = new System.Drawing.Point(200, 175);
            this.lblPostDash.Name = "lblPostDash";
            this.lblPostDash.Size = new System.Drawing.Size(20, 24);
            this.lblPostDash.TabIndex = 109;
            this.lblPostDash.TabStop = false;
            this.lblPostDash.Text = "－";
            this.lblPostDash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iPost2
            // 
            this.iPost2.Location = new System.Drawing.Point(220, 175);
            this.iPost2.Name = "iPost2";
            this.iPost2.Size = new System.Drawing.Size(62, 24);
            this.iPost2.TabIndex = 6;
            // 
            // lblAddr
            // 
            this.lblAddr.BackColor = System.Drawing.Color.Transparent;
            this.lblAddr.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblAddr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblAddr.Location = new System.Drawing.Point(26, 206);
            this.lblAddr.Name = "lblAddr";
            this.lblAddr.Size = new System.Drawing.Size(118, 24);
            this.lblAddr.TabIndex = 110;
            this.lblAddr.TabStop = false;
            this.lblAddr.Text = "住所";
            // 
            // iAddr1
            // 
            this.iAddr1.Location = new System.Drawing.Point(150, 206);
            this.iAddr1.Name = "iAddr1";
            this.iAddr1.Size = new System.Drawing.Size(350, 24);
            this.iAddr1.TabIndex = 7;
            // 
            // iAddr2
            // 
            this.iAddr2.Location = new System.Drawing.Point(150, 234);
            this.iAddr2.Name = "iAddr2";
            this.iAddr2.Size = new System.Drawing.Size(350, 24);
            this.iAddr2.TabIndex = 8;
            // 
            // lblTel1
            // 
            this.lblTel1.BackColor = System.Drawing.Color.Transparent;
            this.lblTel1.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblTel1.Location = new System.Drawing.Point(26, 265);
            this.lblTel1.Name = "lblTel1";
            this.lblTel1.Size = new System.Drawing.Size(118, 24);
            this.lblTel1.TabIndex = 111;
            this.lblTel1.TabStop = false;
            this.lblTel1.Text = "電話番号";
            // 
            // iTel1_1
            // 
            this.iTel1_1.Location = new System.Drawing.Point(150, 265);
            this.iTel1_1.Name = "iTel1_1";
            this.iTel1_1.Size = new System.Drawing.Size(72, 24);
            this.iTel1_1.TabIndex = 9;
            // 
            // lblTel1Dash1
            // 
            this.lblTel1Dash1.BackColor = System.Drawing.Color.Transparent;
            this.lblTel1Dash1.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTel1Dash1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblTel1Dash1.Location = new System.Drawing.Point(222, 265);
            this.lblTel1Dash1.Name = "lblTel1Dash1";
            this.lblTel1Dash1.Size = new System.Drawing.Size(20, 24);
            this.lblTel1Dash1.TabIndex = 112;
            this.lblTel1Dash1.TabStop = false;
            this.lblTel1Dash1.Text = "－";
            this.lblTel1Dash1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iTel1_2
            // 
            this.iTel1_2.Location = new System.Drawing.Point(242, 265);
            this.iTel1_2.Name = "iTel1_2";
            this.iTel1_2.Size = new System.Drawing.Size(72, 24);
            this.iTel1_2.TabIndex = 10;
            // 
            // lblTel1Dash2
            // 
            this.lblTel1Dash2.BackColor = System.Drawing.Color.Transparent;
            this.lblTel1Dash2.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTel1Dash2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblTel1Dash2.Location = new System.Drawing.Point(314, 265);
            this.lblTel1Dash2.Name = "lblTel1Dash2";
            this.lblTel1Dash2.Size = new System.Drawing.Size(20, 24);
            this.lblTel1Dash2.TabIndex = 113;
            this.lblTel1Dash2.TabStop = false;
            this.lblTel1Dash2.Text = "－";
            this.lblTel1Dash2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iTel1_3
            // 
            this.iTel1_3.Location = new System.Drawing.Point(334, 265);
            this.iTel1_3.Name = "iTel1_3";
            this.iTel1_3.Size = new System.Drawing.Size(72, 24);
            this.iTel1_3.TabIndex = 11;
            // 
            // lblTel2
            // 
            this.lblTel2.BackColor = System.Drawing.Color.Transparent;
            this.lblTel2.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblTel2.Location = new System.Drawing.Point(26, 296);
            this.lblTel2.Name = "lblTel2";
            this.lblTel2.Size = new System.Drawing.Size(118, 24);
            this.lblTel2.TabIndex = 114;
            this.lblTel2.TabStop = false;
            this.lblTel2.Text = "FAX";
            // 
            // iTel2_1
            // 
            this.iTel2_1.Location = new System.Drawing.Point(150, 296);
            this.iTel2_1.Name = "iTel2_1";
            this.iTel2_1.Size = new System.Drawing.Size(72, 24);
            this.iTel2_1.TabIndex = 12;
            // 
            // lblTel2Dash1
            // 
            this.lblTel2Dash1.BackColor = System.Drawing.Color.Transparent;
            this.lblTel2Dash1.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTel2Dash1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblTel2Dash1.Location = new System.Drawing.Point(222, 296);
            this.lblTel2Dash1.Name = "lblTel2Dash1";
            this.lblTel2Dash1.Size = new System.Drawing.Size(20, 24);
            this.lblTel2Dash1.TabIndex = 115;
            this.lblTel2Dash1.TabStop = false;
            this.lblTel2Dash1.Text = "－";
            this.lblTel2Dash1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iTel2_2
            // 
            this.iTel2_2.Location = new System.Drawing.Point(242, 296);
            this.iTel2_2.Name = "iTel2_2";
            this.iTel2_2.Size = new System.Drawing.Size(72, 24);
            this.iTel2_2.TabIndex = 13;
            // 
            // lblTel2Dash2
            // 
            this.lblTel2Dash2.BackColor = System.Drawing.Color.Transparent;
            this.lblTel2Dash2.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTel2Dash2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblTel2Dash2.Location = new System.Drawing.Point(314, 296);
            this.lblTel2Dash2.Name = "lblTel2Dash2";
            this.lblTel2Dash2.Size = new System.Drawing.Size(20, 24);
            this.lblTel2Dash2.TabIndex = 116;
            this.lblTel2Dash2.TabStop = false;
            this.lblTel2Dash2.Text = "－";
            this.lblTel2Dash2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iTel2_3
            // 
            this.iTel2_3.Location = new System.Drawing.Point(334, 296);
            this.iTel2_3.Name = "iTel2_3";
            this.iTel2_3.Size = new System.Drawing.Size(72, 24);
            this.iTel2_3.TabIndex = 14;
            // 
            // lblKaisetsushutai
            // 
            this.lblKaisetsushutai.BackColor = System.Drawing.Color.Transparent;
            this.lblKaisetsushutai.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKaisetsushutai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblKaisetsushutai.Location = new System.Drawing.Point(26, 327);
            this.lblKaisetsushutai.Name = "lblKaisetsushutai";
            this.lblKaisetsushutai.Size = new System.Drawing.Size(118, 24);
            this.lblKaisetsushutai.TabIndex = 117;
            this.lblKaisetsushutai.TabStop = false;
            this.lblKaisetsushutai.Text = "開設主体";
            // 
            // iKaisetsushutai
            // 
            this.iKaisetsushutai.Location = new System.Drawing.Point(150, 327);
            this.iKaisetsushutai.Name = "iKaisetsushutai";
            this.iKaisetsushutai.Size = new System.Drawing.Size(200, 24);
            this.iKaisetsushutai.TabIndex = 15;
            // 
            // lblByoshoUmu
            // 
            this.lblByoshoUmu.BackColor = System.Drawing.Color.Transparent;
            this.lblByoshoUmu.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblByoshoUmu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblByoshoUmu.Location = new System.Drawing.Point(26, 358);
            this.lblByoshoUmu.Name = "lblByoshoUmu";
            this.lblByoshoUmu.Required = true;
            this.lblByoshoUmu.Size = new System.Drawing.Size(118, 24);
            this.lblByoshoUmu.TabIndex = 118;
            this.lblByoshoUmu.TabStop = false;
            this.lblByoshoUmu.Text = "病床有無";
            // 
            // iByoshoUmu
            // 
            this.iByoshoUmu.ItemHeight = 18;
            this.iByoshoUmu.Location = new System.Drawing.Point(150, 358);
            this.iByoshoUmu.Name = "iByoshoUmu";
            this.iByoshoUmu.PlaceholderText = "選択必須";
            this.iByoshoUmu.Size = new System.Drawing.Size(100, 24);
            this.iByoshoUmu.TabIndex = 16;
            // 
            // lblKyokabyosho
            // 
            this.lblKyokabyosho.BackColor = System.Drawing.Color.Transparent;
            this.lblKyokabyosho.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKyokabyosho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblKyokabyosho.Location = new System.Drawing.Point(26, 389);
            this.lblKyokabyosho.Name = "lblKyokabyosho";
            this.lblKyokabyosho.Size = new System.Drawing.Size(118, 24);
            this.lblKyokabyosho.TabIndex = 119;
            this.lblKyokabyosho.TabStop = false;
            this.lblKyokabyosho.Text = "許可病床数";
            // 
            // iKyokabyosho
            // 
            this.iKyokabyosho.Location = new System.Drawing.Point(150, 389);
            this.iKyokabyosho.Name = "iKyokabyosho";
            this.iKyokabyosho.Size = new System.Drawing.Size(66, 24);
            this.iKyokabyosho.TabIndex = 17;
            // 
            // lblHeisetsu
            // 
            this.lblHeisetsu.BackColor = System.Drawing.Color.Transparent;
            this.lblHeisetsu.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHeisetsu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblHeisetsu.Location = new System.Drawing.Point(26, 420);
            this.lblHeisetsu.Name = "lblHeisetsu";
            this.lblHeisetsu.Size = new System.Drawing.Size(118, 24);
            this.lblHeisetsu.TabIndex = 120;
            this.lblHeisetsu.TabStop = false;
            this.lblHeisetsu.Text = "併設施設";
            // 
            // chkKaigo
            // 
            this.chkKaigo.Location = new System.Drawing.Point(150, 420);
            this.chkKaigo.Name = "chkKaigo";
            this.chkKaigo.Size = new System.Drawing.Size(92, 24);
            this.chkKaigo.TabIndex = 18;
            this.chkKaigo.Text = "介護施設";
            this.chkKaigo.UseVisualStyleBackColor = false;
            // 
            // chkEtc
            // 
            this.chkEtc.Location = new System.Drawing.Point(248, 420);
            this.chkEtc.Name = "chkEtc";
            this.chkEtc.Size = new System.Drawing.Size(76, 24);
            this.chkEtc.TabIndex = 19;
            this.chkEtc.Text = "その他";
            this.chkEtc.UseVisualStyleBackColor = false;
            // 
            // iHeisetsu
            // 
            this.iHeisetsu.Location = new System.Drawing.Point(328, 420);
            this.iHeisetsu.Name = "iHeisetsu";
            this.iHeisetsu.Size = new System.Drawing.Size(172, 24);
            this.iHeisetsu.TabIndex = 20;
            // 
            // lblKumiCode
            // 
            this.lblKumiCode.BackColor = System.Drawing.Color.Transparent;
            this.lblKumiCode.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKumiCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblKumiCode.Location = new System.Drawing.Point(26, 451);
            this.lblKumiCode.Name = "lblKumiCode";
            this.lblKumiCode.Size = new System.Drawing.Size(118, 24);
            this.lblKumiCode.TabIndex = 121;
            this.lblKumiCode.TabStop = false;
            this.lblKumiCode.Text = "組コード";
            // 
            // iKumiCode
            // 
            this.iKumiCode.Location = new System.Drawing.Point(150, 451);
            this.iKumiCode.Name = "iKumiCode";
            this.iKumiCode.Size = new System.Drawing.Size(160, 24);
            this.iKumiCode.TabIndex = 21;
            // 
            // lblTaikaiKbn
            // 
            this.lblTaikaiKbn.BackColor = System.Drawing.Color.Transparent;
            this.lblTaikaiKbn.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTaikaiKbn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblTaikaiKbn.Location = new System.Drawing.Point(26, 482);
            this.lblTaikaiKbn.Name = "lblTaikaiKbn";
            this.lblTaikaiKbn.Required = true;
            this.lblTaikaiKbn.Size = new System.Drawing.Size(118, 24);
            this.lblTaikaiKbn.TabIndex = 122;
            this.lblTaikaiKbn.TabStop = false;
            this.lblTaikaiKbn.Text = "退会区分";
            // 
            // iTaikaiKbn
            // 
            this.iTaikaiKbn.ItemHeight = 18;
            this.iTaikaiKbn.Location = new System.Drawing.Point(150, 482);
            this.iTaikaiKbn.Name = "iTaikaiKbn";
            this.iTaikaiKbn.PlaceholderText = "選択必須";
            this.iTaikaiKbn.Size = new System.Drawing.Size(100, 24);
            this.iTaikaiKbn.TabIndex = 22;
            // 
            // lblHyoboKamoku
            // 
            this.lblHyoboKamoku.BackColor = System.Drawing.Color.Transparent;
            this.lblHyoboKamoku.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHyoboKamoku.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblHyoboKamoku.Location = new System.Drawing.Point(524, 24);
            this.lblHyoboKamoku.Name = "lblHyoboKamoku";
            this.lblHyoboKamoku.Size = new System.Drawing.Size(84, 24);
            this.lblHyoboKamoku.TabIndex = 123;
            this.lblHyoboKamoku.TabStop = false;
            this.lblHyoboKamoku.Text = "標榜科目";
            // 
            // btnKamokuAdd
            // 
            this.btnKamokuAdd.Location = new System.Drawing.Point(726, 24);
            this.btnKamokuAdd.Name = "btnKamokuAdd";
            this.btnKamokuAdd.Size = new System.Drawing.Size(56, 24);
            this.btnKamokuAdd.TabIndex = 26;
            this.btnKamokuAdd.TabStop = false;
            this.btnKamokuAdd.Text = "追加";
            this.btnKamokuAdd.UseVisualStyleBackColor = false;
            // 
            // btnKamokuDel
            // 
            this.btnKamokuDel.Location = new System.Drawing.Point(788, 24);
            this.btnKamokuDel.Name = "btnKamokuDel";
            this.btnKamokuDel.Size = new System.Drawing.Size(56, 24);
            this.btnKamokuDel.TabIndex = 27;
            this.btnKamokuDel.TabStop = false;
            this.btnKamokuDel.Text = "削除";
            this.btnKamokuDel.UseVisualStyleBackColor = false;
            // 
            // iHyoboKamoku
            // 
            this.iHyoboKamoku.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.iHyoboKamoku.Location = new System.Drawing.Point(534, 95);
            this.iHyoboKamoku.Name = "iHyoboKamoku";
            this.iHyoboKamoku.Size = new System.Drawing.Size(180, 24);
            this.iHyoboKamoku.TabIndex = 24;
            // 
            // iShinryoKamokuChk
            // 
            this.iShinryoKamokuChk.BackColor = System.Drawing.Color.White;
            this.iShinryoKamokuChk.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.iShinryoKamokuChk.Location = new System.Drawing.Point(774, 95);
            this.iShinryoKamokuChk.Name = "iShinryoKamokuChk";
            this.iShinryoKamokuChk.Size = new System.Drawing.Size(24, 24);
            this.iShinryoKamokuChk.TabIndex = 25;
            this.iShinryoKamokuChk.UseVisualStyleBackColor = false;
            // 
            // grid_Sinryo
            // 
            this.grid_Sinryo.CaptionHeight = 16;
            this.grid_Sinryo.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
            this.grid_Sinryo.Images.Add(((System.Drawing.Image)(resources.GetObject("grid_Sinryo.Images"))));
            this.grid_Sinryo.Location = new System.Drawing.Point(524, 55);
            this.grid_Sinryo.Name = "grid_Sinryo";
            this.grid_Sinryo.PreviewInfo.Caption = "印刷プレビューウィンドウ";
            this.grid_Sinryo.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.grid_Sinryo.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.grid_Sinryo.PreviewInfo.ZoomFactor = 75D;
            this.grid_Sinryo.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.grid_Sinryo.PrintInfo.MeasurementPrinterName = null;
            this.grid_Sinryo.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("grid_Sinryo.PrintInfo.PageSettings")));
            this.grid_Sinryo.PropBag = resources.GetString("grid_Sinryo.PropBag");
            this.grid_Sinryo.RowHeight = 14;
            this.grid_Sinryo.Size = new System.Drawing.Size(320, 451);
            this.grid_Sinryo.TabIndex = 23;
            this.grid_Sinryo.UseCompatibleTextRendering = false;
            // 
            // FormMasterIryoKikan2_DlgEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(890, 670);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.oLastUpdateUser);
            this.Controls.Add(this.oLastUpdate);
            this.Controls.Add(this.lblLastUpdate);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMasterIryoKikan2_DlgEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "医療機関マスタ";
            this.Controls.SetChildIndex(this.funckey, 0);
            this.Controls.SetChildIndex(this.icoHeading, 0);
            this.Controls.SetChildIndex(this.lblHeading, 0);
            this.Controls.SetChildIndex(this.lblLastUpdate, 0);
            this.Controls.SetChildIndex(this.oLastUpdate, 0);
            this.Controls.SetChildIndex(this.oLastUpdateUser, 0);
            this.Controls.SetChildIndex(this.secBody, 0);
            this.secBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid_Sinryo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private App.Controls.FunctionKeyBar funckey;
        private App.Controls.IconBox icoHeading;
        private App.Controls.SLabel lblHeading;
        private App.Controls.SLabel lblLastUpdate;
        private App.Controls.SLabel oLastUpdate;
        private App.Controls.SLabel oLastUpdateUser;
        private App.Controls.SectionPanel secBody;
        private App.Controls.SLabel lblCode;
        private App.Controls.STextBox iCode;
        private App.Controls.SLabel lblShisetsuName;
        private App.Controls.STextBox iShisetsuName;
        private App.Controls.SLabel lblShisetsuKana;
        private App.Controls.STextBox iShisetsuKana;
        private App.Controls.SLabel lblShisetsuTsusho;
        private App.Controls.STextBox iShisetsuTsusho;
        private App.Controls.SLabel lblShisetsuTsushoKana;
        private App.Controls.STextBox iShisetsuTsushoKana;
        private App.Controls.SLabel lblPost;
        private App.Controls.STextBox iPost1;
        private App.Controls.SLabel lblPostDash;
        private App.Controls.STextBox iPost2;
        private App.Controls.SLabel lblAddr;
        private App.Controls.STextBox iAddr1;
        private App.Controls.STextBox iAddr2;
        private App.Controls.SLabel lblTel1;
        private App.Controls.STextBox iTel1_1;
        private App.Controls.SLabel lblTel1Dash1;
        private App.Controls.STextBox iTel1_2;
        private App.Controls.SLabel lblTel1Dash2;
        private App.Controls.STextBox iTel1_3;
        private App.Controls.SLabel lblTel2;
        private App.Controls.STextBox iTel2_1;
        private App.Controls.SLabel lblTel2Dash1;
        private App.Controls.STextBox iTel2_2;
        private App.Controls.SLabel lblTel2Dash2;
        private App.Controls.STextBox iTel2_3;
        private App.Controls.SLabel lblKaisetsushutai;
        private App.Controls.STableComboBox iKaisetsushutai;
        private App.Controls.SLabel lblByoshoUmu;
        private App.Controls.SComboBox iByoshoUmu;
        private App.Controls.SLabel lblKyokabyosho;
        private App.Controls.STextBox iKyokabyosho;
        private App.Controls.SLabel lblHeisetsu;
        private App.Controls.SCheckBox chkKaigo;
        private App.Controls.SCheckBox chkEtc;
        private App.Controls.STextBox iHeisetsu;
        private App.Controls.SLabel lblKumiCode;
        private App.Controls.STableComboBox iKumiCode;
        private App.Controls.SLabel lblTaikaiKbn;
        private App.Controls.SComboBox iTaikaiKbn;
        private App.Controls.SLabel lblHyoboKamoku;
        private App.Controls.SButton btnKamokuAdd;
        private App.Controls.SButton btnKamokuDel;
        private App.Controls.STableComboBox iHyoboKamoku;
        private App.Controls.SCheckBox iShinryoKamokuChk;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid_Sinryo;
    }
}
