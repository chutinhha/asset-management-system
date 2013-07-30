namespace AssMngSysCe
{
    partial class ReadForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.reservedButton = new System.Windows.Forms.RadioButton();
            this.tidButton = new System.Windows.Forms.RadioButton();
            this.userButton = new System.Windows.Forms.RadioButton();
            this.epcButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxOff = new System.Windows.Forms.TextBox();
            this.textBoxlen = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxRead = new System.Windows.Forms.TextBox();
            this.textBoxWrite = new System.Windows.Forms.TextBox();
            this.labelMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // reservedButton
            // 
            this.reservedButton.Location = new System.Drawing.Point(14, 22);
            this.reservedButton.Name = "reservedButton";
            this.reservedButton.Size = new System.Drawing.Size(94, 23);
            this.reservedButton.TabIndex = 0;
            this.reservedButton.Text = "Reserved";
            this.reservedButton.CheckedChanged += new System.EventHandler(this.reservedButton_CheckedChanged);
            // 
            // tidButton
            // 
            this.tidButton.Location = new System.Drawing.Point(14, 109);
            this.tidButton.Name = "tidButton";
            this.tidButton.Size = new System.Drawing.Size(69, 23);
            this.tidButton.TabIndex = 1;
            this.tidButton.Text = "TID";
            // 
            // userButton
            // 
            this.userButton.Location = new System.Drawing.Point(14, 80);
            this.userButton.Name = "userButton";
            this.userButton.Size = new System.Drawing.Size(69, 23);
            this.userButton.TabIndex = 2;
            this.userButton.Text = "User";
            // 
            // epcButton
            // 
            this.epcButton.Location = new System.Drawing.Point(14, 51);
            this.epcButton.Name = "epcButton";
            this.epcButton.Size = new System.Drawing.Size(81, 23);
            this.epcButton.TabIndex = 3;
            this.epcButton.Text = "EPC";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(89, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 23);
            this.label1.Text = "偏移";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(89, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 23);
            this.label2.Text = "长度";
            // 
            // textBoxOff
            // 
            this.textBoxOff.Location = new System.Drawing.Point(135, 47);
            this.textBoxOff.Name = "textBoxOff";
            this.textBoxOff.Size = new System.Drawing.Size(79, 23);
            this.textBoxOff.TabIndex = 7;
            this.textBoxOff.Text = "2";
            // 
            // textBoxlen
            // 
            this.textBoxlen.Location = new System.Drawing.Point(135, 80);
            this.textBoxlen.Name = "textBoxlen";
            this.textBoxlen.Size = new System.Drawing.Size(79, 23);
            this.textBoxlen.TabIndex = 8;
            this.textBoxlen.Text = "1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 147);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 30);
            this.button1.TabIndex = 9;
            this.button1.Text = "读标签";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(19, 211);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 30);
            this.button2.TabIndex = 10;
            this.button2.Text = "写标签";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxRead
            // 
            this.textBoxRead.Location = new System.Drawing.Point(114, 137);
            this.textBoxRead.Multiline = true;
            this.textBoxRead.Name = "textBoxRead";
            this.textBoxRead.Size = new System.Drawing.Size(110, 46);
            this.textBoxRead.TabIndex = 11;
            // 
            // textBoxWrite
            // 
            this.textBoxWrite.Location = new System.Drawing.Point(114, 202);
            this.textBoxWrite.Multiline = true;
            this.textBoxWrite.Name = "textBoxWrite";
            this.textBoxWrite.Size = new System.Drawing.Size(110, 46);
            this.textBoxWrite.TabIndex = 12;
            this.textBoxWrite.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // labelMsg
            // 
            this.labelMsg.Location = new System.Drawing.Point(23, 254);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(91, 23);
            // 
            // ReadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 272);
            this.Controls.Add(this.labelMsg);
            this.Controls.Add(this.textBoxWrite);
            this.Controls.Add(this.textBoxRead);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxlen);
            this.Controls.Add(this.textBoxOff);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.epcButton);
            this.Controls.Add(this.userButton);
            this.Controls.Add(this.tidButton);
            this.Controls.Add(this.reservedButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReadForm";
            this.Text = "读写";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ReadForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton reservedButton;
        private System.Windows.Forms.RadioButton tidButton;
        private System.Windows.Forms.RadioButton userButton;
        private System.Windows.Forms.RadioButton epcButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxOff;
        private System.Windows.Forms.TextBox textBoxlen;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBoxRead;
        private System.Windows.Forms.TextBox textBoxWrite;
        private System.Windows.Forms.Label labelMsg;
    }
}