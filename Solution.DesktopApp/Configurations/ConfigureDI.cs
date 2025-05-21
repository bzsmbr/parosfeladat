using Solution.Services.Services;

namespace Solution.DesktopApp.Configurations;

public static class ConfigureDI
{
	public static MauiAppBuilder UseDIConfiguration(this MauiAppBuilder builder)
	{
        builder.Services.AddTransient<ICompetitionService, CompetitionService>();
        builder.Services.AddTransient<ITeamService, TeamService>();
        builder.Services.AddTransient<IJuryService, JuryService>();

        builder.Services.AddScoped<IGoogleDriveService, GoogleDriveService>();


        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<CompListViewModel>();
        builder.Services.AddTransient<TeamListViewModel>();
        builder.Services.AddTransient<JuryListViewModel>();


        builder.Services.AddTransient<MainView>();
        builder.Services.AddTransient<CompListView>();
        builder.Services.AddTransient<TeamListView>();
        builder.Services.AddTransient<JuryListView>();




        return builder;
	}
}
