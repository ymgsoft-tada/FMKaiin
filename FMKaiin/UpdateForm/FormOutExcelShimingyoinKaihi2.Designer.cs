
namespace App
{
    partial class FormOutExcelShimingyoinKaihi2
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
            this.lblPeriod = new App.Controls.SLabel();
            this.iYear = new App.Controls.SDateBox();
            this.iPeriod = new App.Controls.SComboBox();
            this.lblIkai = new App.Controls.SLabel();
            this.rdoKen = new App.Controls.SRadioButton();
            this.rdoShi = new App.Controls.SRadioButton();
            this.secBody.SuspendLayout();
            this.secIkai.SuspendLayout();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 182);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(442, 66);
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
            this.lblHeading.Text = "市民病院会費請求資料";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblPeriod);
            this.secBody.Controls.Add(this.iYear);
            this.secBody.Controls.Add(this.iPeriod);
            this.secBody.Controls.Add(this.lblIkai);
            this.secBody.Controls.Add(this.secIkai);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(422, 116);
            this.secBody.TabIndex = 0;
            // 
            // lblPeriod
            // 
            this.lblPeriod.BackColor = System.Drawing.Color.Transparent;
            this.lblPeriod.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblPeriod.Location = new System.Drawing.Point(26, 24);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Required = true;
            this.lblPeriod.Size = new System.Drawing.Size(118, 24);
            this.lblPeriod.TabIndex = 106;
            this.lblPeriod.TabStop = false;
            this.lblPeriod.Text = "出力期間";
            // 
            // iYear
            // 
            this.iYear.DateFormat = App.Controls.SDateFormat.Year;
            this.iYear.Location = new System.Drawing.Point(150, 24);
            this.iYear.Name = "iYear";
            this.iYear.PlaceholderText = "入力必須";
            this.iYear.Size = new System.Drawing.Size(116, 24);
            this.iYear.TabIndex = 0;
            // 
            // iPeriod
            // 
            this.iPeriod.ItemHeight = 18;
            this.iPeriod.Location = new System.Drawing.Point(272, 24);
            this.iPeriod.Name = "iPeriod";
            this.iPeriod.Size = new System.Drawing.Size(80, 24);
            this.iPeriod.TabIndex = 1;
            // 
            // lblIkai
            // 
            this.lblIkai.BackColor = System.Drawing.Color.Transparent;
            this.lblIkai.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblIkai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblIkai.Location = new System.Drawing.Point(26, 60);
            this.lblIkai.Name = "lblIkai";
            this.lblIkai.Size = new System.Drawing.Size(118, 24);
            this.lblIkai.TabIndex = 107;
            this.lblIkai.TabStop = false;
            this.lblIkai.Text = "医会";
            // 
            // secIkai
            // 
            this.secIkai.BackColor = System.Drawing.Color.Transparent;
            this.secIkai.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(226)))));
            this.secIkai.Controls.Add(this.rdoKen);
            this.secIkai.Controls.Add(this.rdoShi);
            this.secIkai.CornerRadius = 6;
            this.secIkai.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secIkai.HeaderHeight = 0;
            this.secIkai.Location = new System.Drawing.Point(150, 55);
            this.secIkai.Name = "secIkai";
            this.secIkai.Size = new System.Drawing.Size(202, 34);
            this.secIkai.TabIndex = 2;
            // 
            // rdoKen
            // 
            this.rdoKen.Location = new System.Drawing.Point(10, 5);
            this.rdoKen.Name = "rdoKen";
            this.rdoKen.Size = new System.Drawing.Size(76, 24);
            this.rdoKen.TabIndex = 0;
            this.rdoKen.TabStop = true;
            this.rdoKen.Text = "県医";
            this.rdoKen.UseVisualStyleBackColor = false;
            // 
            // rdoShi
            // 
            this.rdoShi.Location = new System.Drawing.Point(92, 5);
            this.rdoShi.Name = "rdoShi";
            this.rdoShi.Size = new System.Drawing.Size(76, 24);
            this.rdoShi.TabIndex = 1;
            this.rdoShi.TabStop = true;
            this.rdoShi.Text = "市医";
            this.rdoShi.UseVisualStyleBackColor = false;
            // 
            // FormOutExcelShimingyoinKaihi2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(442, 248);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOutExcelShimingyoinKaihi2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Excel出力：市民病院会費請求";
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
        private App.Controls.SLabel lblPeriod;
        private App.Controls.SDateBox iYear;
        private App.Controls.SComboBox iPeriod;
        private App.Controls.SLabel lblIkai;
        private App.Controls.SRadioButton rdoKen;
        private App.Controls.SRadioButton rdoShi;
    }
}
