
namespace App
{
	partial class FormMain
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
			if (disposing && ( components != null ))
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
			this.components = new System.ComponentModel.Container();
			this.panel4 = new System.Windows.Forms.Panel();
			this.ibRestore = new App.UcImgButton();
			this.ibBackup = new App.UcImgButton();
			this.ibRemote = new App.UcImgButton();
			this.ibLogout = new App.UcImgButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblVer = new System.Windows.Forms.Label();
			this.iTantosha = new GControlGcTextBoxEx.GcTextBoxEx();
			this.label3 = new System.Windows.Forms.Label();
			this.panel7 = new System.Windows.Forms.Panel();
			this.ibBankCode = new App.UcImgButton();
			this.ibIryoKikan = new App.UcImgButton();
			this.ibKaihi = new App.UcImgButton();
			this.ibTantosha = new App.UcImgButton();
			this.ibShinryoKamoku = new App.UcImgButton();
			this.ibIkaiKaihi = new App.UcImgButton();
			this.ibBasic = new App.UcImgButton();
			this.panel8 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.ibFB = new App.UcImgButton();
			this.ibLabel = new App.UcImgButton();
			this.ibSeikyusho = new App.UcImgButton();
			this.ibHikiotoshimeisai = new App.UcImgButton();
			this.ibHikiotoshiData = new App.UcImgButton();
			this.panel6 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel9 = new System.Windows.Forms.Panel();
			this.ibKaiinjohoToroku = new App.UcImgButton();
			this.ibOutKaihi = new App.UcImgButton();
			this.ibOutKaiinjoho = new App.UcImgButton();
			this.panel10 = new System.Windows.Forms.Panel();
			this.gcShortcut1 = new GrapeCity.Win.Editors.GcShortcut(this.components);
			this.button3 = new System.Windows.Forms.Button();
			this.btnMasterImport = new System.Windows.Forms.Button();
			this.btnRep2 = new System.Windows.Forms.Button();
			this.btnRep1 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.panel4.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.iTantosha)).BeginInit();
			this.panel7.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel9.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
			this.panel4.Controls.Add(this.ibRestore);
			this.panel4.Controls.Add(this.ibBackup);
			this.panel4.Controls.Add(this.ibRemote);
			this.panel4.Controls.Add(this.ibLogout);
			this.panel4.Location = new System.Drawing.Point(1099, 91);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(103, 620);
			this.panel4.TabIndex = 5;
			// 
			// ibRestore
			// 
			this.ibRestore.BackColor = System.Drawing.Color.Transparent;
			this.ibRestore.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibRestore.ImgDefault = global::App.Properties.Resources.button_normal_DataFukkyu;
			this.ibRestore.ImgDisabled = null;
			this.ibRestore.ImgMouseOver = global::App.Properties.Resources.button_hover_datafukkyu;
			this.ibRestore.ImgMousePush = global::App.Properties.Resources.button_push_DataFukkyu;
			this.ibRestore.ImgPullDown = null;
			this.ibRestore.LabelFontSize = 9F;
			this.ibRestore.Location = new System.Drawing.Point(9, 294);
			this.ibRestore.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibRestore.Name = "ibRestore";
			this.ibRestore.PullDown = false;
			this.ibRestore.Size = new System.Drawing.Size(90, 85);
			this.ibRestore.TabIndex = 1;
			this.ibRestore.TextButton = "データ復元";
			this.ibRestore.ToolTip = null;
			this.ibRestore.ToolTipCaption = "";
			this.ibRestore.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibRestore.ToolTipText = "";
			// 
			// ibBackup
			// 
			this.ibBackup.BackColor = System.Drawing.Color.Transparent;
			this.ibBackup.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibBackup.ImgDefault = global::App.Properties.Resources.button_normal_DataHozon;
			this.ibBackup.ImgDisabled = null;
			this.ibBackup.ImgMouseOver = global::App.Properties.Resources.button_hover_datahozon;
			this.ibBackup.ImgMousePush = global::App.Properties.Resources.button_push_DataHozon;
			this.ibBackup.ImgPullDown = null;
			this.ibBackup.LabelFontSize = 9F;
			this.ibBackup.Location = new System.Drawing.Point(9, 199);
			this.ibBackup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibBackup.Name = "ibBackup";
			this.ibBackup.PullDown = false;
			this.ibBackup.Size = new System.Drawing.Size(90, 85);
			this.ibBackup.TabIndex = 0;
			this.ibBackup.TextButton = "データ保存";
			this.ibBackup.ToolTip = null;
			this.ibBackup.ToolTipCaption = "";
			this.ibBackup.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibBackup.ToolTipText = "";
			// 
			// ibRemote
			// 
			this.ibRemote.BackColor = System.Drawing.Color.Transparent;
			this.ibRemote.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibRemote.ImgDefault = global::App.Properties.Resources.button_normal_Remote;
			this.ibRemote.ImgDisabled = null;
			this.ibRemote.ImgMouseOver = global::App.Properties.Resources.button_hover_remote;
			this.ibRemote.ImgMousePush = global::App.Properties.Resources.button_push_Remote;
			this.ibRemote.ImgPullDown = null;
			this.ibRemote.LabelFontSize = 9F;
			this.ibRemote.Location = new System.Drawing.Point(9, 410);
			this.ibRemote.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibRemote.Name = "ibRemote";
			this.ibRemote.PullDown = false;
			this.ibRemote.Size = new System.Drawing.Size(90, 85);
			this.ibRemote.TabIndex = 2;
			this.ibRemote.TextButton = "遠隔サポート";
			this.ibRemote.ToolTip = null;
			this.ibRemote.ToolTipCaption = "";
			this.ibRemote.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibRemote.ToolTipText = "";
			// 
			// ibLogout
			// 
			this.ibLogout.BackColor = System.Drawing.Color.Transparent;
			this.ibLogout.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibLogout.ImgDefault = global::App.Properties.Resources.button_normal_Logout;
			this.ibLogout.ImgDisabled = null;
			this.ibLogout.ImgMouseOver = global::App.Properties.Resources.button_hover_logout;
			this.ibLogout.ImgMousePush = global::App.Properties.Resources.button_push_logout;
			this.ibLogout.ImgPullDown = null;
			this.ibLogout.LabelFontSize = 9F;
			this.ibLogout.Location = new System.Drawing.Point(9, 505);
			this.ibLogout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibLogout.Name = "ibLogout";
			this.ibLogout.PullDown = false;
			this.ibLogout.Size = new System.Drawing.Size(90, 85);
			this.ibLogout.TabIndex = 3;
			this.ibLogout.TextButton = "ログアウト";
			this.ibLogout.ToolTip = null;
			this.ibLogout.ToolTipCaption = "";
			this.ibLogout.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibLogout.ToolTipText = "";
			// 
			// panel1
			// 
			this.panel1.BackgroundImage = global::App.Properties.Resources.ribbon;
			this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.panel1.Controls.Add(this.lblVer);
			this.panel1.Controls.Add(this.iTantosha);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Location = new System.Drawing.Point(1, 1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1201, 98);
			this.panel1.TabIndex = 6;
			// 
			// lblVer
			// 
			this.lblVer.BackColor = System.Drawing.Color.Transparent;
			this.lblVer.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblVer.ForeColor = System.Drawing.Color.Black;
			this.lblVer.Location = new System.Drawing.Point(469, 54);
			this.lblVer.Name = "lblVer";
			this.lblVer.Size = new System.Drawing.Size(163, 22);
			this.lblVer.TabIndex = 103;
			this.lblVer.Text = "Version1.0.0";
			this.lblVer.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// iTantosha
			// 
			this.iTantosha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iTantosha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iTantosha.ContentAlignment = System.Drawing.ContentAlignment.BottomLeft;
			this.iTantosha.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
			this.iTantosha.DisabledForeColor = System.Drawing.Color.Black;
			this.iTantosha.Enabled = false;
			this.iTantosha.ExFocusHighlight = true;
			this.iTantosha.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.iTantosha.ImeStr = "";
			this.iTantosha.Location = new System.Drawing.Point(981, 54);
			this.iTantosha.Name = "iTantosha";
			this.iTantosha.SingleBorderColor = System.Drawing.Color.DarkGray;
			this.iTantosha.Size = new System.Drawing.Size(194, 24);
			this.iTantosha.TabIndex = 99;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.ForeColor = System.Drawing.Color.Black;
			this.label3.Location = new System.Drawing.Point(905, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 23);
			this.label3.TabIndex = 100;
			this.label3.Text = "ログイン";
			// 
			// panel7
			// 
			this.panel7.Controls.Add(this.ibBankCode);
			this.panel7.Controls.Add(this.ibIryoKikan);
			this.panel7.Controls.Add(this.ibKaihi);
			this.panel7.Controls.Add(this.ibTantosha);
			this.panel7.Controls.Add(this.ibShinryoKamoku);
			this.panel7.Controls.Add(this.ibIkaiKaihi);
			this.panel7.Controls.Add(this.ibBasic);
			this.panel7.Controls.Add(this.panel8);
			this.panel7.Location = new System.Drawing.Point(806, 159);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(253, 536);
			this.panel7.TabIndex = 10;
			// 
			// ibBankCode
			// 
			this.ibBankCode.BackColor = System.Drawing.Color.Transparent;
			this.ibBankCode.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibBankCode.ImgDefault = global::App.Properties.Resources.button_normal_1Staff;
			this.ibBankCode.ImgDisabled = null;
			this.ibBankCode.ImgMouseOver = null;
			this.ibBankCode.ImgMousePush = null;
			this.ibBankCode.ImgPullDown = null;
			this.ibBankCode.LabelFontSize = 9F;
			this.ibBankCode.Location = new System.Drawing.Point(9, 151);
			this.ibBankCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibBankCode.Name = "ibBankCode";
			this.ibBankCode.PullDown = false;
			this.ibBankCode.Size = new System.Drawing.Size(116, 95);
			this.ibBankCode.TabIndex = 7;
			this.ibBankCode.TextButton = "銀行マスタ";
			this.ibBankCode.ToolTip = null;
			this.ibBankCode.ToolTipCaption = "";
			this.ibBankCode.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibBankCode.ToolTipText = "";
			// 
			// ibIryoKikan
			// 
			this.ibIryoKikan.BackColor = System.Drawing.Color.Transparent;
			this.ibIryoKikan.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibIryoKikan.ImgDefault = global::App.Properties.Resources.button_normal_1Staff;
			this.ibIryoKikan.ImgDisabled = null;
			this.ibIryoKikan.ImgMouseOver = null;
			this.ibIryoKikan.ImgMousePush = null;
			this.ibIryoKikan.ImgPullDown = null;
			this.ibIryoKikan.LabelFontSize = 9F;
			this.ibIryoKikan.Location = new System.Drawing.Point(20, 361);
			this.ibIryoKikan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibIryoKikan.Name = "ibIryoKikan";
			this.ibIryoKikan.PullDown = false;
			this.ibIryoKikan.Size = new System.Drawing.Size(100, 95);
			this.ibIryoKikan.TabIndex = 3;
			this.ibIryoKikan.TextButton = "医療機関マスタ";
			this.ibIryoKikan.ToolTip = null;
			this.ibIryoKikan.ToolTipCaption = "";
			this.ibIryoKikan.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibIryoKikan.ToolTipText = "";
			// 
			// ibKaihi
			// 
			this.ibKaihi.BackColor = System.Drawing.Color.Transparent;
			this.ibKaihi.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibKaihi.ImgDefault = global::App.Properties.Resources.button_normal_1Staff;
			this.ibKaihi.ImgDisabled = null;
			this.ibKaihi.ImgMouseOver = null;
			this.ibKaihi.ImgMousePush = null;
			this.ibKaihi.ImgPullDown = null;
			this.ibKaihi.LabelFontSize = 9F;
			this.ibKaihi.Location = new System.Drawing.Point(133, 151);
			this.ibKaihi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibKaihi.Name = "ibKaihi";
			this.ibKaihi.PullDown = false;
			this.ibKaihi.Size = new System.Drawing.Size(100, 95);
			this.ibKaihi.TabIndex = 5;
			this.ibKaihi.TextButton = "会費マスタ";
			this.ibKaihi.ToolTip = null;
			this.ibKaihi.ToolTipCaption = "";
			this.ibKaihi.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibKaihi.ToolTipText = "";
			// 
			// ibTantosha
			// 
			this.ibTantosha.BackColor = System.Drawing.Color.Transparent;
			this.ibTantosha.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibTantosha.ImgDefault = global::App.Properties.Resources.button_normal_1Staff;
			this.ibTantosha.ImgDisabled = null;
			this.ibTantosha.ImgMouseOver = global::App.Properties.Resources.button_hover_1Staff;
			this.ibTantosha.ImgMousePush = global::App.Properties.Resources.button_push_1Staff;
			this.ibTantosha.ImgPullDown = null;
			this.ibTantosha.LabelFontSize = 9F;
			this.ibTantosha.Location = new System.Drawing.Point(133, 46);
			this.ibTantosha.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibTantosha.Name = "ibTantosha";
			this.ibTantosha.PullDown = false;
			this.ibTantosha.Size = new System.Drawing.Size(100, 95);
			this.ibTantosha.TabIndex = 4;
			this.ibTantosha.TextButton = "ユーザー";
			this.ibTantosha.ToolTip = null;
			this.ibTantosha.ToolTipCaption = "";
			this.ibTantosha.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibTantosha.ToolTipText = "";
			// 
			// ibShinryoKamoku
			// 
			this.ibShinryoKamoku.BackColor = System.Drawing.Color.Transparent;
			this.ibShinryoKamoku.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibShinryoKamoku.ImgDefault = global::App.Properties.Resources.button_normal_1Staff;
			this.ibShinryoKamoku.ImgDisabled = null;
			this.ibShinryoKamoku.ImgMouseOver = null;
			this.ibShinryoKamoku.ImgMousePush = null;
			this.ibShinryoKamoku.ImgPullDown = null;
			this.ibShinryoKamoku.LabelFontSize = 9F;
			this.ibShinryoKamoku.Location = new System.Drawing.Point(20, 256);
			this.ibShinryoKamoku.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibShinryoKamoku.Name = "ibShinryoKamoku";
			this.ibShinryoKamoku.PullDown = false;
			this.ibShinryoKamoku.Size = new System.Drawing.Size(100, 95);
			this.ibShinryoKamoku.TabIndex = 2;
			this.ibShinryoKamoku.TextButton = "診療科目マスタ";
			this.ibShinryoKamoku.ToolTip = null;
			this.ibShinryoKamoku.ToolTipCaption = "";
			this.ibShinryoKamoku.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibShinryoKamoku.ToolTipText = "";
			// 
			// ibIkaiKaihi
			// 
			this.ibIkaiKaihi.BackColor = System.Drawing.Color.Transparent;
			this.ibIkaiKaihi.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibIkaiKaihi.ImgDefault = global::App.Properties.Resources.button_normal_1Staff;
			this.ibIkaiKaihi.ImgDisabled = null;
			this.ibIkaiKaihi.ImgMouseOver = null;
			this.ibIkaiKaihi.ImgMousePush = null;
			this.ibIkaiKaihi.ImgPullDown = null;
			this.ibIkaiKaihi.LabelFontSize = 9F;
			this.ibIkaiKaihi.Location = new System.Drawing.Point(133, 361);
			this.ibIkaiKaihi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibIkaiKaihi.Name = "ibIkaiKaihi";
			this.ibIkaiKaihi.PullDown = false;
			this.ibIkaiKaihi.Size = new System.Drawing.Size(100, 95);
			this.ibIkaiKaihi.TabIndex = 1;
			this.ibIkaiKaihi.TextButton = "医会会費マスタ";
			this.ibIkaiKaihi.ToolTip = null;
			this.ibIkaiKaihi.ToolTipCaption = "";
			this.ibIkaiKaihi.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibIkaiKaihi.ToolTipText = "";
			// 
			// ibBasic
			// 
			this.ibBasic.BackColor = System.Drawing.Color.Transparent;
			this.ibBasic.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibBasic.ImgDefault = global::App.Properties.Resources.button_normal_1Staff;
			this.ibBasic.ImgDisabled = null;
			this.ibBasic.ImgMouseOver = null;
			this.ibBasic.ImgMousePush = null;
			this.ibBasic.ImgPullDown = null;
			this.ibBasic.LabelFontSize = 9F;
			this.ibBasic.Location = new System.Drawing.Point(20, 46);
			this.ibBasic.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibBasic.Name = "ibBasic";
			this.ibBasic.PullDown = false;
			this.ibBasic.Size = new System.Drawing.Size(100, 95);
			this.ibBasic.TabIndex = 0;
			this.ibBasic.TextButton = "基本情報";
			this.ibBasic.ToolTip = null;
			this.ibBasic.ToolTipCaption = "";
			this.ibBasic.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibBasic.ToolTipText = "";
			// 
			// panel8
			// 
			this.panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.panel8.Location = new System.Drawing.Point(3, 3);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(245, 35);
			this.panel8.TabIndex = 3;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.ibFB);
			this.panel3.Controls.Add(this.ibLabel);
			this.panel3.Controls.Add(this.ibSeikyusho);
			this.panel3.Controls.Add(this.ibHikiotoshimeisai);
			this.panel3.Controls.Add(this.ibHikiotoshiData);
			this.panel3.Controls.Add(this.panel6);
			this.panel3.Location = new System.Drawing.Point(270, 160);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(486, 492);
			this.panel3.TabIndex = 7;
			// 
			// ibFB
			// 
			this.ibFB.BackColor = System.Drawing.Color.Transparent;
			this.ibFB.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibFB.ImgDefault = global::App.Properties.Resources.button_normal_1Staff;
			this.ibFB.ImgDisabled = null;
			this.ibFB.ImgMouseOver = null;
			this.ibFB.ImgMousePush = null;
			this.ibFB.ImgPullDown = null;
			this.ibFB.LabelFontSize = 9F;
			this.ibFB.Location = new System.Drawing.Point(101, 151);
			this.ibFB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibFB.Name = "ibFB";
			this.ibFB.PullDown = false;
			this.ibFB.Size = new System.Drawing.Size(100, 95);
			this.ibFB.TabIndex = 5;
			this.ibFB.TextButton = "FBデータ作成";
			this.ibFB.ToolTip = null;
			this.ibFB.ToolTipCaption = "";
			this.ibFB.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibFB.ToolTipText = "";
			// 
			// ibLabel
			// 
			this.ibLabel.BackColor = System.Drawing.Color.Transparent;
			this.ibLabel.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibLabel.ImgDefault = global::App.Properties.Resources.button_normal_1Staff;
			this.ibLabel.ImgDisabled = null;
			this.ibLabel.ImgMouseOver = null;
			this.ibLabel.ImgMousePush = null;
			this.ibLabel.ImgPullDown = null;
			this.ibLabel.LabelFontSize = 9F;
			this.ibLabel.Location = new System.Drawing.Point(288, 255);
			this.ibLabel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibLabel.Name = "ibLabel";
			this.ibLabel.PullDown = false;
			this.ibLabel.Size = new System.Drawing.Size(100, 95);
			this.ibLabel.TabIndex = 8;
			this.ibLabel.TextButton = "ラベル印刷";
			this.ibLabel.ToolTip = null;
			this.ibLabel.ToolTipCaption = "";
			this.ibLabel.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibLabel.ToolTipText = "";
			// 
			// ibSeikyusho
			// 
			this.ibSeikyusho.BackColor = System.Drawing.Color.Transparent;
			this.ibSeikyusho.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibSeikyusho.ImgDefault = global::App.Properties.Resources.button_normal_1Staff;
			this.ibSeikyusho.ImgDisabled = null;
			this.ibSeikyusho.ImgMouseOver = null;
			this.ibSeikyusho.ImgMousePush = null;
			this.ibSeikyusho.ImgPullDown = null;
			this.ibSeikyusho.LabelFontSize = 9F;
			this.ibSeikyusho.Location = new System.Drawing.Point(288, 150);
			this.ibSeikyusho.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibSeikyusho.Name = "ibSeikyusho";
			this.ibSeikyusho.PullDown = false;
			this.ibSeikyusho.Size = new System.Drawing.Size(100, 95);
			this.ibSeikyusho.TabIndex = 7;
			this.ibSeikyusho.TextButton = "請求書";
			this.ibSeikyusho.ToolTip = null;
			this.ibSeikyusho.ToolTipCaption = "";
			this.ibSeikyusho.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibSeikyusho.ToolTipText = "";
			// 
			// ibHikiotoshimeisai
			// 
			this.ibHikiotoshimeisai.BackColor = System.Drawing.Color.Transparent;
			this.ibHikiotoshimeisai.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibHikiotoshimeisai.ImgDefault = global::App.Properties.Resources.button_normal_1Staff;
			this.ibHikiotoshimeisai.ImgDisabled = null;
			this.ibHikiotoshimeisai.ImgMouseOver = null;
			this.ibHikiotoshimeisai.ImgMousePush = null;
			this.ibHikiotoshimeisai.ImgPullDown = null;
			this.ibHikiotoshimeisai.LabelFontSize = 9F;
			this.ibHikiotoshimeisai.Location = new System.Drawing.Point(288, 45);
			this.ibHikiotoshimeisai.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibHikiotoshimeisai.Name = "ibHikiotoshimeisai";
			this.ibHikiotoshimeisai.PullDown = false;
			this.ibHikiotoshimeisai.Size = new System.Drawing.Size(100, 95);
			this.ibHikiotoshimeisai.TabIndex = 6;
			this.ibHikiotoshimeisai.TextButton = "引落明細表";
			this.ibHikiotoshimeisai.ToolTip = null;
			this.ibHikiotoshimeisai.ToolTipCaption = "";
			this.ibHikiotoshimeisai.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibHikiotoshimeisai.ToolTipText = "";
			// 
			// ibHikiotoshiData
			// 
			this.ibHikiotoshiData.BackColor = System.Drawing.Color.Transparent;
			this.ibHikiotoshiData.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibHikiotoshiData.ImgDefault = global::App.Properties.Resources.button_normal_1Staff;
			this.ibHikiotoshiData.ImgDisabled = null;
			this.ibHikiotoshiData.ImgMouseOver = null;
			this.ibHikiotoshiData.ImgMousePush = null;
			this.ibHikiotoshiData.ImgPullDown = null;
			this.ibHikiotoshiData.LabelFontSize = 9F;
			this.ibHikiotoshiData.Location = new System.Drawing.Point(101, 46);
			this.ibHikiotoshiData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibHikiotoshiData.Name = "ibHikiotoshiData";
			this.ibHikiotoshiData.PullDown = false;
			this.ibHikiotoshiData.Size = new System.Drawing.Size(100, 95);
			this.ibHikiotoshiData.TabIndex = 4;
			this.ibHikiotoshiData.TextButton = "引落データ作成";
			this.ibHikiotoshiData.ToolTip = null;
			this.ibHikiotoshiData.ToolTipCaption = "";
			this.ibHikiotoshiData.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibHikiotoshiData.ToolTipText = "";
			// 
			// panel6
			// 
			this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.panel6.Location = new System.Drawing.Point(3, 3);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(478, 35);
			this.panel6.TabIndex = 3;
			// 
			// panel2
			// 
			this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.panel2.Location = new System.Drawing.Point(12, 105);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(186, 49);
			this.panel2.TabIndex = 8;
			// 
			// panel9
			// 
			this.panel9.Controls.Add(this.ibKaiinjohoToroku);
			this.panel9.Controls.Add(this.ibOutKaihi);
			this.panel9.Controls.Add(this.ibOutKaiinjoho);
			this.panel9.Controls.Add(this.panel10);
			this.panel9.Location = new System.Drawing.Point(58, 160);
			this.panel9.Name = "panel9";
			this.panel9.Size = new System.Drawing.Size(166, 492);
			this.panel9.TabIndex = 9;
			// 
			// ibKaiinjohoToroku
			// 
			this.ibKaiinjohoToroku.BackColor = System.Drawing.Color.Transparent;
			this.ibKaiinjohoToroku.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibKaiinjohoToroku.ImgDefault = global::App.Properties.Resources.button_normal_1Staff;
			this.ibKaiinjohoToroku.ImgDisabled = null;
			this.ibKaiinjohoToroku.ImgMouseOver = null;
			this.ibKaiinjohoToroku.ImgMousePush = null;
			this.ibKaiinjohoToroku.ImgPullDown = null;
			this.ibKaiinjohoToroku.LabelFontSize = 9F;
			this.ibKaiinjohoToroku.Location = new System.Drawing.Point(4, 46);
			this.ibKaiinjohoToroku.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibKaiinjohoToroku.Name = "ibKaiinjohoToroku";
			this.ibKaiinjohoToroku.PullDown = false;
			this.ibKaiinjohoToroku.Size = new System.Drawing.Size(100, 95);
			this.ibKaiinjohoToroku.TabIndex = 0;
			this.ibKaiinjohoToroku.TextButton = "会員情報登録";
			this.ibKaiinjohoToroku.ToolTip = null;
			this.ibKaiinjohoToroku.ToolTipCaption = "";
			this.ibKaiinjohoToroku.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibKaiinjohoToroku.ToolTipText = "";
			// 
			// ibOutKaihi
			// 
			this.ibOutKaihi.BackColor = System.Drawing.Color.Transparent;
			this.ibOutKaihi.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibOutKaihi.ImgDefault = global::App.Properties.Resources.button_normal_1Staff;
			this.ibOutKaihi.ImgDisabled = null;
			this.ibOutKaihi.ImgMouseOver = null;
			this.ibOutKaihi.ImgMousePush = null;
			this.ibOutKaihi.ImgPullDown = null;
			this.ibOutKaihi.LabelFontSize = 9F;
			this.ibOutKaihi.Location = new System.Drawing.Point(4, 256);
			this.ibOutKaihi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibOutKaihi.Name = "ibOutKaihi";
			this.ibOutKaihi.PullDown = false;
			this.ibOutKaihi.Size = new System.Drawing.Size(100, 95);
			this.ibOutKaihi.TabIndex = 2;
			this.ibOutKaihi.TextButton = "会費情報出力";
			this.ibOutKaihi.ToolTip = null;
			this.ibOutKaihi.ToolTipCaption = "";
			this.ibOutKaihi.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibOutKaihi.ToolTipText = "";
			// 
			// ibOutKaiinjoho
			// 
			this.ibOutKaiinjoho.BackColor = System.Drawing.Color.Transparent;
			this.ibOutKaiinjoho.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ibOutKaiinjoho.ImgDefault = global::App.Properties.Resources.button_normal_1Staff;
			this.ibOutKaiinjoho.ImgDisabled = null;
			this.ibOutKaiinjoho.ImgMouseOver = null;
			this.ibOutKaiinjoho.ImgMousePush = null;
			this.ibOutKaiinjoho.ImgPullDown = null;
			this.ibOutKaiinjoho.LabelFontSize = 9F;
			this.ibOutKaiinjoho.Location = new System.Drawing.Point(4, 151);
			this.ibOutKaiinjoho.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ibOutKaiinjoho.Name = "ibOutKaiinjoho";
			this.ibOutKaiinjoho.PullDown = false;
			this.ibOutKaiinjoho.Size = new System.Drawing.Size(100, 95);
			this.ibOutKaiinjoho.TabIndex = 1;
			this.ibOutKaiinjoho.TextButton = "会員情報出力";
			this.ibOutKaiinjoho.ToolTip = null;
			this.ibOutKaiinjoho.ToolTipCaption = "";
			this.ibOutKaiinjoho.ToolTipIcon = System.Windows.Forms.ToolTipIcon.None;
			this.ibOutKaiinjoho.ToolTipText = "";
			// 
			// panel10
			// 
			this.panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.panel10.Location = new System.Drawing.Point(3, 3);
			this.panel10.Name = "panel10";
			this.panel10.Size = new System.Drawing.Size(160, 35);
			this.panel10.TabIndex = 3;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(723, 635);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(139, 29);
			this.button3.TabIndex = 17;
			this.button3.Text = "Excel出力";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// btnMasterImport
			// 
			this.btnMasterImport.Location = new System.Drawing.Point(651, 670);
			this.btnMasterImport.Name = "btnMasterImport";
			this.btnMasterImport.Size = new System.Drawing.Size(139, 29);
			this.btnMasterImport.TabIndex = 18;
			this.btnMasterImport.Text = "マスター取込";
			this.btnMasterImport.UseVisualStyleBackColor = true;
			// 
			// btnRep2
			// 
			this.btnRep2.Location = new System.Drawing.Point(506, 670);
			this.btnRep2.Name = "btnRep2";
			this.btnRep2.Size = new System.Drawing.Size(139, 29);
			this.btnRep2.TabIndex = 20;
			this.btnRep2.Text = "帳票11-17";
			this.btnRep2.UseVisualStyleBackColor = true;
			// 
			// btnRep1
			// 
			this.btnRep1.Location = new System.Drawing.Point(361, 670);
			this.btnRep1.Name = "btnRep1";
			this.btnRep1.Size = new System.Drawing.Size(139, 29);
			this.btnRep1.TabIndex = 19;
			this.btnRep1.Text = "帳票1-10";
			this.btnRep1.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(216, 670);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(139, 29);
			this.button1.TabIndex = 15;
			this.button1.Text = "iFAXデータ出力";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(71, 670);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(139, 29);
			this.button2.TabIndex = 16;
			this.button2.Text = "MAMISデータ取込";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1201, 711);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.btnRep2);
			this.Controls.Add(this.btnRep1);
			this.Controls.Add(this.btnMasterImport);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel7);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel9);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form2";
			this.Controls.SetChildIndex(this.panel4, 0);
			this.Controls.SetChildIndex(this.panel1, 0);
			this.Controls.SetChildIndex(this.panel9, 0);
			this.Controls.SetChildIndex(this.panel2, 0);
			this.Controls.SetChildIndex(this.panel3, 0);
			this.Controls.SetChildIndex(this.panel7, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.button2, 0);
			this.Controls.SetChildIndex(this.btnMasterImport, 0);
			this.Controls.SetChildIndex(this.btnRep1, 0);
			this.Controls.SetChildIndex(this.btnRep2, 0);
			this.Controls.SetChildIndex(this.button3, 0);
			this.panel4.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.iTantosha)).EndInit();
			this.panel7.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel9.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel4;
		private UcImgButton ibRestore;
		private UcImgButton ibBackup;
		private UcImgButton ibRemote;
		private UcImgButton ibLogout;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblVer;
		private GControlGcTextBoxEx.GcTextBoxEx iTantosha;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel7;
		private UcImgButton ibBankCode;
		private UcImgButton ibIryoKikan;
		private UcImgButton ibKaihi;
		private UcImgButton ibTantosha;
		private UcImgButton ibShinryoKamoku;
		private UcImgButton ibIkaiKaihi;
		private UcImgButton ibBasic;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Panel panel3;
		private UcImgButton ibFB;
		private UcImgButton ibLabel;
		private UcImgButton ibSeikyusho;
		private UcImgButton ibHikiotoshimeisai;
		private UcImgButton ibHikiotoshiData;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel9;
		private UcImgButton ibKaiinjohoToroku;
		private UcImgButton ibOutKaihi;
		private UcImgButton ibOutKaiinjoho;
		private System.Windows.Forms.Panel panel10;
		private GrapeCity.Win.Editors.GcShortcut gcShortcut1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button btnMasterImport;
		private System.Windows.Forms.Button btnRep2;
		private System.Windows.Forms.Button btnRep1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	}
}