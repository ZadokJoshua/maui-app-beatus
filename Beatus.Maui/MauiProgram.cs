using Beatus.Maui.ViewModels;
using Beatus.Maui.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Beatus.Maui.Services;

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

        var config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build();

        // Register the configuration as a singleton service
        builder.Services.AddSingleton<IConfiguration>(config);

        builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<DetailsViewModel>();
        builder.Services.AddSingleton<DetailsPage>();

        builder.Services.AddScoped<OpenAiService>();
        builder.Services.AddScoped<CustomVisionAIService>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
