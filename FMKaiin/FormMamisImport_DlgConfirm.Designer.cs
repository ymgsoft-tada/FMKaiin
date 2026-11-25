namespace App
{
	partial class FormMamisImport_DlgConfirm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMamisImport_DlgConfirm));
			this.funckey = new App.Controls.FunctionKeyBar();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.lblSummary = new YControlLabelEx.YcLabelEx();
			this.lblDetail = new YControlLabelEx.YcLabelEx();
			this.gridHead = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.gridDetail = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			((System.ComponentModel.ISupportInitialize)(this.gridHead)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridDetail)).BeginInit();
			this.SuspendLayout();
			// 
			// funckey
			// 
			this.funckey.Location = new System.Drawing.Point(0, 625);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(1100, 66);
			this.funckey.TabIndex = 35;
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor2 = System.Drawing.Color.Empty;
			this.lblTitle.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblTitle.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblTitle.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblTitle.IconImage = null;
			this.lblTitle.Location = new System.Drawing.Point(37, 23);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblTitle.Size = new System.Drawing.Size(299, 23);
			this.lblTitle.TabIndex = 343;
			this.lblTitle.Text = "取込確認";
			// 
			// ycLabelEx2
			// 
			this.ycLabelEx2.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx2.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx2.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx2.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.IconImage = null;
			this.ycLabelEx2.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx2.Location = new System.Drawing.Point(21, 19);
			this.ycLabelEx2.Name = "ycLabelEx2";
			this.ycLabelEx2.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.Size = new System.Drawing.Size(10, 30);
			this.ycLabelEx2.TabIndex = 342;
			// 
			// lblSummary
			// 
			this.lblSummary.BackColor2 = System.Drawing.Color.Empty;
			this.lblSummary.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblSummary.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblSummary.IconImage = null;
			this.lblSummary.Location = new System.Drawing.Point(26, 60);
			this.lblSummary.Name = "lblSummary";
			this.lblSummary.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblSummary.Size = new System.Drawing.Size(1048, 23);
			this.lblSummary.TabIndex = 344;
			this.lblSummary.Text = "CSVの会員数";
			// 
			// lblDetail
			// 
			this.lblDetail.BackColor2 = System.Drawing.Color.Empty;
			this.lblDetail.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblDetail.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblDetail.IconImage = null;
			this.lblDetail.Location = new System.Drawing.Point(26, 368);
			this.lblDetail.Name = "lblDetail";
			this.lblDetail.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblDetail.Size = new System.Drawing.Size(1048, 23);
			this.lblDetail.TabIndex = 345;
			this.lblDetail.Text = "選択中の会員の取込内容";
			// 
			// gridHead
			// 
			this.gridHead.CaptionHeight = 16;
			this.gridHead.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
			this.gridHead.Images.Add(((System.Drawing.Image)(resources.GetObject("gridHead.Images"))));
			this.gridHead.Location = new System.Drawing.Point(26, 88);
			this.gridHead.Name = "gridHead";
			this.gridHead.PreviewInfo.Caption = "印刷プレビューウィンドウ";
			this.gridHead.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.gridHead.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.gridHead.PreviewInfo.ZoomFactor = 75D;
			this.gridHead.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
			this.gridHead.PrintInfo.MeasurementPrinterName = null;
			this.gridHead.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("gridHead.PrintInfo.PageSettings")));
			this.gridHead.PropBag = resources.GetString("gridHead.PropBag");
			this.gridHead.RowHeight = 14;
			this.gridHead.Size = new System.Drawing.Size(1048, 272);
			this.gridHead.TabIndex = 1;
			this.gridHead.UseCompatibleTextRendering = false;
			// 
			// gridDetail
			// 
			this.gridDetail.CaptionHeight = 16;
			this.gridDetail.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
			this.gridDetail.Images.Add(((System.Drawing.Image)(resources.GetObject("gridDetail.Images"))));
			this.gridDetail.Location = new System.Drawing.Point(26, 394);
			this.gridDetail.Name = "gridDetail";
			this.gridDetail.PreviewInfo.Caption = "印刷プレビューウィンドウ";
			this.gridDetail.PreviewInfo.Location = new System.Drawing.Point(0, 0);
			this.gridDetail.PreviewInfo.Size = new System.Drawing.Size(0, 0);
			this.gridDetail.PreviewInfo.ZoomFactor = 75D;
			this.gridDetail.PrintInfo.MeasurementDevice = C1.Win.C1TrueDBGrid.PrintInfo.MeasurementDeviceEnum.Screen;
			this.gridDetail.PrintInfo.MeasurementPrinterName = null;
			this.gridDetail.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("gridDetail.PrintInfo.PageSettings")));
			this.gridDetail.PropBag = resources.GetString("gridDetail.PropBag");
			this.gridDetail.RowHeight = 14;
			this.gridDetail.Size = new System.Drawing.Size(1048, 220);
			this.gridDetail.TabIndex = 2;
			this.gridDetail.UseCompatibleTextRendering = false;
			// 
			// FormMamisImport_DlgConfirm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1100, 691);
			this.Controls.Add(this.gridDetail);
			this.Controls.Add(this.lblDetail);
			this.Controls.Add(this.gridHead);
			this.Controls.Add(this.lblSummary);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.funckey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMamisImport_DlgConfirm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MAMISデータ取込 - 取込確認";
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.lblSummary, 0);
			this.Controls.SetChildIndex(this.gridHead, 0);
			this.Controls.SetChildIndex(this.lblDetail, 0);
			this.Controls.SetChildIndex(this.gridDetail, 0);
			((System.ComponentModel.ISupportInitialize)(this.gridHead)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridDetail)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private App.Controls.FunctionKeyBar funckey;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
		private YControlLabelEx.YcLabelEx lblSummary;
		private YControlLabelEx.YcLabelEx lblDetail;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid gridHead;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid gridDetail;
	}
}
