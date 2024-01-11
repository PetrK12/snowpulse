using Domain.Entities.OrderAggregate;

namespace Infrastructure.DataAccess.Specification.Filters;

public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
{
    public OrderByPaymentIntentIdSpecification(string paymentIntentId)
        : base(o => o.PaymentIntentId == paymentIntentId)
    { }   
}