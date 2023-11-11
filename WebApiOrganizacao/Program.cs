using EDSCore;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.RateLimiting;
using Organizacao.Dominio.Command;
using Organizacao.Dominio.Interfaces;
using OrganizacaoDTOS;
using RepoGenerico;
using ServiceOrganizacoes;
using ServiceOrganizacoes.Command;
using System.Reflection;
using System.Threading.RateLimiting;
using ValidacaoHelper;
using ValidacaoHelper.Notification;
using WebApiOrganizacao.Configs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();


builder.Services.Configure<OrganizacaoDbConfig>(
    builder.Configuration.GetSection("CustomerDatabase"));

builder.Services.Configure<S3ImageCronosConfig>(
    builder.Configuration.GetSection("S3ImageCronosConfig"));

//var emailConfig = builder.Configuration
//           .GetSection("EmailConfiguration")
//           .Get<EmailConfiguration>();
//builder.Services.AddSingleton(emailConfig);
//builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddSingleton<IMongoDBContextEDS, OrganizacaoDbContexto>();
builder.Services.AddSingleton<IUnitOfWorkOrganizacao, UnitOfWorkOrganizacao>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

//var automapper = new MapperConfiguration(item => item.AddProfile(new AutoMapperHandler()));
//IMapper mapper = automapper.CreateMapper();
//builder.Services.AddSingleton(mapper);

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







builder.Services.AddScoped<IRequestHandler<IniciaOrganizacaoCommand, Resultado<OrganizacaoDOC, ValidationFalhas>>, IniciaOrganizacaoHandler>();


builder.Services.AddMediatR(c => 
    {
        c.AddOpenBehavior(typeof(FastValidationBehavior<,>));
        c.RegisterServicesFromAssemblyContaining<Program>();
    });



builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FastValidationBehavior<,>));
builder.Services.AddFluentValidation(new[] { typeof(IniciaOrganizacaoHandler).GetTypeInfo().Assembly });

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





app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


