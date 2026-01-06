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
    public partial class home : UserControl
    {
        public home()
        {
            InitializeComponent();
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
