using System.Data;
using System.Data.SqlClient;
namespace LabAssignment
{
    public class Product
    {
        public string p_id { set; get; }
        public string p_name { set; get; }
        public string p_details { set; get; }
        public string category { set; get; }
        public float u_price { set; get; }
        public int quantity { set; get; }
        public byte[] p_image { set; get; }
        public string p_url { set; get; }
        public string p_urlM { set; get; }
    }
}