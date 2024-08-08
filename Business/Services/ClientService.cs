using System.Net;
using Business.Services.Interfaces;
using Business.Validators.Interfaces;
using Core.DTOs;
using Infrastructure.Repositories.Interfaces;

namespace Business.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IClientValidator _clientValidator;

    public ClientService(
        IClientRepository clientRepository,
        IClientValidator clientValidator
    )
    {
        _clientRepository = clientRepository;
        _clientValidator = clientValidator;
    }

    public async Task<ResponseDto<RegisterClientResponseDto>> Register(
        RegisterClientRequestDto registerClientRequestDto
    )
    {
        var isClientUniqueResponse = await _clientValidator.IsUnique(registerClientRequestDto);

        if (!isClientUniqueResponse.IsSuccessful)
        {
            return isClientUniqueResponse;
        }
        
        var client = MapperService.MapRegisterClientRequestDtoToClient(registerClientRequestDto);

        try
        {
            await _clientRepository.Create(client);

            return new ResponseDto<RegisterClientResponseDto>()
            {
                IsSuccessful = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto<RegisterClientResponseDto>()
            {
                Errors = new List<string>()
                {
                    ex.Message
                },
                IsSuccessful = false,
                HttpCode = HttpStatusCode.InternalServerError
            };
        }
    }
}