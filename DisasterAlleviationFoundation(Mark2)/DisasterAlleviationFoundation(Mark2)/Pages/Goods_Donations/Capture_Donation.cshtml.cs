using DisasterAlleviationFoundation_Mark2_.Pages.Monetary_Donations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DisasterAlleviationFoundation_Mark2_.Pages.Goods_Donations
{
    public class Capture_DonationModel : PageModel
    {
        public goods good = new goods();
        public List<cats> displayCategories = new List<cats>();
        public cats cat = new cats();
        public string err = "";
        public string succ = "";


        public void OnGet()
        {
            using (SqlConnection con = new SqlConnection(Databases.connectionString))  
            {
                con.Open();
                string query = "SELECT * FROM category";
            using (SqlCommand com = new SqlCommand(query,con))
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

        public void OnPost()
        {
            good.date = Request.Form["date"];
            good.quantity = Request.Form["quantity"];
            good.category = Request.Form["category"];
            good.description = Request.Form["description"];
            good.donor = Request.Form["donor"];

            if (good.date.Length == 0 || 
                good.quantity.Length == 0 ||
                good.category.Length == 0 ||
                good.description.Length == 0 || 
                good.donor.Length == 0 )
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
                    string query = "INSERT INTO goods(donor_date,quantity,category,description,donor)VALUES(@date,@quantity,@category,@description,@donor);";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@date", good.date);
                        com.Parameters.AddWithValue("@quantity", good.quantity);
                        com.Parameters.AddWithValue("@category", good.category);
                        com.Parameters.AddWithValue("@description", good.description);
                        com.Parameters.AddWithValue("@donor", good.donor);

                        goods.qty += int.Parse(good.quantity);

                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(err = ex.Message);
            }




            good.date = "";
            good.quantity = "";
            good.category = "";
            good.description = "";
            good.donor = "";

            succ = "Donation Successfully Captured";

        }

    }
}
