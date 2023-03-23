namespace Shared.DTO;

public record CustomerForCreationDto(string FirstName, string LastName, DateOnly DateOfBirth, string PhoneNumber, string Email, string BankAccountNumber);