namespace Family.Domain.Entities.Exceptions;

public class NoPermissionToExcludeMembersException(Family family, FamilyMember excluder, FamilyMember excluded) : 
    InvalidOperationException($"{excluder.Name} has no permission to exclude {excluded.Name} from the {family.FamilyName} family.")
{
    public FamilyMember Excluded => excluded;
    public FamilyMember Excluder => excluder;
    public Family Family => family;
}