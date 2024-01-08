namespace Domain.Entities.OrderAggregate;

public class Address
{
    public Address(string? firstName, string? lastName, string? street, string? city, string? zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        City = city;
        ZipCode = zipCode;
    }

    public Address()
    {
    }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
}