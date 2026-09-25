using Customer.Application;
using Customer.Infrastructure;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
using System.Text.Json.Serialization;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Service", "Customer.api")
    .WriteTo.Console(
        outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Service} | CorrelationId={CorrelationId} | {Message:lj}{Newline}{Exception}")
    .CreateBootstrapLogger();

Log.Information("Starting Customer API...");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, services, config) => config
    .ReadFrom.Configuration(ctx.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Service", "Customer.api")
    .WriteTo.Console(
        outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Service} | CorrelationId={CorrelationId} | {Message:lj}{Newline}{Exception}"));

// Add services to the container.

// OpenAPI uses Http.Json options to generate its schema, while controllers use MVC options.
// Configure both so Scalar documents and the API accept the same string enum values.
builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter(allowIntegerValues: false)));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(allowIntegerValues: false)));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
app.MapOpenApi();
app.MapScalarApiReference(options => options.WithTitle("Customer API")
    .WithTheme(ScalarTheme.Kepler)
    .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
);
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
