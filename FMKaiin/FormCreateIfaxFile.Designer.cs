
namespace App
{
	partial class FormCreateIfaxFile
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
			this.iIfax = new App.UcTableComboBox();
			this.ycLabelEx6 = new YControlLabelEx.YcLabelEx();
			this.funckey = new GrapeCity.Win.Bars.GcFunctionKey();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.SuspendLayout();
			//
			// iIfax
			//
			this.iIfax.BackColor = System.Drawing.Color.Transparent;
			this.iIfax.ComboBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iIfax.CompareValue = null;
			this.iIfax.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.iIfax.DBView = null;
			this.iIfax.DiabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iIfax.DisabledForeColor = System.Drawing.Color.Black;
			this.iIfax.DropDownSize = new System.Drawing.Size(224, 44);
			this.iIfax.EnterImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.iIfax.ForeColor = System.Drawing.Color.Transparent;
			this.iIfax.HighlightText = true;
			this.iIfax.Location = new System.Drawing.Point(166, 74);
			this.iIfax.Name = "iIfax";
			this.iIfax.RowFilter = "";
			this.iIfax.SelectedIndexNullLeave = -1;
			this.iIfax.Size = new System.Drawing.Size(301, 25);
			this.iIfax.Sort = "";
			this.iIfax.TabIndex = 338;
			this.iIfax.TextSubItemIndex = -1;
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
			this.ycLabelEx6.Location = new System.Drawing.Point(42, 74);
			this.ycLabelEx6.Name = "ycLabelEx6";
			this.ycLabelEx6.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx6.Size = new System.Drawing.Size(120, 25);
			this.ycLabelEx6.TabIndex = 337;
			this.ycLabelEx6.Text = "iFAXグループ";
			this.ycLabelEx6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			//
			// funckey
			//
			this.funckey.Location = new System.Drawing.Point(0, 122);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(525, 25);
			this.funckey.TabIndex = 341;
			this.funckey.Text = "gcFunctionKey1";
			//
			// lblTitle
			//
			this.lblTitle.BackColor2 = System.Drawing.Color.Empty;
			this.lblTitle.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblTitle.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblTitle.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblTitle.IconImage = null;
			this.lblTitle.Location = new System.Drawing.Point(37, 23);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblTitle.Size = new System.Drawing.Size(299, 23);
			this.lblTitle.TabIndex = 343;
			this.lblTitle.Text = "iFAXデータ作成";
			//
			// ycLabelEx2
			//
			this.ycLabelEx2.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx2.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx2.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx2.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.IconImage = null;
			this.ycLabelEx2.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRound;
			this.ycLabelEx2.Location = new System.Drawing.Point(21, 19);
			this.ycLabelEx2.Name = "ycLabelEx2";
			this.ycLabelEx2.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx2.Size = new System.Drawing.Size(10, 30);
			this.ycLabelEx2.TabIndex = 342;
			//
			// FormCreateIfaxFile
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(525, 147);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.funckey);
			this.Controls.Add(this.iIfax);
			this.Controls.Add(this.ycLabelEx6);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormCreateIfaxFile";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "iFAXデータ作成";
			this.Controls.SetChildIndex(this.ycLabelEx6, 0);
			this.Controls.SetChildIndex(this.iIfax, 0);
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private UcTableComboBox iIfax;
		private YControlLabelEx.YcLabelEx ycLabelEx6;
		private GrapeCity.Win.Bars.GcFunctionKey funckey;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
	}
}
