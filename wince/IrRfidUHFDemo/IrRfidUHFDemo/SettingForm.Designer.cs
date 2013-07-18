namespace IrRfidUHFDemo
{
    partial class SettingForm
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
            this.buttonSaveSet = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxIp = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxWebSrvIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxClientId = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSaveSet
            // 
            this.buttonSaveSet.Location = new System.Drawing.Point(74, 229);
            this.buttonSaveSet.Name = "buttonSaveSet";
            this.buttonSaveSet.Size = new System.Drawing.Size(80, 39);
            this.buttonSaveSet.TabIndex = 18;
            this.buttonSaveSet.Text = "保存(Ent)";
            this.buttonSaveSet.Click += new System.EventHandler(this.buttonSaveSet_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(14, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 20);
            this.label3.Text = "端口:";
            // 
            // textBoxIp
            // 
            this.textBoxIp.Location = new System.Drawing.Point(71, 157);
            this.textBoxIp.Name = "textBoxIp";
            this.textBoxIp.Size = new System.Drawing.Size(112, 23);
            this.textBoxIp.TabIndex = 16;
            this.textBoxIp.Text = "textBoxIp";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(71, 186);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(112, 23);
            this.textBoxPort.TabIndex = 17;
            this.textBoxPort.Text = "textBoxPort";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(157, 228);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 39);
            this.buttonCancel.TabIndex = 18;
            this.buttonCancel.Text = "返回(F3)";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(14, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.Text = "IP:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 20);
            this.label1.Text = "-- WebService设置 ---------------------";
            // 
            // textBoxWebSrvIp
            // 
            this.textBoxWebSrvIp.Location = new System.Drawing.Point(69, 93);
            this.textBoxWebSrvIp.Name = "textBoxWebSrvIp";
            this.textBoxWebSrvIp.Size = new System.Drawing.Size(109, 23);
            this.textBoxWebSrvIp.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(2, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 20);
            this.label2.Text = "-- 客户端编号 ----------------------";
            // 
            // comboBoxClientId
            // 
            this.comboBoxClientId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxClientId.Items.Add("#1");
            this.comboBoxClientId.Items.Add("#2");
            this.comboBoxClientId.Items.Add("#3");
            this.comboBoxClientId.Items.Add("#4");
            this.comboBoxClientId.Items.Add("#5");
            this.comboBoxClientId.Location = new System.Drawing.Point(69, 34);
            this.comboBoxClientId.Name = "comboBoxClientId";
            this.comboBoxClientId.Size = new System.Drawing.Size(86, 23);
            this.comboBoxClientId.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(14, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 20);
            this.label5.Text = "IP:";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(14, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 20);
            this.label7.Text = "编号:";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(2, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(210, 20);
            this.label8.Text = "-- UDP通信设置 ---------------------";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 268);
            this.Controls.Add(this.comboBoxClientId);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSaveSet);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxWebSrvIp);
            this.Controls.Add(this.textBoxIp);
            this.Controls.Add(this.textBoxPort);
            this.KeyPreview = true;
            this.Name = "SettingForm";
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.Setting_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SettingForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSaveSet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxIp;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxWebSrvIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxClientId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}