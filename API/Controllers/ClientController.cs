using System.Net;
using Business.Services.Interfaces;
using Core.DTOs;
using Core.DTOs.Client;
using Core.Utilities;
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
    public async Task<ActionResult<GenericResponse<RegisterClientResponseDto>>> Register(
        RegisterClientRequestDto registerClientRequestDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var genericResponse = await _clientService.Register(registerClientRequestDto);

        switch (genericResponse.HttpCode)
        {
            case HttpStatusCode.Forbidden:
                return StatusCode(StatusCodes.Status403Forbidden, genericResponse);
            case HttpStatusCode.Conflict:
                return Conflict(genericResponse);
            case HttpStatusCode.InternalServerError:
                return StatusCode(StatusCodes.Status500InternalServerError, genericResponse);
            default:
                return Ok(genericResponse);
        }
    }

    [HttpGet("/query")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<GenericResponse<List<QueryClientResponseDto>>>> Query(
        [FromQuery] QueryClientRequestDto queryClientRequestDto
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var genericResponse = await _clientService.Query(queryClientRequestDto);

        switch (genericResponse.HttpCode)
        {
            case HttpStatusCode.InternalServerError:
                return StatusCode(StatusCodes.Status500InternalServerError, genericResponse);
            default:
                return Ok(genericResponse);
        }
    }

    [HttpGet("/suggestions")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<GenericResponse<Queue<QueryClientRequestDto>>>> GetLastThreeSearchQueries()
    {
        return Ok(await _clientService.GetLastThreeSearchQueries());
    }
}