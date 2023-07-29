using MVC_Task.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

string connString = app.Configuration.GetConnectionString("dbConnection");
DataLayer.dbcon = Convert.ToString(connString);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "student",
    pattern: "EnrollStudent",
    defaults: new { controller = "Home", action = "EnrollStudent" });
            
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=EnrollStudent}/{id?}");

app.Run();
