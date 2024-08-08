using Core.DTOs;
using Core.Utilities;

namespace Business.Validators.Interfaces;

public interface IUserValidator : IUniquenessValidator<RegisterUserRequestDto, RegisterUserResponseDto>
{
    public Task<GenericResponse<LoginUserResponseDto>> CanUserBeAuthenticated(LoginUserRequestDto loginUserRequestDto);
}