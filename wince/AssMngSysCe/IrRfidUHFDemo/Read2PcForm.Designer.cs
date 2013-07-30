namespace IrRfidUHFDemo
{
    partial class Read2PcForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.buttonRead = new System.Windows.Forms.Button();
            this.contextMenuShowDetail = new System.Windows.Forms.ContextMenu();
            this.menuItemShowDetail = new System.Windows.Forms.MenuItem();
            this.buttonClear = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonSyncIni = new System.Windows.Forms.Button();
            this.buttonSyncDiff = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonHide = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.labelHit = new System.Windows.Forms.Label();
            this.buttonTransfer = new System.Windows.Forms.Button();
            this.buttonRepair = new System.Windows.Forms.Button();
            this.buttonBorrow = new System.Windows.Forms.Button();
            this.buttonDiscard = new System.Windows.Forms.Button();
            this.buttonRent = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonLose = new System.Windows.Forms.Button();
            this.buttonReject = new System.Windows.Forms.Button();
            this.buttonOut = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.labelMsg = new System.Windows.Forms.Label();
            this.textBoxEpc = new System.Windows.Forms.TextBox();
            this.textBoxTid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.buttonHideCheck = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxInvListNo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxMan = new System.Windows.Forms.ComboBox();
            this.comboBoxAddr = new System.Windows.Forms.ComboBox();
            this.comboBoxDept = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRead
            // 
            this.buttonRead.Location = new System.Drawing.Point(1, 232);
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Size = new System.Drawing.Size(76, 35);
            this.buttonRead.TabIndex = 1;
            this.buttonRead.Text = "读取(F1)";
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // contextMenuShowDetail
            // 
            this.contextMenuShowDetail.MenuItems.Add(this.menuItemShowDetail);
            this.contextMenuShowDetail.Popup += new System.EventHandler(this.contextMenuShowDetail_Popup);
            // 
            // menuItemShowDetail
            // 
            this.menuItemShowDetail.Text = "显示明细";
            this.menuItemShowDetail.Popup += new System.EventHandler(this.contextMenuShowDetail_Popup);
            this.menuItemShowDetail.Click += new System.EventHandler(this.menuItemShowDetail_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(81, 232);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(76, 35);
            this.buttonClear.TabIndex = 12;
            this.buttonClear.Text = "清除(Ent)";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(161, 232);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(76, 35);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "返回(F3)";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.listBox1);
            this.tabPage5.Controls.Add(this.buttonSyncIni);
            this.tabPage5.Controls.Add(this.buttonSyncDiff);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(230, 202);
            this.tabPage5.Text = "同步";
            // 
            // listBox1
            // 
            this.listBox1.Location = new System.Drawing.Point(12, 23);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(208, 114);
            this.listBox1.TabIndex = 2;
            // 
            // buttonSyncIni
            // 
            this.buttonSyncIni.Location = new System.Drawing.Point(14, 148);
            this.buttonSyncIni.Name = "buttonSyncIni";
            this.buttonSyncIni.Size = new System.Drawing.Size(94, 26);
            this.buttonSyncIni.TabIndex = 1;
            this.buttonSyncIni.Text = "初始化";
            this.buttonSyncIni.Click += new System.EventHandler(this.buttonSyncIni_Click);
            // 
            // buttonSyncDiff
            // 
            this.buttonSyncDiff.Location = new System.Drawing.Point(114, 148);
            this.buttonSyncDiff.Name = "buttonSyncDiff";
            this.buttonSyncDiff.Size = new System.Drawing.Size(95, 26);
            this.buttonSyncDiff.TabIndex = 1;
            this.buttonSyncDiff.Text = "同步";
            this.buttonSyncDiff.Click += new System.EventHandler(this.buttonSyncDiff_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.ContextMenu = this.contextMenuShowDetail;
            this.tabPage2.Controls.Add(this.buttonHide);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Controls.Add(this.labelHit);
            this.tabPage2.Controls.Add(this.buttonTransfer);
            this.tabPage2.Controls.Add(this.buttonRepair);
            this.tabPage2.Controls.Add(this.buttonBorrow);
            this.tabPage2.Controls.Add(this.buttonDiscard);
            this.tabPage2.Controls.Add(this.buttonRent);
            this.tabPage2.Controls.Add(this.buttonSend);
            this.tabPage2.Controls.Add(this.buttonLose);
            this.tabPage2.Controls.Add(this.buttonReject);
            this.tabPage2.Controls.Add(this.buttonOut);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(230, 202);
            this.tabPage2.Text = "管理";
            // 
            // buttonHide
            // 
            this.buttonHide.Location = new System.Drawing.Point(-4, 101);
            this.buttonHide.Name = "buttonHide";
            this.buttonHide.Size = new System.Drawing.Size(235, 15);
            this.buttonHide.TabIndex = 14;
            this.buttonHide.Text = "︾";
            this.buttonHide.Click += new System.EventHandler(this.buttonHide_Click);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(58, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 17);
            this.label6.Text = "*按F4键显示资产明细!";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // listView1
            // 
            this.listView1.ContextMenu = this.contextMenuShowDetail;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(-4, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(238, 101);
            this.listView1.TabIndex = 1;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // labelHit
            // 
            this.labelHit.Location = new System.Drawing.Point(58, 123);
            this.labelHit.Name = "labelHit";
            this.labelHit.Size = new System.Drawing.Size(169, 21);
            this.labelHit.Text = "总共: 0，有效: 0 ";
            // 
            // buttonTransfer
            // 
            this.buttonTransfer.Location = new System.Drawing.Point(171, 174);
            this.buttonTransfer.Name = "buttonTransfer";
            this.buttonTransfer.Size = new System.Drawing.Size(54, 26);
            this.buttonTransfer.TabIndex = 0;
            this.buttonTransfer.Text = "转出";
            this.buttonTransfer.Click += new System.EventHandler(this.buttonTransfer_Click);
            // 
            // buttonRepair
            // 
            this.buttonRepair.Location = new System.Drawing.Point(2, 146);
            this.buttonRepair.Name = "buttonRepair";
            this.buttonRepair.Size = new System.Drawing.Size(54, 26);
            this.buttonRepair.TabIndex = 0;
            this.buttonRepair.Text = "维修";
            this.buttonRepair.Click += new System.EventHandler(this.buttonRepair_Click);
            // 
            // buttonBorrow
            // 
            this.buttonBorrow.Location = new System.Drawing.Point(58, 146);
            this.buttonBorrow.Name = "buttonBorrow";
            this.buttonBorrow.Size = new System.Drawing.Size(54, 26);
            this.buttonBorrow.TabIndex = 0;
            this.buttonBorrow.Text = "借用";
            this.buttonBorrow.Click += new System.EventHandler(this.buttonBorrow_Click);
            // 
            // buttonDiscard
            // 
            this.buttonDiscard.Location = new System.Drawing.Point(115, 174);
            this.buttonDiscard.Name = "buttonDiscard";
            this.buttonDiscard.Size = new System.Drawing.Size(54, 26);
            this.buttonDiscard.TabIndex = 0;
            this.buttonDiscard.Text = "报废";
            this.buttonDiscard.Click += new System.EventHandler(this.buttonDiscard_Click);
            // 
            // buttonRent
            // 
            this.buttonRent.Location = new System.Drawing.Point(171, 146);
            this.buttonRent.Name = "buttonRent";
            this.buttonRent.Size = new System.Drawing.Size(54, 26);
            this.buttonRent.TabIndex = 0;
            this.buttonRent.Text = "租还";
            this.buttonRent.Click += new System.EventHandler(this.buttonRent_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(2, 119);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(54, 26);
            this.buttonSend.TabIndex = 0;
            this.buttonSend.Text = "领用";
            this.buttonSend.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonLose
            // 
            this.buttonLose.Location = new System.Drawing.Point(58, 174);
            this.buttonLose.Name = "buttonLose";
            this.buttonLose.Size = new System.Drawing.Size(54, 26);
            this.buttonLose.TabIndex = 0;
            this.buttonLose.Text = "丢失";
            this.buttonLose.Click += new System.EventHandler(this.buttonLose_Click);
            // 
            // buttonReject
            // 
            this.buttonReject.Location = new System.Drawing.Point(2, 174);
            this.buttonReject.Name = "buttonReject";
            this.buttonReject.Size = new System.Drawing.Size(54, 26);
            this.buttonReject.TabIndex = 0;
            this.buttonReject.Text = "退返";
            this.buttonReject.Click += new System.EventHandler(this.buttonReject_Click);
            // 
            // buttonOut
            // 
            this.buttonOut.Location = new System.Drawing.Point(115, 146);
            this.buttonOut.Name = "buttonOut";
            this.buttonOut.Size = new System.Drawing.Size(54, 26);
            this.buttonOut.TabIndex = 0;
            this.buttonOut.Text = "外出";
            this.buttonOut.Click += new System.EventHandler(this.buttonOut_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.labelMsg);
            this.tabPage1.Controls.Add(this.textBoxEpc);
            this.tabPage1.Controls.Add(this.textBoxTid);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(230, 202);
            this.tabPage1.Text = "采集";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(24, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 22);
            this.label5.Text = "*按F4键显示资产明细!";
            // 
            // labelMsg
            // 
            this.labelMsg.Location = new System.Drawing.Point(60, 127);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(158, 22);
            this.labelMsg.Text = "labelMsg";
            // 
            // textBoxEpc
            // 
            this.textBoxEpc.Location = new System.Drawing.Point(61, 22);
            this.textBoxEpc.Multiline = true;
            this.textBoxEpc.Name = "textBoxEpc";
            this.textBoxEpc.Size = new System.Drawing.Size(121, 45);
            this.textBoxEpc.TabIndex = 1;
            this.textBoxEpc.Text = "112233445566778899001122";
            // 
            // textBoxTid
            // 
            this.textBoxTid.Location = new System.Drawing.Point(60, 81);
            this.textBoxTid.Multiline = true;
            this.textBoxTid.Name = "textBoxTid";
            this.textBoxTid.Size = new System.Drawing.Size(121, 45);
            this.textBoxTid.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 20);
            this.label1.Text = "EPC:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(24, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 20);
            this.label2.Text = "TID:";
            // 
            // tabControl1
            // 
            this.tabControl1.ContextMenu = this.contextMenuShowDetail;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(238, 231);
            this.tabControl1.TabIndex = 11;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.buttonHideCheck);
            this.tabPage4.Controls.Add(this.listView2);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.comboBoxInvListNo);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.comboBoxMan);
            this.tabPage4.Controls.Add(this.comboBoxAddr);
            this.tabPage4.Controls.Add(this.comboBoxDept);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(230, 202);
            this.tabPage4.Text = "盘点";
            // 
            // buttonHideCheck
            // 
            this.buttonHideCheck.Location = new System.Drawing.Point(-2, 75);
            this.buttonHideCheck.Name = "buttonHideCheck";
            this.buttonHideCheck.Size = new System.Drawing.Size(235, 15);
            this.buttonHideCheck.TabIndex = 15;
            this.buttonHideCheck.Text = "︽";
            this.buttonHideCheck.Click += new System.EventHandler(this.buttonHideCheck_Click);
            // 
            // listView2
            // 
            this.listView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView2.ContextMenu = this.contextMenuShowDetail;
            this.listView2.FullRowSelect = true;
            this.listView2.Location = new System.Drawing.Point(-2, 91);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(236, 90);
            this.listView2.TabIndex = 2;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(169, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 20);
            this.label8.Text = "总数 0";
            // 
            // comboBoxInvListNo
            // 
            this.comboBoxInvListNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxInvListNo.Location = new System.Drawing.Point(42, 2);
            this.comboBoxInvListNo.Name = "comboBoxInvListNo";
            this.comboBoxInvListNo.Size = new System.Drawing.Size(122, 23);
            this.comboBoxInvListNo.TabIndex = 8;
            this.comboBoxInvListNo.SelectedIndexChanged += new System.EventHandler(this.comboBoxInvListNo_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 20);
            this.label7.Text = "清单：";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(226, 20);
            this.label4.Text = "总计 0;已盘 0;异常 0;未盘点 0.";
            // 
            // comboBoxMan
            // 
            this.comboBoxMan.Location = new System.Drawing.Point(166, 50);
            this.comboBoxMan.Name = "comboBoxMan";
            this.comboBoxMan.Size = new System.Drawing.Size(61, 23);
            this.comboBoxMan.TabIndex = 4;
            this.comboBoxMan.SelectedIndexChanged += new System.EventHandler(this.comboBoxMan_SelectedIndexChanged);
            // 
            // comboBoxAddr
            // 
            this.comboBoxAddr.Location = new System.Drawing.Point(41, 50);
            this.comboBoxAddr.Name = "comboBoxAddr";
            this.comboBoxAddr.Size = new System.Drawing.Size(123, 23);
            this.comboBoxAddr.TabIndex = 4;
            this.comboBoxAddr.SelectedIndexChanged += new System.EventHandler(this.comboBoxAddr_SelectedIndexChanged);
            // 
            // comboBoxDept
            // 
            this.comboBoxDept.Location = new System.Drawing.Point(41, 26);
            this.comboBoxDept.Name = "comboBoxDept";
            this.comboBoxDept.Size = new System.Drawing.Size(185, 23);
            this.comboBoxDept.TabIndex = 4;
            this.comboBoxDept.SelectedIndexChanged += new System.EventHandler(this.comboBoxDept_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(0, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 19);
            this.label9.Text = "地点：";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(-1, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 19);
            this.label3.Text = "部门：";
            // 
            // Read2PcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 268);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonRead);
            this.Controls.Add(this.tabControl1);
            this.KeyPreview = true;
            this.Name = "Read2PcForm";
            this.Text = "Read2PcForm";
            this.Load += new System.EventHandler(this.Read2PcForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Read2PcForm_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Read2PcForm_KeyUp);
            this.tabPage5.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRead;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.ContextMenu contextMenuShowDetail;
        private System.Windows.Forms.MenuItem menuItemShowDetail;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buttonSyncIni;
        private System.Windows.Forms.Button buttonSyncDiff;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonHide;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label labelHit;
        private System.Windows.Forms.Button buttonTransfer;
        private System.Windows.Forms.Button buttonRepair;
        private System.Windows.Forms.Button buttonBorrow;
        private System.Windows.Forms.Button buttonDiscard;
        private System.Windows.Forms.Button buttonRent;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonLose;
        private System.Windows.Forms.Button buttonReject;
        private System.Windows.Forms.Button buttonOut;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelMsg;
        private System.Windows.Forms.TextBox textBoxEpc;
        private System.Windows.Forms.TextBox textBoxTid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxInvListNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxMan;
        private System.Windows.Forms.ComboBox comboBoxDept;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ComboBox comboBoxAddr;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonHideCheck;
    }
}