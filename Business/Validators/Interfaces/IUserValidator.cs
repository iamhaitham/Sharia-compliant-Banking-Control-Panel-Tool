using Core.DTOs;
using Infrastructure.Entities;

namespace Business.Validators.Interfaces;

public interface IUserValidator
{
    public Task<bool> IsUserUnique(User user);

    public Task<bool> CanUserBeAuthenticated(LoginUserRequestDto loginUserRequestDto);
}