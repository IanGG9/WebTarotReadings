using Microsoft.EntityFrameworkCore;
using WebTarotReadings;
using WebTarotReadings.Components;
using WebTarotReadings.Models;
using WebTarotReadings.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TarotContext") ??
           throw new InvalidOperationException("Connection string TarotContext not found")));

builder.Services.AddScoped<HoroscopeSignService>();
builder.Services.AddScoped<TarotCardsService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<LoggedInUserModel>();

builder.Services.AddBlazorBootstrap();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
