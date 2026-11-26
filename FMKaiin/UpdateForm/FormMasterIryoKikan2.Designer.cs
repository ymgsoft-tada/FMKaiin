
namespace App
{
    partial class FormMasterIryoKikan2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMasterIryoKikan2));
            this.funckey = new App.Controls.FunctionKeyBar();
            this.icoHeading = new App.Controls.IconBox();
            this.lblHeading = new App.Controls.SLabel();
            this.secBody = new App.Controls.SectionPanel();
            this.lblCodeIryo = new App.Controls.SLabel();
            this.iCodeIryo = new App.Controls.STextBox();
            this.lblNameIryo = new App.Controls.SLabel();
            this.iNameIryo = new App.Controls.STextBox();
            this.lblKumiCode = new App.Controls.SLabel();
            this.iKumiCode = new App.Controls.STableComboBox();
            this.lblKbnTaikai = new App.Controls.SLabel();
            this.iKbnTaikai = new App.Controls.SComboBox();
            this.btnClear = new App.Controls.SButton();
            this.grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.secBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 489);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(1016, 66);
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
            this.lblHeading.Text = "医療機関一覧";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblCodeIryo);
            this.secBody.Controls.Add(this.iCodeIryo);
            this.secBody.Controls.Add(this.lblNameIryo);
            this.secBody.Controls.Add(this.iNameIryo);
            this.secBody.Controls.Add(this.lblKumiCode);
            this.secBody.Controls.Add(this.iKumiCode);
            this.secBody.Controls.Add(this.lblKbnTaikai);
            this.secBody.Controls.Add(this.iKbnTaikai);
            this.secBody.Controls.Add(this.btnClear);
            this.secBody.Controls.Add(this.grid);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(996, 423);
            this.secBody.TabIndex = 0;
            // 
            // lblCodeIryo
            // 
            this.lblCodeIryo.BackColor = System.Drawing.Color.Transparent;
            this.lblCodeIryo.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCodeIryo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblCodeIryo.Location = new System.Drawing.Point(16, 20);
            this.lblCodeIryo.Name = "lblCodeIryo";
            this.lblCodeIryo.Size = new System.Drawing.Size(52, 24);
            this.lblCodeIryo.TabIndex = 100;
            this.lblCodeIryo.TabStop = false;
            this.lblCodeIryo.Text = "コード";
            this.lblCodeIryo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // iCodeIryo
            // 
            this.iCodeIryo.Location = new System.Drawing.Point(78, 20);
            this.iCodeIryo.Name = "iCodeIryo";
            this.iCodeIryo.Size = new System.Drawing.Size(76, 24);
            this.iCodeIryo.TabIndex = 1;
            // 
            // lblNameIryo
            // 
            this.lblNameIryo.BackColor = System.Drawing.Color.Transparent;
            this.lblNameIryo.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNameIryo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblNameIryo.Location = new System.Drawing.Point(174, 20);
            this.lblNameIryo.Name = "lblNameIryo";
            this.lblNameIryo.Size = new System.Drawing.Size(40, 24);
            this.lblNameIryo.TabIndex = 101;
            this.lblNameIryo.TabStop = false;
            this.lblNameIryo.Text = "名称";
            this.lblNameIryo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // iNameIryo
            // 
            this.iNameIryo.Location = new System.Drawing.Point(224, 20);
            this.iNameIryo.Name = "iNameIryo";
            this.iNameIryo.Size = new System.Drawing.Size(222, 24);
            this.iNameIryo.TabIndex = 2;
            // 
            // lblKumiCode
            // 
            this.lblKumiCode.BackColor = System.Drawing.Color.Transparent;
            this.lblKumiCode.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKumiCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblKumiCode.Location = new System.Drawing.Point(466, 20);
            this.lblKumiCode.Name = "lblKumiCode";
            this.lblKumiCode.Size = new System.Drawing.Size(68, 24);
            this.lblKumiCode.TabIndex = 102;
            this.lblKumiCode.TabStop = false;
            this.lblKumiCode.Text = "組コード";
            this.lblKumiCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // iKumiCode
            // 
            this.iKumiCode.Location = new System.Drawing.Point(544, 20);
            this.iKumiCode.Name = "iKumiCode";
            this.iKumiCode.Size = new System.Drawing.Size(140, 24);
            this.iKumiCode.TabIndex = 3;
            // 
            // lblKbnTaikai
            // 
            this.lblKbnTaikai.BackColor = System.Drawing.Color.Transparent;
            this.lblKbnTaikai.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKbnTaikai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblKbnTaikai.Location = new System.Drawing.Point(698, 20);
            this.lblKbnTaikai.Name = "lblKbnTaikai";
            this.lblKbnTaikai.Size = new System.Drawing.Size(74, 24);
            this.lblKbnTaikai.TabIndex = 103;
            this.lblKbnTaikai.TabStop = false;
            this.lblKbnTaikai.Text = "退会区分";
            this.lblKbnTaikai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // iKbnTaikai
            // 
            this.iKbnTaikai.ItemHeight = 18;
            this.iKbnTaikai.Location = new System.Drawing.Point(782, 20);
            this.iKbnTaikai.Name = "iKbnTaikai";
            this.iKbnTaikai.Size = new System.Drawing.Size(80, 24);
            this.iKbnTaikai.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(880, 20);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 24);
            this.btnClear.TabIndex = 5;
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
            this.grid.Size = new System.Drawing.Size(964, 351);
            this.grid.TabIndex = 0;
            this.grid.UseCompatibleTextRendering = false;
            // 
            // FormMasterIryoKikan2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1016, 555);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMasterIryoKikan2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "医療機関マスタ";
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
        private App.Controls.SLabel lblCodeIryo;
        private App.Controls.STextBox iCodeIryo;
        private App.Controls.SLabel lblNameIryo;
        private App.Controls.STextBox iNameIryo;
        private App.Controls.SLabel lblKumiCode;
        private App.Controls.STableComboBox iKumiCode;
        private App.Controls.SLabel lblKbnTaikai;
        private App.Controls.SComboBox iKbnTaikai;
        private App.Controls.SButton btnClear;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
    }
}
