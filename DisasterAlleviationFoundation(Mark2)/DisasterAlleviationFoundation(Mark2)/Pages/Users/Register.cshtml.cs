using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DisasterAlleviationFoundation_Mark2_.Pages.Users
{
    public class RegisterModel : PageModel
    {

        public userData users = new userData();
        public string err = "";
        public string succ = "";


        public void OnGet()
        {

        }

        public void OnPost()
        {
            users.Uname = Request.Form["name"];
            users.email = Request.Form["email"];
            users.Upassword = Request.Form["password"];
            users.Utype = Request.Form["type"];
            if (users.Uname.Length == 0 || users.email.Length == 0 || users.Upassword.Length == 0 || users.Utype.Length == 0)
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
                    string query = "INSERT INTO users (Uname,email,Upassword,Utype)VALUES(@name,@email,@password,@type);";
                    using (SqlCommand com = new SqlCommand(query,con)) 
                    {
                        com.Parameters.AddWithValue("@name",users.Uname);
                        com.Parameters.AddWithValue("@email",users.email);
                        com.Parameters.AddWithValue("@password",users.Upassword);
                        com.Parameters.AddWithValue("@type",users.Utype);
                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

               Console.WriteLine( err = ex.Message);
            }
          

            users.Uname = "";
            users.email = "";
            users.Upassword = "";
            users.Utype = "";
            succ = "User Successfully Registered";
            
        }

    }
}

