using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ArriendoPocketApp.Services;
using ArriendoPocketApp.ViewModels;
using ArriendoPocketApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Storage;
using System.IO;

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
		var dbPath = Path.Combine(FileSystem.AppDataDirectory, "logs.db");
		System.Diagnostics.Debug.WriteLine($"Logs DB path: {dbPath}");
        builder.Services.AddDbContext<LogsDbContext>(options =>
			options.UseSqlite($"Filename={dbPath}"));

        builder.Services.AddSingleton<AuthService>();
		builder.Services.AddSingleton<PropiedadService>();
		builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddSingleton<RegisterViewModel>();
		builder.Services.AddSingleton<PropiedadesViewModel>();
		builder.Services.AddSingleton<LogService>();

        var app = builder.Build();

		using (var scope = app.Services.CreateScope())
		{
			var db = scope.ServiceProvider.GetRequiredService<LogsDbContext>();
			db.Database.EnsureCreated();
        }

            Ioc.Default.ConfigureServices(app.Services);

        return app;
	}
}
