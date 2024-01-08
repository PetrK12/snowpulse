using Domain.Entities.BusinessEntities;

namespace Domain.Entities.OrderAggregate;

public class OrderItem : BaseEntity
{
    public OrderItem(ProductItemOrdered itemOrdered, double price, int quantity)
    {
        ItemOrdered = itemOrdered;
        Price = price;
        Quantity = quantity;
    }

    public OrderItem()
    {
        
    }
    public ProductItemOrdered ItemOrdered { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    
}