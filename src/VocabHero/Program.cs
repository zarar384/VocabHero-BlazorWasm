using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using VocabHero;
using VocabHero.Client.Pages;
using VocabHero.Components;
using VocabHero.Components.Account;
using VocabHero.Data;
using VocabHero.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Application Services
var assembly = typeof(Program).Assembly;

builder.Services.AddApiServices(builder.Configuration)
                .AddInfrastructureServices(builder.Configuration)
                .AddApplicationServices(builder.Configuration)
                .AddIdentityServices(builder.Configuration)
                .AddAuthenticationServices(builder.Configuration)
                .AddRazorAndMudServices();

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// global custom exception handler
//builder.Services.AddExceptionHandler<CustomExceptionHandler>();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.MapCarter();

// configure the app to use custom exception handler
app.UseExceptionHandler(options => { });

app.Run();
