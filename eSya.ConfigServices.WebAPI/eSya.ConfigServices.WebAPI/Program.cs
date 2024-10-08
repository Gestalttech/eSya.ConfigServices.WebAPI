using eSya.ConfigServices.WebAPI.Utility;
using eSya.ConfigServices.WebAPI.Filters;
using Microsoft.Extensions.Configuration;
using DL_ConfigServices = eSya.ConfigServices.DL.Entities;
using eSya.ConfigServices.IF;
using eSya.ConfigServices.DL.Repository;
using Microsoft.Extensions.Localization;
using eSya.ConfigServices.DL.Localization;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

DL_ConfigServices.eSyaEnterprise._connString = builder.Configuration.GetConnectionString("dbConn_eSyaEnterprise");

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ApikeyAuthAttribute>();
});

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<HttpAuthAttribute>();
});

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<CultureAuthAttribute>();
});
//Localization

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
                    //new CultureInfo(name:"en-IN"),
                    new CultureInfo(name:"en-US"),
                    new CultureInfo("hi-IN"),
                    new CultureInfo(name:"ar-EG"),
                };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

});

builder.Services.AddLocalization();
//localization


builder.Services.AddScoped<ICommonMethodRepository, CommonMethodRepository>();
builder.Services.AddScoped<IServiceCodesRepository, ServiceCodesRepository>();
builder.Services.AddScoped<IClinicServicesRepository, ClinicServicesRepository>();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();

//Localization

var supportedCultures = new[] { /*"en-IN", */ "en-US", "hi-IN", "ar-EG" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);
//Localization



app.MapControllers();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=values}/{action=Get}/{id?}");

app.Run();
