namespace AssMngSys
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.资产登记ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.领用管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.使用管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.资产盘点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.资产注销ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ttoolStripButtonAssInput = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSupply = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUse = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLogoff = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonInv = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始ToolStripMenuItem,
            this.系统ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1087, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 开始ToolStripMenuItem
            // 
            this.开始ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.资产登记ToolStripMenuItem,
            this.领用管理ToolStripMenuItem,
            this.使用管理ToolStripMenuItem,
            this.资产盘点ToolStripMenuItem,
            this.资产注销ToolStripMenuItem});
            this.开始ToolStripMenuItem.Image = global::AssMngSys.Properties.Resources.home;
            this.开始ToolStripMenuItem.Name = "开始ToolStripMenuItem";
            this.开始ToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.开始ToolStripMenuItem.Text = "菜单(&F)";
            // 
            // 资产登记ToolStripMenuItem
            // 
            this.资产登记ToolStripMenuItem.Image = global::AssMngSys.Properties.Resources.file_edit;
            this.资产登记ToolStripMenuItem.Name = "资产登记ToolStripMenuItem";
            this.资产登记ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.资产登记ToolStripMenuItem.Text = "资产登记(&L)";
            this.资产登记ToolStripMenuItem.Click += new System.EventHandler(this.ttoolStripButtonAssInput_Click);
            // 
            // 领用管理ToolStripMenuItem
            // 
            this.领用管理ToolStripMenuItem.Image = global::AssMngSys.Properties.Resources.imac;
            this.领用管理ToolStripMenuItem.Name = "领用管理ToolStripMenuItem";
            this.领用管理ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.领用管理ToolStripMenuItem.Text = "领用管理(&S)";
            this.领用管理ToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonSupply_Click);
            // 
            // 使用管理ToolStripMenuItem
            // 
            this.使用管理ToolStripMenuItem.Image = global::AssMngSys.Properties.Resources.run;
            this.使用管理ToolStripMenuItem.Name = "使用管理ToolStripMenuItem";
            this.使用管理ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.使用管理ToolStripMenuItem.Text = "使用管理(&U)";
            this.使用管理ToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonUse_Click);
            // 
            // 资产盘点ToolStripMenuItem
            // 
            this.资产盘点ToolStripMenuItem.Image = global::AssMngSys.Properties.Resources.call;
            this.资产盘点ToolStripMenuItem.Name = "资产盘点ToolStripMenuItem";
            this.资产盘点ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.资产盘点ToolStripMenuItem.Text = "资产盘点(&I)";
            this.资产盘点ToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonInv_Click);
            // 
            // 资产注销ToolStripMenuItem
            // 
            this.资产注销ToolStripMenuItem.Image = global::AssMngSys.Properties.Resources.reload;
            this.资产注销ToolStripMenuItem.Name = "资产注销ToolStripMenuItem";
            this.资产注销ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.资产注销ToolStripMenuItem.Text = "资产注销(&O)";
            this.资产注销ToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonLogoff_Click);
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemQuit});
            this.系统ToolStripMenuItem.Image = global::AssMngSys.Properties.Resources.info;
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.系统ToolStripMenuItem.Text = "系统(&S)";
            // 
            // MenuItemQuit
            // 
            this.MenuItemQuit.Image = global::AssMngSys.Properties.Resources.delete;
            this.MenuItemQuit.Name = "MenuItemQuit";
            this.MenuItemQuit.Size = new System.Drawing.Size(112, 22);
            this.MenuItemQuit.Text = "退出(&Q)";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ttoolStripButtonAssInput,
            this.toolStripButtonSupply,
            this.toolStripButtonUse,
            this.toolStripButtonLogoff,
            this.toolStripButtonInv,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(1063, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(24, 572);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ttoolStripButtonAssInput
            // 
            this.ttoolStripButtonAssInput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ttoolStripButtonAssInput.Image = global::AssMngSys.Properties.Resources.file_edit;
            this.ttoolStripButtonAssInput.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ttoolStripButtonAssInput.Name = "ttoolStripButtonAssInput";
            this.ttoolStripButtonAssInput.Size = new System.Drawing.Size(21, 20);
            this.ttoolStripButtonAssInput.Text = "资产登记";
            this.ttoolStripButtonAssInput.Click += new System.EventHandler(this.ttoolStripButtonAssInput_Click);
            // 
            // toolStripButtonSupply
            // 
            this.toolStripButtonSupply.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSupply.Image = global::AssMngSys.Properties.Resources.imac;
            this.toolStripButtonSupply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSupply.Name = "toolStripButtonSupply";
            this.toolStripButtonSupply.Size = new System.Drawing.Size(21, 20);
            this.toolStripButtonSupply.Text = "领用管理";
            this.toolStripButtonSupply.Click += new System.EventHandler(this.toolStripButtonSupply_Click);
            // 
            // toolStripButtonUse
            // 
            this.toolStripButtonUse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUse.Image = global::AssMngSys.Properties.Resources.run;
            this.toolStripButtonUse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUse.Name = "toolStripButtonUse";
            this.toolStripButtonUse.Size = new System.Drawing.Size(21, 20);
            this.toolStripButtonUse.Text = "使用管理";
            this.toolStripButtonUse.Click += new System.EventHandler(this.toolStripButtonUse_Click);
            // 
            // toolStripButtonLogoff
            // 
            this.toolStripButtonLogoff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLogoff.Image = global::AssMngSys.Properties.Resources.reload;
            this.toolStripButtonLogoff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLogoff.Name = "toolStripButtonLogoff";
            this.toolStripButtonLogoff.Size = new System.Drawing.Size(21, 20);
            this.toolStripButtonLogoff.Text = "资产注销";
            this.toolStripButtonLogoff.Click += new System.EventHandler(this.toolStripButtonLogoff_Click);
            // 
            // toolStripButtonInv
            // 
            this.toolStripButtonInv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInv.Image = global::AssMngSys.Properties.Resources.call;
            this.toolStripButtonInv.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInv.Name = "toolStripButtonInv";
            this.toolStripButtonInv.Size = new System.Drawing.Size(21, 20);
            this.toolStripButtonInv.Text = "资产盘点";
            this.toolStripButtonInv.Click += new System.EventHandler(this.toolStripButtonInv_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::AssMngSys.Properties.Resources.delete;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(21, 20);
            this.toolStripButton1.Text = "退出系统";
            this.toolStripButton1.Click += new System.EventHandler(this.MenuItemQuit_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBoxLog);
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Panel2.Resize += new System.EventHandler(this.splitContainer1_Panel2_Resize);
            this.splitContainer1.Size = new System.Drawing.Size(1060, 566);
            this.splitContainer1.SplitterDistance = 159;
            this.splitContainer1.TabIndex = 3;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.Location = new System.Drawing.Point(3, 383);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(151, 183);
            this.textBoxLog.TabIndex = 1;
            this.textBoxLog.TextChanged += new System.EventHandler(this.textBoxLog_TextChanged);
            this.textBoxLog.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxLog_MouseDoubleClick);
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.treeView1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.Indent = 19;
            this.treeView1.ItemHeight = 25;
            this.treeView1.Location = new System.Drawing.Point(3, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(150, 378);
            this.treeView1.StateImageList = this.imageList1;
            this.treeView1.TabIndex = 0;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "file_new.ico");
            this.imageList1.Images.SetKeyName(1, "note pad.ico");
            this.imageList1.Images.SetKeyName(2, "folder.ico");
            this.imageList1.Images.SetKeyName(3, "file open.ico");
            this.imageList1.Images.SetKeyName(4, "add.ico");
            this.imageList1.Images.SetKeyName(5, "ok.ico");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 596);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1087, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 618);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Asset management system";
            this.Load += new System.EventHandler(this.MainWnd_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWnd_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 开始ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton ttoolStripButtonAssInput;
        private System.Windows.Forms.ToolStripButton toolStripButtonSupply;
        private System.Windows.Forms.ToolStripButton toolStripButtonUse;
        private System.Windows.Forms.ToolStripButton toolStripButtonLogoff;
        private System.Windows.Forms.ToolStripButton toolStripButtonInv;
        private System.Windows.Forms.ToolStripMenuItem 资产登记ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 领用管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 使用管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 资产盘点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemQuit;
        private System.Windows.Forms.ToolStripMenuItem 资产注销ToolStripMenuItem;
    }
}

