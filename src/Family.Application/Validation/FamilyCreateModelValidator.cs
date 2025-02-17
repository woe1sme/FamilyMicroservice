using Family.Application.Models.Family;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.Application.Validation
{
    public class FamilyCreateModelValidator : AbstractValidator<FamilyCreateModel>
    {
        public FamilyCreateModelValidator()
        {
            RuleFor(x => x.FamilyName)
                .NotEmpty()
                .WithMessage("Specify family Name.")
                .Length(1, 100)
                .WithMessage("Not greater than 100");

            RuleFor(x => x.CreatorUserName)
                .NotEmpty()
                .WithMessage("Specify family creator UserName.")
                .Length(1, 100)
                .WithMessage("Not greater than 100");

            RuleFor(x => x.CreatorUserId)
                .NotEmpty()
                .WithMessage("Specify family creator UserId.");
        }
    }
}
