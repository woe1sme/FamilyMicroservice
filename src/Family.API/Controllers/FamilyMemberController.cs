using Family.Application.Abstractions;
using Family.Application.Models.FamilyMember;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Family.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamilyMemberController : Controller
    {
        private readonly IFamilyMemberService _familyMemberService;
        private readonly ILogger<FamilyMemberController> _logger;
        private readonly IValidator<FamilyMemberCreateModel> _familyMemberCreateModelValidator;

        public FamilyMemberController(IFamilyMemberService familyMemberService, ILogger<FamilyMemberController> logger, IValidator<FamilyMemberCreateModel> familyMemberCreateModelValidator) 
        {
            _familyMemberService = familyMemberService;
            _logger = logger;
            _familyMemberCreateModelValidator = familyMemberCreateModelValidator;
        }

        // POST api/familyMember/{familyId}
        [HttpPost("{familyId:guid}")]
        public async Task<ActionResult<FamilyMemberModel>> CreateFamilyMember(Guid familyId, [FromBody] FamilyMemberCreateModel familyMemberCreateModel)
        {
            try
            {
                var validateResult = _familyMemberCreateModelValidator.Validate(familyMemberCreateModel);
                if (!validateResult.IsValid) 
                {
                    _logger.LogError(message: $"{validateResult}");
                    return StatusCode(StatusCodes.Status400BadRequest, $"An error occurred while processing your request. {validateResult}");
                }

                var result = await _familyMemberService.CreateMemberAsync(familyMemberCreateModel, familyId);
                return CreatedAtAction(nameof(CreateFamilyMember),new {id = result.Id}, result);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error creating family");
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        // GET api/familyMember
        [HttpGet]
        public ActionResult<IEnumerable<FamilyMemberModel>> GetFamilyMember()
        {
            try
            {
                var result = _familyMemberService.GetAllFamilyMembers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting family members");
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }
    }
}
