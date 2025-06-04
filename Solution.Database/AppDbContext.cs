namespace Solution.DataBase;

public class AppDbContext() : DbContext
{
    public DbSet<CityEntity> Cities { get; set; }

    public DbSet<TeamMemberEntity> TeamMembers { get; set; }

    public DbSet<StreetEntity> Streets { get; set; }

    public DbSet<TeamEntity> Teams { get; set; }

    public DbSet<JuryEntity> Juries { get; set; }

    public DbSet<CompetitionEntity> Competitions { get; set; }

    private static string connectionString = string.Empty;

    static AppDbContext()
    {
        connectionString = GetConnectionString();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ArgumentNullException.ThrowIfNull(connectionString);

        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(connectionString, o => o.UseCompatibilityLevel(120));
    }

    private static string GetConnectionString()
    {
#if DEBUG
        var file = "appSettings.Development.json";
#else
        var file = "appSettings.Production.json";
#endif
        var stream = new MemoryStream(File.ReadAllBytes($"{file}"));

        var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();

        var cs = config.GetValue<string>("SqlConnectionString");
        return cs;
    }
}
