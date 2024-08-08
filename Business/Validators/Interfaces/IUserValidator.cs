using Core.DTOs;

namespace Business.Validators.Interfaces;

public interface IUserValidator : IUniquenessValidator<RegisterUserRequestDto, RegisterUserResponseDto>
{
    public Task<ResponseDto<LoginUserResponseDto>> CanUserBeAuthenticated(LoginUserRequestDto loginUserRequestDto);
}