using System.Linq.Expressions;
using Domain.Entities.OrderAggregate;

namespace Infrastructure.DataAccess.Specification.Filters;

public class OrdersWithItemsAndOrderingSpecification : BaseSpecification<Order>
{
    public OrdersWithItemsAndOrderingSpecification(string email) : base(o => o.BuyerEmail == email)
    {
        AddInclude(o => o.OrderItems);
        AddInclude(o => o.DeliveryMethod);
        AddOrderByDesc(o => o.OrderDate);
    }

    public OrdersWithItemsAndOrderingSpecification(int id, string email) 
        : base(o => o.Id == id && o.BuyerEmail == email)
    {
        AddInclude(o => o.OrderItems);
        AddInclude(o => o.DeliveryMethod);
    }
    
}