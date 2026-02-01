using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


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

            LoadUserControl<HomePanel>();


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
        public void LoadUserControl<T>() where T : UserControl, new()
        {
            // 清除 panel1 中的所有控件
            panel1.Controls.Clear();

            // 創建新的 UserControl 實例
            T userControl = new T();
            userControl.Dock = DockStyle.Fill;

            // 添加到 panel1
            panel1.Controls.Add(userControl);
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            LoadUserControl<HomePanel>();
        }

        private void toolStripLabel1_Click_1(object sender, EventArgs e)
        {


            LoadUserControl<MarketPanel>();

        }

        private void toolStripLblShopCar_Click(object sender, EventArgs e)
        {

            if (globalVal.islogin)
            {
                LoadUserControl<CartPanel>();
            }
            else
            {
                MessageBox.Show("請先登入會員！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Form1 loginForm = new Form1();
                loginForm.ShowDialog();
            }
        }

        private void toolStripLblOrder_Click(object sender, EventArgs e)
        {
            if (globalVal.islogin)
            {
                LoadUserControl<OrderPanel>();
            }
            else
            {
                MessageBox.Show("請先登入會員！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Form1 loginForm = new Form1();
                loginForm.ShowDialog();
            }

        }

        private void toolStripLblProfile_Click(object sender, EventArgs e)
        {
            if (globalVal.islogin)
            {
                LoadUserControl<Profile>();
            }
            else
            {
                MessageBox.Show("請先登入會員！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Form1 loginForm = new Form1();
                loginForm.ShowDialog();
            }
        }


        public void NotLoginState()
        {
            DatabaseHelper.clearUserProfile();
            globalVal.islogin = false;
            toolStripLBlUserName.Text = "Login";
            toolStripLBlUserName.Enabled = true;
            toolStripReport.Visible = false;
            avatar1.Image = null;
            avatar1.Refresh();
        }

        private void toolStripLblLogout_Click(object sender, EventArgs e)
        {
            if (globalVal.islogin)
            {
                NotLoginState();
                MessageBox.Show("已登出！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                toolStripLblLogout.Visible = false;
                LoadUserControl<HomePanel>();
                Form1 loginForm = new Form1();
                loginForm.ShowDialog();
            }

        }

        private void toolStripLBlUserName_Click(object sender, EventArgs e)
        {
            Form Form1 = new Form1();
            Form1.ShowDialog();
        }

        private void Form3_Activated(object sender, EventArgs e)
        {
            if (globalVal.islogin)
            {
                toolStripLBlUserName.Text = UserProfile.Username;
                toolStripLblLogout.Visible = true;
                toolStripLBlUserName.Enabled = false;
                DatabaseHelper.GetUserProfile();
                if (UserProfile.Role == 1)
                {
                    toolStripReport.Visible = true;
                }
                LoadAvatarFromUrl();

            }
            else
            {
                NotLoginState();
            }


        }

        public async Task LoadAvatarFromUrl()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    // 下載資料流
                    byte[] imageData = await client.DownloadDataTaskAsync(UserProfile.Photo);
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        Image img = Image.FromStream(ms);
                        avatar1.Image = img;
                    }
                }
            }
            catch (Exception ex)
            {
                // 如果網址無效或沒網路，顯示預設圖片
                Console.WriteLine("圖片載入失敗: " + ex.Message);
                // targetControl.BackgroundImage = Properties.Resources.DefaultAvatar; 
            }
        }

        private void toolStripReport_Click(object sender, EventArgs e)
        {
            LoadUserControl<Reports>();
        }
    }
}

