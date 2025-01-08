using Family.Domain.Entities.Base;
using Family.Domain.Entities.Enums;
using Family.Domain.Entities.Exceptions;

namespace Family.Domain.Entities;

public class FamilyMember(long id) : Entity(id), IRoleBearer
{
    public required string Name { get; init; }
    
    public Role Role { get; private set; }

    public bool IsPermissible(Permissions permission) => ((IRoleBearer)this).IsPermissible(permission);
    
    public Family Family { get; private set; }

    public FamilyMember(long id, string name, Family family, Role role) : this(id)
    {
        Family = family;
        Role = role;
        Name = name;
        
        if(!family.FamilyMembers.Contains(this))
            family.AddFamilyMember(this);
    }

    /// <summary>
    /// Include FamilyMember to a Family
    /// </summary>
    /// <param name="familyMember">Member to Include</param>
    /// <exception cref="NoPermissionToInviteMembersException">If this member has not allowed to include members</exception>
    public void IncludeToFamily(FamilyMember familyMember)
    {
        if (IsPermissible(Permissions.InviteToFamily))
            Family.AddFamilyMember(familyMember);
        else
            throw new NoPermissionToInviteMembersException(Family, this, familyMember);
    }

    /// <summary>
    /// Remove FamilyMember from a Family
    /// </summary>
    /// <param name="familyMember">Member to remove</param>
    /// <exception cref="NoPermissionToExcludeMembersException">If this member has not allowed to exclude members</exception>
    public void ExcludeFromFamily(FamilyMember familyMember)
    {
        if (IsPermissible(Permissions.ExcludeFromFamily))
            Family.RemoveFamilyMember(familyMember);
        else
            throw new NoPermissionToExcludeMembersException(Family, this, familyMember);
    }

    /// <summary>
    /// Change Role to a family member
    /// </summary>
    /// <param name="member">Family member</param>
    /// <param name="role">Role to assign</param>
    /// <exception cref="NoPermissionToChangeMemberRoleException">If this member has not allowed to change roles</exception>
    public void AssignRoleToMember(FamilyMember member, Role role)
    {
        if (IsPermissible(Permissions.ChangeRole))
            member.Role = role;
        else
            throw new NoPermissionToChangeMemberRoleException(this);
    }
}