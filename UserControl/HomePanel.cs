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
    public partial class HomePanel : UserControl
    {
        public HomePanel()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (globalVal.bannerIndex < 3)
            {
                globalVal.bannerIndex++;
                BannerSlider.ImageLocation = $"Image/Banner{globalVal.bannerIndex}.png";

            }
            else
            {
                globalVal.bannerIndex = 1;
                BannerSlider.ImageLocation = $"Image/Banner{globalVal.bannerIndex}.png";

            }
        }

        private void BannerSlider_Click(object sender, EventArgs e)
        {

        }

        private void HomePanel_Load(object sender, EventArgs e)
        {
            SqlConnection con = DatabaseHelper.GetConnection();
            con.Open();
            string query = "SELECT COUNT(*) FROM Product";
            SqlCommand cmd = new SqlCommand(query, con);
            int productCount = (int)cmd.ExecuteScalar();
            label2.Text = productCount.ToString();

            string query2 = "SELECT COUNT(*) FROM [Orders]";
            cmd = new SqlCommand(query2, con);
            int orderCount = (int)cmd.ExecuteScalar();
            label7.Text = orderCount.ToString();

            string query3 = "SELECT COUNT(*) FROM [User]";
            cmd = new SqlCommand(query3, con);
            int userCount = (int)cmd.ExecuteScalar();
            label10.Text = userCount.ToString();
            con.Close();

        }
    }
}
