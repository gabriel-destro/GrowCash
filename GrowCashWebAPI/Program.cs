using System.Text.Json.Serialization;
using GrowCashWebAPI.Model.Context;
using GrowCashWebAPI.Service;
using GrowCashWebAPI.Service.Implementations;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var appName = "REST API's RESTful from 0 to Azure with ASP.NET Core 8 and Docker";
var appVersion = "v1";
var appDescription = $"REST API RESTful developed in course '{appName}'";

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

builder.Services.AddControllersWithViews()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
    connection,
    new MySqlServerVersion(new Version(8, 0,29)))
);

//Dependency Injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IRevenueService, RevenueService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appName} - {appVersion}");});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseAuthorization();

app.MapControllers();
//app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

app.UseHttpsRedirection();

app.Run();
