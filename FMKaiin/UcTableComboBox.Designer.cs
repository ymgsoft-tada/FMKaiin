namespace App
{
	partial class UcTableComboBox
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
			this.dropDownButton1 = new GrapeCity.Win.Editors.DropDownButton();
			this.cmb = new GControlGcComboBoxEx.GcComboBoxEx(this.components);
			this.icoInfo = new App.Controls.IconBox();
			((System.ComponentModel.ISupportInitialize)(this.cmb)).BeginInit();
			this.SuspendLayout();
			// 
			// dropDownButton1
			// 
			this.dropDownButton1.Name = "dropDownButton1";
			// 
			// cmb
			// 
			this.cmb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cmb.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft;
			this.cmb.DropDown.AllowResize = false;
			this.cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmb.ExCompareContent = null;
			this.cmb.ExCompareValue = null;
			this.cmb.ExDataSource = null;
			this.cmb.ExFocusHighlight = true;
			this.cmb.FlatStyle = GrapeCity.Win.Editors.FlatStyleEx.Flat;
			this.cmb.HighlightText = true;
			this.cmb.ListHeaderPane.Height = 19;
			this.cmb.ListHeaderPane.Visible = false;
			this.cmb.Location = new System.Drawing.Point(0, 0);
			this.cmb.Name = "cmb";
			this.cmb.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.cmb.Size = new System.Drawing.Size(100, 20);
			this.cmb.TabIndex = 0;
			// 
			// icoInfo
			//
			this.icoInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(110)))), ((int)(((byte)(190)))));
			this.icoInfo.IconKind = App.Controls.MenuIconKind.Info;
			this.icoInfo.Location = new System.Drawing.Point(100, 0);
			this.icoInfo.Name = "icoInfo";
			this.icoInfo.Padding = new System.Windows.Forms.Padding(2);
			this.icoInfo.Size = new System.Drawing.Size(20, 20);
			this.icoInfo.TabIndex = 1;
			this.icoInfo.TabStop = false;
			// 
			// UcTableComboBox
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.icoInfo);
			this.Controls.Add(this.cmb);
			this.ForeColor = System.Drawing.Color.Transparent;
			this.Name = "UcTableComboBox";
			this.Size = new System.Drawing.Size(120, 20);
			((System.ComponentModel.ISupportInitialize)(this.cmb)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private GControlGcComboBoxEx.GcComboBoxEx cmb;
		private GrapeCity.Win.Editors.DropDownButton dropDownButton1;
		private App.Controls.IconBox icoInfo;
	}
}
