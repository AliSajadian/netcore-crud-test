namespace Shared.DTO;

public record CustomerForUpdateDto(
    string FirstName, 
    string LastName, 
    DateTime DateOfBirth, 
    string PhoneNumber, 
    string Email, 
    string BankAccountNumber
    );