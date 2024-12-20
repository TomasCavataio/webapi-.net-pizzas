using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webapi.core.IFeaturModule;
using webapi.core.ioc;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInjectables();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret_key123456789999999999999999999999999999")) 

        };
    });

builder.Services.AddAuthorization();

builder.Services.AddDbContext<PizzaDbContext>(options =>
{
    options.UseInMemoryDatabase("pizzas");
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
        });
});


//builder.Services.AddTransient

//cada vez que tu solicitas la interface hace un new de la clase

//builder.Services.AddScoped

//cada vez que se crea un request se crea un new de la clase
//y las interfaces están disponibles

//builder.Services.AddSingleton

//se crean las intancias de las clases una sola vez
//y las interfaces están disponibles como singleton

var app = builder.Build();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();

/*var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (IAdd<Ingredient> reposiroty) =>
{
    var ingredient = Ingredient.Create("Tomate", 1.2M);
    reposiroty.Add(ingredient);
    
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();*/

app.MapFeatures();
app.MapControllers();
app.Run();

/*record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}*/



