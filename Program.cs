using CatalogService.API;
using CatalogService.API.Data.Interfaces;
using CatalogService.API.Data.Repositories;
using CatalogService.API.Data;
using GreenPipes;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddDbContext<CatalogDBContext>();

var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);
builder.Services.AddSingleton(appSettings);



// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config => {
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(appSettings.RabbitMQConnectionString);
        cfg.UseHealthCheck(ctx);
        cfg.UseMessageRetry(r => r.Immediate(3));
    });
});
builder.Services.AddMassTransitHostedService();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
