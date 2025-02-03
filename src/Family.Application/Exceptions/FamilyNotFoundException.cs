namespace Family.Application.Exceptions;

public class FamilyNotFoundException(Guid familyId) : 
    Exception($"No family with id: {familyId} was found")
{
}