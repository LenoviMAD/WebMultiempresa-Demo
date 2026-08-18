using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebMultiempresa.Blazor.Auth;
using WebMultiempresa.Infrastructure;
using WebMultiempresa.Infrastructure.Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Infraestructura: EF Core, repositorios, servicios, handlers
builder.Services.AddInfrastructure(builder.Configuration);

// Auth: proveedor de estado de autenticación para Blazor Server
builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("SuperAdmin", policy =>
        policy.RequireClaim("rol", "0"));
});
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<BlazorAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(
    sp => sp.GetRequiredService<BlazorAuthStateProvider>());

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await DbSeeder.SeedAsync(app.Services);

app.Run();
