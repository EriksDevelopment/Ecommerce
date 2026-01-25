using Ecommerce_Api.Data;
using Ecommerce_Api.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGenServices();

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API");
    c.RoutePrefix = string.Empty;
});

app.MapControllers();
app.Run();