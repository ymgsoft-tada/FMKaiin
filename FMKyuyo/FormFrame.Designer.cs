namespace App
{
	partial class FormFrame
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFrame));
			this.pnlMask = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// pnlMask
			// 
			this.pnlMask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlMask.BackColor = System.Drawing.Color.WhiteSmoke;
			this.pnlMask.Location = new System.Drawing.Point(0, 0);
			this.pnlMask.Name = "pnlMask";
			this.pnlMask.Size = new System.Drawing.Size(2043, 1132);
			this.pnlMask.TabIndex = 0;
			// 
			// FormFrame
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(1600, 900);
			this.Controls.Add(this.pnlMask);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormFrame";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "FormFlame";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlMask;
	}
}