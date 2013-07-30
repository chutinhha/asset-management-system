namespace IrRfidUHFDemo
{
    partial class FindForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listViewControl = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "识别";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(68, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(59, 34);
            this.button2.TabIndex = 1;
            this.button2.Text = "清除";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listViewControl
            // 
            this.listViewControl.Location = new System.Drawing.Point(3, 53);
            this.listViewControl.Name = "listViewControl";
            this.listViewControl.Size = new System.Drawing.Size(232, 212);
            this.listViewControl.TabIndex = 2;
            this.listViewControl.View = System.Windows.Forms.View.Details;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(142, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 29);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 268);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewControl);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindForm";
            this.Text = "循环读取";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FindForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FindForm_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FindForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListView listViewControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
    }
}