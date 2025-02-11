namespace Family.Application.Models.Base;

public interface IUpdateModel : IModel
{
    public string FamilyName { get; init; }
}