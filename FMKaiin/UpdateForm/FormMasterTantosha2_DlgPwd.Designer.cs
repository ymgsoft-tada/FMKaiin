
namespace App
{
    partial class FormMasterTantosha2_DlgPwd
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
            this.lblPwd_Old = new ComponentSUI.SLabel();
            this.iPwd_Old = new ComponentSUI.STextBox();
            this.lblPwd_New = new ComponentSUI.SLabel();
            this.iPwd_New = new ComponentSUI.STextBox();
            this.lblPwd_New2 = new ComponentSUI.SLabel();
            this.iPwd_New2 = new ComponentSUI.STextBox();
            this.secBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 208);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(486, 66);
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
            this.lblHeading.Text = "パスワードの変更";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblPwd_Old);
            this.secBody.Controls.Add(this.iPwd_Old);
            this.secBody.Controls.Add(this.lblPwd_New);
            this.secBody.Controls.Add(this.iPwd_New);
            this.secBody.Controls.Add(this.lblPwd_New2);
            this.secBody.Controls.Add(this.iPwd_New2);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(466, 142);
            this.secBody.TabIndex = 0;
            // 
            // lblPwd_Old
            // 
            this.lblPwd_Old.BackColor = System.Drawing.Color.Transparent;
            this.lblPwd_Old.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPwd_Old.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblPwd_Old.Location = new System.Drawing.Point(26, 24);
            this.lblPwd_Old.Name = "lblPwd_Old";
            this.lblPwd_Old.Required = true;
            this.lblPwd_Old.Size = new System.Drawing.Size(164, 24);
            this.lblPwd_Old.TabIndex = 100;
            this.lblPwd_Old.TabStop = false;
            this.lblPwd_Old.Text = "現在のパスワード";
            // 
            // iPwd_Old
            // 
            this.iPwd_Old.Location = new System.Drawing.Point(196, 24);
            this.iPwd_Old.Name = "iPwd_Old";
            this.iPwd_Old.PlaceholderText = "入力必須";
            this.iPwd_Old.Size = new System.Drawing.Size(200, 24);
            this.iPwd_Old.TabIndex = 0;
            // 
            // lblPwd_New
            // 
            this.lblPwd_New.BackColor = System.Drawing.Color.Transparent;
            this.lblPwd_New.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPwd_New.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblPwd_New.Location = new System.Drawing.Point(26, 55);
            this.lblPwd_New.Name = "lblPwd_New";
            this.lblPwd_New.Required = true;
            this.lblPwd_New.Size = new System.Drawing.Size(164, 24);
            this.lblPwd_New.TabIndex = 101;
            this.lblPwd_New.TabStop = false;
            this.lblPwd_New.Text = "新パスワード";
            // 
            // iPwd_New
            // 
            this.iPwd_New.Location = new System.Drawing.Point(196, 55);
            this.iPwd_New.Name = "iPwd_New";
            this.iPwd_New.PasswordChar = '*';
            this.iPwd_New.PlaceholderText = "入力必須";
            this.iPwd_New.Size = new System.Drawing.Size(200, 24);
            this.iPwd_New.TabIndex = 1;
            // 
            // lblPwd_New2
            // 
            this.lblPwd_New2.BackColor = System.Drawing.Color.Transparent;
            this.lblPwd_New2.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPwd_New2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblPwd_New2.Location = new System.Drawing.Point(26, 86);
            this.lblPwd_New2.Name = "lblPwd_New2";
            this.lblPwd_New2.Required = true;
            this.lblPwd_New2.Size = new System.Drawing.Size(164, 24);
            this.lblPwd_New2.TabIndex = 102;
            this.lblPwd_New2.TabStop = false;
            this.lblPwd_New2.Text = "新パスワード(確認)";
            // 
            // iPwd_New2
            // 
            this.iPwd_New2.Location = new System.Drawing.Point(196, 86);
            this.iPwd_New2.Name = "iPwd_New2";
            this.iPwd_New2.PasswordChar = '*';
            this.iPwd_New2.PlaceholderText = "入力必須";
            this.iPwd_New2.Size = new System.Drawing.Size(200, 24);
            this.iPwd_New2.TabIndex = 2;
            // 
            // FormMasterTantosha2_DlgPwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(486, 274);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMasterTantosha2_DlgPwd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "担当者マスタ：パスワード変更";
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
        private ComponentSUI.SLabel lblPwd_Old;
        private ComponentSUI.STextBox iPwd_Old;
        private ComponentSUI.SLabel lblPwd_New;
        private ComponentSUI.STextBox iPwd_New;
        private ComponentSUI.SLabel lblPwd_New2;
        private ComponentSUI.STextBox iPwd_New2;
    }
}
