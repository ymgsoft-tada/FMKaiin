
namespace App
{
    partial class FormOutExcelNenkanChoshu2
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
            this.secType = new ComponentSUI.SectionPanel();
            this.lblYear = new ComponentSUI.SLabel();
            this.iYear = new ComponentSUI.SDateBox();
            this.lblKaiin = new ComponentSUI.SLabel();
            this.iKaiinFrom = new ComponentSUI.STableComboBox();
            this.lblType = new ComponentSUI.SLabel();
            this.rdoYotei = new ComponentSUI.SRadioButton();
            this.rdoJisseki = new ComponentSUI.SRadioButton();
            this.secBody.SuspendLayout();
            this.secType.SuspendLayout();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 244);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(460, 66);
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
            this.lblHeading.Text = "年間会費徴収書類";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblYear);
            this.secBody.Controls.Add(this.iYear);
            this.secBody.Controls.Add(this.lblKaiin);
            this.secBody.Controls.Add(this.iKaiinFrom);
            this.secBody.Controls.Add(this.lblType);
            this.secBody.Controls.Add(this.secType);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(440, 178);
            this.secBody.TabIndex = 0;
            // 
            // lblYear
            // 
            this.lblYear.BackColor = System.Drawing.Color.Transparent;
            this.lblYear.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblYear.Location = new System.Drawing.Point(26, 24);
            this.lblYear.Name = "lblYear";
            this.lblYear.Required = true;
            this.lblYear.Size = new System.Drawing.Size(118, 24);
            this.lblYear.TabIndex = 100;
            this.lblYear.TabStop = false;
            this.lblYear.Text = "年";
            // 
            // iYear
            // 
            this.iYear.DateFormat = ComponentSUI.SDateFormat.Year;
            this.iYear.Location = new System.Drawing.Point(150, 24);
            this.iYear.Name = "iYear";
            this.iYear.PlaceholderText = "入力必須";
            this.iYear.Size = new System.Drawing.Size(116, 24);
            this.iYear.TabIndex = 0;
            // 
            // lblKaiin
            // 
            this.lblKaiin.BackColor = System.Drawing.Color.Transparent;
            this.lblKaiin.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKaiin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblKaiin.Location = new System.Drawing.Point(26, 55);
            this.lblKaiin.Name = "lblKaiin";
            this.lblKaiin.Required = true;
            this.lblKaiin.Size = new System.Drawing.Size(118, 24);
            this.lblKaiin.TabIndex = 101;
            this.lblKaiin.TabStop = false;
            this.lblKaiin.Text = "会員";
            // 
            // iKaiinFrom
            // 
            this.iKaiinFrom.Location = new System.Drawing.Point(150, 55);
            this.iKaiinFrom.Name = "iKaiinFrom";
            this.iKaiinFrom.PlaceholderText = "選択必須";
            this.iKaiinFrom.Size = new System.Drawing.Size(220, 24);
            this.iKaiinFrom.TabIndex = 1;
            // 
            // lblType
            // 
            this.lblType.BackColor = System.Drawing.Color.Transparent;
            this.lblType.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblType.Location = new System.Drawing.Point(26, 91);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(118, 24);
            this.lblType.TabIndex = 102;
            this.lblType.TabStop = false;
            this.lblType.Text = "書類タイプ";
            // 
            // secType
            // 
            this.secType.BackColor = System.Drawing.Color.Transparent;
            this.secType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(226)))));
            this.secType.Controls.Add(this.rdoYotei);
            this.secType.Controls.Add(this.rdoJisseki);
            this.secType.CornerRadius = 6;
            this.secType.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secType.HeaderHeight = 0;
            this.secType.Location = new System.Drawing.Point(150, 86);
            this.secType.Name = "secType";
            this.secType.Size = new System.Drawing.Size(220, 65);
            this.secType.TabIndex = 2;
            // 
            // rdoYotei
            // 
            this.rdoYotei.Location = new System.Drawing.Point(10, 5);
            this.rdoYotei.Name = "rdoYotei";
            this.rdoYotei.Size = new System.Drawing.Size(180, 24);
            this.rdoYotei.TabIndex = 0;
            this.rdoYotei.TabStop = true;
            this.rdoYotei.Text = "予定通知書";
            this.rdoYotei.UseVisualStyleBackColor = false;
            // 
            // rdoJisseki
            // 
            this.rdoJisseki.Location = new System.Drawing.Point(10, 36);
            this.rdoJisseki.Name = "rdoJisseki";
            this.rdoJisseki.Size = new System.Drawing.Size(180, 24);
            this.rdoJisseki.TabIndex = 1;
            this.rdoJisseki.TabStop = true;
            this.rdoJisseki.Text = "徴収明細書（実績）";
            this.rdoJisseki.UseVisualStyleBackColor = false;
            // 
            // FormOutExcelNenkanChoshu2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(460, 310);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOutExcelNenkanChoshu2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Excel出力：年間会費徴収書類";
            this.Controls.SetChildIndex(this.funckey, 0);
            this.Controls.SetChildIndex(this.icoHeading, 0);
            this.Controls.SetChildIndex(this.lblHeading, 0);
            this.Controls.SetChildIndex(this.secBody, 0);
            this.secBody.ResumeLayout(false);
            this.secType.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentSUI.FunctionKeyBar funckey;
        private ComponentSUI.IconBox icoHeading;
        private ComponentSUI.SLabel lblHeading;
        private ComponentSUI.SectionPanel secBody;
        private ComponentSUI.SectionPanel secType;
        private ComponentSUI.SLabel lblYear;
        private ComponentSUI.SDateBox iYear;
        private ComponentSUI.SLabel lblKaiin;
        private ComponentSUI.STableComboBox iKaiinFrom;
        private ComponentSUI.SLabel lblType;
        private ComponentSUI.SRadioButton rdoYotei;
        private ComponentSUI.SRadioButton rdoJisseki;
    }
}
