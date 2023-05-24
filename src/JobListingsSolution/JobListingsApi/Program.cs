using JobListingsApi.Adapters;
using Marten;

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

var dataConnectionString = builder.Configuration.GetConnectionString("data") ?? throw new ArgumentNullException("Need a connection string");
var kafkaConnectionString = builder.Configuration.GetConnectionString("kafka") ?? throw new ArgumentNullException("Need a kafka broker");
builder.Services.AddMarten(options =>
{
    options.Connection(dataConnectionString);
    // talk more about this later.
    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;
    }
});

builder.Services.AddCap(options =>
{
    options.UseKafka(kafkaConnectionString); // message broker
    options.UsePostgreSql(dataConnectionString); // it uses an "outbox" pattern.
    options.UseDashboard(); // just for class, but I think it's cool. 
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
