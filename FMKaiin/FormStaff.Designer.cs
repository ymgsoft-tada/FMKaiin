
namespace App
{
	partial class FormStaff
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
			if (disposing && ( components != null ))
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStaff));
			this.ycLabelEx4 = new YControlLabelEx.YcLabelEx();
			this.chkUnUsed = new YControlYcCheckBoxEx.YcCheckBoxEx();
			this.ycLabelEx3 = new YControlLabelEx.YcLabelEx();
			this.iJob = new GControlGcComboBoxEx.GcComboBoxEx(this.components);
			this.iSearch = new GControlGcTextBoxEx.GcTextBoxEx();
			this.btnClear = new System.Windows.Forms.Button();
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			((System.ComponentModel.ISupportInitialize)(this.iJob)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iSearch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.SuspendLayout();
			// 
			// ycLabelEx4
			// 
			this.ycLabelEx4.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx4.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx4.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ycLabelEx4.ForeColor = System.Drawing.Color.DarkGray;
			this.ycLabelEx4.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.IconImage = null;
			this.ycLabelEx4.Location = new System.Drawing.Point(109, 48);
			this.ycLabelEx4.Name = "ycLabelEx4";
			this.ycLabelEx4.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.Size = new System.Drawing.Size(237, 14);
			this.ycLabelEx4.TabIndex = 80;
			this.ycLabelEx4.Text = "コード、氏名、フリガナで検索\r\n";
			// 
			// chkUnUsed
			// 
			this.chkUnUsed.AutoSize = true;
			this.chkUnUsed.DisabledBackColor = System.Drawing.SystemColors.Control;
			this.chkUnUsed.Location = new System.Drawing.Point(30, 508);
			this.chkUnUsed.Name = "chkUnUsed";
			this.chkUnUsed.Position = YControlYcCheckBoxEx.YcCheckBoxEx.CheckBoxExPosition.Normal;
			this.chkUnUsed.SingleBorderColor = System.Drawing.Color.DimGray;
			this.chkUnUsed.Size = new System.Drawing.Size(132, 24);
			this.chkUnUsed.TabIndex = 74;
			this.chkUnUsed.TabStop = false;
			this.chkUnUsed.Text = "退職者を表示する";
			this.chkUnUsed.UseVisualStyleBackColor = true;
			// 
			// ycLabelEx3
			// 
			this.ycLabelEx3.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx3.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx3.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx3.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx3.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.IconImage = null;
			this.ycLabelEx3.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx3.Location = new System.Drawing.Point(376, 64);
			this.ycLabelEx3.Name = "ycLabelEx3";
			this.ycLabelEx3.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.Size = new System.Drawing.Size(90, 25);
			this.ycLabelEx3.TabIndex = 79;
			this.ycLabelEx3.Text = "職種区分";
			this.ycLabelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iJob
			// 
			this.iJob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iJob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.iJob.ExCompareContent = null;
			this.iJob.ExCompareValue = null;
			this.iJob.ExDataSource = null;
			this.iJob.ExFocusHighlight = true;
			this.iJob.FlatStyle = GrapeCity.Win.Editors.FlatStyleEx.Flat;
			this.iJob.ListHeaderPane.Height = 27;
			this.iJob.ListHeaderPane.Visible = false;
			this.iJob.Location = new System.Drawing.Point(469, 64);
			this.iJob.Name = "iJob";
			this.iJob.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iJob.Size = new System.Drawing.Size(94, 25);
			this.iJob.TabIndex = 71;
			// 
			// iSearch
			// 
			this.iSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iSearch.ExFocusHighlight = true;
			this.iSearch.ImeStr = "";
			this.iSearch.Location = new System.Drawing.Point(109, 65);
			this.iSearch.Name = "iSearch";
			this.iSearch.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iSearch.Size = new System.Drawing.Size(250, 23);
			this.iSearch.TabIndex = 70;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(606, 62);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(99, 28);
			this.btnClear.TabIndex = 72;
			this.btnClear.Text = "検索クリア";
			this.btnClear.UseVisualStyleBackColor = true;
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
			this.ycLabelEx2.Location = new System.Drawing.Point(26, 65);
			this.ycLabelEx2.Name = "ycLabelEx2";
			this.ycLabelEx2.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.Size = new System.Drawing.Size(80, 23);
			this.ycLabelEx2.TabIndex = 78;
			this.ycLabelEx2.Text = "検索";
			this.ycLabelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// grid
			// 
			this.grid.CaptionHeight = 16;
			this.grid.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
			this.grid.Images.Add(((System.Drawing.Image)(resources.GetObject("grid.Images"))));
			this.grid.Location = new System.Drawing.Point(26, 96);
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
			this.grid.Size = new System.Drawing.Size(907, 407);
			this.grid.TabIndex = 73;
			this.grid.UseCompatibleTextRendering = false;
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.BackColor2 = System.Drawing.Color.Empty;
			this.lblTitle.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblTitle.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblTitle.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblTitle.IconImage = null;
			this.lblTitle.Location = new System.Drawing.Point(46, 16);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblTitle.Size = new System.Drawing.Size(70, 23);
			this.lblTitle.TabIndex = 77;
			this.lblTitle.Text = "会員一覧";
			// 
			// ycLabelEx1
			// 
			this.ycLabelEx1.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx1.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx1.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx1.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.IconImage = null;
			this.ycLabelEx1.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx1.Location = new System.Drawing.Point(26, 12);
			this.ycLabelEx1.Name = "ycLabelEx1";
			this.ycLabelEx1.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.Size = new System.Drawing.Size(10, 29);
			this.ycLabelEx1.TabIndex = 76;
			// 
			// funckey
			// 
			this.funckey.Location = new System.Drawing.Point(0, 541);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(964, 25);
			this.funckey.TabIndex = 75;
			this.funckey.Text = "gcFunctionKey1";
			// 
			// FormStaff
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(964, 566);
			this.Controls.Add(this.ycLabelEx4);
			this.Controls.Add(this.chkUnUsed);
			this.Controls.Add(this.ycLabelEx3);
			this.Controls.Add(this.iJob);
			this.Controls.Add(this.iSearch);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx1);
			this.Controls.Add(this.funckey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "FormStaff";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "会員情報";
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx1, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.grid, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.btnClear, 0);
			this.Controls.SetChildIndex(this.iSearch, 0);
			this.Controls.SetChildIndex(this.iJob, 0);
			this.Controls.SetChildIndex(this.ycLabelEx3, 0);
			this.Controls.SetChildIndex(this.chkUnUsed, 0);
			this.Controls.SetChildIndex(this.ycLabelEx4, 0);
			((System.ComponentModel.ISupportInitialize)(this.iJob)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iSearch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private YControlLabelEx.YcLabelEx ycLabelEx4;
		private YControlYcCheckBoxEx.YcCheckBoxEx chkUnUsed;
		private YControlLabelEx.YcLabelEx ycLabelEx3;
		private GControlGcComboBoxEx.GcComboBoxEx iJob;
		private GControlGcTextBoxEx.GcTextBoxEx iSearch;
		private System.Windows.Forms.Button btnClear;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
		private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private GrapeCity.Win.Bars.GcFunctionKey funckey;
	}
}