using Microsoft.Data.SqlClient;
using PracticeForCSharp.Models;
using Dapper;

namespace PracticeForCSharp.Dao;

public class OrderQueryDaoAdoNet : IOrderQueryDao
{
    private readonly string _connectionString;

    public OrderQueryDaoAdoNet(string connectionstring)
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

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);
        log.QueryTime = DateTime.Now;

        command.Parameters.AddWithValue("@OrderId", log.OrderId);
        command.Parameters.AddWithValue("@QueryTime", log.QueryTime);

        connection.Open();
        command.ExecuteNonQuery();
    }
}

