namespace AssMngSysCe
{
    partial class SelectAssForm
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxNotWrite = new System.Windows.Forms.CheckBox();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(75, 229);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(77, 36);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "确认(Ent)";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // checkBoxNotWrite
            // 
            this.checkBoxNotWrite.Location = new System.Drawing.Point(3, 0);
            this.checkBoxNotWrite.Name = "checkBoxNotWrite";
            this.checkBoxNotWrite.Size = new System.Drawing.Size(165, 27);
            this.checkBoxNotWrite.TabIndex = 3;
            this.checkBoxNotWrite.Text = "只显示未发卡标签";
            this.checkBoxNotWrite.CheckStateChanged += new System.EventHandler(this.checkBoxNotWrite_CheckStateChanged);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(3, 26);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(232, 199);
            this.dataGrid1.TabIndex = 4;
            this.dataGrid1.DoubleClick += new System.EventHandler(this.dataGrid1_DoubleClick);
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            this.dataGrid1.GotFocus += new System.EventHandler(this.dataGrid1_GotFocus);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonCancel.Location = new System.Drawing.Point(156, 229);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(77, 36);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "取消(F3)";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // SelectAssForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 268);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.checkBoxNotWrite);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectAssForm";
            this.Text = "SelectAss";
            this.Load += new System.EventHandler(this.SelectAss_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelectAssForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckBox checkBoxNotWrite;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button buttonCancel;

    }
}