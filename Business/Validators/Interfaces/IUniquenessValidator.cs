using Core.DTOs;

namespace Business.Validators.Interfaces;

public interface IUniquenessValidator<TRequestDto, TResponseDto>
{
    public Task<ResponseDto<TResponseDto>> IsUnique(TRequestDto requestDto);
}