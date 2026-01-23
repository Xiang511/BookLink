namespace Online_Ordering_System
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.login = new MaterialSkin.Controls.MaterialButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.title = new System.Windows.Forms.Label();
            this.subTitle = new System.Windows.Forms.Label();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.input_pwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.input_username = new System.Windows.Forms.TextBox();
            this.googleLogin = new MaterialSkin.Controls.MaterialButton();
            this.registerBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // login
            // 
            this.login.AutoSize = false;
            this.login.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.login.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.login.Depth = 0;
            this.login.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login.HighEmphasis = true;
            this.login.Icon = null;
            this.login.Location = new System.Drawing.Point(618, 431);
            this.login.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.login.MouseState = MaterialSkin.MouseState.HOVER;
            this.login.Name = "login";
            this.login.NoAccentTextColor = System.Drawing.Color.Empty;
            this.login.Size = new System.Drawing.Size(408, 36);
            this.login.TabIndex = 0;
            this.login.Text = "Login";
            this.login.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.login.UseAccentColor = false;
            this.login.UseVisualStyleBackColor = true;
            this.login.Click += new System.EventHandler(this.materialButton1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Online_Ordering_System.Properties.Resources.undraw_login_weas;
            this.pictureBox1.Location = new System.Drawing.Point(104, 106);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(480, 469);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Consolas", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(590, 82);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(471, 112);
            this.title.TabIndex = 2;
            this.title.Text = "Welcome!";
            // 
            // subTitle
            // 
            this.subTitle.AutoSize = true;
            this.subTitle.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subTitle.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.subTitle.Location = new System.Drawing.Point(603, 194);
            this.subTitle.Name = "subTitle";
            this.subTitle.Size = new System.Drawing.Size(255, 34);
            this.subTitle.TabIndex = 3;
            this.subTitle.Text = "Login to access";
            this.subTitle.Click += new System.EventHandler(this.label1_Click);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(690, 326);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(1, 0);
            this.materialLabel1.TabIndex = 4;
            // 
            // input_pwd
            // 
            this.input_pwd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.input_pwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.input_pwd.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.input_pwd.ForeColor = System.Drawing.SystemColors.MenuText;
            this.input_pwd.Location = new System.Drawing.Point(739, 361);
            this.input_pwd.Name = "input_pwd";
            this.input_pwd.PasswordChar = '*';
            this.input_pwd.Size = new System.Drawing.Size(289, 33);
            this.input_pwd.TabIndex = 6;
            this.input_pwd.Text = "123456";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(615, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 28);
            this.label1.TabIndex = 8;
            this.label1.Text = "username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(615, 361);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 28);
            this.label2.TabIndex = 9;
            this.label2.Text = "password";
            // 
            // input_username
            // 
            this.input_username.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.input_username.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.input_username.ForeColor = System.Drawing.SystemColors.MenuText;
            this.input_username.Location = new System.Drawing.Point(739, 294);
            this.input_username.Name = "input_username";
            this.input_username.Size = new System.Drawing.Size(287, 32);
            this.input_username.TabIndex = 7;
            this.input_username.Text = "user";
            this.input_username.TextChanged += new System.EventHandler(this.input_username_TextChanged);
            // 
            // googleLogin
            // 
            this.googleLogin.AutoSize = false;
            this.googleLogin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.googleLogin.BackColor = System.Drawing.Color.IndianRed;
            this.googleLogin.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.googleLogin.Depth = 0;
            this.googleLogin.Font = new System.Drawing.Font("Consolas", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.googleLogin.HighEmphasis = true;
            this.googleLogin.Icon = null;
            this.googleLogin.Location = new System.Drawing.Point(618, 491);
            this.googleLogin.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.googleLogin.MouseState = MaterialSkin.MouseState.HOVER;
            this.googleLogin.Name = "googleLogin";
            this.googleLogin.NoAccentTextColor = System.Drawing.Color.Empty;
            this.googleLogin.Size = new System.Drawing.Size(408, 36);
            this.googleLogin.TabIndex = 10;
            this.googleLogin.Text = "Google";
            this.googleLogin.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.googleLogin.UseAccentColor = false;
            this.googleLogin.UseVisualStyleBackColor = false;
            // 
            // registerBtn
            // 
            this.registerBtn.BackColor = System.Drawing.SystemColors.Window;
            this.registerBtn.FlatAppearance.BorderSize = 0;
            this.registerBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.registerBtn.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerBtn.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.registerBtn.Location = new System.Drawing.Point(666, 552);
            this.registerBtn.Name = "registerBtn";
            this.registerBtn.Size = new System.Drawing.Size(345, 23);
            this.registerBtn.TabIndex = 11;
            this.registerBtn.Text = "Don\'t have an account ?  Register";
            this.registerBtn.UseVisualStyleBackColor = false;
            this.registerBtn.Click += new System.EventHandler(this.registerBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1130, 641);
            this.Controls.Add(this.registerBtn);
            this.Controls.Add(this.googleLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.input_pwd);
            this.Controls.Add(this.input_username);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.subTitle);
            this.Controls.Add(this.title);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.login);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "login";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialButton login;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label subTitle;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private System.Windows.Forms.TextBox input_pwd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox input_username;
        private MaterialSkin.Controls.MaterialButton googleLogin;
        private System.Windows.Forms.Button registerBtn;
    }
}

