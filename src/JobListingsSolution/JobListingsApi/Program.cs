using JobListingsApi.Adapters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jobsApiUrl = builder.Configuration.GetValue<string>("jobs-api") ?? throw new ArgumentNullException("No url for jobs api");

builder.Services.AddHttpClient<JobsApiHttpAdapter>(client =>
{
    client.BaseAddress = new Uri(jobsApiUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
