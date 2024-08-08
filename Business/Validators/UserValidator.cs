using System.Net;
using Business.Validators.Interfaces;
using Core.DTOs;
using Core.Utilities;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Business.Validators;

public class UserValidator : IUserValidator
{
    private readonly IUserRepository _userRepository;

    public UserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResponseDto<RegisterUserResponseDto>> IsUnique(RegisterUserRequestDto registerUserRequestDto)
    {
        User? userFromDatabase;
        try
        {
            userFromDatabase = await _userRepository.GetByFilter(u =>
                    u.PersonalId == registerUserRequestDto.PersonalId 
                    || u.Email == registerUserRequestDto.Email
                    || u.MobileNumber == registerUserRequestDto.MobileNumber.Number
                );
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

        var isUserUnique = userFromDatabase == null;

        if (!isUserUnique)
        {
            return new ResponseDto<RegisterUserResponseDto>()
            {
                Errors = new List<string>()
                {
                    CustomErrorMessage.UserAlreadyExists(
                        registerUserRequestDto.PersonalId,
                        registerUserRequestDto.Email,
                        registerUserRequestDto.MobileNumber.Number
                    )
                },
                IsSuccessful = false,
                HttpCode = HttpStatusCode.Conflict
            };
        }

        return new ResponseDto<RegisterUserResponseDto>()
        {
            IsSuccessful = true
        };
    }

    public async Task<ResponseDto<LoginUserResponseDto>> CanUserBeAuthenticated(LoginUserRequestDto loginUserRequestDto)
    {
        User? userFromDatabase;

        try
        {
            userFromDatabase = await _userRepository.GetByFilter(u => u.Email == loginUserRequestDto.Email);
        }
        catch (Exception ex)
        {
            return new ResponseDto<LoginUserResponseDto>()
            {
                Errors = new List<string>()
                {
                    ex.Message
                },
                IsSuccessful = false,
                HttpCode = HttpStatusCode.InternalServerError
            };
        }

        // If no user exists with the same email address, then there is nothing to check and the user cannot be authenticated.
        if (userFromDatabase is null)
        {
            return new ResponseDto<LoginUserResponseDto>()
            {
                Errors = new List<string>()
                {
                    CustomErrorMessage.UserDoesNotExist(loginUserRequestDto.Email)
                },
                IsSuccessful = false,
                HttpCode = HttpStatusCode.NotFound
            };
        }
        
        // If user exists, check whether the hash of the password they entered matches with the hashed one in the database.
        if (!BCrypt.Net.BCrypt.Verify(loginUserRequestDto.Password, userFromDatabase.PasswordHash))
        {
            return new ResponseDto<LoginUserResponseDto>()
            {
                Errors = new List<string>()
                {
                    CustomErrorMessage.UserInfoDidNotMatch()
                },
                IsSuccessful = false,
                HttpCode = HttpStatusCode.Unauthorized
            };
        }
        
        return new ResponseDto<LoginUserResponseDto>()
        {
            Body = new LoginUserResponseDto(),
            IsSuccessful = true
        };
    }
}