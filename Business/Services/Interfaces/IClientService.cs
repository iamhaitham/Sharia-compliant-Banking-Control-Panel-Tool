using Business.DTOs;
using Core.DTOs.Client;
using Core.Utilities;

namespace Business.Services.Interfaces;

public interface IClientService
{
    public Task<GenericResponse<RegisterClientResponseDto>> Register(RegisterClientRequestDto registerClientRequestDto);

    public Task<GenericResponse<List<QueryClientResponseDto>>> Query(QueryClientRequestDto queryClientRequestDto);
    
    public Task<GenericResponse<Queue<QueryClientRequestDto>>> GetLastThreeSearchQueries();
}