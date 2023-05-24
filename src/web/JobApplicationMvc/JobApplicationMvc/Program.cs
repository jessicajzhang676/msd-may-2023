using Microsoft.EntityFrameworkCore;
using JobApplicationMvc.Data;
using JobApplicationMvc.Areas.Identity.Data;
using JobApplicationMvc;
using JobApplicationMvc.Adapters;

var builder = WebApplication.CreateBuilder(args);
var authConnectionString = builder.Configuration.GetConnectionString("Auth") ?? throw new InvalidOperationException("Connection string 'Auth' not found.");
var dataConnectionString = builder.Configuration.GetConnectionString("Data") ?? throw new InvalidOperationException("Connection string 'Data' not found.");

builder.Services.AddDbContext<JobApplicationContext>(options => options.UseNpgsql(authConnectionString));
builder.Services.AddDbContext<JobApplicationsDataContext>(options => options.UseNpgsql(dataConnectionString));

builder.Services.AddDefaultIdentity<JobApplicationMvcUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<JobApplicationContext>();

// Add services to the container.
builder.Services.AddRazorPages();
var kafkaConnectionString = builder.Configuration.GetConnectionString("kafka") ?? throw new ArgumentNullException("Need a kafka broker");

builder.Services.AddTransient<JobOpeningsSubscriber>();

builder.Services.AddCap(options =>
{
    options.UseKafka(kafkaConnectionString);
    options.UsePostgreSql(dataConnectionString); // it uses an "outbox" pattern.
    options.UseDashboard(); // just for class, but I think it's cool. 
});

var jobsApiUrl = builder.Configuration.GetValue<string>("jobs-api-url") ?? throw new ArgumentNullException("Needs a Jobs API Url");
builder.Services.AddHttpClient<JobsApiAdapter>((client) =>
{
    client.BaseAddress = new Uri(jobsApiUrl);
});
var jobsOpeningsApiUrl = builder.Configuration.GetValue<string>("jobs-openings-api-url") ?? throw new ArgumentNullException("Needs a Jobs API Url");
builder.Services.AddHttpClient<JobOpeningsApiAdapter>((client) =>
{
    client.BaseAddress = new Uri(jobsOpeningsApiUrl);
});
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
