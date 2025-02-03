namespace Family.Application.Exceptions;

public class FamilyMemberNotFoundException(Guid memberId) : 
    Exception($"No family member with id: {memberId} was found")
{
    
}