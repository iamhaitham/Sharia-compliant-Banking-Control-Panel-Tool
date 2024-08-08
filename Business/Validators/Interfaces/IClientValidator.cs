using Business.DTOs;
using Core.DTOs.Client;

namespace Business.Validators.Interfaces;

public interface IClientValidator : IUniquenessValidator<RegisterClientRequestDto, RegisterClientResponseDto>
{
    
}