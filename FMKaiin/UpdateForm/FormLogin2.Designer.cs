
namespace App
{
    partial class FormLogin2
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
            this.iconLogo = new App.Controls.IconLogo();
            this.lblCorporateType = new ComponentSUI.SLabel();
            this.lblOrganization = new ComponentSUI.SLabel();
            this.sepHeader = new ComponentSUI.BufferedPanel();
            this.lblAppTitle = new ComponentSUI.SLabel();
            this.icoHeading = new ComponentSUI.IconBox();
            this.lblHeading = new ComponentSUI.SLabel();
            this.pnlHeadingLine = new ComponentSUI.BufferedPanel();
            this.secBody = new ComponentSUI.SectionPanel();
            this.lblCode = new ComponentSUI.SLabel();
            this.iCode = new ComponentSUI.STextBox();
            this.lblPwd = new ComponentSUI.SLabel();
            this.iPwd = new ComponentSUI.STextBox();
            this.btnLogin = new ComponentSUI.SButton();
            this.secBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // iconLogo
            // 
            this.iconLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(84)))), ((int)(((byte)(145)))));
            this.iconLogo.CornerRadius = 9;
            this.iconLogo.ForeColor = System.Drawing.Color.White;
            this.iconLogo.Location = new System.Drawing.Point(40, 30);
            this.iconLogo.Name = "iconLogo";
            this.iconLogo.Padding = new System.Windows.Forms.Padding(6);
            this.iconLogo.Size = new System.Drawing.Size(42, 42);
            this.iconLogo.TabIndex = 10;
            this.iconLogo.TabStop = false;
            // 
            // lblCorporateType
            // 
            this.lblCorporateType.BackColor = System.Drawing.Color.Transparent;
            this.lblCorporateType.Font = new System.Drawing.Font("BIZ UDPゴシック", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCorporateType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(60)))), ((int)(((byte)(84)))));
            this.lblCorporateType.Location = new System.Drawing.Point(92, 29);
            this.lblCorporateType.Name = "lblCorporateType";
            this.lblCorporateType.Size = new System.Drawing.Size(120, 18);
            this.lblCorporateType.TabIndex = 102;
            this.lblCorporateType.TabStop = false;
            this.lblCorporateType.Text = "公益社団法人";
            // 
            // lblOrganization
            // 
            this.lblOrganization.BackColor = System.Drawing.Color.Transparent;
            this.lblOrganization.Font = new System.Drawing.Font("BIZ UDPゴシック", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOrganization.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(60)))), ((int)(((byte)(84)))));
            this.lblOrganization.Location = new System.Drawing.Point(91, 47);
            this.lblOrganization.Name = "lblOrganization";
            this.lblOrganization.Size = new System.Drawing.Size(136, 26);
            this.lblOrganization.TabIndex = 103;
            this.lblOrganization.TabStop = false;
            this.lblOrganization.Text = "藤沢市医師会";
            // 
            // sepHeader
            // 
            this.sepHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(96)))), ((int)(((byte)(112)))), ((int)(((byte)(134)))));
            this.sepHeader.Location = new System.Drawing.Point(234, 33);
            this.sepHeader.Name = "sepHeader";
            this.sepHeader.Size = new System.Drawing.Size(1, 36);
            this.sepHeader.TabIndex = 11;
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblAppTitle.Font = new System.Drawing.Font("BIZ UDPゴシック", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblAppTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(38)))), ((int)(((byte)(66)))));
            this.lblAppTitle.Location = new System.Drawing.Point(250, 36);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(220, 30);
            this.lblAppTitle.TabIndex = 104;
            this.lblAppTitle.TabStop = false;
            this.lblAppTitle.Text = "会員管理システム";
            // 
            // icoHeading
            // 
            this.icoHeading.BackColor = System.Drawing.Color.Transparent;
            this.icoHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(100)))), ((int)(((byte)(161)))));
            this.icoHeading.IconKind = ComponentSUI.MenuIconKind.DoubleSlash;
            this.icoHeading.LineWeight = 0.9F;
            this.icoHeading.Location = new System.Drawing.Point(44, 102);
            this.icoHeading.Name = "icoHeading";
            this.icoHeading.Size = new System.Drawing.Size(30, 30);
            this.icoHeading.TabIndex = 12;
            this.icoHeading.TabStop = false;
            // 
            // lblHeading
            // 
            this.lblHeading.BackColor = System.Drawing.Color.Transparent;
            this.lblHeading.Font = new System.Drawing.Font("BIZ UDPゴシック", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(42)))), ((int)(((byte)(74)))));
            this.lblHeading.Location = new System.Drawing.Point(84, 98);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(116, 38);
            this.lblHeading.TabIndex = 105;
            this.lblHeading.TabStop = false;
            this.lblHeading.Text = "ログイン";
            // 
            // pnlHeadingLine
            // 
            this.pnlHeadingLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(198)))), ((int)(((byte)(236)))));
            this.pnlHeadingLine.Location = new System.Drawing.Point(206, 117);
            this.pnlHeadingLine.Name = "pnlHeadingLine";
            this.pnlHeadingLine.Size = new System.Drawing.Size(390, 2);
            this.pnlHeadingLine.TabIndex = 13;
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblCode);
            this.secBody.Controls.Add(this.iCode);
            this.secBody.Controls.Add(this.lblPwd);
            this.secBody.Controls.Add(this.iPwd);
            this.secBody.Controls.Add(this.btnLogin);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(52, 152);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(544, 168);
            this.secBody.TabIndex = 0;
            // 
            // lblCode
            // 
            this.lblCode.BackColor = System.Drawing.Color.Transparent;
            this.lblCode.Font = new System.Drawing.Font("BIZ UDPゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(38)))), ((int)(((byte)(66)))));
            this.lblCode.Location = new System.Drawing.Point(40, 26);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(118, 30);
            this.lblCode.TabIndex = 100;
            this.lblCode.TabStop = false;
            this.lblCode.Text = "担当者コード";
            // 
            // iCode
            // 
            this.iCode.Font = new System.Drawing.Font("BIZ UDPゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.iCode.Location = new System.Drawing.Point(164, 26);
            this.iCode.Name = "iCode";
            this.iCode.Size = new System.Drawing.Size(150, 30);
            this.iCode.TabIndex = 0;
            // 
            // lblPwd
            // 
            this.lblPwd.BackColor = System.Drawing.Color.Transparent;
            this.lblPwd.Font = new System.Drawing.Font("BIZ UDPゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(38)))), ((int)(((byte)(66)))));
            this.lblPwd.Location = new System.Drawing.Point(40, 70);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(118, 30);
            this.lblPwd.TabIndex = 101;
            this.lblPwd.TabStop = false;
            this.lblPwd.Text = "パスワード";
            // 
            // iPwd
            // 
            this.iPwd.Font = new System.Drawing.Font("BIZ UDPゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.iPwd.Location = new System.Drawing.Point(164, 70);
            this.iPwd.Name = "iPwd";
            this.iPwd.PasswordChar = '●';
            this.iPwd.Size = new System.Drawing.Size(340, 30);
            this.iPwd.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("BIZ UDPゴシック", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnLogin.Location = new System.Drawing.Point(386, 116);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Primary = true;
            this.btnLogin.Size = new System.Drawing.Size(118, 34);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "ログイン";
            this.btnLogin.UseVisualStyleBackColor = false;
            // 
            // FormLogin2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(648, 396);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.pnlHeadingLine);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.lblAppTitle);
            this.Controls.Add(this.sepHeader);
            this.Controls.Add(this.lblOrganization);
            this.Controls.Add(this.lblCorporateType);
            this.Controls.Add(this.iconLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogin2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ログイン";
            this.Controls.SetChildIndex(this.iconLogo, 0);
            this.Controls.SetChildIndex(this.lblCorporateType, 0);
            this.Controls.SetChildIndex(this.lblOrganization, 0);
            this.Controls.SetChildIndex(this.sepHeader, 0);
            this.Controls.SetChildIndex(this.lblAppTitle, 0);
            this.Controls.SetChildIndex(this.icoHeading, 0);
            this.Controls.SetChildIndex(this.lblHeading, 0);
            this.Controls.SetChildIndex(this.pnlHeadingLine, 0);
            this.Controls.SetChildIndex(this.secBody, 0);
            this.secBody.ResumeLayout(false);
            this.secBody.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private App.Controls.IconLogo iconLogo;
        private ComponentSUI.SLabel lblCorporateType;
        private ComponentSUI.SLabel lblOrganization;
        private ComponentSUI.BufferedPanel sepHeader;
        private ComponentSUI.SLabel lblAppTitle;
        private ComponentSUI.IconBox icoHeading;
        private ComponentSUI.SLabel lblHeading;
        private ComponentSUI.BufferedPanel pnlHeadingLine;
        private ComponentSUI.SectionPanel secBody;
        private ComponentSUI.SLabel lblCode;
        private ComponentSUI.STextBox iCode;
        private ComponentSUI.SLabel lblPwd;
        private ComponentSUI.STextBox iPwd;
        private ComponentSUI.SButton btnLogin;
    }
}
