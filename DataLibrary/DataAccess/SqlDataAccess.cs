using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DataLibrary.DataAccess;

public sealed class SqlDataAccess : IDataAccess
{
    public async Task<T> QueryFirstAsync<T, U>(string storedProcedure, U parameters, string connectionString, ushort? commandTimeout = 180)
    {
        using IDbConnection connection = new SqlConnection(connectionString);
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
        using IDbConnection connection = new SqlConnection(connectionString);
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
        using IDbConnection connection = new SqlConnection(connectionString);
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
        using IDbConnection connection = new SqlConnection(connectionString);
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
        using IDbConnection connection = new SqlConnection(connectionString);
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
        using IDbConnection connection = new SqlConnection(connectionString);
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

    public async Task<int> ExecuteAsync(string storedProcedure, DynamicParameters parameters, string connectionString, ushort? commandTimeout = 180)
    {
        using IDbConnection connection = new SqlConnection(connectionString);
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
        using IDbConnection connection = new SqlConnection(connectionString);
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
        using IDbConnection connection = new SqlConnection(connectionString);
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

        using IDbConnection connection = new SqlConnection(connectionString);
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
            using IDbConnection connection = new SqlConnection(connectionString);
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
            using IDbConnection connection = new SqlConnection(connectionString);
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