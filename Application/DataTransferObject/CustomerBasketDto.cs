
using System.ComponentModel.DataAnnotations;

namespace Application.DataTransferObject;

public class CustomerBasketDto
{
    [Required]
    public string Id { get; set; }
    public List<BasketItemDto> Items { get; set; }
}