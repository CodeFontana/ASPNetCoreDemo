using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace DataLibrary.DataAccess;
public interface IDataAccess
{
    void ExecuteAndForget(string storedProcedure, DynamicParameters parameters, IDbConnection connection);
    void ExecuteAndForget(string storedProcedure, DynamicParameters parameters, string connectionString);
    void ExecuteAndForget<T>(string storedProcedure, T parameters, IDbConnection connection);
    void ExecuteAndForget<T>(string storedProcedure, T parameters, string connectionString);
    Task<int> ExecuteAsync(string storedProcedure, DynamicParameters parameters, IDbConnection connection, ushort? commandTimeout = 180);
    Task<int> ExecuteAsync(string storedProcedure, DynamicParameters parameters, string connectionString, ushort? commandTimeout = 180);
    Task<T?> ExecuteAsync<T, U>(string storedProcedure, U dbEntity, string inputParameterName, string outputParameterName, IDbConnection connection, ushort? commandTimeout = 180);
    Task<T?> ExecuteAsync<T, U>(string storedProcedure, U dbEntity, string inputParameterName, string outputParameterName, string connectionString, ushort? commandTimeout = 180);
    Task<T> ExecuteAsync<T>(string storedProcedure, DynamicParameters parameters, string outputParameterName, IDbConnection connection, ushort? commandTimeout = 180);
    Task<T> ExecuteAsync<T>(string storedProcedure, DynamicParameters parameters, string outputParameterName, string connectionString, ushort? commandTimeout = 180);
    Task<int> ExecuteAsync<T>(string storedProcedure, T parameters, IDbConnection connection, ushort? commandTimeout = 180);
    Task<int> ExecuteAsync<T>(string storedProcedure, T parameters, string connectionString, ushort? commandTimeout = 180);
    Task<T> QueryFirstAsync<T, U>(string storedProcedure, U parameters, IDbConnection connection, ushort? commandTimeout = 180);
    Task<T> QueryFirstAsync<T, U>(string storedProcedure, U parameters, string connectionString, ushort? commandTimeout = 180);
    Task<T?> QueryFirstOrDefaultAsync<T, U>(string storedProcedure, U parameters, IDbConnection connection, ushort? commandTimeout = 180);
    Task<T?> QueryFirstOrDefaultAsync<T, U>(string storedProcedure, U parameters, string connectionString, ushort? commandTimeout = 180);
    Task<IEnumerable<T>> QueryMultipleAsync<T, U>(string storedProcedure, U parameters, IDbConnection connection, ushort? commandTimeout = 180);
    Task<IEnumerable<T>> QueryMultipleAsync<T, U>(string storedProcedure, U parameters, string connectionString, ushort? commandTimeout = 180);
    Task<IEnumerable<T>> QueryMultipleAsync<T>(string storedProcedure, IDbConnection connection, ushort? commandTimeout = 180);
    Task<IEnumerable<T>> QueryMultipleAsync<T>(string storedProcedure, string connectionString, ushort? commandTimeout = 180);
    Task<T> QuerySingleAsync<T, U>(string storedProcedure, U parameters, IDbConnection connection, ushort? commandTimeout = 180);
    Task<T> QuerySingleAsync<T, U>(string storedProcedure, U parameters, string connectionString, ushort? commandTimeout = 180);
    Task<T?> QuerySingleOrDefaultAsync<T, U>(string storedProcedure, U parameters, IDbConnection connection, ushort? commandTimeout = 180);
    Task<T?> QuerySingleOrDefaultAsync<T, U>(string storedProcedure, U parameters, string connectionString, ushort? commandTimeout = 180);
}