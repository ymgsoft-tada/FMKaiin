
namespace App
{
    partial class FormOutSeikyuMinyukin2
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
            this.lblTaishoYM = new ComponentSUI.SLabel();
            this.iTaishoYM = new ComponentSUI.SDateBox();
            this.lblKaiin = new ComponentSUI.SLabel();
            this.iKaiinFrom = new ComponentSUI.STableComboBox();
            this.lblKaiinRange = new ComponentSUI.SLabel();
            this.iKaiinTo = new ComponentSUI.STableComboBox();
            this.lblShiharai = new ComponentSUI.SLabel();
            this.iShiharaiFrom = new ComponentSUI.SComboBox();
            this.lblShiharaiRange = new ComponentSUI.SLabel();
            this.iShiharaiTo = new ComponentSUI.SComboBox();
            this.secBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 208);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(580, 66);
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
            this.lblHeading.Text = "出力条件";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblTaishoYM);
            this.secBody.Controls.Add(this.iTaishoYM);
            this.secBody.Controls.Add(this.lblKaiin);
            this.secBody.Controls.Add(this.iKaiinFrom);
            this.secBody.Controls.Add(this.lblKaiinRange);
            this.secBody.Controls.Add(this.iKaiinTo);
            this.secBody.Controls.Add(this.lblShiharai);
            this.secBody.Controls.Add(this.iShiharaiFrom);
            this.secBody.Controls.Add(this.lblShiharaiRange);
            this.secBody.Controls.Add(this.iShiharaiTo);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(560, 142);
            this.secBody.TabIndex = 0;
            // 
            // lblTaishoYM
            // 
            this.lblTaishoYM.BackColor = System.Drawing.Color.Transparent;
            this.lblTaishoYM.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTaishoYM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblTaishoYM.Location = new System.Drawing.Point(26, 24);
            this.lblTaishoYM.Name = "lblTaishoYM";
            this.lblTaishoYM.Required = true;
            this.lblTaishoYM.Size = new System.Drawing.Size(118, 24);
            this.lblTaishoYM.TabIndex = 105;
            this.lblTaishoYM.TabStop = false;
            this.lblTaishoYM.Text = "対象年月";
            // 
            // iTaishoYM
            // 
            this.iTaishoYM.DateFormat = ComponentSUI.SDateFormat.YearMonth;
            this.iTaishoYM.Location = new System.Drawing.Point(150, 24);
            this.iTaishoYM.Name = "iTaishoYM";
            this.iTaishoYM.PlaceholderText = "入力必須";
            this.iTaishoYM.Size = new System.Drawing.Size(150, 24);
            this.iTaishoYM.TabIndex = 0;
            // 
            // lblKaiin
            // 
            this.lblKaiin.BackColor = System.Drawing.Color.Transparent;
            this.lblKaiin.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKaiin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblKaiin.Location = new System.Drawing.Point(26, 55);
            this.lblKaiin.Name = "lblKaiin";
            this.lblKaiin.Size = new System.Drawing.Size(118, 24);
            this.lblKaiin.TabIndex = 106;
            this.lblKaiin.TabStop = false;
            this.lblKaiin.Text = "会員";
            // 
            // iKaiinFrom
            // 
            this.iKaiinFrom.Location = new System.Drawing.Point(150, 55);
            this.iKaiinFrom.Name = "iKaiinFrom";
            this.iKaiinFrom.Size = new System.Drawing.Size(160, 24);
            this.iKaiinFrom.TabIndex = 1;
            // 
            // lblKaiinRange
            // 
            this.lblKaiinRange.BackColor = System.Drawing.Color.Transparent;
            this.lblKaiinRange.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKaiinRange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblKaiinRange.Location = new System.Drawing.Point(310, 55);
            this.lblKaiinRange.Name = "lblKaiinRange";
            this.lblKaiinRange.Size = new System.Drawing.Size(20, 24);
            this.lblKaiinRange.TabIndex = 107;
            this.lblKaiinRange.TabStop = false;
            this.lblKaiinRange.Text = "～";
            this.lblKaiinRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iKaiinTo
            // 
            this.iKaiinTo.Location = new System.Drawing.Point(330, 55);
            this.iKaiinTo.Name = "iKaiinTo";
            this.iKaiinTo.Size = new System.Drawing.Size(160, 24);
            this.iKaiinTo.TabIndex = 2;
            // 
            // lblShiharai
            // 
            this.lblShiharai.BackColor = System.Drawing.Color.Transparent;
            this.lblShiharai.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShiharai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblShiharai.Location = new System.Drawing.Point(26, 86);
            this.lblShiharai.Name = "lblShiharai";
            this.lblShiharai.Size = new System.Drawing.Size(118, 24);
            this.lblShiharai.TabIndex = 108;
            this.lblShiharai.TabStop = false;
            this.lblShiharai.Text = "支払方法";
            // 
            // iShiharaiFrom
            // 
            this.iShiharaiFrom.ItemHeight = 18;
            this.iShiharaiFrom.Location = new System.Drawing.Point(150, 86);
            this.iShiharaiFrom.Name = "iShiharaiFrom";
            this.iShiharaiFrom.Size = new System.Drawing.Size(160, 24);
            this.iShiharaiFrom.TabIndex = 3;
            // 
            // lblShiharaiRange
            // 
            this.lblShiharaiRange.BackColor = System.Drawing.Color.Transparent;
            this.lblShiharaiRange.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShiharaiRange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblShiharaiRange.Location = new System.Drawing.Point(310, 86);
            this.lblShiharaiRange.Name = "lblShiharaiRange";
            this.lblShiharaiRange.Size = new System.Drawing.Size(20, 24);
            this.lblShiharaiRange.TabIndex = 109;
            this.lblShiharaiRange.TabStop = false;
            this.lblShiharaiRange.Text = "～";
            this.lblShiharaiRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iShiharaiTo
            // 
            this.iShiharaiTo.ItemHeight = 18;
            this.iShiharaiTo.Location = new System.Drawing.Point(330, 86);
            this.iShiharaiTo.Name = "iShiharaiTo";
            this.iShiharaiTo.Size = new System.Drawing.Size(160, 24);
            this.iShiharaiTo.TabIndex = 4;
            // 
            // FormOutSeikyuMinyukin2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(580, 274);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOutSeikyuMinyukin2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "未入金者用請求書";
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
        private ComponentSUI.SLabel lblTaishoYM;
        private ComponentSUI.SDateBox iTaishoYM;
        private ComponentSUI.SLabel lblKaiin;
        private ComponentSUI.STableComboBox iKaiinFrom;
        private ComponentSUI.SLabel lblKaiinRange;
        private ComponentSUI.STableComboBox iKaiinTo;
        private ComponentSUI.SLabel lblShiharai;
        private ComponentSUI.SComboBox iShiharaiFrom;
        private ComponentSUI.SLabel lblShiharaiRange;
        private ComponentSUI.SComboBox iShiharaiTo;
    }
}
