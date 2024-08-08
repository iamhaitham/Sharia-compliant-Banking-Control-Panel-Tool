using Business.DTOs;
using Core.DTOs.Address;
using Core.DTOs.Client;
using Core.DTOs.User;
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
            CreatedOn = DateTime.UtcNow
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
            ProfilePhoto = registerClientRequestDto.ProfilePhoto,
            CreatedOn = DateTime.UtcNow
        };
    }
    
    public static QueryClientResponseDto MapClientToQueryClientResponseDto(Client client)
    {
        return new QueryClientResponseDto()
        {
            Accounts = client.Accounts,
            FirstName = client.FirstName,
            LastName = client.LastName,
            MobileNumber = client.MobileNumber,
            PersonalId = client.PersonalId,
            Address =  MapAddressToAddressDto(client.Address),
            Sex = client.Sex,
            Email = client.Email
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

    private static AddressDto MapAddressToAddressDto(Address address)
    {
        return new AddressDto()
        {
            Country = address.Country,
            City = address.City,
            Street = address.Street,
            ZipCode = address.ZipCode
        };
    }
}