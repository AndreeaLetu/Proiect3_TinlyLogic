using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using TinyLogic_ok.Models; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<TinyLogicDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddRazorPages();

builder.Services.AddDbContext<TinyLogicDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TinyLogicDB")));


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();


app.Run();
