using IdentityServer.WebApp.Areas.Identity.Data.IdentityServer;
using IdentityServer.WebApp.Data.IdentityServer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("WebAppContextConnection") ??
                       throw new InvalidOperationException("Connection string 'WebAppContextConnection' not found.");

// Add services to the container.
builder.Services.AddDbContext<WebAppContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<WebAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<WebAppContext>();

// Add Razor Pages for Identity UI
builder.Services.AddRazorPages();

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

app.MapGet("/", context =>
{
    if (!context.User.Identity.IsAuthenticated)
        context.Response.Redirect("/Identity/Account/Login"); // Redirect to the login page
    return Task.CompletedTask;
});


app.Run();