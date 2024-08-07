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
        // Replace the plain password with a hashed one to store it in the database
        var updatedDto = MapperService.MapMapRegisterUserRequestDtoToACopy(registerUserRequestDto);
        updatedDto.Password = BCrypt.Net.BCrypt.HashPassword(registerUserRequestDto.Password);
        
        var user = MapperService.MapRegisterUserRequestDtoToUser(updatedDto);

        // If user is unique proceed with the registration. Otherwise, return null
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

        // Create the user and return the representing object. If an exception is throw, log it.
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

    public async Task<ResponseDto<LoginUserResponseDto>> Login(LoginUserRequestDto loginUserRequestDto)
    {
        return await _userValidator.CanUserBeAuthenticated(loginUserRequestDto);
    } 
}