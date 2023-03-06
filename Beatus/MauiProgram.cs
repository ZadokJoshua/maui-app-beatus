using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Beatus.Services;
using CommunityToolkit.Maui;
using Beatus.Services.Interfaces;
using Beatus.ViewModels;
using Beatus.Views;

namespace Beatus
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            
            // Register Configuration
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("Beatus.appsettings.json");
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
            builder.Services.AddSingleton<IConfiguration>(config);

            // Register Views
            builder.Services.AddSingleton<DetailsPage>();
            builder.Services.AddSingleton<SavedPage>();
            builder.Services.AddSingleton<MainPage>();

            // Register ViewModels
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<DetailsViewModel>();
            builder.Services.AddSingleton<SavedViewModel>();

            // Register Services
            builder.Services.AddScoped<OpenAiService>();
            builder.Services.AddScoped<CustomVisionAIService>();
            builder.Services.AddScoped<IDataService, DataService>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}