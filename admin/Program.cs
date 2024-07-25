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

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    
    // Inject custom HTML at the start of the page
    c.HeadContent = @"
        <div style='padding: 20px; background-color: #f0f0f0; text-align: center;'>
            <h1 style='color: #333;'>UK Parliament Endpoints Admin API</h1>
            <h2 style='color: #333;'>For Coach And Focus</h2>
            <p>The repo for this API is <a href='https://github.com/ChrisBrooksbank/UKParliamentEndpoints'>here</a></p>
        </div>";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
