using System.Linq.Expressions;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<Address> _dbSet;

    public AddressRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<Address>();
    }

    public async Task Create(Address address)
    {
        await _dbSet.AddAsync(address);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Address?> GetByFilter(Expression<Func<Address, bool>> filter)
    {
        return await _dbSet.FirstOrDefaultAsync(filter);
    }
}