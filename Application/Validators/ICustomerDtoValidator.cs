using FluentValidation;
using Shared.DTO;

namespace Application.Validators
{
    public class ICustomerDtoValidator : AbstractValidator<ICustomerDto>
    {
        public ICustomerDtoValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.CountryCode)
                .MaximumLength(4).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            RuleFor(p => p.PhoneNumber)
                .MaximumLength(20).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
                
            RuleFor(p => p.BankAccountNumber)
                .MaximumLength(40).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}