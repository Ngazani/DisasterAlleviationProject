using DisasterAlleviationFoundation_Mark2_.Pages.Goods_Donations;
using DisasterAlleviationFoundation_Mark2_.Pages.Monetary_Donations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DisasterAlleviationFoundation_Mark2_.Pages.Users
{
    public class LoginModel : PageModel
    {
        public userData users = new userData();
        public string err = "";
        public string succ = "";
        public string email = "";
        public string password = "";


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
                                
                                
                                goods.qty = int.Parse(read.GetString(1));
                                monetary.total = int.Parse(read.GetString(2));
                               
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
 
            email = Request.Form["email"];
            password = Request.Form["password"];
 
            if ( email.Length == 0 || password.Length == 0 )
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
                    string query = $"SELECT * FROM users WHERE email = '{email}' AND Upassword = '{password}';";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader read = com.ExecuteReader())
                        {
                            while (read.Read())
                            {
                  
                               email = read.GetString(2);
                               password = read.GetString(3);
                                if (email.Equals(Request.Form["email"]) && password.Equals(Request.Form["password"]))
                                {
                                    users.email = "";
                                    users.Upassword = "";
                                    userData.type = "Admin";
                                    succ = "User Successfully Logged in";
                                }
                                else
                                {
                                    err = "Check Credentials and try again";
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(err = ex.Message);
            }



           
            
           

        }

    }
}
