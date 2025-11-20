namespace Domain.Models;

public class OrderItem
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalAmount => Quantity * Price;
    public DateTime OrderDate { get; set; } = DateTime.Now;
}
