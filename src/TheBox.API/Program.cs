using TheBox.API.Configurations;
using TheBox.API.Configurations.Interfaces;
using TheBox.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddEndpoints();
builder.Services.AddPersistence(builder.Configuration);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();

// Register all endpoints
var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
foreach (var endpoint in endpoints) endpoint.DefineEndpoints(app);

app.Run();