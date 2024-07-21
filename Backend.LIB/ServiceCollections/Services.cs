

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Backend.LIB.ServiceCollections;

/// <summary>
/// Stellt Methoden bereit um Services zu registrieren
/// </summary>
public static class Services
{
    /// <summary>
    /// Stellt die Services für eine MS SQL Datenbank bereit
    /// </summary>
    /// <typeparam name="TContext">Datenbank Context</typeparam>
    /// <param name="services"></param>
    /// <param name="connectionString">Verbindungszeichenfolge zur Datenbank</param>
    /// <returns></returns>
    public static IServiceCollection AddDatabaseServices<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
    {
        if (connectionString.IsNullOrEmpty())
        {
            throw new CannotConnectToDatabaseException(connectionString);
        }
        services.AddDbContext<TContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
        services.TryAddScoped<DatabaseContextFactory<TContext>>(provider =>
        {
            var dbContextOptions = provider.GetRequiredService<DbContextOptions<TContext>>();
            return new DatabaseContextFactory<TContext>(dbContextOptions);
        });
        services.TryAddScoped<MigrationService<TContext>>();
        return services;
    }
    /// <summary>
    /// Stellt die Services für die Datenbank bereit und fügt einen Hintergrund Services hinzu
    /// Fügt einen Migrationsservice hinzu
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDatabaseAndHostedServices<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
    {
        services.AddDatabaseServices<TContext>(connectionString);
        services.AddHostedService<MigrationBackgroundService<TContext>>();
        return services;
    }

    /// <summary>
    /// Stellt die Services für die PostGres Datenbank bereit
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    /// <exception cref="CannotConnectToDatabaseException"></exception>
    public static IServiceCollection AddPostGResDatabase<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
    {
        if (connectionString.IsNullOrEmpty())
        {
            throw new CannotConnectToDatabaseException(connectionString);
        }
        services.AddDbContext<TContext>(opt =>
        {
            opt.UseNpgsql(connectionString);
        });
        services.TryAddScoped<DatabaseContextFactory<TContext>>(provider =>
        {
            var dbContextOptions = provider.GetRequiredService<DbContextOptions<TContext>>();
            return new DatabaseContextFactory<TContext>(dbContextOptions);
        });
        services.TryAddScoped<MigrationService<TContext>>();
        return services;
    }

    /// <summary>
    /// Stellt die Services für die Datenbank bereit und fügt einen Hintergrund Services hinzu
    /// Fügt einen Migrationsservice hinzu
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddPostGresDatabaseAndHostedServices<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
    {
        services.AddPostGResDatabase<TContext>(connectionString);
        services.AddHostedService<MigrationBackgroundService<TContext>>();
        return services;
    }
}