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
}