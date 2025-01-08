using Family.Domain.Entities.Enums;

namespace Family.Domain.Entities.Base;

public interface IRoleBearer
{
    public Role Role { get; }
    public bool IsPermissible(Permissions permission) => GetPermissions().Contains(permission);
    private List<Permissions> GetPermissions() => RolePermissions.RolesPermissions[Role];
}