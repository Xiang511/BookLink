using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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

        public void loadProducts()
        {   


            listView1.View = View.LargeIcon;
            imageList1.ImageSize = new Size(120, 160);
            listView1.LargeImageList = imageList1;
            foreach(ProductInfo item in ProductList.InfoList)
            {
                ListViewItem Lvitem = new ListViewItem();

                Lvitem.ImageKey = item.productID.ToString();
                Lvitem.Text = item.productName;
                Lvitem.Font = new Font("微軟正黑體", 12, FontStyle.Bold);
                Lvitem.Tag = item.productID;
                //Console.WriteLine(Lvitem.Text);
                //Console.WriteLine(Lvitem.Tag);
                listView1.Items.Add(Lvitem);

            }


        }



        public void GetProduct()
        {

            try
            {

                SqlConnection conn = DatabaseHelper.GetConnection();
                conn.Open();
                string query = strCategoryFilter;
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {   
                    ProductInfo info = new ProductInfo();

                    info.productID = ((int)reader["productid"]);
                    info.productName = ((string)reader["productname"]);
                    info.productPrice = ((decimal)reader["price"]);
                    info.productQuantity = ((int)reader["stock"]);
                    info.productImage = ((string)reader["image"]);
                    ProductList.InfoList.Add(info);

                    string imageName = (string)reader["image"];
                    string ImageDir = globalVal.strImageDir + @"/" + imageName;
                    FileStream fs = File.OpenRead(ImageDir);
                    Image ProductImg = Image.FromStream(fs);
                    imageList1.Images.Add(info.productID.ToString(), ProductImg);
                    fs.Close();
                }
                reader.Close();
                conn.Close();



                loadProducts();


                //foreach (ProductInfo item in ProductList.InfoList)
                //{
                //    Console.WriteLine($"{item.productID}, {item.productName},{item.productPrice}\n");
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);

            }
        }

        public void ClearProducts()
        {
            ProductList.InfoList.Clear();
            //imageList1.Images.Clear();
            listView1.Items.Clear();
        }

        private void MarketPanel_Load(object sender, EventArgs e)
        {
            ClearProducts();
            GetProduct();

            if(UserProfile.Role == 1)
            {
                btnInsert.Visible = true;
                button1.Visible = true;
            }
            else
            {
                btnInsert.Visible = false;
                button1.Visible = false;
            }
        }


        public void ResetCategoryFilter()
        {
            txtSearch.Text = "";
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
        }

        public static string strCategoryFilter = "SELECT productid,productname, price, stock,image FROM Product where status = '上架中' and isDel = 'false' ";

        public static string strCategoryFilterDefault = "SELECT productid,productname, price, stock,image FROM Product where status = '上架中' and isDel = 'false' ";

        private void button2_Click(object sender, EventArgs e)
        {
            string CategoryStr = " SELECT productid,productname, price, stock,image FROM Product where status = '上架中' and isDel = 'false'";
            strCategoryFilter = CategoryStr;
            ClearProducts();
            GetProduct();
            ResetCategoryFilter();
            strCategoryFilter = strCategoryFilterDefault;
        }

        private void btnCategroy1_Click(object sender, EventArgs e)
        {
            string CategoryStr = " SELECT productid,productname, price, stock,image FROM Product where categoryid = 1 and status = '上架中' and isDel = 'false'";
            strCategoryFilter = CategoryStr;
            ClearProducts();
            GetProduct();
            ResetCategoryFilter();
            strCategoryFilter = strCategoryFilterDefault;
        }

        private void btnCategroy2_Click(object sender, EventArgs e)
        {
            string CategoryStr = " SELECT productid,productname, price, stock,image FROM Product where categoryid = 2 and status = '上架中' and isDel = 'false' ";
            strCategoryFilter = CategoryStr;
            ClearProducts();
            GetProduct();
            ResetCategoryFilter();
            strCategoryFilter = strCategoryFilterDefault;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string CategoryStr = " SELECT productid,productname, price, stock,image FROM Product where status = '已下架' and isDel = 'false' ";
            strCategoryFilter = CategoryStr;
            ClearProducts();
            GetProduct();
            ResetCategoryFilter();
            strCategoryFilter = strCategoryFilterDefault;
        }


        public static string strSearchFilter = "";

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                ClearProducts();
                string searchTerm = txtSearch.Text.Trim();



                string strSearch = " SELECT productid,productname, price, stock,image FROM Product where productname LIKE @searchTerm where status = '上架中' and isDel = 'false'";

                if ((radioButton1.Checked))
                {
                    strSearch = " SELECT productid,productname, price, stock,image FROM Product where productname LIKE @searchTerm and  status = '上架中' and isDel = 'false'";
                }
                else if ((radioButton2.Checked))
                {
                    strSearch = " SELECT productid,productname, price, stock,image FROM Product where publisher LIKE @searchTerm and status = '上架中' and isDel = 'false'";
                }
                else if ((radioButton3.Checked))
                {
                    strSearch = " SELECT productid,productname, price, stock,image FROM Product where ISBN LIKE @searchTerm and status = '上架中' and isDel = 'false'";
                }


                try
                {
                    int count = 0;

                    SqlConnection conn = DatabaseHelper.GetConnection();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(strSearch, conn);
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ProductInfo info = new ProductInfo();

                        info.productID = ((int)reader["productid"]);
                        info.productName = ((string)reader["productname"]);
                        info.productPrice = ((decimal)reader["price"]);
                        info.productQuantity = ((int)reader["stock"]);
                        info.productImage = ((string)reader["image"]);
                        ProductList.InfoList.Add(info);

                        string imageName = (string)reader["image"];
                        string ImageDir = globalVal.strImageDir + @"/" + imageName;


                        FileStream fs = File.OpenRead(ImageDir);
                        Image ProductImg = Image.FromStream(fs);
                        imageList1.Images.Add(ProductImg);
                        fs.Close();
                        count++;
                    }

                    if (count == 0)
                    {
                        txtSearch.Text = "";

                        txtSearch.Focus();

                        ListViewItem item = new ListViewItem();                        
                        item.Text = "查無此書";
                        item.Font = new Font("微軟正黑體", 16, FontStyle.Bold);
                        listView1.Items.Add(item);
                    }

                    reader.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading products: " + ex.Message);
                }


                loadProducts();
            }
            else
            {
                MessageBox.Show("請輸入關鍵字");
                return;
            }


        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            ProductDetail productDetailForm = new ProductDetail();
            globalVal.LoadId = (int)listView1.SelectedItems[0].Tag;
            Console.WriteLine(globalVal.LoadId);
            //Console.WriteLine(globalVal.LoadId);
            productDetailForm.ShowDialog();

      


        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
          
            globalVal.LoadId = 0;
            globalVal.currentid = 0;
            ProductDetail_insert productDetailForm_insert = new ProductDetail_insert();
            productDetailForm_insert.ShowDialog();
        }


    }
}
