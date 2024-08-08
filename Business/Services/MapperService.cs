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

    public static Client MapRegisterClientRequestDtoToClient(
        RegisterClientRequestDto registerClientRequestDto
    )
    {
        return new Client()
        {
            Accounts = registerClientRequestDto.Accounts,
            Email = registerClientRequestDto.Email,
            Sex = registerClientRequestDto.Sex,
            FirstName = registerClientRequestDto.FirstName,
            LastName = registerClientRequestDto.LastName,
            MobileNumber = registerClientRequestDto.MobileNumber.Number,
            PersonalId = registerClientRequestDto.PersonalId,
            Address = MapAddressDtoToAddress(registerClientRequestDto.Address),
            ProfilePhoto = registerClientRequestDto.ProfilePhoto
        };
    }

    private static Address MapAddressDtoToAddress(AddressDto addressDto)
    {
        return new Address()
        {
            Country = addressDto.Country,
            City = addressDto.City,
            Street = addressDto.Street,
            ZipCode = addressDto.ZipCode
        };
    }
}