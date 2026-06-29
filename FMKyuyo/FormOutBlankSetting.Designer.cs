namespace App
{
	partial class FormOutBlankSetting
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

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOutBlankSetting));
			this.hScroll = new System.Windows.Forms.HScrollBar();
			this.vScroll = new System.Windows.Forms.VScrollBar();
			this.picPaper = new System.Windows.Forms.PictureBox();
			this.lblH = new System.Windows.Forms.Label();
			this.lblV = new System.Windows.Forms.Label();
			this.butClear = new System.Windows.Forms.Button();
			this.butOK = new System.Windows.Forms.Button();
			this.butCancel = new System.Windows.Forms.Button();
			this.imgList1 = new System.Windows.Forms.ImageList(this.components);
			this.imgList2 = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.picPaper)).BeginInit();
			this.SuspendLayout();
			// 
			// hScroll
			// 
			this.hScroll.Location = new System.Drawing.Point(33, 25);
			this.hScroll.Maximum = 109;
			this.hScroll.Name = "hScroll";
			this.hScroll.Size = new System.Drawing.Size(200, 16);
			this.hScroll.TabIndex = 0;
			// 
			// vScroll
			// 
			this.vScroll.Location = new System.Drawing.Point(17, 41);
			this.vScroll.Maximum = 109;
			this.vScroll.Name = "vScroll";
			this.vScroll.Size = new System.Drawing.Size(16, 200);
			this.vScroll.TabIndex = 1;
			// 
			// picPaper
			// 
			this.picPaper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picPaper.Location = new System.Drawing.Point(33, 41);
			this.picPaper.Name = "picPaper";
			this.picPaper.Size = new System.Drawing.Size(200, 200);
			this.picPaper.TabIndex = 2;
			this.picPaper.TabStop = false;
			// 
			// lblH
			// 
			this.lblH.AutoSize = true;
			this.lblH.Location = new System.Drawing.Point(236, 27);
			this.lblH.Name = "lblH";
			this.lblH.Size = new System.Drawing.Size(53, 12);
			this.lblH.TabIndex = 3;
			this.lblH.Text = "横 0.0mm";
			// 
			// lblV
			// 
			this.lblV.AutoSize = true;
			this.lblV.Location = new System.Drawing.Point(15, 246);
			this.lblV.Name = "lblV";
			this.lblV.Size = new System.Drawing.Size(53, 12);
			this.lblV.TabIndex = 4;
			this.lblV.Text = "縦 0.0mm";
			// 
			// butClear
			// 
			this.butClear.Location = new System.Drawing.Point(259, 139);
			this.butClear.Name = "butClear";
			this.butClear.Size = new System.Drawing.Size(87, 23);
			this.butClear.TabIndex = 5;
			this.butClear.Text = "初期値に戻す";
			this.butClear.UseVisualStyleBackColor = true;
			// 
			// butOK
			// 
			this.butOK.Location = new System.Drawing.Point(259, 190);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(87, 23);
			this.butOK.TabIndex = 6;
			this.butOK.Text = "OK";
			this.butOK.UseVisualStyleBackColor = true;
			// 
			// butCancel
			// 
			this.butCancel.Location = new System.Drawing.Point(259, 218);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(87, 23);
			this.butCancel.TabIndex = 7;
			this.butCancel.Text = "キャンセル";
			this.butCancel.UseVisualStyleBackColor = true;
			// 
			// imgList1
			// 
			this.imgList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList1.ImageStream")));
			this.imgList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imgList1.Images.SetKeyName(0, "MarginSet0");
			this.imgList1.Images.SetKeyName(1, "MarginSet2");
			// 
			// imgList2
			// 
			this.imgList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList2.ImageStream")));
			this.imgList2.TransparentColor = System.Drawing.Color.Transparent;
			this.imgList2.Images.SetKeyName(0, "MarginSet1");
			// 
			// OutBlankSetting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.WhiteSmoke;
			this.ClientSize = new System.Drawing.Size(365, 285);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butClear);
			this.Controls.Add(this.lblV);
			this.Controls.Add(this.lblH);
			this.Controls.Add(this.picPaper);
			this.Controls.Add(this.vScroll);
			this.Controls.Add(this.hScroll);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OutBlankSetting";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "余白設定";
			this.Load += new System.EventHandler(this.Form_Load);
			((System.ComponentModel.ISupportInitialize)(this.picPaper)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.HScrollBar hScroll;
		private System.Windows.Forms.VScrollBar vScroll;
		private System.Windows.Forms.PictureBox picPaper;
		private System.Windows.Forms.Label lblH;
		private System.Windows.Forms.Label lblV;
		private System.Windows.Forms.Button butClear;
		private System.Windows.Forms.Button butOK;
		private System.Windows.Forms.Button butCancel;
		private System.Windows.Forms.ImageList imgList1;
		private System.Windows.Forms.ImageList imgList2;
	}
}