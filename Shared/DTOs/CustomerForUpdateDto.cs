namespace Shared.DTO;

public record CustomerForUpdateDto : ICustomerDto
{ 
    // public int Id { get; init; } 
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!; 
    public DateTime? DateOfBirth { get; set; } = null!;
    public string? CountryCode { get; set; }
    public string? PhoneNumber { get; set; } 
    public string Email { get; set; } = null!; 
    public string? BankAccountNumber { get; set; }
}