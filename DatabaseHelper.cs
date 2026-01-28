using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace Online_Ordering_System
{
    public class DatabaseHelper
    {   


        private static string connectionString;

        static DatabaseHelper()
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();

            scsb.DataSource = @".";                 // 伺服器地址 ( . 代表本機)
            scsb.InitialCatalog = "OnlineOrderingSystem";           // 資料庫名稱
            scsb.IntegratedSecurity = true;         // 使用 Windows 驗證

            // 若要使用 SQL Server 驗證，則設為 false 並提供帳密：
            // scsb.IntegratedSecurity = false;
            // scsb.UserID = "your_id";
            // scsb.Password = "your_password";

            connectionString = scsb.ConnectionString;
        }

        // 提供一個公開方法來獲取連線對象
        public static SqlConnection GetConnection()
        {   
            return new SqlConnection(connectionString);
        }

        //------------------------------------------------Login start----------------------------------------------

        /// <summary>
        /// 驗證使用者登入
        /// </summary>
        public static bool ValidateUser(string username, string password)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM [User] WHERE username = @Username AND password = @Password";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            // 驗證成功，設定全域變數
                            UserProfile.Username = username;
                            UserProfile.Role = (int)reader["Role"];
                            if(UserProfile.Role == 3)
                            {
                                MessageBox.Show("此帳號已被停用，請聯絡管理員。", "帳號停用", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                reader.Close();
                                return false;
                            }
                            if (UserProfile.Role == 4)
                            {
                                MessageBox.Show("帳號審核中，請耐心等待。", "審核中",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                reader.Close();
                                return false;
                            }
                            return true;
                        }
                        
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("登入驗證錯誤：" + ex.Message, "錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

       

        /// <summary>
        /// 檢查使用者名稱是否已存在
        /// </summary>
        public static bool UserExists(string username)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM [User] WHERE Username = @Username";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("檢查使用者失敗：" + ex.Message, "錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static bool EmailExists(string email)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM [User] WHERE Email = @Email";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("檢查email失敗：" + ex.Message, "錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// 註冊新使用者
        /// </summary>
        public static bool RegisterUser(string username, string password, string email = "")
        {
            try
            {   


                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO [User] (username, password, email, createdate,role) VALUES (@Username, @Password, @Email, @CreatedDate,@role)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@role", 2);

                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("註冊失敗：" + ex.Message, "錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        //------------------------------------------------Login end----------------------------------------------

        //------------------------------------------------Profile start----------------------------------------------
        public static void clearUserProfile()
        {
            UserProfile.UserId = 0;
            UserProfile.Username = null;
            UserProfile.Email = null;
            UserProfile.Password = null;
            UserProfile.Phone = null;
            //UserProfile.Photo = null;
            UserProfile.Role = 0;
            UserProfile.CreatedDate = DateTime.MinValue;
        }

        public static void GetUserProfile()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM [User] WHERE username = @Username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", UserProfile.Username);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UserProfile.UserId = (int)reader["userid"];
                                UserProfile.Email = (string)reader["email"];
                                UserProfile.Password = (string)reader["password"];
                                UserProfile.CreatedDate = (DateTime)reader["createdate"];
                                UserProfile.Role = (int)reader["role"];
                                UserProfile.Phone = reader["phone"] == DBNull.Value ? "" : (string)reader["phone"];
                                UserProfile.Photo = reader["photo"] == DBNull.Value ? "" : (string)reader["photo"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("取得使用者資料失敗：" + ex.Message, "錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
