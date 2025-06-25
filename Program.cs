using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trava.Blazor.Services.Client;
using Trava.Blazor.Services.Server;

//dotnet run --property WarningLevel=0 
var builder = WebApplication.CreateBuilder(args);

//Blazor Spam Supression
builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);
builder.Logging.AddFilter("Microsoft", LogLevel.Warning);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.Configure<RazorPagesOptions>(options =>
{
    options.RootDirectory = "/Blazor";
});

builder.Services.Configure<AuthConfig>(builder.Configuration.GetSection("AuthConfig"));

//PER SERVER INSTANCE
builder.Services.AddSingleton<IServerLogger>(); //Logger
builder.Services.AddSingleton<IAuthorizationService>(); //Network Config
builder.Services.AddSingleton<CircuitHandler>(sp => sp.GetRequiredService<IAuthorizationService>()); //Network Config

builder.Services.AddSingleton<INoteService>();
builder.Services.AddSingleton<ILemmaService>();
builder.Services.AddSingleton<ILexemeService>();


//PER CLIENT INSTANCE
builder.Services.AddScoped<IBrowserLogger>(); //Logger
builder.Services.AddScoped<INetworkIdentity>(); //Network Config
builder.Services.AddScoped<CircuitHandler>(sp => sp.GetRequiredService<INetworkIdentity>()); //Network Config

builder.Services.AddScoped<ISpeechService>();

builder.Services.AddBlazorBootstrap();
var app = builder.Build();

IServerLogger Logger = app.Services.GetRequiredService<IServerLogger>();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();

app.MapFallbackToPage("/Index");

Logger.Log("Server Started.", IServerLogger.LogSource.System);

await app.RunAsync();

Logger.Log("Server Stopped.", IServerLogger.LogSource.System);