using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.Db;

public interface IDataAccess
{
    Task<List<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters, string connectionStringName);
    Task<int> ExecuteAsync<T>(string storedProcedure, T parameters, string connectionStringName);
}