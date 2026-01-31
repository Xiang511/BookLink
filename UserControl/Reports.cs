using AntdUI;
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

namespace Online_Ordering_System
{
    public partial class Reports : UserControl
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            // 設定 Segmented 選項
            segmented1.Items.Clear();
            segmented1.Items.Add(new AntdUI.SegmentedItem { Text = "當日統計" });
            segmented1.Items.Add(new AntdUI.SegmentedItem { Text = "當月統計" });
            segmented1.Items.Add(new AntdUI.SegmentedItem { Text = "產品排名" });
            segmented1.SelectIndex = 0;

            // 設定 DataGridView 樣式
            ConfigureDataGridView();

            // 載入當日統計
            LoadDailyReport();
        }

        /// <summary>
        /// 設定 DataGridView 外觀
        /// </summary>
        private void ConfigureDataGridView()
        {
            // 基礎顏色
            // 基礎顏色修正
            Color borderColor = Color.FromArgb(240, 240, 240);
            Color headerBg = Color.White;
            Color textColor = Color.FromArgb(217, 0, 0, 0); // 修正後的深灰色

            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = borderColor;

            // 表頭樣式優化
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 45; // 稍微加高更有呼吸感
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = headerBg;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = textColor;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold); // AntD 常用字體
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerBg;

            // 行樣式優化
            dgv.RowTemplate.Height = 45;
            dgv.RowHeadersVisible = false; // 隱藏最左側的空白列，這是 AntD 的關鍵
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 247, 255);
            dgv.DefaultCellStyle.SelectionForeColor = textColor;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgv.DefaultCellStyle.Padding = new Padding(10, 0, 10, 0); // 增加文字左右間距

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// Segmented 選項變更事件
        /// </summary>
        private void segmented1_SelectIndexChanged(object sender, IntEventArgs e)
        {
            try
            {
                dgv.DataSource = null;
                
                switch (e.Value)
                {
                    case 0: // 當日統計
                        LoadDailyReport();
                        break;
                    case 1: // 當月統計
                        LoadMonthlyReport();
                        break;
                    case 2: // 產品排名
                        LoadProductRanking();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"載入報表失敗: {ex.Message}", "錯誤", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 載入當日統計報表
        /// </summary>
        private void LoadDailyReport()
        {
            string query = @"
                SELECT 
                    o.orderid AS '訂單編號',
                    u.username AS '用戶名稱',
                    p.productname AS '產品名稱',
                    od.quantity AS '數量',
                    od.price AS '單價',
                    o.totalamount AS '總金額',
                    CONVERT(VARCHAR(5), o.OrderDate, 108) AS '訂購時間',
                    o.Status AS '狀態'
                FROM Orders o
                INNER JOIN [user] u ON o.userid = u.userid
                INNER JOIN OrderDetail od ON o.orderid = od.orderid
                inner JOIN Product p ON od.productid = p.productid
                
                ORDER BY o.orderdate DESC";

            LoadData(query, "當日統計");
            
            // 顯示當日統計摘要
            ShowDailySummary();
        }

        /// <summary>
        /// 顯示當日統計摘要
        /// </summary>
        private void ShowDailySummary()
        {
            //using (SqlConnection conn = DatabaseHelper.GetConnection())
            //{
            //    conn.Open();
            //    string query = @"
            //        SELECT 
            //            COUNT(orderid) AS TotalOrders,
            //            SUM(quantity) AS TotalQuantity,
            //            SUM(totalamount) AS TotalSales
            //        FROM Orders
            //        WHERE CONVERT(DATE, OrderDate) = CONVERT(DATE, GETDATE())";

            //    using (SqlCommand cmd = new SqlCommand(query, conn))
            //    using (SqlDataReader reader = cmd.ExecuteReader())
            //    {
            //        if (reader.Read())
            //        {
            //            int totalOrders = reader["TotalOrders"] != DBNull.Value ? Convert.ToInt32(reader["TotalOrders"]) : 0;
            //            int totalQuantity = reader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(reader["TotalQuantity"]) : 0;
            //            decimal totalSales = reader["TotalSales"] != DBNull.Value ? Convert.ToDecimal(reader["TotalSales"]) : 0;

            //            label2.Text = $"今日訂單: {totalOrders} 筆 | 銷售數量: {totalQuantity} 件 | 銷售總額: NT$ {totalSales:N0}";
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 載入當月統計報表
        /// </summary>
        private void LoadMonthlyReport()
        {
            //string query = @"
            //    SELECT 
            //        CONVERT(DATE, o.OrderDate) AS '日期',
            //        COUNT(o.OrderId) AS '訂單數量',
            //        SUM(o.Quantity) AS '商品總數',
            //        SUM(o.TotalAmount) AS '銷售總額',
            //        AVG(o.TotalAmount) AS '平均金額'
            //    FROM Orders o
            //    WHERE YEAR(o.OrderDate) = YEAR(GETDATE()) 
            //        AND MONTH(o.OrderDate) = MONTH(GETDATE())
            //    GROUP BY CONVERT(DATE, o.OrderDate)
            //    ORDER BY CONVERT(DATE, o.OrderDate) DESC";

            //LoadData(query, "當月統計");
            
            //// 顯示當月統計摘要
            //ShowMonthlySummary();
        }

        /// <summary>
        /// 顯示當月統計摘要
        /// </summary>
        private void ShowMonthlySummary()
        {
            //using (SqlConnection conn = DatabaseHelper.GetConnection())
            //{
            //    conn.Open();
            //    string query = @"
            //        SELECT 
            //            COUNT(OrderId) AS TotalOrders,
            //            SUM(Quantity) AS TotalQuantity,
            //            SUM(TotalAmount) AS TotalSales
            //        FROM Orders
            //        WHERE YEAR(OrderDate) = YEAR(GETDATE()) 
            //            AND MONTH(OrderDate) = MONTH(GETDATE())";

            //    using (SqlCommand cmd = new SqlCommand(query, conn))
            //    using (SqlDataReader reader = cmd.ExecuteReader())
            //    {
            //        if (reader.Read())
            //        {
            //            int totalOrders = reader["TotalOrders"] != DBNull.Value ? Convert.ToInt32(reader["TotalOrders"]) : 0;
            //            int totalQuantity = reader["TotalQuantity"] != DBNull.Value ? Convert.ToInt32(reader["TotalQuantity"]) : 0;
            //            decimal totalSales = reader["TotalSales"] != DBNull.Value ? Convert.ToDecimal(reader["TotalSales"]) : 0;

            //            label2.Text = $"本月訂單: {totalOrders} 筆 | 銷售數量: {totalQuantity} 件 | 銷售總額: NT$ {totalSales:N0}";
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 載入產品排名報表
        /// </summary>
        private void LoadProductRanking()
        {
            //string query = @"
            //    SELECT 
            //        ROW_NUMBER() OVER (ORDER BY SUM(o.Quantity) DESC) AS '排名',
            //        o.ProductName AS '產品名稱',
            //        COUNT(o.OrderId) AS '訂單次數',
            //        SUM(o.Quantity) AS '銷售數量',
            //        SUM(o.TotalAmount) AS '銷售金額',
            //        AVG(o.Price) AS '平均單價'
            //    FROM Orders o
            //    GROUP BY o.ProductName
            //    ORDER BY SUM(o.Quantity) DESC";

            //LoadData(query, "產品排名");
            
            //label2.Text = "依據銷售數量排序，顯示所有產品的銷售統計";
        }

        /// <summary>
        /// 載入資料到 DataGridView
        /// </summary>
        private void LoadData(string query, string reportType)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // 綁定資料
                        dgv.DataSource = dt;

                        // 格式化欄位
                        FormatColumns(reportType);

                        // 顯示記錄數
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("目前沒有資料", "提示", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"載入資料失敗: {ex.Message}\n\nSQL: {query}", 
                    "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 格式化 DataGridView 欄位
        /// </summary>
        private void FormatColumns(string reportType)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                // 設定欄位對齊方式
                if (col.Name.Contains("編號") || col.Name.Contains("數量") || col.Name.Contains("排名"))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else if (col.Name.Contains("金額") || col.Name.Contains("單價"))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.DefaultCellStyle.Format = "N0"; // 千分位格式
                }

                // 設定欄位寬度
                switch (col.Name)
                {
                    case "訂單編號":
                    case "排名":
                        col.Width = 80;
                        break;
                    case "用戶名稱":
                        col.Width = 120;
                        break;
                    case "產品名稱":
                        col.Width = 200;
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                    case "數量":
                    case "訂單次數":
                    case "銷售數量":
                        col.Width = 100;
                        break;
                    case "訂購時間":
                        col.Width = 100;
                        break;
                    case "狀態":
                        col.Width = 90;
                        break;
                    case "日期":
                        col.Width = 120;
                        col.DefaultCellStyle.Format = "yyyy/MM/dd";
                        break;
                }
            }
        }
    }
}
