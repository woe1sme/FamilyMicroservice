using System.Collections.Generic;
using Family.Domain.Entities.Enums;

namespace Family.Domain.Entities;

public static class RolePermissions
{
    public static readonly Dictionary<Role, List<Permissions>> RolesPermissions =
        new()
        {
            {
                Role.Head, 
                [
                    Permissions.InviteToFamily,
                    Permissions.ExcludeFromFamily,
                    Permissions.ChangeRole
                ]
            },
            {
                Role.Adult,
                [
                    Permissions.InviteToFamily
                ]
            },
            {
                Role.Child, 
                []
            }
        };
}