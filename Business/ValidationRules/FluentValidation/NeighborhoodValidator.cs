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
    public class NeighborhoodValidator:AbstractValidator<Neighborhood>
    {
        public NeighborhoodValidator()
        {
            RuleFor(n => n.NeighborhoodName)
                .NotEmpty().WithMessage(Messages.NeighborhoodNameIsRequired)
                .MaximumLength(100).WithMessage(Messages.NeighborhoodIsNotValid);
            RuleFor(n => n.DistrictId)
                .NotNull().WithMessage(Messages.DistrictNameIsRequired);
        }
    }
}
