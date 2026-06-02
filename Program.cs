using Microsoft.Extensions.Configuration;
using PracticeForCSharp.Dao;
using PracticeForCSharp.Services;
using System.Text.Json;

var configuration  = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection")!;

IOrderDao orderDao = new OrderDaoAdoNet(connectionString);
//IOrderDao orderDao = new OrderDaoDapper(connectionString);

IOrderQueryDao orderQueryDao = new OrderQueryDaoAdoNet(connectionString);
//IOrderQueryDao orderQueryDao = new OrderQueryDaoDapper(connectionString);

var orderService = new OrderService(orderDao, orderQueryDao);

while (true)
{
    Console.Write("請輸入訂單編號： ");
    var input = Console.ReadLine();

    if (!int.TryParse(input, out var orderId))
    {
        Console.WriteLine("訂單號碼只限制輸入整數");
        continue;
    }

    var order = orderService.GetOrder(orderId);

    if (order == null)
    {
        Console.WriteLine("查無此訂單");
        continue;
    }

    var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    Console.WriteLine(JsonSerializer.Serialize(order, options));
    Console.WriteLine("輸入任意鍵查詢下一筆，或輸入「end」結束程式...");
    var nextAction = Console.ReadLine();

    if (nextAction?.ToLower() == "end")
    {
       Console.WriteLine("結束查詢");
       break;
    }
}