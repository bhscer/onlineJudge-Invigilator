namespace Invigilator
{
    partial class FormLogin
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.textBoxPwd = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_apiurl = new System.Windows.Forms.TextBox();
            this.textBox_weburl = new System.Windows.Forms.TextBox();
            this.textBox_wsurl = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonSetLocal = new System.Windows.Forms.Button();
            this.buttonSetDemo = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.buttonViewPwd = new System.Windows.Forms.Button();
            this.buttonVm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(-11, -2);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(720, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "注册完成，等待登录...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(49, 359);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 46);
            this.label2.TabIndex = 1;
            this.label2.Text = "考号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(49, 418);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 46);
            this.label3.TabIndex = 2;
            this.label3.Text = "密码";
            // 
            // textBoxUserId
            // 
            this.textBoxUserId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUserId.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxUserId.Location = new System.Drawing.Point(149, 357);
            this.textBoxUserId.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.Size = new System.Drawing.Size(462, 55);
            this.textBoxUserId.TabIndex = 3;
            // 
            // textBoxPwd
            // 
            this.textBoxPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPwd.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPwd.Location = new System.Drawing.Point(149, 416);
            this.textBoxPwd.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.textBoxPwd.Name = "textBoxPwd";
            this.textBoxPwd.Size = new System.Drawing.Size(462, 55);
            this.textBoxPwd.TabIndex = 4;
            this.textBoxPwd.UseSystemPasswordChar = true;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonLogin.Location = new System.Drawing.Point(211, 590);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(282, 74);
            this.buttonLogin.TabIndex = 5;
            this.buttonLogin.Text = "登录";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(-5, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(714, 133);
            this.label4.TabIndex = 6;
            this.label4.Text = "Invigilator";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox_apiurl
            // 
            this.textBox_apiurl.Location = new System.Drawing.Point(149, 696);
            this.textBox_apiurl.Name = "textBox_apiurl";
            this.textBox_apiurl.Size = new System.Drawing.Size(383, 28);
            this.textBox_apiurl.TabIndex = 7;
            this.textBox_apiurl.Text = "https://oj-api.bhscer.com";
            this.textBox_apiurl.Visible = false;
            // 
            // textBox_weburl
            // 
            this.textBox_weburl.Location = new System.Drawing.Point(149, 730);
            this.textBox_weburl.Name = "textBox_weburl";
            this.textBox_weburl.Size = new System.Drawing.Size(383, 28);
            this.textBox_weburl.TabIndex = 8;
            this.textBox_weburl.Text = "https://oj.bhscer.com";
            this.textBox_weburl.Visible = false;
            // 
            // textBox_wsurl
            // 
            this.textBox_wsurl.Location = new System.Drawing.Point(149, 764);
            this.textBox_wsurl.Name = "textBox_wsurl";
            this.textBox_wsurl.Size = new System.Drawing.Size(383, 28);
            this.textBox_wsurl.TabIndex = 9;
            this.textBox_wsurl.Text = "wss://oj-api.bhscer.com/ws";
            this.textBox_wsurl.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(81, 696);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 18);
            this.label5.TabIndex = 10;
            this.label5.Text = "api:";
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(81, 730);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = "web:";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(81, 767);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 18);
            this.label7.TabIndex = 12;
            this.label7.Text = "ws:";
            this.label7.Visible = false;
            // 
            // buttonSetLocal
            // 
            this.buttonSetLocal.Location = new System.Drawing.Point(560, 700);
            this.buttonSetLocal.Name = "buttonSetLocal";
            this.buttonSetLocal.Size = new System.Drawing.Size(110, 24);
            this.buttonSetLocal.TabIndex = 13;
            this.buttonSetLocal.Text = "开发环境";
            this.buttonSetLocal.UseVisualStyleBackColor = true;
            this.buttonSetLocal.Visible = false;
            this.buttonSetLocal.Click += new System.EventHandler(this.buttonSetLocal_Click);
            // 
            // buttonSetDemo
            // 
            this.buttonSetDemo.Location = new System.Drawing.Point(560, 734);
            this.buttonSetDemo.Name = "buttonSetDemo";
            this.buttonSetDemo.Size = new System.Drawing.Size(110, 27);
            this.buttonSetDemo.TabIndex = 14;
            this.buttonSetDemo.Text = "公网环境";
            this.buttonSetDemo.UseVisualStyleBackColor = true;
            this.buttonSetDemo.Visible = false;
            this.buttonSetDemo.Click += new System.EventHandler(this.buttonSetDemo_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(560, 767);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 25);
            this.button1.TabIndex = 15;
            this.button1.Text = "局域网环境";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(81, 800);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 18);
            this.label8.TabIndex = 16;
            this.label8.Text = "ip:";
            this.label8.Visible = false;
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(149, 797);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(383, 28);
            this.textBox_ip.TabIndex = 17;
            this.textBox_ip.Text = "192.168.110.244";
            this.textBox_ip.Visible = false;
            // 
            // buttonViewPwd
            // 
            this.buttonViewPwd.FlatAppearance.BorderSize = 0;
            this.buttonViewPwd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonViewPwd.Image = global::Invigilator.Properties.Resources.masked_text;
            this.buttonViewPwd.Location = new System.Drawing.Point(614, 418);
            this.buttonViewPwd.Margin = new System.Windows.Forms.Padding(0);
            this.buttonViewPwd.Name = "buttonViewPwd";
            this.buttonViewPwd.Size = new System.Drawing.Size(56, 53);
            this.buttonViewPwd.TabIndex = 18;
            this.buttonViewPwd.UseVisualStyleBackColor = true;
            this.buttonViewPwd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonViewPwd_MouseDown);
            this.buttonViewPwd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonViewPwd_MouseUp);
            // 
            // buttonVm
            // 
            this.buttonVm.Location = new System.Drawing.Point(560, 800);
            this.buttonVm.Name = "buttonVm";
            this.buttonVm.Size = new System.Drawing.Size(110, 25);
            this.buttonVm.TabIndex = 19;
            this.buttonVm.Text = "虚拟机";
            this.buttonVm.UseVisualStyleBackColor = true;
            this.buttonVm.Visible = false;
            this.buttonVm.Click += new System.EventHandler(this.buttonVm_Click);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(698, 944);
            this.Controls.Add(this.buttonVm);
            this.Controls.Add(this.buttonViewPwd);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonSetDemo);
            this.Controls.Add(this.buttonSetLocal);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_wsurl);
            this.Controls.Add(this.textBox_weburl);
            this.Controls.Add(this.textBox_apiurl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.textBoxPwd);
            this.Controls.Add(this.textBoxUserId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormLogin";
            this.Text = "登录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxUserId;
        private System.Windows.Forms.TextBox textBoxPwd;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBox_apiurl;
        public System.Windows.Forms.TextBox textBox_weburl;
        public System.Windows.Forms.TextBox textBox_wsurl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonSetLocal;
        private System.Windows.Forms.Button buttonSetDemo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.Button buttonViewPwd;
        private System.Windows.Forms.Button buttonVm;
    }
}