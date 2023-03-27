using FluentValidation;
using Shared.DTO;

namespace Application.Validators
{
    public class UpdateCustomerDtoValidator : AbstractValidator<CustomerForUpdateDto>
    {
        public UpdateCustomerDtoValidator()
        {
            Include(new ICustomerDtoValidator());

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
