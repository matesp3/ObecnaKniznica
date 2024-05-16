using Blazored.LocalStorage;
using ObecnaKniznica.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<WeatherForecastService>(); // po prvej poziadavke sa instancia udrziava pocas celeho behu aplikacie a iba tato sa vyuziva
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7156/") });
builder.Services.AddHttpClient("LibraryResources", httpClient =>      // pomenovany httpClient, vyuzitelny, ked potrebujem viac roznych API
{
	httpClient.BaseAddress = new Uri(builder.Configuration.GetValue<string>("LibraryResourcesAPI")!); // ziskaj Uri z konfiguracie
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
