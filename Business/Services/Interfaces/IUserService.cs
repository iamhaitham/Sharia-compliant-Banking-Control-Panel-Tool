using Core.DTOs;

namespace Business.Services.Interfaces;

public interface IUserService
{
    public RegisterUserResponseDto Register(RegisterUserRequestDto registerUserRequestDto);
}