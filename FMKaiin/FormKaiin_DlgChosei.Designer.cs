
namespace App
{
	partial class FormKaiin_DlgChosei
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKaiin_DlgChosei));
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			this.lblCreateDate = new YControlLabelEx.YcLabelEx();
			this.iKaiin = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx6 = new YControlLabelEx.YcLabelEx();
			this.grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.iKaihi = new App.UcTableComboBox();
			this.iCost = new GControlGcNumberEx.GcNumberEx(this.components);
			this.iMemo = new GControlGcTextBoxEx.GcTextBoxEx();
			this.iDateYM = new App.UcDate();
			this.iShiharai = new GControlGcComboBoxEx.GcComboBoxEx(this.components);
			((System.ComponentModel.ISupportInitialize)(this.iKaiin)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iCost)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iMemo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iShiharai)).BeginInit();
			this.SuspendLayout();
			// 
			// funckey
			// 
			this.funckey.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.funckey.Location = new System.Drawing.Point(0, 426);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(848, 25);
			this.funckey.TabIndex = 56;
			this.funckey.Text = "gcFunctionKey1";
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.BackColor2 = System.Drawing.Color.Empty;
			this.lblTitle.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblTitle.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblTitle.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblTitle.IconImage = null;
			this.lblTitle.Location = new System.Drawing.Point(54, 26);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblTitle.Size = new System.Drawing.Size(115, 23);
			this.lblTitle.TabIndex = 79;
			this.lblTitle.Text = "会費予定額調整";
			// 
			// ycLabelEx1
			// 
			this.ycLabelEx1.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx1.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx1.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx1.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.IconImage = null;
			this.ycLabelEx1.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx1.Location = new System.Drawing.Point(34, 22);
			this.ycLabelEx1.Name = "ycLabelEx1";
			this.ycLabelEx1.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.Size = new System.Drawing.Size(10, 29);
			this.ycLabelEx1.TabIndex = 78;
			// 
			// lblCreateDate
			// 
			this.lblCreateDate.BackColor2 = System.Drawing.Color.Empty;
			this.lblCreateDate.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblCreateDate.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblCreateDate.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lblCreateDate.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblCreateDate.IconImage = null;
			this.lblCreateDate.Location = new System.Drawing.Point(626, 70);
			this.lblCreateDate.Name = "lblCreateDate";
			this.lblCreateDate.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblCreateDate.Size = new System.Drawing.Size(182, 25);
			this.lblCreateDate.TabIndex = 354;
			this.lblCreateDate.Text = "グリッドで直接編集できます。";
			this.lblCreateDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.iKaiin.Location = new System.Drawing.Point(119, 70);
			this.iKaiin.Name = "iKaiin";
			this.iKaiin.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iKaiin.Size = new System.Drawing.Size(270, 25);
			this.iKaiin.TabIndex = 353;
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
			this.ycLabelEx6.Location = new System.Drawing.Point(33, 70);
			this.ycLabelEx6.Name = "ycLabelEx6";
			this.ycLabelEx6.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx6.Size = new System.Drawing.Size(81, 25);
			this.ycLabelEx6.TabIndex = 352;
			this.ycLabelEx6.Text = "会　員";
			this.ycLabelEx6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// grid
			// 
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grid.CaptionHeight = 16;
			this.grid.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
			this.grid.Images.Add(((System.Drawing.Image)(resources.GetObject("grid.Images"))));
			this.grid.Location = new System.Drawing.Point(36, 103);
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
			this.grid.Size = new System.Drawing.Size(788, 306);
			this.grid.TabIndex = 355;
			this.grid.UseCompatibleTextRendering = false;
			// 
			// iKaihi
			// 
			this.iKaihi.BackColor = System.Drawing.Color.Transparent;
			this.iKaihi.ComboBorderStyle = System.Windows.Forms.BorderStyle.None;
			this.iKaihi.CompareValue = null;
			this.iKaihi.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.iKaihi.DBView = null;
			this.iKaihi.DiabledBackColor = System.Drawing.SystemColors.Control;
			this.iKaihi.DisabledForeColor = System.Drawing.SystemColors.GrayText;
			this.iKaihi.DropDownSize = new System.Drawing.Size(100, 44);
			this.iKaihi.EnterImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.iKaihi.ForeColor = System.Drawing.Color.Transparent;
			this.iKaihi.HighlightText = true;
			this.iKaihi.Location = new System.Drawing.Point(245, 257);
			this.iKaihi.Name = "iKaihi";
			this.iKaihi.RowFilter = "";
			this.iKaihi.SelectedIndexNullLeave = -1;
			this.iKaihi.Size = new System.Drawing.Size(120, 20);
			this.iKaihi.Sort = "";
			this.iKaihi.TabIndex = 358;
			this.iKaihi.TextSubItemIndex = -1;
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
			this.iCost.Location = new System.Drawing.Point(230, 174);
			this.iCost.Name = "iCost";
			this.iCost.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iCost.Size = new System.Drawing.Size(120, 23);
			this.iCost.Spin.SpinOnKeys = false;
			this.iCost.TabIndex = 357;
			// 
			// iMemo
			// 
			this.iMemo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.iMemo.ExFocusHighlight = true;
			this.iMemo.ImeStr = "";
			this.iMemo.Location = new System.Drawing.Point(439, 213);
			this.iMemo.Name = "iMemo";
			this.iMemo.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iMemo.Size = new System.Drawing.Size(71, 23);
			this.iMemo.TabIndex = 356;
			// 
			// iDateYM
			// 
			this.iDateYM.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(251)))));
			this.iDateYM.DisabledForeColor = System.Drawing.Color.Black;
			this.iDateYM.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.iDateYM.Location = new System.Drawing.Point(100, 139);
			this.iDateYM.Name = "iDateYM";
			this.iDateYM.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iDateYM.Size = new System.Drawing.Size(120, 25);
			this.iDateYM.TabIndex = 359;
			this.iDateYM.UcBackColor = System.Drawing.SystemColors.Window;
			this.iDateYM.UcBorderStyle = System.Windows.Forms.BorderStyle.None;
			this.iDateYM.UcCalenderMaxDate = new System.DateTime(2100, 12, 31, 23, 59, 59, 0);
			this.iDateYM.UcCalenderMinDate = new System.DateTime(1868, 1, 1, 0, 0, 0, 0);
			this.iDateYM.UcEnabled = true;
			this.iDateYM.UcFormat = App.UcDate.UcDateFormat.YYYYMM;
			this.iDateYM.UcReadOnly = false;
			this.iDateYM.UcSideButton = true;
			this.iDateYM.UcValue = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
			// 
			// iShiharai
			// 
			this.iShiharai.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.iShiharai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.iShiharai.ExCompareContent = null;
			this.iShiharai.ExCompareValue = null;
			this.iShiharai.ExDataSource = null;
			this.iShiharai.ExFocusHighlight = true;
			this.iShiharai.FlatStyle = GrapeCity.Win.Editors.FlatStyleEx.Flat;
			this.iShiharai.ListHeaderPane.Height = 27;
			this.iShiharai.ListHeaderPane.Visible = false;
			this.iShiharai.Location = new System.Drawing.Point(349, 213);
			this.iShiharai.Name = "iShiharai";
			this.iShiharai.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iShiharai.Size = new System.Drawing.Size(42, 25);
			this.iShiharai.TabIndex = 452;
			// 
			// FormKaiin_DlgChosei
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(848, 451);
			this.Controls.Add(this.iShiharai);
			this.Controls.Add(this.iDateYM);
			this.Controls.Add(this.iKaihi);
			this.Controls.Add(this.iCost);
			this.Controls.Add(this.iMemo);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.lblCreateDate);
			this.Controls.Add(this.iKaiin);
			this.Controls.Add(this.ycLabelEx6);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx1);
			this.Controls.Add(this.funckey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "FormKaiin_DlgChosei";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "会費調整";
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx1, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.ycLabelEx6, 0);
			this.Controls.SetChildIndex(this.iKaiin, 0);
			this.Controls.SetChildIndex(this.lblCreateDate, 0);
			this.Controls.SetChildIndex(this.grid, 0);
			this.Controls.SetChildIndex(this.iMemo, 0);
			this.Controls.SetChildIndex(this.iCost, 0);
			this.Controls.SetChildIndex(this.iKaihi, 0);
			this.Controls.SetChildIndex(this.iDateYM, 0);
			this.Controls.SetChildIndex(this.iShiharai, 0);
			((System.ComponentModel.ISupportInitialize)(this.iKaiin)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iCost)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iMemo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iShiharai)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private GrapeCity.Win.Bars.GcFunctionKey funckey;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private YControlLabelEx.YcLabelEx lblCreateDate;
		private GControlGcTextBoxEx.GcTextBoxEx iKaiin;
		private YControlLabelEx.YcLabelEx ycLabelEx6;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
		private UcTableComboBox iKaihi;
		private GControlGcNumberEx.GcNumberEx iCost;
		private GControlGcTextBoxEx.GcTextBoxEx iMemo;
		private UcDate iDateYM;
		private GControlGcComboBoxEx.GcComboBoxEx iShiharai;
	}
}