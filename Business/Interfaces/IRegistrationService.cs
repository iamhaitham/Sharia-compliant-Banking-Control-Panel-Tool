using Core.DTOs;

namespace Business.Interfaces;

public interface IRegistrationService
{
    public RegisterUserResponseDto Register(RegisterUserRequestDto registerUserRequestDto);
}