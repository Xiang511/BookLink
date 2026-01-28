using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

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
                txtPhone.MaxLength = 10;
            }

            string photo = UserProfile.Photo;
            if (string.IsNullOrEmpty(photo))
            {
                Console.WriteLine("未設定大頭貼");
            }
            else
            {
                pictureBox1.ImageLocation = photo;
                txtPic.Text = photo;
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
                    level = "停權";
                    break;
                default:
                    level = "2";
                    break;
            }
            LblLv.Text = level;
        }

        private void button3_Click(object sender, EventArgs e)
        { //修改資料
            button3.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            btnChangePhoto.Visible = true;
            txtPic.Visible = true;


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
            btnChangePhoto.Visible = false;
            txtPic.Visible = false;

            txtEmail.Enabled = false;
            txtPhone.Enabled = false;
            txtPassword.Enabled = false;
            txtNewpassword.Enabled = false;
            txtConfirmpassword.Enabled = false;

            int phone;

            if ((txtEmail.Text != UserProfile.Email) | (txtPhone.Text != UserProfile.Phone))
            {   
                if (string.IsNullOrEmpty(txtEmail.Text))
                {
                    MessageBox.Show("電子郵件不可為空白", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }else if (txtPhone.Text.Length != 10)
                {
                    MessageBox.Show("電話號碼必須是10位數字", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (!int.TryParse(txtPhone.Text, out phone))
                {
                    MessageBox.Show("電話號碼格式錯誤，請輸入數字", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    try
                    {
                        SqlConnection con = DatabaseHelper.GetConnection();
                        con.Open();
                        string updateQuery = "UPDATE [User] SET Email = @Email, Phone = @Phone WHERE UserId = @UserId";
                        SqlCommand cmd = new SqlCommand(updateQuery, con);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@UserId", UserProfile.UserId);
                        int rows = cmd.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine($"共{rows}筆資料更新");
                        MessageBox.Show("基本資料已更新", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UserProfile.Email = txtEmail.Text;
                        UserProfile.Phone = txtPhone.Text;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                    
            }




            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                try
                {
                    if (txtPassword.Text == UserProfile.Password)
                    {
                        string Changepassword = "";

                        if (txtNewpassword.Text == txtConfirmpassword.Text)
                        {
                            Changepassword = txtNewpassword.Text;
                        }
                        else
                        {
                            MessageBox.Show("新密碼與確認密碼不符，無法修改密碼", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        SqlConnection con = DatabaseHelper.GetConnection();
                        con.Open();
                        string updateQuery = "UPDATE [User] SET password = @password WHERE UserId = @UserId";
                        SqlCommand cmd = new SqlCommand(updateQuery, con);
                        cmd.Parameters.AddWithValue("@password", Changepassword.Trim());
                        cmd.Parameters.AddWithValue("@UserId", UserProfile.UserId);
                        int rows = cmd.ExecuteNonQuery();
                        con.Close();
                        Console.WriteLine($"共{rows}筆資料更新");
                        MessageBox.Show("密碼已更新", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtConfirmpassword.Clear();
                        txtNewpassword.Clear();
                        txtPassword.Clear();

                        UserProfile.Password = Changepassword;
                    }
                    else
                    {
                        MessageBox.Show("目前密碼輸入錯誤，無法修改密碼", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtConfirmpassword.Clear();
                        txtNewpassword.Clear();
                        txtPassword.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtConfirmpassword.Clear();
                    txtNewpassword.Clear();
                    txtPassword.Clear();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        { //取消修改
            LblUsername.Text = UserProfile.Username;
            LblEmail.Text = UserProfile.Email;
            txtPhone.Text = UserProfile.Phone;
            txtEmail.Text = UserProfile.Email;
            LblCreatdate.Text = UserProfile.CreatedDate.ToString("MMMM dd, yyyy");
            txtPic.Text = UserProfile.Photo;

            txtConfirmpassword.Clear();
            txtNewpassword.Clear();
            txtPassword.Clear();

            button3.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            btnChangePhoto.Visible = false;
            txtPic.Visible = false;


            txtEmail.Enabled = false;
            txtPhone.Enabled = false;
            txtPassword.Enabled = false;
            txtNewpassword.Enabled = false;
            txtConfirmpassword.Enabled = false;

        }

        private void btnChangePhoto_Click(object sender, EventArgs e)
        {

            UserProfile.Photo = txtPic.Text;
            pictureBox1.ImageLocation = UserProfile.Photo;

            UserProfile.Photo = txtPic.Text;
            SqlConnection con = DatabaseHelper.GetConnection();
            con.Open();
            string updateQuery = "UPDATE [User] SET photo = @photo WHERE UserId = @UserId";
            SqlCommand cmd = new SqlCommand(updateQuery, con);
            cmd.Parameters.AddWithValue("@photo", UserProfile.Photo);
            cmd.Parameters.AddWithValue("@UserId", UserProfile.UserId);
            int rows = cmd.ExecuteNonQuery();
            con.Close();
            Console.WriteLine($"共{rows}筆資料更新");
            MessageBox.Show("大頭貼已更新", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
