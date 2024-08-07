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

    public async Task<bool> IsUserUnique(User user)
    {
        return await _userRepository.GetByFilter(u => u.PersonalId == user.PersonalId) == null;
    }

    public async Task<ResponseDto<LoginUserResponseDto>> CanUserBeAuthenticated(LoginUserRequestDto loginUserRequestDto)
    {
        var realUser = await _userRepository.GetByFilter(u => u.Email == loginUserRequestDto.Email);

        // If no user exists with the same email address, then there is nothing to check and the user cannot be authenticated.
        if (realUser is null)
        {
            return new ResponseDto<LoginUserResponseDto>()
            {
                Errors = new List<string>()
                {
                    CustomErrorMessage.UserDoesNotExist(loginUserRequestDto.Email)
                },
                IsSuccessful = false
            };
        }
        
        // If user exists, check whether the hash of the password they entered matches with the hashed one in the database.
        if (!BCrypt.Net.BCrypt.Verify(loginUserRequestDto.Password, realUser.PasswordHash))
        {
            return new ResponseDto<LoginUserResponseDto>()
            {
                Errors = new List<string>()
                {
                    CustomErrorMessage.UserInfoDidNotMatch()
                },
                IsSuccessful = false
            };
        }
        
        return new ResponseDto<LoginUserResponseDto>()
        {
            Body = new LoginUserResponseDto(),
            IsSuccessful = true
        };
    }
}