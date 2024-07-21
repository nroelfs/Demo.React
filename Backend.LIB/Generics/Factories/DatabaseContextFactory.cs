using global::System;

namespace Backend.LIB.Generics.Factories;
public class DatabaseContextFactory<TContext> : IDisposable where TContext : DbContext
{
    #region private members
    private readonly DbContextOptions<TContext> _options;
    #endregion
    #region public constructors
    public DatabaseContextFactory(DbContextOptions<TContext> options)
    {
        _options = options;
    }
    #endregion
    #region public methods
    /// <summary>
    /// Erstellt eine neue Instanz der Datenbank
    /// </summary>
    /// <returns></returns>
    public TContext CreateDbContext()
    {
        return Activator.CreateInstance(typeof(TContext), _options) as TContext ?? throw new ArgumentNullException();
    }
    /// <summary>
    /// Schließt die Instanz der  Datenbank
    /// </summary>
    public void Dispose()
    {
        using (var context = CreateDbContext())
        {
            context.Dispose();
        }
    }

    #endregion
}