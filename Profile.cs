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
    public partial class Profile : UserControl
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void Profile_Load(object sender, EventArgs e)
        {   
            DatabaseHelper.GetUserProfile();

            LblUsername.Text = UserProfile.Username;
            LblEmail.Text = UserProfile.Email;
         
            txtEmail.Text = UserProfile.Email;
            LblCreatdate.Text = UserProfile.CreatedDate.ToString("MMMM dd, yyyy");

            string phone = UserProfile.Phone;
            if (string.IsNullOrEmpty(phone))
            {
                txtPhone.Text = "尚未設定電話號碼";
            }
            else
            {
                txtPhone.Text = phone;
            }

            string level = "";
            switch (UserProfile.Role)
            {
                case 1:
                    level = "管理員";
                    break;
                case 2:
                    level = "一般會員";
                    break;
                case 3:
                    level = " ";
                    break;
                default:
                    level = " ";
                    break;
            }
            LblLv.Text = level;
        }

        private void button3_Click(object sender, EventArgs e)
        { //修改資料
            button3.Visible = false;
            button1.Visible = true;
            button2.Visible = true;

            txtEmail.Enabled = true;
            txtPhone.Enabled = true;
            txtPassword.Enabled = true;
            txtNewpassword.Enabled = true;
            txtConfirmpassword.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        { //確認修改
            button3.Visible = true;
            button1.Visible = false;
            button2.Visible = false;

            txtEmail.Enabled = false;
            txtPhone.Enabled = false;
            txtPassword.Enabled = false;
            txtNewpassword.Enabled = false;
            txtConfirmpassword.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        { //取消修改
            LblUsername.Text = UserProfile.Username;
            LblEmail.Text = UserProfile.Email;
            txtPhone.Text = UserProfile.Phone;
            txtEmail.Text = UserProfile.Email;
            LblCreatdate.Text = UserProfile.CreatedDate.ToString("MMMM dd, yyyy");

            button3.Visible = true;
            button1.Visible = false;
            button2.Visible = false;

                        txtEmail.Enabled = false;
            txtPhone.Enabled = false;
            txtPassword.Enabled = false;
            txtNewpassword.Enabled = false;
            txtConfirmpassword.Enabled = false;

        }
    }
}
