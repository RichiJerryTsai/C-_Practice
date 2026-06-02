using PracticeForCSharp.Models;

namespace PracticeForCSharp.Dao;

public interface IOrderDao
{
    OrderResult? GetOrderById(int orderId);
}

public interface IOrderQueryDao
{
    void InsertLog(OrderQueryResult log);
}