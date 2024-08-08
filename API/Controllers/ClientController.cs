using System.Net;
using Business.Services.Interfaces;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost("/register")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<ResponseDto<RegisterClientResponseDto>>> Register(
        RegisterClientRequestDto registerClientRequestDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var responseDto = await _clientService.Register(registerClientRequestDto);

        switch (responseDto.HttpCode)
        {
            case HttpStatusCode.Forbidden:
                return StatusCode(StatusCodes.Status403Forbidden, responseDto);
            case HttpStatusCode.Conflict:
                return Conflict(responseDto);
            case HttpStatusCode.InternalServerError:
                return StatusCode(StatusCodes.Status500InternalServerError, responseDto);
            default:
                return Ok(responseDto);
        }
    }
}