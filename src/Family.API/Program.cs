using Family.API.Extension;
using Family.Application.Abstractions;
using Family.Application.Mapping;
using Family.Application.Models.Family;
using Family.Application.Models.FamilyMember;
using Family.Application.Services;
using Family.Application.Validation;
using Family.Domain.Repositories.Abstractions;
using Family.Infrastructure.EntityFramework;
using Family.Infrastructure.Repositories.Implementations.EntityFramework;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

//automapper
builder.Services.AddAutoMapper(typeof(FamilyMemberMapping), typeof(FamilyMapping));

//services and dbContext
builder.Services.AddDbContext<FamilyDbContext>(options =>
    options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")
                      ?? builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFamilyRepository, EfFamilyRepository>();
builder.Services.AddScoped<IFamilyMemberRepository, EfFamilyMemberRepository>();
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
builder.Services.AddScoped<IFamilyService, FamilyService>();
builder.Services.AddScoped<IFamilyMemberService, FamilyMemberService>();

//controllers and validator
builder.Services.AddScoped<IValidator<FamilyAndFamilyHeadCreateModel>, FamilyAndFamilyHeadCreateModelValidator>();
builder.Services.AddScoped<IValidator<FamilyUpdateModel>, FamilyUpdateModelValidator>();
builder.Services.AddScoped<IValidator<FamilyMemberCreateModel>, FamilyMemberCreateModelValidator>();

builder.Services.AddControllers();

var app = builder.Build();

// Применение миграций при запуске приложения
//app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();