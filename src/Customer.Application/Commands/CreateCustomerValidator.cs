using Customer.Domain.Enums;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Application.Commands
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Fullname is required")
            .MaximumLength(200).WithMessage("Fullname must not exceed 200 characters");

            RuleFor(x => x.Email)
             .NotEmpty().WithMessage("Email is required")
             .EmailAddress().WithMessage("El email doesn't have valid format")
             .MaximumLength(200).WithMessage("Email must not exceed 200 characters");

            RuleFor(x => x.EmploymentType)
                .IsInEnum().WithMessage("EmploymentType is required or invalid");

        }
    }
}
