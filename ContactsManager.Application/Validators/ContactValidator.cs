using ContactsManager.Application.DTOs;
using ContactsManager.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Application.Validators
{
    public class ContactValidator : AbstractValidator<ContactDTO>
    {
        public ContactValidator()
        {
            RuleFor(x => x.FirstName).
                            NotEmpty().
                            Length(1, 50);

            RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^[6-9]\d{9}$");
            RuleFor(x => x.Email).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.LastName).NotEmpty().Length(1, 50);
            RuleFor(x => x.Status).NotEmpty().Must(x => x.Equals("Active") || x.Equals("Inactive"));
        }
    }
}
