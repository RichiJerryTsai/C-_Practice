using System.Text.Json.Serialization;

namespace PracticeForCSharp.Models;

public class OrderResult
{
    [JsonPropertyName("訂單編號")]

    public int OrderId { get; set; }

    [JsonPropertyName("訂購日期")]

    public string OrderDate { get; set; }="";

    [JsonPropertyName("訂購金額")]
    
    public decimal TotalAmount{ get; set; }

    [JsonPropertyName("訂購商品清單")]

    public List<OrderDetailResult> OrderDetails { get; set;} = new();
}