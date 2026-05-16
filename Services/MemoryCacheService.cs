using Microsoft.Extensions.Caching.Memory;

namespace BackEnd_student.Services;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public MemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public T? Get<T>(string key)
    {
        _cache.TryGetValue(key, out T? value);
        return value;
    }

    public void Set<T>(string key, T value, TimeSpan? expiration = null)
    {
        var options = new MemoryCacheEntryOptions();
        if (expiration.HasValue)
            options.AbsoluteExpirationRelativeToNow = expiration;
        else
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
        
        _cache.Set(key, value, options);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }

    public bool Exists(string key)
    {
        return _cache.TryGetValue(key, out _);
    }
}