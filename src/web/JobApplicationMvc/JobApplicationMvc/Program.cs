using Microsoft.EntityFrameworkCore;
using JobApplicationMvc.Data;
using JobApplicationMvc.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var authConnectionString = builder.Configuration.GetConnectionString("Auth") ?? throw new InvalidOperationException("Connection string 'Auth' not found.");
var dataConnectionString = builder.Configuration.GetConnectionString("Data") ?? throw new InvalidOperationException("Connection string 'Data' not found.");

builder.Services.AddDbContext<JobApplicationContext>(options => options.UseNpgsql(authConnectionString));
builder.Services.AddDbContext<JobApplicationsDataContext>(options => options.UseNpgsql(dataConnectionString));

builder.Services.AddDefaultIdentity<JobApplicationMvcUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<JobApplicationContext>();

// Add services to the container.
builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
