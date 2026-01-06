using MaterialSkin;
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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            panel1.Dock = DockStyle.Fill;

            // 修正：Image.Width 是唯讀屬性，無法直接設定。需先建立一個指定大小的新 Image，再指派給 toolStripLblTitle.Image。
            if (toolStripLblTitle.Image != null)
            {
                toolStripLblTitle.Image = new Bitmap(toolStripLblTitle.Image, new Size(38, 38));
            }

            toolStripLblTitle.Paint += toolStripLblHome_Paint; // 註冊 Paint 事件

            if (globalVal.islogin)
            {
                toolStripLBlUserName.Text = "歡迎 " + globalVal.Username + "！";
                toolStripLBlUserName.IsLink = false;
                toolStripLblLogout.Visible = true;
            }
            

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            LoadUserControl<home>();
        }

        private void toolStripLblHome_Paint(object sender, PaintEventArgs e)
        {
            ToolStripLabel lbl = sender as ToolStripLabel;
            if (lbl != null)
            {
                Rectangle rect = lbl.Bounds;
                using (Pen pen = new Pen(ColorTranslator.FromHtml("#F4F4F5"), 1))
                {
                    e.Graphics.DrawLine(pen, 0, rect.Height - 1, rect.Width, rect.Height - 1);
                }
            }
        }

        /// <summary>
        /// 載入 UserControl 到 panel1 的共用方法
        /// </summary>
        /// <typeparam name="T">UserControl 類型</typeparam>
        private void LoadUserControl<T>() where T : UserControl, new()
        {
            // 清除 panel1 中的所有控件
            panel1.Controls.Clear();

            // 創建新的 UserControl 實例
            T userControl = new T();
            userControl.Dock = DockStyle.Fill;

            // 添加到 panel1
            panel1.Controls.Add(userControl);
        }


        private void toolStripLblLogout_Click(object sender, EventArgs e)
        {
            globalVal.Username = string.Empty;
            globalVal.UserOrderCount = 0;
            globalVal.islogin = false;
            this.Close();
            Form1 loginForm = new Form1();
            loginForm.Show();
            MessageBox.Show("已登出！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void toolStripLblOrder_Click(object sender, EventArgs e)
        {
            LoadUserControl<UserControl1>();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripLBlUserName_Click(object sender, EventArgs e)
        {
            Form Form1 = new Form1();
            Form1.Show();

            Form3 form3 = this;
            form3.Hide();
        }

        private void toolStrip2_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripLBlUserName_OwnerChanged(object sender, EventArgs e)
        {

        }


        private void timer1_Tick(object sender, EventArgs e)
        {

            if (globalVal.bannerIndex < 5)
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
    }
}

