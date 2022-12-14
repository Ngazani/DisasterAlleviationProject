using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DisasterAlleviationFoundation_Mark2_.Pages.Disasters
{
    public class IndexModel : PageModel
    {
        public List<disasters> displayDisasters = new List<disasters>();
        public disasters disaster = new disasters();
        public void OnGet()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Databases.connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM disaster";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader read = com.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                disasters disaster = new disasters();
                                disaster.id = "" + read.GetInt32(0);
                                disaster.start = read.GetString(1);
                                disaster.end = read.GetString(2);
                                disaster.location = read.GetString(3);
                                disaster.description = read.GetString(4);
                                disaster.aid = read.GetString(5);
                                disaster.status = read.GetString(6);
                                displayDisasters.Add(disaster);
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
    public class disasters
    {
        public String id;
        public String start;
        public String end;
        public String location;
        public String description;
        public String aid;
        public String status;

    }
}
