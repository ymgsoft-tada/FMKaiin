namespace App
{
	partial class FormLogin
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
			this.ycLabelEx3 = new YControlLabelEx.YcLabelEx();
			this.iCode = new GControlGcTextBoxEx.GcTextBoxEx();
			this.ycLabelEx1 = new YControlLabelEx.YcLabelEx();
			this.iPwd = new GControlGcTextBoxEx.GcTextBoxEx();
			this.btnLogin = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.iCode)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iPwd)).BeginInit();
			this.SuspendLayout();
			// 
			// ycLabelEx3
			// 
			this.ycLabelEx3.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx3.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx3.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx3.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ycLabelEx3.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx3.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.IconImage = null;
			this.ycLabelEx3.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx3.Location = new System.Drawing.Point(31, 41);
			this.ycLabelEx3.Name = "ycLabelEx3";
			this.ycLabelEx3.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx3.Size = new System.Drawing.Size(93, 22);
			this.ycLabelEx3.TabIndex = 100;
			this.ycLabelEx3.Text = "担当者コード";
			this.ycLabelEx3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iCode
			// 
			this.iCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iCode.ContentAlignment = System.Drawing.ContentAlignment.BottomLeft;
			this.iCode.ExFocusHighlight = true;
			this.iCode.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.iCode.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.iCode.ImeStr = "";
			this.iCode.Location = new System.Drawing.Point(130, 41);
			this.iCode.Name = "iCode";
			this.iCode.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iCode.Size = new System.Drawing.Size(100, 22);
			this.iCode.TabIndex = 0;
			// 
			// ycLabelEx1
			// 
			this.ycLabelEx1.BackColor = System.Drawing.Color.SteelBlue;
			this.ycLabelEx1.BackColor2 = System.Drawing.Color.Empty;
			this.ycLabelEx1.DisabledBackColor = System.Drawing.SystemColors.ControlDark;
			this.ycLabelEx1.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ycLabelEx1.ForeColor = System.Drawing.Color.White;
			this.ycLabelEx1.ForeShadowColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.IconImage = null;
			this.ycLabelEx1.LabelBorderStyle = YControlLabelEx.YcLabelEx.SingleBorderStyle.FixedRoundLeft;
			this.ycLabelEx1.Location = new System.Drawing.Point(31, 69);
			this.ycLabelEx1.Name = "ycLabelEx1";
			this.ycLabelEx1.SingleBorderColor = System.Drawing.Color.Empty;
			this.ycLabelEx1.Size = new System.Drawing.Size(93, 22);
			this.ycLabelEx1.TabIndex = 102;
			this.ycLabelEx1.Text = "パスワード";
			this.ycLabelEx1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// iPwd
			// 
			this.iPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iPwd.ContentAlignment = System.Drawing.ContentAlignment.BottomLeft;
			this.iPwd.ExFocusHighlight = true;
			this.iPwd.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.iPwd.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.iPwd.ImeStr = "";
			this.iPwd.Location = new System.Drawing.Point(130, 69);
			this.iPwd.Name = "iPwd";
			this.iPwd.PasswordChar = '*';
			this.iPwd.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iPwd.Size = new System.Drawing.Size(230, 22);
			this.iPwd.TabIndex = 1;
			// 
			// btnLogin
			// 
			this.btnLogin.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.btnLogin.Location = new System.Drawing.Point(281, 107);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(80, 27);
			this.btnLogin.TabIndex = 2;
			this.btnLogin.Text = "ログイン";
			this.btnLogin.UseVisualStyleBackColor = true;
			// 
			// FormLogin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(391, 158);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.ycLabelEx1);
			this.Controls.Add(this.iPwd);
			this.Controls.Add(this.ycLabelEx3);
			this.Controls.Add(this.iCode);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLogin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ログイン";
			this.Controls.SetChildIndex(this.iCode, 0);
			this.Controls.SetChildIndex(this.ycLabelEx3, 0);
			this.Controls.SetChildIndex(this.iPwd, 0);
			this.Controls.SetChildIndex(this.ycLabelEx1, 0);
			this.Controls.SetChildIndex(this.btnLogin, 0);
			((System.ComponentModel.ISupportInitialize)(this.iCode)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iPwd)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private YControlLabelEx.YcLabelEx ycLabelEx3;
		private GControlGcTextBoxEx.GcTextBoxEx iCode;
		private YControlLabelEx.YcLabelEx ycLabelEx1;
		private GControlGcTextBoxEx.GcTextBoxEx iPwd;
		private System.Windows.Forms.Button btnLogin;
	}
}