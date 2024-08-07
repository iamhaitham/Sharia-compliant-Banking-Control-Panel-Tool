using Infrastructure.Entities;

namespace Business.Validators.Interfaces;

public interface IUserValidator
{
    public Task<bool> IsUserUnique(User user);
}