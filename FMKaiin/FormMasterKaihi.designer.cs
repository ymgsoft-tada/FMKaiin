namespace App
{
	partial class FormMasterKaihi
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMasterKaihi));
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnClear = new System.Windows.Forms.Button();
			this.iCodeKaihi = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx4 = new YControlLabelEx.YcLabelEx();
			this.iKbnKaihi = new GControlGcComboBoxEx.GcComboBoxEx(this.components);
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			this.iNameKaihi = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx5 = new YControlLabelEx.YcLabelEx();
			this.iKbnUsed = new GControlGcComboBoxEx.GcComboBoxEx(this.components);
			this.ycLabelEx6 = new YControlLabelEx.YcLabelEx();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iCodeKaihi)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iKbnKaihi)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iNameKaihi)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iKbnUsed)).BeginInit();
			this.SuspendLayout();
			// 
			// funckey
			// 
			this.funckey.Location = new System.Drawing.Point(0, 515);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(925, 25);
			this.funckey.TabIndex = 33;
			this.funckey.Text = "gcFunctionKey1";
			// 
			// grid
			// 
			this.grid.CaptionHeight = 16;
			this.grid.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
			this.grid.Images.Add(((System.Drawing.Image)(resources.GetObject("grid.Images"))));
			this.grid.Location = new System.Drawing.Point(26, 99);
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
			this.grid.Size = new System.Drawing.Size(887, 388);
			this.grid.TabIndex = 5;
			this.grid.UseCompatibleTextRendering = false;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(805, 63);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(99, 28);
			this.btnClear.TabIndex = 4;
			this.btnClear.Text = "検索クリア";
			this.btnClear.UseVisualStyleBackColor = true;
			// 
			// iCodeKaihi
			// 
			this.iCodeKaihi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iCodeKaihi.ExFocusHighlight = true;
			this.iCodeKaihi.ImeStr = "";
			this.iCodeKaihi.Location = new System.Drawing.Point(305, 64);
			this.iCodeKaihi.Name = "iCodeKaihi";
			this.iCodeKaihi.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iCodeKaihi.Size = new System.Drawing.Size(70, 25);
			this.iCodeKaihi.TabIndex = 1;
			// 
			// ycLabelEx4
			// 
			this.ycLabelEx4.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx4.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx4.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx4.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx4.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.IconImage = null;
			this.ycLabelEx4.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx4.Location = new System.Drawing.Point(234, 64);
			this.ycLabelEx4.Name = "ycLabelEx4";
			this.ycLabelEx4.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.Size = new System.Drawing.Size(65, 25);
			this.ycLabelEx4.TabIndex = 66;
			this.ycLabelEx4.Text = "コード";
			this.ycLabelEx4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iKbnKaihi
			// 
			this.iKbnKaihi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iKbnKaihi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.iKbnKaihi.ExCompareContent = null;
			this.iKbnKaihi.ExCompareValue = null;
			this.iKbnKaihi.ExDataSource = null;
			this.iKbnKaihi.ExFocusHighlight = true;
			this.iKbnKaihi.FlatStyle = GrapeCity.Win.Editors.FlatStyleEx.Flat;
			this.iKbnKaihi.ListHeaderPane.Height = 27;
			this.iKbnKaihi.ListHeaderPane.Visible = false;
			this.iKbnKaihi.Location = new System.Drawing.Point(96, 64);
			this.iKbnKaihi.Name = "iKbnKaihi";
			this.iKbnKaihi.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iKbnKaihi.Size = new System.Drawing.Size(115, 25);
			this.iKbnKaihi.TabIndex = 0;
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
			this.ycLabelEx2.Location = new System.Drawing.Point(26, 64);
			this.ycLabelEx2.Name = "ycLabelEx2";
			this.ycLabelEx2.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.Size = new System.Drawing.Size(67, 25);
			this.ycLabelEx2.TabIndex = 40;
			this.ycLabelEx2.Text = "区分";
			this.ycLabelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.BackColor2 = System.Drawing.Color.Empty;
			this.lblTitle.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblTitle.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblTitle.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblTitle.IconImage = null;
			this.lblTitle.Location = new System.Drawing.Point(46, 28);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblTitle.Size = new System.Drawing.Size(100, 23);
			this.lblTitle.TabIndex = 37;
			this.lblTitle.Text = "医会会費一覧";
			// 
			// ycLabelEx1
			// 
			this.ycLabelEx1.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx1.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx1.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx1.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.IconImage = null;
			this.ycLabelEx1.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx1.Location = new System.Drawing.Point(26, 24);
			this.ycLabelEx1.Name = "ycLabelEx1";
			this.ycLabelEx1.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.Size = new System.Drawing.Size(10, 29);
			this.ycLabelEx1.TabIndex = 36;
			// 
			// iNameKaihi
			// 
			this.iNameKaihi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iNameKaihi.ExFocusHighlight = true;
			this.iNameKaihi.ImeStr = "";
			this.iNameKaihi.Location = new System.Drawing.Point(452, 64);
			this.iNameKaihi.Name = "iNameKaihi";
			this.iNameKaihi.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iNameKaihi.Size = new System.Drawing.Size(153, 25);
			this.iNameKaihi.TabIndex = 2;
			// 
			// ycLabelEx5
			// 
			this.ycLabelEx5.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx5.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx5.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx5.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx5.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx5.IconImage = null;
			this.ycLabelEx5.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx5.Location = new System.Drawing.Point(381, 64);
			this.ycLabelEx5.Name = "ycLabelEx5";
			this.ycLabelEx5.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx5.Size = new System.Drawing.Size(68, 25);
			this.ycLabelEx5.TabIndex = 67;
			this.ycLabelEx5.Text = "名称";
			this.ycLabelEx5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iKbnUsed
			// 
			this.iKbnUsed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iKbnUsed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.iKbnUsed.ExCompareContent = null;
			this.iKbnUsed.ExCompareValue = null;
			this.iKbnUsed.ExDataSource = null;
			this.iKbnUsed.ExFocusHighlight = true;
			this.iKbnUsed.FlatStyle = GrapeCity.Win.Editors.FlatStyleEx.Flat;
			this.iKbnUsed.ListHeaderPane.Height = 27;
			this.iKbnUsed.ListHeaderPane.Visible = false;
			this.iKbnUsed.Location = new System.Drawing.Point(681, 64);
			this.iKbnUsed.Name = "iKbnUsed";
			this.iKbnUsed.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iKbnUsed.Size = new System.Drawing.Size(85, 25);
			this.iKbnUsed.TabIndex = 3;
			// 
			// ycLabelEx6
			// 
			this.ycLabelEx6.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx6.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx6.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx6.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx6.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx6.IconImage = null;
			this.ycLabelEx6.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx6.Location = new System.Drawing.Point(611, 64);
			this.ycLabelEx6.Name = "ycLabelEx6";
			this.ycLabelEx6.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx6.Size = new System.Drawing.Size(67, 25);
			this.ycLabelEx6.TabIndex = 68;
			this.ycLabelEx6.Text = "検索表示";
			this.ycLabelEx6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FormMasterKaihi
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(925, 540);
			this.Controls.Add(this.iKbnUsed);
			this.Controls.Add(this.ycLabelEx6);
			this.Controls.Add(this.iNameKaihi);
			this.Controls.Add(this.ycLabelEx5);
			this.Controls.Add(this.iCodeKaihi);
			this.Controls.Add(this.ycLabelEx4);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.iKbnKaihi);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx1);
			this.Controls.Add(this.funckey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMasterKaihi";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "医会会費マスタ";
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx1, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.grid, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.iKbnKaihi, 0);
			this.Controls.SetChildIndex(this.btnClear, 0);
			this.Controls.SetChildIndex(this.ycLabelEx4, 0);
			this.Controls.SetChildIndex(this.iCodeKaihi, 0);
			this.Controls.SetChildIndex(this.ycLabelEx5, 0);
			this.Controls.SetChildIndex(this.iNameKaihi, 0);
			this.Controls.SetChildIndex(this.ycLabelEx6, 0);
			this.Controls.SetChildIndex(this.iKbnUsed, 0);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iCodeKaihi)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iKbnKaihi)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iNameKaihi)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iKbnUsed)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private GrapeCity.Win.Bars.GcFunctionKey funckey;
		private GControlGcTextBoxEx.GcTextBoxEx iNameKaihi;
		private YControlLabelEx.YcLabelEx ycLabelEx5;
		private GControlGcComboBoxEx.GcComboBoxEx iKbnUsed;
		private YControlLabelEx.YcLabelEx ycLabelEx6;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
		private GControlGcComboBoxEx.GcComboBoxEx iKbnKaihi;
		private System.Windows.Forms.Button btnClear;
		private YControlLabelEx.YcLabelEx ycLabelEx4;
		private GControlGcTextBoxEx.GcTextBoxEx iCodeKaihi;
	}
}