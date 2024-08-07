using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<User> _dbSet;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<User>();
    }

    public async Task Create(User user)
    {
        await _dbSet.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}