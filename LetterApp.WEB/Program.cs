using LetterApp.WEB.IdentityContext;
using LetterApp.WEB.IdentityContext.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity services
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<WebContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/home/index";
    options.AccessDeniedPath = "/user/register";
});

builder.Services.Configure<IdentityOptions>(x =>
{
    x.User.AllowedUserNameCharacters =
    "abcçdefgðhýijklmnoöprsþtuüvyzwxqABCDEFGÐHIÝJKLMNOÖPRSÞTUÜVYZWXQ0123456789-,_";


    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireDigit = false;
    x.Password.RequiredLength = 6;
    x.Password.RequireLowercase = false;
    x.Password.RequireUppercase = false;
});
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
