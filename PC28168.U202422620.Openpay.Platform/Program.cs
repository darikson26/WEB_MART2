using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PC28168.U202422620.Openpay.Platform.Infrastructure.Persistence;
using PC28168.U202422620.Openpay.Platform.Infrastructure.Persistence.Repositories;
using PC28168.U202422620.Openpay.Platform.Resources;
using PC28168.U202422620.Openpay.Platform.Settlements.Application.Handlers;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Repositories;
using PC28168.U202422620.Openpay.Platform.Settlements.Interfaces.REST;
using PC28168.U202422620.Openpay.Platform.Settlements.Interfaces.REST.Resources;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<SettlementsDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySQL(connectionString ?? string.Empty);
});
builder.Services.AddScoped<IDisbursementOrderRepository, DisbursementOrderRepository>();
builder.Services.AddScoped<CreateDisbursementOrderCommandHandler>();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("en"),
        new CultureInfo("es-PE"),
        new CultureInfo("es")
    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new AcceptLanguageHeaderRequestCultureProvider()
    };
    options.ApplyCurrentCultureToResponseHeaders = true;
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<ErrorMessages>>();
        return new BadRequestObjectResult(new ErrorResource(localizer["InvalidRequest"].Value));
    };
});

var app = builder.Build();

app.UseRequestLocalization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapOpenApi();
app.MapControllers();

app.Run();
