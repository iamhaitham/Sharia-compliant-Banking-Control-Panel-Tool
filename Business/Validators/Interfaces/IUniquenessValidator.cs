using Core.DTOs;
using Core.Utilities;

namespace Business.Validators.Interfaces;

public interface IUniquenessValidator<TRequestDto, TResponseDto>
{
    public Task<GenericResponse<TResponseDto>> IsUnique(TRequestDto requestDto);
}