namespace App
{
	partial class UcImgButton
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
			this.pBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pBox)).BeginInit();
			this.SuspendLayout();
			// 
			// pLbl
			// 
			this.pLbl.BackColor = System.Drawing.Color.Transparent;
			this.pLbl.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.pLbl.Location = new System.Drawing.Point(0, 64);
			this.pLbl.Name = "pLbl";
			this.pLbl.Size = new System.Drawing.Size(64, 32);
			this.pLbl.TabIndex = 1;
			this.pLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pBox
			// 
			this.pBox.BackColor = System.Drawing.Color.Transparent;
			this.pBox.Location = new System.Drawing.Point(0, 0);
			this.pBox.Name = "pBox";
			this.pBox.Size = new System.Drawing.Size(64, 64);
			this.pBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pBox.TabIndex = 0;
			this.pBox.TabStop = false;
			// 
			// UcImgButton
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.pLbl);
			this.Controls.Add(this.pBox);
			this.Name = "UcImgButton";
			this.Size = new System.Drawing.Size(64, 96);
			((System.ComponentModel.ISupportInitialize)(this.pBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pBox;
		private System.Windows.Forms.Label pLbl;
	}
}
