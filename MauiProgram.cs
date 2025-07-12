using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ArriendoPocketApp.Services;
using ArriendoPocketApp.ViewModels;

namespace ArriendoPocketApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<AuthService>();
		builder.Services.AddSingleton<PropiedadService>();
		builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddSingleton<RegisterViewModel>();
		builder.Services.AddSingleton<PropiedadesViewModel>();

        var app = builder.Build();

        Ioc.Default.ConfigureServices(app.Services);

        return app;
	}
}
