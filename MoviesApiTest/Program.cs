using GrowthApi.Endpoints;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using MoviesApiTest;
using MoviesApiTest.Endpoints;
using MoviesApiTest.Repository;
using MoviesApiTest.Services;

var builder = WebApplication.CreateBuilder(args);
var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins")!;
#region Services

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("name=DefaultConnection"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(configuration =>
    {
        configuration.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod();
    });

    options.AddPolicy("free", configuration =>
    {
        configuration.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddOutputCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IActorsRepository, ActorsRepository>();
builder.Services.AddScoped<IFileStorage, LocalFileStorage>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(Program));

#endregion
var app = builder.Build();

#region Middleware

app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseCors();

app.UseOutputCache();

app.MapGet("/", [EnableCors("free")] () => "Hello World!");

app.MapGroup("/genres").MapGenres();
app.MapGroup("/actors").MapActors();

#endregion
app.Run();