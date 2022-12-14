using DisasterAlleviationFoundation_Mark2_.Pages.Goods_Donations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DisasterAlleviationFoundation_Mark2_.Pages.Categories.Purchases
{
    public class AllocateGoodsModel : PageModel
    {
        public List<goodsAllocation> allocations = new List<goodsAllocation>();
        public goodsAllocation allocate = new goodsAllocation();
        public string err = "";
        public string succ = "";
        public int quantity = goods.qty;


        public void OnGet()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Databases.connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM goodsAllocation";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader read = com.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                goodsAllocation allocate = new goodsAllocation();
                                allocate.id = "" + read.GetInt32(0);
                                allocate.item = read.GetString(1);
                                allocate.quantity = read.GetString(2);
                                allocate.category = read.GetString(3);
                                allocate.disaster = read.GetString(4);


                                allocations.Add(allocate);
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                Console.WriteLine("App Crashed As a Result of" + ex.ToString());
            }

        }

        public void OnPost()
        {
            allocate.item = Request.Form["item"];
            allocate.quantity = Request.Form["quantity"];
            allocate.category = Request.Form["category"];
            allocate.disaster = Request.Form["disaster"];


            if (allocate.item.Length == 0 ||
                allocate.quantity.Length == 0 ||
                allocate.category.Length == 0 ||
                allocate.disaster.Length == 0)
            {
                err = "No Fields Can Be Left Empty";
                return;
            }
            //save userdata
            try
            {
                using (SqlConnection con = new SqlConnection(Databases.connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO goodsAllocation(item,quantity,category,disaster)VALUES(@item,@quantity,@category,@disaster);";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@item", allocate.item);
                        com.Parameters.AddWithValue("@quantity", allocate.quantity);
                        com.Parameters.AddWithValue("@category", allocate.category);
                        com.Parameters.AddWithValue("@disaster", allocate.disaster);
                        goods.qty = goods.qty - int.Parse(allocate.quantity);
                        com.ExecuteNonQuery();
                    }
                }
                
            }
            catch (Exception ex)
            {

                Console.WriteLine(err = ex.Message);
            }


            allocate.item = "";
            allocate.quantity = "";
            allocate.category = "";
            allocate.disaster = "";

            succ = "Resource Assigned";

        }

    }

    public class goodsAllocation 
    {
        public String id;
        public String item;
        public String quantity;
        public String category;
        public String disaster;
    }
}
