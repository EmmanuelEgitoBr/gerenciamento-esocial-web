using Gerenciamento.Informacoes.ESocial.Api.Extensions;
using Gerenciamento.Informacoes.ESocial.CrossCutting.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.AddSwaggerConfiguration();
builder.Services.AddSqlServerConfiguration(builder.Configuration);
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddMediatorConfiguration();
builder.Services.AddApplicationServices();
builder.AddAuthConfiguration();
builder.Services.AddSecurityInfrastructure(builder.Configuration);
builder.Services.AddRabbitMqConfiguration();
builder.AddEmailConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
