using Beatus.Maui.ViewModels;
using Beatus.Maui.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Beatus.Maui.Services;
using System.Reflection;
using Beatus.Maui.Services.Interfaces;

namespace Beatus.Maui;

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
				fonts.AddFont("Rubik-Bold.ttf", "RubikBold");
				fonts.AddFont("Rubik-Light.ttf", "RubikLight");
				fonts.AddFont("Rubik-Medium.ttf", "RubikMedium");
				fonts.AddFont("Rubik-Regular.ttf", "RubikRegular");
			});
		

		var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("Beatus.Maui.appsettings.json");
        var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

        builder.Services.AddSingleton<IConfiguration>(config);

        builder.Services.AddSingleton<DetailsPage>();
        builder.Services.AddSingleton<SavedPage>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<DetailsViewModel>();
		builder.Services.AddSingleton<SavedViewModel>();

        builder.Services.AddScoped<OpenAiService>();
        builder.Services.AddScoped<CustomVisionAIService>();
        builder.Services.AddScoped<IDataService, DataService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
