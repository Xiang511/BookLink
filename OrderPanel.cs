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
    public partial class OrderPanel : UserControl
    {
        public OrderPanel()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Image, new Size(38, 38));


            // 載入使用者訂單資料到 DataGridView
            LoadUserOrders();
            // 顯示使用者名稱和訂單計數
            LblUsername.Text = globalVal.Username;
            LblOrderCount.Text = globalVal.UserOrderCount.ToString();
        }
        /// <summary>
        /// 載入使用者訂單資料到 DataGridView
        /// </summary>
        private void LoadUserOrders()
        {
            try
            {
                // 取得訂單資料
                DataTable ordersData = DatabaseHelper.GetUserOrders(globalVal.Username);

                // 綁定到 DataGridView
                dataGridView1.DataSource = ordersData;

                // 設定 DataGridView 外觀
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"載入訂單資料時發生錯誤：\n{ex.Message}",
                    "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 設定 DataGridView 的外觀和格式
        /// </summary>
        private void ConfigureDataGridView()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                // 設定欄位標題
                dataGridView1.Columns["OrderId"].HeaderText = "訂單編號";
                dataGridView1.Columns["ProductName"].HeaderText = "產品名稱";
                dataGridView1.Columns["Quantity"].HeaderText = "數量";
                dataGridView1.Columns["Price"].HeaderText = "單價";
                dataGridView1.Columns["TotalAmount"].HeaderText = "總金額";
                dataGridView1.Columns["OrderDate"].HeaderText = "訂購日期";
                dataGridView1.Columns["Status"].HeaderText = "狀態";

                // 設定欄位寬度
                dataGridView1.Columns["OrderId"].Width = 80;
                dataGridView1.Columns["ProductName"].Width = 200;
                dataGridView1.Columns["Quantity"].Width = 80;
                dataGridView1.Columns["Price"].Width = 100;
                dataGridView1.Columns["TotalAmount"].Width = 100;
                dataGridView1.Columns["OrderDate"].Width = 150;
                dataGridView1.Columns["Status"].Width = 100;

                // 設定數字和日期格式
                dataGridView1.Columns["Price"].DefaultCellStyle.Format = "C2"; // 貨幣格式
                dataGridView1.Columns["TotalAmount"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["OrderDate"].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm";

                // 設定對齊方式
                dataGridView1.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                // 設定整體外觀
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;

                // 設定交替行顏色
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
