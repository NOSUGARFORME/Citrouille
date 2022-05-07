using System.Reflection;
using Citrouille.Infrastructure;
using Citrouille.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddShared();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseShared();

app.UseAuthorization();

app.MapControllers();

app.Run();