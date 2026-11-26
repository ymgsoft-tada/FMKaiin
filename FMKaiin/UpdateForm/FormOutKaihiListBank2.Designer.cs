
namespace App
{
    partial class FormOutKaihiListBank2
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
            this.lblTypeKaihi = new App.Controls.SLabel();
            this.iTypeKaihiFrom = new App.Controls.SComboBox();
            this.lblTypeKaihiRange = new App.Controls.SLabel();
            this.iTypeKaihiTo = new App.Controls.SComboBox();
            this.lblKaihi = new App.Controls.SLabel();
            this.iKaihiFrom = new App.Controls.STableComboBox();
            this.lblKaihiRange = new App.Controls.SLabel();
            this.iKaihiTo = new App.Controls.STableComboBox();
            this.secBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 177);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(580, 66);
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
            this.lblHeading.Text = "出力条件";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblTypeKaihi);
            this.secBody.Controls.Add(this.iTypeKaihiFrom);
            this.secBody.Controls.Add(this.lblTypeKaihiRange);
            this.secBody.Controls.Add(this.iTypeKaihiTo);
            this.secBody.Controls.Add(this.lblKaihi);
            this.secBody.Controls.Add(this.iKaihiFrom);
            this.secBody.Controls.Add(this.lblKaihiRange);
            this.secBody.Controls.Add(this.iKaihiTo);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(560, 111);
            this.secBody.TabIndex = 0;
            // 
            // lblTypeKaihi
            // 
            this.lblTypeKaihi.BackColor = System.Drawing.Color.Transparent;
            this.lblTypeKaihi.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTypeKaihi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblTypeKaihi.Location = new System.Drawing.Point(26, 24);
            this.lblTypeKaihi.Name = "lblTypeKaihi";
            this.lblTypeKaihi.Size = new System.Drawing.Size(118, 24);
            this.lblTypeKaihi.TabIndex = 114;
            this.lblTypeKaihi.TabStop = false;
            this.lblTypeKaihi.Text = "会費区分";
            // 
            // iTypeKaihiFrom
            // 
            this.iTypeKaihiFrom.ItemHeight = 18;
            this.iTypeKaihiFrom.Location = new System.Drawing.Point(150, 24);
            this.iTypeKaihiFrom.Name = "iTypeKaihiFrom";
            this.iTypeKaihiFrom.Size = new System.Drawing.Size(160, 24);
            this.iTypeKaihiFrom.TabIndex = 0;
            // 
            // lblTypeKaihiRange
            // 
            this.lblTypeKaihiRange.BackColor = System.Drawing.Color.Transparent;
            this.lblTypeKaihiRange.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTypeKaihiRange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblTypeKaihiRange.Location = new System.Drawing.Point(310, 24);
            this.lblTypeKaihiRange.Name = "lblTypeKaihiRange";
            this.lblTypeKaihiRange.Size = new System.Drawing.Size(20, 24);
            this.lblTypeKaihiRange.TabIndex = 115;
            this.lblTypeKaihiRange.TabStop = false;
            this.lblTypeKaihiRange.Text = "～";
            this.lblTypeKaihiRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iTypeKaihiTo
            // 
            this.iTypeKaihiTo.ItemHeight = 18;
            this.iTypeKaihiTo.Location = new System.Drawing.Point(330, 24);
            this.iTypeKaihiTo.Name = "iTypeKaihiTo";
            this.iTypeKaihiTo.Size = new System.Drawing.Size(160, 24);
            this.iTypeKaihiTo.TabIndex = 1;
            // 
            // lblKaihi
            // 
            this.lblKaihi.BackColor = System.Drawing.Color.Transparent;
            this.lblKaihi.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKaihi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblKaihi.Location = new System.Drawing.Point(26, 55);
            this.lblKaihi.Name = "lblKaihi";
            this.lblKaihi.Size = new System.Drawing.Size(118, 24);
            this.lblKaihi.TabIndex = 116;
            this.lblKaihi.TabStop = false;
            this.lblKaihi.Text = "会費";
            // 
            // iKaihiFrom
            // 
            this.iKaihiFrom.Location = new System.Drawing.Point(150, 55);
            this.iKaihiFrom.Name = "iKaihiFrom";
            this.iKaihiFrom.Size = new System.Drawing.Size(160, 24);
            this.iKaihiFrom.TabIndex = 2;
            // 
            // lblKaihiRange
            // 
            this.lblKaihiRange.BackColor = System.Drawing.Color.Transparent;
            this.lblKaihiRange.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKaihiRange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblKaihiRange.Location = new System.Drawing.Point(310, 55);
            this.lblKaihiRange.Name = "lblKaihiRange";
            this.lblKaihiRange.Size = new System.Drawing.Size(20, 24);
            this.lblKaihiRange.TabIndex = 117;
            this.lblKaihiRange.TabStop = false;
            this.lblKaihiRange.Text = "～";
            this.lblKaihiRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iKaihiTo
            // 
            this.iKaihiTo.Location = new System.Drawing.Point(330, 55);
            this.iKaihiTo.Name = "iKaihiTo";
            this.iKaihiTo.Size = new System.Drawing.Size(160, 24);
            this.iKaihiTo.TabIndex = 3;
            // 
            // FormOutKaihiListBank2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(580, 243);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOutKaihiListBank2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "会費種別一覧表（銀行有）";
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
        private App.Controls.SLabel lblTypeKaihi;
        private App.Controls.SComboBox iTypeKaihiFrom;
        private App.Controls.SLabel lblTypeKaihiRange;
        private App.Controls.SComboBox iTypeKaihiTo;
        private App.Controls.SLabel lblKaihi;
        private App.Controls.STableComboBox iKaihiFrom;
        private App.Controls.SLabel lblKaihiRange;
        private App.Controls.STableComboBox iKaihiTo;
    }
}
