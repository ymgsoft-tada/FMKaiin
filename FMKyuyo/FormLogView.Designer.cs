namespace App
{
	partial class FormLogView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogView));
			this.grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.ycLabelEx5 = new YControlLabelEx.YcLabelEx();
			this.btnLog = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnExec = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.CaptionHeight = 16;
			this.grid.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
			this.grid.Images.Add(((System.Drawing.Image)(resources.GetObject("grid.Images"))));
			this.grid.Location = new System.Drawing.Point(12, 57);
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
			this.grid.Size = new System.Drawing.Size(849, 392);
			this.grid.TabIndex = 568;
			this.grid.UseCompatibleTextRendering = false;
			// 
			// lblTitle
			// 
			this.lblTitle.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblTitle.ForeColor = System.Drawing.Color.Black;
			this.lblTitle.Location = new System.Drawing.Point(28, 21);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(603, 21);
			this.lblTitle.TabIndex = 570;
			this.lblTitle.Text = "結果";
			// 
			// btnClose
			// 
			this.btnClose.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.btnClose.Location = new System.Drawing.Point(375, 470);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(113, 34);
			this.btnClose.TabIndex = 571;
			this.btnClose.Text = "閉じる";
			this.btnClose.UseVisualStyleBackColor = true;
			// 
			// ycLabelEx5
			// 
			this.ycLabelEx5.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx5.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx5.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx5.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx5.IconImage = null;
			this.ycLabelEx5.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx5.Location = new System.Drawing.Point(12, 18);
			this.ycLabelEx5.Name = "ycLabelEx5";
			this.ycLabelEx5.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx5.Size = new System.Drawing.Size(10, 26);
			this.ycLabelEx5.TabIndex = 572;
			// 
			// btnLog
			// 
			this.btnLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLog.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.btnLog.Location = new System.Drawing.Point(770, 455);
			this.btnLog.Name = "btnLog";
			this.btnLog.Size = new System.Drawing.Size(91, 30);
			this.btnLog.TabIndex = 573;
			this.btnLog.TabStop = false;
			this.btnLog.Text = "ログ出力";
			this.btnLog.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.btnCancel.Location = new System.Drawing.Point(443, 470);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(113, 34);
			this.btnCancel.TabIndex = 574;
			this.btnCancel.Text = "キャンセル";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnExec
			// 
			this.btnExec.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.btnExec.Location = new System.Drawing.Point(315, 470);
			this.btnExec.Name = "btnExec";
			this.btnExec.Size = new System.Drawing.Size(113, 34);
			this.btnExec.TabIndex = 575;
			this.btnExec.Text = "実行";
			this.btnExec.UseVisualStyleBackColor = true;
			// 
			// FormLogView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(878, 528);
			this.Controls.Add(this.btnExec);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnLog);
			this.Controls.Add(this.ycLabelEx5);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.grid);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLogView";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "実行結果";
			this.Controls.SetChildIndex(this.grid, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.btnClose, 0);
			this.Controls.SetChildIndex(this.ycLabelEx5, 0);
			this.Controls.SetChildIndex(this.btnLog, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.btnExec, 0);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Button btnClose;
		private YControlLabelEx.YcLabelEx ycLabelEx5;
		private System.Windows.Forms.Button btnLog;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnExec;
	}
}