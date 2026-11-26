
namespace App
{
    partial class FormMasterBankCode2_DlgEntry
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
            this.funckey = new App.Controls.FunctionKeyBar();
            this.icoHeading = new App.Controls.IconBox();
            this.lblHeading = new App.Controls.SLabel();
            this.secBody = new App.Controls.SectionPanel();
            this.lblBankCode = new App.Controls.SLabel();
            this.iBankCode = new App.Controls.STextBox();
            this.lblShitenCode = new App.Controls.SLabel();
            this.iShitenCode = new App.Controls.STextBox();
            this.lblNameBank = new App.Controls.SLabel();
            this.iNameBank = new App.Controls.STextBox();
            this.lblNameShiten = new App.Controls.SLabel();
            this.iNameShiten = new App.Controls.STextBox();
            this.lblNameKanaBank = new App.Controls.SLabel();
            this.iNameKanaBank = new App.Controls.STextBox();
            this.lblNameKanaShiten = new App.Controls.SLabel();
            this.iNameKanaShiten = new App.Controls.STextBox();
            this.lblFullNameBank = new App.Controls.SLabel();
            this.iFullNameBank = new App.Controls.STextBox();
            this.secBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 237);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(744, 66);
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
            this.lblHeading.Text = "銀行コード情報";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblBankCode);
            this.secBody.Controls.Add(this.iBankCode);
            this.secBody.Controls.Add(this.lblShitenCode);
            this.secBody.Controls.Add(this.iShitenCode);
            this.secBody.Controls.Add(this.lblNameBank);
            this.secBody.Controls.Add(this.iNameBank);
            this.secBody.Controls.Add(this.lblNameShiten);
            this.secBody.Controls.Add(this.iNameShiten);
            this.secBody.Controls.Add(this.lblNameKanaBank);
            this.secBody.Controls.Add(this.iNameKanaBank);
            this.secBody.Controls.Add(this.lblNameKanaShiten);
            this.secBody.Controls.Add(this.iNameKanaShiten);
            this.secBody.Controls.Add(this.lblFullNameBank);
            this.secBody.Controls.Add(this.iFullNameBank);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(724, 171);
            this.secBody.TabIndex = 0;
            // 
            // lblBankCode
            // 
            this.lblBankCode.BackColor = System.Drawing.Color.Transparent;
            this.lblBankCode.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBankCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblBankCode.Location = new System.Drawing.Point(26, 24);
            this.lblBankCode.Name = "lblBankCode";
            this.lblBankCode.Required = true;
            this.lblBankCode.Size = new System.Drawing.Size(118, 24);
            this.lblBankCode.TabIndex = 102;
            this.lblBankCode.TabStop = false;
            this.lblBankCode.Text = "銀行コード";
            // 
            // iBankCode
            // 
            this.iBankCode.Location = new System.Drawing.Point(150, 24);
            this.iBankCode.Name = "iBankCode";
            this.iBankCode.PlaceholderText = "入力必須";
            this.iBankCode.Size = new System.Drawing.Size(80, 24);
            this.iBankCode.TabIndex = 0;
            // 
            // lblShitenCode
            // 
            this.lblShitenCode.BackColor = System.Drawing.Color.Transparent;
            this.lblShitenCode.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShitenCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblShitenCode.Location = new System.Drawing.Point(374, 24);
            this.lblShitenCode.Name = "lblShitenCode";
            this.lblShitenCode.Required = true;
            this.lblShitenCode.Size = new System.Drawing.Size(118, 24);
            this.lblShitenCode.TabIndex = 103;
            this.lblShitenCode.TabStop = false;
            this.lblShitenCode.Text = "支店コード";
            // 
            // iShitenCode
            // 
            this.iShitenCode.Location = new System.Drawing.Point(498, 24);
            this.iShitenCode.Name = "iShitenCode";
            this.iShitenCode.PlaceholderText = "入力必須";
            this.iShitenCode.Size = new System.Drawing.Size(80, 24);
            this.iShitenCode.TabIndex = 4;
            // 
            // lblNameBank
            // 
            this.lblNameBank.BackColor = System.Drawing.Color.Transparent;
            this.lblNameBank.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNameBank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblNameBank.Location = new System.Drawing.Point(26, 55);
            this.lblNameBank.Name = "lblNameBank";
            this.lblNameBank.Required = true;
            this.lblNameBank.Size = new System.Drawing.Size(118, 24);
            this.lblNameBank.TabIndex = 104;
            this.lblNameBank.TabStop = false;
            this.lblNameBank.Text = "銀行名";
            // 
            // iNameBank
            // 
            this.iNameBank.Location = new System.Drawing.Point(150, 55);
            this.iNameBank.Name = "iNameBank";
            this.iNameBank.PlaceholderText = "入力必須";
            this.iNameBank.Size = new System.Drawing.Size(200, 24);
            this.iNameBank.TabIndex = 1;
            // 
            // lblNameShiten
            // 
            this.lblNameShiten.BackColor = System.Drawing.Color.Transparent;
            this.lblNameShiten.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNameShiten.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblNameShiten.Location = new System.Drawing.Point(374, 55);
            this.lblNameShiten.Name = "lblNameShiten";
            this.lblNameShiten.Required = true;
            this.lblNameShiten.Size = new System.Drawing.Size(118, 24);
            this.lblNameShiten.TabIndex = 105;
            this.lblNameShiten.TabStop = false;
            this.lblNameShiten.Text = "支店名";
            // 
            // iNameShiten
            // 
            this.iNameShiten.Location = new System.Drawing.Point(498, 55);
            this.iNameShiten.Name = "iNameShiten";
            this.iNameShiten.PlaceholderText = "入力必須";
            this.iNameShiten.Size = new System.Drawing.Size(200, 24);
            this.iNameShiten.TabIndex = 5;
            // 
            // lblNameKanaBank
            // 
            this.lblNameKanaBank.BackColor = System.Drawing.Color.Transparent;
            this.lblNameKanaBank.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNameKanaBank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblNameKanaBank.Location = new System.Drawing.Point(26, 84);
            this.lblNameKanaBank.Name = "lblNameKanaBank";
            this.lblNameKanaBank.Required = true;
            this.lblNameKanaBank.Size = new System.Drawing.Size(118, 24);
            this.lblNameKanaBank.TabIndex = 106;
            this.lblNameKanaBank.TabStop = false;
            this.lblNameKanaBank.Text = "　銀行名カナ";
            // 
            // iNameKanaBank
            // 
            this.iNameKanaBank.Location = new System.Drawing.Point(150, 84);
            this.iNameKanaBank.Name = "iNameKanaBank";
            this.iNameKanaBank.PlaceholderText = "入力必須";
            this.iNameKanaBank.Size = new System.Drawing.Size(200, 24);
            this.iNameKanaBank.TabIndex = 2;
            // 
            // lblNameKanaShiten
            // 
            this.lblNameKanaShiten.BackColor = System.Drawing.Color.Transparent;
            this.lblNameKanaShiten.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNameKanaShiten.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblNameKanaShiten.Location = new System.Drawing.Point(374, 84);
            this.lblNameKanaShiten.Name = "lblNameKanaShiten";
            this.lblNameKanaShiten.Required = true;
            this.lblNameKanaShiten.Size = new System.Drawing.Size(118, 24);
            this.lblNameKanaShiten.TabIndex = 107;
            this.lblNameKanaShiten.TabStop = false;
            this.lblNameKanaShiten.Text = "　支店名カナ";
            // 
            // iNameKanaShiten
            // 
            this.iNameKanaShiten.Location = new System.Drawing.Point(498, 84);
            this.iNameKanaShiten.Name = "iNameKanaShiten";
            this.iNameKanaShiten.PlaceholderText = "入力必須";
            this.iNameKanaShiten.Size = new System.Drawing.Size(200, 24);
            this.iNameKanaShiten.TabIndex = 6;
            // 
            // lblFullNameBank
            // 
            this.lblFullNameBank.BackColor = System.Drawing.Color.Transparent;
            this.lblFullNameBank.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblFullNameBank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblFullNameBank.Location = new System.Drawing.Point(26, 115);
            this.lblFullNameBank.Name = "lblFullNameBank";
            this.lblFullNameBank.Required = true;
            this.lblFullNameBank.Size = new System.Drawing.Size(118, 24);
            this.lblFullNameBank.TabIndex = 108;
            this.lblFullNameBank.TabStop = false;
            this.lblFullNameBank.Text = "正式名称";
            // 
            // iFullNameBank
            // 
            this.iFullNameBank.Location = new System.Drawing.Point(150, 115);
            this.iFullNameBank.Name = "iFullNameBank";
            this.iFullNameBank.PlaceholderText = "入力必須";
            this.iFullNameBank.Size = new System.Drawing.Size(200, 24);
            this.iFullNameBank.TabIndex = 3;
            // 
            // FormMasterBankCode2_DlgEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(744, 303);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMasterBankCode2_DlgEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "銀行コードマスタ";
            this.Controls.SetChildIndex(this.funckey, 0);
            this.Controls.SetChildIndex(this.icoHeading, 0);
            this.Controls.SetChildIndex(this.lblHeading, 0);
            this.Controls.SetChildIndex(this.secBody, 0);
            this.secBody.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private App.Controls.FunctionKeyBar funckey;
        private App.Controls.IconBox icoHeading;
        private App.Controls.SLabel lblHeading;
        private App.Controls.SectionPanel secBody;
        private App.Controls.SLabel lblBankCode;
        private App.Controls.STextBox iBankCode;
        private App.Controls.SLabel lblShitenCode;
        private App.Controls.STextBox iShitenCode;
        private App.Controls.SLabel lblNameBank;
        private App.Controls.STextBox iNameBank;
        private App.Controls.SLabel lblNameShiten;
        private App.Controls.STextBox iNameShiten;
        private App.Controls.SLabel lblNameKanaBank;
        private App.Controls.STextBox iNameKanaBank;
        private App.Controls.SLabel lblNameKanaShiten;
        private App.Controls.STextBox iNameKanaShiten;
        private App.Controls.SLabel lblFullNameBank;
        private App.Controls.STextBox iFullNameBank;
    }
}
