using AccessManagementWebApp.Repositories;
using AccessManagementWebApp.Models;

// Create the web application builder with command-line arguments
var builder = WebApplication.CreateBuilder(args);

// Add services to the dependency injection container
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserAccessRepository, UserAccessRepository>();

// Build the web application
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Map controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Index}/{id?}");

// Run the application
app.Run();
