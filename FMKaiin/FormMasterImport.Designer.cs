
namespace App
{
	partial class FormMasterImport
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
			this.grpIryoKikan = new System.Windows.Forms.GroupBox();
			this.btnIryoKikan = new System.Windows.Forms.Button();
			this.iFileIryoKikan = new App.UcFileSelect();
			this.grpKaihi = new System.Windows.Forms.GroupBox();
			this.btnKaihi = new System.Windows.Forms.Button();
			this.iFileKaihi = new App.UcFileSelect();
			this.grpKaiin = new System.Windows.Forms.GroupBox();
			this.btnKaiin = new System.Windows.Forms.Button();
			this.iFileKaiin = new App.UcFileSelect();
			this.chkClearIryoKikan = new System.Windows.Forms.CheckBox();
			this.chkClearKaihi = new System.Windows.Forms.CheckBox();
			this.chkClearKaiin = new System.Windows.Forms.CheckBox();
			this.chkFillIryoKikan = new System.Windows.Forms.CheckBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.grpIryoKikan.SuspendLayout();
			this.grpKaihi.SuspendLayout();
			this.grpKaiin.SuspendLayout();
			this.SuspendLayout();
			//
			// grpIryoKikan
			//
			this.grpIryoKikan.Controls.Add(this.chkClearIryoKikan);
			this.grpIryoKikan.Controls.Add(this.btnIryoKikan);
			this.grpIryoKikan.Controls.Add(this.iFileIryoKikan);
			this.grpIryoKikan.Location = new System.Drawing.Point(20, 16);
			this.grpIryoKikan.Name = "grpIryoKikan";
			this.grpIryoKikan.Size = new System.Drawing.Size(680, 104);
			this.grpIryoKikan.TabIndex = 1;
			this.grpIryoKikan.TabStop = false;
			this.grpIryoKikan.Text = "医療機関";
			//
			// chkClearIryoKikan
			//
			this.chkClearIryoKikan.AutoSize = true;
			this.chkClearIryoKikan.Location = new System.Drawing.Point(16, 68);
			this.chkClearIryoKikan.Name = "chkClearIryoKikan";
			this.chkClearIryoKikan.Size = new System.Drawing.Size(300, 24);
			this.chkClearIryoKikan.TabIndex = 3;
			this.chkClearIryoKikan.Text = "取込前に医療機関データを全てクリアする";
			this.chkClearIryoKikan.UseVisualStyleBackColor = true;
			//
			// btnIryoKikan
			//
			this.btnIryoKikan.Location = new System.Drawing.Point(554, 28);
			this.btnIryoKikan.Name = "btnIryoKikan";
			this.btnIryoKikan.Size = new System.Drawing.Size(110, 32);
			this.btnIryoKikan.TabIndex = 2;
			this.btnIryoKikan.Text = "取込実行";
			this.btnIryoKikan.UseVisualStyleBackColor = true;
			//
			// iFileIryoKikan
			//
			this.iFileIryoKikan.FileDialogType = App.UcFileSelect.eFileDialogType.FileOpen;
			this.iFileIryoKikan.FileFilter = "Excelファイル|*.xlsx;*.xls";
			this.iFileIryoKikan.FileFilterIndex = 0;
			this.iFileIryoKikan.Location = new System.Drawing.Point(16, 31);
			this.iFileIryoKikan.Name = "iFileIryoKikan";
			this.iFileIryoKikan.Size = new System.Drawing.Size(526, 26);
			this.iFileIryoKikan.TabIndex = 1;
			this.iFileIryoKikan.UcText = "";
			//
			// grpKaihi
			//
			this.grpKaihi.Controls.Add(this.chkClearKaihi);
			this.grpKaihi.Controls.Add(this.btnKaihi);
			this.grpKaihi.Controls.Add(this.iFileKaihi);
			this.grpKaihi.Location = new System.Drawing.Point(20, 132);
			this.grpKaihi.Name = "grpKaihi";
			this.grpKaihi.Size = new System.Drawing.Size(680, 104);
			this.grpKaihi.TabIndex = 2;
			this.grpKaihi.TabStop = false;
			this.grpKaihi.Text = "会費";
			//
			// chkClearKaihi
			//
			this.chkClearKaihi.AutoSize = true;
			this.chkClearKaihi.Location = new System.Drawing.Point(16, 68);
			this.chkClearKaihi.Name = "chkClearKaihi";
			this.chkClearKaihi.Size = new System.Drawing.Size(300, 24);
			this.chkClearKaihi.TabIndex = 3;
			this.chkClearKaihi.Text = "取込前に会費データを全てクリアする";
			this.chkClearKaihi.UseVisualStyleBackColor = true;
			//
			// btnKaihi
			//
			this.btnKaihi.Location = new System.Drawing.Point(554, 28);
			this.btnKaihi.Name = "btnKaihi";
			this.btnKaihi.Size = new System.Drawing.Size(110, 32);
			this.btnKaihi.TabIndex = 2;
			this.btnKaihi.Text = "取込実行";
			this.btnKaihi.UseVisualStyleBackColor = true;
			//
			// iFileKaihi
			//
			this.iFileKaihi.FileDialogType = App.UcFileSelect.eFileDialogType.FileOpen;
			this.iFileKaihi.FileFilter = "Excelファイル|*.xlsx;*.xls";
			this.iFileKaihi.FileFilterIndex = 0;
			this.iFileKaihi.Location = new System.Drawing.Point(16, 31);
			this.iFileKaihi.Name = "iFileKaihi";
			this.iFileKaihi.Size = new System.Drawing.Size(526, 26);
			this.iFileKaihi.TabIndex = 1;
			this.iFileKaihi.UcText = "";
			//
			// grpKaiin
			//
			this.grpKaiin.Controls.Add(this.chkFillIryoKikan);
			this.grpKaiin.Controls.Add(this.chkClearKaiin);
			this.grpKaiin.Controls.Add(this.btnKaiin);
			this.grpKaiin.Controls.Add(this.iFileKaiin);
			this.grpKaiin.Location = new System.Drawing.Point(20, 248);
			this.grpKaiin.Name = "grpKaiin";
			this.grpKaiin.Size = new System.Drawing.Size(680, 134);
			this.grpKaiin.TabIndex = 3;
			this.grpKaiin.TabStop = false;
			this.grpKaiin.Text = "会員情報";
			//
			// chkFillIryoKikan
			//
			this.chkFillIryoKikan.AutoSize = true;
			this.chkFillIryoKikan.Location = new System.Drawing.Point(16, 98);
			this.chkFillIryoKikan.Name = "chkFillIryoKikan";
			this.chkFillIryoKikan.Size = new System.Drawing.Size(400, 24);
			this.chkFillIryoKikan.TabIndex = 4;
			this.chkFillIryoKikan.Text = "医療機関マスターの開設主体・病床・併設を、会員情報から補完する";
			this.chkFillIryoKikan.UseVisualStyleBackColor = true;
			//
			// chkClearKaiin
			//
			this.chkClearKaiin.AutoSize = true;
			this.chkClearKaiin.Location = new System.Drawing.Point(16, 68);
			this.chkClearKaiin.Name = "chkClearKaiin";
			this.chkClearKaiin.Size = new System.Drawing.Size(300, 24);
			this.chkClearKaiin.TabIndex = 3;
			this.chkClearKaiin.Text = "取込前に会員情報データを全てクリアする";
			this.chkClearKaiin.UseVisualStyleBackColor = true;
			//
			// btnKaiin
			//
			this.btnKaiin.Location = new System.Drawing.Point(554, 28);
			this.btnKaiin.Name = "btnKaiin";
			this.btnKaiin.Size = new System.Drawing.Size(110, 32);
			this.btnKaiin.TabIndex = 2;
			this.btnKaiin.Text = "取込実行";
			this.btnKaiin.UseVisualStyleBackColor = true;
			//
			// iFileKaiin
			//
			this.iFileKaiin.FileDialogType = App.UcFileSelect.eFileDialogType.FileOpen;
			this.iFileKaiin.FileFilter = "Excelファイル|*.xlsx;*.xls";
			this.iFileKaiin.FileFilterIndex = 0;
			this.iFileKaiin.Location = new System.Drawing.Point(16, 31);
			this.iFileKaiin.Name = "iFileKaiin";
			this.iFileKaiin.Size = new System.Drawing.Size(526, 26);
			this.iFileKaiin.TabIndex = 1;
			this.iFileKaiin.UcText = "";
			//
			// btnClose
			//
			this.btnClose.Location = new System.Drawing.Point(590, 398);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(110, 32);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "閉じる";
			this.btnClose.UseVisualStyleBackColor = true;
			//
			// FormMasterImport
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(720, 446);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.grpKaiin);
			this.Controls.Add(this.grpKaihi);
			this.Controls.Add(this.grpIryoKikan);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMasterImport";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "マスター取込";
			this.Controls.SetChildIndex(this.grpIryoKikan, 0);
			this.Controls.SetChildIndex(this.grpKaihi, 0);
			this.Controls.SetChildIndex(this.grpKaiin, 0);
			this.Controls.SetChildIndex(this.btnClose, 0);
			this.grpIryoKikan.ResumeLayout(false);
			this.grpIryoKikan.PerformLayout();
			this.grpKaihi.ResumeLayout(false);
			this.grpKaihi.PerformLayout();
			this.grpKaiin.ResumeLayout(false);
			this.grpKaiin.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox grpIryoKikan;
		private System.Windows.Forms.Button btnIryoKikan;
		private System.Windows.Forms.CheckBox chkClearIryoKikan;
		private UcFileSelect iFileIryoKikan;
		private System.Windows.Forms.GroupBox grpKaihi;
		private System.Windows.Forms.Button btnKaihi;
		private System.Windows.Forms.CheckBox chkClearKaihi;
		private UcFileSelect iFileKaihi;
		private System.Windows.Forms.GroupBox grpKaiin;
		private System.Windows.Forms.Button btnKaiin;
		private System.Windows.Forms.CheckBox chkClearKaiin;
		private System.Windows.Forms.CheckBox chkFillIryoKikan;
		private UcFileSelect iFileKaiin;
		private System.Windows.Forms.Button btnClose;
	}
}
