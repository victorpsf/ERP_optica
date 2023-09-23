using Application.Interfaces.Middleware;
using Application.Services;
using Microsoft.AspNetCore.Http;

namespace Application.Middleware;

public class HostCache: IHostCache
{
    private AppHostTemporaryCache appHostTemporaryCache;
    private IHttpContextAccessor contextAcessor;
    private HttpContext? httpContext;

    public HostCache(IHttpContextAccessor contextAcessor, AppHostTemporaryCache appHostTemporaryCache)
    {
        this.appHostTemporaryCache = appHostTemporaryCache;
        this.contextAcessor = contextAcessor;
        this.httpContext = contextAcessor.HttpContext;
    }

    private string? Host
    {
        get => this.httpContext?.Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
    }

    public T? Get<T>(string name)
    {
        if (this.Host == null) return default(T);
        return this.appHostTemporaryCache.Get<T>(this.Host, name);
    }

    public void Set<T>(string name, T value, int secconds)
    {
        if (this.Host == null) return;
        this.appHostTemporaryCache.Set(this.Host, name, value, secconds);
    }

    public void Unset(string name)
    {
        if (this.Host == null) return;
        this.appHostTemporaryCache.Unset(this.Host, name);
    }
}
