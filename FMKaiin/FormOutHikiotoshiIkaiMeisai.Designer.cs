
namespace App
{
	partial class FormOutHikiotoshiIkaiMeisai
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
			this.ycLabelEx6 = new YControlLabelEx.YcLabelEx();
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx3 = new YControlLabelEx.YcLabelEx();
			this.iIkaiFrom = new App.UcTableComboBox();
			this.iIkaiTo = new App.UcTableComboBox();
			this.ycLabelEx14 = new YControlLabelEx.YcLabelEx();
			this.iTaishoYM = new App.UcDate();
			this.ycLabelEx33 = new YControlLabelEx.YcLabelEx();
			this.iCostFrom = new GControlGcTextBoxEx.GcTextBoxEx();
			this.iCostTo = new GControlGcTextBoxEx.GcTextBoxEx();
			((System.ComponentModel.ISupportInitialize)(this.iCostFrom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iCostTo)).BeginInit();
			this.SuspendLayout();
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
			this.ycLabelEx6.Location = new System.Drawing.Point(28, 99);
			this.ycLabelEx6.Name = "ycLabelEx6";
			this.ycLabelEx6.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx6.Size = new System.Drawing.Size(100, 25);
			this.ycLabelEx6.TabIndex = 75;
			this.ycLabelEx6.Text = "医会";
			this.ycLabelEx6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// funckey
			// 
			this.funckey.Location = new System.Drawing.Point(0, 189);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(491, 25);
			this.funckey.TabIndex = 74;
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
			this.lblTitle.Location = new System.Drawing.Point(44, 17);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblTitle.Size = new System.Drawing.Size(70, 23);
			this.lblTitle.TabIndex = 73;
			this.lblTitle.Text = "出力条件";
			// 
			// ycLabelEx3
			// 
			this.ycLabelEx3.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx3.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx3.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx3.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.IconImage = null;
			this.ycLabelEx3.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx3.Location = new System.Drawing.Point(24, 13);
			this.ycLabelEx3.Name = "ycLabelEx3";
			this.ycLabelEx3.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.Size = new System.Drawing.Size(10, 29);
			this.ycLabelEx3.TabIndex = 72;
			// 
			// iIkaiFrom
			// 
			this.iIkaiFrom.BackColor = System.Drawing.Color.Transparent;
			this.iIkaiFrom.ComboBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iIkaiFrom.CompareValue = null;
			this.iIkaiFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.iIkaiFrom.DBView = null;
			this.iIkaiFrom.DiabledBackColor = System.Drawing.SystemColors.Control;
			this.iIkaiFrom.DisabledForeColor = System.Drawing.SystemColors.GrayText;
			this.iIkaiFrom.DropDownSize = new System.Drawing.Size(100, 44);
			this.iIkaiFrom.EnterImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.iIkaiFrom.ForeColor = System.Drawing.Color.Transparent;
			this.iIkaiFrom.HighlightText = true;
			this.iIkaiFrom.Location = new System.Drawing.Point(134, 99);
			this.iIkaiFrom.Name = "iIkaiFrom";
			this.iIkaiFrom.RowFilter = "";
			this.iIkaiFrom.SelectedIndexNullLeave = -1;
			this.iIkaiFrom.Size = new System.Drawing.Size(138, 25);
			this.iIkaiFrom.Sort = "";
			this.iIkaiFrom.TabIndex = 2;
			this.iIkaiFrom.TextSubItemIndex = -1;
			// 
			// iIkaiTo
			// 
			this.iIkaiTo.BackColor = System.Drawing.Color.Transparent;
			this.iIkaiTo.ComboBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iIkaiTo.CompareValue = null;
			this.iIkaiTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.iIkaiTo.DBView = null;
			this.iIkaiTo.DiabledBackColor = System.Drawing.SystemColors.Control;
			this.iIkaiTo.DisabledForeColor = System.Drawing.SystemColors.GrayText;
			this.iIkaiTo.DropDownSize = new System.Drawing.Size(100, 44);
			this.iIkaiTo.EnterImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.iIkaiTo.ForeColor = System.Drawing.Color.Transparent;
			this.iIkaiTo.HighlightText = true;
			this.iIkaiTo.Location = new System.Drawing.Point(320, 99);
			this.iIkaiTo.Name = "iIkaiTo";
			this.iIkaiTo.RowFilter = "";
			this.iIkaiTo.SelectedIndexNullLeave = -1;
			this.iIkaiTo.Size = new System.Drawing.Size(138, 25);
			this.iIkaiTo.Sort = "";
			this.iIkaiTo.TabIndex = 3;
			this.iIkaiTo.TextSubItemIndex = -1;
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
			this.ycLabelEx14.Location = new System.Drawing.Point(28, 142);
			this.ycLabelEx14.Name = "ycLabelEx14";
			this.ycLabelEx14.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx14.Size = new System.Drawing.Size(100, 25);
			this.ycLabelEx14.TabIndex = 394;
			this.ycLabelEx14.Text = "金額";
			this.ycLabelEx14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iTaishoYM
			// 
			this.iTaishoYM.DisabledBackColor = System.Drawing.SystemColors.Control;
			this.iTaishoYM.DisabledForeColor = System.Drawing.Color.Black;
			this.iTaishoYM.Location = new System.Drawing.Point(135, 59);
			this.iTaishoYM.Name = "iTaishoYM";
			this.iTaishoYM.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iTaishoYM.Size = new System.Drawing.Size(115, 25);
			this.iTaishoYM.TabIndex = 1;
			this.iTaishoYM.UcBackColor = System.Drawing.SystemColors.Window;
			this.iTaishoYM.UcBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iTaishoYM.UcCalenderMaxDate = new System.DateTime(2100, 12, 31, 23, 59, 59, 0);
			this.iTaishoYM.UcCalenderMinDate = new System.DateTime(1868, 1, 1, 0, 0, 0, 0);
			this.iTaishoYM.UcEnabled = true;
			this.iTaishoYM.UcFormat = App.UcDate.UcDateFormat.YYYYMMDD;
			this.iTaishoYM.UcReadOnly = false;
			this.iTaishoYM.UcSideButton = true;
			this.iTaishoYM.UcValue = new System.DateTime(2023, 3, 23, 15, 30, 51, 214);
			// 
			// ycLabelEx33
			// 
			this.ycLabelEx33.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx33.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx33.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx33.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx33.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx33.IconImage = null;
			this.ycLabelEx33.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx33.Location = new System.Drawing.Point(28, 59);
			this.ycLabelEx33.Name = "ycLabelEx33";
			this.ycLabelEx33.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx33.Size = new System.Drawing.Size(100, 25);
			this.ycLabelEx33.TabIndex = 418;
			this.ycLabelEx33.Text = "対象年月";
			this.ycLabelEx33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iCostFrom
			// 
			this.iCostFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iCostFrom.ExFocusHighlight = true;
			this.iCostFrom.ImeStr = "";
			this.iCostFrom.Location = new System.Drawing.Point(134, 143);
			this.iCostFrom.Name = "iCostFrom";
			this.iCostFrom.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iCostFrom.Size = new System.Drawing.Size(88, 23);
			this.iCostFrom.TabIndex = 4;
			// 
			// iCostTo
			// 
			this.iCostTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iCostTo.ExFocusHighlight = true;
			this.iCostTo.ImeStr = "";
			this.iCostTo.Location = new System.Drawing.Point(320, 143);
			this.iCostTo.Name = "iCostTo";
			this.iCostTo.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iCostTo.Size = new System.Drawing.Size(88, 23);
			this.iCostTo.TabIndex = 5;
			// 
			// FormOutHikiotoshiIkaiMeisai
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(491, 214);
			this.Controls.Add(this.iCostTo);
			this.Controls.Add(this.iCostFrom);
			this.Controls.Add(this.iTaishoYM);
			this.Controls.Add(this.ycLabelEx33);
			this.Controls.Add(this.ycLabelEx14);
			this.Controls.Add(this.iIkaiTo);
			this.Controls.Add(this.iIkaiFrom);
			this.Controls.Add(this.ycLabelEx6);
			this.Controls.Add(this.funckey);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormOutHikiotoshiIkaiMeisai";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "所属医会別引落明細表";
			this.Controls.SetChildIndex(this.ycLabelEx3, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx6, 0);
			this.Controls.SetChildIndex(this.iIkaiFrom, 0);
			this.Controls.SetChildIndex(this.iIkaiTo, 0);
			this.Controls.SetChildIndex(this.ycLabelEx14, 0);
			this.Controls.SetChildIndex(this.ycLabelEx33, 0);
			this.Controls.SetChildIndex(this.iTaishoYM, 0);
			this.Controls.SetChildIndex(this.iCostFrom, 0);
			this.Controls.SetChildIndex(this.iCostTo, 0);
			((System.ComponentModel.ISupportInitialize)(this.iCostFrom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iCostTo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private YControlLabelEx.YcLabelEx ycLabelEx6;
		private GrapeCity.Win.Bars.GcFunctionKey funckey;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx3;
		private UcTableComboBox iIkaiFrom;
		private UcTableComboBox iIkaiTo;
		private YControlLabelEx.YcLabelEx ycLabelEx14;
		private UcDate iTaishoYM;
		private YControlLabelEx.YcLabelEx ycLabelEx33;
		private GControlGcTextBoxEx.GcTextBoxEx iCostFrom;
		private GControlGcTextBoxEx.GcTextBoxEx iCostTo;
	}
}