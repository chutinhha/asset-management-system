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
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxYnPrint = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxDate = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxYnWrite = new System.Windows.Forms.ComboBox();
            this.comboBoxAlive = new System.Windows.Forms.ComboBox();
            this.comboBoxAssNam = new System.Windows.Forms.ComboBox();
            this.comboBoxTyp = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxUseMan = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxDutyMan = new System.Windows.Forms.ComboBox();
            this.checkBoxPri = new System.Windows.Forms.CheckBox();
            this.textBoxPriLow = new System.Windows.Forms.TextBox();
            this.textBoxPriUp = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBoxDept = new System.Windows.Forms.ComboBox();
            this.comboBoxAddr = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBoxYnRepair = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxPid
            // 
            this.textBoxPid.Location = new System.Drawing.Point(162, 37);
            this.textBoxPid.Name = "textBoxPid";
            this.textBoxPid.Size = new System.Drawing.Size(150, 21);
            this.textBoxPid.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(378, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 40;
            this.label1.Text = "资产编码：";
            // 
            // textBoxAssId
            // 
            this.textBoxAssId.Location = new System.Drawing.Point(449, 39);
            this.textBoxAssId.Name = "textBoxAssId";
            this.textBoxAssId.Size = new System.Drawing.Size(150, 21);
            this.textBoxAssId.TabIndex = 2;
            // 
            // comboBoxStatSub
            // 
            this.comboBoxStatSub.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxStatSub.FormattingEnabled = true;
            this.comboBoxStatSub.Items.AddRange(new object[] {
            "",
            "借用",
            "归还",
            "送修",
            "修返",
            "外出",
            "返回"});
            this.comboBoxStatSub.Location = new System.Drawing.Point(449, 116);
            this.comboBoxStatSub.Name = "comboBoxStatSub";
            this.comboBoxStatSub.Size = new System.Drawing.Size(150, 20);
            this.comboBoxStatSub.TabIndex = 9;
            // 
            // comboBoxStat
            // 
            this.comboBoxStat.FormattingEnabled = true;
            this.comboBoxStat.Items.AddRange(new object[] {
            "",
            "库存",
            "领用",
            "租还",
            "退返",
            "丢失",
            "报废",
            "转出"});
            this.comboBoxStat.Location = new System.Drawing.Point(250, 116);
            this.comboBoxStat.Name = "comboBoxStat";
            this.comboBoxStat.Size = new System.Drawing.Size(61, 20);
            this.comboBoxStat.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(378, 119);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 42;
            this.label14.Text = "使用状态：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(281, 271);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 43;
            this.label10.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 43;
            this.label2.Text = "标签喷码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(366, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 42;
            this.label3.Text = "是否已打印：";
            // 
            // comboBoxYnPrint
            // 
            this.comboBoxYnPrint.FormattingEnabled = true;
            this.comboBoxYnPrint.Items.AddRange(new object[] {
            "",
            "Y",
            "N"});
            this.comboBoxYnPrint.Location = new System.Drawing.Point(449, 90);
            this.comboBoxYnPrint.Name = "comboBoxYnPrint";
            this.comboBoxYnPrint.Size = new System.Drawing.Size(150, 20);
            this.comboBoxYnPrint.TabIndex = 6;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(161, 264);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(111, 21);
            this.dateTimePicker1.TabIndex = 19;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Enabled = false;
            this.dateTimePicker2.Location = new System.Drawing.Point(295, 265);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(111, 21);
            this.dateTimePicker2.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(161, 268);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 42;
            this.label5.Text = "-";
            // 
            // checkBoxDate
            // 
            this.checkBoxDate.AutoSize = true;
            this.checkBoxDate.Location = new System.Drawing.Point(72, 267);
            this.checkBoxDate.Name = "checkBoxDate";
            this.checkBoxDate.Size = new System.Drawing.Size(84, 16);
            this.checkBoxDate.TabIndex = 18;
            this.checkBoxDate.Text = "购置日期：";
            this.checkBoxDate.UseVisualStyleBackColor = true;
            this.checkBoxDate.CheckedChanged += new System.EventHandler(this.checkBoxDate_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(91, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 42;
            this.label4.Text = "库存状态：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(79, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 42;
            this.label6.Text = "是否已发卡：";
            // 
            // comboBoxYnWrite
            // 
            this.comboBoxYnWrite.FormattingEnabled = true;
            this.comboBoxYnWrite.Items.AddRange(new object[] {
            "",
            "Y",
            "N"});
            this.comboBoxYnWrite.Location = new System.Drawing.Point(162, 90);
            this.comboBoxYnWrite.Name = "comboBoxYnWrite";
            this.comboBoxYnWrite.Size = new System.Drawing.Size(150, 20);
            this.comboBoxYnWrite.TabIndex = 5;
            // 
            // comboBoxAlive
            // 
            this.comboBoxAlive.FormattingEnabled = true;
            this.comboBoxAlive.Items.AddRange(new object[] {
            "",
            "有效资产",
            "无效资产"});
            this.comboBoxAlive.Location = new System.Drawing.Point(162, 116);
            this.comboBoxAlive.Name = "comboBoxAlive";
            this.comboBoxAlive.Size = new System.Drawing.Size(83, 20);
            this.comboBoxAlive.TabIndex = 7;
            this.comboBoxAlive.Text = "有效资产";
            this.comboBoxAlive.SelectedIndexChanged += new System.EventHandler(this.comboBoxAlive_SelectedIndexChanged);
            // 
            // comboBoxAssNam
            // 
            this.comboBoxAssNam.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxAssNam.FormattingEnabled = true;
            this.comboBoxAssNam.Location = new System.Drawing.Point(449, 65);
            this.comboBoxAssNam.Name = "comboBoxAssNam";
            this.comboBoxAssNam.Size = new System.Drawing.Size(150, 20);
            this.comboBoxAssNam.TabIndex = 4;
            // 
            // comboBoxTyp
            // 
            this.comboBoxTyp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxTyp.FormattingEnabled = true;
            this.comboBoxTyp.Location = new System.Drawing.Point(162, 64);
            this.comboBoxTyp.Name = "comboBoxTyp";
            this.comboBoxTyp.Size = new System.Drawing.Size(150, 20);
            this.comboBoxTyp.TabIndex = 3;
            this.comboBoxTyp.SelectionChangeCommitted += new System.EventHandler(this.comboBoxTyp_SelectionChangeCommitted);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(91, 69);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(65, 12);
            this.label20.TabIndex = 44;
            this.label20.Text = "资产类型：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(378, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 45;
            this.label7.Text = "资产名称：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(91, 149);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 45;
            this.label8.Text = "领用人员：";
            // 
            // comboBoxUseMan
            // 
            this.comboBoxUseMan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxUseMan.FormattingEnabled = true;
            this.comboBoxUseMan.Location = new System.Drawing.Point(162, 144);
            this.comboBoxUseMan.Name = "comboBoxUseMan";
            this.comboBoxUseMan.Size = new System.Drawing.Size(150, 20);
            this.comboBoxUseMan.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(378, 143);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 45;
            this.label9.Text = "保管人员：";
            // 
            // comboBoxDutyMan
            // 
            this.comboBoxDutyMan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDutyMan.FormattingEnabled = true;
            this.comboBoxDutyMan.Location = new System.Drawing.Point(449, 139);
            this.comboBoxDutyMan.Name = "comboBoxDutyMan";
            this.comboBoxDutyMan.Size = new System.Drawing.Size(150, 20);
            this.comboBoxDutyMan.TabIndex = 11;
            // 
            // checkBoxPri
            // 
            this.checkBoxPri.AutoSize = true;
            this.checkBoxPri.Location = new System.Drawing.Point(72, 234);
            this.checkBoxPri.Name = "checkBoxPri";
            this.checkBoxPri.Size = new System.Drawing.Size(84, 16);
            this.checkBoxPri.TabIndex = 15;
            this.checkBoxPri.Text = "金额范围：";
            this.checkBoxPri.UseVisualStyleBackColor = true;
            this.checkBoxPri.CheckedChanged += new System.EventHandler(this.checkBoxPri_CheckedChanged);
            // 
            // textBoxPriLow
            // 
            this.textBoxPriLow.Enabled = false;
            this.textBoxPriLow.Location = new System.Drawing.Point(162, 232);
            this.textBoxPriLow.Name = "textBoxPriLow";
            this.textBoxPriLow.Size = new System.Drawing.Size(111, 21);
            this.textBoxPriLow.TabIndex = 16;
            // 
            // textBoxPriUp
            // 
            this.textBoxPriUp.Enabled = false;
            this.textBoxPriUp.Location = new System.Drawing.Point(295, 232);
            this.textBoxPriUp.Name = "textBoxPriUp";
            this.textBoxPriUp.Size = new System.Drawing.Size(111, 21);
            this.textBoxPriUp.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(278, 238);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 43;
            this.label11.Text = "-";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(91, 200);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 49;
            this.label12.Text = "所属部门：";
            // 
            // comboBoxDept
            // 
            this.comboBoxDept.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDept.FormattingEnabled = true;
            this.comboBoxDept.Location = new System.Drawing.Point(162, 198);
            this.comboBoxDept.Name = "comboBoxDept";
            this.comboBoxDept.Size = new System.Drawing.Size(438, 20);
            this.comboBoxDept.TabIndex = 14;
            // 
            // comboBoxAddr
            // 
            this.comboBoxAddr.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxAddr.FormattingEnabled = true;
            this.comboBoxAddr.Location = new System.Drawing.Point(162, 172);
            this.comboBoxAddr.Name = "comboBoxAddr";
            this.comboBoxAddr.Size = new System.Drawing.Size(174, 20);
            this.comboBoxAddr.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(91, 176);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 51;
            this.label13.Text = "所在地点：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(366, 171);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 12);
            this.label15.TabIndex = 42;
            this.label15.Text = "是否维修过：";
            // 
            // comboBoxYnRepair
            // 
            this.comboBoxYnRepair.FormattingEnabled = true;
            this.comboBoxYnRepair.Items.AddRange(new object[] {
            "",
            "Y",
            "N"});
            this.comboBoxYnRepair.Location = new System.Drawing.Point(449, 168);
            this.comboBoxYnRepair.Name = "comboBoxYnRepair";
            this.comboBoxYnRepair.Size = new System.Drawing.Size(150, 20);
            this.comboBoxYnRepair.TabIndex = 13;
            // 
            // buttonSave
            // 
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Image = global::AssMngSys.Properties.Resources.ok;
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.Location = new System.Drawing.Point(230, 339);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(106, 36);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "    提交(&S)";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::AssMngSys.Properties.Resources.stop;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(342, 339);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(106, 36);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "    取消(&Q)";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // QryAssDlg
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(647, 398);
            this.Controls.Add(this.comboBoxAddr);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.comboBoxDept);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxPriUp);
            this.Controls.Add(this.textBoxPriLow);
            this.Controls.Add(this.comboBoxDutyMan);
            this.Controls.Add(this.comboBoxUseMan);
            this.Controls.Add(this.comboBoxAssNam);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBoxTyp);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.checkBoxPri);
            this.Controls.Add(this.checkBoxDate);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxPid);
            this.Controls.Add(this.comboBoxYnWrite);
            this.Controls.Add(this.comboBoxYnRepair);
            this.Controls.Add(this.comboBoxYnPrint);
            this.Controls.Add(this.comboBoxStatSub);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxAlive);
            this.Controls.Add(this.comboBoxStat);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label11);
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
        private System.Windows.Forms.ComboBox comboBoxYnPrint;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxYnWrite;
        private System.Windows.Forms.ComboBox comboBoxAlive;
        private System.Windows.Forms.ComboBox comboBoxAssNam;
        private System.Windows.Forms.ComboBox comboBoxTyp;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxUseMan;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxDutyMan;
        private System.Windows.Forms.CheckBox checkBoxPri;
        private System.Windows.Forms.TextBox textBoxPriLow;
        private System.Windows.Forms.TextBox textBoxPriUp;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBoxDept;
        private System.Windows.Forms.ComboBox comboBoxAddr;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox comboBoxYnRepair;
    }
}