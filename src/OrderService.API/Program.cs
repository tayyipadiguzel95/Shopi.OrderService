using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Business.Orders.Extension;
using OrderService.Domain.Data.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(s => 
    { 
        s.RegisterValidatorsFromAssemblyContaining<Program>();
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region RegisterServices

builder.Services.RegisterOrderService();

#endregion


builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("TestDB"));
builder.Services.AddScoped<DataContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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