using Microsoft.AspNetCore.Mvc;
using Stays.Api.Models;
using Stays.Api.Services.Authentication;

namespace Stays.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserAuthenticationService _authenticationService;

    public AuthenticationController(IUserAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("validate")]
    public async Task<IActionResult> ValidatePassword([FromBody] AuthenticateUserRequest request)
    {
        if (request is null)
        {
            return BadRequest();
        }

        var result = await _authenticationService.AuthenticateAsync(request.Email, request.Password);

        if (!result.IsAuthenticated)
        {
            return Unauthorized(new AuthenticateUserResponse(false, null, null, result.ErrorMessage));
        }

        return Ok(new AuthenticateUserResponse(true, result.UserId, result.DisplayName));
    }
}
