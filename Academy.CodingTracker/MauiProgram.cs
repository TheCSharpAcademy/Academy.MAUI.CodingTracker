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
				fonts.AddFont("AlbertSans-Black.ttf", "AlbertSansBlack");
                fonts.AddFont("AlbertSans-Bold.ttf", "AlbertSansBold");
                fonts.AddFont("AlbertSans-ExtraBold.ttf", "AlbertSansExtraBold");
                fonts.AddFont("AlbertSans-ExtraLight.ttf", "AlbertSansExtraLight");
                fonts.AddFont("AlbertSans-Light.ttf", "AlbertSansLight");
                fonts.AddFont("AlbertSans-Medium.ttf", "AlbertSansMedium");
                fonts.AddFont("AlbertSans-Regular.ttf", "AlbertSansRegular");
                fonts.AddFont("AlbertSans-SemiBold.ttf", "AlbertSansSemiBold");
                fonts.AddFont("AlbertSans-Thin.ttf", "AlbertSansThin");
                fonts.AddFont("AlbertSans-Regular.ttf", "AlbertSansRegular");
                fonts.AddFont("AlbertSans-SemiBold.ttf", "AlbertSansSemiBold");
            });

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "coding.db");

        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<CodingRepository>(s, dbPath));
        builder.Services.AddSingleton<CodingService>();

        return builder.Build();
	}
}
