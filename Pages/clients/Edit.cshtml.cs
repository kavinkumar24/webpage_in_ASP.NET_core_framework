using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRUD.Pages.clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientinfo = new ClientInfo();
        public string errorMessage = "";
        public string GetMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=LAPTOP-G0NV6EHS;Initial Catalog=CRUD;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                   
                    connection.Open();
                    String mysql = "SELECT * FROM clients WHERE in id=id";
                    using (SqlCommand command = new SqlCommand(mysql, connection))
                    {

                        //sqlcommand allows you to query and sends the command to database
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //Exception methods returns the the sqldatareader object for viewing the results of select query
                            while (reader.Read()) /*reader a row of record,only for reading not to edit*/
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();
                                
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        } 
        public void Onpost()
        {
            string id = Request.Query["id"];
            clientinfo.id = Request.Form["id"];
            clientinfo.name = Request.Form["name"];
            clientinfo.email = Request.Form["email"];
            clientinfo.phone = Request.Form["phone"];
            clientinfo.address = Request.Form["address"];

            try
            {
                string connectionString = "Data Source=LAPTOP-G0NV6EHS;Initial Catalog=CRUD;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string mysql = "UPDATE clients SET name=@name,email=@email,phone=@phone,address=@address WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(mysql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                    Response.Redirect("/clients/Index");
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

        }
    }
}
