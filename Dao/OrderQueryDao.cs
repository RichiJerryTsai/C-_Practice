using Microsoft.Data.SqlClient;
using PracticeForCSharp.Models;

namespace PracticeForCSharp.Dao;

public class OrderQueryDao
{
    private readonly string _connectionString;

    public OrderQueryDao(string connectionstring)
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

