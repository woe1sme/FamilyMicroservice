using Family.Application.Abstractions;
using Family.Application.Models.FamilyMember;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Family.API.Controllers
{
    [ApiController]
    [Route("api/families/{familyId:guid}/members")]
    public class FamilyMembersController : Controller
    {
        private readonly IFamilyMemberService _familyMemberService;
        private readonly ILogger<FamilyMembersController> _logger;
        private readonly IValidator<FamilyMemberCreateModel> _familyMemberCreateModelValidator;
        private readonly IValidator<FamilyMemberUpdateModel> _familyMemberUpdateModelValidator;

        public FamilyMembersController(IFamilyMemberService familyMemberService,
                                      ILogger<FamilyMembersController> logger,
                                      IValidator<FamilyMemberCreateModel> familyMemberCreateModelValidator,
                                      IValidator<FamilyMemberUpdateModel> familyMemberUpdateModelValidator) 
        {
            _familyMemberService = familyMemberService;
            _logger = logger;
            _familyMemberCreateModelValidator = familyMemberCreateModelValidator;
            _familyMemberUpdateModelValidator = familyMemberUpdateModelValidator;
        }

        // POST api/families/{familyId}/members
        /// <summary>
        /// Создаёт нового члена семьи.
        /// </summary>
        /// <param name="familyId">Идентификатор семьи.</param>
        /// <param name="familyMemberCreateModel">Данные для создания члена семьи.</param>
        /// <returns>Созданный член семьи.</returns>
        /// <response code="201">Член семьи успешно создан.</response>
        /// <response code="400">Неверные данные запроса.</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FamilyMemberModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                _logger.LogError(ex, "Error creating family member.");
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        // PATCH api/families/{familyId}/members/{familyMemberId}
        /// <summary>
        /// Обновляет данные члена семьи.
        /// </summary>
        /// <param name="familyMemberId">Идентификатор члена семьи.</param>
        /// <param name="familyMemberUpdateModel">Данные для обновления.</param>
        /// <response code="200">Обновление прошло успешно.</response>
        /// <response code="404">Ресурс не найден</response>
        /// <response code="400">Неверные данные запроса.</response>
        [HttpPatch("{familyMemberId:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FamilyMemberModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FamilyMemberModel>> UpdateFamilyMember(Guid familyMemberId, [FromBody] FamilyMemberUpdateModel familyMemberUpdateModel)
        {
            try
            {
                var validateResult = _familyMemberUpdateModelValidator.Validate(familyMemberUpdateModel);
                if (!validateResult.IsValid)
                {
                    _logger.LogError(message: $"{validateResult}");
                    return StatusCode(StatusCodes.Status400BadRequest, $"An error occurred while processing your request. {validateResult}");
                }

                var result = await _familyMemberService.UpdateMemberAsync(familyMemberUpdateModel, familyMemberId);
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error updating family member.");
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        // GET api/families/{familyId}/members
        /// <summary>
        /// Получает список всех членов семьи.
        /// </summary>
        /// <param name="familyId">Идентификатор семьи.</param>
        /// <returns>Список членов семьи.</returns>
        /// <response code="200">Список получен успешно.</response>
        /// <response code="404">Ресурс не найден</response>
        /// <response code="400">Неверные данные запроса.</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<FamilyMemberModel>>> GetFamilyMembersByFamilyId(Guid familyId) 
        {
            try
            {
                var result = await _familyMemberService.GetAllMembersByFamilyIdAsync(familyId);
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error updating family member.");
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        // GET api/families/{familyId}/members/{familyMemberId}
        /// <summary>
        /// Получает информацию о члене семьи по идентификатору.
        /// </summary>
        /// <param name="familyMemberId">Идентификатор члена семьи.</param>
        /// <returns>Информация о члене семьи.</returns>
        /// <response code="200">Информация получена успешно.</response>
        /// <response code="404">Ресурс не найден</response>
        /// <response code="400">Неверные данные запроса.</response>
        [HttpGet("{familyMemberId:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FamilyMemberModel>> GetFamilyMemberById(Guid familyMemberId) 
        {
            try
            {
                var result = await _familyMemberService.GetMemberByIdAsync(familyMemberId);
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error updating family member.");
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        // GET api/members/by-user/{userId}
        /// <summary>
        /// Получает список членов семей по идентификатору пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Информация о членах семей.</returns>
        /// <response code="200">Информация получена успешно.</response>
        /// <response code="404">Ресурс не найден</response>
        /// <response code="400">Неверные данные запроса.</response>
        [HttpGet("~/api/members/by-user/{userId:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<FamilyMemberModel>> GetAllFamilyMembersByUserId(Guid userId) 
        {
            try 
            {
                var result = _familyMemberService.GetFamilyMemberByUserInfo(userId);
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating family member.");
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        // GET api/families/{familyId}/members/by-user/{userId}
        /// <summary>
        /// Получает члена семьи по идентификатору пользователя.
        /// </summary>
        /// <param name="familyId">Идентификатор семьи.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Информация о члене семьи.</returns>
        /// <response code="200">Информация получена успешно.</response>
        /// <response code="404">Ресурс не найден</response>
        /// <response code="400">Неверные данные запроса.</response>
        [HttpGet("by-user/{userId:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<IEnumerable<FamilyMemberModel>> GetFamilyMemberByUserId(Guid familyId, Guid userId) 
        {
            try 
            {
                var result = _familyMemberService.GetFamilyMemberByUserIdAsync(userId, familyId);
                return result == null ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating family member.");
                return StatusCode(StatusCodes.Status400BadRequest, "An error occurred while processing your request.");
            }
        }

        // GET api/members
        [HttpGet("~/api/members")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
