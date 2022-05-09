using Citrouille.Infrastructure;
using Citrouille.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddShared()
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

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

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());



app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Initialize();

app.Run();
