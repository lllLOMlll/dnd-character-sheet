using Azure.Identity;
using CharacterSheetDnD.Data;
using CharacterSheetDnD.Models;
using CharacterSheetDnD.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
//{
//	var configuration = serviceProvider.GetRequiredService<IConfiguration>();
//	var connectionString = configuration.GetConnectionString("DefaultConnection");

//	var connection = new SqlConnection(connectionString);
//	var tokenCredential = new DefaultAzureCredential();
//	var token = tokenCredential.GetToken(new Azure.Core.TokenRequestContext(new[] { "https://database.windows.net/.default" })).Token;
//	connection.AccessToken = token;

//	options.UseSqlServer(connection);
//});



builder.Services.AddSession(options => {
	options.IdleTimeout = TimeSpan.FromMinutes(300); // Set session timeout as needed
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication()
   .AddGoogle(options =>
   {
       IConfigurationSection googleAuthNSection =
       builder.Configuration.GetSection("Authentication:Google");
       options.ClientId = googleAuthNSection["ClientId"];
       options.ClientSecret = googleAuthNSection["ClientSecret"];
   });



// I added this to be able to populate scroll down list of classes (Druid, Wizard, Bard, etc.)
builder.Services.AddScoped<IClassService, ClassService>();

builder.Services.AddScoped<IArmorService, ArmorService>();

// I added this for the Google external login setup
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days...
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession(); // Activate session middleware here

app.UseRouting();

app.UseAuthentication(); // Make sure authentication is after UseRouting and before UseAuthorization
app.UseAuthorization();

app.UseForwardedHeaders(); // You've already got this set up for forwarded headers

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

