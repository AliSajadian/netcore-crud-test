using FluentValidation;
using Shared.DTO;

namespace Application.Customers.Validators
{
    public class CreateCustomerDtoValidator : AbstractValidator<CustomerForCreationDto>
    {
        public CreateCustomerDtoValidator()
        {
            Include(new ICustomerDtoValidator());
        }
    }
}
