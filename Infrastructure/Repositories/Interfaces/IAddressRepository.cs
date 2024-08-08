using System.Linq.Expressions;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces;

public interface IAddressRepository
{
    public Task Create(Address address);

    public Task<Address?> GetByFilter(Expression<Func<Address, bool>> filter);
}