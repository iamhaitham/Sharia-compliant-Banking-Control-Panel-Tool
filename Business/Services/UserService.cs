using Business.Services.Interfaces;
using Business.Validators.Interfaces;
using Core.DTOs;
using Core.Utilities;
using Infrastructure.Repositories.Interfaces;

namespace Business.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserValidator _userValidator;

    public UserService(
        IUserRepository userRepository,
        IUserValidator userValidator
    )
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
    }

    public async Task<RegisterUserResponseDto?> Register(RegisterUserRequestDto registerUserRequestDto)
    {
        var updatedDto = MapperService.MapMapRegisterUserRequestDtoToACopy(registerUserRequestDto);
        updatedDto.Password = BCrypt.Net.BCrypt.HashPassword(registerUserRequestDto.Password);

        var user = MapperService.MapRegisterUserRequestDtoToUser(updatedDto);

        if (!await _userValidator.IsUserUnique(user))
        {
            Console.WriteLine(
                CustomErrorMessage.UserAlreadyExists(
                    $"{user.FirstName} {user.LastName}",
                    user.PersonalId
                )
            );

            return null;
        }

        try
        {
            await _userRepository.Create(user);
            
            return MapperService.MapUserToRegisterUserResponseDto(user) ;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}