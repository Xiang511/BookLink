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

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        void LoadAllMemberDataTodataGridView1()
        {
            SqlConnection con = DatabaseHelper.GetConnection();
            con.Open();
            string strsql = "";
            if (UserProfile.Role != 1)
            {
                strsql = " select orderid,orderdate,totalamount,status from orders where userid = @UserId order by orderdate desc";
            }
            else
            {
                strsql = " select o.orderid, u.userid,u.username ,o.orderdate,o.totalamount,o.status from orders o inner join [user] u on o.userid = u.userid order by o.orderdate desc";
            }
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

            listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            SqlConnection con = DatabaseHelper.GetConnection();
            con.Open();
            string strsql = "select * from Orders o inner join OrderDetail od on o.orderid = od.orderid inner join product p on od.productid = p.productid where o.orderid = @OrderId  ";

            if(UserProfile.Role != 1)
            {
                strsql += " and o.userid = @UserId ";
            }

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

        void ShowOrderDetail_member(int CurrentUser)
        {
            listView3.Clear();
            listView3.View = View.Details;
            listView3.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView3.Columns.Add("會員名稱", 120);
            listView3.Columns.Add("電子郵件", 150);
            listView3.Columns.Add("電話", 100);


            SqlConnection con = DatabaseHelper.GetConnection();
            con.Open();
            string strsql = "select * from [user] where userid = @userid";
            SqlCommand cmd = new SqlCommand(strsql, con);
            cmd.Parameters.AddWithValue("@userid", CurrentUser);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = (string)reader["username"];
                lvi.SubItems.Add((string)reader["email"]);
                if (reader["phone"] != DBNull.Value)
                {
                    lvi.SubItems.Add((string)reader["phone"]);
                }
                else
                {
                    lvi.SubItems.Add("未設定");
                }


                listView3.Items.Add(lvi);
            }
        }


        private void OrderPanel_Load(object sender, EventArgs e)
        {
            if (AdminToolCB.Items.Count < 6)
            {
                AdminToolCB.Items.Add("未處理");
                AdminToolCB.Items.Add("處理中");
                AdminToolCB.Items.Add("已出貨");
                AdminToolCB.Items.Add("已到貨");
                AdminToolCB.Items.Add("已完成");
                AdminToolCB.Items.Add("已退貨");
                AdminToolCB.SelectedIndex = 0;
            }

            LoadAllMemberDataTodataGridView1();
            ApplyAntdStyle(dataGridView1);
            isAdmin();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
            {
                int selectid = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;

                ShowOrderDetail(selectid);
                isAdmin(selectid);
                AdminToolSave.Enabled = true;
                AdminToolCB.Enabled = true;
                if (UserProfile.Role == 1)
                {
                    int CurrentUser = (int)dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                    ShowOrderDetail_member(CurrentUser);
                }
                else
                {
                    string status = (string)dataGridView1.Rows[e.RowIndex].Cells[3].Value;
                    
                    if (status == "未處理"){
                        btncancel.Visible = true;
                    }
                    else
                    {
                        btncancel.Visible = false;
                    }
                }


            }
            else
            {
                AdminToolSave.Enabled = false;
                AdminToolCB.Enabled = false;
                btncancel.Visible = false;
            }




        }
        private void isAdmin()
        {
            if (UserProfile.Role == 1)
            {
                AdminTool.Visible = true;
                materialCard1.Visible = true;
            }
            else
            {
                AdminTool.Visible = false;
            }
        }

        private void isAdmin(int selectid)
        {
            AdminTool.Visible = true;
            if (UserProfile.Role != 1)
            {
                AdminTool.Visible = false;
            }


            SqlConnection con = DatabaseHelper.GetConnection();
            con.Open();
            string strsql = "select status from orders where orderid = @OrderId";
            SqlCommand cmd = new SqlCommand(strsql, con);
            cmd.Parameters.AddWithValue("@OrderId", selectid);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string status = (string)reader["status"];
                AdminToolCB.SelectedItem = status;
            }
            reader.Close();
            con.Close();


        }


        private void btncancel_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("確定取消訂單?", "確認", MessageBoxButtons.YesNo))
            {
                SqlConnection con = DatabaseHelper.GetConnection();
                con.Open();
                string strsql = "update orders set status = @Status where orderid = @OrderId";
                SqlCommand cmd = new SqlCommand(strsql, con);
                cmd.Parameters.AddWithValue("@Status", "已取消");
                cmd.Parameters.AddWithValue("@OrderId", (int)dataGridView1.CurrentRow.Cells[0].Value);
                cmd.ExecuteNonQuery();

                string strsql2 = "update product set stock = stock + od.quantity from product p inner join OrderDetail od on p.productid = od.productid where od.orderid = @OrderId";
                SqlCommand cmd2 = new SqlCommand(strsql2, con);
                cmd2.Parameters.AddWithValue("@OrderId", (int)dataGridView1.CurrentRow.Cells[0].Value);
                cmd2.ExecuteNonQuery();

                con.Close();
            }
            MessageBox.Show("訂單狀態已更新");
            OrderPanel_Load(this, EventArgs.Empty);
        }

        private void AdminToolSave_Click(object sender, EventArgs e)
        {
            try
            {
                int test = (int)dataGridView1.CurrentRow.Cells[0].Value;

            }
            catch (Exception)
            {
                MessageBox.Show("請選擇訂單");
                return;
            }
            SqlConnection con = DatabaseHelper.GetConnection();
            con.Open();
            string strsql = "update orders set status = @Status where orderid = @OrderId";
            SqlCommand cmd = new SqlCommand(strsql, con);
            cmd.Parameters.AddWithValue("@Status", AdminToolCB.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@OrderId", (int)dataGridView1.CurrentRow.Cells[0].Value);
            cmd.ExecuteNonQuery();

            if (AdminToolCB.SelectedItem.ToString() == "已退貨")
            {
                string strsql2 = "update product set stock = stock + od.quantity from product p inner join OrderDetail od on p.productid = od.productid where od.orderid = @OrderId";
                SqlCommand cmd2 = new SqlCommand(strsql2, con);
                cmd2.Parameters.AddWithValue("@OrderId", (int)dataGridView1.CurrentRow.Cells[0].Value);
                cmd2.ExecuteNonQuery();
            }

            con.Close();
            MessageBox.Show("訂單狀態已更新");
            OrderPanel_Load(this, EventArgs.Empty);
        }


    }
}
