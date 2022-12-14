using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DisasterAlleviationFoundation_Mark2_.Pages.Goods_Donations
{
    public class IndexModel : PageModel
    {
        public List<goods> displayGoods = new List<goods>();
        public goods good = new goods();
        public void OnGet()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Databases.connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM goods";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader read = com.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                goods good = new goods();
                                good.id = "" + read.GetInt32(0);
                                good.date = read.GetString(1);
                                good.quantity = read.GetString(2);
                                good.category = read.GetString(3);
                                good.description = read.GetString(4);
                                good.donor = read.GetString(5);

                                displayGoods.Add(good);
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
    }
    public class goods
    {
        public String id;
        public String date;
        public String quantity;
        public String category;
        public String description;
        public String donor;
        public static int qty = 0;


    }
}
