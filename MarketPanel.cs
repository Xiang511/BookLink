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
    public partial class MarketPanel : UserControl
    {
        public MarketPanel()
        {
            InitializeComponent();
        }


        public void GetProduct()
        {
            try
            {

                SqlConnection conn = DatabaseHelper.GetConnection();
                conn.Open();
                string query = "SELECT productid,productname, price, stock,image FROM Product";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductInfo.productID.Add((int)reader["productid"]);
                    ProductInfo.productName.Add((string)reader["productname"]);
                    ProductInfo.productPrice.Add((decimal)reader["price"]);
                    ProductInfo.productQuantity.Add((int)reader["stock"]);
                    ProductInfo.productImage.Add((string)reader["image"]);
                    string imageName = (string)reader["image"];
                    string ImageDir = globalVal.strImageDir + @"/" + imageName;
                    Console.WriteLine(ImageDir);
                    FileStream fs = File.OpenRead(ImageDir);
                    Image ProductImg = Image.FromStream(fs);
                    imageList1.Images.Add(ProductImg);
                    fs.Close();
                }
                reader.Close();
                conn.Close();



                loadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);

            }
        }

        public void ClearProducts()
        {
            ProductInfo.productID.Clear();
            ProductInfo.productName.Clear();
            ProductInfo.productPrice.Clear();
            ProductInfo.productQuantity.Clear();
            ProductInfo.productImage.Clear();
            imageList1.Images.Clear();
            listView1.Items.Clear();
        }

        private void MarketPanel_Load(object sender, EventArgs e)
        {   


                ClearProducts();
                GetProduct();
                
           
        }

        public void loadProducts()
        {   
        

            listView1.View = View.LargeIcon;
            imageList1.ImageSize = new Size(120, 160);
            listView1.LargeImageList = imageList1;
            for (int i = 0; i < ProductInfo.productID.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                item.Text = ProductInfo.productName[i];
                item.Font = new Font("微軟正黑體", 12, FontStyle.Bold);
                Console.WriteLine(item.Text);

                listView1.Items.Add(item);
            }

        }

    }
}
