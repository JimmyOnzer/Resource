using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages.Resource;

public class CreateModel : PageModel
{
    public ResourceInfo resourceInfo = new ResourceInfo();
    public String errorMessage = "";
    public String successMessage = "";
    public void OnGet()
    {
    }

    public void OnPost()
    {
        resourceInfo.name = Request.Form["name"];
        resourceInfo.description = Request.Form["description"];
        resourceInfo.location = Request.Form["location"];
        resourceInfo.capacity = Request.Form["capacity"];
        resourceInfo.is_available = Request.Form["is_available"];

        if (resourceInfo.name.Length == 0 || resourceInfo.description.Length == 0 ||
            resourceInfo.location.Length == 0 || resourceInfo.capacity.Length == 0 | resourceInfo.is_available.Length == 0 )
        {
            errorMessage = "All fields are required";
            return;
        }
        else if (resourceInfo.capacity.Length < 0)
        {
            errorMessage = "Capacity must be a positive number";
        }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Clients;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO ResourceMan " +
                    "(name, description, location, capacity, is_available) VALUES" +
                    "(@name, @description, @location, @capacity, @is_available);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", resourceInfo.name);
                        command.Parameters.AddWithValue("@description", resourceInfo.description);
                        command.Parameters.AddWithValue("@location", resourceInfo.location);
                        command.Parameters.AddWithValue("@capacity", resourceInfo.capacity);
                        command.Parameters.AddWithValue("@is_available", resourceInfo.is_available);

                        command.ExecuteNonQuery();
                    }


                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

        // save new client into the database
        resourceInfo.name = ""; resourceInfo.description = ""; resourceInfo.location = ""; resourceInfo.capacity = ""; resourceInfo.is_available = "";
        successMessage = "New Resource Added Successfully";

        Response.Redirect("/Resource/Resource");
    }

}
