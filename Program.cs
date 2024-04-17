using Microsoft.EntityFrameworkCore;
using NewsAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//*: this is add to init the database
builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//*: this is add to detect all controllers
builder.Services.AddControllers();
builder.Services.AddCors();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000", "http://localhost:8080", "http://localhost:52084"));
//*: this is added to detect all controllers
app.MapControllers();
app.Run();


