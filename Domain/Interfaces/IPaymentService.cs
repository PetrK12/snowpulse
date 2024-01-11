using Domain.Entities.BusinessEntities;

namespace Domain.Interfaces;

public interface IPaymentService
{
    Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId);
}