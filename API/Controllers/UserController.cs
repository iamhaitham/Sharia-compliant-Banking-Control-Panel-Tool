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
    public async Task<ActionResult<RegisterUserResponseDto>> Register(RegisterUserRequestDto registerUserRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var registerUserResponseDto = await _userService.Register(registerUserRequestDto);

        if (registerUserResponseDto is null)
        {
            return Conflict(CustomErrorMessage.DuplicatedEntry());
        }
        
        return Ok(registerUserResponseDto);
    }

    [HttpPost("login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<ActionResult<ResponseDto<LoginUserResponseDto>>> Login(LoginUserRequestDto loginUserRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDto<LoginUserResponseDto>()
            {
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList(),
                IsSuccessful = false
            });
        }
        
        var responseDto = await _userService.Login(loginUserRequestDto);

        if (!responseDto.IsSuccessful)
        {
            return Unauthorized(responseDto);
        }

        return Ok(responseDto);
    }
}