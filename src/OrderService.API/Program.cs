using System.Text;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrderService.Application.Business.Orders.Validations;
using OrderService.Application.Common.Extensions;
using OrderService.Infrastructure.Public;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers()
    .AddFluentValidation(s =>
    {
        s.RegisterValidatorsFromAssemblyContaining<Program>();
        s.RegisterValidatorsFromAssemblyContaining<OrderValidator>();
    });


builder.Services.Register(builder.Configuration);


#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Basic Microservice",
        Description = "Tayyip A",
        TermsOfService = new Uri("http://localhost:5001"),
        Contact = new OpenApiContact { Name = "", Email = "", Url = new Uri("http://localhost:5001/") }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { In = ParameterLocation.Header, Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = SecuritySchemeType.ApiKey });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new[] { "Bearer", "writeAccess" }
        }
    });
});

builder.Services.AddFluentValidationRulesToSwagger();

#endregion
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

ServiceProviderFactory.Configure(app.Services);


app.Run();