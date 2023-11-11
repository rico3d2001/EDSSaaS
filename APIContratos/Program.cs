using APIContratos.Configs;
using ContratoDTOs;
using Contratos.Dominio.Commands;
using Contratos.Dominio.Interfaces;
using EDSCore;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.RateLimiting;
using RepoGenerico;
using ServiceContratos;
using ServiceContratos.Command;
using System.Reflection;
using System.Threading.RateLimiting;
using ValidacaoHelper;
using ValidacaoHelper.Notification;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.Configure<ContratoDbConfig>(
    builder.Configuration.GetSection("EDSDatabase"));

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddSingleton<IMongoDBContextEDS, ContratoDbContexto>();
builder.Services.AddSingleton<IUnitOfWorkContratos, UnitOfWorkContratos>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader();
}));
builder.Services.AddCors(p => p.AddPolicy("corspolicy1", build =>
{
    build.WithOrigins("https://domain3.com")
    .AllowAnyMethod()
    .AllowAnyHeader();
}));
builder.Services.AddCors(p => p.AddDefaultPolicy(build =>
{
    build.WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

builder.Services.AddRateLimiter(_ => _.AddFixedWindowLimiter(policyName: "fixedwindow",
    options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 1;
        options.QueueLimit = 0;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    }).RejectionStatusCode = 401);

//var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

builder.Services.AddScoped<IRequestHandler<IniciarContratoCommand, Resultado<ContratoDOC, ValidationFalhas>>, IniciaContratoHandler>();

builder.Services.AddMediatR(c =>
{
    c.AddOpenBehavior(typeof(FastValidationBehavior<,>));
    c.RegisterServicesFromAssemblyContaining<Program>();
});

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FastValidationBehavior<,>));
builder.Services.AddFluentValidation(new[] { typeof(IniciaContratoHandler).GetTypeInfo().Assembly });
builder.Services.AddScoped<IDomainNotificationContext, DomainNotificationContext>();


var app = builder.Build();

app.UseRateLimiter();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseCors();

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
