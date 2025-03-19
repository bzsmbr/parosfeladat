namespace Solution.DataBase;

public class AppDbContext() : DbContext
{
	private static string connectionString = string.Empty;

	static AppDbContext()
	{
		connectionString = GetConnectionString();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		ArgumentNullException.ThrowIfNull(connectionString);

		base.OnConfiguring(optionsBuilder);

		optionsBuilder.UseSqlServer(connectionString);
	}

	private static string GetConnectionString()
	{
#if DEBUG
		var file = "connectionString.Development.json";
#else
        var file = "connectionString.Production.json";
#endif

		var assembly = typeof(AppDbContext).GetTypeInfo().Assembly;
		var assemblyName = assembly.GetName().Name.Replace(" ", "_");

		var stream = assembly.GetManifestResourceStream($"{assemblyName}.{file}");

		var config = new ConfigurationBuilder()
					.AddJsonStream(stream)
					.Build();

		var connectionStirng = config.GetValue<string>("SqlConnectionString");
		return connectionStirng;
	}
}
