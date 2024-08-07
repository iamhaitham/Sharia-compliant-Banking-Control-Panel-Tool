using Core.DTOs;

namespace Business.Services.Interfaces;

public interface IUserService
{
    public Task<RegisterUserResponseDto?> Register(RegisterUserRequestDto registerUserRequestDto);
    
    public Task<LoginUserResponseDto?> Login(LoginUserRequestDto loginUserRequestDto);
}