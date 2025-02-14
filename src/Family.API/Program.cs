using Family.API.Extension;
using Family.Application.Abstractions;
using Family.Application.Mapping;
using Family.Application.Models.Family;
using Family.Application.Services;
using Family.Application.Validation;
using Family.Domain.Repositories.Abstractions;
using Family.Infrastructure.EntityFramework;
using Family.Infrastructure.Repositories.Implementations.EntityFramework;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "family-issuer", 
        ValidAudience = "family-audience", 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aUYNC5NmUzAXKvAGREGbiNkjPG7p3QbT"))
    };
});

//HttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextService, UserContextService>();

//logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

//automapper
builder.Services.AddAutoMapper(typeof(FamilyMemberMapping), typeof(FamilyMapping), typeof(UserInfoMapping));

//services and dbContext
builder.Services.AddDbContext<FamilyDbContext>(options =>
    options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")
                      ?? builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFamilyRepository, EfFamilyRepository>();
builder.Services.AddScoped<IFamilyMemberRepository, EfFamilyMemberRepository>();
builder.Services.AddScoped<IUserInfoRepository, EfUserInfoRepository>();
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
builder.Services.AddScoped<IFamilyService, FamilyService>();
builder.Services.AddScoped<IFamilyMemberService, FamilyMemberService>();

//controllers and validator
builder.Services.AddScoped<IValidator<FamilyCreateModel>, FamilyCreateModelValidator>();
builder.Services.AddScoped<IValidator<FamilyUpdateModel>, FamilyUpdateModelValidator>();

builder.Services.AddControllers();

var app = builder.Build();

// ѕрименение миграций при запуске приложени€
app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();