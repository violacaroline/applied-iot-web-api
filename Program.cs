using applied_iot_api.Services;
using Elasticsearch.Net;
using Nest;


var builder = WebApplication.CreateBuilder(args);

var elasticUrl = "https://localhost:9200";
var elasticUsername = "elastic";
var elasticPassword = "tgX28u4CSZpcDG3lU8Rz";

// var elasticUrl = builder.Configuration["ElasticSearch__URL"];
// var elasticUsername = builder.Configuration["ElasticSearch__Username"];
// var elasticPassword = builder.Configuration["ElasticSearch__Password"];

// Checking if the URL configuration is present, if not, throwing an exception
if (elasticUrl is null)
{
    throw new ArgumentNullException(elasticUrl, "The ElasticSearch URL configuration is missing.");
}

// Set up elastic client
var connectionSettings = new ConnectionSettings(new Uri(elasticUrl));
connectionSettings.DisableDirectStreaming();
connectionSettings.BasicAuthentication(elasticUsername, elasticPassword);
connectionSettings.ServerCertificateValidationCallback(CertificateValidations.AllowAll);

var elasticClient = new ElasticClient(connectionSettings);

// Add services to the container.
builder.Services.AddControllers();

builder.Services
  .AddSingleton<IElasticClient>(elasticClient)
  .AddScoped<IAirQualityDataService, AirQualityDataService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
var allowSpecificOrigins = "AllowSpecificOrigins";

builder.Services.AddCors(options =>
{
  options.AddPolicy(
      name: allowSpecificOrigins,
      policy =>
      {
          policy.WithOrigins("https://moti.serveo.net");
      });
});


var app = builder.Build();

app.UseCors(allowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use https when in production
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
