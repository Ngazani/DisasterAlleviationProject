using DisasterAlleviationFoundation_Mark2_.Pages.Goods_Donations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DisasterAlleviationFoundation_Mark2_.Pages.Categories
{
    public class IndexModel : PageModel
    {
        public List<cats> displayCategories = new List<cats>();
        public cats cat = new cats();
        public string err = "";
        public string succ = "";

        public void OnGet()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Databases.connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM category";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader read = com.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                cats cat = new cats();
                                cat.id = "" + read.GetInt32(0);
                                cat.name = read.GetString(1);
                               

                                displayCategories.Add(cat);
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
                
                cat.name = Request.Form["name"];

                if (cat.name.Length == 0 )
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
                        string query = "INSERT INTO category(name)VALUES(@name);";
                        using (SqlCommand com = new SqlCommand(query, con))
                        {
                            com.Parameters.AddWithValue("@name", cat.name);
                            

                            com.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(err = ex.Message);
                }


                cat.name = "";
               

                succ = " New Category Successfully Added";

            }

        }
    }
    public class cats
    {
        public String id;
        public String name;
    }

    

