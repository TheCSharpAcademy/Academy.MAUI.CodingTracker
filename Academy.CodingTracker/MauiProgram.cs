using Academy.CodingTracker.Data;

namespace Academy.CodingTracker;

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

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "coding.db");

        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<CodingRepository>(s, dbPath));

        return builder.Build();
	}
}
