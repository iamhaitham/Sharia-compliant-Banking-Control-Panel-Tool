using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    public void Create(User user);
}