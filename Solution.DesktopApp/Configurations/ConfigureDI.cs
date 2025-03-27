using Solution.Services.Services;

namespace Solution.DesktopApp.Configurations;

public static class ConfigureDI
{
	public static MauiAppBuilder UseDIConfiguration(this MauiAppBuilder builder)
	{
		builder.Services.AddTransient<MainViewModel>();

		builder.Services.AddTransient<CompCreateOrEditViewModel>();

        builder.Services.AddTransient<CompListViewModel>();

        builder.Services.AddTransient<JuryCreateOrEditViewModel>();

        builder.Services.AddTransient<JuryListViewModel>();

        builder.Services.AddTransient<TeamCreateOrEditViewModel>();

        builder.Services.AddTransient<TeamListViewModel>();

        builder.Services.AddTransient<MainView>();

        builder.Services.AddScoped<IGoogleDriveService, GoogleDriveService> ();

        return builder;
	}
}
