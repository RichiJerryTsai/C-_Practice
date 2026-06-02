using Microsoft.Data.SqlClient;
using PracticeForCSharp.Models;
using Dapper;

namespace PracticeForCSharp.Dao;

public class OrderDaoDapper : IOrderDao
{
    private readonly string _connectionString;

    public OrderDaoDapper(string connectionString)
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
        connection.Query<OrderResult, OrderDetailResult, OrderResult>(
            sql,
            (order, detail) =>
            {
                if(orderResult == null)
                {
                    orderResult = order;
                }
                orderResult.OrderDetails.Add(detail);
                return orderResult;
            },
            param: new { OrderId = orderId },
            splitOn: "ProductID"
        );
        return orderResult;
    }
}

