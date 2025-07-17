using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages.Resource;

public class EditModel : PageModel
{
    public ResourceInfo resourceInfo = new ResourceInfo();
    public String errorMessage = "";
    public String successMessage = "";
    public void OnGet()
    {
        String id = Request.Query["id"];

        try
        {
            String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Clients;Integrated Security=True;Encrypt=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "SELECT * FROM ResourceMan WHERE id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            resourceInfo.id = "" + reader.GetInt32(0);
                            resourceInfo.name = reader.GetString(1);
                            resourceInfo.description = reader.GetString(2);
                            resourceInfo.location = reader.GetString(3);
                            resourceInfo.capacity = "" + reader.GetInt32(4);
                            resourceInfo.is_available = reader.GetBoolean(5).ToString();

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
        }
    }
    public void OnPost()
    {
        resourceInfo.id = Request.Form["id"];
        resourceInfo.name = Request.Form["name"];
        resourceInfo.description = Request.Form["description"];
        resourceInfo.location = Request.Form["location"];
        resourceInfo.capacity = Request.Form["capacity"];
        resourceInfo.is_available = Request.Form["is_available"];

        if (resourceInfo.id.Length == 0 || resourceInfo.name.Length == 0 || resourceInfo.description.Length == 0 ||
            resourceInfo.location.Length == 0 || resourceInfo.capacity.Length == 0 | resourceInfo.is_available.Length == 0)
        {
            errorMessage = "All fields are required";
            return;
        }

        try
        {
            String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Clients;Integrated Security=True;Encrypt=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE ResourceMan " +
                             "SET name=@name, description=@description, location=@location, capacity=@capacity, is_available=@is_available " +
                             "WHERE id=@id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", resourceInfo.name);
                    command.Parameters.AddWithValue("@description", resourceInfo.description);
                    command.Parameters.AddWithValue("@location", resourceInfo.location);
                    command.Parameters.AddWithValue("@capacity", resourceInfo.capacity);
                    command.Parameters.AddWithValue("@is_available", resourceInfo.is_available);
                    command.Parameters.AddWithValue("@id", resourceInfo.id);

                    command.ExecuteNonQuery();
                }


            }
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            return;
        }

        Response.Redirect("/Resource/Resource");
    }

}
