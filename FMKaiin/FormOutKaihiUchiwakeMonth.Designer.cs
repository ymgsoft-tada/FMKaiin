
namespace App
{
	partial class FormOutKaihiUchiwakeMonth
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
			this.ycLabelEx6 = new YControlLabelEx.YcLabelEx();
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx3 = new YControlLabelEx.YcLabelEx();
			this.iKaiinFrom = new App.UcTableComboBox();
			this.iKaiinTo = new App.UcTableComboBox();
			this.iZaisekiFrom = new GControlGcComboBoxEx.GcComboBoxEx(this.components);
			this.ycLabelEx14 = new YControlLabelEx.YcLabelEx();
			this.iZaisekiTo = new GControlGcComboBoxEx.GcComboBoxEx(this.components);
			this.iTaishoYM = new App.UcDate();
			this.ycLabelEx33 = new YControlLabelEx.YcLabelEx();
			this.iUchiwakeFrom = new GControlGcComboBoxEx.GcComboBoxEx(this.components);
			this.ycLabelEx4 = new YControlLabelEx.YcLabelEx();
			((System.ComponentModel.ISupportInitialize)(this.iZaisekiFrom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iZaisekiTo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iUchiwakeFrom)).BeginInit();
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
			this.ycLabelEx6.Text = "会員";
			this.ycLabelEx6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// funckey
			// 
			this.funckey.Location = new System.Drawing.Point(0, 234);
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
			// iKaiinFrom
			// 
			this.iKaiinFrom.BackColor = System.Drawing.Color.Transparent;
			this.iKaiinFrom.ComboBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iKaiinFrom.CompareValue = null;
			this.iKaiinFrom.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.iKaiinFrom.DBView = null;
			this.iKaiinFrom.DiabledBackColor = System.Drawing.SystemColors.Control;
			this.iKaiinFrom.DisabledForeColor = System.Drawing.SystemColors.GrayText;
			this.iKaiinFrom.DropDownSize = new System.Drawing.Size(100, 44);
			this.iKaiinFrom.EnterImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.iKaiinFrom.ForeColor = System.Drawing.Color.Transparent;
			this.iKaiinFrom.HighlightText = true;
			this.iKaiinFrom.Location = new System.Drawing.Point(134, 99);
			this.iKaiinFrom.Name = "iKaiinFrom";
			this.iKaiinFrom.RowFilter = "";
			this.iKaiinFrom.SelectedIndexNullLeave = -1;
			this.iKaiinFrom.Size = new System.Drawing.Size(138, 25);
			this.iKaiinFrom.Sort = "";
			this.iKaiinFrom.TabIndex = 2;
			this.iKaiinFrom.TextSubItemIndex = -1;
			// 
			// iKaiinTo
			// 
			this.iKaiinTo.BackColor = System.Drawing.Color.Transparent;
			this.iKaiinTo.ComboBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iKaiinTo.CompareValue = null;
			this.iKaiinTo.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.iKaiinTo.DBView = null;
			this.iKaiinTo.DiabledBackColor = System.Drawing.SystemColors.Control;
			this.iKaiinTo.DisabledForeColor = System.Drawing.SystemColors.GrayText;
			this.iKaiinTo.DropDownSize = new System.Drawing.Size(100, 44);
			this.iKaiinTo.EnterImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.iKaiinTo.ForeColor = System.Drawing.Color.Transparent;
			this.iKaiinTo.HighlightText = true;
			this.iKaiinTo.Location = new System.Drawing.Point(320, 99);
			this.iKaiinTo.Name = "iKaiinTo";
			this.iKaiinTo.RowFilter = "";
			this.iKaiinTo.SelectedIndexNullLeave = -1;
			this.iKaiinTo.Size = new System.Drawing.Size(138, 25);
			this.iKaiinTo.Sort = "";
			this.iKaiinTo.TabIndex = 3;
			this.iKaiinTo.TextSubItemIndex = -1;
			// 
			// iZaisekiFrom
			// 
			this.iZaisekiFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iZaisekiFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.iZaisekiFrom.ExCompareContent = null;
			this.iZaisekiFrom.ExCompareValue = null;
			this.iZaisekiFrom.ExDataSource = null;
			this.iZaisekiFrom.ExFocusHighlight = true;
			this.iZaisekiFrom.FlatStyle = GrapeCity.Win.Editors.FlatStyleEx.Flat;
			this.iZaisekiFrom.ListHeaderPane.Height = 27;
			this.iZaisekiFrom.ListHeaderPane.Visible = false;
			this.iZaisekiFrom.Location = new System.Drawing.Point(134, 142);
			this.iZaisekiFrom.Name = "iZaisekiFrom";
			this.iZaisekiFrom.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iZaisekiFrom.Size = new System.Drawing.Size(71, 25);
			this.iZaisekiFrom.TabIndex = 4;
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
			this.ycLabelEx14.Text = "在籍区分";
			this.ycLabelEx14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iZaisekiTo
			// 
			this.iZaisekiTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iZaisekiTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.iZaisekiTo.ExCompareContent = null;
			this.iZaisekiTo.ExCompareValue = null;
			this.iZaisekiTo.ExDataSource = null;
			this.iZaisekiTo.ExFocusHighlight = true;
			this.iZaisekiTo.FlatStyle = GrapeCity.Win.Editors.FlatStyleEx.Flat;
			this.iZaisekiTo.ListHeaderPane.Height = 27;
			this.iZaisekiTo.ListHeaderPane.Visible = false;
			this.iZaisekiTo.Location = new System.Drawing.Point(320, 142);
			this.iZaisekiTo.Name = "iZaisekiTo";
			this.iZaisekiTo.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iZaisekiTo.Size = new System.Drawing.Size(71, 25);
			this.iZaisekiTo.TabIndex = 5;
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
			// iUchiwakeFrom
			// 
			this.iUchiwakeFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iUchiwakeFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.iUchiwakeFrom.ExCompareContent = null;
			this.iUchiwakeFrom.ExCompareValue = null;
			this.iUchiwakeFrom.ExDataSource = null;
			this.iUchiwakeFrom.ExFocusHighlight = true;
			this.iUchiwakeFrom.FlatStyle = GrapeCity.Win.Editors.FlatStyleEx.Flat;
			this.iUchiwakeFrom.ListHeaderPane.Height = 27;
			this.iUchiwakeFrom.ListHeaderPane.Visible = false;
			this.iUchiwakeFrom.Location = new System.Drawing.Point(134, 182);
			this.iUchiwakeFrom.Name = "iUchiwakeFrom";
			this.iUchiwakeFrom.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iUchiwakeFrom.Size = new System.Drawing.Size(71, 25);
			this.iUchiwakeFrom.TabIndex = 6;
			// 
			// ycLabelEx4
			// 
			this.ycLabelEx4.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx4.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx4.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx4.Font = new System.Drawing.Font("メイリオ", 9.75F);
			this.ycLabelEx4.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx4.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.IconImage = null;
			this.ycLabelEx4.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx4.Location = new System.Drawing.Point(28, 182);
			this.ycLabelEx4.Name = "ycLabelEx4";
			this.ycLabelEx4.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx4.Size = new System.Drawing.Size(100, 25);
			this.ycLabelEx4.TabIndex = 421;
			this.ycLabelEx4.Text = "月別会費内訳";
			this.ycLabelEx4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FormOutKaihiUchiwakeMonth
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(491, 259);
			this.Controls.Add(this.iUchiwakeFrom);
			this.Controls.Add(this.ycLabelEx4);
			this.Controls.Add(this.iTaishoYM);
			this.Controls.Add(this.ycLabelEx33);
			this.Controls.Add(this.iZaisekiTo);
			this.Controls.Add(this.iZaisekiFrom);
			this.Controls.Add(this.ycLabelEx14);
			this.Controls.Add(this.iKaiinTo);
			this.Controls.Add(this.iKaiinFrom);
			this.Controls.Add(this.ycLabelEx6);
			this.Controls.Add(this.funckey);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormOutKaihiUchiwakeMonth";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "会費月別内訳";
			this.Controls.SetChildIndex(this.ycLabelEx3, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx6, 0);
			this.Controls.SetChildIndex(this.iKaiinFrom, 0);
			this.Controls.SetChildIndex(this.iKaiinTo, 0);
			this.Controls.SetChildIndex(this.ycLabelEx14, 0);
			this.Controls.SetChildIndex(this.iZaisekiFrom, 0);
			this.Controls.SetChildIndex(this.iZaisekiTo, 0);
			this.Controls.SetChildIndex(this.ycLabelEx33, 0);
			this.Controls.SetChildIndex(this.iTaishoYM, 0);
			this.Controls.SetChildIndex(this.ycLabelEx4, 0);
			this.Controls.SetChildIndex(this.iUchiwakeFrom, 0);
			((System.ComponentModel.ISupportInitialize)(this.iZaisekiFrom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iZaisekiTo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iUchiwakeFrom)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private YControlLabelEx.YcLabelEx ycLabelEx6;
		private GrapeCity.Win.Bars.GcFunctionKey funckey;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx3;
		private UcTableComboBox iKaiinFrom;
		private UcTableComboBox iKaiinTo;
		private GControlGcComboBoxEx.GcComboBoxEx iZaisekiFrom;
		private YControlLabelEx.YcLabelEx ycLabelEx14;
		private GControlGcComboBoxEx.GcComboBoxEx iZaisekiTo;
		private UcDate iTaishoYM;
		private YControlLabelEx.YcLabelEx ycLabelEx33;
		private GControlGcComboBoxEx.GcComboBoxEx iUchiwakeFrom;
		private YControlLabelEx.YcLabelEx ycLabelEx4;
	}
}