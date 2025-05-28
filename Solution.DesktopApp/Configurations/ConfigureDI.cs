using Solution.Services.Services;

namespace Solution.DesktopApp.Configurations;

public static class ConfigureDI
{
	public static MauiAppBuilder UseDIConfiguration(this MauiAppBuilder builder)
	{
		builder.Services.AddTransient<MainViewModel>();


        builder.Services.AddTransient<ICompetitionService, CompetitionService>();

        builder.Services.AddTransient<CompCreateOrEditViewModel>();

        builder.Services.AddTransient<CompListViewModel>();


        builder.Services.AddTransient<IJuryService, JuryService>();

        builder.Services.AddTransient<JuryCreateOrEditViewModel>();

        builder.Services.AddTransient<JuryListViewModel>();


        builder.Services.AddTransient<ITeamService, TeamService>();

        builder.Services.AddTransient<TeamCreateOrEditViewModel>();

        builder.Services.AddTransient<TeamListViewModel>();


        builder.Services.AddTransient<ITeamMemberService, TeamMemberService>();

        builder.Services.AddTransient<TeamMemberCreateOrEditViewModel>();

        builder.Services.AddTransient<TeamMemberListViewModel>();


        builder.Services.AddTransient<MainView>();

        builder.Services.AddScoped<IGoogleDriveService, GoogleDriveService> ();

        return builder;
	}
}
