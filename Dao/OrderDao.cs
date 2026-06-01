using Microsoft.Data.SqlClient;
using PracticeForCSharp.Models;

namespace PracticeForCSharp.Dao;

public class OrderDao
{
    private readonly string _connectionString;

    public OrderDao(string connectionString)
    {
        _connectionString = connectionString;
    }

    public OrderResult? GetOrderById(int orderId)
    {
        var sql= @"
            SELECT
                o.OrderId,
                o.OrderDate,
                od.ProductID,
                pd.ProductName,
                od.UnitPrice,
                od.Quantity

            FROM Orders o
            
            JOIN [Order Details] od 
            ON o.OrderId = od.OrderID
            
            JOIN Products pd 
            ON od.ProductID = pd.ProductID
            
            WHERE o.OrderId=@OrderId
        ";
        
        OrderResult? orderResult = null;

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@OrderId", orderId);
        connection.Open();

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            if (orderResult == null)
            {
                orderResult = new OrderResult
                {
                    OrderId = reader.GetInt32(reader.GetOrdinal("OrderID")),
                    OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")).ToString("yyyy/M/d")
                };
            }

            var detail = new OrderDetailResult
            {
                ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                UnitPrice  = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                Quantity = reader.GetInt16(reader.GetOrdinal("Quantity"))
            };

            orderResult.OrderDetails.Add(detail);
        }
        return orderResult;
    }
}

