using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DisasterAlleviationFoundation_Mark2_.Pages.Inventory
{
    public class IndexModel : PageModel
    {

        public List<inventory> displayInventory = new List<inventory>();
        public inventory inventory = new inventory();
        public void OnGet()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Databases.connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM inventory";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader read = com.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                inventory inventory = new inventory();
                                inventory.id = "" + read.GetInt32(0);
                                inventory.date = read.GetString(1);
                                inventory.quantity = read.GetString(2);
                                inventory.category = read.GetString(3);
                                inventory.description = read.GetString(4);


                                displayInventory.Add(inventory);
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
    public class inventory
    {
        public String id;
        public String date;
        public String quantity;
        public String category;
        public String description;

    }
}
