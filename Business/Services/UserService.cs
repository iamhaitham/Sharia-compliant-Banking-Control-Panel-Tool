using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Business.Services.Interfaces;
using Business.Validators.Interfaces;
using Core.DTOs.User;
using Core.Enums;
using Core.Utilities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserValidator _userValidator;
    private readonly IConfiguration _configuration;
    private static readonly string JwtKeyFromAppSettings = "JWT";

    public UserService(
        IUserRepository userRepository,
        IUserValidator userValidator,
        IConfiguration configuration
    )
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _configuration = configuration;
    }

    /// <summary>
    /// Checks whether a user exists in the database.
    /// <br/>
    /// If yes, stop the process.
    /// <br/>
    /// If no, register the user.
    /// </summary>
    /// <param name="registerUserRequestDto">The DTO representing the data for registering a user.</param>
    /// <returns>A <see cref="RegisterUserResponseDto">RegisterUserResponseDto</see> wrapped in a <see cref="GenericResponse">GenericResponse</see>.</returns>
    public async Task<GenericResponse<RegisterUserResponseDto>> Register(
        RegisterUserRequestDto registerUserRequestDto
    )
    {
        var isUserUniqueResponse = await _userValidator.IsUnique(registerUserRequestDto);
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
            
            return new GenericResponse<RegisterUserResponseDto>()
            {
                Body = MapperService.MapUserToRegisterUserResponseDto(user),
                IsSuccessful = true
            };
        }
        catch (Exception ex)
        {
            return new GenericResponse<RegisterUserResponseDto>()
            {
                Error = ex.Message,
                IsSuccessful = false,
                HttpCode = HttpStatusCode.InternalServerError
            };
        }
    }

    /// <summary>
    /// Authenticates a user if possible.
    /// </summary>
    /// <param name="loginUserRequestDto">The DTO representing the data for logging a user in.</param>
    /// <returns>A <see cref="LoginUserResponseDto">LoginUserResponseDto</see> wrapped in a <see cref="GenericResponse">GenericResponse</see>.</returns>
    public async Task<GenericResponse<LoginUserResponseDto>> Login(
        LoginUserRequestDto loginUserRequestDto
    )
    {
        var loginUserResponseDto = await _userValidator.CanUserBeAuthenticated(loginUserRequestDto);

        if (!loginUserResponseDto.IsSuccessful)
        {
            return new GenericResponse<LoginUserResponseDto>()
            {
                Error = loginUserResponseDto.Error,
                IsSuccessful = loginUserResponseDto.IsSuccessful,
                HttpCode = loginUserResponseDto.HttpCode
            };
        }

        return new GenericResponse<LoginUserResponseDto>()
        {
            Body = new LoginUserResponseDto()
            {
                Email = loginUserResponseDto.Body!.Email,
                Jwt = CreateToken(loginUserResponseDto.Body),
                Role = loginUserResponseDto.Body.Role
            },
            IsSuccessful = true
        };
    }

    private string CreateToken(LoginUserResponseDto loginUserResponseDto)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, loginUserResponseDto.Email),
            new Claim(ClaimTypes.Role, Enum.GetName(typeof(UserRole), loginUserResponseDto.Role)!)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration[JwtKeyFromAppSettings]!
            )
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256Signature
        );

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials 
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        
        return jwt;
    }
}