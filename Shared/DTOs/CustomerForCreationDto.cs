namespace Shared.DTO;

public record CustomerForCreationDto(
    string FirstName, 
    string LastName, 
    DateTime DateOfBirth, 
    string PhoneNumber, 
    string Email, 
    string BankAccountNumber
    );