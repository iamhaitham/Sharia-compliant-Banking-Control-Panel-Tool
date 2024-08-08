using Core.DTOs.User;
using Core.Utilities;

namespace Business.Services.Interfaces;

public interface IUserService
{
    public Task<GenericResponse<RegisterUserResponseDto>> Register(RegisterUserRequestDto registerUserRequestDto);
    
    public Task<GenericResponse<LoginUserResponseDto>> Login(LoginUserRequestDto loginUserRequestDto);
}