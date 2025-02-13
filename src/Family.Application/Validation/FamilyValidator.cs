using Family.Application.Models.Family;
using FluentValidation;

namespace Family.Application.Validation;

public class FamilyValidator<T> : AbstractValidator<T> where T : FamilyModelBase
{
    public FamilyValidator() 
    {
        RuleFor(x => x.FamilyName).NotEmpty()
                                      .WithMessage("Specify family Name.")
                                      .Length(1, 100)
                                      .WithMessage("Not greater than 100");
    }
}