using System.Net;
using Business.Services.Interfaces;
using Business.Validators.Interfaces;
using Core.DTOs;
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

    /// <summary>
    /// Checks whether a user exists in the database.
    /// <br/>
    /// If yes, stop the process.
    /// <br/>
    /// If no, register the user.
    /// </summary>
    /// <param name="registerUserRequestDto">The DTO representing the data for registering a user.</param>
    /// <returns>A <see cref="RegisterUserResponseDto">RegisterUserResponseDto</see> wrapped in a <see cref="RegisterUserResponseDto">ResponseDto</see></returns>
    public async Task<ResponseDto<RegisterUserResponseDto>> Register(RegisterUserRequestDto registerUserRequestDto)
    {
        var isUserUniqueResponse = await _userValidator.IsUserUnique(registerUserRequestDto);
        if (!isUserUniqueResponse.IsSuccessful)
        {
            return isUserUniqueResponse;
        }

        var updatedDto = MapperService.MapRegisterUserRequestDtoToACopy(registerUserRequestDto);
        updatedDto.Password = BCrypt.Net.BCrypt.HashPassword(registerUserRequestDto.Password);
        var user = MapperService.MapRegisterUserRequestDtoToUser(updatedDto);

        try
        {
            await _userRepository.Create(user);
            
            return new ResponseDto<RegisterUserResponseDto>()
            {
                Body = MapperService.MapUserToRegisterUserResponseDto(user),
                IsSuccessful = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto<RegisterUserResponseDto>()
            {
                Errors = new List<string>()
                {
                    ex.Message
                },
                IsSuccessful = false,
                HttpCode = HttpStatusCode.InternalServerError
            };
        }
    }

    /// <summary>
    /// Authenticates a user if possible.
    /// </summary>
    /// <param name="loginUserRequestDto">The DTO representing the data for logging a user in.</param>
    /// <returns>A <see cref="LoginUserResponseDto">LoginUserResponseDto</see> wrapped in a <see cref="RegisterUserResponseDto">ResponseDto</see></returns>
    public async Task<ResponseDto<LoginUserResponseDto>> Login(LoginUserRequestDto loginUserRequestDto)
    {
        return await _userValidator.CanUserBeAuthenticated(loginUserRequestDto);
    } 
}