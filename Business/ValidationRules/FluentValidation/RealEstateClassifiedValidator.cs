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
    public class RealEstateClassifiedValidator : AbstractValidator<RealEstateClassified>
    {
        public RealEstateClassifiedValidator()
        {
            RuleFor(r => r.BuildingAge)
                .NotNull().WithMessage(Messages.BuildingAgeIsRequired)
                .GreaterThanOrEqualTo(0).WithMessage(Messages.BuildingAgeIsNotValid);
            RuleFor(r => r.FullAddress)
                .NotEmpty().WithMessage(Messages.FullAddressIsRequired)
                .MaximumLength(250).WithMessage(Messages.FullAddressIsNotValid);
            RuleFor(r => r.Floor)
                .NotNull().WithMessage(Messages.FloorIsRequired)
                .GreaterThan(-2).WithMessage(Messages.FloorIsNotValid);
            RuleFor(r => r.SquareMeters)
                .NotNull().WithMessage(Messages.SquareMetersIsRequired);
            RuleFor(r => r.RealEstatePrice)
                .NotNull().WithMessage(Messages.RealEstatePriceIsRequired)
                .GreaterThan(0).WithMessage(Messages.RealEstatePriceIsNotValid);
            RuleFor(r => r.NeighborhoodId)
                .NotNull().WithMessage(Messages.NeighborhoodNameIsRequired)
                .GreaterThan(0).WithMessage(Messages.NeighborhoodIsNotValid);
        }
    }
}
