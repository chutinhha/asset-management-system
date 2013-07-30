namespace AssMngSys
{
    partial class QryAssDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QryAssDlg));
            this.textBoxPid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAssId = new System.Windows.Forms.TextBox();
            this.comboBoxStatSub = new System.Windows.Forms.ComboBox();
            this.comboBoxStat = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxDate = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxPid
            // 
            this.textBoxPid.Location = new System.Drawing.Point(150, 74);
            this.textBoxPid.Name = "textBoxPid";
            this.textBoxPid.Size = new System.Drawing.Size(133, 21);
            this.textBoxPid.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 40;
            this.label1.Text = "资产编码：";
            // 
            // textBoxAssId
            // 
            this.textBoxAssId.Location = new System.Drawing.Point(150, 38);
            this.textBoxAssId.Name = "textBoxAssId";
            this.textBoxAssId.Size = new System.Drawing.Size(133, 21);
            this.textBoxAssId.TabIndex = 39;
            // 
            // comboBoxStatSub
            // 
            this.comboBoxStatSub.FormattingEnabled = true;
            this.comboBoxStatSub.Items.AddRange(new object[] {
            "借用",
            "归还",
            "送修",
            "修返",
            "外出",
            "返回"});
            this.comboBoxStatSub.Location = new System.Drawing.Point(150, 181);
            this.comboBoxStatSub.Name = "comboBoxStatSub";
            this.comboBoxStatSub.Size = new System.Drawing.Size(133, 20);
            this.comboBoxStatSub.TabIndex = 44;
            // 
            // comboBoxStat
            // 
            this.comboBoxStat.FormattingEnabled = true;
            this.comboBoxStat.Items.AddRange(new object[] {
            "库存",
            "领用",
            "租还",
            "退返",
            "丢失",
            "报废",
            "转出"});
            this.comboBoxStat.Location = new System.Drawing.Point(150, 147);
            this.comboBoxStat.Name = "comboBoxStat";
            this.comboBoxStat.Size = new System.Drawing.Size(133, 20);
            this.comboBoxStat.TabIndex = 45;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(67, 186);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 42;
            this.label14.Text = "使用状态：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(289, 223);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 43;
            this.label10.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 43;
            this.label2.Text = "电子标签：";
            // 
            // buttonSave
            // 
            this.buttonSave.Image = global::AssMngSys.Properties.Resources.ok;
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.Location = new System.Drawing.Point(69, 295);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(106, 36);
            this.buttonSave.TabIndex = 46;
            this.buttonSave.Text = "    提交(&S)";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::AssMngSys.Properties.Resources.stop;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(181, 295);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(106, 36);
            this.buttonCancel.TabIndex = 47;
            this.buttonCancel.Text = "    取消(&Q)";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 42;
            this.label3.Text = "是否已打印：";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Y",
            "N"});
            this.comboBox1.Location = new System.Drawing.Point(150, 111);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(133, 20);
            this.comboBox1.TabIndex = 44;
            this.comboBox1.Text = "N";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(150, 216);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(133, 21);
            this.dateTimePicker1.TabIndex = 48;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(150, 243);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(133, 21);
            this.dateTimePicker2.TabIndex = 48;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(150, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 42;
            this.label5.Text = "-";
            // 
            // checkBoxDate
            // 
            this.checkBoxDate.AutoSize = true;
            this.checkBoxDate.Location = new System.Drawing.Point(48, 219);
            this.checkBoxDate.Name = "checkBoxDate";
            this.checkBoxDate.Size = new System.Drawing.Size(84, 16);
            this.checkBoxDate.TabIndex = 49;
            this.checkBoxDate.Text = "录入日期：";
            this.checkBoxDate.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 42;
            this.label4.Text = "库存状态：";
            // 
            // QryAssDlg
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(351, 370);
            this.Controls.Add(this.checkBoxDate);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxPid);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.comboBoxStatSub);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxStat);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxAssId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QryAssDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "请选择查询条件";
            this.Load += new System.EventHandler(this.QryAssDlg_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QryAssDlg_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAssId;
        private System.Windows.Forms.ComboBox comboBoxStatSub;
        private System.Windows.Forms.ComboBox comboBoxStat;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxDate;
        private System.Windows.Forms.Label label4;
    }
}