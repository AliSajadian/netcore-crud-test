namespace Shared.DTO;

public record CustomerDto 
{ 
    public Guid Id { get; init; } 
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!; 
    public DateTime? DateOfBirth { get; init; } = null!;
    public string? PhoneNumber { get; init; } 
    public string Email { get; init; } = null!; 
    public string? BankAccountNumber { get; init; }
}
