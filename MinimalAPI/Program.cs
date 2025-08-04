using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScalar();

var app = builder.Build();

app.MapGet("/saludo", () => "Hola desde Scalar!");

app.UseScalarUI();

app.Run();
