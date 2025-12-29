using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace Online_Ordering_System
{
    /// <summary>
    /// 資料庫操作輔助類別 - 使用 ADO.NET
    /// </summary>
    public class DatabaseHelper
    {
        private static string connectionString;

        /// <summary>
        /// 靜態建構子 - 初始化連線字串
        /// </summary>
        static DatabaseHelper()
        {
            try
            {
                // 從 App.config 讀取連線字串
                string configPath = Path.Combine(Application.StartupPath, "Online Ordering System.exe.config");
                if (!File.Exists(configPath))
                {
                    configPath = Path.Combine(Application.StartupPath, "App.config");
                }

                if (File.Exists(configPath))
                {
                    XDocument doc = XDocument.Load(configPath);
                    var connString = doc.Descendants("connectionStrings")
                        .Descendants("add")
                        .Where(x => x.Attribute("name")?.Value == "OnlineOrderingDB")
                        .Select(x => x.Attribute("connectionString")?.Value)
                        .FirstOrDefault();

                    if (!string.IsNullOrEmpty(connString))
                    {
                        connectionString = connString;
                        return;
                    }
                }
            }
            catch
            {
                // 如果讀取失敗，繼續使用預設值
            }

            // 預設使用 SQL Server Express
            connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=OnlineOrderingDB;Integrated Security=True;Connect Timeout=30";
        }

        /// <summary>
        /// 取得資料庫連線
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// 取得目前使用的連線字串（隱藏密碼）
        /// </summary>
        public static string GetConnectionStringInfo()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                builder.Password = "****";
                return builder.ConnectionString;
            }
            catch
            {
                return "無法解析連線字串";
            }
        }

        /// <summary>
        /// 測試資料庫連線
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    MessageBox.Show($"資料庫連線成功！\n\n伺服器：{conn.DataSource}\n資料庫：{conn.Database}", 
                        "連線測試", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"資料庫連線失敗！\n\n錯誤訊息：{ex.Message}\n\n連線字串：{GetConnectionStringInfo()}", 
                    "連線錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// 檢查資料庫是否存在
        /// </summary>
        public static bool DatabaseExists()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

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
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
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
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                    
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
                    string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";

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
                    string query = "INSERT INTO Users (Username, Password, Email, CreatedDate) VALUES (@Username, @Password, @Email, @CreatedDate)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        
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

        /// <summary>
        /// 取得所有產品資料
        /// </summary>
        public static DataTable GetProducts()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string query = "SELECT ProductId, ProductName, Price, Stock, Description FROM Products";
                    
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("載入產品資料失敗：" + ex.Message, "錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// 取得使用者訂單資料
        /// </summary>
        public static DataTable GetUserOrders(string username)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT o.OrderId, o.ProductName, o.Quantity, o.Price, 
                                    o.TotalAmount, o.OrderDate, o.Status 
                                    FROM Orders o 
                                    INNER JOIN Users u ON o.UserId = u.UserId 
                                    WHERE u.Username = @Username 
                                    ORDER BY o.OrderDate DESC";
                    
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@Username", username);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("載入訂單資料失敗：" + ex.Message, "錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// 新增訂單
        /// </summary>
        public static bool AddOrder(string username, string productName, int quantity, decimal price)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    
                    // 取得 UserId
                    string getUserIdQuery = "SELECT UserId FROM Users WHERE Username = @Username";
                    int userId;
                    
                    using (SqlCommand cmd = new SqlCommand(getUserIdQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        userId = (int)cmd.ExecuteScalar();
                    }
                    
                    // 新增訂單
                    string insertQuery = @"INSERT INTO Orders (UserId, ProductName, Quantity, Price, TotalAmount, OrderDate, Status) 
                                          VALUES (@UserId, @ProductName, @Quantity, @Price, @TotalAmount, @OrderDate, @Status)";
                    
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@ProductName", productName);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@TotalAmount", quantity * price);
                        cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Status", "待處理");
                        
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("新增訂單失敗：" + ex.Message, "錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// 初始化資料庫表格 (如果不存在則建立)
        /// </summary>
        public static void InitializeDatabase()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    
                    // 建立 Users 資料表
                    string createUsersTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
                        BEGIN
                            CREATE TABLE Users (
                                UserId INT PRIMARY KEY IDENTITY(1,1),
                                Username NVARCHAR(50) UNIQUE NOT NULL,
                                Password NVARCHAR(255) NOT NULL,
                                Email NVARCHAR(100),
                                CreatedDate DATETIME DEFAULT GETDATE()
                            )
                        END";
                    
                    using (SqlCommand cmd = new SqlCommand(createUsersTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    
                    // 建立 Products 資料表
                    string createProductsTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
                        BEGIN
                            CREATE TABLE Products (
                                ProductId INT PRIMARY KEY IDENTITY(1,1),
                                ProductName NVARCHAR(100) NOT NULL,
                                Price DECIMAL(10,2) NOT NULL,
                                Stock INT DEFAULT 0,
                                Description NVARCHAR(500),
                                CreatedDate DATETIME DEFAULT GETDATE()
                            )
                        END";
                    
                    using (SqlCommand cmd = new SqlCommand(createProductsTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    
                    // 建立 Orders 資料表
                    string createOrdersTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Orders')
                        BEGIN
                            CREATE TABLE Orders (
                                OrderId INT PRIMARY KEY IDENTITY(1,1),
                                UserId INT NOT NULL,
                                ProductName NVARCHAR(100) NOT NULL,
                                Quantity INT NOT NULL,
                                Price DECIMAL(10,2) NOT NULL,
                                TotalAmount DECIMAL(10,2) NOT NULL,
                                OrderDate DATETIME DEFAULT GETDATE(),
                                Status NVARCHAR(20) DEFAULT N'待處理',
                                FOREIGN KEY (UserId) REFERENCES Users(UserId)
                            )
                        END";
                    
                    using (SqlCommand cmd = new SqlCommand(createOrdersTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                string errorMsg = $"初始化資料庫失敗！\n\n";
                errorMsg += $"錯誤代碼：{ex.Number}\n";
                errorMsg += $"錯誤訊息：{ex.Message}\n\n";
                
                // 根據錯誤代碼提供解決建議
                switch (ex.Number)
                {
                    case 53:
                    case 26:
                        errorMsg += "【問題】無法連線到 SQL Server\n";
                        errorMsg += "【建議】\n";
                        errorMsg += "1. 確認 SQL Server 服務是否已啟動\n";
                        errorMsg += "2. 檢查伺服器名稱是否正確\n";
                        errorMsg += "3. 目前連線字串：" + GetConnectionStringInfo();
                        break;
                    case 4060:
                        errorMsg += "【問題】資料庫不存在\n";
                        errorMsg += "【解決方法】請在 SQL Server Management Studio 中執行：\n";
                        errorMsg += "CREATE DATABASE OnlineOrderingDB;";
                        break;
                    case 18456:
                        errorMsg += "【問題】登入失敗\n";
                        errorMsg += "【建議】檢查使用者帳號和密碼是否正確";
                        break;
                    default:
                        errorMsg += $"【詳細資訊】\n{ex.ToString()}";
                        break;
                }
                
                MessageBox.Show(errorMsg, "資料庫初始化錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化資料庫時發生未預期的錯誤：\n\n{ex.Message}\n\n{ex.ToString()}", 
                    "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
