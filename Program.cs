using Trava.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//PER SERVER INSTANCE
builder.Services.AddSingleton<MorphologyService>();
builder.Services.AddSingleton<TranslationService>();

//PER CLIENT INSTANCE
//builder.Services.AddScoped<ClientService>();

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
