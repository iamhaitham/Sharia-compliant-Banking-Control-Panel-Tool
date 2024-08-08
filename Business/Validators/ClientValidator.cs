using System.Net;
using Business.Validators.Interfaces;
using Core.DTOs;
using Core.Utilities;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Business.Validators;

public class ClientValidator : IClientValidator
{
    private readonly IClientRepository _clientRepository;

    public ClientValidator(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<GenericResponse<RegisterClientResponseDto>> IsUnique(
        RegisterClientRequestDto registerClientRequestDto
    )
    {
        Client? clientFromDatabase;

        try
        {
            clientFromDatabase = await _clientRepository.GetByFilter(c =>
                c.PersonalId == registerClientRequestDto.PersonalId
                || c.Email == registerClientRequestDto.Email
                || c.MobileNumber == registerClientRequestDto.MobileNumber.Number
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

        var isClientUnique = clientFromDatabase == null;

        if (!isClientUnique)
        {
            return new GenericResponse<RegisterClientResponseDto>()
            {
                Error = CustomErrorMessage.ClientAlreadyExists(
                    registerClientRequestDto.PersonalId,
                    registerClientRequestDto.Email,
                    registerClientRequestDto.MobileNumber.Number
                ),
                IsSuccessful = false,
                HttpCode = HttpStatusCode.Conflict
            };
        }

        return new GenericResponse<RegisterClientResponseDto>()
        {
            IsSuccessful = true
        };
    }
}