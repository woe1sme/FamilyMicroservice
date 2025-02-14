using Family.Application.Models.Family;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.Application.Validation
{
    public class FamilyUpdateModelValidator : AbstractValidator<FamilyUpdateModel>
    {
        public FamilyUpdateModelValidator()
        {
            RuleFor(x => x.FamilyName).NotEmpty()
                                      .WithMessage("Specify family Name.")
                                      .Length(1, 100)
                                      .WithMessage("Not greater than 100");
        } 
    }
}
