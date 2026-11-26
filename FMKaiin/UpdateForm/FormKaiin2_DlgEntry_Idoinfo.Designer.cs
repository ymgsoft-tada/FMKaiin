
namespace App
{
    partial class FormKaiin2_DlgEntry_Idoinfo
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
            this.lblIdoDate = new ComponentSUI.SLabel();
            this.iIdoDate = new ComponentSUI.SDateBox();
            this.lblIdoKbn = new ComponentSUI.SLabel();
            this.iIdoKbn = new ComponentSUI.SComboBox();
            this.lblIdoDetail = new ComponentSUI.SLabel();
            this.pnl_Ido = new System.Windows.Forms.Panel();
            this.iIdoIdo = new ComponentSUI.SComboBox();
            this.iIdoIdoMemo = new ComponentSUI.STextBox();
            this.pnl_Kaiin = new System.Windows.Forms.Panel();
            this.iIdoKaiin = new ComponentSUI.SComboBox();
            this.pnl_Shozai = new System.Windows.Forms.Panel();
            this.iIdoShozai = new ComponentSUI.SComboBox();
            this.iIdoShozaiMemo = new ComponentSUI.STextBox();
            this.pnl_Address = new System.Windows.Forms.Panel();
            this.iIdoAddress = new ComponentSUI.SComboBox();
            this.iIdoAddressMemo = new ComponentSUI.STextBox();
            this.pnl_Etc = new System.Windows.Forms.Panel();
            this.iIdoEtc = new ComponentSUI.SComboBox();
            this.iIdoEtcMemo = new ComponentSUI.STextBox();
            this.secBody.SuspendLayout();
            this.pnl_Ido.SuspendLayout();
            this.pnl_Kaiin.SuspendLayout();
            this.pnl_Shozai.SuspendLayout();
            this.pnl_Address.SuspendLayout();
            this.pnl_Etc.SuspendLayout();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 239);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(500, 66);
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
            this.lblHeading.Text = "異動情報";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblIdoDate);
            this.secBody.Controls.Add(this.iIdoDate);
            this.secBody.Controls.Add(this.lblIdoKbn);
            this.secBody.Controls.Add(this.iIdoKbn);
            this.secBody.Controls.Add(this.lblIdoDetail);
            this.secBody.Controls.Add(this.pnl_Ido);
            this.secBody.Controls.Add(this.pnl_Kaiin);
            this.secBody.Controls.Add(this.pnl_Shozai);
            this.secBody.Controls.Add(this.pnl_Address);
            this.secBody.Controls.Add(this.pnl_Etc);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(480, 173);
            this.secBody.TabIndex = 0;
            // 
            // lblIdoDate
            // 
            this.lblIdoDate.BackColor = System.Drawing.Color.Transparent;
            this.lblIdoDate.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblIdoDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblIdoDate.Location = new System.Drawing.Point(26, 24);
            this.lblIdoDate.Name = "lblIdoDate";
            this.lblIdoDate.Size = new System.Drawing.Size(118, 24);
            this.lblIdoDate.TabIndex = 100;
            this.lblIdoDate.TabStop = false;
            this.lblIdoDate.Text = "異動年月日";
            // 
            // iIdoDate
            // 
            this.iIdoDate.Location = new System.Drawing.Point(150, 24);
            this.iIdoDate.Name = "iIdoDate";
            this.iIdoDate.Size = new System.Drawing.Size(186, 24);
            this.iIdoDate.TabIndex = 1;
            // 
            // lblIdoKbn
            // 
            this.lblIdoKbn.BackColor = System.Drawing.Color.Transparent;
            this.lblIdoKbn.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblIdoKbn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblIdoKbn.Location = new System.Drawing.Point(26, 55);
            this.lblIdoKbn.Name = "lblIdoKbn";
            this.lblIdoKbn.Size = new System.Drawing.Size(118, 24);
            this.lblIdoKbn.TabIndex = 101;
            this.lblIdoKbn.TabStop = false;
            this.lblIdoKbn.Text = "異動事由";
            // 
            // iIdoKbn
            // 
            this.iIdoKbn.ItemHeight = 18;
            this.iIdoKbn.Location = new System.Drawing.Point(150, 55);
            this.iIdoKbn.Name = "iIdoKbn";
            this.iIdoKbn.Size = new System.Drawing.Size(260, 24);
            this.iIdoKbn.TabIndex = 2;
            // 
            // lblIdoDetail
            // 
            this.lblIdoDetail.BackColor = System.Drawing.Color.Transparent;
            this.lblIdoDetail.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblIdoDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblIdoDetail.Location = new System.Drawing.Point(26, 86);
            this.lblIdoDetail.Name = "lblIdoDetail";
            this.lblIdoDetail.Size = new System.Drawing.Size(118, 24);
            this.lblIdoDetail.TabIndex = 102;
            this.lblIdoDetail.TabStop = false;
            this.lblIdoDetail.Text = "詳細";
            // 
            // pnl_Ido
            // 
            this.pnl_Ido.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Ido.Controls.Add(this.iIdoIdo);
            this.pnl_Ido.Controls.Add(this.iIdoIdoMemo);
            this.pnl_Ido.Location = new System.Drawing.Point(150, 86);
            this.pnl_Ido.Name = "pnl_Ido";
            this.pnl_Ido.Size = new System.Drawing.Size(260, 55);
            this.pnl_Ido.TabIndex = 3;
            // 
            // iIdoIdo
            // 
            this.iIdoIdo.ItemHeight = 18;
            this.iIdoIdo.Location = new System.Drawing.Point(0, 0);
            this.iIdoIdo.Name = "iIdoIdo";
            this.iIdoIdo.Size = new System.Drawing.Size(170, 24);
            this.iIdoIdo.TabIndex = 0;
            // 
            // iIdoIdoMemo
            // 
            this.iIdoIdoMemo.Location = new System.Drawing.Point(0, 31);
            this.iIdoIdoMemo.Name = "iIdoIdoMemo";
            this.iIdoIdoMemo.Size = new System.Drawing.Size(260, 24);
            this.iIdoIdoMemo.TabIndex = 1;
            // 
            // pnl_Kaiin
            // 
            this.pnl_Kaiin.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Kaiin.Controls.Add(this.iIdoKaiin);
            this.pnl_Kaiin.Location = new System.Drawing.Point(150, 86);
            this.pnl_Kaiin.Name = "pnl_Kaiin";
            this.pnl_Kaiin.Size = new System.Drawing.Size(260, 55);
            this.pnl_Kaiin.TabIndex = 4;
            // 
            // iIdoKaiin
            // 
            this.iIdoKaiin.ItemHeight = 18;
            this.iIdoKaiin.Location = new System.Drawing.Point(0, 0);
            this.iIdoKaiin.Name = "iIdoKaiin";
            this.iIdoKaiin.Size = new System.Drawing.Size(170, 24);
            this.iIdoKaiin.TabIndex = 0;
            // 
            // pnl_Shozai
            // 
            this.pnl_Shozai.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Shozai.Controls.Add(this.iIdoShozai);
            this.pnl_Shozai.Controls.Add(this.iIdoShozaiMemo);
            this.pnl_Shozai.Location = new System.Drawing.Point(150, 86);
            this.pnl_Shozai.Name = "pnl_Shozai";
            this.pnl_Shozai.Size = new System.Drawing.Size(260, 55);
            this.pnl_Shozai.TabIndex = 5;
            // 
            // iIdoShozai
            // 
            this.iIdoShozai.ItemHeight = 18;
            this.iIdoShozai.Location = new System.Drawing.Point(0, 0);
            this.iIdoShozai.Name = "iIdoShozai";
            this.iIdoShozai.Size = new System.Drawing.Size(170, 24);
            this.iIdoShozai.TabIndex = 0;
            // 
            // iIdoShozaiMemo
            // 
            this.iIdoShozaiMemo.Location = new System.Drawing.Point(0, 31);
            this.iIdoShozaiMemo.Name = "iIdoShozaiMemo";
            this.iIdoShozaiMemo.Size = new System.Drawing.Size(260, 24);
            this.iIdoShozaiMemo.TabIndex = 1;
            // 
            // pnl_Address
            // 
            this.pnl_Address.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Address.Controls.Add(this.iIdoAddress);
            this.pnl_Address.Controls.Add(this.iIdoAddressMemo);
            this.pnl_Address.Location = new System.Drawing.Point(150, 86);
            this.pnl_Address.Name = "pnl_Address";
            this.pnl_Address.Size = new System.Drawing.Size(260, 55);
            this.pnl_Address.TabIndex = 6;
            // 
            // iIdoAddress
            // 
            this.iIdoAddress.ItemHeight = 18;
            this.iIdoAddress.Location = new System.Drawing.Point(0, 0);
            this.iIdoAddress.Name = "iIdoAddress";
            this.iIdoAddress.Size = new System.Drawing.Size(170, 24);
            this.iIdoAddress.TabIndex = 0;
            // 
            // iIdoAddressMemo
            // 
            this.iIdoAddressMemo.Location = new System.Drawing.Point(0, 31);
            this.iIdoAddressMemo.Name = "iIdoAddressMemo";
            this.iIdoAddressMemo.Size = new System.Drawing.Size(260, 24);
            this.iIdoAddressMemo.TabIndex = 1;
            // 
            // pnl_Etc
            // 
            this.pnl_Etc.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Etc.Controls.Add(this.iIdoEtc);
            this.pnl_Etc.Controls.Add(this.iIdoEtcMemo);
            this.pnl_Etc.Location = new System.Drawing.Point(150, 86);
            this.pnl_Etc.Name = "pnl_Etc";
            this.pnl_Etc.Size = new System.Drawing.Size(260, 55);
            this.pnl_Etc.TabIndex = 7;
            // 
            // iIdoEtc
            // 
            this.iIdoEtc.ItemHeight = 18;
            this.iIdoEtc.Location = new System.Drawing.Point(0, 0);
            this.iIdoEtc.Name = "iIdoEtc";
            this.iIdoEtc.Size = new System.Drawing.Size(170, 24);
            this.iIdoEtc.TabIndex = 0;
            // 
            // iIdoEtcMemo
            // 
            this.iIdoEtcMemo.Location = new System.Drawing.Point(0, 31);
            this.iIdoEtcMemo.Name = "iIdoEtcMemo";
            this.iIdoEtcMemo.Size = new System.Drawing.Size(260, 24);
            this.iIdoEtcMemo.TabIndex = 1;
            // 
            // FormKaiin2_DlgEntry_Idoinfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(500, 305);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormKaiin2_DlgEntry_Idoinfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "会員情報：異動情報";
            this.Controls.SetChildIndex(this.funckey, 0);
            this.Controls.SetChildIndex(this.icoHeading, 0);
            this.Controls.SetChildIndex(this.lblHeading, 0);
            this.Controls.SetChildIndex(this.secBody, 0);
            this.secBody.ResumeLayout(false);
            this.pnl_Ido.ResumeLayout(false);
            this.pnl_Kaiin.ResumeLayout(false);
            this.pnl_Shozai.ResumeLayout(false);
            this.pnl_Address.ResumeLayout(false);
            this.pnl_Etc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentSUI.FunctionKeyBar funckey;
        private ComponentSUI.IconBox icoHeading;
        private ComponentSUI.SLabel lblHeading;
        private ComponentSUI.SectionPanel secBody;
        private ComponentSUI.SLabel lblIdoDate;
        private ComponentSUI.SDateBox iIdoDate;
        private ComponentSUI.SLabel lblIdoKbn;
        private ComponentSUI.SComboBox iIdoKbn;
        private ComponentSUI.SLabel lblIdoDetail;
        private System.Windows.Forms.Panel pnl_Ido;
        private ComponentSUI.SComboBox iIdoIdo;
        private ComponentSUI.STextBox iIdoIdoMemo;
        private System.Windows.Forms.Panel pnl_Kaiin;
        private ComponentSUI.SComboBox iIdoKaiin;
        private System.Windows.Forms.Panel pnl_Shozai;
        private ComponentSUI.SComboBox iIdoShozai;
        private ComponentSUI.STextBox iIdoShozaiMemo;
        private System.Windows.Forms.Panel pnl_Address;
        private ComponentSUI.SComboBox iIdoAddress;
        private ComponentSUI.STextBox iIdoAddressMemo;
        private System.Windows.Forms.Panel pnl_Etc;
        private ComponentSUI.SComboBox iIdoEtc;
        private ComponentSUI.STextBox iIdoEtcMemo;
    }
}
