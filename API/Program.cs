using BL.Extensions;
using DAL.Context;
using DAL.Extensions;
using DTOs.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.LoadServiceExtetion();
builder.Services.LoadDalExtension(builder.Configuration);
builder.Services.LoadDtosExtensions();
// Add services to the container.

builder.Services.AddControllers();
// DbContext ve connection string ayarý
builder.Services.AddDbContext<ApiContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Local")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
