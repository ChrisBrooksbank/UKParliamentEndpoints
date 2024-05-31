using UKParliamentEndPointsAdmin.Shared;

var builder = WebApplication.CreateBuilder(args);

// Configure settings
var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var configuration = configurationBuilder.Build();
builder.Services.Configure<AzureStorageSettings>(configuration.GetSection("AzureStorage"));

// Add services to the container.
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IParliamentEndPointService, ParliamentEndPointService>();
builder.Services.AddScoped<IEndPointMapper, EndPointMapper>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
