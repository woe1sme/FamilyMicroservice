using Family.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Family.Domain.Entities;
using Family.Domain.Entities.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Family.Application.Attributes;


public class AuthorizeFamilyMemberAttribute(Role requiredRole) : Attribute, IAsyncActionFilter
{
    private readonly Role _requiredRole = requiredRole;
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.HttpContext.Items["UserInfo"] is not UserInfo userInfo)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var familyMemberService = context.HttpContext.RequestServices.GetService<IFamilyMemberService>();

        if (!context.ActionArguments.TryGetValue("familyId", out var familyIdValue) || familyIdValue is not Guid familyId)
        {
            context.Result = new BadRequestObjectResult("FamilyId is required.");
            return;
        }

        var familyMember = await familyMemberService.GetFamilyMemberByUserIdAsync(userInfo.Id, familyId);
        if (familyMember is null)
        {
            context.Result = new ForbidResult();
            return;
        }

        if (Enum.Parse<Role>(familyMember.Role) < _requiredRole)
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }
}