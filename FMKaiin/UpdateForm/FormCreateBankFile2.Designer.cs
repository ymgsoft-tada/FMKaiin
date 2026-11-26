
namespace App
{
    partial class FormCreateBankFile2
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
            this.lblDateYM = new ComponentSUI.SLabel();
            this.iDateYM = new ComponentSUI.SDateBox();
            this.lblBank = new ComponentSUI.SLabel();
            this.iBank = new ComponentSUI.STableComboBox();
            this.lblDate = new ComponentSUI.SLabel();
            this.iDate = new ComponentSUI.SDateBox();
            this.secBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 208);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(540, 66);
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
            this.lblHeading.Text = "銀行用引落データ作成";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblDateYM);
            this.secBody.Controls.Add(this.iDateYM);
            this.secBody.Controls.Add(this.lblBank);
            this.secBody.Controls.Add(this.iBank);
            this.secBody.Controls.Add(this.lblDate);
            this.secBody.Controls.Add(this.iDate);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(520, 142);
            this.secBody.TabIndex = 0;
            // 
            // lblDateYM
            // 
            this.lblDateYM.BackColor = System.Drawing.Color.Transparent;
            this.lblDateYM.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDateYM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblDateYM.Location = new System.Drawing.Point(26, 24);
            this.lblDateYM.Name = "lblDateYM";
            this.lblDateYM.Required = true;
            this.lblDateYM.Size = new System.Drawing.Size(118, 24);
            this.lblDateYM.TabIndex = 101;
            this.lblDateYM.TabStop = false;
            this.lblDateYM.Text = "月度";
            // 
            // iDateYM
            // 
            this.iDateYM.DateFormat = ComponentSUI.SDateFormat.YearMonth;
            this.iDateYM.Location = new System.Drawing.Point(150, 24);
            this.iDateYM.Name = "iDateYM";
            this.iDateYM.PlaceholderText = "入力必須";
            this.iDateYM.Size = new System.Drawing.Size(150, 24);
            this.iDateYM.TabIndex = 0;
            // 
            // lblBank
            // 
            this.lblBank.BackColor = System.Drawing.Color.Transparent;
            this.lblBank.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblBank.Location = new System.Drawing.Point(26, 55);
            this.lblBank.Name = "lblBank";
            this.lblBank.Required = true;
            this.lblBank.Size = new System.Drawing.Size(118, 24);
            this.lblBank.TabIndex = 102;
            this.lblBank.TabStop = false;
            this.lblBank.Text = "引落元銀行";
            // 
            // iBank
            // 
            this.iBank.Location = new System.Drawing.Point(150, 55);
            this.iBank.Name = "iBank";
            this.iBank.PlaceholderText = "選択必須";
            this.iBank.Size = new System.Drawing.Size(300, 24);
            this.iBank.TabIndex = 1;
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblDate.Location = new System.Drawing.Point(26, 86);
            this.lblDate.Name = "lblDate";
            this.lblDate.Required = true;
            this.lblDate.Size = new System.Drawing.Size(118, 24);
            this.lblDate.TabIndex = 103;
            this.lblDate.TabStop = false;
            this.lblDate.Text = "引落日";
            // 
            // iDate
            // 
            this.iDate.Location = new System.Drawing.Point(150, 86);
            this.iDate.Name = "iDate";
            this.iDate.PlaceholderText = "入力必須";
            this.iDate.Size = new System.Drawing.Size(186, 24);
            this.iDate.TabIndex = 2;
            // 
            // FormCreateBankFile2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(540, 274);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCreateBankFile2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FBデータ作成";
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
        private ComponentSUI.SLabel lblDateYM;
        private ComponentSUI.SDateBox iDateYM;
        private ComponentSUI.SLabel lblBank;
        private ComponentSUI.STableComboBox iBank;
        private ComponentSUI.SLabel lblDate;
        private ComponentSUI.SDateBox iDate;
    }
}
