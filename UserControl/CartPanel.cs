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
using static Online_Ordering_System.Form3;

namespace Online_Ordering_System
{
    public partial class CartPanel : UserControl
    {
        public CartPanel()
        {
            InitializeComponent();
        }

        private void CartPanel_Load(object sender, EventArgs e)
        {
            Lblitemcount.Text = $"共 {CartList.InfoList.Count.ToString()} 件商品";
            Lblprice.Text = $"{(decimal)price()}";

            listView1.View = View.Details;
            imageList1.ImageSize = new Size(120, 160);
            listView1.SmallImageList = imageList1;
            listView1.FullRowSelect = true;

            if (CartList.InfoList.Count > 0)
            {
                Lblshipvia.Text = $"{(CartList.InfoList.Count > 0 && price() < 1000 ? 60 : 0)}";
                Lbltotalmount.Text = $"{(decimal)CalculateTotalPrice()}";

                try
                {

                    SqlConnection conn = DatabaseHelper.GetConnection();
                    conn.Open();
                    string query = "SELECT productid,image FROM Product";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int productid = (int)reader["productid"];
                        string imageName = (string)reader["image"];
                        string ImageDir = globalVal.strImageDir + @"/" + imageName;
                        FileStream fs = File.OpenRead(ImageDir);
                        Image ProductImg = Image.FromStream(fs);
                        imageList1.Images.Add(productid.ToString(), ProductImg);
                        fs.Close();
                    }
                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading products: " + ex.Message);

                }

                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.Columns.Add("商品圖片", 120);
                listView1.Columns.Add("商品名稱", 120);
                listView1.Columns.Add("單價", 110);
                listView1.Columns.Add("數量", 60);
                listView1.Columns.Add("小計", 150);

                foreach (CartInfo item in CartList.InfoList)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.ImageKey = item.productID.ToString();
                    lvi.SubItems.Add(item.productName);

                    lvi.SubItems.Add(item.productPrice.ToString("C")); // 格式化為貨幣
                    lvi.SubItems.Add(item.orderQuantity.ToString());
                    lvi.SubItems.Add((item.productPrice * item.orderQuantity).ToString("C"));

                    listView1.Items.Add(lvi);

                    lvi.Tag = item.productID;

                    Console.WriteLine($"{item.productName}, {item.productPrice}, {item.orderQuantity}");
                }
            }
        }

        decimal price()
        {
            decimal totalPrice = 0;
            foreach (CartInfo item in CartList.InfoList)
            {
                totalPrice += item.productPrice * item.orderQuantity;
            }
            return totalPrice;
        }

        decimal CalculateTotalPrice()
        {
            if(CartList.InfoList.Count == 0)
            {
                return 0;
            }else
            {
                decimal totalPrice = 0;
                decimal shipvia = 0;

                foreach (CartInfo item in CartList.InfoList)
                {
                    totalPrice += item.productPrice * item.orderQuantity;
                }
                if (totalPrice < 1000)
                {
                    shipvia = 60;
                    totalPrice += shipvia;

                }
                else
                {
                    shipvia = 0;
                }

                return totalPrice;

            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            Lblitemcount.Text = $"共 {CartList.InfoList.Count.ToString()} 件商品";
            Lblprice.Text = $"{(decimal)price()}";
            Lblshipvia.Text = $"{(CartList.InfoList.Count > 0 && price() < 1000 ? 60 : 0)}";
            Lbltotalmount.Text = $"{(decimal)CalculateTotalPrice()}";
            Console.WriteLine(Lblprice.Text);
            gbtool.Visible = true;
            Int32.TryParse(listView1.SelectedItems[0].SubItems[3].Text, out int quantity);
            globalVal.currentid = (int)listView1.SelectedItems[0].Tag;
            Console.WriteLine(globalVal.currentid);
            numericUpDown1.Value = quantity;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (globalVal.currentid != 0)
            {
                foreach (CartInfo item in CartList.InfoList)
                {
                    if (item.productID == globalVal.currentid)
                    {
                        item.orderQuantity = (int)numericUpDown1.Value;
                        break;
                    }
                }
                // 重新載入 CartPanel 以更新顯示
                CartPanel_Load(this, EventArgs.Empty);
                gbtool.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (globalVal.currentid != 0)
            {
                foreach (CartInfo item in CartList.InfoList)
                {
                    if (item.productID == globalVal.currentid)
                    {
                        CartList.InfoList.Remove(item);
                        break;
                    }
                }
                // 重新載入 CartPanel 以更新顯示
                CartPanel_Load(this, EventArgs.Empty);
                gbtool.Visible = false;
            }
            if (CartList.InfoList.Count == 0)
            {
                MessageBox.Show("購物車已清空，請返回商品頁面選購商品。", "購物車空", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listView1.Clear();
                Lbltotalmount.Text = "0";
                Lblshipvia.Text = "0";
                Lblprice.Text = "0";
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Form3 form3 = this.FindForm() as Form3;
            form3.LoadUserControl<MarketPanel>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CartList.InfoList.Count > 0)
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();


                    string query = @"INSERT INTO [Orders] (userid, orderdate, totalamount, status) 
                     VALUES (@userid, @orderdate, @totalamount, @status);
                     SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userid", UserProfile.UserId);
                    cmd.Parameters.AddWithValue("@orderdate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@totalamount", CalculateTotalPrice());
                    cmd.Parameters.AddWithValue("@status", "未處理");

                    object result = cmd.ExecuteScalar();
                    int newOrderId = Convert.ToInt32(result);

                    foreach (CartInfo item in CartList.InfoList)
                    {
                        int productId = item.productID;
                        int quantity = item.orderQuantity;
                        decimal price = item.productPrice;
                        string query2 = "INSERT INTO OrderDetail (orderid, productid, quantity, price) VALUES (@orderid, @productid, @quantity, @price);";
                        SqlCommand cmd2 = new SqlCommand(query2, conn);
                        cmd2.Parameters.AddWithValue("@orderid", newOrderId);
                        cmd2.Parameters.AddWithValue("@productid", productId);
                        cmd2.Parameters.AddWithValue("@quantity", quantity);
                        cmd2.Parameters.AddWithValue("@price", price);
                        cmd2.ExecuteNonQuery();
                    }

                    foreach (CartInfo item in CartList.InfoList)
                    {   
                        string stockQuery = "UPDATE Product SET stock = stock - @quantity WHERE productid = @productid";
                        int quantity = item.orderQuantity;
                        SqlCommand cmd3 = new SqlCommand(stockQuery, conn);
                        cmd3.Parameters.AddWithValue("@quantity", quantity);
                        cmd3.Parameters.AddWithValue("@productid", item.productID);
                        cmd3.ExecuteNonQuery();
                    }
                    MessageBox.Show($"訂單編號 {newOrderId} 已成功建立！");
                }
                listView1.Clear();
                gbtool.Visible = false;
                CartList.InfoList.Clear();
                Lbltotalmount.Text = "0";
                Lblshipvia.Text = "0";
                Lblprice.Text = "0";
                Lblitemcount.Text = "共 0 件商品";

            }
            else
            {
                MessageBox.Show("購物車為空，無法結帳。請先加入商品至購物車。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
