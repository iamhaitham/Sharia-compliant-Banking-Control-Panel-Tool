using Core.DTOs;

namespace Business.Services.Interfaces;

public interface IClientService
{
    public Task<ResponseDto<RegisterClientResponseDto>> Register(RegisterClientRequestDto registerClientRequestDto);
}