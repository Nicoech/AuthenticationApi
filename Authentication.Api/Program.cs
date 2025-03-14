using AuthenticationApi;
using AuthenticationApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext configuration
builder.Services.AddDbContext(builder.Configuration);

// Add controllers
builder.Services.AddControllers();

// Add Swagger for development
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add JWT authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

// Add authorization
builder.Services.AddAuthorizationBuilder();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();