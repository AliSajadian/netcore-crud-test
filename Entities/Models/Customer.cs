using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models;

[Index(nameof(FirstName), nameof(LastName), nameof(DateOfBirth), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class Customer 
{
     [Column("CustomerId")]
     public int Id { get; set; } 
     [Required(ErrorMessage = "Customer first name is a required field.")] 
     [MaxLength(50, ErrorMessage = "Maximum length for the first name is 50 characters.")] 
     public string? FirstName { get; set; } = null!;
     [Required(ErrorMessage = "Customer last name is a required field.")] 
     [MaxLength(50, ErrorMessage = "Maximum length for the last name is 50 characters.")] 
     public string LastName { get; set; } = null!;
     [Required(ErrorMessage = "Customer date of birth is a required field.")] 
     public DateTime? DateOfBirth { get; set; } = null!;
     [Column(TypeName = "varchar(4)")]
     [MaxLength(4, ErrorMessage = "Maximum length for the country code is 4 characters.")] 
     public string? CountryCode { get; set; }
     [Column(TypeName = "varchar(20)")]
     [MaxLength(20, ErrorMessage = "Maximum length for the phone number is 20 characters.")] 
     public string? PhoneNumber { get; set; } 
     [Required(ErrorMessage = "Customer email is a required field.")] 
     [MaxLength(100, ErrorMessage = "Maximum length for the email is 100 characters.")] 
     public string Email { get; set; } = null!;
     [Column(TypeName = "varchar(40)")]
     [MaxLength(40, ErrorMessage = "Maximum length for the bank account number is 40 characters.")] 
     public string? BankAccountNumber { get; set; } 
} 
