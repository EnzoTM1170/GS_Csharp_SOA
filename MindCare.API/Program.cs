using Microsoft.EntityFrameworkCore;
using MindCare.Application.Interfaces;
using MindCare.Application.Mappings;
using MindCare.Application.Services;
using MindCare.Application.Validators;
using MindCare.Infrastructure.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Data Source=MindCareDB.db";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IHealthMetricService, HealthMetricService>();
builder.Services.AddScoped<IEmotionalAnalysisService, EmotionalAnalysisService>();
builder.Services.AddScoped<IStressAlertService, StressAlertService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddHttpClient<IExternalAPIService, ExternalAPIService>();
builder.Services.AddScoped<IExternalAPIService, ExternalAPIService>();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(typeof(CreateEmployeeDTOValidator).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Ensure database is created
try
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.EnsureCreated();
        Console.WriteLine("Banco de dados verificado/criado.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"AVISO: Erro ao criar banco de dados: {ex.Message}");
    Console.WriteLine("A API continuará rodando, mas pode não ter dados.");
}

// Popular dados em background (não bloqueia a inicialização)
_ = Task.Run(() =>
{
    try
    {
        Thread.Sleep(2000); // Aguardar servidor iniciar
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            MindCare.Infrastructure.Data.DbSeeder.SeedData(db);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao popular banco de dados: {ex.Message}");
    }
});

app.Run();

