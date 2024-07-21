using global::System;

namespace Backend.LIB.Exceptions;

[Serializable]
internal class CannotConnectToDatabaseException : Exception
{
    internal CannotConnectToDatabaseException() { }
    internal CannotConnectToDatabaseException(Exception innerException) : base($"Es konnte keine Verbindung zur Datenbank hergestellt werden. Überprüfen Sie bitte die Verbindungszeichenfolge", innerException) { }
    internal CannotConnectToDatabaseException(string connectionString) : base($"Es konnte keine Verbindung zur Datenbank {connectionString} hergestellt werden. Überprüfen Sie bitte die Verbindungszeichenfolge")
    {
        Data.Add("ConnectionString", connectionString);
    }
}