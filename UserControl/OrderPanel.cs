using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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

        public void ApplyAntdStyle(DataGridView dgv)
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

        void LoadAllMemberDataTodataGridView1()
        {
            SqlConnection con = DatabaseHelper.GetConnection();
            con.Open();
            string strsql = "select * from orders o where userid = @UserId order by o.orderid  desc";
            SqlCommand cmd = new SqlCommand(strsql, con);
            cmd.Parameters.AddWithValue("@UserId", UserProfile.UserId);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
            int Count = dt.Rows.Count;
            Console.WriteLine($"dataGridView1筆數: {Count}");
            reader.Close();
            con.Close();

        }

        void ShowOrderDetail(int orderid)
        {
            listView2.Clear();
            imageList2.Images.Clear();
            OrderList.InfoList.Clear();

            SqlConnection con = DatabaseHelper.GetConnection();
            con.Open();
            string strsql = "select * from Orders o inner join OrderDetail od on o.orderid = od.orderid inner join product p on od.productid = p.productid where o.orderid = @OrderId and o.userid = @UserId ";
            SqlCommand cmd = new SqlCommand(strsql, con);
            cmd.Parameters.AddWithValue("@UserId", UserProfile.UserId);
            cmd.Parameters.AddWithValue("@OrderId", orderid);

            SqlDataReader reader = cmd.ExecuteReader();

            listView2.View = View.Details;
            imageList2.ImageSize = new Size(120, 160);
            listView2.SmallImageList = imageList2;
            listView2.FullRowSelect = true;


            while (reader.Read())
            {
                OrderInfo orderInfo = new OrderInfo();

                orderInfo.productID = (int)reader["productid"];
                orderInfo.productName = (string)reader["productname"];
                orderInfo.productPrice = (decimal)reader["price"];
                orderInfo.productQuantity = (int)reader["quantity"];
                orderInfo.productImage = (string)reader["image"];

                OrderList.InfoList.Add(orderInfo);
                string imageName = (string)reader["image"];
                string ImageDir = globalVal.strImageDir + @"/" + imageName;
                FileStream fs = File.OpenRead(ImageDir);
                Image ProductImg = Image.FromStream(fs);
                imageList2.Images.Add(orderInfo.productID.ToString(), ProductImg);
                fs.Close();

                listView2.Columns.Clear();
                listView2.Items.Clear();
                listView2.Columns.Add("商品圖片", 120);
                listView2.Columns.Add("商品名稱", 120);
                listView2.Columns.Add("單價", 110);
                listView2.Columns.Add("數量", 60);
                listView2.Columns.Add("小計", 110);

                foreach (OrderInfo item in OrderList.InfoList)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.ImageKey = item.productID.ToString();
                    lvi.SubItems.Add(item.productName);

                    lvi.SubItems.Add(item.productPrice.ToString("C")); // 格式化為貨幣
                    lvi.SubItems.Add(item.productQuantity.ToString());
                    lvi.SubItems.Add((item.productPrice * item.productQuantity).ToString("C"));

                    listView2.Items.Add(lvi);

                    lvi.Tag = item.productID;

                    Console.WriteLine($"{item.productName}, {item.productPrice}, {item.productQuantity}");
                }
            }




            reader.Close();
            con.Close();
        }

        private void OrderPanel_Load(object sender, EventArgs e)
        {
            LoadAllMemberDataTodataGridView1();
            ApplyAntdStyle(dataGridView1);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
            {
                int selectid = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;

                ShowOrderDetail(selectid);
            }
        }
    }
}
