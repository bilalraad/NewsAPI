using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewsAPI;
using NewsAPI.Extensions;
using NewsAPI.Interfaces;
using NewsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAppServices(builder.Configuration);
builder.Services.addJWT(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000", "http://localhost:8080", "http://localhost:52084"));

//**: these two must always be before MapControllers and after UseCors
app.UseAuthentication();
app.UseAuthorization();

//*: this is added to detect all controllers
app.MapControllers();
app.Run();


