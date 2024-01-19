using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;


namespace DataAccessLibrary.Data;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> LoadDataAsync<T, U>(string storedProcedure, 
        U parameters, 
        string connectionStringName = "Default") 
    {
        string connectionString = _config.GetConnectionString(connectionStringName); 

        using IDbConnection connection = new SqlConnection(connectionString); 
     

        var rows = await connection.QueryAsync<T>(storedProcedure, 
            parameters,
            commandType: CommandType.StoredProcedure);

        return rows;
    }

    public async Task SaveDataAsync<T>(string storedProcedure,
        T parameters,
        string connectionStringName = "Default")
    {
        string connectionString = _config.GetConnectionString(connectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);

        try
        {
            await connection.ExecuteAsync(storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        //chjangesyx

       
    }
}
