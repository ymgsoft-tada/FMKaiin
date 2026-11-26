
namespace App
{
    partial class FormHikiotoshi2_DlgEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHikiotoshi2_DlgEntry));
            this.funckey = new App.Controls.FunctionKeyBar();
            this.icoHeading = new App.Controls.IconBox();
            this.lblHeading = new App.Controls.SLabel();
            this.secBody = new App.Controls.SectionPanel();
            this.lblDateYM = new App.Controls.SLabel();
            this.iDateYM = new App.Controls.STextBox();
            this.lblKaiin = new App.Controls.SLabel();
            this.iKaiin = new App.Controls.STextBox();
            this.lblNote = new App.Controls.SLabel();
            this.iKaihi = new App.Controls.STableComboBox();
            this.iCost = new App.Controls.SNumberBox();
            this.iMemo = new App.Controls.STextBox();
            this.grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.lblTotalCost = new App.Controls.SLabel();
            this.iTotalCost = new App.Controls.SNumberBox();
            this.secBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 428);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(664, 66);
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
            this.lblHeading.Text = "引落データ編集";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblDateYM);
            this.secBody.Controls.Add(this.iDateYM);
            this.secBody.Controls.Add(this.lblKaiin);
            this.secBody.Controls.Add(this.iKaiin);
            this.secBody.Controls.Add(this.lblNote);
            this.secBody.Controls.Add(this.iKaihi);
            this.secBody.Controls.Add(this.iCost);
            this.secBody.Controls.Add(this.iMemo);
            this.secBody.Controls.Add(this.grid);
            this.secBody.Controls.Add(this.lblTotalCost);
            this.secBody.Controls.Add(this.iTotalCost);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(644, 362);
            this.secBody.TabIndex = 0;
            // 
            // lblDateYM
            // 
            this.lblDateYM.BackColor = System.Drawing.Color.Transparent;
            this.lblDateYM.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDateYM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblDateYM.Location = new System.Drawing.Point(26, 24);
            this.lblDateYM.Name = "lblDateYM";
            this.lblDateYM.Size = new System.Drawing.Size(118, 24);
            this.lblDateYM.TabIndex = 105;
            this.lblDateYM.TabStop = false;
            this.lblDateYM.Text = "処理月度";
            // 
            // iDateYM
            // 
            this.iDateYM.Enabled = false;
            this.iDateYM.Location = new System.Drawing.Point(150, 24);
            this.iDateYM.Name = "iDateYM";
            this.iDateYM.Size = new System.Drawing.Size(130, 24);
            this.iDateYM.TabIndex = 10;
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
            // iKaiin
            // 
            this.iKaiin.Enabled = false;
            this.iKaiin.Location = new System.Drawing.Point(150, 55);
            this.iKaiin.Name = "iKaiin";
            this.iKaiin.Size = new System.Drawing.Size(270, 24);
            this.iKaiin.TabIndex = 11;
            // 
            // lblNote
            // 
            this.lblNote.BackColor = System.Drawing.Color.Transparent;
            this.lblNote.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblNote.Location = new System.Drawing.Point(428, 55);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(200, 24);
            this.lblNote.TabIndex = 107;
            this.lblNote.TabStop = false;
            this.lblNote.Text = "グリッドで直接編集できます。";
            this.lblNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // iKaihi
            // 
            this.iKaihi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.iKaihi.Location = new System.Drawing.Point(40, 130);
            this.iKaihi.Name = "iKaihi";
            this.iKaihi.Size = new System.Drawing.Size(180, 24);
            this.iKaihi.TabIndex = 20;
            // 
            // iCost
            // 
            this.iCost.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.iCost.Location = new System.Drawing.Point(230, 130);
            this.iCost.Name = "iCost";
            this.iCost.Size = new System.Drawing.Size(150, 24);
            this.iCost.TabIndex = 21;
            // 
            // iMemo
            // 
            this.iMemo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.iMemo.Location = new System.Drawing.Point(390, 130);
            this.iMemo.Name = "iMemo";
            this.iMemo.Size = new System.Drawing.Size(200, 24);
            this.iMemo.TabIndex = 22;
            // 
            // grid
            // 
            this.grid.CaptionHeight = 16;
            this.grid.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
            this.grid.Images.Add(((System.Drawing.Image)(resources.GetObject("grid.Images"))));
            this.grid.Location = new System.Drawing.Point(16, 92);
            this.grid.Name = "grid";
            this.grid.PreviewInfo.Caption = "印刷プレビューウィンドウ";
            this.grid.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.grid.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.grid.PreviewInfo.ZoomFactor = 75D;
            this.grid.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
            this.grid.PrintInfo.MeasurementPrinterName = null;
            this.grid.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("grid.PrintInfo.PageSettings")));
            this.grid.PropBag = resources.GetString("grid.PropBag");
            this.grid.RowHeight = 14;
            this.grid.Size = new System.Drawing.Size(612, 214);
            this.grid.TabIndex = 0;
            this.grid.UseCompatibleTextRendering = false;
            // 
            // lblTotalCost
            // 
            this.lblTotalCost.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalCost.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTotalCost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblTotalCost.Location = new System.Drawing.Point(432, 316);
            this.lblTotalCost.Name = "lblTotalCost";
            this.lblTotalCost.Size = new System.Drawing.Size(60, 24);
            this.lblTotalCost.TabIndex = 108;
            this.lblTotalCost.TabStop = false;
            this.lblTotalCost.Text = "引落金額";
            // 
            // iTotalCost
            // 
            this.iTotalCost.Enabled = false;
            this.iTotalCost.Location = new System.Drawing.Point(498, 316);
            this.iTotalCost.Name = "iTotalCost";
            this.iTotalCost.Size = new System.Drawing.Size(130, 24);
            this.iTotalCost.TabIndex = 12;
            // 
            // FormHikiotoshi2_DlgEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(664, 494);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormHikiotoshi2_DlgEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "引落データ作成：編集";
            this.Controls.SetChildIndex(this.funckey, 0);
            this.Controls.SetChildIndex(this.icoHeading, 0);
            this.Controls.SetChildIndex(this.lblHeading, 0);
            this.Controls.SetChildIndex(this.secBody, 0);
            this.secBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private App.Controls.FunctionKeyBar funckey;
        private App.Controls.IconBox icoHeading;
        private App.Controls.SLabel lblHeading;
        private App.Controls.SectionPanel secBody;
        private App.Controls.SLabel lblDateYM;
        private App.Controls.STextBox iDateYM;
        private App.Controls.SLabel lblKaiin;
        private App.Controls.STextBox iKaiin;
        private App.Controls.SLabel lblNote;
        private App.Controls.STableComboBox iKaihi;
        private App.Controls.SNumberBox iCost;
        private App.Controls.STextBox iMemo;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
        private App.Controls.SLabel lblTotalCost;
        private App.Controls.SNumberBox iTotalCost;
    }
}
