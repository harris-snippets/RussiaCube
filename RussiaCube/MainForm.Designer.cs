namespace RussiaCube
{
	partial class MainForm
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.controlPanel = new System.Windows.Forms.Panel();
            this.preBox = new System.Windows.Forms.PictureBox();
            this.labelStart = new System.Windows.Forms.Label();
            this.labelPause = new System.Windows.Forms.Label();
            this.labelContinue = new System.Windows.Forms.Label();
            this.viewBox = new System.Windows.Forms.PictureBox();
            this.labelScore = new System.Windows.Forms.Label();
            this.controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.preBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // controlPanel
            // 
            this.controlPanel.Controls.Add(this.preBox);
            this.controlPanel.Controls.Add(this.labelStart);
            this.controlPanel.Controls.Add(this.labelPause);
            this.controlPanel.Controls.Add(this.labelContinue);
            this.controlPanel.Location = new System.Drawing.Point(358, 9);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(100, 270);
            this.controlPanel.TabIndex = 6;
            // 
            // preBox
            // 
            this.preBox.BackColor = System.Drawing.SystemColors.Control;
            this.preBox.Location = new System.Drawing.Point(0, 0);
            this.preBox.Name = "preBox";
            this.preBox.Size = new System.Drawing.Size(100, 100);
            this.preBox.TabIndex = 2;
            this.preBox.TabStop = false;
            // 
            // labelStart
            // 
            this.labelStart.AutoSize = true;
            this.labelStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelStart.Location = new System.Drawing.Point(28, 125);
            this.labelStart.Name = "labelStart";
            this.labelStart.Size = new System.Drawing.Size(37, 14);
            this.labelStart.TabIndex = 3;
            this.labelStart.Text = "开 始";
            this.labelStart.Click += new System.EventHandler(this.labelStart_Click);
            // 
            // labelPause
            // 
            this.labelPause.AutoSize = true;
            this.labelPause.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelPause.Location = new System.Drawing.Point(28, 151);
            this.labelPause.Name = "labelPause";
            this.labelPause.Size = new System.Drawing.Size(37, 14);
            this.labelPause.TabIndex = 3;
            this.labelPause.Text = "暂 停";
            this.labelPause.Click += new System.EventHandler(this.labelPause_Click);
            // 
            // labelContinue
            // 
            this.labelContinue.AutoSize = true;
            this.labelContinue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelContinue.Location = new System.Drawing.Point(28, 177);
            this.labelContinue.Name = "labelContinue";
            this.labelContinue.Size = new System.Drawing.Size(37, 14);
            this.labelContinue.TabIndex = 3;
            this.labelContinue.Text = "继 续";
            this.labelContinue.Click += new System.EventHandler(this.labelContinue_Click);
            // 
            // viewBox
            // 
            this.viewBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.viewBox.Location = new System.Drawing.Point(9, 9);
            this.viewBox.Margin = new System.Windows.Forms.Padding(0);
            this.viewBox.Name = "viewBox";
            this.viewBox.Size = new System.Drawing.Size(340, 459);
            this.viewBox.TabIndex = 5;
            this.viewBox.TabStop = false;
            // 
            // labelScore
            // 
            this.labelScore.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelScore.ForeColor = System.Drawing.Color.Red;
            this.labelScore.Location = new System.Drawing.Point(384, 249);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(41, 28);
            this.labelScore.TabIndex = 7;
            this.labelScore.Text = "0";
            this.labelScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 478);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.viewBox);
            this.Controls.Add(this.labelScore);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "俄罗斯方块";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.preBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBox)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel controlPanel;
		private System.Windows.Forms.PictureBox preBox;
		private System.Windows.Forms.Label labelStart;
		private System.Windows.Forms.Label labelPause;
		private System.Windows.Forms.Label labelContinue;
		private System.Windows.Forms.PictureBox viewBox;
		private System.Windows.Forms.Label labelScore;
	}
}

