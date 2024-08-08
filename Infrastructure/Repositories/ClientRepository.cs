using System.Linq.Expressions;
using Core.DTOs.Client;
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

    /// <summary>
    /// Get the "Client" table, and include the "Address" entity as well.
    /// Start finding the applied filters, and add them to the overall filter query.
    /// Once done, executed the query. 
    /// </summary>
    /// <param name="queryClientRequestDto">The query client request dto.</param>
    /// <returns>A list of the clients that satisfy the conditions of the query.</returns>
    public async Task<List<Client>> Query(QueryClientRequestDto queryClientRequestDto)
    {
        IQueryable<Client> query = _dbSet.Include(c => c.Address);
        
        if (!string.IsNullOrWhiteSpace(queryClientRequestDto.FirstName))
        {
            query = query.Where(c => c.FirstName.ToLower() == queryClientRequestDto.FirstName.ToLower());
        }
        
        if (!string.IsNullOrWhiteSpace(queryClientRequestDto.LastName))
        {
            query = query.Where(c => c.LastName.ToLower() == queryClientRequestDto.LastName.ToLower());
        }
        
        if (queryClientRequestDto.Sex is not null)
        {
            query = query.Where(c => c.Sex == queryClientRequestDto.Sex);
        }
        
        if (!string.IsNullOrWhiteSpace(queryClientRequestDto.Country))
        {
            query = query.Where(c => c.Address.Country == queryClientRequestDto.Country);
        }
        
        if (!string.IsNullOrWhiteSpace(queryClientRequestDto.City))
        {
            query = query.Where(c => c.Address.City == queryClientRequestDto.City);
        }
        
        if (!string.IsNullOrWhiteSpace(queryClientRequestDto.Street))
        {
            query = query.Where(c => c.Address.Street == queryClientRequestDto.Street);
        }
        
        if (!string.IsNullOrWhiteSpace(queryClientRequestDto.ZipCode))
        {
            query = query.Where(c => c.Address.ZipCode == queryClientRequestDto.ZipCode);
        }
        
        if (!string.IsNullOrWhiteSpace(queryClientRequestDto.MobileNumber))
        {
            query = query.Where(c => c.MobileNumber == queryClientRequestDto.MobileNumber);
        }
        
        if (!string.IsNullOrWhiteSpace(queryClientRequestDto.Email))
        {
            query = query.Where(c => c.Email == queryClientRequestDto.Email);
        }
        
        if (!string.IsNullOrWhiteSpace(queryClientRequestDto.PersonalId))
        {
            query = query.Where(c => c.PersonalId == queryClientRequestDto.PersonalId);
        }
        
        if (queryClientRequestDto.Accounts.Count != 0)
        {
            foreach (var account in queryClientRequestDto.Accounts)
            {
                query = query.Where(c => c.Accounts.Any(a => a == account));
            }
        }

        if (queryClientRequestDto.Skip is not null && queryClientRequestDto.Skip != 0)
        {
            query = query.Skip((int)queryClientRequestDto.Skip);
        }
        
        if (queryClientRequestDto.Take is not null && queryClientRequestDto.Take != 0)
        {
            query = query.Take((int)queryClientRequestDto.Take);
        }
        
        return await query.ToListAsync();
    }
}