using Core.DTOs.Client;
using Core.Utilities;

namespace Business.Services.Interfaces;

public interface IClientService
{
    public Task<GenericResponse<RegisterClientResponseDto>> Register(RegisterClientRequestDto registerClientRequestDto);
}