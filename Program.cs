using Microsoft.EntityFrameworkCore;
using TOHBackend.Contexts;
using TOHBackend.DTOS;
using TOHBackend.Entities;
using TOHBackend.Services;
using TOHBackend.Model;
using TOHBackend.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
       policy =>
       {
         policy.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
       });
});
// Add services to the container.
builder.Services.AddScoped<IHeroService, HeroService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<HeroAndCityServices>();
builder.Services.AddScoped<ErrorHandlerService>();
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string"
        + "'DefaultConnection' not found.");

builder.Services.AddDbContext<HeroesAndCitiesDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<City, CityDTO>().ReverseMap();
    cfg.CreateMap<Hero, HeroDTO>().ReverseMap();
});

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

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
