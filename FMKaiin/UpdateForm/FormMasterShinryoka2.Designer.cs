
namespace App
{
    partial class FormMasterShinryoka2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMasterShinryoka2));
            this.funckey = new App.Controls.FunctionKeyBar();
            this.icoHeading = new App.Controls.IconBox();
            this.lblHeading = new App.Controls.SLabel();
            this.secBody = new App.Controls.SectionPanel();
            this.lblCodeShinryoka = new App.Controls.SLabel();
            this.iCodeShinryoka = new App.Controls.STextBox();
            this.lblNameShinryoka = new App.Controls.SLabel();
            this.iNameShinryoka = new App.Controls.STextBox();
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
            this.funckey.Size = new System.Drawing.Size(566, 66);
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
            this.lblHeading.Text = "診療科一覧";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblCodeShinryoka);
            this.secBody.Controls.Add(this.iCodeShinryoka);
            this.secBody.Controls.Add(this.lblNameShinryoka);
            this.secBody.Controls.Add(this.iNameShinryoka);
            this.secBody.Controls.Add(this.btnClear);
            this.secBody.Controls.Add(this.grid);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(546, 460);
            this.secBody.TabIndex = 0;
            // 
            // lblCodeShinryoka
            // 
            this.lblCodeShinryoka.BackColor = System.Drawing.Color.Transparent;
            this.lblCodeShinryoka.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCodeShinryoka.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblCodeShinryoka.Location = new System.Drawing.Point(26, 20);
            this.lblCodeShinryoka.Name = "lblCodeShinryoka";
            this.lblCodeShinryoka.Size = new System.Drawing.Size(46, 24);
            this.lblCodeShinryoka.TabIndex = 100;
            this.lblCodeShinryoka.TabStop = false;
            this.lblCodeShinryoka.Text = "コード";
            // 
            // iCodeShinryoka
            // 
            this.iCodeShinryoka.Location = new System.Drawing.Point(78, 20);
            this.iCodeShinryoka.Name = "iCodeShinryoka";
            this.iCodeShinryoka.Size = new System.Drawing.Size(60, 24);
            this.iCodeShinryoka.TabIndex = 1;
            // 
            // lblNameShinryoka
            // 
            this.lblNameShinryoka.BackColor = System.Drawing.Color.Transparent;
            this.lblNameShinryoka.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNameShinryoka.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblNameShinryoka.Location = new System.Drawing.Point(170, 20);
            this.lblNameShinryoka.Name = "lblNameShinryoka";
            this.lblNameShinryoka.Size = new System.Drawing.Size(32, 24);
            this.lblNameShinryoka.TabIndex = 101;
            this.lblNameShinryoka.TabStop = false;
            this.lblNameShinryoka.Text = "名称";
            // 
            // iNameShinryoka
            // 
            this.iNameShinryoka.Location = new System.Drawing.Point(208, 20);
            this.iNameShinryoka.Name = "iNameShinryoka";
            this.iNameShinryoka.Size = new System.Drawing.Size(204, 24);
            this.iNameShinryoka.TabIndex = 2;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(430, 20);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 24);
            this.btnClear.TabIndex = 3;
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
            this.grid.Size = new System.Drawing.Size(514, 388);
            this.grid.TabIndex = 0;
            this.grid.UseCompatibleTextRendering = false;
            // 
            // FormMasterShinryoka2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(566, 592);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMasterShinryoka2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "診療科マスタ";
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
        private App.Controls.SLabel lblCodeShinryoka;
        private App.Controls.STextBox iCodeShinryoka;
        private App.Controls.SLabel lblNameShinryoka;
        private App.Controls.STextBox iNameShinryoka;
        private App.Controls.SButton btnClear;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
    }
}
