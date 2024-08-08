using Core.DTOs;
using Core.Utilities;

namespace Business.Services.Interfaces;

public interface IClientService
{
    public Task<GenericResponse<RegisterClientResponseDto>> Register(RegisterClientRequestDto registerClientRequestDto);
}