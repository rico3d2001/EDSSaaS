using BlazorPortal.Auth;
using BlazorPortal.Client.Pages;
using BlazorPortal.Components;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<TokenAutheticationProvider>();
builder.Services.AddScoped<IAuthorizeService, TokenAutheticationProvider>(
    provider => provider.GetRequiredService<TokenAutheticationProvider>()
    );

builder.Services.AddScoped<AuthenticationStateProvider, TokenAutheticationProvider>(
    provider => provider.GetRequiredService<TokenAutheticationProvider>()
    );
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.Run();
