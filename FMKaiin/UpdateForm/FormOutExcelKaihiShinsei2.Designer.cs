
namespace App
{
    partial class FormOutExcelKaihiShinsei2
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
            this.lblKaiin = new ComponentSUI.SLabel();
            this.iKaiinFrom = new ComponentSUI.STableComboBox();
            this.lblType = new ComponentSUI.SLabel();
            this.rdoNyukai = new ComponentSUI.SRadioButton();
            this.rdoNyukaiKenshui = new ComponentSUI.SRadioButton();
            this.rdoHenko = new ComponentSUI.SRadioButton();
            this.rdoHenkoKorei = new ComponentSUI.SRadioButton();
            this.rdoHenko4joC = new ComponentSUI.SRadioButton();
            this.secBody.SuspendLayout();
            this.secType.SuspendLayout();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 306);
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
            this.lblHeading.Text = "各種医会等申請書";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
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
            this.secBody.Size = new System.Drawing.Size(440, 240);
            this.secBody.TabIndex = 0;
            // 
            // lblKaiin
            // 
            this.lblKaiin.BackColor = System.Drawing.Color.Transparent;
            this.lblKaiin.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKaiin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblKaiin.Location = new System.Drawing.Point(26, 24);
            this.lblKaiin.Name = "lblKaiin";
            this.lblKaiin.Required = true;
            this.lblKaiin.Size = new System.Drawing.Size(118, 24);
            this.lblKaiin.TabIndex = 104;
            this.lblKaiin.TabStop = false;
            this.lblKaiin.Text = "会員";
            // 
            // iKaiinFrom
            // 
            this.iKaiinFrom.Location = new System.Drawing.Point(150, 24);
            this.iKaiinFrom.Name = "iKaiinFrom";
            this.iKaiinFrom.PlaceholderText = "選択必須";
            this.iKaiinFrom.Size = new System.Drawing.Size(220, 24);
            this.iKaiinFrom.TabIndex = 0;
            // 
            // lblType
            // 
            this.lblType.BackColor = System.Drawing.Color.Transparent;
            this.lblType.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblType.Location = new System.Drawing.Point(26, 60);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(118, 24);
            this.lblType.TabIndex = 105;
            this.lblType.TabStop = false;
            this.lblType.Text = "書類タイプ";
            // 
            // secType
            // 
            this.secType.BackColor = System.Drawing.Color.Transparent;
            this.secType.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(226)))));
            this.secType.Controls.Add(this.rdoNyukai);
            this.secType.Controls.Add(this.rdoNyukaiKenshui);
            this.secType.Controls.Add(this.rdoHenko);
            this.secType.Controls.Add(this.rdoHenkoKorei);
            this.secType.Controls.Add(this.rdoHenko4joC);
            this.secType.CornerRadius = 6;
            this.secType.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secType.HeaderHeight = 0;
            this.secType.Location = new System.Drawing.Point(150, 55);
            this.secType.Name = "secType";
            this.secType.Size = new System.Drawing.Size(220, 158);
            this.secType.TabIndex = 1;
            // 
            // rdoNyukai
            // 
            this.rdoNyukai.Location = new System.Drawing.Point(10, 5);
            this.rdoNyukai.Name = "rdoNyukai";
            this.rdoNyukai.Size = new System.Drawing.Size(160, 24);
            this.rdoNyukai.TabIndex = 0;
            this.rdoNyukai.TabStop = true;
            this.rdoNyukai.Text = "新入会";
            this.rdoNyukai.UseVisualStyleBackColor = false;
            // 
            // rdoNyukaiKenshui
            // 
            this.rdoNyukaiKenshui.Location = new System.Drawing.Point(10, 36);
            this.rdoNyukaiKenshui.Name = "rdoNyukaiKenshui";
            this.rdoNyukaiKenshui.Size = new System.Drawing.Size(160, 24);
            this.rdoNyukaiKenshui.TabIndex = 1;
            this.rdoNyukaiKenshui.TabStop = true;
            this.rdoNyukaiKenshui.Text = "新入会（研修医）";
            this.rdoNyukaiKenshui.UseVisualStyleBackColor = false;
            // 
            // rdoHenko
            // 
            this.rdoHenko.Location = new System.Drawing.Point(10, 67);
            this.rdoHenko.Name = "rdoHenko";
            this.rdoHenko.Size = new System.Drawing.Size(160, 24);
            this.rdoHenko.TabIndex = 2;
            this.rdoHenko.TabStop = true;
            this.rdoHenko.Text = "変更";
            this.rdoHenko.UseVisualStyleBackColor = false;
            // 
            // rdoHenkoKorei
            // 
            this.rdoHenkoKorei.Location = new System.Drawing.Point(10, 98);
            this.rdoHenkoKorei.Name = "rdoHenkoKorei";
            this.rdoHenkoKorei.Size = new System.Drawing.Size(160, 24);
            this.rdoHenkoKorei.TabIndex = 3;
            this.rdoHenkoKorei.TabStop = true;
            this.rdoHenkoKorei.Text = "変更（高齢用）";
            this.rdoHenkoKorei.UseVisualStyleBackColor = false;
            // 
            // rdoHenko4joC
            // 
            this.rdoHenko4joC.Location = new System.Drawing.Point(10, 129);
            this.rdoHenko4joC.Name = "rdoHenko4joC";
            this.rdoHenko4joC.Size = new System.Drawing.Size(160, 24);
            this.rdoHenko4joC.TabIndex = 4;
            this.rdoHenko4joC.TabStop = true;
            this.rdoHenko4joC.Text = "変更（4条C）";
            this.rdoHenko4joC.UseVisualStyleBackColor = false;
            // 
            // FormOutExcelKaihiShinsei2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(460, 372);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOutExcelKaihiShinsei2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Excel出力：各種医会等申請書";
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
        private ComponentSUI.SLabel lblKaiin;
        private ComponentSUI.STableComboBox iKaiinFrom;
        private ComponentSUI.SLabel lblType;
        private ComponentSUI.SRadioButton rdoNyukai;
        private ComponentSUI.SRadioButton rdoNyukaiKenshui;
        private ComponentSUI.SRadioButton rdoHenko;
        private ComponentSUI.SRadioButton rdoHenkoKorei;
        private ComponentSUI.SRadioButton rdoHenko4joC;
    }
}
