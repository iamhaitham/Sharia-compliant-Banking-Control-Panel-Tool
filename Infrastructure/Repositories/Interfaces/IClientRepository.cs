using System.Linq.Expressions;
using Core.DTOs.Client;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces;

public interface IClientRepository
{
    public Task Create(Client client);

    public Task<Client?> GetByFilter(Expression<Func<Client, bool>> filter);

    public Task<List<Client>> Query(QueryClientRequestDto queryClientRequestDto);
}