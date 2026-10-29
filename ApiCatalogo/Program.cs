using ApiCatalogo.Context;
using ApiCatalogo.Extensions;
using ApiCatalogo.Filters;
using ApiCatalogo.Logging;
using ApiCatalogo.Repository;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => 
                            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mysqlConnetion = builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine($"Conexão carregada: {mysqlConnetion}");

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(mysqlConnetion, ServerVersion.AutoDetect(mysqlConnetion)));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddScoped<ApiLogginFilter>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseCors("AngularApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
