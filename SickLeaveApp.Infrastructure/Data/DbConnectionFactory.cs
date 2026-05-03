namespace SickLeaveApp.Infrastructure.Data;

using Npgsql;

public class DbConnectionFactory
{
    private readonly string _conn;

    public DbConnectionFactory(string conn)
    {
        _conn = conn;
    }

    public NpgsqlConnection Create() => new NpgsqlConnection(_conn);
}