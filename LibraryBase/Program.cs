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
