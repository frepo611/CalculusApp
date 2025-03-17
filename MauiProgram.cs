using CalculusApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CalculusApp;

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

        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expressions.db");
        builder.Services.AddDbContext<Data.AppDbContext>(options => options.UseSqlite($"Filename={dbPath}"));

        builder.Services.AddHttpClient();

        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddSingleton<Services.SolutionService>();
        builder.Services.AddSingleton<Services.DatabaseService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        // Ensure the database is created
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<Data.AppDbContext>();
            dbContext.Database.EnsureCreated();
        }

        return app;
    }
}
