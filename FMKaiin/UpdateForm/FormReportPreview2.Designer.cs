namespace App
{
    partial class FormReportPreview2
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
            this.secTool = new ComponentSUI.SectionPanel();
            this.btnSave = new ComponentSUI.ToolbarButton();
            this.btnPrint = new ComponentSUI.ToolbarButton();
            this.btnSetting = new ComponentSUI.ToolbarButton();
            this.sepZoom = new ComponentSUI.BufferedPanel();
            this.btnZoomOut = new ComponentSUI.ToolbarButton();
            this.iZoom = new ComponentSUI.STextBox();
            this.lblPercent = new ComponentSUI.SLabel();
            this.btnZoomIn = new ComponentSUI.ToolbarButton();
            this.iZoomList = new ComponentSUI.SComboBox();
            this.sepPage = new ComponentSUI.BufferedPanel();
            this.btnPageFirst = new ComponentSUI.ToolbarButton();
            this.btnPagePrev = new ComponentSUI.ToolbarButton();
            this.iPage = new ComponentSUI.STextBox();
            this.lblMaxPage = new ComponentSUI.SLabel();
            this.btnPageNext = new ComponentSUI.ToolbarButton();
            this.btnPageLast = new ComponentSUI.ToolbarButton();
            this.ppc = new C1.Win.FlexViewer.C1FlexViewerPane();
            this.secTool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ppc)).BeginInit();
            this.SuspendLayout();
            //
            // secTool
            //
            this.secTool.BackColor = System.Drawing.Color.Transparent;
            this.secTool.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secTool.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secTool.Controls.Add(this.btnSave);
            this.secTool.Controls.Add(this.btnPrint);
            this.secTool.Controls.Add(this.btnSetting);
            this.secTool.Controls.Add(this.sepZoom);
            this.secTool.Controls.Add(this.btnZoomOut);
            this.secTool.Controls.Add(this.iZoom);
            this.secTool.Controls.Add(this.lblPercent);
            this.secTool.Controls.Add(this.btnZoomIn);
            this.secTool.Controls.Add(this.iZoomList);
            this.secTool.Controls.Add(this.sepPage);
            this.secTool.Controls.Add(this.btnPageFirst);
            this.secTool.Controls.Add(this.btnPagePrev);
            this.secTool.Controls.Add(this.iPage);
            this.secTool.Controls.Add(this.lblMaxPage);
            this.secTool.Controls.Add(this.btnPageNext);
            this.secTool.Controls.Add(this.btnPageLast);
            this.secTool.CornerRadius = 8;
            this.secTool.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secTool.HeaderHeight = 0;
            this.secTool.Location = new System.Drawing.Point(10, 8);
            this.secTool.Name = "secTool";
            this.secTool.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secTool.ShadowSize = 6;
            this.secTool.Size = new System.Drawing.Size(930, 60);
            this.secTool.TabIndex = 0;
            //
            // btnSave
            //
            this.btnSave.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.btnSave.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(246)))));
            this.btnSave.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(96)))), ((int)(((byte)(160)))));
            this.btnSave.IconKind = ComponentSUI.MenuIconKind.Save;
            this.btnSave.IconLineWeight = 0.85F;
            this.btnSave.IconSize = 24;
            this.btnSave.Location = new System.Drawing.Point(8, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 40);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "PDF保存";
            //
            // btnPrint
            //
            this.btnPrint.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.btnPrint.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(246)))));
            this.btnPrint.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(96)))), ((int)(((byte)(160)))));
            this.btnPrint.IconKind = ComponentSUI.MenuIconKind.Printer;
            this.btnPrint.IconLineWeight = 0.85F;
            this.btnPrint.IconSize = 24;
            this.btnPrint.Location = new System.Drawing.Point(122, 7);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(84, 40);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "印刷";
            //
            // btnSetting
            //
            this.btnSetting.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.btnSetting.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(246)))));
            this.btnSetting.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(96)))), ((int)(((byte)(160)))));
            this.btnSetting.IconKind = ComponentSUI.MenuIconKind.Sliders;
            this.btnSetting.IconLineWeight = 0.85F;
            this.btnSetting.IconSize = 24;
            this.btnSetting.Location = new System.Drawing.Point(208, 7);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(112, 40);
            this.btnSetting.TabIndex = 2;
            this.btnSetting.Text = "余白設定";
            //
            // sepZoom
            //
            this.sepZoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.sepZoom.Location = new System.Drawing.Point(328, 11);
            this.sepZoom.Name = "sepZoom";
            this.sepZoom.Size = new System.Drawing.Size(1, 32);
            this.sepZoom.TabIndex = 103;
            //
            // btnZoomOut
            //
            this.btnZoomOut.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnZoomOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.btnZoomOut.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(246)))));
            this.btnZoomOut.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(96)))), ((int)(((byte)(160)))));
            this.btnZoomOut.IconKind = ComponentSUI.MenuIconKind.ZoomOut;
            this.btnZoomOut.IconLineWeight = 0.85F;
            this.btnZoomOut.IconSize = 24;
            this.btnZoomOut.Location = new System.Drawing.Point(336, 7);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(40, 40);
            this.btnZoomOut.TabIndex = 3;
            this.btnZoomOut.Text = "";
            //
            // iZoom
            //
            this.iZoom.Format = "9";
            this.iZoom.Location = new System.Drawing.Point(378, 15);
            this.iZoom.MaxLength = 4;
            this.iZoom.Name = "iZoom";
            this.iZoom.Size = new System.Drawing.Size(54, 24);
            this.iZoom.TabIndex = 4;
            this.iZoom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lblPercent
            //
            this.lblPercent.BackColor = System.Drawing.Color.Transparent;
            this.lblPercent.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPercent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.lblPercent.Location = new System.Drawing.Point(435, 15);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(16, 24);
            this.lblPercent.TabIndex = 106;
            this.lblPercent.TabStop = false;
            this.lblPercent.Text = "%";
            //
            // btnZoomIn
            //
            this.btnZoomIn.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnZoomIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.btnZoomIn.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(246)))));
            this.btnZoomIn.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(96)))), ((int)(((byte)(160)))));
            this.btnZoomIn.IconKind = ComponentSUI.MenuIconKind.ZoomIn;
            this.btnZoomIn.IconLineWeight = 0.85F;
            this.btnZoomIn.IconSize = 24;
            this.btnZoomIn.Location = new System.Drawing.Point(452, 7);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(40, 40);
            this.btnZoomIn.TabIndex = 5;
            this.btnZoomIn.Text = "";
            //
            // iZoomList
            //
            this.iZoomList.Location = new System.Drawing.Point(498, 15);
            this.iZoomList.Name = "iZoomList";
            this.iZoomList.PlaceholderText = "倍率を選択";
            this.iZoomList.Size = new System.Drawing.Size(130, 24);
            this.iZoomList.TabIndex = 6;
            //
            // sepPage
            //
            this.sepPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.sepPage.Location = new System.Drawing.Point(638, 11);
            this.sepPage.Name = "sepPage";
            this.sepPage.Size = new System.Drawing.Size(1, 32);
            this.sepPage.TabIndex = 109;
            //
            // btnPageFirst
            //
            this.btnPageFirst.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnPageFirst.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.btnPageFirst.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(246)))));
            this.btnPageFirst.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(96)))), ((int)(((byte)(160)))));
            this.btnPageFirst.IconKind = ComponentSUI.MenuIconKind.ChevronFirst;
            this.btnPageFirst.IconLineWeight = 0.85F;
            this.btnPageFirst.IconSize = 24;
            this.btnPageFirst.Location = new System.Drawing.Point(646, 7);
            this.btnPageFirst.Name = "btnPageFirst";
            this.btnPageFirst.Size = new System.Drawing.Size(40, 40);
            this.btnPageFirst.TabIndex = 7;
            this.btnPageFirst.Text = "";
            //
            // btnPagePrev
            //
            this.btnPagePrev.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnPagePrev.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.btnPagePrev.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(246)))));
            this.btnPagePrev.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(96)))), ((int)(((byte)(160)))));
            this.btnPagePrev.IconKind = ComponentSUI.MenuIconKind.ChevronLeft;
            this.btnPagePrev.IconLineWeight = 0.85F;
            this.btnPagePrev.IconSize = 24;
            this.btnPagePrev.Location = new System.Drawing.Point(686, 7);
            this.btnPagePrev.Name = "btnPagePrev";
            this.btnPagePrev.Size = new System.Drawing.Size(40, 40);
            this.btnPagePrev.TabIndex = 8;
            this.btnPagePrev.Text = "";
            //
            // iPage
            //
            this.iPage.Format = "9";
            this.iPage.Location = new System.Drawing.Point(730, 15);
            this.iPage.MaxLength = 4;
            this.iPage.Name = "iPage";
            this.iPage.Size = new System.Drawing.Size(50, 24);
            this.iPage.TabIndex = 9;
            this.iPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lblMaxPage
            //
            this.lblMaxPage.BackColor = System.Drawing.Color.Transparent;
            this.lblMaxPage.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMaxPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.lblMaxPage.Location = new System.Drawing.Point(786, 15);
            this.lblMaxPage.Name = "lblMaxPage";
            this.lblMaxPage.Size = new System.Drawing.Size(52, 24);
            this.lblMaxPage.TabIndex = 113;
            this.lblMaxPage.TabStop = false;
            this.lblMaxPage.Text = "/ 999";
            //
            // btnPageNext
            //
            this.btnPageNext.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnPageNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.btnPageNext.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(246)))));
            this.btnPageNext.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(96)))), ((int)(((byte)(160)))));
            this.btnPageNext.IconKind = ComponentSUI.MenuIconKind.ChevronRight;
            this.btnPageNext.IconLineWeight = 0.85F;
            this.btnPageNext.IconSize = 24;
            this.btnPageNext.Location = new System.Drawing.Point(838, 7);
            this.btnPageNext.Name = "btnPageNext";
            this.btnPageNext.Size = new System.Drawing.Size(40, 40);
            this.btnPageNext.TabIndex = 10;
            this.btnPageNext.Text = "";
            //
            // btnPageLast
            //
            this.btnPageLast.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnPageLast.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.btnPageLast.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(204)))), ((int)(((byte)(226)))), ((int)(((byte)(246)))));
            this.btnPageLast.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(96)))), ((int)(((byte)(160)))));
            this.btnPageLast.IconKind = ComponentSUI.MenuIconKind.ChevronLast;
            this.btnPageLast.IconLineWeight = 0.85F;
            this.btnPageLast.IconSize = 24;
            this.btnPageLast.Location = new System.Drawing.Point(878, 7);
            this.btnPageLast.Name = "btnPageLast";
            this.btnPageLast.Size = new System.Drawing.Size(40, 40);
            this.btnPageLast.TabIndex = 11;
            this.btnPageLast.Text = "";
            //
            // ppc
            //
            this.ppc.Location = new System.Drawing.Point(10, 72);
            this.ppc.Name = "ppc";
            this.ppc.Size = new System.Drawing.Size(930, 679);
            this.ppc.TabIndex = 1;
            //
            // FormReportPreview2
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(950, 761);
            this.Controls.Add(this.secTool);
            this.Controls.Add(this.ppc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "FormReportPreview2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "印刷プレビュー";
            this.Controls.SetChildIndex(this.ppc, 0);
            this.Controls.SetChildIndex(this.secTool, 0);
            this.secTool.ResumeLayout(false);
            this.secTool.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ppc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentSUI.SectionPanel secTool;
        private ComponentSUI.ToolbarButton btnSave;
        private ComponentSUI.ToolbarButton btnPrint;
        private ComponentSUI.ToolbarButton btnSetting;
        private ComponentSUI.BufferedPanel sepZoom;
        private ComponentSUI.ToolbarButton btnZoomOut;
        private ComponentSUI.STextBox iZoom;
        private ComponentSUI.SLabel lblPercent;
        private ComponentSUI.ToolbarButton btnZoomIn;
        private ComponentSUI.SComboBox iZoomList;
        private ComponentSUI.BufferedPanel sepPage;
        private ComponentSUI.ToolbarButton btnPageFirst;
        private ComponentSUI.ToolbarButton btnPagePrev;
        private ComponentSUI.STextBox iPage;
        private ComponentSUI.SLabel lblMaxPage;
        private ComponentSUI.ToolbarButton btnPageNext;
        private ComponentSUI.ToolbarButton btnPageLast;
        private C1.Win.FlexViewer.C1FlexViewerPane ppc;
    }
}
