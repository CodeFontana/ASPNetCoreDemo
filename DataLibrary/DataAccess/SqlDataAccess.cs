using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DataLibrary.DataAccess;

public sealed class SqlDataAccess : IDataAccess
{
    public async Task<T> QueryFirstAsync<T, U>(string storedProcedure, U parameters, string connectionString, ushort? commandTimeout = 180)
    {
        await using SqlConnection connection = new(connectionString);
        T result = await connection.QueryFirstAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        return result;
    }

    public async Task<T> QueryFirstAsync<T, U>(string storedProcedure, U parameters, IDbConnection connection, ushort? commandTimeout = 180)
    {
        bool callerOpenedConnection = await OpenConnectionAsync(connection);
        T result = await connection.QueryFirstAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        if (!callerOpenedConnection && connection.State == ConnectionState.Open)
            connection.Close();
        return result;
    }

    public async Task<T?> QueryFirstOrDefaultAsync<T, U>(string storedProcedure, U parameters, string connectionString, ushort? commandTimeout = 180)
    {
        await using SqlConnection connection = new(connectionString);
        T? result = await connection.QueryFirstOrDefaultAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        return result;
    }

    public async Task<T?> QueryFirstOrDefaultAsync<T, U>(string storedProcedure, U parameters, IDbConnection connection, ushort? commandTimeout = 180)
    {
        bool callerOpenedConnection = await OpenConnectionAsync(connection);
        T? result = await connection.QueryFirstOrDefaultAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        if (!callerOpenedConnection && connection.State == ConnectionState.Open)
            connection.Close();
        return result;
    }

    public async Task<T> QuerySingleAsync<T, U>(string storedProcedure, U parameters, string connectionString, ushort? commandTimeout = 180)
    {
        await using SqlConnection connection = new(connectionString);
        T result = await connection.QuerySingleAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        return result;
    }

