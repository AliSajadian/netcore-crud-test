namespace Shared.DTO;

public record CustomerForCreationDto : ICustomerDto
{ 
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!; 
    public DateTime? DateOfBirth { get; set; } = null!;
    public string? PhoneNumber { get; set; } 
    public string Email { get; set; } = null!; 
    public string? BankAccountNumber { get; set; }
}