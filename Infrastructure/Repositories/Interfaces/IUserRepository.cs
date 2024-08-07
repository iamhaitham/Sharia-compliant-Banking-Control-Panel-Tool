using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    public Task Create(User user);
}