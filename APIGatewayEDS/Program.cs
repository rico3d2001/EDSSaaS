

using APIGatewayEDS.Configs;
using APIGatewayEDS.DI;
using EDSCore;
using FluentValidation.AspNetCore;
using HubDTOs.Documentos;
using Hubs.Dominio.Commands;
using Hubs.Dominio.Interfaces;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using RepoGenerico;
using ServiceCustomer.Handlers;
using ServiceFotoUsuario;
using ServiceHub.Handlers;
using ServicoAutorizacao;
using ServicoAutorizacao.Configs;
using ServicoAutorizacao.Handlers;
using ServicosEmailHub;
using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;
using ValidacaoHelper;
using ValidacaoHelper.Notification;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.Configure<HubDbConfig>(
    builder.Configuration.GetSection("CustomerDatabase"));

builder.Services.Configure<S3ImageCronosConfig>(
    builder.Configuration.GetSection("S3ImageCronosConfig"));

var emailConfig = builder.Configuration
           .GetSection("EmailConfiguration")
           .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddSingleton<IMongoDBContextEDS, HubDbContexto>();
builder.Services.AddSingleton<IUnitOfWorkHub, UnitOfWorkHub>();
builder.Services.AddSingleton<IRefreshHandler, RefreshHandler>();

builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();
builder.Services.AddInfraestructureSwagger();

var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
.AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
(mongoDbSettings.ConnectionString, mongoDbSettings.Name);

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

var jwtsettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtsettings);

var authkey = builder.Configuration.GetValue<string>("JwtSettings:securitykey");
builder.Services.AddAuthentication(item =>
{
    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(item =>
{
    item.RequireHttpsMetadata = true;
    item.SaveToken = true;
    item.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authkey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});


//builder.Services.AddScoped<IRequestHandler<CriaRoleCustomerCommand, Resultado<RoleCustomerDOC, ValidationFalhas>>, CriaRoleCustomerHandler>();
//builder.Services.AddScoped<IRequestHandler<UserRegistroCommand, Resultado<HubDOC, ValidationFalhas>>, ConfirmaCustomerHandler>();

builder.Services.AddScoped<IRequestHandler<ConfirmaCustomerCommand, Resultado<HubDOC, ValidationFalhas>>, ConfirmaCustomerHandler>();
builder.Services.AddScoped<IRequestHandler<GeraTokenCommand, Resultado<TokenResponse, ValidationFalhas>>, GeraTokenHandler>();
builder.Services.AddScoped<IRequestHandler<GeraRefreshTokenCommand, Resultado<TokenResponse, ValidationFalhas>>, GeraRefreshTokenHandler>();
builder.Services.AddScoped<IRequestHandler<AtribuiClaimsCommand, Resultado<IdentityResult, ValidationFalhas>>, AtribuiClaimsHandler>();
builder.Services.AddScoped<IRequestHandler<AtribuiRolesCustomerCommand, Resultado<IdentityResult, ValidationFalhas>>, AtribuiRolesCustomerHandler>();

builder.Services.AddMediatR(c =>
{
    c.AddOpenBehavior(typeof(FastValidationBehavior<,>));
    c.RegisterServicesFromAssemblyContaining<Program>();
});

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FastValidationBehavior<,>));
builder.Services.AddFluentValidation(new[] { typeof(IniciaHubHandler).GetTypeInfo().Assembly });
builder.Services.AddFluentValidation(new[] { typeof(ConfirmaCustomerHandler).GetTypeInfo().Assembly });
builder.Services.AddScoped<IDomainNotificationContext, DomainNotificationContext>();

builder.Services.AddHttpClient<IRequestHandler<IniciaHubCommand, Resultado<HubDOC, ValidationFalhas>>, IniciaHubHandler>();


var app = builder.Build();

app.UseRateLimiter();

//if (app.Environment.IsDevelopment())
//{
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway");
});
//}

app.UseCors();

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
