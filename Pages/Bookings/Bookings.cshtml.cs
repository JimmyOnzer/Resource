using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Pages.Resource;

namespace WebClient.Pages.Bookings
{
    public class BookingsModel : PageModel
    {
        public BookingInfo bookingInfo = new BookingInfo();
        public String errorMessage = "";
        public String successMessage = "";


        public void OnGet()
        {

            bookingInfo.bookedBy = Request.Form["startTime"];
            bookingInfo.purpose = Request.Form["endTime"];
            bookingInfo.bookedBy = Request.Form["bookedBy"];
            bookingInfo.purpose = Request.Form["purpose"];

            if (bookingInfo.bookedBy.Length == 0 || bookingInfo.purpose.Length == 0) {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Clients;Integrated Security=True;Encrypt=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Booking " +
                    "(startTime, endTime, bookedBy, purpose) VALUES" +
                    "(@startTime, @endTime, @bookedBy, @purpose);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("startTime", bookingInfo.startTime);
                        command.Parameters.AddWithValue("endTime", bookingInfo.endTime);
                        command.Parameters.AddWithValue("bookedBy", bookingInfo.bookedBy);
                        command.Parameters.AddWithValue("purpose", bookingInfo.purpose);

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
            bookingInfo.startTime = ""; bookingInfo.endTime = ""; bookingInfo.bookedBy = ""; bookingInfo.purpose = "";
            successMessage = "New Resource Added Successfully";

            Response.Redirect("/Resource/Resource");


        } } }

public class BookingInfo
        {
            public int id { get; set; }
            public string resourceId { get; set; }
            public string startTime { get; set; }
            public string endTime { get; set; }
            public string bookedBy { get; set; }
            public string purpose { get; set; }
        } 

