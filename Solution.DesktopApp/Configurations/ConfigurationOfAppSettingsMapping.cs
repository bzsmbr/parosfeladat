namespace Solution.DesktopApp.Configurations;

public static class ConfigurationOfAppSettingsMapping
{
    public static MauiAppBuilder UseAppSettingsMapping(this MauiAppBuilder builder)
    {
        var googleDriveSettings = builder.Configuration.GetRequiredSection("GoogleDrive").Get<GoogleDriveSettings>();
        builder.Services.AddSingleton<GoogleDriveSettings>(googleDriveSettings);

        return builder;
    }
}