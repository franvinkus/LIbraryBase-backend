using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Library.Entities;
using LibraryBase.Validator;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkSqlServer();
builder.Services.AddDbContextPool<LibraryBaseContext>(options =>
{
    var constring = configuration.GetConnectionString("DbSqlServer");
    options.UseSqlServer(constring);
});

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddValidatorsFromAssemblyContaining<SignupCustomerValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddDistributedMemoryCache(); // Memory cache untuk session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(3); // Session timeout
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var app = builder.Build();

app.UseHttpsRedirection();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.Always
});

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
