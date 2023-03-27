using FluentValidation;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Customers.Validators
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
