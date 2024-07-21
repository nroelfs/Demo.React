using global::System;

namespace Backend.LIB.Generics.DatabaseServices;

/// <summary>
/// Service zum Migrieren der Datenbank im Hintergrund
/// </summary>
/// <typeparam name="TContext">Datenak Kontext</typeparam>
internal sealed class MigrationBackgroundService<TContext> : IHostedService where TContext : DbContext
{
    #region private fields
    private readonly IServiceProvider _serviceProvider;
    #endregion
    #region public constructor
    public MigrationBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    #endregion
    #region public methods
    /// <summary>
    /// Startet den MigrationService
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var migrationService = scope.ServiceProvider.GetRequiredService<MigrationService<TContext>>();
            migrationService.ApplyMigrations();
        }

        return Task.CompletedTask;
    }
    /// <summary>
    /// stoppt den MigrationService
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    #endregion
}