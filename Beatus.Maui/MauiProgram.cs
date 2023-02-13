using Beatus.Maui.ViewModels;
using Beatus.Maui.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

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

		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddSingleton<MainPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
