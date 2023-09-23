﻿using System.Text.Json;
using static Application.Base.Models.AppHostTemporaryCacheModels;

namespace Application.Services;

public sealed class AppHostTemporaryCache
{
    public AppHostTemporaryCache() { }

    private Dictionary<string, AppHostTemporaryDataCache> cache = new Dictionary<string, AppHostTemporaryDataCache>();

    public T? Get<T>(string host, string key)
    {
        if (!this.cache.ContainsKey(host))
            return default(T);

        var value = this.cache[host].Get(key);

        if (value is not null)
            return JsonSerializer.Deserialize<T>(value.data);

        return default(T);
    }

    public void Set<T>(string host, string key, T value, int secconds)
    {
        if (!this.cache.ContainsKey(host))
            this.cache.Add(host, new AppHostTemporaryDataCache());

        this.cache[host].Set(key.ToUpperInvariant(), JsonSerializer.Serialize<T>(value), secconds);
    }

    public void Unset(string host, string key)
    {
        if (!this.cache.ContainsKey(host)) return;
        this.cache[host].Unset(key);
    }

    private List<string> GetKeys()
    {
        List<string> keys = new List<string>();
        foreach (string key in this.cache.Keys) keys.Add(key);
        return keys;
    }

    public void UnsetValue()
    {
        foreach (string host in this.GetKeys())
            foreach (string key in this.cache[host].GetKeys())
            {
                Console.WriteLine($"{host} {key}");
                var value = this.cache[host].Get(key);

                if (value is null) continue;
                var end = value.now.AddSeconds(value.secconds);

                if (end <= DateTime.UtcNow) 
                    this.cache[host].Unset(key);
            }

    }
}

