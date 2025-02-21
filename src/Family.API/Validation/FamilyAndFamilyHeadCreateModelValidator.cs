using Family.Application.Models.Family;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.API.Validation
{
    public class FamilyAndFamilyHeadCreateModelValidator : AbstractValidator<FamilyAndFamilyHeadCreateModel>
    {
        public FamilyAndFamilyHeadCreateModelValidator()
        {
            RuleFor(x => x.FamilyName).NotEmpty()
                                      .WithMessage("Specify family Name.")
                                      .Length(1, 100)
                                      .WithMessage("Not greater than 100");
            RuleFor(x => x.FamilyHeadUserName).NotEmpty()
                                      .WithMessage("Specify family head Name.")
                                      .Length(1, 100)
                                      .WithMessage("Not greater than 100");
            RuleFor(x => x.FamilyHeadUserId).NotEmpty();
        }
    }
}
