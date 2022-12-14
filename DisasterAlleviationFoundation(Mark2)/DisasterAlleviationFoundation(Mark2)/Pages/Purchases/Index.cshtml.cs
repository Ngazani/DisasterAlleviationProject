using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DisasterAlleviationFoundation_Mark2_.Pages.Categories.Purchases
{
    public class IndexModel : PageModel
    {
        public List<purchase> displayPurchases = new List<purchase>();
        public purchase purchase = new purchase();
        public void OnGet()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Databases.connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM purchases";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader read = com.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                purchase purchase = new purchase();
                                purchase.id = "" + read.GetInt32(0);
                                purchase.item = read.GetString(1);
                                purchase.quantity = read.GetString(2);
                                purchase.category = read.GetString(3);
                                purchase.price = read.GetString(4);
                              

                                displayPurchases.Add(purchase);
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
    public class purchase
    {
        public String id;
        public String item;
        public String quantity;
        public String category;
        public String price;
    }
}
