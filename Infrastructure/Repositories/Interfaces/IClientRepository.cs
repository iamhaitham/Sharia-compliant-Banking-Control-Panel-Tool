﻿using System.Linq.Expressions;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces;

public interface IClientRepository
{
    public Task Create(Client client);

    public Task<Client?> GetByFilter(Expression<Func<Client, bool>> filter);
}