using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourceManagement_Fe_App.Authentication;
using ResourceManagement_Fe_App.Data;
using ResourceManagement_Fe_App.Helpers;
using ResourceManagement_Fe_App.Helpers.Clients;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var moduleConfiguration = builder.Configuration.GetSection("Modules");

//custom options
builder.Services.Configure<ServerOptions>(moduleConfiguration.GetSection("ServerOptions"));

builder.Services.AddAntDesign();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

//ApiClient
builder.Services.AddScoped<IRmHttpClient, RmHttpClient>();
builder.Services.AddScoped<TransactionApiClient>();

var baseUrl = moduleConfiguration.GetValue<string>("ServerOptions:BaseUrl");
builder.Services.AddHttpClient("RM", c => {
    

	c.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
builder.Services.AddScoped<IAuthenticationHelperWriter, AuthenticationHelper>();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        GlobalCulture.CurrentCulture
    };

    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
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
