using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Ordering_System
{
    public class globalVal
    {
        public static bool islogin = false;

        public static int bannerIndex = 0;

        public static string strImageDir = @"../../Image";

        public static int LoadId = 0 ;
    }
    public class UserProfile
    {
        public static int UserId { get; set; }
        public static string Username { get; set; }
        public static string Email { get; set; }

        public static string Password { get; set; }
        public static string Phone { get; set; }
        public static string Photo { get; set; }

        public static int Role { get; set; }

        public static DateTime CreatedDate { get; set; }
    }

    public class ProductInfo
    {        
        public int productID { get; set; }
        public string productName { get; set; }
        public decimal productPrice { get; set; }
        public int productQuantity { get; set; }
        public string productImage { get; set; }
    }

    public class ProductList
    {
        public static List<ProductInfo> InfoList = new List<ProductInfo>();
    }
}
