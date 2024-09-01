using WebApp.Core;
using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();

app.Run();
