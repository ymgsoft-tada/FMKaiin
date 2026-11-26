namespace App
{
    partial class FormOutBlankSetting2
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOutBlankSetting2));
            this.icoHeading = new ComponentSUI.IconBox();
            this.lblHeading = new ComponentSUI.SLabel();
            this.secBody = new ComponentSUI.SectionPanel();
            this.hScroll = new System.Windows.Forms.HScrollBar();
            this.vScroll = new System.Windows.Forms.VScrollBar();
            this.picPaper = new System.Windows.Forms.PictureBox();
            this.lblH = new ComponentSUI.SLabel();
            this.lblV = new ComponentSUI.SLabel();
            this.btnClear = new ComponentSUI.SButton();
            this.btnOK = new ComponentSUI.SButton();
            this.btnCancel = new ComponentSUI.SButton();
            this.imgList1 = new System.Windows.Forms.ImageList(this.components);
            this.imgList2 = new System.Windows.Forms.ImageList(this.components);
            this.secBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPaper)).BeginInit();
            this.SuspendLayout();
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
            this.lblHeading.Text = "余白設定";
            //
            // secBody
            //
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.hScroll);
            this.secBody.Controls.Add(this.vScroll);
            this.secBody.Controls.Add(this.picPaper);
            this.secBody.Controls.Add(this.lblH);
            this.secBody.Controls.Add(this.lblV);
            this.secBody.Controls.Add(this.btnClear);
            this.secBody.Controls.Add(this.btnOK);
            this.secBody.Controls.Add(this.btnCancel);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(400, 286);
            this.secBody.TabIndex = 0;
            //
            // hScroll
            //
            this.hScroll.Location = new System.Drawing.Point(43, 22);
            this.hScroll.Maximum = 109;
            this.hScroll.Name = "hScroll";
            this.hScroll.Size = new System.Drawing.Size(200, 17);
            this.hScroll.TabIndex = 0;
            //
            // vScroll
            //
            this.vScroll.Location = new System.Drawing.Point(26, 39);
            this.vScroll.Maximum = 109;
            this.vScroll.Name = "vScroll";
            this.vScroll.Size = new System.Drawing.Size(17, 200);
            this.vScroll.TabIndex = 1;
            //
            // picPaper
            //
            this.picPaper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(238)))), ((int)(((byte)(246)))));
            this.picPaper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPaper.Location = new System.Drawing.Point(43, 39);
            this.picPaper.Name = "picPaper";
            this.picPaper.Size = new System.Drawing.Size(200, 200);
            this.picPaper.TabIndex = 5;
            this.picPaper.TabStop = false;
            //
            // lblH
            //
            this.lblH.BackColor = System.Drawing.Color.Transparent;
            this.lblH.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblH.Location = new System.Drawing.Point(251, 19);
            this.lblH.Name = "lblH";
            this.lblH.Size = new System.Drawing.Size(120, 24);
            this.lblH.TabIndex = 100;
            this.lblH.TabStop = false;
            this.lblH.Text = "横　0.0mm";
            //
            // lblV
            //
            this.lblV.BackColor = System.Drawing.Color.Transparent;
            this.lblV.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblV.Location = new System.Drawing.Point(26, 245);
            this.lblV.Name = "lblV";
            this.lblV.Size = new System.Drawing.Size(120, 24);
            this.lblV.TabIndex = 101;
            this.lblV.TabStop = false;
            this.lblV.Text = "縦　0.0mm";
            //
            // btnClear
            //
            this.btnClear.Location = new System.Drawing.Point(263, 118);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(112, 28);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "初期値に戻す";
            this.btnClear.UseVisualStyleBackColor = false;
            //
            // btnOK
            //
            this.btnOK.Location = new System.Drawing.Point(263, 177);
            this.btnOK.Name = "btnOK";
            this.btnOK.Primary = true;
            this.btnOK.Size = new System.Drawing.Size(112, 28);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(263, 211);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 28);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = false;
            //
            // imgList1
            //
            this.imgList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList1.ImageStream")));
            this.imgList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList1.Images.SetKeyName(0, "MarginSet0");
            this.imgList1.Images.SetKeyName(1, "MarginSet2");
            //
            // imgList2
            //
            this.imgList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList2.ImageStream")));
            this.imgList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList2.Images.SetKeyName(0, "MarginSet1");
            //
            // FormOutBlankSetting2
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(420, 346);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOutBlankSetting2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "余白設定";
            this.Controls.SetChildIndex(this.icoHeading, 0);
            this.Controls.SetChildIndex(this.lblHeading, 0);
            this.Controls.SetChildIndex(this.secBody, 0);
            this.secBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPaper)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentSUI.IconBox icoHeading;
        private ComponentSUI.SLabel lblHeading;
        private ComponentSUI.SectionPanel secBody;
        private System.Windows.Forms.HScrollBar hScroll;
        private System.Windows.Forms.VScrollBar vScroll;
        private System.Windows.Forms.PictureBox picPaper;
        private ComponentSUI.SLabel lblH;
        private ComponentSUI.SLabel lblV;
        private ComponentSUI.SButton btnClear;
        private ComponentSUI.SButton btnOK;
        private ComponentSUI.SButton btnCancel;
        private System.Windows.Forms.ImageList imgList1;
        private System.Windows.Forms.ImageList imgList2;
    }
}
