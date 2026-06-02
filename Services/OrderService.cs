using PracticeForCSharp.Models;
using PracticeForCSharp.Dao;
using Microsoft.Extensions.Logging;

namespace PracticeForCSharp.Services;
public class OrderService
{
    private readonly IOrderDao _dao;

    private readonly IOrderQueryDao _logDao;

    public OrderService(IOrderDao dao, IOrderQueryDao logDao)
    {
        _dao = dao;
        _logDao = logDao;
    }

    public OrderResult? GetOrder(int orderId)
    {
        var order = _dao.GetOrderById(orderId);

        if (order == null)
        {
            return null;
        }
        _logDao.InsertLog(new OrderQueryResult
        {
            OrderId=orderId,
        });
        
        decimal total = 0;
        foreach (var detail in order.OrderDetails)
        {
            total += detail.Quantity * detail.UnitPrice;
        }
        order.TotalAmount = total;

        return order;
    }
}