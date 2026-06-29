namespace App
{
	partial class UcDate
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
			this.components = new System.ComponentModel.Container();
			this.date = new GControlGcDateEx.GcDateEx(this.components);
			this.dropDownButton1 = new GrapeCity.Win.Editors.DropDownButton();
			((System.ComponentModel.ISupportInitialize)(this.date)).BeginInit();
			this.SuspendLayout();
			// 
			// date
			// 
			this.date.ContentAlignment = System.Drawing.ContentAlignment.BottomCenter;
			this.date.DropDownCalendar.MaxDate = new System.DateTime(9998, 12, 31, 23, 59, 59, 0);
			this.date.DropDownCalendar.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
			this.date.ExFocusHighlight = true;
			this.date.ExMaxDate = new System.DateTime(9998, 12, 31, 23, 59, 59, 0);
			this.date.ExMinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
			this.date.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.date.Location = new System.Drawing.Point(0, 0);
			this.date.Name = "date";
			this.date.Size = new System.Drawing.Size(150, 22);
			this.date.TabIndex = 0;
			this.date.Value = new System.DateTime(2010, 1, 19, 9, 21, 27, 0);
			// 
			// dropDownButton1
			// 
			this.dropDownButton1.Name = "dropDownButton1";
			// 
			// UcDate
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this.date);
			this.Name = "UcDate";
			this.Size = new System.Drawing.Size(150, 22);
			((System.ComponentModel.ISupportInitialize)(this.date)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private GControlGcDateEx.GcDateEx date;
		private GrapeCity.Win.Editors.DropDownButton dropDownButton1;
	}
}
