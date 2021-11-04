using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class DistrictValidator:AbstractValidator<District>
    {
        public DistrictValidator()
        {
            RuleFor(d => d.DistrictName)
                .MaximumLength(100).WithMessage(Messages.DistrictNameIsNotValid)
                .NotEmpty().WithMessage(Messages.DistrictNameIsRequired);
            RuleFor(d => d.CityId)
                .NotNull().WithMessage(Messages.CityNameIsRequired);
        }
    }
}
