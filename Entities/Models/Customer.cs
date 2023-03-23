using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models;

[Index(nameof(FirstName), nameof(LastName), nameof(DateOfBirth), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class Customer 
{
     [Column("CustomerId")]
     public Guid Id { get; set; } 
     [Required(ErrorMessage = "Customer first name is a required field.")] 
     [MaxLength(50, ErrorMessage = "Maximum length for the first name is 50 characters.")] 
     public string? FirstName { get; set; } = null!;
     [Required(ErrorMessage = "Customer last name is a required field.")] 
     [MaxLength(50, ErrorMessage = "Maximum length for the last name is 50 characters.")] 
     public string LastName { get; set; } = null!;
     [Required(ErrorMessage = "Customer date of birth is a required field.")] 
     public DateOnly? DateOfBirth { get; set; } = null!;
     [Column(TypeName = "varchar(20)")]
     [MaxLength(20, ErrorMessage = "Maximum length for the PhoneNumber is 20 characters.")] 
     public string? PhoneNumber { get; set; } 
     [Required(ErrorMessage = "Customer email is a required field.")] 
     public string Email { get; set; } = null!;
     [MaxLength(20, ErrorMessage = "Maximum length for the BankAccountNumber is 20 characters.")] 
     [Column(TypeName = "varchar(20)")]
     public string? BankAccountNumber { get; set; } 
} 