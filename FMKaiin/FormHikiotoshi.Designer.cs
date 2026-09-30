
namespace App
{
	partial class FormHikiotoshi
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHikiotoshi));
			this.grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.iDateYM = new App.UcDate();
			this.ycLabelEx14 = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx3 = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx4 = new YControlLabelEx.YcLabelEx();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grid.CaptionHeight = 16;
			this.grid.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
			this.grid.Images.Add(((System.Drawing.Image)(resources.GetObject("grid.Images"))));
			this.grid.Location = new System.Drawing.Point(32, 95);
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
			this.grid.Size = new System.Drawing.Size(517, 449);
			this.grid.TabIndex = 56;
			this.grid.UseCompatibleTextRendering = false;
			// 
			// funckey
			// 
			this.funckey.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.funckey.Location = new System.Drawing.Point(0, 570);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(579, 25);
			this.funckey.TabIndex = 55;
			this.funckey.Text = "gcFunctionKey1";
			// 
			// iDateYM
			// 
			this.iDateYM.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(251)))));
			this.iDateYM.DisabledForeColor = System.Drawing.Color.Black;
			this.iDateYM.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.iDateYM.Location = new System.Drawing.Point(138, 64);
			this.iDateYM.Name = "iDateYM";
			this.iDateYM.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iDateYM.Size = new System.Drawing.Size(120, 25);
			this.iDateYM.TabIndex = 331;
			this.iDateYM.UcBackColor = System.Drawing.SystemColors.Window;
			this.iDateYM.UcBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iDateYM.UcCalenderMaxDate = new System.DateTime(2100, 12, 31, 23, 59, 59, 0);
			this.iDateYM.UcCalenderMinDate = new System.DateTime(1868, 1, 1, 0, 0, 0, 0);
			this.iDateYM.UcEnabled = true;
			this.iDateYM.UcFormat = App.UcDate.UcDateFormat.YYYYMM;
			this.iDateYM.UcReadOnly = false;
			this.iDateYM.UcSideButton = true;
			this.iDateYM.UcValue = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
			// 
			// ycLabelEx14
			// 
			this.ycLabelEx14.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx14.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx14.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx14.Font = new System.Drawing.Font("メイリオ", 9.75F);
			this.ycLabelEx14.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx14.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx14.IconImage = null;
			this.ycLabelEx14.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx14.Location = new System.Drawing.Point(32, 64);
			this.ycLabelEx14.Name = "ycLabelEx14";
			this.ycLabelEx14.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx14.Size = new System.Drawing.Size(100, 25);
			this.ycLabelEx14.TabIndex = 330;
			this.ycLabelEx14.Text = "処理月度";
			this.ycLabelEx14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ycLabelEx3
			// 
			this.ycLabelEx3.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx3.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx3.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ycLabelEx3.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.IconImage = null;
			this.ycLabelEx3.Location = new System.Drawing.Point(48, 23);
			this.ycLabelEx3.Name = "ycLabelEx3";
			this.ycLabelEx3.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.Size = new System.Drawing.Size(299, 23);
			this.ycLabelEx3.TabIndex = 335;
			this.ycLabelEx3.Text = "引落データ作成";
			// 
			// ycLabelEx4
			// 
			this.ycLabelEx4.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx4.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx4.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx4.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.IconImage = null;
			this.ycLabelEx4.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx4.Location = new System.Drawing.Point(32, 19);
			this.ycLabelEx4.Name = "ycLabelEx4";
			this.ycLabelEx4.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.Size = new System.Drawing.Size(10, 30);
			this.ycLabelEx4.TabIndex = 334;
			// 
			// FormHikiotoshi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(579, 595);
			this.Controls.Add(this.ycLabelEx3);
			this.Controls.Add(this.ycLabelEx4);
			this.Controls.Add(this.iDateYM);
			this.Controls.Add(this.ycLabelEx14);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.funckey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "FormHikiotoshi";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "引落データ作成";
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.grid, 0);
			this.Controls.SetChildIndex(this.ycLabelEx14, 0);
			this.Controls.SetChildIndex(this.iDateYM, 0);
			this.Controls.SetChildIndex(this.ycLabelEx4, 0);
			this.Controls.SetChildIndex(this.ycLabelEx3, 0);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
		private GrapeCity.Win.Bars.GcFunctionKey funckey;
		private UcDate iDateYM;
		private YControlLabelEx.YcLabelEx ycLabelEx14;
		private YControlLabelEx.YcLabelEx ycLabelEx3;
		private YControlLabelEx.YcLabelEx ycLabelEx4;
	}
}