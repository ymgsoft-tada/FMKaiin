
namespace App
{
    partial class FormMasterIryoKikan2_Out
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
            this.funckey = new App.Controls.FunctionKeyBar();
            this.icoHeading = new App.Controls.IconBox();
            this.lblHeading = new App.Controls.SLabel();
            this.secBody = new App.Controls.SectionPanel();
            this.secHani = new App.Controls.SectionPanel();
            this.lblHani = new App.Controls.SLabel();
            this.rdoList = new App.Controls.SRadioButton();
            this.rdoJoho = new App.Controls.SRadioButton();
            this.secBody.SuspendLayout();
            this.secHani.SuspendLayout();
            this.SuspendLayout();
            // 
            // funckey
            // 
            this.funckey.Location = new System.Drawing.Point(0, 182);
            this.funckey.Name = "funckey";
            this.funckey.Size = new System.Drawing.Size(420, 66);
            this.funckey.TabIndex = 1;
            // 
            // icoHeading
            // 
            this.icoHeading.BackColor = System.Drawing.Color.Transparent;
            this.icoHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(100)))), ((int)(((byte)(161)))));
            this.icoHeading.IconKind = App.Controls.MenuIconKind.DoubleSlash;
            this.icoHeading.LineWeight = 0.9F;
            this.icoHeading.Location = new System.Drawing.Point(16, 13);
            this.icoHeading.Name = "icoHeading";
            this.icoHeading.Size = new System.Drawing.Size(24, 24);
            this.icoHeading.TabIndex = 2;
            this.icoHeading.TabStop = false;
            // 
            // lblHeading
            // 
            this.lblHeading.BackColor = System.Drawing.Color.Transparent;
            this.lblHeading.Font = new System.Drawing.Font("BIZ UDPゴシック", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(42)))), ((int)(((byte)(74)))));
            this.lblHeading.Location = new System.Drawing.Point(46, 10);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(300, 30);
            this.lblHeading.TabIndex = 3;
            this.lblHeading.TabStop = false;
            this.lblHeading.Text = "印刷条件";
            // 
            // secBody
            // 
            this.secBody.BackColor = System.Drawing.Color.Transparent;
            this.secBody.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secBody.BorderGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(196)))), ((int)(((byte)(208)))), ((int)(((byte)(222)))));
            this.secBody.Controls.Add(this.lblHani);
            this.secBody.Controls.Add(this.secHani);
            this.secBody.CornerRadius = 8;
            this.secBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.secBody.HeaderHeight = 0;
            this.secBody.Location = new System.Drawing.Point(10, 46);
            this.secBody.Name = "secBody";
            this.secBody.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.secBody.ShadowSize = 6;
            this.secBody.Size = new System.Drawing.Size(400, 116);
            this.secBody.TabIndex = 0;
            // 
            // lblHani
            // 
            this.lblHani.BackColor = System.Drawing.Color.Transparent;
            this.lblHani.Font = new System.Drawing.Font("BIZ UDPゴシック", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHani.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(86)))), ((int)(((byte)(134)))));
            this.lblHani.Location = new System.Drawing.Point(26, 29);
            this.lblHani.Name = "lblHani";
            this.lblHani.Size = new System.Drawing.Size(118, 24);
            this.lblHani.TabIndex = 100;
            this.lblHani.TabStop = false;
            this.lblHani.Text = "印刷範囲";
            // 
            // secHani
            // 
            this.secHani.BackColor = System.Drawing.Color.Transparent;
            this.secHani.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(226)))));
            this.secHani.Controls.Add(this.rdoList);
            this.secHani.Controls.Add(this.rdoJoho);
            this.secHani.CornerRadius = 6;
            this.secHani.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.secHani.HeaderHeight = 0;
            this.secHani.Location = new System.Drawing.Point(150, 24);
            this.secHani.Name = "secHani";
            this.secHani.Size = new System.Drawing.Size(180, 65);
            this.secHani.TabIndex = 0;
            // 
            // rdoList
            // 
            this.rdoList.Location = new System.Drawing.Point(10, 5);
            this.rdoList.Name = "rdoList";
            this.rdoList.Size = new System.Drawing.Size(160, 24);
            this.rdoList.TabIndex = 0;
            this.rdoList.TabStop = true;
            this.rdoList.Text = "医療機関一覧表";
            this.rdoList.UseVisualStyleBackColor = false;
            // 
            // rdoJoho
            // 
            this.rdoJoho.Location = new System.Drawing.Point(10, 36);
            this.rdoJoho.Name = "rdoJoho";
            this.rdoJoho.Size = new System.Drawing.Size(160, 24);
            this.rdoJoho.TabIndex = 1;
            this.rdoJoho.TabStop = true;
            this.rdoJoho.Text = "医療機関情報";
            this.rdoJoho.UseVisualStyleBackColor = false;
            // 
            // FormMasterIryoKikan2_Out
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.BackgroundWave;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(420, 248);
            this.Controls.Add(this.secBody);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.icoHeading);
            this.Controls.Add(this.funckey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMasterIryoKikan2_Out";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "医療機関マスタ：印刷プレビュー";
            this.Controls.SetChildIndex(this.funckey, 0);
            this.Controls.SetChildIndex(this.icoHeading, 0);
            this.Controls.SetChildIndex(this.lblHeading, 0);
            this.Controls.SetChildIndex(this.secBody, 0);
            this.secBody.ResumeLayout(false);
            this.secHani.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private App.Controls.FunctionKeyBar funckey;
        private App.Controls.IconBox icoHeading;
        private App.Controls.SLabel lblHeading;
        private App.Controls.SectionPanel secBody;
        private App.Controls.SectionPanel secHani;
        private App.Controls.SLabel lblHani;
        private App.Controls.SRadioButton rdoList;
        private App.Controls.SRadioButton rdoJoho;
    }
}
