using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DisasterAlleviationFoundation_Mark2_.Pages.Monetary_Donations
{
    public class IndexModel : PageModel
    {
        public List<monetary> displayMoney = new List<monetary>();
        public monetary monetary = new monetary();
        public void OnGet()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Databases.connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM monetary";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader read = com.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                monetary monetary = new monetary();
                                monetary.id = "" + read.GetInt32(0);
                                monetary.date = read.GetString(1);
                                monetary.amount = read.GetString(2);
                                monetary.donor = read.GetString(3);
                               
                                displayMoney.Add(monetary);
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
    public class monetary
    {
        public String id;
        public String date;
        public String amount;
        public String donor;
        public static int total = 0;
    

    }
}

