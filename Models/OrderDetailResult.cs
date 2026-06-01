using System.Text.Json.Serialization;

namespace PracticeForCSharp.Models;

public class OrderDetailResult

{
    [JsonPropertyName("商品編號")]
    public int ProductId {get; set;}

    [JsonPropertyName("商品名稱")]
    public string ProductName {get; set;} ="";

    [JsonPropertyName("購買數量")]
    public int Quantity {get; set;}

    [JsonPropertyName("單價")]
    public decimal UnitPrice {get; set;}

}