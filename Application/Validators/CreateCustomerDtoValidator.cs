using FluentValidation;
using Shared.DTO;

namespace Application.Validators
{
    public class CreateCustomerDtoValidator : AbstractValidator<CustomerForCreationDto>
    {
        public CreateCustomerDtoValidator()
        {
            Include(new ICustomerDtoValidator());
        }
    }
}
