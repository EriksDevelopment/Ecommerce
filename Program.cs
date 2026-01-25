using Ecommerce_Api.Data;
using Ecommerce_Api.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGenServices();
builder.Services.AddAuthenticationServices(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddScopedServices();
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API");
    c.RoutePrefix = string.Empty;
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();