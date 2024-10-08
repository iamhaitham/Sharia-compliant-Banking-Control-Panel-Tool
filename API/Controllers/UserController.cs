using System.Net;
using Business.Services.Interfaces;
using Core.DTOs.User;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GenericResponse<RegisterUserResponseDto>>> Register(
        RegisterUserRequestDto registerUserRequestDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var genericResponse = await _userService.Register(registerUserRequestDto);

        switch (genericResponse.HttpCode)
        {
            case HttpStatusCode.Conflict:
                return Conflict(genericResponse);
            case HttpStatusCode.InternalServerError:
                return StatusCode(StatusCodes.Status500InternalServerError, genericResponse);
            default:
                return Ok(genericResponse);
        }
    }

    [HttpPost("login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GenericResponse<LoginUserResponseDto>>> Login(
        LoginUserRequestDto loginUserRequestDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var genericResponse = await _userService.Login(loginUserRequestDto);

        switch (genericResponse.HttpCode)
        {
            case HttpStatusCode.Unauthorized:
                return Unauthorized(genericResponse);
            case HttpStatusCode.NotFound:
                return NotFound(genericResponse);
            case HttpStatusCode.InternalServerError:
                return StatusCode(StatusCodes.Status500InternalServerError, genericResponse);
            default:
                return Ok(genericResponse);
        }
    }
}