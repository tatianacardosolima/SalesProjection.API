using Nest;
using SalesProjection.Application.Abstraction.Made.IRepositories;
using SalesProjection.Application.Abstraction.Made.IUseCases;
using SalesProjection.Application.Made.UseCases;
using SalesProjection.Application.Projection.Factories;
using SalesProjection.Application.Projection.UseCases;
using SalesProjection.Domain.Entities;
using SalesProjection.Infrastructure.Database.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar o ElasticClient
// 4T+=xbigWVCai1F4N1wZ
var elasticSettings = new ConnectionSettings(new Uri("http://localhost:9200/"))
    .BasicAuthentication("elastic", "4T+=xbigWVCai1F4N1wZ")
    .DefaultIndex("sales-projection")    
    .ServerCertificateValidationCallback((sender, cert, chain, sslPolicyErrors) => true)
    .DisablePing()
    .EnableDebugMode();

var elasticClient = new ElasticClient(elasticSettings);
var indexExistsResponse = await elasticClient.Indices.ExistsAsync("sales-projection");
if (!indexExistsResponse.Exists)
{
    var createIndexResponse = await elasticClient.Indices.CreateAsync("sales-projection", c => c
        .Map<ProcessLot>(m => m.AutoMap())
    );

    if (!createIndexResponse.IsValid)
    {
        Console.WriteLine($"Erro ao criar índice: {createIndexResponse.ServerError?.Error.Reason}");
    }
    else
    {
        Console.WriteLine("Índice criado com sucesso.");
    }
}

// Registrar o ElasticClient no DI
builder.Services.AddSingleton(elasticClient);



builder.Services.AddTransient<IProcessLotRepository, ProcessLotRepository>();
builder.Services.AddTransient<ILoadSalesMadeUseCase, LoadSalesMadeUseCase>();
builder.Services.AddTransient<IProjectionFactory, ProjectionFactory>();
builder.Services.AddTransient<IGetProjectCurrentMonthUseCase, GetProjectCurrentMonthUseCase>();
builder.Services.AddTransient<ICleanLoadSalesUseCase, CleanLoadSalesUseCase>();


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
