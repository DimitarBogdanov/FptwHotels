using Blazored.SessionStorage;
using Blazorise;
using Blazorise.FluentUI2;
using Blazorise.Icons.FluentUI;
using FptwHotels.Auth;
using FptwHotels.Components;
using FptwHotels.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddBlazorise()
    .AddFluentUI2Providers()
    .AddFluentUIIcons();

builder.Services.AddDbContext<HotelDbContext>();

builder.Services.AddIdentity<UserAccountDto, IdentityRole<int>>()
    .AddEntityFrameworkStores<HotelDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, HotelAuthenticationStateProvider>();
builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

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