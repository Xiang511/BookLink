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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string email = txtEmail.Text.Trim();

            // 驗證輸入
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("請輸入用戶名", "註冊錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("請輸入密碼", "註冊錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("密碼長度至少需要6個字元", "註冊錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("兩次輸入的密碼不一致", "註冊錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            // 檢查用戶名是否已存在
            if (DatabaseHelper.EmailExists(email))
            {
                MessageBox.Show("此 email 已被使用，請選擇其他 email ", "註冊錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }
            // 1. 檢查帳號是否重複
            if (DatabaseHelper.UserExists(username))
            {
                MessageBox.Show("此帳號名稱已被使用！");
                return;
            }

            // 註冊新使用者
            if (DatabaseHelper.RegisterUser(username, password, email))
            {
                MessageBox.Show("註冊成功！請使用新帳號登入。", "註冊成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
