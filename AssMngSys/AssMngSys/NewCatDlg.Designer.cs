namespace AssMngSys
{
    partial class NewCatDlg
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxTyp = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxAssNam = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCatNo = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "资产类别：";
            // 
            // comboBoxTyp
            // 
            this.comboBoxTyp.FormattingEnabled = true;
            this.comboBoxTyp.Location = new System.Drawing.Point(167, 78);
            this.comboBoxTyp.Name = "comboBoxTyp";
            this.comboBoxTyp.Size = new System.Drawing.Size(167, 20);
            this.comboBoxTyp.TabIndex = 5;
            this.comboBoxTyp.SelectionChangeCommitted += new System.EventHandler(this.comboBoxCatName_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "资产名称：";
            // 
            // comboBoxAssNam
            // 
            this.comboBoxAssNam.FormattingEnabled = true;
            this.comboBoxAssNam.Location = new System.Drawing.Point(167, 119);
            this.comboBoxAssNam.Name = "comboBoxAssNam";
            this.comboBoxAssNam.Size = new System.Drawing.Size(167, 20);
            this.comboBoxAssNam.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "类别编码：";
            // 
            // textBoxCatNo
            // 
            this.textBoxCatNo.Location = new System.Drawing.Point(167, 40);
            this.textBoxCatNo.Name = "textBoxCatNo";
            this.textBoxCatNo.Size = new System.Drawing.Size(167, 21);
            this.textBoxCatNo.TabIndex = 6;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Image = global::AssMngSys.Properties.Resources.stop;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(232, 180);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(122, 36);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "    取消(&Q)";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Image = global::AssMngSys.Properties.Resources.save;
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.Location = new System.Drawing.Point(101, 180);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(122, 36);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "    保存(&S)";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // NewCatDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 248);
            this.Controls.Add(this.textBoxCatNo);
            this.Controls.Add(this.comboBoxAssNam);
            this.Controls.Add(this.comboBoxTyp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Name = "NewCatDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "资产类别维护";
            this.Load += new System.EventHandler(this.NewCatDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxTyp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxAssNam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCatNo;
    }
}