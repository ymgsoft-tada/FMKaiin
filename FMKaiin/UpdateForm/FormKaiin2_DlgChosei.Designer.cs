
namespace App
{
    partial class FormKaiin2_DlgChosei
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKaiin2_DlgChosei));
            this.funckey = new ComponentSUI.FunctionKeyBar();
            this.icoHeading = new ComponentSUI.IconBox();
            this.lblHeading = new ComponentSUI.SLabel();
            this.secBody = new ComponentSUI.SectionPanel();
            this.lblKaiin = new ComponentSUI.SLabel();
            this.iKaiin = new ComponentSUI.STextBox();
            this.lblNote = new ComponentSUI.SLabel();
            this.iDateYM = new ComponentSUI.SDateBox();
            this.iKaihi = new ComponentSUI.STableComboBox();
            this.iCost = new ComponentSUI.SNumberBox();
            this.iShiharai = new ComponentSUI.SComboBox();
            this.iMemo = new ComponentSUI.STextBox();
            this.grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.secBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 455);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(840, 66);
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
            this.lblHeading.Text = "会費予定額調整";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblKaiin);
            this.secBody.Controls.Add(this.iKaiin);
            this.secBody.Controls.Add(this.lblNote);
            this.secBody.Controls.Add(this.iDateYM);
            this.secBody.Controls.Add(this.iKaihi);
            this.secBody.Controls.Add(this.iCost);
            this.secBody.Controls.Add(this.iShiharai);
            this.secBody.Controls.Add(this.iMemo);
            this.secBody.Controls.Add(this.grid);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(820, 389);
            this.secBody.TabIndex = 0;
            // 
            // lblKaiin
            // 
            this.lblKaiin.BackColor = System.Drawing.Color.Transparent;
            this.lblKaiin.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKaiin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblKaiin.Location = new System.Drawing.Point(26, 24);
            this.lblKaiin.Name = "lblKaiin";
            this.lblKaiin.Size = new System.Drawing.Size(118, 24);
            this.lblKaiin.TabIndex = 103;
            this.lblKaiin.TabStop = false;
            this.lblKaiin.Text = "会員";
            // 
            // iKaiin
            // 
            this.iKaiin.Enabled = false;
            this.iKaiin.Location = new System.Drawing.Point(150, 24);
            this.iKaiin.Name = "iKaiin";
            this.iKaiin.Size = new System.Drawing.Size(270, 24);
            this.iKaiin.TabIndex = 10;
            // 
            // lblNote
            // 
            this.lblNote.BackColor = System.Drawing.Color.Transparent;
            this.lblNote.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblNote.Location = new System.Drawing.Point(578, 24);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(226, 24);
            this.lblNote.TabIndex = 104;
            this.lblNote.TabStop = false;
            this.lblNote.Text = "グリッドで直接編集できます。";
            this.lblNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // iDateYM
            // 
            this.iDateYM.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.iDateYM.DateFormat = ComponentSUI.SDateFormat.YearMonth;
            this.iDateYM.Location = new System.Drawing.Point(40, 100);
            this.iDateYM.Name = "iDateYM";
            this.iDateYM.Size = new System.Drawing.Size(130, 24);
            this.iDateYM.TabIndex = 20;
            // 
            // iKaihi
            // 
            this.iKaihi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.iKaihi.Location = new System.Drawing.Point(180, 100);
            this.iKaihi.Name = "iKaihi";
            this.iKaihi.Size = new System.Drawing.Size(200, 24);
            this.iKaihi.TabIndex = 21;
            // 
            // iCost
            // 
            this.iCost.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.iCost.Location = new System.Drawing.Point(390, 100);
            this.iCost.Name = "iCost";
            this.iCost.Size = new System.Drawing.Size(120, 24);
            this.iCost.TabIndex = 22;
            // 
            // iShiharai
            // 
            this.iShiharai.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.iShiharai.ItemHeight = 18;
            this.iShiharai.Location = new System.Drawing.Point(520, 100);
            this.iShiharai.Name = "iShiharai";
            this.iShiharai.Size = new System.Drawing.Size(100, 24);
            this.iShiharai.TabIndex = 23;
            // 
            // iMemo
            // 
            this.iMemo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.iMemo.Location = new System.Drawing.Point(630, 100);
            this.iMemo.Name = "iMemo";
            this.iMemo.Size = new System.Drawing.Size(150, 24);
            this.iMemo.TabIndex = 24;
            // 
            // grid
            // 
            this.grid.CaptionHeight = 16;
            this.grid.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
            this.grid.Images.Add(((System.Drawing.Image)(resources.GetObject("grid.Images"))));
            this.grid.Location = new System.Drawing.Point(16, 61);
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
            this.grid.Size = new System.Drawing.Size(788, 306);
            this.grid.TabIndex = 0;
            this.grid.UseCompatibleTextRendering = false;
            // 
            // FormKaiin2_DlgChosei
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(840, 521);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormKaiin2_DlgChosei";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "会員情報：会費調整";
            this.Controls.SetChildIndex(this.funckey, 0);
            this.Controls.SetChildIndex(this.icoHeading, 0);
            this.Controls.SetChildIndex(this.lblHeading, 0);
            this.Controls.SetChildIndex(this.secBody, 0);
            this.secBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentSUI.FunctionKeyBar funckey;
        private ComponentSUI.IconBox icoHeading;
        private ComponentSUI.SLabel lblHeading;
        private ComponentSUI.SectionPanel secBody;
        private ComponentSUI.SLabel lblKaiin;
        private ComponentSUI.STextBox iKaiin;
        private ComponentSUI.SLabel lblNote;
        private ComponentSUI.SDateBox iDateYM;
        private ComponentSUI.STableComboBox iKaihi;
        private ComponentSUI.SNumberBox iCost;
        private ComponentSUI.SComboBox iShiharai;
        private ComponentSUI.STextBox iMemo;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
    }
}
