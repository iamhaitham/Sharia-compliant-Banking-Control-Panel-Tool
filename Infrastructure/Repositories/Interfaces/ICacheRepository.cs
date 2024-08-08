namespace Infrastructure.Repositories.Interfaces;

public interface ICacheRepository
{
    public Task<Queue<T>> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        where T : class;
    
    public Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
        where T : class;
}