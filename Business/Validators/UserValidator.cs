using Business.Validators.Interfaces;
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
}