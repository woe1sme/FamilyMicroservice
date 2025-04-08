using Family.Application.Abstractions;
using Family.Application.Models.Family;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Family.API.Controllers
{
    [ApiController]
    [Route("api/families")]
    public class FamiliesController : ControllerBase
    {
        private readonly IFamilyService _familyService;
        private readonly ILogger _logger;
        private readonly IValidator<FamilyAndFamilyHeadCreateModel> _familyAndFamilyHeadCreateModelValidator;
        private readonly IValidator<FamilyUpdateModel> _familyUpdateModelValidator;

        public FamiliesController(IFamilyService familyService,
                                ILogger<FamiliesController> logger,
                                IValidator<FamilyAndFamilyHeadCreateModel> familyAndFamilyHeadCreateModelValidator,
                                IValidator<FamilyUpdateModel> familyUpdateModelValidator)
        {
            _familyService = familyService;
            _logger = logger;
            _familyAndFamilyHeadCreateModelValidator = familyAndFamilyHeadCreateModelValidator;
            _familyUpdateModelValidator = familyUpdateModelValidator;
        }

        // POST /api/families
        /// <summary>
        /// Создает новую семью вместе с главой семьи.
        /// </summary>
        /// <param name="model">Модель, содержащая данные семьи и главы семьи.</param>
        /// <returns>Созданная семья.</returns>
        /// <response code="201">Семья успешно создана.</response>
        /// <response code="400">Неверные входные данные.</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FamilyModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FamilyModel>> CreateFamily([FromBody] FamilyAndFamilyHeadCreateModel model)
        {
            try
            {
                var validateResult = _familyAndFamilyHeadCreateModelValidator.Validate(model);
                if (!validateResult.IsValid)
                {
                    _logger.LogError(message: $"{validateResult}");
                    return StatusCode(StatusCodes.Status400BadRequest, $"An error occurred while processing your request. {validateResult}");
                }

                var family = await _familyService.CreateFamilyAsync(model);

                return CreatedAtAction(nameof(CreateFamily), new { id = family.Id }, family);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating family");
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        // PATCH /api/families/{familyId}
        /// <summary>
        /// Обновляет данные существующей семьи.
        /// </summary>
        /// <param name="familyId">Идентификатор семьи для обновления.</param>
        /// <param name="familyUpdateModel">Модель с информацией для обновления.</param>
        /// <response code="204">Семья успешно обновлена.</response>
        /// <response code="400">Неверные входные данные.</response>
        /// <response code="404">Семья не найдена.</response>
        [HttpPatch("{familyId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        // GET /api/families/{familyId}
        /// <summary>
        /// Получает информацию о семье по ее идентификатору.
        /// </summary>
        /// <param name="familyId">Идентификатор семьи.</param>
        /// <returns>Запрашиваемая семья.</returns>
        /// <response code="200">Семья найдена.</response>
        /// <response code="404">Семья не найдена.</response>
        [HttpGet("{familyId:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FamilyModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FamilyModel>> GetFamilyById(Guid familyId)
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

        // GET /api/families/user/{userId}
        /// <summary>
        /// Получает семьи, связанные с пользователем.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список семей.</returns>
        /// <response code="200">Семьи найдены.</response>
        /// <response code="404">Семьи для данного пользователя не найдены.</response>
        [HttpGet("user/{userId:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FamilyModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<FamilyModel>> GetFamiliesByUserId(Guid userId)
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

        // GET /api/families
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FamilyModel>))]
        public ActionResult<IEnumerable<FamilyModel>> GetAllFamilies()
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
