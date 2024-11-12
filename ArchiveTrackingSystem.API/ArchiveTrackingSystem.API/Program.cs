using ArchiveTrackingSystem.API.Controllers;
using ArchiveTrackingSystem.Core.Entities;
using ArchiveTrackingSystem.Core.Helper;
using ArchiveTrackingSystem.Core.IRepoistories;
using ArchiveTrackingSystem.Core.Services;
using ArchiveTrackingSystem.EF.Data;
using ArchiveTrackingSystem.EF.RepoistoriesImplementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DbContext
builder.Services.AddDbContext<ArchiveTrackingDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

#region Services
builder.Services.AddTransient<UserServices>();
builder.Services.AddTransient<RoleServices>();
builder.Services.AddTransient<AuthorizationServices>();
builder.Services.AddTransient<AuthenticatiomServices>();
builder.Services.AddTransient<ActiveServices>();
builder.Services.AddTransient<PaymentServices>();
builder.Services.AddTransient<EmployeSrevices>();
builder.Services.AddTransient<FileServices>();
builder.Services.AddTransient<AddressServices>();
builder.Services.AddTransient<FileOutSideServices>();
builder.Services.AddTransient<ArchiveServices>();
#endregion

#region Repoistory
builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseReposotory<>));
#endregion

#region Mapper
builder.Services.AddAutoMapper(typeof(ProfileMapper).Assembly);
#endregion

#region Identity

builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireNonAlphanumeric = true;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ArchiveTrackingDbContext>().AddDefaultTokenProviders();

#endregion

#region jwtSettings
var jwtSettings = new JwtSettings();
builder.Configuration.GetSection("Jwt").Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);
#endregion

#region JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.SaveToken = true;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidIssuer = jwtSettings.Issuer,
                       ValidateAudience = true,
                       ValidAudience = jwtSettings.Audience,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                   };
               });
#endregion

var app = builder.Build();

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
