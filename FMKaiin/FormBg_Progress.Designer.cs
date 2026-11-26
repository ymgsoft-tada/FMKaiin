namespace App
{
	partial class FormBg_Progress
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
			this.bgWorker = new System.ComponentModel.BackgroundWorker();
			this.secBody = new ComponentSUI.SectionPanel();
			this.IconIndicator = new ComponentSUI.SBusyIndicator();
			this.LblInfo = new ComponentSUI.SLabel();
			this.ProgressBar = new ComponentSUI.SProgressBar();
			this.secBody.SuspendLayout();
			this.SuspendLayout();
			//
			// bgWorker
			//
			this.bgWorker.WorkerReportsProgress = true;
			this.bgWorker.WorkerSupportsCancellation = true;
			this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
			this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			//
			// secBody
			//
			this.secBody.BackColor = System.Drawing.Color.Transparent;
			this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
			this.secBody.Controls.Add(this.IconIndicator);
			this.secBody.Controls.Add(this.LblInfo);
			this.secBody.Controls.Add(this.ProgressBar);
			this.secBody.CornerRadius = 8;
			this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
			this.secBody.HeaderHeight = 0;
			this.secBody.Location = new System.Drawing.Point(10, 10);
			this.secBody.Name = "secBody";
			this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
			this.secBody.ShadowSize = 6;
			this.secBody.Size = new System.Drawing.Size(360, 82);
			this.secBody.TabIndex = 0;
			//
			// IconIndicator
			//
			this.IconIndicator.Location = new System.Drawing.Point(20, 20);
			this.IconIndicator.Name = "IconIndicator";
			this.IconIndicator.Size = new System.Drawing.Size(36, 36);
			this.IconIndicator.TabIndex = 0;
			//
			// LblInfo
			//
			this.LblInfo.BackColor = System.Drawing.Color.Transparent;
			this.LblInfo.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.LblInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(62)))));
			this.LblInfo.Location = new System.Drawing.Point(72, 16);
			this.LblInfo.Name = "LblInfo";
			this.LblInfo.Size = new System.Drawing.Size(268, 24);
			this.LblInfo.TabIndex = 1;
			//
			// ProgressBar
			//
			this.ProgressBar.Location = new System.Drawing.Point(72, 48);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(268, 8);
			this.ProgressBar.TabIndex = 2;
			//
			// FormBg_Progress
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(380, 102);
			this.Controls.Add(this.secBody);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBg_Progress";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "しばらくお待ちください";
			this.secBody.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		/// <summary>
		/// バックグウンドタスク実行コントロール
		/// </summary>
		protected System.ComponentModel.BackgroundWorker bgWorker;
		private ComponentSUI.SectionPanel secBody;
		/// <summary>
		/// メッセージ
		/// </summary>
		public ComponentSUI.SLabel LblInfo;
		/// <summary>
		/// 進み具合のバー
		/// </summary>
		public ComponentSUI.SProgressBar ProgressBar;
		/// <summary>
		/// 処理中であることを示す、回る輪
		/// </summary>
		public ComponentSUI.SBusyIndicator IconIndicator;
	}
}