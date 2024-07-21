namespace Backend.LIB.Generics.DatabaseServices;

/// <summary>
/// Stellt die Grundfunktionen für einen Service bereit der Migration in eine Datenbank durchführt
/// </summary>
/// <typeparam name="TContext">Datenbank Kontext</typeparam>
internal sealed class MigrationService<TContext> where TContext : DbContext
{
    #region private fields
    private readonly IHost _host;
    #endregion
    #region public constructors
    public MigrationService(IHost host)
    {
        _host = host;
    }
    #endregion
    #region public Methods
    /// <summary>
    /// Migriert die Datenbank auf den Aktuellsten Stand und Fügt die Stammdaten ein falls diese noch nicht in der Datenbank sind.
    /// </summary>
    public void ApplyMigrations()
    {
        var serviceProvider = _host.Services.CreateScope().ServiceProvider;

        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<MigrationService<TContext>>>();

            int maxAttempts = 3;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                try
                {
                    dbContext.Database.Migrate();
                    logger.LogInformation("Migrations applied successfully.");
                    break;
                }
                catch (Exception ex)
                {
                    attempts++;
                    logger.LogError($"Migration attempt {attempts} failed: {ex.Message}");
                    if (attempts >= maxAttempts)
                    {
                        logger.LogError("Maximum migration attempts reached. The application will now exit.");
                        Environment.Exit(1);
                    }
                }
            }
        }
    }
    #endregion
    #region private Methods

    #endregion
}