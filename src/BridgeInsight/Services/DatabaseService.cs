using System.Net.Http.Json;
using BlazorWASMEntityFrameworkSQLite;
using BridgeInsight.Data;
using BridgeInsight.Models;
using BridgeInsight.Reference;
using Microsoft.EntityFrameworkCore;

namespace BridgeInsight.Services;

public class DatabaseService
{
    private readonly BWEFSFactory<BridgeDbContext> _dbFactory;
    private readonly HttpClient _http;
    private bool _isInitialized;

    public bool IsInitialized => _isInitialized;
    public int BridgeCount { get; private set; }
    public event Action? OnInitialized;

    public DatabaseService(BWEFSFactory<BridgeDbContext> dbFactory, HttpClient http)
    {
        _dbFactory = dbFactory;
        _http = http;
    }

    public async Task InitializeAsync()
    {
        if (_isInitialized) return;

        using var db = await _dbFactory.CreateDbContextAsync();
        BridgeCount = await db.Bridges.CountAsync();

        if (BridgeCount == 0)
        {
            await SeedAsync(db);
            BridgeCount = await db.Bridges.CountAsync();
        }

        _isInitialized = true;
        OnInitialized?.Invoke();
    }

    private async Task SeedAsync(BridgeDbContext db)
    {
        var bridges = await _http.GetFromJsonAsync<List<Bridge>>("data/wa-bridges-2024.json");
        if (bridges == null || bridges.Count == 0) return;

        // Decode county names
        foreach (var bridge in bridges)
        {
            if (string.IsNullOrEmpty(bridge.CountyName) && !string.IsNullOrEmpty(bridge.CountyCode))
            {
                bridge.CountyName = WaCountyCodes.GetCountyName(bridge.CountyCode);
            }
        }

        // Batch insert for performance
        const int batchSize = 500;
        for (int i = 0; i < bridges.Count; i += batchSize)
        {
            var batch = bridges.Skip(i).Take(batchSize);
            db.Bridges.AddRange(batch);
            await db.SaveChangesAsync();
        }
    }

    public async Task<BridgeDbContext> GetContextAsync()
    {
        if (!_isInitialized)
            await InitializeAsync();
        return await _dbFactory.CreateDbContextAsync();
    }
}
