using LinuxExamAPI.StaticClass;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

AppSettingsKeys.IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

builder.Configuration.AddJsonFile(
                    $"appsettings.Production.json",
                    optional: true,
                    reloadOnChange: true
);

builder.Configuration.GetSection("AppSettingsKeys").Get<AppSettingsKeys>();

string fullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
AppSettingsKeys.ApplicationTitle = fullPath.Substring(fullPath.LastIndexOf('/') + 1);
AppSettingsKeys.TextLogPath += AppSettingsKeys.ApplicationTitle;

// Add services to the container.

//For Nginx Deploy
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddControllersWithViews()
    .AddMvcOptions((options) =>
    {
        options.Filters.Add(new ConsumesAttribute("application/json"));
    });

var app = builder.Build();

//For Nginx Deploy
app.UseForwardedHeaders();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
