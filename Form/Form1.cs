using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Online_Ordering_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void input_username_TextChanged(object sender, EventArgs e)
        {
    
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void input_username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                input_pwd.Focus();
            }
        }

        private void input_pwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                materialButton1_Click(sender, e);
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            string username = input_username.Text.Trim();
            string password = input_pwd.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("請輸入用戶名", "登入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                input_username.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("請輸入密碼", "登入錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                input_pwd.Focus();
                return;
            }

            if (ValidateLogin(username, password))
            {
                MessageBox.Show("登入成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form3 mainForm = new Form3();
                globalVal.islogin = true;
                UserProfile.Username = username;
                //mainForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("用戶名或密碼錯誤", "登入失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                input_pwd.Clear();
                input_username.Focus();
            }
        }

        private bool ValidateLogin(string username, string password)
        {
            return DatabaseHelper.ValidateUser(username, password);
        }


        private void registerBtn_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
            




        }
    }
}
