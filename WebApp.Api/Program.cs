using WebApp.Core;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddCarter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();

app.Run();
