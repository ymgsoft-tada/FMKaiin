
namespace App
{
    partial class FormOutExcelNofusho2
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
            this.secIkai = new App.Controls.SectionPanel();
            this.lblDateYM = new App.Controls.SLabel();
            this.iDateYM = new App.Controls.SDateBox();
            this.lblDate = new App.Controls.SLabel();
            this.iDate = new App.Controls.SDateBox();
            this.lblIkai = new App.Controls.SLabel();
            this.rdoNichii = new App.Controls.SRadioButton();
            this.rdoKeni = new App.Controls.SRadioButton();
            this.rdoShii = new App.Controls.SRadioButton();
            this.secBody.SuspendLayout();
            this.secIkai.SuspendLayout();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 213);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(500, 66);
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
            this.lblHeading.Text = "会費納付書";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblDateYM);
            this.secBody.Controls.Add(this.iDateYM);
            this.secBody.Controls.Add(this.lblDate);
            this.secBody.Controls.Add(this.iDate);
            this.secBody.Controls.Add(this.lblIkai);
            this.secBody.Controls.Add(this.secIkai);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(480, 147);
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
            this.lblDateYM.TabIndex = 100;
            this.lblDateYM.TabStop = false;
            this.lblDateYM.Text = "月分";
            // 
            // iDateYM
            // 
            this.iDateYM.DateFormat = App.Controls.SDateFormat.YearMonth;
            this.iDateYM.Location = new System.Drawing.Point(150, 24);
            this.iDateYM.Name = "iDateYM";
            this.iDateYM.PlaceholderText = "入力必須";
            this.iDateYM.Size = new System.Drawing.Size(150, 24);
            this.iDateYM.TabIndex = 0;
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblDate.Location = new System.Drawing.Point(26, 55);
            this.lblDate.Name = "lblDate";
            this.lblDate.Required = true;
            this.lblDate.Size = new System.Drawing.Size(118, 24);
            this.lblDate.TabIndex = 101;
            this.lblDate.TabStop = false;
            this.lblDate.Text = "処理日";
            // 
            // iDate
            // 
            this.iDate.Location = new System.Drawing.Point(150, 55);
            this.iDate.Name = "iDate";
            this.iDate.PlaceholderText = "入力必須";
            this.iDate.Size = new System.Drawing.Size(186, 24);
            this.iDate.TabIndex = 1;
            // 
            // lblIkai
            // 
            this.lblIkai.BackColor = System.Drawing.Color.Transparent;
            this.lblIkai.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblIkai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblIkai.Location = new System.Drawing.Point(26, 91);
            this.lblIkai.Name = "lblIkai";
            this.lblIkai.Size = new System.Drawing.Size(118, 24);
            this.lblIkai.TabIndex = 102;
            this.lblIkai.TabStop = false;
            this.lblIkai.Text = "医師会";
            // 
            // secIkai
            // 
            this.secIkai.BackColor = System.Drawing.Color.Transparent;
            this.secIkai.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(226)))));
            this.secIkai.Controls.Add(this.rdoNichii);
            this.secIkai.Controls.Add(this.rdoKeni);
            this.secIkai.Controls.Add(this.rdoShii);
            this.secIkai.CornerRadius = 6;
            this.secIkai.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secIkai.HeaderHeight = 0;
            this.secIkai.Location = new System.Drawing.Point(150, 86);
            this.secIkai.Name = "secIkai";
            this.secIkai.Size = new System.Drawing.Size(260, 34);
            this.secIkai.TabIndex = 2;
            // 
            // rdoNichii
            // 
            this.rdoNichii.Location = new System.Drawing.Point(10, 5);
            this.rdoNichii.Name = "rdoNichii";
            this.rdoNichii.Size = new System.Drawing.Size(76, 24);
            this.rdoNichii.TabIndex = 0;
            this.rdoNichii.TabStop = true;
            this.rdoNichii.Text = "日医";
            this.rdoNichii.UseVisualStyleBackColor = false;
            // 
            // rdoKeni
            // 
            this.rdoKeni.Location = new System.Drawing.Point(92, 5);
            this.rdoKeni.Name = "rdoKeni";
            this.rdoKeni.Size = new System.Drawing.Size(76, 24);
            this.rdoKeni.TabIndex = 1;
            this.rdoKeni.TabStop = true;
            this.rdoKeni.Text = "県医";
            this.rdoKeni.UseVisualStyleBackColor = false;
            // 
            // rdoShii
            // 
            this.rdoShii.Location = new System.Drawing.Point(174, 5);
            this.rdoShii.Name = "rdoShii";
            this.rdoShii.Size = new System.Drawing.Size(76, 24);
            this.rdoShii.TabIndex = 2;
            this.rdoShii.TabStop = true;
            this.rdoShii.Text = "市医";
            this.rdoShii.UseVisualStyleBackColor = false;
            // 
            // FormOutExcelNofusho2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(500, 279);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOutExcelNofusho2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Excel出力：会費納付書";
            this.Controls.SetChildIndex(this.funckey, 0);
            this.Controls.SetChildIndex(this.icoHeading, 0);
            this.Controls.SetChildIndex(this.lblHeading, 0);
            this.Controls.SetChildIndex(this.secBody, 0);
            this.secBody.ResumeLayout(false);
            this.secIkai.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private App.Controls.FunctionKeyBar funckey;
        private App.Controls.IconBox icoHeading;
        private App.Controls.SLabel lblHeading;
        private App.Controls.SectionPanel secBody;
        private App.Controls.SectionPanel secIkai;
        private App.Controls.SLabel lblDateYM;
        private App.Controls.SDateBox iDateYM;
        private App.Controls.SLabel lblDate;
        private App.Controls.SDateBox iDate;
        private App.Controls.SLabel lblIkai;
        private App.Controls.SRadioButton rdoNichii;
        private App.Controls.SRadioButton rdoKeni;
        private App.Controls.SRadioButton rdoShii;
    }
}
