
namespace App
{
	partial class FormSelectorBankCode
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectorBankCode));
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			this.grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.iBank = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.iShiten = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx3 = new YControlLabelEx.YcLabelEx();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iBank)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iShiten)).BeginInit();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.BackColor2 = System.Drawing.Color.Empty;
			this.lblTitle.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblTitle.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblTitle.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblTitle.IconImage = null;
			this.lblTitle.Location = new System.Drawing.Point(35, 29);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblTitle.Size = new System.Drawing.Size(115, 23);
			this.lblTitle.TabIndex = 47;
			this.lblTitle.Text = "銀行コード選択";
			// 
			// ycLabelEx1
			// 
			this.ycLabelEx1.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx1.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx1.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx1.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.IconImage = null;
			this.ycLabelEx1.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx1.Location = new System.Drawing.Point(19, 25);
			this.ycLabelEx1.Name = "ycLabelEx1";
			this.ycLabelEx1.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.Size = new System.Drawing.Size(10, 30);
			this.ycLabelEx1.TabIndex = 46;
			// 
			// grid
			// 
			this.grid.CaptionHeight = 16;
			this.grid.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
			this.grid.Images.Add(((System.Drawing.Image)(resources.GetObject("grid.Images"))));
			this.grid.Location = new System.Drawing.Point(23, 106);
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
			this.grid.Size = new System.Drawing.Size(456, 360);
			this.grid.TabIndex = 48;
			this.grid.UseCompatibleTextRendering = false;
			// 
			// funckey
			// 
			this.funckey.Location = new System.Drawing.Point(0, 493);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(507, 25);
			this.funckey.TabIndex = 49;
			this.funckey.Text = "gcFunctionKey1";
			// 
			// iBank
			// 
			this.iBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iBank.ExFocusHighlight = true;
			this.iBank.ImeStr = "";
			this.iBank.Location = new System.Drawing.Point(96, 77);
			this.iBank.Name = "iBank";
			this.iBank.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iBank.Size = new System.Drawing.Size(148, 23);
			this.iBank.TabIndex = 64;
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
			this.ycLabelEx2.Location = new System.Drawing.Point(22, 77);
			this.ycLabelEx2.Name = "ycLabelEx2";
			this.ycLabelEx2.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.Size = new System.Drawing.Size(71, 23);
			this.ycLabelEx2.TabIndex = 65;
			this.ycLabelEx2.Text = "銀行検索";
			this.ycLabelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iShiten
			// 
			this.iShiten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iShiten.ExFocusHighlight = true;
			this.iShiten.ImeStr = "";
			this.iShiten.Location = new System.Drawing.Point(316, 77);
			this.iShiten.Name = "iShiten";
			this.iShiten.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iShiten.Size = new System.Drawing.Size(147, 23);
			this.iShiten.TabIndex = 66;
			// 
			// ycLabelEx3
			// 
			this.ycLabelEx3.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx3.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx3.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx3.Font = new System.Drawing.Font("メイリオ", 9.75F);
			this.ycLabelEx3.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx3.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.IconImage = null;
			this.ycLabelEx3.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx3.Location = new System.Drawing.Point(245, 77);
			this.ycLabelEx3.Name = "ycLabelEx3";
			this.ycLabelEx3.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.Size = new System.Drawing.Size(69, 23);
			this.ycLabelEx3.TabIndex = 67;
			this.ycLabelEx3.Text = "支店検索";
			this.ycLabelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FormSelectorBankCode
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(507, 518);
			this.Controls.Add(this.iShiten);
			this.Controls.Add(this.ycLabelEx3);
			this.Controls.Add(this.iBank);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.funckey);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSelectorBankCode";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "銀行コード選択";
			this.Controls.SetChildIndex(this.ycLabelEx1, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.grid, 0);
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.iBank, 0);
			this.Controls.SetChildIndex(this.ycLabelEx3, 0);
			this.Controls.SetChildIndex(this.iShiten, 0);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iBank)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iShiten)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
		private GrapeCity.Win.Bars.GcFunctionKey funckey;
		private GControlGcTextBoxEx.GcTextBoxEx iBank;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
		private GControlGcTextBoxEx.GcTextBoxEx iShiten;
		private YControlLabelEx.YcLabelEx ycLabelEx3;
	}
}