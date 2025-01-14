using System;

namespace Family.Domain.Entities.Exceptions;

public class NoPermissionToChangeMemberRoleException(FamilyMember member) : 
    InvalidOperationException($"{member.Name} has no permission to change roles.")
{
    public FamilyMember Member => member;
}