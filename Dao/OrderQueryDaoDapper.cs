using Microsoft.Data.SqlClient;
using PracticeForCSharp.Models;
using Dapper;

namespace PracticeForCSharp.Dao;

public class OrderQueryDaoDapper : IOrderQueryDao
{
    private readonly string _connectionString;

    public OrderQueryDaoDapper(string connectionstring)
    {
        _connectionString = connectionstring;
    }

    public void InsertLog(OrderQueryResult log)
    {
        var sql = @"
            IF EXISTS (SELECT 1 FROM OrderQueryLogs WHERE OrderId = @OrderId)
                UPDATE OrderQueryLogs
                SET QueryTime = @QueryTime
                WHERE orderId = @OrderId
            ELSE
                INSERT INTO OrderQueryLogs(OrderId, QueryTime)
                VALUES(@OrderId, @QueryTime)
        ";
        log.QueryTime=DateTime.Now;
        
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(sql, log);
    }
}

