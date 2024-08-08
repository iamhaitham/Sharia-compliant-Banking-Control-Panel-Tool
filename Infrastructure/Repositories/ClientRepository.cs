using System.Linq.Expressions;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<Client> _dbSet;
    
    public ClientRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<Client>();
    }

    public async Task Create(Client client)
    {
        await _dbSet.AddAsync(client);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Client?> GetByFilter(Expression<Func<Client, bool>> filter)
    {
        return await _dbSet.FirstOrDefaultAsync(filter);
    }
}