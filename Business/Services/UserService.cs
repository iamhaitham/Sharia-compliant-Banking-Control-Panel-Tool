using Business.Services.Interfaces;
using Core.DTOs;
using Infrastructure.Repositories.Interfaces;

namespace Business.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public RegisterUserResponseDto Register(RegisterUserRequestDto registerUserRequestDto)
    {
        var updatedDto = MapperService.MapMapRegisterUserRequestDtoToACopy(registerUserRequestDto);
        updatedDto.Password = BCrypt.Net.BCrypt.HashPassword(registerUserRequestDto.Password);

        var user = MapperService.MapRegisterUserRequestDtoToUser(updatedDto);

        try
        {
            _userRepository.Create(user);
            
            return MapperService.MapUserToRegisterUserResponseDto(user) ;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}