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
    public partial class ProductDetail_insert : Form
    {
        public ProductDetail_insert()
        {
            InitializeComponent();
        }

        private void ProductDetail_insert_Load(object sender, EventArgs e)
        {
            if (globalVal.LoadId > 0)
            {
                SqlConnection con = DatabaseHelper.GetConnection();
                con.Open();
                string query = "select * from Product where productid = @LoadId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@LoadId", globalVal.LoadId);
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    txtBookName.Text = (string)reader["productname"];
                    txtcategory.Text = reader["categoryid"].ToString();
                    txtPrice.Text = reader["price"].ToString();
                    txtStock.Text = reader["stock"].ToString();
                    txtdescription.Text = (string)reader["description"];
                    txtISBN.Text = reader["ISBN"].ToString();
                    txtPublisher.Text = (string)reader["publisher"];
                    pictureBox1.ImageLocation = Path.Combine(globalVal.strImageDir, (string)reader["image"]);
                    pictureBox1.Tag = (string)reader["image"];

                }
                reader.Close();
                con.Close();
            }
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            if (globalVal.LoadId > 0)//修改商品
            {
                if (txtBookName.Text != "" && txtcategory.Text != "" && txtPrice.Text != "" && txtStock.Text != "" && txtdescription.Text != "" && txtISBN.Text != "" && txtPublisher.Text != "" && pictureBox1.Image != null)

                {
                    if ((Int32.TryParse(txtStock.Text.Trim(), out int stock) &&
                    decimal.TryParse(txtPrice.Text.Trim(), out decimal price) &&
                    Int32.TryParse(txtcategory.Text.Trim(), out int category)))
                    {

                        SqlConnection con = DatabaseHelper.GetConnection();
                        con.Open();
                        string query = "UPDATE Product SET productname = @BookName, categoryid = @Category, price = @Price, stock = @Stock, description = @Description, ISBN = @ISBN, publisher = @Publisher, status = @status, image = @image WHERE productid = @LoadId";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@BookName", txtBookName.Text);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Stock", stock);
                        cmd.Parameters.AddWithValue("@Description", txtdescription.Text);
                        cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text.Trim());
                        cmd.Parameters.AddWithValue("@Publisher", txtPublisher.Text);
                        cmd.Parameters.AddWithValue("@status", "上架中");
                        cmd.Parameters.AddWithValue("@LoadId", globalVal.LoadId);

                        if (pictureBox1.Tag != null)
                        {
                            string imageData = (string)pictureBox1.Tag;
                            cmd.Parameters.AddWithValue("@image", imageData);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@image", DBNull.Value);
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("商品更新成功");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("請確認欄位數值是否正確");
                    }
                }
                else
                {
                    MessageBox.Show("請填寫所有欄位");
                }

                // 重新載入商品列表
                Form3 form3 = Application.OpenForms.OfType<Form3>().FirstOrDefault();
                if (form3 != null)
                {
                    form3.LoadUserControl<MarketPanel>();
                }
            }
            else //新增商品
            {
                if (txtBookName.Text != "" && txtcategory.Text != "" && txtPrice.Text != "" && txtStock.Text != "" && txtdescription.Text != "" && txtISBN.Text != "" && txtPublisher.Text != "" && pictureBox1.Image != null)

                {
                    if ((Int32.TryParse(txtStock.Text, out int stock) &&
                    decimal.TryParse(txtPrice.Text, out decimal price) &&
                    Int32.TryParse(txtcategory.Text, out int category)))
                    {

                        SqlConnection con = DatabaseHelper.GetConnection();
                        con.Open();
                        string query = "INSERT INTO Product (productname, categoryid, price, stock, description, ISBN, publisher,status,image) VALUES (@BookName, @Category, @Price, @Stock, @Description, @ISBN, @Publisher,@status,@image)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@BookName", txtBookName.Text);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Stock", stock);
                        cmd.Parameters.AddWithValue("@Description", txtdescription.Text);
                        cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text.Trim());
                        cmd.Parameters.AddWithValue("@Publisher", txtPublisher.Text);
                        cmd.Parameters.AddWithValue("@status", "上架中");

                        if (pictureBox1.Tag != null)
                        {
                            string imageData = (string)pictureBox1.Tag;
                            cmd.Parameters.AddWithValue("@image", imageData);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@image", DBNull.Value);
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("商品新增成功");
                        this.Close();

                        Form3 form3 = Application.OpenForms.OfType<Form3>().FirstOrDefault();
                        if (form3 != null)
                        {
                            form3.LoadUserControl<MarketPanel>();
                        }
                    }
                    else
                    {
                        MessageBox.Show("請確認欄位數值是否正確");
                    }
                }
                else
                {
                    MessageBox.Show("請填寫所有欄位");
                }
            }
        }


        private void Btnimage_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = globalVal.strImageDir;
            ofd.Filter = "圖片檔案類型(PNG)|*.png";
            DialogResult R = ofd.ShowDialog();

            if (R == DialogResult.OK)
            {
                // --- 關鍵修改：使用 MemoryStream 徹底斷開檔案鎖定 ---
                using (FileStream fs = File.OpenRead(ofd.FileName))
                {
                    MemoryStream ms = new MemoryStream();
                    fs.CopyTo(ms);
                    // 釋放舊圖片資源，避免記憶體洩漏
                    pictureBox1.Image?.Dispose();
                    pictureBox1.Image = Image.FromStream(ms);
                }
                // 檔名

                pictureBox1.Tag = Path.GetFileName(ofd.FileName);
            }

            if (pictureBox1.Image != null && pictureBox1.Tag != null)
            {
                // 1. 從 Tag 取得純檔名
                string fileName = pictureBox1.Tag.ToString();

                // 2. 結合全域的圖片目錄路徑，組成完整的儲存路徑
                // 假設你的圖片要存到 globalVal.strImageDir
                string fullPath = Path.Combine(globalVal.strImageDir, fileName);

                try
                {
                    // 3. 確保目標目錄存在
                    string directoryPath = Path.GetDirectoryName(fullPath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // 4. 執行儲存
                    pictureBox1.Image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
                    //MessageBox.Show($"圖片已成功儲存至：\n{fullPath}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"儲存失敗：{ex.Message}");
                }
            }
        }
    }
}
