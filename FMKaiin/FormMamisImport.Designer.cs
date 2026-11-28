namespace App
{
	partial class FormMamisImport
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
			this.iFile = new App.UcFileSelect();
			this.ycLabelEx6 = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx7 = new YControlLabelEx.YcLabelEx();
			this.lblSetting = new YControlLabelEx.YcLabelEx();
			this.funckey = new App.Controls.FunctionKeyBar();
			this.lblTitle = new YControlLabelEx.YcLabelEx();
			this.ycLabelEx2 = new YControlLabelEx.YcLabelEx();
			this.SuspendLayout();
			// 
			// iFile
			// 
			this.iFile.FileDialogType = App.UcFileSelect.eFileDialogType.FileOpen;
			this.iFile.FileFilter = "CSVファイル|*.csv";
			this.iFile.FileFilterIndex = 0;
			this.iFile.Location = new System.Drawing.Point(178, 83);
			this.iFile.Name = "iFile";
			this.iFile.Size = new System.Drawing.Size(379, 26);
			this.iFile.TabIndex = 1;
			this.iFile.UcText = "";
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
			this.ycLabelEx6.Location = new System.Drawing.Point(42, 83);
			this.ycLabelEx6.Name = "ycLabelEx6";
			this.ycLabelEx6.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx6.Size = new System.Drawing.Size(132, 25);
			this.ycLabelEx6.TabIndex = 337;
			this.ycLabelEx6.Text = "取込CSVファイル";
			this.ycLabelEx6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ycLabelEx7
			// 
			this.ycLabelEx7.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx7.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx7.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx7.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx7.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx7.IconImage = null;
			this.ycLabelEx7.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx7.Location = new System.Drawing.Point(42, 117);
			this.ycLabelEx7.Name = "ycLabelEx7";
			this.ycLabelEx7.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx7.Size = new System.Drawing.Size(132, 25);
			this.ycLabelEx7.TabIndex = 338;
			this.ycLabelEx7.Text = "取込項目";
			this.ycLabelEx7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblSetting
			// 
			this.lblSetting.BackColor2 = System.Drawing.Color.Empty;
			this.lblSetting.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.lblSetting.ForeShadowColor = System.Drawing.Color.Empty;
			this.lblSetting.IconImage = null;
			this.lblSetting.Location = new System.Drawing.Point(180, 119);
			this.lblSetting.Name = "lblSetting";
			this.lblSetting.SingleBorderColor = System.Drawing.Color.Empty;
			this.lblSetting.Size = new System.Drawing.Size(400, 44);
			this.lblSetting.TabIndex = 339;
			this.lblSetting.Text = "取込項目";
			// 
			// funckey
			// 
			this.funckey.Location = new System.Drawing.Point(0, 172);
			this.funckey.Name = "funckey";
			this.funckey.Size = new System.Drawing.Size(602, 66);
			this.funckey.TabIndex = 341;
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
			this.lblTitle.Text = "MAMISデータ取込";
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
			// FormMamisImport
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(602, 238);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.ycLabelEx2);
			this.Controls.Add(this.funckey);
			this.Controls.Add(this.iFile);
			this.Controls.Add(this.ycLabelEx6);
			this.Controls.Add(this.ycLabelEx7);
			this.Controls.Add(this.lblSetting);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMamisImport";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "MAMISデータ取込";
			this.Controls.SetChildIndex(this.ycLabelEx6, 0);
			this.Controls.SetChildIndex(this.ycLabelEx7, 0);
			this.Controls.SetChildIndex(this.lblSetting, 0);
			this.Controls.SetChildIndex(this.iFile, 0);
			this.Controls.SetChildIndex(this.funckey, 0);
			this.Controls.SetChildIndex(this.ycLabelEx2, 0);
			this.Controls.SetChildIndex(this.lblTitle, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private UcFileSelect iFile;
		private YControlLabelEx.YcLabelEx ycLabelEx6;
		private YControlLabelEx.YcLabelEx ycLabelEx7;
		private YControlLabelEx.YcLabelEx lblSetting;
		private App.Controls.FunctionKeyBar funckey;
		private YControlLabelEx.YcLabelEx lblTitle;
		private YControlLabelEx.YcLabelEx ycLabelEx2;
	}
}
