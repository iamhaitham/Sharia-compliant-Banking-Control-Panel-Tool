using System.Net;
using Business.Services.Interfaces;
using Business.Validators.Interfaces;
using Core.DTOs.Client;
using Core.Utilities;
using Infrastructure.Repositories.Interfaces;

namespace Business.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IClientValidator _clientValidator;
    private readonly IAddressRepository _addressRepository;
    private readonly IAddressValidator _addressValidator;

    public ClientService(
        IClientRepository clientRepository,
        IClientValidator clientValidator,
        IAddressRepository addressRepository,
        IAddressValidator addressValidator
    )
    {
        _clientRepository = clientRepository;
        _clientValidator = clientValidator;
        _addressRepository = addressRepository;
        _addressValidator = addressValidator;
    }

    public async Task<GenericResponse<RegisterClientResponseDto>> Register(
        RegisterClientRequestDto registerClientRequestDto
    )
    {
        // If client is already registered, stop the registration process.
        var isClientUniqueResponse = await _clientValidator.IsUnique(registerClientRequestDto);

        if (!isClientUniqueResponse.IsSuccessful)
        {
            return isClientUniqueResponse;
        }
        
        var client = MapperService.MapRegisterClientRequestDtoToClient(registerClientRequestDto);

        // Validate whether the address already exists or not, and only create it if it does not exist yet (unique).
        var isAddressUniqueResponse = await _addressValidator.IsUnique(registerClientRequestDto.Address);

        if (!isAddressUniqueResponse.IsSuccessful)
        {
            return new GenericResponse<RegisterClientResponseDto>()
            {
                Error = isAddressUniqueResponse.Error,
                IsSuccessful = isAddressUniqueResponse.IsSuccessful,
                HttpCode = isAddressUniqueResponse.HttpCode
            };
        }

        if (isAddressUniqueResponse.HttpCode == HttpStatusCode.Conflict)
        {
            client.Address = isAddressUniqueResponse.Body!;
        }
        else
        {
            try
            {
                await _addressRepository.Create(
                    client.Address
                );
            }
            catch (Exception ex)
            {
                return new GenericResponse<RegisterClientResponseDto>()
                {
                    Error = ex.Message,
                    IsSuccessful = false,
                    HttpCode = HttpStatusCode.InternalServerError
                };
            }
        }
        
        try
        {
            await _clientRepository.Create(client);

            return new GenericResponse<RegisterClientResponseDto>()
            {
                IsSuccessful = true
            };
        }
        catch (Exception ex)
        {
            return new GenericResponse<RegisterClientResponseDto>()
            {
                Error = ex.Message,
                IsSuccessful = false,
                HttpCode = HttpStatusCode.InternalServerError
            };
        }
    }

    public async Task<GenericResponse<List<QueryClientResponseDto>>> Query(QueryClientRequestDto queryClientRequestDto)
    {
        try
        {
            var clients = await _clientRepository.Query(queryClientRequestDto);
            var result = new List<QueryClientResponseDto>();

            foreach (var c in clients)
            {
                result.Add(MapperService.MapClientToQueryClientResponseDto(c));
            }

            return new GenericResponse<List<QueryClientResponseDto>>()
            {
                Body = result,
                IsSuccessful = true
            };
        }
        catch (Exception ex)
        {
            return new GenericResponse<List<QueryClientResponseDto>>()
            {
                Error = ex.Message,
                IsSuccessful = false,
                HttpCode = HttpStatusCode.InternalServerError
            };
        }
    }
}