    public async Task<T> QuerySingleAsync<T, U>(string storedProcedure, U parameters, IDbConnection connection, ushort? commandTimeout = 180)
    {
        bool callerOpenedConnection = await OpenConnectionAsync(connection);
        T result = await connection.QuerySingleAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        if (!callerOpenedConnection && connection.State == ConnectionState.Open)
            connection.Close();
        return result;
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T, U>(string storedProcedure, U parameters, string connectionString, ushort? commandTimeout = 180)
    {
        await using SqlConnection connection = new(connectionString);
        T? result = await connection.QuerySingleOrDefaultAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        return result;
    }

    public async Task<T?> QuerySingleOrDefaultAsync<T, U>(string storedProcedure, U parameters, IDbConnection connection, ushort? commandTimeout = 180)
    {
        bool callerOpenedConnection = await OpenConnectionAsync(connection);
        T? result = await connection.QuerySingleOrDefaultAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        if (!callerOpenedConnection && connection.State == ConnectionState.Open)
            connection.Close();
        return result;
    }

    public async Task<IEnumerable<T>> QueryMultipleAsync<T>(string storedProcedure, string connectionString, ushort? commandTimeout = 180)
    {
        await using SqlConnection connection = new(connectionString);
        IEnumerable<T> result = await connection.QueryAsync<T>(storedProcedure, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        return result;
    }

    public async Task<IEnumerable<T>> QueryMultipleAsync<T>(string storedProcedure, IDbConnection connection, ushort? commandTimeout = 180)
    {
        bool callerOpenedConnection = await OpenConnectionAsync(connection);
        IEnumerable<T> result = await connection.QueryAsync<T>(storedProcedure, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        if (!callerOpenedConnection && connection.State == ConnectionState.Open)
            connection.Close();
        return result;
    }

    public async Task<IEnumerable<T>> QueryMultipleAsync<T, U>(string storedProcedure, U parameters, string connectionString, ushort? commandTimeout = 180)
    {
        await using SqlConnection connection = new(connectionString);
        IEnumerable<T> result = await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        return result;
    }

    public async Task<IEnumerable<T>> QueryMultipleAsync<T, U>(string storedProcedure, U parameters, IDbConnection connection, ushort? commandTimeout = 180)
    {
        bool callerOpenedConnection = await OpenConnectionAsync(connection);
        IEnumerable<T> result = await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        if (!callerOpenedConnection && connection.State == ConnectionState.Open)
            connection.Close();
        return result;
    }

    public IAsyncEnumerable<T> QueryMultipleUnbufferedAsync<T>(string storedProcedure, string connectionString, ushort? commandTimeout = 180, CancellationToken cancellationToken = default)
    {
        return EnumerateFromConnectionString(cancellationToken);

        async IAsyncEnumerable<T> EnumerateFromConnectionString([EnumeratorCancellation] CancellationToken ct)
        {
            await using SqlConnection connection = new(connectionString);
            await connection.OpenAsync(ct);
            IAsyncEnumerable<T> rows = connection.QueryUnbufferedAsync<T>(
                storedProcedure,
                commandType: CommandType.StoredProcedure,
                commandTimeout: commandTimeout);
            await foreach (T row in rows.WithCancellation(ct))
            {
                yield return row;
            }
        }
    }

    public IAsyncEnumerable<T> QueryMultipleUnbufferedAsync<T>(string storedProcedure, IDbConnection connection, ushort? commandTimeout = 180, CancellationToken cancellationToken = default)
    {
        return EnumerateFromConnection(cancellationToken);

        async IAsyncEnumerable<T> EnumerateFromConnection([EnumeratorCancellation] CancellationToken ct)
        {
            if (connection is not DbConnection dbConnection)
                throw new ArgumentException("QueryMultipleUnbufferedAsync requires a DbConnection (for example SqlConnection).", nameof(connection));

            bool callerOpenedConnection = await OpenConnectionAsync(connection);
            try
            {
                IAsyncEnumerable<T> rows = dbConnection.QueryUnbufferedAsync<T>(
                    storedProcedure,
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: commandTimeout);
                await foreach (T row in rows.WithCancellation(ct))
                {
                    yield return row;
                }
            }
            finally
            {
                if (!callerOpenedConnection && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
    }

    public IAsyncEnumerable<T> QueryMultipleUnbufferedAsync<T, U>(string storedProcedure, U parameters, string connectionString, ushort? commandTimeout = 180, CancellationToken cancellationToken = default)
    {
        return EnumerateFromConnectionString(cancellationToken);

        async IAsyncEnumerable<T> EnumerateFromConnectionString([EnumeratorCancellation] CancellationToken ct)
        {
            await using SqlConnection connection = new(connectionString);
            await connection.OpenAsync(ct);
            IAsyncEnumerable<T> rows = connection.QueryUnbufferedAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure,
                commandTimeout: commandTimeout);
            await foreach (T row in rows.WithCancellation(ct))
            {
                yield return row;
            }
        }
    }

    public IAsyncEnumerable<T> QueryMultipleUnbufferedAsync<T, U>(string storedProcedure, U parameters, IDbConnection connection, ushort? commandTimeout = 180, CancellationToken cancellationToken = default)
    {
        return EnumerateFromConnection(cancellationToken);

        async IAsyncEnumerable<T> EnumerateFromConnection([EnumeratorCancellation] CancellationToken ct)
        {
            if (connection is not DbConnection dbConnection)
                throw new ArgumentException("QueryMultipleUnbufferedAsync requires a DbConnection (for example SqlConnection).", nameof(connection));

            bool callerOpenedConnection = await OpenConnectionAsync(connection);
            try
            {
                IAsyncEnumerable<T> rows = dbConnection.QueryUnbufferedAsync<T>(
                    storedProcedure,
                    parameters,
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: commandTimeout);
                await foreach (T row in rows.WithCancellation(ct))
                {
                    yield return row;
                }
            }
            finally
            {
                if (!callerOpenedConnection && connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
    }

    public async Task<int> ExecuteAsync(string storedProcedure, DynamicParameters parameters, string connectionString, ushort? commandTimeout = 180)
    {
        await using SqlConnection connection = new(connectionString);
        var result = await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        return result;
    }

    public async Task<int> ExecuteAsync(string storedProcedure, DynamicParameters parameters, IDbConnection connection, ushort? commandTimeout = 180)
    {
        bool callerOpenedConnection = await OpenConnectionAsync(connection);
        var result = await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        if (!callerOpenedConnection && connection.State == ConnectionState.Open)
            connection.Close();
        return result;
    }

    public async Task<int> ExecuteAsync<T>(string storedProcedure, T parameters, string connectionString, ushort? commandTimeout = 180)
    {
        await using SqlConnection connection = new(connectionString);
        var result = await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        return result;
    }

    public async Task<int> ExecuteAsync<T>(string storedProcedure, T parameters, IDbConnection connection, ushort? commandTimeout = 180)
    {
        bool callerOpenedConnection = await OpenConnectionAsync(connection);
        var result = await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        if (!callerOpenedConnection && connection.State == ConnectionState.Open)
            connection.Close();
        return result;
    }

    public async Task<T> ExecuteAsync<T>(string storedProcedure, DynamicParameters parameters, string outputParameterName, string connectionString, ushort? commandTimeout = 180)
    {
        await using SqlConnection connection = new(connectionString);
        await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        return parameters.Get<T>(outputParameterName);
    }

    public async Task<T> ExecuteAsync<T>(string storedProcedure, DynamicParameters parameters, string outputParameterName, IDbConnection connection, ushort? commandTimeout = 180)
    {
        bool callerOpenedConnection = await OpenConnectionAsync(connection);
        await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        T output = parameters.Get<T>(outputParameterName);
        if (!callerOpenedConnection && connection.State == ConnectionState.Open)
            connection.Close();
        return output;
    }

    public async Task<T?> ExecuteAsync<T, U>(string storedProcedure, U dbEntity, string inputParameterName, string outputParameterName, string connectionString, ushort? commandTimeout = 180)
    {
        string json = JsonSerializer.Serialize(dbEntity);
        DynamicParameters parameters = new();
        parameters.Add(inputParameterName, json);
        parameters.Add(name: outputParameterName, size: Marshal.SizeOf<T>(), direction: ParameterDirection.Output);

        await using SqlConnection connection = new(connectionString);
        await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);

        string output = parameters.Get<string>(outputParameterName);
        if (string.IsNullOrWhiteSpace(output))
        {
            return default;
        }

        return (T)Convert.ChangeType(output, typeof(T));
    }

    public async Task<T?> ExecuteAsync<T, U>(string storedProcedure, U dbEntity, string inputParameterName, string outputParameterName, IDbConnection connection, ushort? commandTimeout = 180)
    {
        string json = JsonSerializer.Serialize(dbEntity);
        DynamicParameters parameters = new();
        parameters.Add(inputParameterName, json);
        parameters.Add(name: outputParameterName, size: Marshal.SizeOf<T>(), direction: ParameterDirection.Output);

        bool callerOpenedConnection = await OpenConnectionAsync(connection);
        await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: commandTimeout);
        if (!callerOpenedConnection && connection.State == ConnectionState.Open)
            connection.Close();

        string output = parameters.Get<string>(outputParameterName);
        if (string.IsNullOrWhiteSpace(output))
        {
            return default;
        }

        return (T)Convert.ChangeType(output, typeof(T));
    }

    public void ExecuteAndForget(string storedProcedure, DynamicParameters parameters, string connectionString)
    {
        _ = Task.Run(async () =>
        {
            await using SqlConnection connection = new(connectionString);
            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 180);
        });
    }

    public void ExecuteAndForget(string storedProcedure, DynamicParameters parameters, IDbConnection connection)
    {
        _ = Task.Run(async () =>
        {
            bool callerOpenedConnection = await OpenConnectionAsync(connection);
            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 180);
            if (!callerOpenedConnection && connection.State == ConnectionState.Open)
                connection.Close();
        });
    }

    public void ExecuteAndForget<T>(string storedProcedure, T parameters, string connectionString)
    {
        _ = Task.Run(async () =>
        {
            await using SqlConnection connection = new(connectionString);
            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 180);
        });
    }

    public void ExecuteAndForget<T>(string storedProcedure, T parameters, IDbConnection connection)
    {
        _ = Task.Run(async () =>
        {
            bool callerOpenedConnection = await OpenConnectionAsync(connection);
            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 180);
            if (!callerOpenedConnection && connection.State == ConnectionState.Open)
                connection.Close();
        });
    }

    private static async Task<bool> OpenConnectionAsync(IDbConnection cnn)
    {
        bool callerOpenedConnection = cnn.State == ConnectionState.Open;

        if (!callerOpenedConnection)
        {
            if (cnn is SqlConnection sqlConnection)
            {
                await sqlConnection.OpenAsync();
            }
            else
            {
                cnn.Open();
            }
        }

        return callerOpenedConnection;
    }
}
