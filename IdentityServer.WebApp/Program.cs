using IdentityServer.WebApp.Areas.Identity.Data.IdentityServer;
using IdentityServer.WebApp.Data.IdentityServer;
using IdentityServer.WebApp.MailService.Models;
using IdentityServer.WebApp.MailService.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("WebAppContextConnection") ??
                       throw new InvalidOperationException("Connection string 'WebAppContextConnection' not found.");

// Add services to the container.

// Register EmailSender service
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddDbContext<WebAppContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<WebAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<WebAppContext>()
    .AddDefaultTokenProviders();;

// Add Razor Pages for Identity UI
builder.Services.AddRazorPages();

// Add Google authentication
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    })
    .AddFacebook(options =>
    {
        options.AppId = builder.Configuration["Authentication:Facebook:AppId"];
        options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
        options.Scope.Add("email");  // Add 'email' permission explicitly
        options.Fields.Add("email");  // Ensure you request the 'email' field
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Enable authentication
app.UseAuthorization(); // Enable authorization

// Map Razor Pages
app.MapRazorPages();
app.MapDefaultControllerRoute();


app.Run();