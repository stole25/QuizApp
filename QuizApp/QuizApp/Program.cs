using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using QuizApp.Server.Data;
using QuizApp.Client.Pages;
using QuizApp.Components;
using QuizApp.Components.Pages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// --- Add API controllers support ---
builder.Services.AddControllers();

// --- Add Razor Components and interactive modes ---
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// --- Register HttpClient for prerendering ---
builder.Services.AddScoped<HttpClient>(sp =>
{
    // NavigationManager will provide the base URI during prerendering.
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
});

// --- Register your DbContext using SQL Server ---
// Make sure your appsettings.json defines the "QuizDb" connection string.
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuizDb"))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// --- IMPORTANT: Map controllers BEFORE mapping Blazor components ---
app.MapControllers();

// Map the Blazor root component.
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(QuizApp.Client._Imports).Assembly);

app.Run();