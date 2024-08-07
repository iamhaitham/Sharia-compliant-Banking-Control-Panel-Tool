using System.Linq.Expressions;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    public Task Create(User user);

    public Task<User?> GetByFilter(Expression<Func<User, bool>> filter);
}