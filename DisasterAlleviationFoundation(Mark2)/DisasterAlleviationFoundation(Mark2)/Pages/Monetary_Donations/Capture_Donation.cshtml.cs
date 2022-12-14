using DisasterAlleviationFoundation_Mark2_.Pages.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DisasterAlleviationFoundation_Mark2_.Pages.Monetary_Donations
{
    public class Capture_DonationModel : PageModel
    {
        public monetary monetary = new monetary();
        public string err = "";
        public string succ = "";


        public void OnGet()
        {

        }

        public void OnPost()
        {
            monetary.date = Request.Form["date"];
            monetary.amount = Request.Form["amount"];
            monetary.donor = Request.Form["donor"];
            
            if ( monetary.date.Length == 0 || monetary.amount.Length == 0 || monetary.donor.Length == 0)
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
                    string query = "INSERT INTO monetary(Donor_date,amount,donor)VALUES(@date,@amount,@donor);";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@date", monetary.date);
                        com.Parameters.AddWithValue("@amount", monetary.amount);
                        com.Parameters.AddWithValue("@donor", monetary.donor);

                        monetary.total += int.Parse(monetary.amount);
                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(err = ex.Message);
            }


            monetary.date =  "";
            monetary.amount = "";
            monetary.donor = "";
            
            succ = "Donation Successfully Captured";

        }

    }
}
