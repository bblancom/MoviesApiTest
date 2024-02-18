using GrowthApi;
using GrowthApi.Endpoints;
using GrowthApi.Entities;
using GrowthApi.Migrations;
using GrowthApi.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddAutoMapper(typeof(Program));

#endregion
var app = builder.Build();

#region Middleware

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseOutputCache();

app.MapGet("/", [EnableCors("free")] () => "Hello World!");

app.MapGroup("/genres").MapGenres();


#endregion
app.Run();