using Business.Constants;
using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.CompanyName)
                .NotEmpty().WithMessage(Messages.CompanyNameIsRequired)
                .MaximumLength(150).WithMessage(Messages.CompanyNameIsNotValid);
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage(Messages.EmailIsRequired)
                .EmailAddress().WithMessage(Messages.EmailIsNotValid);
        }
        
    }
}
