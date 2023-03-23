namespace Shared.DTO;

public record CustomerForUpdateDto(string FirstName, string LastName, DateOnly DateOfBirth, string PhoneNumber, string Email, string BankAccountNumber);