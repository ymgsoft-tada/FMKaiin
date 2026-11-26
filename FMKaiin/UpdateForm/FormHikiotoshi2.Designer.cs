
namespace App
{
    partial class FormHikiotoshi2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHikiotoshi2));
            this.funckey = new ComponentSUI.FunctionKeyBar();
            this.icoHeading = new ComponentSUI.IconBox();
            this.lblHeading = new ComponentSUI.SLabel();
            this.lblCreateDate = new ComponentSUI.SLabel();
            this.lblUpdateUser = new ComponentSUI.SLabel();
            this.secBody = new ComponentSUI.SectionPanel();
            this.lblDateYM = new ComponentSUI.SLabel();
            this.iDateYM = new ComponentSUI.SDateBox();
            this.grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.lblTotalCount = new ComponentSUI.SLabel();
            this.iTotalCount = new ComponentSUI.SNumberBox();
            this.lblTotalCost = new ComponentSUI.SLabel();
            this.iTotalCost = new ComponentSUI.SNumberBox();
            this.secBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 651);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(612, 66);
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
            this.lblHeading.Text = "引落データ作成";
            // 
            // lblCreateDate
            // 
            this.lblCreateDate.BackColor = System.Drawing.Color.Transparent;
            this.lblCreateDate.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCreateDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblCreateDate.Location = new System.Drawing.Point(342, 8);
            this.lblCreateDate.Name = "lblCreateDate";
            this.lblCreateDate.Size = new System.Drawing.Size(260, 18);
            this.lblCreateDate.TabIndex = 103;
            this.lblCreateDate.TabStop = false;
            this.lblCreateDate.Text = "最終更新：";
            this.lblCreateDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUpdateUser
            // 
            this.lblUpdateUser.BackColor = System.Drawing.Color.Transparent;
            this.lblUpdateUser.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblUpdateUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
            this.lblUpdateUser.Location = new System.Drawing.Point(342, 26);
            this.lblUpdateUser.Name = "lblUpdateUser";
            this.lblUpdateUser.Size = new System.Drawing.Size(260, 18);
            this.lblUpdateUser.TabIndex = 104;
            this.lblUpdateUser.TabStop = false;
            this.lblUpdateUser.Text = "";
            this.lblUpdateUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblDateYM);
            this.secBody.Controls.Add(this.iDateYM);
            this.secBody.Controls.Add(this.grid);
            this.secBody.Controls.Add(this.lblTotalCount);
            this.secBody.Controls.Add(this.iTotalCount);
            this.secBody.Controls.Add(this.lblTotalCost);
            this.secBody.Controls.Add(this.iTotalCost);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(592, 585);
            this.secBody.TabIndex = 0;
            // 
            // lblDateYM
            // 
            this.lblDateYM.BackColor = System.Drawing.Color.Transparent;
            this.lblDateYM.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDateYM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblDateYM.Location = new System.Drawing.Point(12, 20);
            this.lblDateYM.Name = "lblDateYM";
            this.lblDateYM.Required = true;
            this.lblDateYM.Size = new System.Drawing.Size(84, 24);
            this.lblDateYM.TabIndex = 100;
            this.lblDateYM.TabStop = false;
            this.lblDateYM.Text = "処理月度";
            this.lblDateYM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // iDateYM
            // 
            this.iDateYM.DateFormat = ComponentSUI.SDateFormat.YearMonth;
            this.iDateYM.Location = new System.Drawing.Point(106, 20);
            this.iDateYM.Name = "iDateYM";
            this.iDateYM.PlaceholderText = "入力必須";
            this.iDateYM.Size = new System.Drawing.Size(150, 24);
            this.iDateYM.TabIndex = 0;
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
            this.grid.Size = new System.Drawing.Size(560, 473);
            this.grid.TabIndex = 1;
            this.grid.UseCompatibleTextRendering = false;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalCount.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTotalCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblTotalCount.Location = new System.Drawing.Point(196, 539);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(74, 24);
            this.lblTotalCount.TabIndex = 101;
            this.lblTotalCount.TabStop = false;
            this.lblTotalCount.Text = "合計人数";
            this.lblTotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // iTotalCount
            // 
            this.iTotalCount.Enabled = false;
            this.iTotalCount.Location = new System.Drawing.Point(280, 539);
            this.iTotalCount.Name = "iTotalCount";
            this.iTotalCount.Size = new System.Drawing.Size(90, 24);
            this.iTotalCount.TabIndex = 2;
            // 
            // lblTotalCost
            // 
            this.lblTotalCost.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalCost.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTotalCost.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblTotalCost.Location = new System.Drawing.Point(378, 539);
            this.lblTotalCost.Name = "lblTotalCost";
            this.lblTotalCost.Size = new System.Drawing.Size(58, 24);
            this.lblTotalCost.TabIndex = 102;
            this.lblTotalCost.TabStop = false;
            this.lblTotalCost.Text = "月額計";
            this.lblTotalCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // iTotalCost
            // 
            this.iTotalCost.Enabled = false;
            this.iTotalCost.Location = new System.Drawing.Point(446, 539);
            this.iTotalCost.Name = "iTotalCost";
            this.iTotalCost.Size = new System.Drawing.Size(130, 24);
            this.iTotalCost.TabIndex = 3;
            // 
            // FormHikiotoshi2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(612, 717);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblUpdateUser);
            this.Controls.Add(this.lblCreateDate);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormHikiotoshi2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "引落データ作成";
            this.Controls.SetChildIndex(this.funckey, 0);
            this.Controls.SetChildIndex(this.icoHeading, 0);
            this.Controls.SetChildIndex(this.lblHeading, 0);
            this.Controls.SetChildIndex(this.lblCreateDate, 0);
            this.Controls.SetChildIndex(this.lblUpdateUser, 0);
            this.Controls.SetChildIndex(this.secBody, 0);
            this.secBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentSUI.FunctionKeyBar funckey;
        private ComponentSUI.IconBox icoHeading;
        private ComponentSUI.SLabel lblHeading;
        private ComponentSUI.SLabel lblCreateDate;
        private ComponentSUI.SLabel lblUpdateUser;
        private ComponentSUI.SectionPanel secBody;
        private ComponentSUI.SLabel lblDateYM;
        private ComponentSUI.SDateBox iDateYM;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
        private ComponentSUI.SLabel lblTotalCount;
        private ComponentSUI.SNumberBox iTotalCount;
        private ComponentSUI.SLabel lblTotalCost;
        private ComponentSUI.SNumberBox iTotalCost;
    }
}
