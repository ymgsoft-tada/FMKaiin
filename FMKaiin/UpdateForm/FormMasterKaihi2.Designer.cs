
namespace App
{
    partial class FormMasterKaihi2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMasterKaihi2));
            this.funckey = new App.Controls.FunctionKeyBar();
            this.icoHeading = new App.Controls.IconBox();
            this.lblHeading = new App.Controls.SLabel();
            this.secBody = new App.Controls.SectionPanel();
            this.lblKbnKaihi = new App.Controls.SLabel();
            this.iKbnKaihi = new App.Controls.SComboBox();
            this.lblCodeKaihi = new App.Controls.SLabel();
            this.iCodeKaihi = new App.Controls.STextBox();
            this.lblNameKaihi = new App.Controls.SLabel();
            this.iNameKaihi = new App.Controls.STextBox();
            this.lblKbnUsed = new App.Controls.SLabel();
            this.iKbnUsed = new App.Controls.SComboBox();
            this.btnClear = new App.Controls.SButton();
            this.grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.secBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 526);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(986, 66);
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
            this.lblHeading.Text = "医会会費一覧";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblKbnKaihi);
            this.secBody.Controls.Add(this.iKbnKaihi);
            this.secBody.Controls.Add(this.lblCodeKaihi);
            this.secBody.Controls.Add(this.iCodeKaihi);
            this.secBody.Controls.Add(this.lblNameKaihi);
            this.secBody.Controls.Add(this.iNameKaihi);
            this.secBody.Controls.Add(this.lblKbnUsed);
            this.secBody.Controls.Add(this.iKbnUsed);
            this.secBody.Controls.Add(this.btnClear);
            this.secBody.Controls.Add(this.grid);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(966, 460);
            this.secBody.TabIndex = 0;
            // 
            // lblKbnKaihi
            // 
            this.lblKbnKaihi.BackColor = System.Drawing.Color.Transparent;
            this.lblKbnKaihi.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKbnKaihi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblKbnKaihi.Location = new System.Drawing.Point(26, 20);
            this.lblKbnKaihi.Name = "lblKbnKaihi";
            this.lblKbnKaihi.Size = new System.Drawing.Size(32, 24);
            this.lblKbnKaihi.TabIndex = 100;
            this.lblKbnKaihi.TabStop = false;
            this.lblKbnKaihi.Text = "区分";
            // 
            // iKbnKaihi
            // 
            this.iKbnKaihi.ItemHeight = 18;
            this.iKbnKaihi.Location = new System.Drawing.Point(64, 20);
            this.iKbnKaihi.Name = "iKbnKaihi";
            this.iKbnKaihi.Size = new System.Drawing.Size(120, 24);
            this.iKbnKaihi.TabIndex = 0;
            // 
            // lblCodeKaihi
            // 
            this.lblCodeKaihi.BackColor = System.Drawing.Color.Transparent;
            this.lblCodeKaihi.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCodeKaihi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblCodeKaihi.Location = new System.Drawing.Point(216, 20);
            this.lblCodeKaihi.Name = "lblCodeKaihi";
            this.lblCodeKaihi.Size = new System.Drawing.Size(46, 24);
            this.lblCodeKaihi.TabIndex = 101;
            this.lblCodeKaihi.TabStop = false;
            this.lblCodeKaihi.Text = "コード";
            // 
            // iCodeKaihi
            // 
            this.iCodeKaihi.Location = new System.Drawing.Point(268, 20);
            this.iCodeKaihi.Name = "iCodeKaihi";
            this.iCodeKaihi.Size = new System.Drawing.Size(76, 24);
            this.iCodeKaihi.TabIndex = 1;
            // 
            // lblNameKaihi
            // 
            this.lblNameKaihi.BackColor = System.Drawing.Color.Transparent;
            this.lblNameKaihi.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNameKaihi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblNameKaihi.Location = new System.Drawing.Point(376, 20);
            this.lblNameKaihi.Name = "lblNameKaihi";
            this.lblNameKaihi.Size = new System.Drawing.Size(32, 24);
            this.lblNameKaihi.TabIndex = 102;
            this.lblNameKaihi.TabStop = false;
            this.lblNameKaihi.Text = "名称";
            // 
            // iNameKaihi
            // 
            this.iNameKaihi.Location = new System.Drawing.Point(414, 20);
            this.iNameKaihi.Name = "iNameKaihi";
            this.iNameKaihi.Size = new System.Drawing.Size(230, 24);
            this.iNameKaihi.TabIndex = 2;
            // 
            // lblKbnUsed
            // 
            this.lblKbnUsed.BackColor = System.Drawing.Color.Transparent;
            this.lblKbnUsed.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKbnUsed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblKbnUsed.Location = new System.Drawing.Point(676, 20);
            this.lblKbnUsed.Name = "lblKbnUsed";
            this.lblKbnUsed.Size = new System.Drawing.Size(60, 24);
            this.lblKbnUsed.TabIndex = 103;
            this.lblKbnUsed.TabStop = false;
            this.lblKbnUsed.Text = "検索表示";
            // 
            // iKbnUsed
            // 
            this.iKbnUsed.ItemHeight = 18;
            this.iKbnUsed.Location = new System.Drawing.Point(742, 20);
            this.iKbnUsed.Name = "iKbnUsed";
            this.iKbnUsed.Size = new System.Drawing.Size(90, 24);
            this.iKbnUsed.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(850, 20);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 24);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "検索クリア";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // grid
            // 
            this.grid.CaptionHeight = 16;
            this.grid.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
            this.grid.Images.Add(((System.Drawing.Image)(resources.GetObject("grid.Images"))));
            this.grid.Location = new System.Drawing.Point(16, 56);
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
            this.grid.Size = new System.Drawing.Size(934, 388);
            this.grid.TabIndex = 5;
            this.grid.UseCompatibleTextRendering = false;
            // 
            // FormMasterKaihi2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(986, 592);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMasterKaihi2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "医会会費マスタ";
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
        private App.Controls.SLabel lblKbnKaihi;
        private App.Controls.SComboBox iKbnKaihi;
        private App.Controls.SLabel lblCodeKaihi;
        private App.Controls.STextBox iCodeKaihi;
        private App.Controls.SLabel lblNameKaihi;
        private App.Controls.STextBox iNameKaihi;
        private App.Controls.SLabel lblKbnUsed;
        private App.Controls.SComboBox iKbnUsed;
        private App.Controls.SButton btnClear;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
    }
}
