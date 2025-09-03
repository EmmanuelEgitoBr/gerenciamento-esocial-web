using Gerenciamento.Informacoes.ESocial.CrossCutting.IoC;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "API de Gerenciamento de Informações",
        Version = "v1",
        Description = "Api destinada para o gerenciamento de informações que estão integradas ao E-Social",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Name = "Emmanuel Egito",
            Url = new Uri("https://github.com/EmmanuelEgitoBr/gerenciamento-esocial-web")
        }
    });

    // Adiciona o arquivo XML gerado para incluir os comentários
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
builder.Services.AddSqlServerConfiguration(builder.Configuration);
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddMediatorConfiguration();
builder.Services.AddApplicationServices();

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
