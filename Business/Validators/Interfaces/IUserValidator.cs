using Core.DTOs;

namespace Business.Validators.Interfaces;

public interface IUserValidator
{
    public Task<ResponseDto<RegisterUserResponseDto>> IsUserUnique(RegisterUserRequestDto registerUserRequestDto);

    public Task<ResponseDto<LoginUserResponseDto>> CanUserBeAuthenticated(LoginUserRequestDto loginUserRequestDto);
}