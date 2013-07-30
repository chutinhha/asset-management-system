namespace AssMngSysCe
{
    partial class GetReasonForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxEmpNam = new System.Windows.Forms.ComboBox();
            this.comboBoxReason = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxDept = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPri = new System.Windows.Forms.Button();
            this.labelHit = new System.Windows.Forms.Label();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 23);
            this.label2.Text = "人员：";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 23);
            this.label3.Text = "事由：";
            // 
            // comboBoxEmpNam
            // 
            this.comboBoxEmpNam.Location = new System.Drawing.Point(58, 69);
            this.comboBoxEmpNam.Name = "comboBoxEmpNam";
            this.comboBoxEmpNam.Size = new System.Drawing.Size(127, 23);
            this.comboBoxEmpNam.TabIndex = 3;
            this.comboBoxEmpNam.SelectedIndexChanged += new System.EventHandler(this.comboBoxEmpNam_SelectedIndexChanged);
            // 
            // comboBoxReason
            // 
            this.comboBoxReason.Items.Add("事由描述1");
            this.comboBoxReason.Items.Add("事由描述2");
            this.comboBoxReason.Items.Add("事由描述3");
            this.comboBoxReason.Location = new System.Drawing.Point(57, 38);
            this.comboBoxReason.Name = "comboBoxReason";
            this.comboBoxReason.Size = new System.Drawing.Size(158, 23);
            this.comboBoxReason.TabIndex = 3;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(2, 232);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(76, 35);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "领用(F1)";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(158, 232);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(76, 35);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "取消(F3)";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxDept
            // 
            this.textBoxDept.Location = new System.Drawing.Point(58, 98);
            this.textBoxDept.Multiline = true;
            this.textBoxDept.Name = "textBoxDept";
            this.textBoxDept.ReadOnly = true;
            this.textBoxDept.Size = new System.Drawing.Size(158, 56);
            this.textBoxDept.TabIndex = 7;
            this.textBoxDept.Text = "textBox1";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 23);
            this.label1.Text = "部门：";
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(96, 157);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(39, 25);
            this.buttonNext.TabIndex = 9;
            this.buttonNext.Text = ">";
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPri
            // 
            this.buttonPri.Location = new System.Drawing.Point(58, 157);
            this.buttonPri.Name = "buttonPri";
            this.buttonPri.Size = new System.Drawing.Size(35, 25);
            this.buttonPri.TabIndex = 9;
            this.buttonPri.Text = "<";
            this.buttonPri.Click += new System.EventHandler(this.buttonPri_Click);
            // 
            // labelHit
            // 
            this.labelHit.Location = new System.Drawing.Point(143, 157);
            this.labelHit.Name = "labelHit";
            this.labelHit.Size = new System.Drawing.Size(73, 34);
            this.labelHit.Text = "*多部门请选择!";
            this.labelHit.Visible = false;
            // 
            // buttonReturn
            // 
            this.buttonReturn.Location = new System.Drawing.Point(80, 232);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(76, 35);
            this.buttonReturn.TabIndex = 4;
            this.buttonReturn.Text = "退领(Ent)";
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // GetReasonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 268);
            this.Controls.Add(this.labelHit);
            this.Controls.Add(this.buttonPri);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.textBoxDept);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonReturn);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxReason);
            this.Controls.Add(this.comboBoxEmpNam);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.KeyPreview = true;
            this.Name = "GetReasonForm";
            this.Text = "GetReasonForm";
            this.Load += new System.EventHandler(this.GetReasonForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GetReasonForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxEmpNam;
        private System.Windows.Forms.ComboBox comboBoxReason;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxDept;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPri;
        private System.Windows.Forms.Label labelHit;
        private System.Windows.Forms.Button buttonReturn;
    }
}