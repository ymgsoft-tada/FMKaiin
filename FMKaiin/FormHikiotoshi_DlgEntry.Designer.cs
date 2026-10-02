
namespace App
{
	partial class FormHikiotoshi_DlgEntry
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHikiotoshi_DlgEntry));
			this.grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.iKaiin = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx6 = new YControlLabelEx.YcLabelEx();
			this.iDateYM = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			this.iTotalCost = new GControlGcNumberEx.GcNumberEx(this.components);
			this.iMemo = new GControlGcTextBoxEx.GcTextBoxEx();
			this.iCost = new GControlGcNumberEx.GcNumberEx(this.components);
			this.lblCreateDate = new YControlLabelEx.YcLabelEx();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iKaiin)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iDateYM)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iTotalCost)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iMemo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iCost)).BeginInit();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.CaptionHeight = 16;
			this.grid.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
			this.grid.Images.Add(((System.Drawing.Image)(resources.GetObject("grid.Images"))));
			this.grid.Location = new System.Drawing.Point(12, 86);
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
			this.grid.TabIndex = 1;
			this.grid.UseCompatibleTextRendering = false;
			// 
			// funckey
			// 
			this.funckey.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.funckey.Location = new System.Drawing.Point(0, 344);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(636, 25);
			this.funckey.TabIndex = 52;
			this.funckey.Text = "gcFunctionKey1";
			// 
			// iKaiin
			// 
			this.iKaiin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iKaiin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iKaiin.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.iKaiin.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iKaiin.DisabledForeColor = System.Drawing.Color.Black;
			this.iKaiin.Enabled = false;
			this.iKaiin.ExFocusHighlight = true;
			this.iKaiin.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.iKaiin.ImeStr = "";
			this.iKaiin.Location = new System.Drawing.Point(99, 55);
			this.iKaiin.Name = "iKaiin";
			this.iKaiin.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iKaiin.Size = new System.Drawing.Size(270, 25);
			this.iKaiin.TabIndex = 346;
			// 
			// ycLabelEx6
			// 
			this.ycLabelEx6.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx6.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx6.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx6.Font = new System.Drawing.Font("メイリオ", 9.75F);
			this.ycLabelEx6.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx6.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx6.IconImage = null;
			this.ycLabelEx6.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx6.Location = new System.Drawing.Point(13, 55);
			this.ycLabelEx6.Name = "ycLabelEx6";
			this.ycLabelEx6.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx6.Size = new System.Drawing.Size(81, 25);
			this.ycLabelEx6.TabIndex = 345;
			this.ycLabelEx6.Text = "会　員";
			this.ycLabelEx6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iDateYM
			// 
			this.iDateYM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iDateYM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iDateYM.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.iDateYM.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iDateYM.DisabledForeColor = System.Drawing.Color.Black;
			this.iDateYM.Enabled = false;
			this.iDateYM.ExFocusHighlight = true;
			this.iDateYM.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.iDateYM.ImeStr = "";
			this.iDateYM.Location = new System.Drawing.Point(99, 24);
			this.iDateYM.Name = "iDateYM";
			this.iDateYM.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iDateYM.Size = new System.Drawing.Size(110, 25);
			this.iDateYM.TabIndex = 344;
			// 
			// ycLabelEx2
			// 
			this.ycLabelEx2.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx2.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx2.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx2.Font = new System.Drawing.Font("メイリオ", 9.75F);
			this.ycLabelEx2.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx2.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.IconImage = null;
			this.ycLabelEx2.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx2.Location = new System.Drawing.Point(14, 24);
			this.ycLabelEx2.Name = "ycLabelEx2";
			this.ycLabelEx2.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.Size = new System.Drawing.Size(80, 25);
			this.ycLabelEx2.TabIndex = 343;
			this.ycLabelEx2.Text = "処理月度";
			this.ycLabelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ycLabelEx1
			// 
			this.ycLabelEx1.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx1.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx1.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx1.Font = new System.Drawing.Font("メイリオ", 9.75F);
			this.ycLabelEx1.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx1.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.IconImage = null;
			this.ycLabelEx1.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx1.Location = new System.Drawing.Point(107, 306);
			this.ycLabelEx1.Name = "ycLabelEx1";
			this.ycLabelEx1.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.Size = new System.Drawing.Size(89, 23);
			this.ycLabelEx1.TabIndex = 348;
			this.ycLabelEx1.Text = "引落金額";
			this.ycLabelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iTotalCost
			// 
			this.iTotalCost.AlternateText.DisplayZero.Text = "0";
			this.iTotalCost.AlternateText.Zero.Text = "0";
			this.iTotalCost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iTotalCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iTotalCost.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iTotalCost.DisabledForeColor = System.Drawing.Color.Black;
			this.iTotalCost.Enabled = false;
			this.iTotalCost.ExAlternateBlank = true;
			this.iTotalCost.ExCommaDelimiter = ",";
			this.iTotalCost.ExFocusHighlight = true;
			this.iTotalCost.ExMaxValue = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
			this.iTotalCost.ExMinValue = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
			this.iTotalCost.ExPrefix = null;
			this.iTotalCost.ExSuffix = null;
			this.iTotalCost.Location = new System.Drawing.Point(200, 306);
			this.iTotalCost.Name = "iTotalCost";
			this.iTotalCost.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iTotalCost.Size = new System.Drawing.Size(150, 23);
			this.iTotalCost.Spin.SpinOnKeys = false;
			this.iTotalCost.TabIndex = 347;
			// 
			// iMemo
			// 
			this.iMemo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.iMemo.ExFocusHighlight = true;
			this.iMemo.ImeStr = "";
			this.iMemo.Location = new System.Drawing.Point(283, 173);
			this.iMemo.Name = "iMemo";
			this.iMemo.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iMemo.Size = new System.Drawing.Size(71, 23);
			this.iMemo.TabIndex = 349;
			// 
			// iCost
			// 
			this.iCost.AlternateText.DisplayZero.Text = "0";
			this.iCost.AlternateText.Zero.Text = "0";
			this.iCost.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.iCost.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iCost.DisabledForeColor = System.Drawing.Color.Black;
			this.iCost.ExAlternateBlank = true;
			this.iCost.ExCommaDelimiter = ",";
			this.iCost.ExFocusHighlight = true;
			this.iCost.ExMaxValue = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
			this.iCost.ExMinValue = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
			this.iCost.ExPrefix = null;
			this.iCost.ExSuffix = null;
			this.iCost.Location = new System.Drawing.Point(74, 134);
			this.iCost.Name = "iCost";
			this.iCost.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iCost.Size = new System.Drawing.Size(120, 23);
			this.iCost.Spin.SpinOnKeys = false;
			this.iCost.TabIndex = 350;
			// 
			// lblCreateDate
			// 
			this.lblCreateDate.BackColor2 = System.Drawing.Color.Empty;
			this.lblCreateDate.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblCreateDate.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblCreateDate.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lblCreateDate.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblCreateDate.IconImage = null;
			this.lblCreateDate.Location = new System.Drawing.Point(442, 58);
			this.lblCreateDate.Name = "lblCreateDate";
			this.lblCreateDate.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblCreateDate.Size = new System.Drawing.Size(182, 25);
			this.lblCreateDate.TabIndex = 351;
			this.lblCreateDate.Text = "金額と備考を編集できます。";
			this.lblCreateDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormHikiotoshi_DlgEntry
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(636, 369);
			this.Controls.Add(this.lblCreateDate);
			this.Controls.Add(this.iCost);
			this.Controls.Add(this.iMemo);
			this.Controls.Add(this.ycLabelEx1);
			this.Controls.Add(this.iTotalCost);
			this.Controls.Add(this.iKaiin);
			this.Controls.Add(this.ycLabelEx6);
			this.Controls.Add(this.iDateYM);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.funckey);
			this.Controls.Add(this.grid);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "FormHikiotoshi_DlgEntry";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "引落データ編集";
			this.Controls.SetChildIndex(this.grid, 0);
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.iDateYM, 0);
			this.Controls.SetChildIndex(this.ycLabelEx6, 0);
			this.Controls.SetChildIndex(this.iKaiin, 0);
			this.Controls.SetChildIndex(this.iTotalCost, 0);
			this.Controls.SetChildIndex(this.ycLabelEx1, 0);
			this.Controls.SetChildIndex(this.iMemo, 0);
			this.Controls.SetChildIndex(this.iCost, 0);
			this.Controls.SetChildIndex(this.lblCreateDate, 0);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iKaiin)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iDateYM)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iTotalCost)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iMemo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iCost)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
		private GrapeCity.Win.Bars.GcFunctionKey funckey;
		private GControlGcTextBoxEx.GcTextBoxEx iKaiin;
		private YControlLabelEx.YcLabelEx ycLabelEx6;
		private GControlGcTextBoxEx.GcTextBoxEx iDateYM;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private GControlGcNumberEx.GcNumberEx iTotalCost;
		private GControlGcTextBoxEx.GcTextBoxEx iMemo;
		private GControlGcNumberEx.GcNumberEx iCost;
		private YControlLabelEx.YcLabelEx lblCreateDate;
	}
}