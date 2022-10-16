using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace CRUD.Pages.clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientinfo = new ClientInfo();
        public string errorMessage = "";
        public string GetMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            clientinfo.name = Request.Form["name"];
            clientinfo.email = Request.Form["email"];
            clientinfo.phone = Request.Form["phone"];
            clientinfo.address = Request.Form["address"];
            if (clientinfo.name.Length == 0 || clientinfo.email.Length == 0 || clientinfo.phone.Length == 0 || clientinfo.address.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }
            try
            {
                string connectionString = "Data Source=LAPTOP-G0NV6EHS;Initial Catalog=CRUD;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string mysql = "Insert Into clients " + "(name,email,phone,address) Values "
                        + "(@name,@email,@phone,@address)";
                    using (SqlCommand command = new SqlCommand(mysql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientinfo.name);
                        command.Parameters.AddWithValue("@email", clientinfo.email);
                        command.Parameters.AddWithValue("@phone", clientinfo.phone);
                        command.Parameters.AddWithValue("@address", clientinfo.address);
                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            clientinfo.name = ""; clientinfo.email = ""; clientinfo.phone = ""; clientinfo.address = "";
            GetMessage = "Added Sucessfully";
            Response.Redirect("/clients/Index");
        }
    }
}