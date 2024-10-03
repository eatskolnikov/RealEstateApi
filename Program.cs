using Microsoft.EntityFrameworkCore;
using RealEstateListingApi.Data;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using RealEstateListingApi.Framework.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .Select(x => new
            {
                Field = x.Key,
                Error = x.Value.Errors.First().ErrorMessage
            }).ToArray();

        var response = new
        {
            Title = "One or more validation errors occurred.",
            Status = StatusCodes.Status400BadRequest,
            Errors = errors
        };

        return new BadRequestObjectResult(response);
    };
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("RealEstateListings"));

builder.Services.AddScoped<IListingsRepository, ListingsRepository>();
builder.Services.AddScoped<IListingsService, ListingsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Real Estate Listing API", Version = "v1" });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Real Estate Listing API V1"));
// }

app.UseMiddleware<CustomExceptionMiddleware>();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
