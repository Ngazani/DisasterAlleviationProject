using DisasterAlleviationFoundation_Mark2_.Pages.Monetary_Donations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DisasterAlleviationFoundation_Mark2_.Pages.Categories.Purchases
{
    public class AllocateMoneyModel : PageModel
    {
        public List<moneyAllocation> mallocations = new List<moneyAllocation>();
        public moneyAllocation allocatem = new moneyAllocation();
        public string err = "";
        public string succ = "";


        public void OnGet()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Databases.connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM moneyAllocation";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        using (SqlDataReader read = com.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                goodsAllocation allocate = new goodsAllocation();
                                allocatem.id = "" + read.GetInt32(0);
                                allocatem.amount = read.GetString(1);
                                allocate.disaster = read.GetString(2);


                                mallocations.Add(allocatem);
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
            allocatem.amount = Request.Form["amount"];
            allocatem.disaster = Request.Form["disaster"];


            if (allocatem.amount.Length == 0 ||
             
                allocatem.disaster.Length == 0)
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
                    string query = "INSERT INTO moneyAllocation(amount,disaster)VALUES(@amount,@disaster);";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@amount", allocatem.amount);
                        com.Parameters.AddWithValue("@disaster", allocatem.disaster);
                        monetary.total = monetary.total - int.Parse(allocatem.amount);
                        com.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(err = ex.Message);
            }


            allocatem.amount = "";

            allocatem.disaster = "";

            succ = "Resource Assigned";

        }

    }

    public class moneyAllocation
    {
        public String id;
        public String amount;
        public String disaster;
    }
}
