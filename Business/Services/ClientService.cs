using System.Net;
using Business.DTOs;
using Business.Services.Interfaces;
using Business.Validators.Interfaces;
using Core.DTOs.Client;
using Core.Utilities;
using Infrastructure.Repositories.Interfaces;

namespace Business.Services;

public class ClientService : IClientService
{
    private static readonly string SuggestionsCacheKey = "suggestions";
    private readonly IClientRepository _clientRepository;
    private readonly IClientValidator _clientValidator;
    private readonly IAddressRepository _addressRepository;
    private readonly IAddressValidator _addressValidator;
    private readonly ICacheRepository _cacheRepository;

    public ClientService(
        IClientRepository clientRepository,
        IClientValidator clientValidator,
        IAddressRepository addressRepository,
        IAddressValidator addressValidator,
        ICacheRepository cacheRepository
    )
    {
        _clientRepository = clientRepository;
        _clientValidator = clientValidator;
        _addressRepository = addressRepository;
        _addressValidator = addressValidator;
        _cacheRepository = cacheRepository;
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
            // Pass the QueryClientResponseDto to IClientRepository to do the needed filtration logic. 
            var clients = await _clientRepository.Query(queryClientRequestDto);
            var result = new List<QueryClientResponseDto>();

            // Map all returned clients to QueryClientResponseDto.
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
        finally
        {
            // Retrieve the suggestions, and check their number.
            // If they exceed 3, then remove the oldest value, and append the newest one.
            // The cache will then be updated regardless of the suggestions' size. 
            var cachedSuggestions = 
                await _cacheRepository.GetAsync<QueryClientRequestDto>("suggestions");

            if (cachedSuggestions.Count >= 3)
            {
                cachedSuggestions.Dequeue();
            }
            
            cachedSuggestions.Enqueue(queryClientRequestDto);
            await _cacheRepository.SetAsync(SuggestionsCacheKey, cachedSuggestions);
        }
    }

    public async Task<GenericResponse<Queue<QueryClientRequestDto>>> GetLastThreeSearchQueries()
    {
        return new GenericResponse<Queue<QueryClientRequestDto>>()
        {
            Body = await _cacheRepository.GetAsync<QueryClientRequestDto>(SuggestionsCacheKey),
            IsSuccessful = true
        };
    }
}