namespace App
{
	partial class UcFileSelect
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
			this.butSelect = new System.Windows.Forms.Button();
			this.txtSelect = new GControlGcTextBoxEx.GcTextBoxEx();
			((System.ComponentModel.ISupportInitialize)(this.txtSelect)).BeginInit();
			this.SuspendLayout();
			// 
			// butSelect
			// 
			this.butSelect.Location = new System.Drawing.Point(348, -1);
			this.butSelect.Name = "butSelect";
			this.butSelect.Size = new System.Drawing.Size(39, 26);
			this.butSelect.TabIndex = 1;
			this.butSelect.Text = "...";
			this.butSelect.UseVisualStyleBackColor = true;
			// 
			// txtSelect
			// 
			this.txtSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSelect.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.txtSelect.DisabledForeColor = System.Drawing.Color.Black;
			this.txtSelect.Enabled = false;
			this.txtSelect.ExFocusHighlight = true;
			this.txtSelect.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.txtSelect.ImeStr = "";
			this.txtSelect.Location = new System.Drawing.Point(0, 0);
			this.txtSelect.Name = "txtSelect";
			this.txtSelect.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.txtSelect.Size = new System.Drawing.Size(342, 25);
			this.txtSelect.TabIndex = 2;
			// 
			// UcFileSelect
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.txtSelect);
			this.Controls.Add(this.butSelect);
			this.Name = "UcFileSelect";
			this.Size = new System.Drawing.Size(393, 26);
			((System.ComponentModel.ISupportInitialize)(this.txtSelect)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button butSelect;
		private GControlGcTextBoxEx.GcTextBoxEx txtSelect;
	}
}
