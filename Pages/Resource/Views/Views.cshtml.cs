using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Pages.Resource;

namespace WebClient.Pages.Resource.Views
{
    public class ViewModel : PageModel
    {
        public List<ResourceInfo> listResource = new List<ResourceInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Clients;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM ResourceMan";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ResourceInfo resourceInfo = new ResourceInfo();
                                resourceInfo.id = "" + reader.GetInt32(0);
                                resourceInfo.name = reader.GetString(1);
                                resourceInfo.description = reader.GetString(2);
                                resourceInfo.location = reader.GetString(3);
                                resourceInfo.capacity = "" + reader.GetInt32(4);
                                resourceInfo.is_available = reader.GetBoolean(5).ToString();

                                listResource.Add(resourceInfo);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
}

public class ResourceInfo
{
    public string id;
    public string name;
    public string description;
    public string location;
    public string capacity;
    public string is_available;
}

