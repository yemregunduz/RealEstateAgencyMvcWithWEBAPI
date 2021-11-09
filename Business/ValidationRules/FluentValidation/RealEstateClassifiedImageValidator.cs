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
    public class RealEstateClassifiedImageValidator:AbstractValidator<RealEstateClassifiedImage>
    {
        public RealEstateClassifiedImageValidator()
        {
            RuleFor(r => r.RealEstateClassifiedId)
                .NotNull().WithMessage(Messages.RealEstateIdIsRequired)
                .GreaterThan(0).WithMessage(Messages.RealEstateIdIsRequired);
        }
    }
}
