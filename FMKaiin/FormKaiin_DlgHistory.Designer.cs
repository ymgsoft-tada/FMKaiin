
namespace App
{
	partial class FormKaiin_DlgHistory
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKaiin_DlgHistory));
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.iKaiin = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx25 = new YControlLabelEx.YcLabelEx();
			this.iTotal = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			this.grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.iIsekiNo = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			((System.ComponentModel.ISupportInitialize)(this.iKaiin)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iTotal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iIsekiNo)).BeginInit();
			this.SuspendLayout();
			// 
			// funckey
			// 
			this.funckey.Location = new System.Drawing.Point(0, 278);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(597, 25);
			this.funckey.TabIndex = 35;
			this.funckey.Text = "gcFunctionKey1";
			// 
			// iKaiin
			// 
			this.iKaiin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iKaiin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iKaiin.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iKaiin.DisabledForeColor = System.Drawing.Color.Black;
			this.iKaiin.ExFocusHighlight = true;
			this.iKaiin.ImeStr = "";
			this.iKaiin.Location = new System.Drawing.Point(139, 32);
			this.iKaiin.Name = "iKaiin";
			this.iKaiin.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iKaiin.Size = new System.Drawing.Size(263, 23);
			this.iKaiin.TabIndex = 401;
			// 
			// ycLabelEx25
			// 
			this.ycLabelEx25.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx25.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx25.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx25.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx25.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx25.IconImage = null;
			this.ycLabelEx25.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx25.Location = new System.Drawing.Point(27, 32);
			this.ycLabelEx25.Name = "ycLabelEx25";
			this.ycLabelEx25.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx25.Size = new System.Drawing.Size(106, 23);
			this.ycLabelEx25.TabIndex = 402;
			this.ycLabelEx25.Text = "会員名";
			this.ycLabelEx25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iTotal
			// 
			this.iTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iTotal.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iTotal.DisabledForeColor = System.Drawing.Color.Black;
			this.iTotal.ExFocusHighlight = true;
			this.iTotal.ImeStr = "";
			this.iTotal.Location = new System.Drawing.Point(139, 90);
			this.iTotal.Name = "iTotal";
			this.iTotal.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iTotal.Size = new System.Drawing.Size(90, 23);
			this.iTotal.TabIndex = 403;
			// 
			// ycLabelEx1
			// 
			this.ycLabelEx1.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx1.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx1.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx1.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx1.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.IconImage = null;
			this.ycLabelEx1.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx1.Location = new System.Drawing.Point(27, 90);
			this.ycLabelEx1.Name = "ycLabelEx1";
			this.ycLabelEx1.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.Size = new System.Drawing.Size(106, 23);
			this.ycLabelEx1.TabIndex = 404;
			this.ycLabelEx1.Text = "在籍期間";
			this.ycLabelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// grid
			// 
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grid.CaptionHeight = 16;
			this.grid.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
			this.grid.Images.Add(((System.Drawing.Image)(resources.GetObject("grid.Images"))));
			this.grid.Location = new System.Drawing.Point(26, 121);
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
			this.grid.Size = new System.Drawing.Size(545, 139);
			this.grid.TabIndex = 405;
			this.grid.UseCompatibleTextRendering = false;
			// 
			// iIsekiNo
			// 
			this.iIsekiNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iIsekiNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iIsekiNo.ContentAlignment = System.Drawing.ContentAlignment.TopRight;
			this.iIsekiNo.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iIsekiNo.DisabledForeColor = System.Drawing.Color.Black;
			this.iIsekiNo.ExFocusHighlight = true;
			this.iIsekiNo.ImeStr = "";
			this.iIsekiNo.Location = new System.Drawing.Point(139, 61);
			this.iIsekiNo.Name = "iIsekiNo";
			this.iIsekiNo.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iIsekiNo.Size = new System.Drawing.Size(90, 23);
			this.iIsekiNo.TabIndex = 406;
			// 
			// ycLabelEx2
			// 
			this.ycLabelEx2.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx2.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx2.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx2.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx2.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.IconImage = null;
			this.ycLabelEx2.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx2.Location = new System.Drawing.Point(27, 61);
			this.ycLabelEx2.Name = "ycLabelEx2";
			this.ycLabelEx2.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.Size = new System.Drawing.Size(106, 23);
			this.ycLabelEx2.TabIndex = 407;
			this.ycLabelEx2.Text = "医籍登録番号";
			this.ycLabelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FormKaiin_DlgHistory
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(597, 303);
			this.Controls.Add(this.iIsekiNo);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.iTotal);
			this.Controls.Add(this.ycLabelEx1);
			this.Controls.Add(this.iKaiin);
			this.Controls.Add(this.ycLabelEx25);
			this.Controls.Add(this.funckey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "FormKaiin_DlgHistory";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "在籍期間表示";
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx25, 0);
			this.Controls.SetChildIndex(this.iKaiin, 0);
			this.Controls.SetChildIndex(this.ycLabelEx1, 0);
			this.Controls.SetChildIndex(this.iTotal, 0);
			this.Controls.SetChildIndex(this.grid, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.iIsekiNo, 0);
			((System.ComponentModel.ISupportInitialize)(this.iKaiin)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iTotal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iIsekiNo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private GrapeCity.Win.Bars.GcFunctionKey funckey;
		private GControlGcTextBoxEx.GcTextBoxEx iKaiin;
		private YControlLabelEx.YcLabelEx ycLabelEx25;
		private GControlGcTextBoxEx.GcTextBoxEx iTotal;
		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
		private GControlGcTextBoxEx.GcTextBoxEx iIsekiNo;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
	}
}