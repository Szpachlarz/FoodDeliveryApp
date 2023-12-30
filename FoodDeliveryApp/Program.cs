using FoodDeliveryApp.Data;
using FoodDeliveryApp.Interface;
using FoodDeliveryApp.Models;
using FoodDeliveryApp.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDishRepository,  DishRepository>();
builder.Services.AddScoped<IDishCategoryRepository, DishCategoryRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
//builder.Services.AddIdentity<Restaurant, IdentityRole>()
//    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    await Seed.SeedUsersAndRolesAsync(app);
    //Seed.SeedData(app);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

//app.Use(async (context, next) =>
//{
//    if (!context.Items.ContainsKey("AlreadyRedirected"))
//    {
//        context.Items["AlreadyRedirected"] = true;

//        if (context.User.Identity.IsAuthenticated)
//        {
//            var userManager = context.RequestServices.GetRequiredService<UserManager<User>>();
//            var user = await userManager.GetUserAsync(context.User);

//            if (user != null)
//            {
//                var roles = await userManager.GetRolesAsync(user);

//                if (roles.Contains("admin"))
//                {
//                    context.Response.Redirect("/Admin/Index");
//                    return;
//                }
//                else if (roles.Contains("restaurant"))
//                {
//                    context.Response.Redirect("/Restaurant/Index");
//                    return;
//                }
//                //else if (roles.Contains("User"))
//                //{
//                //    context.Response.Redirect("/User/Index");
//                //    return;
//                //}
//            }
//        }
//    }

//    await next();
//});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
