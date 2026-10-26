using ApiCatalogo.Context;
using ApiCatalogo.Services;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => 
                            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mysqlConnetion = builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine($"Conexão carregada: {mysqlConnetion}");

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(mysqlConnetion, ServerVersion.AutoDetect(mysqlConnetion)));


builder.Services.AddTransient<IMeuServices, MeuServices>();


var app = builder.Build();

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
