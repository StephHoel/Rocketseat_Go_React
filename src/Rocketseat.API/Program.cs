using Rocketseat.API.Filters;
using Rocketseat.API.Middleware;
using Rocketseat.Infra.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console().CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services
    .AddExtensions()
    .AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
        .UseSwaggerUI();
}

app.UseWebSockets();
app.UseMiddleware<WebSocketMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("=== Starting web host ===");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}