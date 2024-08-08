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

    private static async Task<string> MapIFormFileProfileImageToFilePath(IFormFile? profilePhoto)
    {
        if (profilePhoto is null)
        {
            return string.Empty;
        }

        try
        {
            // Get the root path of the solution.
            var rootPathOfApp = Path.GetFullPath("..");

            // This is the name of the directory that will be created/used on the root path of the solution.
            const string staticImagesDirectoryName = "profile_photos";

            // This is the path from the root of the solution to the "static images" directory.
            var staticImagesPath = $"{rootPathOfApp}/{staticImagesDirectoryName}";

            // A new name for the image that will now be stored locally.
            var imageNameWithExtensions = $"{Guid.NewGuid()}.{Path.GetExtension(profilePhoto.FileName)}";

            // If the directory exists, do nothing. Otherwise, create it.
            Directory.CreateDirectory(staticImagesPath);

            // This is the complete path of the file with the extension starting from the root of the solution.
            var pathToStoreTheFile = $"{staticImagesPath}/{imageNameWithExtensions}";

            // Store the image locally to the determined path.
            using (var stream = File.Create(pathToStoreTheFile))
            {
                await profilePhoto.CopyToAsync(stream);
            }

            return pathToStoreTheFile;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            return string.Empty;
        }
    }
}