using System.Net;
using Business.Services.Interfaces;
using Core.DTOs;
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
    public async Task<ActionResult<ResponseDto<RegisterUserResponseDto>>> Register(
        RegisterUserRequestDto registerUserRequestDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var responseDto = await _userService.Register(registerUserRequestDto);

        switch (responseDto.HttpCode)
        {
            case HttpStatusCode.Conflict:
                return Conflict(responseDto);
            case HttpStatusCode.InternalServerError:
                return StatusCode(StatusCodes.Status500InternalServerError, responseDto);
            default:
                return Ok(responseDto);
        }
    }

    [HttpPost("login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ResponseDto<LoginUserResponseDto>>> Login(
        LoginUserRequestDto loginUserRequestDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var responseDto = await _userService.Login(loginUserRequestDto);

        switch (responseDto.HttpCode)
        {
            case HttpStatusCode.Unauthorized:
                return Unauthorized(responseDto);
            case HttpStatusCode.NotFound:
                return NotFound(responseDto);
            case HttpStatusCode.InternalServerError:
                return StatusCode(StatusCodes.Status500InternalServerError, responseDto);
            default:
                return Ok(responseDto);
        }
    }
}