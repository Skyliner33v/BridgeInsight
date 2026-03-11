using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWASMEntityFrameworkSQLite;
using BridgeInsight;
using BridgeInsight.Data;
using BridgeInsight.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// EF Core + SQLite via BlazorWASMEntityFrameworkSQLite
builder.Services.AddBWEFSDbContextFactory<BridgeDbContext>(useMigrations: false);

// Application services
builder.Services.AddScoped<DatabaseService>();
builder.Services.AddScoped<BridgeDataService>();
builder.Services.AddScoped<ClaudeAnalysisService>();

await builder.Build().RunAsync();
