using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsAPI.Data;
using NewsAPI.Entities;
using NewsAPI.Extensions;
using NewsAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAppServices(builder.Configuration);
builder.Services.addJWT(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
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

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<NewsAPI.Context>();
    var userManager = services.GetRequiredService<UserManager<NewsAPI.AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    await context.Database.MigrateAsync();
    await Seed.UsersSeed(userManager, roleManager);
    await Seed.NewsSeed(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
    throw;
}


app.Run();


