
namespace App
{
    partial class FormOutKaihiUchiwakeMonth2
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
            this.lblZaiseki = new ComponentSUI.SLabel();
            this.iZaisekiFrom = new ComponentSUI.SComboBox();
            this.lblZaisekiRange = new ComponentSUI.SLabel();
            this.iZaisekiTo = new ComponentSUI.SComboBox();
            this.lblUchiwake = new ComponentSUI.SLabel();
            this.iUchiwakeFrom = new ComponentSUI.SComboBox();
            this.secBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 239);
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
            this.secBody.Controls.Add(this.lblZaiseki);
            this.secBody.Controls.Add(this.iZaisekiFrom);
            this.secBody.Controls.Add(this.lblZaisekiRange);
            this.secBody.Controls.Add(this.iZaisekiTo);
            this.secBody.Controls.Add(this.lblUchiwake);
            this.secBody.Controls.Add(this.iUchiwakeFrom);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(560, 173);
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
            this.lblTaishoYM.TabIndex = 118;
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
            this.lblKaiin.TabIndex = 119;
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
            this.lblKaiinRange.TabIndex = 120;
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
            // lblZaiseki
            // 
            this.lblZaiseki.BackColor = System.Drawing.Color.Transparent;
            this.lblZaiseki.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblZaiseki.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblZaiseki.Location = new System.Drawing.Point(26, 86);
            this.lblZaiseki.Name = "lblZaiseki";
            this.lblZaiseki.Size = new System.Drawing.Size(118, 24);
            this.lblZaiseki.TabIndex = 121;
            this.lblZaiseki.TabStop = false;
            this.lblZaiseki.Text = "在籍区分";
            // 
            // iZaisekiFrom
            // 
            this.iZaisekiFrom.ItemHeight = 18;
            this.iZaisekiFrom.Location = new System.Drawing.Point(150, 86);
            this.iZaisekiFrom.Name = "iZaisekiFrom";
            this.iZaisekiFrom.Size = new System.Drawing.Size(160, 24);
            this.iZaisekiFrom.TabIndex = 3;
            // 
            // lblZaisekiRange
            // 
            this.lblZaisekiRange.BackColor = System.Drawing.Color.Transparent;
            this.lblZaisekiRange.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblZaisekiRange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblZaisekiRange.Location = new System.Drawing.Point(310, 86);
            this.lblZaisekiRange.Name = "lblZaisekiRange";
            this.lblZaisekiRange.Size = new System.Drawing.Size(20, 24);
            this.lblZaisekiRange.TabIndex = 122;
            this.lblZaisekiRange.TabStop = false;
            this.lblZaisekiRange.Text = "～";
            this.lblZaisekiRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iZaisekiTo
            // 
            this.iZaisekiTo.ItemHeight = 18;
            this.iZaisekiTo.Location = new System.Drawing.Point(330, 86);
            this.iZaisekiTo.Name = "iZaisekiTo";
            this.iZaisekiTo.Size = new System.Drawing.Size(160, 24);
            this.iZaisekiTo.TabIndex = 4;
            // 
            // lblUchiwake
            // 
            this.lblUchiwake.BackColor = System.Drawing.Color.Transparent;
            this.lblUchiwake.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblUchiwake.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblUchiwake.Location = new System.Drawing.Point(26, 117);
            this.lblUchiwake.Name = "lblUchiwake";
            this.lblUchiwake.Size = new System.Drawing.Size(118, 24);
            this.lblUchiwake.TabIndex = 123;
            this.lblUchiwake.TabStop = false;
            this.lblUchiwake.Text = "月別会費内訳";
            // 
            // iUchiwakeFrom
            // 
            this.iUchiwakeFrom.ItemHeight = 18;
            this.iUchiwakeFrom.Location = new System.Drawing.Point(150, 117);
            this.iUchiwakeFrom.Name = "iUchiwakeFrom";
            this.iUchiwakeFrom.Size = new System.Drawing.Size(160, 24);
            this.iUchiwakeFrom.TabIndex = 5;
            // 
            // FormOutKaihiUchiwakeMonth2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(580, 305);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOutKaihiUchiwakeMonth2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "会費月別内訳";
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
        private ComponentSUI.SLabel lblZaiseki;
        private ComponentSUI.SComboBox iZaisekiFrom;
        private ComponentSUI.SLabel lblZaisekiRange;
        private ComponentSUI.SComboBox iZaisekiTo;
        private ComponentSUI.SLabel lblUchiwake;
        private ComponentSUI.SComboBox iUchiwakeFrom;
    }
}
