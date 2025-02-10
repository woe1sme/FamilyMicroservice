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
        // Получаем информацию о пользователе из HttpContext
        if (context.HttpContext.Items["UserInfo"] is not UserInfo userInfo)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Получаем необходимые сервисы из контейнера зависимостей
        var familyMemberService = context.HttpContext.RequestServices.GetService<IFamilyMemberService>();

        // Допустим, у вас в маршруте передается familyId
        if (!context.ActionArguments.TryGetValue("familyId", out var familyIdValue) || familyIdValue is not Guid familyId)
        {
            context.Result = new BadRequestObjectResult("FamilyId is required.");
            return;
        }

        // Получаем информацию о члене семьи
        var familyMember = await familyMemberService.GetFamilyMemberByUserIdAsync(userInfo.Id, familyId);
        if (familyMember is null)
        {
            context.Result = new ForbidResult();
            return;
        }

        // Проверяем, соответствует ли роль пользователя требуемой роли
        if (Enum.Parse<Role>(familyMember.Role) != _requiredRole)
        {
            context.Result = new ForbidResult();
            return;
        }

        // Продолжаем выполнение действия
        await next();
    }
}