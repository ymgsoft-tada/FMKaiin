
namespace App
{
    partial class FormMasterTantosha2_DlgEntry
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
            this.lblCode = new App.Controls.SLabel();
            this.iCode = new App.Controls.STextBox();
            this.lblName = new App.Controls.SLabel();
            this.iName = new App.Controls.STextBox();
            this.lblPwd = new App.Controls.SLabel();
            this.iPwd = new App.Controls.STextBox();
            this.lblAuth = new App.Controls.SLabel();
            this.iAuth = new App.Controls.SComboBox();
            this.secBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 239);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(440, 66);
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
            this.lblHeading.Text = "担当者情報";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblCode);
            this.secBody.Controls.Add(this.iCode);
            this.secBody.Controls.Add(this.lblName);
            this.secBody.Controls.Add(this.iName);
            this.secBody.Controls.Add(this.lblPwd);
            this.secBody.Controls.Add(this.iPwd);
            this.secBody.Controls.Add(this.lblAuth);
            this.secBody.Controls.Add(this.iAuth);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(420, 173);
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
            this.lblCode.TabIndex = 100;
            this.lblCode.TabStop = false;
            this.lblCode.Text = "コード";
            // 
            // iCode
            // 
            this.iCode.Location = new System.Drawing.Point(150, 24);
            this.iCode.Name = "iCode";
            this.iCode.PlaceholderText = "入力必須";
            this.iCode.Size = new System.Drawing.Size(72, 24);
            this.iCode.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblName.Location = new System.Drawing.Point(26, 55);
            this.lblName.Name = "lblName";
            this.lblName.Required = true;
            this.lblName.Size = new System.Drawing.Size(118, 24);
            this.lblName.TabIndex = 101;
            this.lblName.TabStop = false;
            this.lblName.Text = "氏名";
            // 
            // iName
            // 
            this.iName.Location = new System.Drawing.Point(150, 55);
            this.iName.Name = "iName";
            this.iName.PlaceholderText = "入力必須";
            this.iName.Size = new System.Drawing.Size(200, 24);
            this.iName.TabIndex = 1;
            // 
            // lblPwd
            // 
            this.lblPwd.BackColor = System.Drawing.Color.Transparent;
            this.lblPwd.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPwd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblPwd.Location = new System.Drawing.Point(26, 86);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Required = true;
            this.lblPwd.Size = new System.Drawing.Size(118, 24);
            this.lblPwd.TabIndex = 102;
            this.lblPwd.TabStop = false;
            this.lblPwd.Text = "パスワード";
            // 
            // iPwd
            // 
            this.iPwd.Location = new System.Drawing.Point(150, 86);
            this.iPwd.Name = "iPwd";
            this.iPwd.PasswordChar = '*';
            this.iPwd.PlaceholderText = "入力必須";
            this.iPwd.Size = new System.Drawing.Size(200, 24);
            this.iPwd.TabIndex = 2;
            // 
            // lblAuth
            // 
            this.lblAuth.BackColor = System.Drawing.Color.Transparent;
            this.lblAuth.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblAuth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblAuth.Location = new System.Drawing.Point(26, 117);
            this.lblAuth.Name = "lblAuth";
            this.lblAuth.Size = new System.Drawing.Size(118, 24);
            this.lblAuth.TabIndex = 103;
            this.lblAuth.TabStop = false;
            this.lblAuth.Text = "権限";
            // 
            // iAuth
            // 
            this.iAuth.ItemHeight = 18;
            this.iAuth.Location = new System.Drawing.Point(150, 117);
            this.iAuth.Name = "iAuth";
            this.iAuth.Size = new System.Drawing.Size(120, 24);
            this.iAuth.TabIndex = 3;
            // 
            // FormMasterTantosha2_DlgEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(440, 305);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMasterTantosha2_DlgEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "担当者マスタ";
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
        private App.Controls.SLabel lblCode;
        private App.Controls.STextBox iCode;
        private App.Controls.SLabel lblName;
        private App.Controls.STextBox iName;
        private App.Controls.SLabel lblPwd;
        private App.Controls.STextBox iPwd;
        private App.Controls.SLabel lblAuth;
        private App.Controls.SComboBox iAuth;
    }
}
