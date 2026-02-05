using CinemaWebsite.Data;
using CinemaWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<CustomUserModel>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=AdminDashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Staff",
    pattern: "{area:exists}/{controller=StaffDashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

//Seeding roles and an admin user when the project starts
using (var scope = app.Services.CreateScope())
{
    //Access all services
    var services = scope.ServiceProvider;
    //Role manager 
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    //User manager 
    var userManager = services.GetRequiredService<UserManager<CustomUserModel>>();

    //CREATE THE ROLES--------------------------------------------------------
    //Set the roles in an array
    string[] roles = new[] { "Admin", "Staff", "Customer" };

    //Loop through the array and create each role using the role manager
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    //SEED THE ADMIN USER-----------------------------------------------------
    var adminEmail = "admin@cinema.com";
    var adminPassword = "Admin123!";

    //check if the account already exists
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        //If the admin user does not exist, then create it.
        adminUser = new CustomUserModel
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            Fullname = "Admin",
            Address = "Admin",
            DoB = new DateOnly(0001,01,01)
        };
        //Create the user
        var result = await userManager.CreateAsync(adminUser, adminPassword);

        //If the user has been created, assign them to the admin role
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }


}




app.Run();
