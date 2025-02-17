using Family.Application.Abstractions;
using Family.Application.Models.Family;
using Family.Application.Models.UserInfo;
using Family.Domain.Entities;
using Family.Domain.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Family.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyService _familyService;
        private readonly ILogger _logger;

        public FamilyController(IFamilyService familyService, ILogger<FamilyController> logger)
        {
            _familyService = familyService;
            _logger = logger;
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
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
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
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
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
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // POST /api/families
        [HttpPost]
        public async Task<IActionResult> CreateFamily([FromBody] FamilyCreateModel familyCreateModel)
        {
            try
            {
                var family = await _familyService.CreateFamilyAsync(familyCreateModel);
                return CreatedAtAction(nameof(CreateFamily), new { id = family.Id }, family);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating family");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPatch("{familyId:guid}")]
        public async Task<IActionResult> UpdateFamily(Guid familyId, [FromBody] FamilyUpdateModel familyUpdateModel)
        {
            try
            {
                await _familyService.UpdateFamilyAsync(familyId, familyUpdateModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating family {familyId}", familyId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        //временный метод получения токена тестового пользователя
        //[HttpGet("~/api/token")]
        //public IActionResult GetToken()
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, "b5bb7427-5922-4262-9cd4-0758d87bc1d2"),
        //        new Claim(ClaimTypes.Name, "TestUserName")
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aUYNC5NmUzAXKvAGREGbiNkjPG7p3QbT"));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer: "family-issuer",
        //        audience: "family-audience",
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddHours(24),
        //        signingCredentials: creds
        //    );

        //    return Ok( new JwtSecurityTokenHandler().WriteToken(token));
        //}
    }
}
