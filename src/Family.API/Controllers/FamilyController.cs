using Family.Application.Abstractions;
using Family.Application.Attributes;
using Family.Application.Models.Family;
using Family.Application.Models.UserInfo;
using Family.Domain.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Family.API.Controllers
{
    [ApiController]
    public class FamilyController(IFamilyService familyService, ILogger logger) : Controller
    {
        [HttpGet("{familyId:guid}")]
        public async Task<IActionResult> GetFamily(Guid familyId)
        {
            var family = await familyService.GetFamilyByIdAsync(familyId);

            return family == null ? NotFound() : Ok(family);
        }

        [HttpGet]
        [AuthorizeFamilyMember(Role.Child)]
        public IActionResult GetFamily([FromBody] UserInfoModel userInfoModel) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userFamilies = familyService.GetFamilyByUserInfo(userInfoModel);

            return !userFamilies.Any() ? NotFound() : Ok(userFamilies);
        }

        [HttpGet]
        public IActionResult GetFamily() 
        {
            return Ok(familyService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> CreateFamily([FromBody] FamilyCreateModel familyCreateModel, [FromBody] UserInfoModel userInfoModel) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var family = await familyService.CreateFamilyAsync(familyCreateModel, userInfoModel);
            return CreatedAtAction(nameof(CreateFamily), new { id = family.Id }, family);
        }

        [HttpPatch]
        [AuthorizeFamilyMember(Role.Head)]
        public async Task<IActionResult> UpdateFamily([FromBody] FamilyUpdateModel familyUpdateModel) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await familyService.UpdateFamilyAsync(familyUpdateModel);
            return NoContent();
        }

    }
}
