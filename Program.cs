using Microsoft.AspNetCore.Mvc.RazorPages;
using Trava.Blazor.Services.Client;
using Trava.Blazor.Services.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.Configure<RazorPagesOptions>(options =>
{
    options.RootDirectory = "/Blazor";
});

//PER SERVER INSTANCE
builder.Services.AddSingleton<ILemmaService>();
builder.Services.AddSingleton<ILexemeService>();

//PER CLIENT INSTANCE
builder.Services.AddScoped<IBrowserLogger>();
builder.Services.AddScoped<ISpeechService>();

builder.Services.AddBlazorBootstrap();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();

app.MapFallbackToPage("/Index");

await app.RunAsync();
