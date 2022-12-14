using DisasterAlleviationFoundation_Mark2_.Pages.Goods_Donations;
using DisasterAlleviationFoundation_Mark2_.Pages.Monetary_Donations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DisasterAlleviationFoundation_Mark2_.Pages.Categories.Purchases
{
    public class Capture_PurchaseModel : PageModel
    {
        public purchase purchase = new purchase();
        public string err = "";
        public string succ = "";


        public void OnGet()
        {


        }

        public void OnPost()
        {
            purchase.item = Request.Form["item"];
            purchase.quantity = Request.Form["quantity"];
            purchase.category = Request.Form["category"];
            purchase.price = Request.Form["price"];
        

            if (purchase.item.Length == 0 ||
                purchase.quantity.Length == 0 ||
                purchase.category.Length == 0 ||
                purchase.price.Length == 0 )
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
                    string query = "INSERT INTO purchases(item,quantity,category,price)VALUES(@item,@quantity,@category,@price);";
                    using (SqlCommand com = new SqlCommand(query, con))
                    {
                        com.Parameters.AddWithValue("@item", purchase.item);
                        com.Parameters.AddWithValue("@quantity", purchase.quantity);
                        com.Parameters.AddWithValue("@category", purchase.category);
                        com.Parameters.AddWithValue("@price", purchase.price);
                        monetary.total = monetary.total - (int.Parse(purchase.price ) * int.Parse(purchase.quantity));
                        goods.qty = goods.qty + int.Parse(purchase.quantity);

                        com.ExecuteNonQuery();
                    }
                }

                using (SqlConnection con2 = new SqlConnection(Databases.connectionString))
                {
                    con2.Open();
                    string query = "INSERT INTO inventory(quantity,Amount)VALUES(@quantity,@price);";
                    using (SqlCommand com2 = new SqlCommand(query, con2))
                    {
         
                        com2.Parameters.AddWithValue("@quantity", purchase.quantity);
                        com2.Parameters.AddWithValue("@price", purchase.price);
                      

                        com2.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(err = ex.Message);
            }


            purchase.item = "";
            purchase.quantity = "";
            purchase.category = "";
            purchase.price = "";

            succ = "Donation Successfully Captured";

        }

    }
}