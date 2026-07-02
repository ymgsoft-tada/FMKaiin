namespace App
{
	partial class UcImgButton2
	{
		/// <summary> 
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナで生成されたコード

		/// <summary> 
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.pLbl = new System.Windows.Forms.Label();
			this.pIcon = new System.Windows.Forms.PictureBox();
			this.pBox = new System.Windows.Forms.PictureBox();
			this.pShadow = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pShadow)).BeginInit();
			this.SuspendLayout();
			// 
			// pLbl
			// 
			this.pLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pLbl.BackColor = System.Drawing.Color.Transparent;
			this.pLbl.Font = new System.Drawing.Font("HGP創英角ｺﾞｼｯｸUB", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.pLbl.Location = new System.Drawing.Point(0, 123);
			this.pLbl.Margin = new System.Windows.Forms.Padding(0);
			this.pLbl.Name = "pLbl";
			this.pLbl.Size = new System.Drawing.Size(59, 45);
			this.pLbl.TabIndex = 1;
			this.pLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pIcon
			// 
			this.pIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pIcon.Location = new System.Drawing.Point(0, 0);
			this.pIcon.Margin = new System.Windows.Forms.Padding(0);
			this.pIcon.Name = "pIcon";
			this.pIcon.Size = new System.Drawing.Size(167, 158);
			this.pIcon.TabIndex = 2;
			this.pIcon.TabStop = false;
			// 
			// pBox
			// 
			this.pBox.BackColor = System.Drawing.Color.Transparent;
			this.pBox.Location = new System.Drawing.Point(0, 0);
			this.pBox.Name = "pBox";
			this.pBox.Size = new System.Drawing.Size(167, 158);
			this.pBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pBox.TabIndex = 0;
			this.pBox.TabStop = false;
			// 
			// pShadow
			// 
			this.pShadow.BackColor = System.Drawing.Color.Transparent;
			this.pShadow.Location = new System.Drawing.Point(-13, -12);
			this.pShadow.Name = "pShadow";
			this.pShadow.Size = new System.Drawing.Size(158, 155);
			this.pShadow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pShadow.TabIndex = 3;
			this.pShadow.TabStop = false;
			// 
			// UcImgButton2
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.pLbl);
			this.Controls.Add(this.pIcon);
			this.Controls.Add(this.pBox);
			this.Controls.Add(this.pShadow);
			this.Name = "UcImgButton2";
			this.Size = new System.Drawing.Size(167, 158);
			((System.ComponentModel.ISupportInitialize)(this.pIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pShadow)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pBox;
		private System.Windows.Forms.Label pLbl;
        private System.Windows.Forms.PictureBox pIcon;
		private System.Windows.Forms.PictureBox pShadow;
	}
}
