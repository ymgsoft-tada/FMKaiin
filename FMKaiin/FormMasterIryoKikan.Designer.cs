
namespace App
{
	partial class FormMasterIryoKikan
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMasterIryoKikan));
			this.grid = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.iCodeIryo = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx4 = new YControlLabelEx.YcLabelEx();
			this.btnClear = new System.Windows.Forms.Button();
			this.iKbnTaikai = new GControlGcComboBoxEx.GcComboBoxEx(this.components);
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx3 = new YControlLabelEx.YcLabelEx();
			this.iKumiCode = new App.UcTableComboBox();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iCodeIryo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iKbnTaikai)).BeginInit();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.CaptionHeight = 16;
			this.grid.GroupByCaption = "列でグループ化するには、ここに列ヘッダをドラッグします。";
			this.grid.Images.Add(((System.Drawing.Image)(resources.GetObject("grid.Images"))));
			this.grid.Location = new System.Drawing.Point(26, 95);
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
			this.grid.Size = new System.Drawing.Size(754, 351);
			this.grid.TabIndex = 54;
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
			this.lblTitle.Location = new System.Drawing.Point(40, 20);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblTitle.Size = new System.Drawing.Size(100, 23);
			this.lblTitle.TabIndex = 53;
			this.lblTitle.Text = "医療機関一覧";
			// 
			// ycLabelEx1
			// 
			this.ycLabelEx1.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx1.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx1.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx1.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.IconImage = null;
			this.ycLabelEx1.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx1.Location = new System.Drawing.Point(24, 16);
			this.ycLabelEx1.Name = "ycLabelEx1";
			this.ycLabelEx1.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.Size = new System.Drawing.Size(10, 30);
			this.ycLabelEx1.TabIndex = 52;
			// 
			// funckey
			// 
			this.funckey.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.funckey.Location = new System.Drawing.Point(0, 475);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(800, 25);
			this.funckey.TabIndex = 51;
			this.funckey.Text = "gcFunctionKey1";
			// 
			// iCodeIryo
			// 
			this.iCodeIryo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iCodeIryo.ExFocusHighlight = true;
			this.iCodeIryo.ImeStr = "";
			this.iCodeIryo.Location = new System.Drawing.Point(102, 56);
			this.iCodeIryo.Name = "iCodeIryo";
			this.iCodeIryo.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iCodeIryo.Size = new System.Drawing.Size(88, 25);
			this.iCodeIryo.TabIndex = 68;
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
			this.ycLabelEx4.Location = new System.Drawing.Point(24, 56);
			this.ycLabelEx4.Name = "ycLabelEx4";
			this.ycLabelEx4.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.Size = new System.Drawing.Size(72, 25);
			this.ycLabelEx4.TabIndex = 71;
			this.ycLabelEx4.Text = "機関コード";
			this.ycLabelEx4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(681, 54);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(99, 28);
			this.btnClear.TabIndex = 69;
			this.btnClear.Text = "検索クリア";
			this.btnClear.UseVisualStyleBackColor = true;
			// 
			// iKbnTaikai
			// 
			this.iKbnTaikai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iKbnTaikai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.iKbnTaikai.ExCompareContent = null;
			this.iKbnTaikai.ExCompareValue = null;
			this.iKbnTaikai.ExDataSource = null;
			this.iKbnTaikai.ExFocusHighlight = true;
			this.iKbnTaikai.FlatStyle = GrapeCity.Win.Editors.FlatStyleEx.Flat;
			this.iKbnTaikai.ListHeaderPane.Height = 27;
			this.iKbnTaikai.ListHeaderPane.Visible = false;
			this.iKbnTaikai.Location = new System.Drawing.Point(530, 56);
			this.iKbnTaikai.Name = "iKbnTaikai";
			this.iKbnTaikai.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iKbnTaikai.Size = new System.Drawing.Size(80, 25);
			this.iKbnTaikai.TabIndex = 67;
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
			this.ycLabelEx2.Location = new System.Drawing.Point(225, 56);
			this.ycLabelEx2.Name = "ycLabelEx2";
			this.ycLabelEx2.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.Size = new System.Drawing.Size(67, 25);
			this.ycLabelEx2.TabIndex = 70;
			this.ycLabelEx2.Text = "組コード";
			this.ycLabelEx2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			this.ycLabelEx3.Location = new System.Drawing.Point(457, 56);
			this.ycLabelEx3.Name = "ycLabelEx3";
			this.ycLabelEx3.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.Size = new System.Drawing.Size(67, 25);
			this.ycLabelEx3.TabIndex = 72;
			this.ycLabelEx3.Text = "退会区分";
			this.ycLabelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iKumiCode
			// 
			this.iKumiCode.BackColor = System.Drawing.Color.Transparent;
			this.iKumiCode.ComboBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iKumiCode.CompareValue = null;
			this.iKumiCode.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.iKumiCode.DBView = null;
			this.iKumiCode.DiabledBackColor = System.Drawing.SystemColors.Control;
			this.iKumiCode.DisabledForeColor = System.Drawing.SystemColors.GrayText;
			this.iKumiCode.DropDownSize = new System.Drawing.Size(100, 44);
			this.iKumiCode.EnterImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.iKumiCode.ForeColor = System.Drawing.Color.Transparent;
			this.iKumiCode.HighlightText = true;
			this.iKumiCode.Location = new System.Drawing.Point(298, 55);
			this.iKumiCode.Name = "iKumiCode";
			this.iKumiCode.RowFilter = "";
			this.iKumiCode.SelectedIndexNullLeave = -1;
			this.iKumiCode.Size = new System.Drawing.Size(124, 26);
			this.iKumiCode.Sort = "";
			this.iKumiCode.TabIndex = 73;
			this.iKumiCode.TextSubItemIndex = -1;
			// 
			// FormMasterIryoKikan
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 500);
			this.Controls.Add(this.iKumiCode);
			this.Controls.Add(this.ycLabelEx3);
			this.Controls.Add(this.iCodeIryo);
			this.Controls.Add(this.ycLabelEx4);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.iKbnTaikai);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.grid);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx1);
			this.Controls.Add(this.funckey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "FormMasterIryoKikan";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "医療機関マスタ";
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx1, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.grid, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.iKbnTaikai, 0);
			this.Controls.SetChildIndex(this.btnClear, 0);
			this.Controls.SetChildIndex(this.ycLabelEx4, 0);
			this.Controls.SetChildIndex(this.iCodeIryo, 0);
			this.Controls.SetChildIndex(this.ycLabelEx3, 0);
			this.Controls.SetChildIndex(this.iKumiCode, 0);
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iCodeIryo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iKbnTaikai)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private GrapeCity.Win.Bars.GcFunctionKey funckey;
		private GControlGcTextBoxEx.GcTextBoxEx iCodeIryo;
		private YControlLabelEx.YcLabelEx ycLabelEx4;
		private System.Windows.Forms.Button btnClear;
		private GControlGcComboBoxEx.GcComboBoxEx iKbnTaikai;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
		private YControlLabelEx.YcLabelEx ycLabelEx3;
		private UcTableComboBox iKumiCode;
	}
}