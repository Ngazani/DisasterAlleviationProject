using DisasterAlleviationFoundation_Mark2_.Pages.Goods_Donations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DisasterAlleviationFoundation_Mark2_.Pages.Disasters
{
    public class Capture_DisaserModel : PageModel
    {
        public disasters disaster = new disasters();
        public string err = "";
        public string succ = "";


        public void OnGet()
        {

        }

        public void OnPost()
        {
            DateTime end = DateTime.Parse(disaster.end = Request.Form["end"]);
            DateTime today = DateTime.Now;
            disaster.start = Request.Form["start"];
            disaster.end = Request.Form["end"];
            disaster.location = Request.Form["location"];
            disaster.description = Request.Form["description"];
            disaster.aid = Request.Form["aid"];
         
           

            if (disaster.start.Length == 0 || disaster.end.Length == 0 || disaster.location.Length == 0 || disaster.description.Length == 0 ||
                disaster.aid.Length == 0)
            {
                err = "No Fields Can Be Left Empty";
                return;
            }
            //save userdata
            try
            {
                if (end > today) 
                {
                    disaster.status = "Active";
                }
                else 
                {
                    disaster.status = "Inactive";
                }
                using (SqlConnection con = new SqlConnection(Databases.connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO disaster(start_date,end_date,location,description,aid,diStatus)VALUES(@start,@end,@location,@description,@aid,@status);";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@start", disaster.start);
                        com.Parameters.AddWithValue("@end", disaster.end);
                        com.Parameters.AddWithValue("@location", disaster.location);
                        com.Parameters.AddWithValue("@description", disaster.description);
                        com.Parameters.AddWithValue("@aid", disaster.aid);
                        com.Parameters.AddWithValue("@status", disaster.status);
                        ;

                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(err = ex.Message);
            }


            disaster.start = "";
            disaster.end = "";
            disaster.location = "";
            disaster.description = "";
            disaster.aid = "";
          

            succ = "Disaster Successfully Captured";

        }

    }
}
