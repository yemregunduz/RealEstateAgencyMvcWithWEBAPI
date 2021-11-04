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
    public class RealEstateImageValidator:AbstractValidator<RealEstateImage>
    {
        public RealEstateImageValidator()
        {
            RuleFor(r => r.RealEstateId)
                .NotNull().WithMessage(Messages.RealEstateIdIsRequired)
                .GreaterThan(0).WithMessage(Messages.RealEstateIdIsRequired);
        }
    }
}
