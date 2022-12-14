using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DisasterAlleviationFoundation_Mark2_.Pages.Users
{
    public class IndexModel : PageModel
    {
        public List<userData> displayUsers = new List<userData>();
        public userData users = new userData();
        public void OnGet()
        {
            try
            {using (SqlConnection con = new SqlConnection(Databases.connectionString) ) 
                { 
                    con.Open();
                    string query = "SELECT * FROM users";
                    using (SqlCommand com = new SqlCommand(query,con)) 
                    {
                        using (SqlDataReader read = com.ExecuteReader())
                        {
                            while (read.Read()) 
                            { 
                                userData users = new userData();
                                users.id = "" + read.GetInt32(0);
                                users.Uname = read.GetString(1);
                                users.email =   read.GetString(2);
                                users.Upassword = read.GetString(3);
                                users.Utype = read.GetString(4);
                                userData.type = read.GetString(4);
                                displayUsers.Add(users);
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
    public class userData 
    {
        public String id;
        public String Uname;
        public String email;
        public String Upassword;
        public String Utype;
        public static String type;

    }
}
