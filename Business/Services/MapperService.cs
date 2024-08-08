using Core.DTOs;
using Infrastructure.Entities;

namespace Business.Services;

public static class MapperService
{
    public static RegisterUserRequestDto MapRegisterUserRequestDtoToACopy(RegisterUserRequestDto registerUserRequestDto)
    {
        return new RegisterUserRequestDto()
        {
            Email = registerUserRequestDto.Email,
            Password = registerUserRequestDto.Password,
            Role = registerUserRequestDto.Role,
            Sex = registerUserRequestDto.Sex,
            FirstName = registerUserRequestDto.FirstName,
            LastName = registerUserRequestDto.LastName,
            MobileNumber = registerUserRequestDto.MobileNumber,
            PersonalId = registerUserRequestDto.PersonalId
        };
    }

    public static User MapRegisterUserRequestDtoToUser(RegisterUserRequestDto registerUserRequestDto)
    {
        return new User()
        {
            Email = registerUserRequestDto.Email,
            PasswordHash = registerUserRequestDto.Password,
            Role = registerUserRequestDto.Role,
            Sex = registerUserRequestDto.Sex,
            FirstName = registerUserRequestDto.FirstName,
            LastName = registerUserRequestDto.LastName,
            MobileNumber = registerUserRequestDto.MobileNumber.Number,
            PersonalId = registerUserRequestDto.PersonalId,
            CreatedOn = DateTimeOffset.Now
        };
    }

    public static RegisterUserResponseDto MapUserToRegisterUserResponseDto(User user)
    {
        return new RegisterUserResponseDto()
        {
            Email = user.Email,
            PasswordHash = user.PasswordHash
        };
    }
}