using MediatR;
using Microsoft.EntityFrameworkCore;
using Questao5.Application.AutoMapper;
using Questao5.Domain.Language;
using Questao5.Domain.Repository;
using Questao5.Infrastructure.CrossCutting;
using Questao5.Infrastructure.Database.Contexts;
using Questao5.Infrastructure.Database.Repository;
using Questao5.Infrastructure.Database.Uow;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// permite acessar e definir a configuração
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Configuration.AddJsonFile("appsettings.json", true, true)
                    .SetBasePath(environment.ContentRootPath)
                    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true)
                    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Notification
builder.Services.AddScoped<LogNotifications>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ILanguageSystem, LanguageSystem>();

//Others
builder.Services.AddAutoMapper(typeof(CommandToDomainMappingProfile));

builder.Services.AddTransient(typeof(IBaseConsultRepository<>), typeof(RepositoryConsult<>));

builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

//Add SqlLite
var connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
     options.UseSqlite(connectionString)
     .EnableSensitiveDataLogging()
     .UseLazyLoadingProxies());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    //});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();

// Informações úteis:
// Tipos do Sqlite - https://www.sqlite.org/datatype3.html
