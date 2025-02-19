using Family.Application.Abstractions;
using Family.Application.Models.Family;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Family.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyService _familyService;
        private readonly ILogger _logger;
        private readonly IValidator<FamilyAndFamilyHeadCreateModel> _familyAndFamilyHeadCreateModelValidator;
        private readonly IValidator<FamilyUpdateModel> _familyUpdateModelValidator;

        public FamilyController(IFamilyService familyService,
                                ILogger<FamilyController> logger,
                                IValidator<FamilyAndFamilyHeadCreateModel> familyAndFamilyHeadCreateModelValidator,
                                IValidator<FamilyUpdateModel> familyUpdateModelValidator)
        {
            _familyService = familyService;
            _logger = logger;
            _familyAndFamilyHeadCreateModelValidator = familyAndFamilyHeadCreateModelValidator;
            _familyUpdateModelValidator = familyUpdateModelValidator;
        }

        // POST /api/families
        [HttpPost]
        public async Task<IActionResult> CreateFamily([FromBody] FamilyAndFamilyHeadCreateModel familyAndFamilyHeadCreateModel)
        {
            try
            {
                var validateResult = _familyAndFamilyHeadCreateModelValidator.Validate(familyAndFamilyHeadCreateModel);
                if (!validateResult.IsValid)
                {
                    _logger.LogError(message: $"{validateResult}");
                    return StatusCode(StatusCodes.Status400BadRequest, $"An error occurred while processing your request. {validateResult}");
                }

                var family = await _familyService.CreateFamilyAsync(familyAndFamilyHeadCreateModel);
                return CreatedAtAction(nameof(CreateFamily), new { id = family.Id }, family);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating family");
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        // PATCH api/family/{familyId}
        [HttpPatch("{familyId:guid}")]
        public async Task<IActionResult> UpdateFamily(Guid familyId, [FromBody] FamilyUpdateModel familyUpdateModel)
        {
            try
            {
                var validateResult = _familyUpdateModelValidator.Validate(familyUpdateModel);
                if (!validateResult.IsValid)
                {
                    _logger.LogError(message: $"{validateResult}");
                    return StatusCode(StatusCodes.Status400BadRequest, $"An error occurred while processing your request. {validateResult}");
                }

                await _familyService.UpdateFamilyAsync(familyId, familyUpdateModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating family {familyId}", familyId);
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        // GET api/family/{familyId}
        [HttpGet("{familyId:guid}")]
        public async Task<IActionResult> GetFamilyById(Guid familyId)
        {
            try
            {
                var family = await _familyService.GetFamilyByIdAsync(familyId);
                return family == null ? NotFound() : Ok(family);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting family with ID {FamilyId}", familyId);
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        // GET /api/users/{userId}/families
        [HttpGet("~/api/users/{userId:guid}/families")]
        public IActionResult GetFamiliesByUserId(Guid userId)
        {
            try
            {
                var userFamilies = _familyService.GetFamilyByUserId(userId);

                return !userFamilies.Any() ? NotFound() : Ok(userFamilies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting family by UserId {userId}", userId);
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        // GET api/family
        [HttpGet]
        public IActionResult GetAllFamilies()
        {
            try
            {
                return Ok(_familyService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting families");
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }  
    }
}
