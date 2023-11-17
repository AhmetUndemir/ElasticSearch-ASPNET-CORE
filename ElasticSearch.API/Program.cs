using Elasticsearch.Net;
using Nest;
using ElasticSearch.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddElasticClient(builder.Configuration);

var app = builder.Build();

var pool = new SingleNodeConnectionPool(new Uri(builder.Configuration.GetSection("Elastic")["Url"]!));
var settings = new ConnectionSettings(pool);
var client = new ElasticClient(settings);
builder.Services.AddSingleton(client);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
