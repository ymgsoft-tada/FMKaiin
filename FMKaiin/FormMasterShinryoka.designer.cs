namespace App
{
	partial class FormMasterShinryoka
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMasterShinryoka));
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.btnClear = new System.Windows.Forms.Button();
			this.iCodeShinryoka = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx4 = new YControlLabelEx.YcLabelEx();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iCodeShinryoka)).BeginInit();
			this.SuspendLayout();
			// 
			// funckey
			// 
			this.funckey.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.funckey.Location = new System.Drawing.Point(0, 515);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(808, 25);
			this.funckey.TabIndex = 33;
			this.funckey.Text = "gcFunctionKey1";
			// 
			// grid
			// 
			this.grid.CaptionHeight = 16;
			this.grid.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
			this.grid.Images.Add(((System.Drawing.Image)(resources.GetObject("grid.Images"))));
			this.grid.Location = new System.Drawing.Point(26, 107);
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
			this.grid.Size = new System.Drawing.Size(750, 388);
			this.grid.TabIndex = 3;
			this.grid.UseCompatibleTextRendering = false;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(226, 77);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(99, 28);
			this.btnClear.TabIndex = 2;
			this.btnClear.Text = "検索クリア";
			this.btnClear.UseVisualStyleBackColor = true;
			// 
			// iCodeShinryoka
			// 
			this.iCodeShinryoka.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iCodeShinryoka.ExFocusHighlight = true;
			this.iCodeShinryoka.ImeStr = "";
			this.iCodeShinryoka.Location = new System.Drawing.Point(104, 79);
			this.iCodeShinryoka.Name = "iCodeShinryoka";
			this.iCodeShinryoka.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iCodeShinryoka.Size = new System.Drawing.Size(88, 25);
			this.iCodeShinryoka.TabIndex = 1;
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
			this.ycLabelEx4.Location = new System.Drawing.Point(26, 79);
			this.ycLabelEx4.Name = "ycLabelEx4";
			this.ycLabelEx4.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.Size = new System.Drawing.Size(72, 25);
			this.ycLabelEx4.TabIndex = 66;
			this.ycLabelEx4.Text = "科コード";
			this.ycLabelEx4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			this.lblTitle.Size = new System.Drawing.Size(107, 28);
			this.lblTitle.TabIndex = 37;
			this.lblTitle.Text = "診療科一覧";
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
			// FormMasterShinryoka
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(808, 540);
			this.Controls.Add(this.iCodeShinryoka);
			this.Controls.Add(this.ycLabelEx4);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx1);
			this.Controls.Add(this.funckey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMasterShinryoka";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "診療科マスタ";
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx1, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.grid, 0);
			this.Controls.SetChildIndex(this.btnClear, 0);
			this.Controls.SetChildIndex(this.ycLabelEx4, 0);
			this.Controls.SetChildIndex(this.iCodeShinryoka, 0);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iCodeShinryoka)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private GrapeCity.Win.Bars.GcFunctionKey funckey;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
		private System.Windows.Forms.Button btnClear;
		private YControlLabelEx.YcLabelEx ycLabelEx4;
		private GControlGcTextBoxEx.GcTextBoxEx iCodeShinryoka;
	}
}