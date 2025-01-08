namespace Family.Domain.Entities.Exceptions;

public class NoPermissionToInviteMembersException(Family family, FamilyMember inviter, FamilyMember invited) : 
    InvalidOperationException($"{inviter.Name} has no permission to invite {invited.Name}  to {family.FamilyName} family.")
{
    public FamilyMember Invited => invited;
    public FamilyMember Inviter => inviter;
    public Family Family => family;
}