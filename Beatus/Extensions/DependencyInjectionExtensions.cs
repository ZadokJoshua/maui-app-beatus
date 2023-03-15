using Beatus.Services;
using Beatus.Services.Interfaces;
using Beatus.ViewModels;
using Beatus.Views;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Beatus.Extensions;

public static class DependencyInjectionExtensions
{
    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<DetailsPage>();
        mauiAppBuilder.Services.AddSingleton<SavedPage>();
        mauiAppBuilder.Services.AddSingleton<MainPage>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<MainViewModel>();
        mauiAppBuilder.Services.AddSingleton<DetailsViewModel>();
        mauiAppBuilder.Services.AddSingleton<SavedViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<OpenAiService>();
        mauiAppBuilder.Services.AddSingleton<CustomVisionAIService>();
        mauiAppBuilder.Services.AddSingleton<IDataService, DataService>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterConfigFile(this MauiAppBuilder mauiAppBuilder)
    {
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("Beatus.appsettings.json");
        var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

        mauiAppBuilder.Services.AddSingleton<IConfiguration>(config);

        return mauiAppBuilder;
    }
}
