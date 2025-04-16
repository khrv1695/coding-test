using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Application.Mapping;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(sp => new UnitOfWork(
    sp.GetRequiredService<ApplicationDbContext>(),
    sp.GetRequiredService<IProductRepository>()
));
builder.Services.AddScoped<IProductService, ProductService>(sp => new ProductService(
    sp.GetRequiredService<IUnitOfWork>(),
    sp.GetRequiredService<IMapper>()
));

builder.Services.AddAutoMapper(typeof(ProductProfile));

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
