using Microsoft.EntityFrameworkCore;
using WebClient.Pages.Bookings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<BookingContext>(options =>
{
    options.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=Clients;Integrated Security=True;Encrypt=False;");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
