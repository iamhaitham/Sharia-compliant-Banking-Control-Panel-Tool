using Core.DTOs;

namespace Business.Validators.Interfaces;

public interface IClientValidator : IUniquenessValidator<RegisterClientRequestDto, RegisterClientResponseDto>
{
    
}