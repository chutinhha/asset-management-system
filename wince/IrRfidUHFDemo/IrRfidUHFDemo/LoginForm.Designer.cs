namespace IrRfidUHFDemo
{
    partial class LoginForm
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
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemSetting = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemPowerRate = new System.Windows.Forms.MenuItem();
            this.menuItemModeSet = new System.Windows.Forms.MenuItem();
            this.menuItemSetQ = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemRead = new System.Windows.Forms.MenuItem();
            this.menuItemInv = new System.Windows.Forms.MenuItem();
            this.menuItemLockTag = new System.Windows.Forms.MenuItem();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.checkBoxStorePass = new System.Windows.Forms.CheckBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem6);
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem6
            // 
            this.menuItem6.MenuItems.Add(this.menuItem10);
            this.menuItem6.MenuItems.Add(this.menuItem7);
            this.menuItem6.Text = "开始";
            // 
            // menuItem10
            // 
            this.menuItem10.Text = "开始";
            this.menuItem10.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Text = "结束";
            this.menuItem7.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemSetting);
            this.menuItem1.MenuItems.Add(this.menuItem3);
            this.menuItem1.MenuItems.Add(this.menuItemPowerRate);
            this.menuItem1.MenuItems.Add(this.menuItemModeSet);
            this.menuItem1.MenuItems.Add(this.menuItemSetQ);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.Text = "设置";
            // 
            // menuItemSetting
            // 
            this.menuItemSetting.Text = "同步设置";
            this.menuItemSetting.Click += new System.EventHandler(this.menuItemSetting_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "-";
            // 
            // menuItemPowerRate
            // 
            this.menuItemPowerRate.Text = "功率设置";
            this.menuItemPowerRate.Click += new System.EventHandler(this.menuItemPowerRate_Click);
            // 
            // menuItemModeSet
            // 
            this.menuItemModeSet.Text = "模式设置";
            this.menuItemModeSet.Click += new System.EventHandler(this.menuItemModeSet_Click);
            // 
            // menuItemSetQ
            // 
            this.menuItemSetQ.Text = "Q值设置";
            this.menuItemSetQ.Click += new System.EventHandler(this.menuItemSetQ_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "-";
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.menuItemRead);
            this.menuItem2.MenuItems.Add(this.menuItemInv);
            this.menuItem2.MenuItems.Add(this.menuItemLockTag);
            this.menuItem2.Text = "其它测试";
            // 
            // menuItemRead
            // 
            this.menuItemRead.Text = "单标签读取";
            this.menuItemRead.Click += new System.EventHandler(this.menuItemRead_Click);
            // 
            // menuItemInv
            // 
            this.menuItemInv.Text = "循环读取";
            this.menuItemInv.Click += new System.EventHandler(this.menuItemInv_Click);
            // 
            // menuItemLockTag
            // 
            this.menuItemLockTag.Text = "锁定标签";
            this.menuItemLockTag.Click += new System.EventHandler(this.menuItemLockTag_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(159, 229);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(76, 36);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "结束(F3)";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(31, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 73);
            this.label1.Text = "账号：";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(30, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 19);
            this.label2.Text = "密码：";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(77, 81);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(129, 23);
            this.textBoxUser.TabIndex = 17;
            this.textBoxUser.Text = "000";
            // 
            // textBoxPass
            // 
            this.textBoxPass.Location = new System.Drawing.Point(77, 110);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.PasswordChar = '*';
            this.textBoxPass.Size = new System.Drawing.Size(129, 23);
            this.textBoxPass.TabIndex = 17;
            this.textBoxPass.Text = "123";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(79, 229);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(76, 36);
            this.buttonLogin.TabIndex = 14;
            this.buttonLogin.Text = "登录(Ent)";
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // checkBoxStorePass
            // 
            this.checkBoxStorePass.Location = new System.Drawing.Point(74, 137);
            this.checkBoxStorePass.Name = "checkBoxStorePass";
            this.checkBoxStorePass.Size = new System.Drawing.Size(100, 20);
            this.checkBoxStorePass.TabIndex = 18;
            this.checkBoxStorePass.Text = "记住密码";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(0, 229);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(76, 36);
            this.buttonStart.TabIndex = 14;
            this.buttonStart.Text = "开始(F1)";
            this.buttonStart.Visible = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 268);
            this.Controls.Add(this.checkBoxStorePass);
            this.Controls.Add(this.textBoxPass);
            this.Controls.Add(this.textBoxUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.buttonCancel);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "LoginForm";
            this.Text = "金溢RFID资产管理系统";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.Closed += new System.EventHandler(this.LoginForm_Closed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemModeSet;
        private System.Windows.Forms.MenuItem menuItemSetQ;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxPass;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.CheckBox checkBoxStorePass;
        private System.Windows.Forms.MenuItem menuItemSetting;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItemRead;
        private System.Windows.Forms.MenuItem menuItemInv;
        private System.Windows.Forms.MenuItem menuItemLockTag;
        private System.Windows.Forms.MenuItem menuItemPowerRate;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.Button buttonStart;
    }
}

