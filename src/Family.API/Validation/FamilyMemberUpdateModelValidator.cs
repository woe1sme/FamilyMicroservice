using Family.Application.Models.FamilyMember;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.API.Validation
{
    public class FamilyMemberUpdateModelValidator : AbstractValidator<FamilyMemberUpdateModel>
    {
        public FamilyMemberUpdateModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                                .WithMessage("Specify name.")
                                .Length(1, 100)
                                .WithMessage("Not greater than 100");
        }
    }
}
