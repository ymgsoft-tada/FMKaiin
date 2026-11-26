
namespace App
{
    partial class FormMasterBasic2
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
            this.funckey = new ComponentSUI.FunctionKeyBar();
            this.icoHeading = new ComponentSUI.IconBox();
            this.lblHeading = new ComponentSUI.SLabel();
            this.secBody = new ComponentSUI.SectionPanel();
            this.lblName = new ComponentSUI.SLabel();
            this.iName = new ComponentSUI.STextBox();
            this.lblNameFurigana = new ComponentSUI.SLabel();
            this.iNameFurigana = new ComponentSUI.STextBox();
            this.lblNameDaihyo = new ComponentSUI.SLabel();
            this.iNameDaihyo = new ComponentSUI.STextBox();
            this.lblPost = new ComponentSUI.SLabel();
            this.iPost1 = new ComponentSUI.STextBox();
            this.lblPostDash = new ComponentSUI.SLabel();
            this.iPost2 = new ComponentSUI.STextBox();
            this.lblAddr = new ComponentSUI.SLabel();
            this.iAddr1 = new ComponentSUI.STextBox();
            this.iAddr2 = new ComponentSUI.STextBox();
            this.lblTel1 = new ComponentSUI.SLabel();
            this.iTel1_1 = new ComponentSUI.STextBox();
            this.lblTel1Dash1 = new ComponentSUI.SLabel();
            this.iTel1_2 = new ComponentSUI.STextBox();
            this.lblTel1Dash2 = new ComponentSUI.SLabel();
            this.iTel1_3 = new ComponentSUI.STextBox();
            this.lblFax = new ComponentSUI.SLabel();
            this.iFax_1 = new ComponentSUI.STextBox();
            this.lblFaxDash1 = new ComponentSUI.SLabel();
            this.iFax_2 = new ComponentSUI.STextBox();
            this.lblFaxDash2 = new ComponentSUI.SLabel();
            this.iFax_3 = new ComponentSUI.STextBox();
            this.lblHojinNo = new ComponentSUI.SLabel();
            this.iHojinNo = new ComponentSUI.STextBox();
            this.lblDispSeireki = new ComponentSUI.SLabel();
            this.iDispSeireki = new ComponentSUI.SComboBox();
            this.lblBank = new ComponentSUI.SLabel();
            this.iBank = new ComponentSUI.STableComboBox();
            this.secBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 453);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(590, 66);
            this.funckey.TabIndex = 1;
            // 
            // icoHeading
            // 
            this.icoHeading.BackColor = System.Drawing.Color.Transparent;
            this.icoHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(100)))), ((int)(((byte)(161)))));
            this.icoHeading.IconKind = ComponentSUI.MenuIconKind.DoubleSlash;
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
            this.lblHeading.Text = "基本情報";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblName);
            this.secBody.Controls.Add(this.iName);
            this.secBody.Controls.Add(this.lblNameFurigana);
            this.secBody.Controls.Add(this.iNameFurigana);
            this.secBody.Controls.Add(this.lblNameDaihyo);
            this.secBody.Controls.Add(this.iNameDaihyo);
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
            this.secBody.Controls.Add(this.lblFax);
            this.secBody.Controls.Add(this.iFax_1);
            this.secBody.Controls.Add(this.lblFaxDash1);
            this.secBody.Controls.Add(this.iFax_2);
            this.secBody.Controls.Add(this.lblFaxDash2);
            this.secBody.Controls.Add(this.iFax_3);
            this.secBody.Controls.Add(this.lblHojinNo);
            this.secBody.Controls.Add(this.iHojinNo);
            this.secBody.Controls.Add(this.lblDispSeireki);
            this.secBody.Controls.Add(this.iDispSeireki);
            this.secBody.Controls.Add(this.lblBank);
            this.secBody.Controls.Add(this.iBank);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(570, 387);
            this.secBody.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblName.Location = new System.Drawing.Point(26, 24);
            this.lblName.Name = "lblName";
            this.lblName.Required = true;
            this.lblName.Size = new System.Drawing.Size(118, 24);
            this.lblName.TabIndex = 100;
            this.lblName.TabStop = false;
            this.lblName.Text = "名称";
            // 
            // iName
            // 
            this.iName.Location = new System.Drawing.Point(150, 24);
            this.iName.Name = "iName";
            this.iName.PlaceholderText = "入力必須";
            this.iName.Size = new System.Drawing.Size(350, 24);
            this.iName.TabIndex = 0;
            // 
            // lblNameFurigana
            // 
            this.lblNameFurigana.BackColor = System.Drawing.Color.Transparent;
            this.lblNameFurigana.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNameFurigana.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(120)))), ((int)(((byte)(160)))));
            this.lblNameFurigana.Location = new System.Drawing.Point(26, 50);
            this.lblNameFurigana.Name = "lblNameFurigana";
            this.lblNameFurigana.Required = true;
            this.lblNameFurigana.Size = new System.Drawing.Size(118, 24);
            this.lblNameFurigana.TabIndex = 101;
            this.lblNameFurigana.TabStop = false;
            this.lblNameFurigana.Text = "フリガナ";
            // 
            // iNameFurigana
            // 
            this.iNameFurigana.Location = new System.Drawing.Point(150, 50);
            this.iNameFurigana.Name = "iNameFurigana";
            this.iNameFurigana.PlaceholderText = "入力必須";
            this.iNameFurigana.Size = new System.Drawing.Size(350, 24);
            this.iNameFurigana.TabIndex = 1;
            // 
            // lblNameDaihyo
            // 
            this.lblNameDaihyo.BackColor = System.Drawing.Color.Transparent;
            this.lblNameDaihyo.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNameDaihyo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblNameDaihyo.Location = new System.Drawing.Point(26, 84);
            this.lblNameDaihyo.Name = "lblNameDaihyo";
            this.lblNameDaihyo.Size = new System.Drawing.Size(118, 24);
            this.lblNameDaihyo.TabIndex = 102;
            this.lblNameDaihyo.TabStop = false;
            this.lblNameDaihyo.Text = "代表者名";
            // 
            // iNameDaihyo
            // 
            this.iNameDaihyo.Location = new System.Drawing.Point(150, 84);
            this.iNameDaihyo.Name = "iNameDaihyo";
            this.iNameDaihyo.Size = new System.Drawing.Size(208, 24);
            this.iNameDaihyo.TabIndex = 2;
            // 
            // lblPost
            // 
            this.lblPost.BackColor = System.Drawing.Color.Transparent;
            this.lblPost.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblPost.Location = new System.Drawing.Point(26, 115);
            this.lblPost.Name = "lblPost";
            this.lblPost.Size = new System.Drawing.Size(118, 24);
            this.lblPost.TabIndex = 103;
            this.lblPost.TabStop = false;
            this.lblPost.Text = "郵便番号";
            // 
            // iPost1
            // 
            this.iPost1.Location = new System.Drawing.Point(150, 115);
            this.iPost1.Name = "iPost1";
            this.iPost1.Size = new System.Drawing.Size(50, 24);
            this.iPost1.TabIndex = 3;
            // 
            // lblPostDash
            // 
            this.lblPostDash.BackColor = System.Drawing.Color.Transparent;
            this.lblPostDash.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPostDash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblPostDash.Location = new System.Drawing.Point(200, 115);
            this.lblPostDash.Name = "lblPostDash";
            this.lblPostDash.Size = new System.Drawing.Size(20, 24);
            this.lblPostDash.TabIndex = 104;
            this.lblPostDash.TabStop = false;
            this.lblPostDash.Text = "－";
            this.lblPostDash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iPost2
            // 
            this.iPost2.Location = new System.Drawing.Point(220, 115);
            this.iPost2.Name = "iPost2";
            this.iPost2.Size = new System.Drawing.Size(62, 24);
            this.iPost2.TabIndex = 4;
            // 
            // lblAddr
            // 
            this.lblAddr.BackColor = System.Drawing.Color.Transparent;
            this.lblAddr.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblAddr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblAddr.Location = new System.Drawing.Point(26, 147);
            this.lblAddr.Name = "lblAddr";
            this.lblAddr.Size = new System.Drawing.Size(118, 24);
            this.lblAddr.TabIndex = 105;
            this.lblAddr.TabStop = false;
            this.lblAddr.Text = "住所";
            // 
            // iAddr1
            // 
            this.iAddr1.Location = new System.Drawing.Point(150, 146);
            this.iAddr1.Name = "iAddr1";
            this.iAddr1.Size = new System.Drawing.Size(350, 24);
            this.iAddr1.TabIndex = 5;
            // 
            // iAddr2
            // 
            this.iAddr2.Location = new System.Drawing.Point(150, 174);
            this.iAddr2.Name = "iAddr2";
            this.iAddr2.Size = new System.Drawing.Size(350, 24);
            this.iAddr2.TabIndex = 6;
            // 
            // lblTel1
            // 
            this.lblTel1.BackColor = System.Drawing.Color.Transparent;
            this.lblTel1.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblTel1.Location = new System.Drawing.Point(26, 207);
            this.lblTel1.Name = "lblTel1";
            this.lblTel1.Size = new System.Drawing.Size(118, 24);
            this.lblTel1.TabIndex = 106;
            this.lblTel1.TabStop = false;
            this.lblTel1.Text = "電話番号";
            // 
            // iTel1_1
            // 
            this.iTel1_1.Location = new System.Drawing.Point(150, 207);
            this.iTel1_1.Name = "iTel1_1";
            this.iTel1_1.Size = new System.Drawing.Size(72, 24);
            this.iTel1_1.TabIndex = 7;
            // 
            // lblTel1Dash1
            // 
            this.lblTel1Dash1.BackColor = System.Drawing.Color.Transparent;
            this.lblTel1Dash1.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTel1Dash1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblTel1Dash1.Location = new System.Drawing.Point(222, 207);
            this.lblTel1Dash1.Name = "lblTel1Dash1";
            this.lblTel1Dash1.Size = new System.Drawing.Size(20, 24);
            this.lblTel1Dash1.TabIndex = 107;
            this.lblTel1Dash1.TabStop = false;
            this.lblTel1Dash1.Text = "－";
            this.lblTel1Dash1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iTel1_2
            // 
            this.iTel1_2.Location = new System.Drawing.Point(242, 207);
            this.iTel1_2.Name = "iTel1_2";
            this.iTel1_2.Size = new System.Drawing.Size(72, 24);
            this.iTel1_2.TabIndex = 8;
            // 
            // lblTel1Dash2
            // 
            this.lblTel1Dash2.BackColor = System.Drawing.Color.Transparent;
            this.lblTel1Dash2.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTel1Dash2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblTel1Dash2.Location = new System.Drawing.Point(314, 207);
            this.lblTel1Dash2.Name = "lblTel1Dash2";
            this.lblTel1Dash2.Size = new System.Drawing.Size(20, 24);
            this.lblTel1Dash2.TabIndex = 108;
            this.lblTel1Dash2.TabStop = false;
            this.lblTel1Dash2.Text = "－";
            this.lblTel1Dash2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iTel1_3
            // 
            this.iTel1_3.Location = new System.Drawing.Point(334, 207);
            this.iTel1_3.Name = "iTel1_3";
            this.iTel1_3.Size = new System.Drawing.Size(72, 24);
            this.iTel1_3.TabIndex = 9;
            // 
            // lblFax
            // 
            this.lblFax.BackColor = System.Drawing.Color.Transparent;
            this.lblFax.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblFax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblFax.Location = new System.Drawing.Point(26, 238);
            this.lblFax.Name = "lblFax";
            this.lblFax.Size = new System.Drawing.Size(118, 24);
            this.lblFax.TabIndex = 109;
            this.lblFax.TabStop = false;
            this.lblFax.Text = "FAX番号";
            // 
            // iFax_1
            // 
            this.iFax_1.Location = new System.Drawing.Point(150, 238);
            this.iFax_1.Name = "iFax_1";
            this.iFax_1.Size = new System.Drawing.Size(72, 24);
            this.iFax_1.TabIndex = 10;
            // 
            // lblFaxDash1
            // 
            this.lblFaxDash1.BackColor = System.Drawing.Color.Transparent;
            this.lblFaxDash1.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblFaxDash1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblFaxDash1.Location = new System.Drawing.Point(222, 238);
            this.lblFaxDash1.Name = "lblFaxDash1";
            this.lblFaxDash1.Size = new System.Drawing.Size(20, 24);
            this.lblFaxDash1.TabIndex = 110;
            this.lblFaxDash1.TabStop = false;
            this.lblFaxDash1.Text = "－";
            this.lblFaxDash1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iFax_2
            // 
            this.iFax_2.Location = new System.Drawing.Point(242, 238);
            this.iFax_2.Name = "iFax_2";
            this.iFax_2.Size = new System.Drawing.Size(72, 24);
            this.iFax_2.TabIndex = 11;
            // 
            // lblFaxDash2
            // 
            this.lblFaxDash2.BackColor = System.Drawing.Color.Transparent;
            this.lblFaxDash2.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblFaxDash2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblFaxDash2.Location = new System.Drawing.Point(314, 238);
            this.lblFaxDash2.Name = "lblFaxDash2";
            this.lblFaxDash2.Size = new System.Drawing.Size(20, 24);
            this.lblFaxDash2.TabIndex = 111;
            this.lblFaxDash2.TabStop = false;
            this.lblFaxDash2.Text = "－";
            this.lblFaxDash2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iFax_3
            // 
            this.iFax_3.Location = new System.Drawing.Point(334, 238);
            this.iFax_3.Name = "iFax_3";
            this.iFax_3.Size = new System.Drawing.Size(72, 24);
            this.iFax_3.TabIndex = 12;
            // 
            // lblHojinNo
            // 
            this.lblHojinNo.BackColor = System.Drawing.Color.Transparent;
            this.lblHojinNo.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHojinNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblHojinNo.Location = new System.Drawing.Point(26, 269);
            this.lblHojinNo.Name = "lblHojinNo";
            this.lblHojinNo.Size = new System.Drawing.Size(118, 24);
            this.lblHojinNo.TabIndex = 112;
            this.lblHojinNo.TabStop = false;
            this.lblHojinNo.Text = "法人番号";
            // 
            // iHojinNo
            // 
            this.iHojinNo.Location = new System.Drawing.Point(150, 269);
            this.iHojinNo.Name = "iHojinNo";
            this.iHojinNo.Size = new System.Drawing.Size(186, 24);
            this.iHojinNo.TabIndex = 13;
            // 
            // lblDispSeireki
            // 
            this.lblDispSeireki.BackColor = System.Drawing.Color.Transparent;
            this.lblDispSeireki.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDispSeireki.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblDispSeireki.Location = new System.Drawing.Point(26, 300);
            this.lblDispSeireki.Name = "lblDispSeireki";
            this.lblDispSeireki.Size = new System.Drawing.Size(118, 24);
            this.lblDispSeireki.TabIndex = 113;
            this.lblDispSeireki.TabStop = false;
            this.lblDispSeireki.Text = "日付表示";
            // 
            // iDispSeireki
            // 
            this.iDispSeireki.ItemHeight = 18;
            this.iDispSeireki.Location = new System.Drawing.Point(150, 300);
            this.iDispSeireki.Name = "iDispSeireki";
            this.iDispSeireki.Size = new System.Drawing.Size(80, 24);
            this.iDispSeireki.TabIndex = 14;
            // 
            // lblBank
            // 
            this.lblBank.BackColor = System.Drawing.Color.Transparent;
            this.lblBank.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblBank.Location = new System.Drawing.Point(26, 331);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(118, 24);
            this.lblBank.TabIndex = 114;
            this.lblBank.TabStop = false;
            this.lblBank.Text = "振込元銀行";
            // 
            // iBank
            // 
            this.iBank.Location = new System.Drawing.Point(150, 331);
            this.iBank.Name = "iBank";
            this.iBank.Size = new System.Drawing.Size(350, 24);
            this.iBank.TabIndex = 15;
            // 
            // FormMasterBasic2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(590, 519);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMasterBasic2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "基本情報";
            this.Controls.SetChildIndex(this.funckey, 0);
            this.Controls.SetChildIndex(this.icoHeading, 0);
            this.Controls.SetChildIndex(this.lblHeading, 0);
            this.Controls.SetChildIndex(this.secBody, 0);
            this.secBody.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentSUI.FunctionKeyBar funckey;
        private ComponentSUI.IconBox icoHeading;
        private ComponentSUI.SLabel lblHeading;
        private ComponentSUI.SectionPanel secBody;
        private ComponentSUI.SLabel lblName;
        private ComponentSUI.STextBox iName;
        private ComponentSUI.SLabel lblNameFurigana;
        private ComponentSUI.STextBox iNameFurigana;
        private ComponentSUI.SLabel lblNameDaihyo;
        private ComponentSUI.STextBox iNameDaihyo;
        private ComponentSUI.SLabel lblPost;
        private ComponentSUI.STextBox iPost1;
        private ComponentSUI.SLabel lblPostDash;
        private ComponentSUI.STextBox iPost2;
        private ComponentSUI.SLabel lblAddr;
        private ComponentSUI.STextBox iAddr1;
        private ComponentSUI.STextBox iAddr2;
        private ComponentSUI.SLabel lblTel1;
        private ComponentSUI.STextBox iTel1_1;
        private ComponentSUI.SLabel lblTel1Dash1;
        private ComponentSUI.STextBox iTel1_2;
        private ComponentSUI.SLabel lblTel1Dash2;
        private ComponentSUI.STextBox iTel1_3;
        private ComponentSUI.SLabel lblFax;
        private ComponentSUI.STextBox iFax_1;
        private ComponentSUI.SLabel lblFaxDash1;
        private ComponentSUI.STextBox iFax_2;
        private ComponentSUI.SLabel lblFaxDash2;
        private ComponentSUI.STextBox iFax_3;
        private ComponentSUI.SLabel lblHojinNo;
        private ComponentSUI.STextBox iHojinNo;
        private ComponentSUI.SLabel lblDispSeireki;
        private ComponentSUI.SComboBox iDispSeireki;
        private ComponentSUI.SLabel lblBank;
        private ComponentSUI.STableComboBox iBank;
    }
}
