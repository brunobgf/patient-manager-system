
using Microsoft.EntityFrameworkCore;
using PatientManager.Domain.Ports;
using PatientManager.Domain.Services;
using PatientManager.Infra.Adapters;
using PatientManager.Infra.Messaging;
using PatientManager.Infra.Persistance;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("PatientDB"));

builder.Services.AddSingleton<IConnectionFactory>(sp =>
    new ConnectionFactory() { HostName = "localhost" });

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientUseCase, PatientService>();
builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();

builder.Services.AddControllers();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();