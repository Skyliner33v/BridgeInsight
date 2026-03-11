using BridgeInsight.Data;
using BridgeInsight.Models;
using BridgeInsight.Reference;
using BlazorWASMEntityFrameworkSQLite;
using Microsoft.EntityFrameworkCore;

namespace BridgeInsight.Services;

public class BridgeDataService
{
    private readonly BWEFSFactory<BridgeDbContext> _dbFactory;

    public BridgeDataService(BWEFSFactory<BridgeDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<BridgeSearchResult> SearchBridgesAsync(BridgeSearchCriteria criteria)
    {
        using var db = await _dbFactory.CreateDbContextAsync();
        IQueryable<Bridge> query = db.Bridges;

        // Text search
        if (!string.IsNullOrWhiteSpace(criteria.SearchText))
        {
            var search = criteria.SearchText.ToUpper();
            query = query.Where(b =>
                b.FacilityCarried.ToUpper().Contains(search) ||
                b.FeaturesIntersected.ToUpper().Contains(search) ||
                b.StructureNumber.ToUpper().Contains(search));
        }

        // County filter
        if (!string.IsNullOrEmpty(criteria.CountyCode))
            query = query.Where(b => b.CountyCode == criteria.CountyCode);

        // Condition filter
        if (criteria.MinCondition.HasValue || criteria.MaxCondition.HasValue)
        {
            var min = criteria.MinCondition ?? 0;
            var max = criteria.MaxCondition ?? 9;

            query = query.Where(b =>
                (b.DeckCondition >= min && b.DeckCondition <= max) ||
                (b.SuperstructureCondition >= min && b.SuperstructureCondition <= max) ||
                (b.SubstructureCondition >= min && b.SubstructureCondition <= max) ||
                (b.CulvertCondition >= min && b.CulvertCondition <= max));
        }

        // Year built range
        if (criteria.MinYearBuilt.HasValue)
            query = query.Where(b => b.YearBuilt >= criteria.MinYearBuilt.Value);
        if (criteria.MaxYearBuilt.HasValue)
            query = query.Where(b => b.YearBuilt <= criteria.MaxYearBuilt.Value);

        // ADT range
        if (criteria.MinAdt.HasValue)
            query = query.Where(b => b.AverageDailyTraffic >= criteria.MinAdt.Value);
        if (criteria.MaxAdt.HasValue)
            query = query.Where(b => b.AverageDailyTraffic <= criteria.MaxAdt.Value);

        // Structurally deficient only
        if (criteria.StructurallyDeficientOnly)
        {
            query = query.Where(b =>
                b.DeckCondition <= 4 ||
                b.SuperstructureCondition <= 4 ||
                b.SubstructureCondition <= 4 ||
                b.CulvertCondition <= 4);
        }

        // Scour critical only
        if (criteria.ScourCriticalOnly)
        {
            query = query.Where(b =>
                b.ScourCritical == "0" || b.ScourCritical == "1" ||
                b.ScourCritical == "2" || b.ScourCritical == "3");
        }

        // Get total count before pagination
        var totalCount = await query.CountAsync();

        // Compute stats on filtered set
        var allFiltered = await query.ToListAsync();
        var avgAge = allFiltered.Where(b => b.YearBuilt.HasValue)
            .Select(b => DateTime.Now.Year - b.YearBuilt!.Value)
            .DefaultIfEmpty(0).Average();
        var sdCount = allFiltered.Count(b => b.IsStructurallyDeficient);
        var pctSd = totalCount > 0 ? (double)sdCount / totalCount * 100 : 0;
        var avgAdt = allFiltered.Where(b => b.AverageDailyTraffic.HasValue)
            .Select(b => (double)b.AverageDailyTraffic!.Value)
            .DefaultIfEmpty(0).Average();

        // Sort
        query = criteria.SortBy switch
        {
            "YearBuilt" => criteria.SortDescending ? query.OrderByDescending(b => b.YearBuilt) : query.OrderBy(b => b.YearBuilt),
            "ADT" => criteria.SortDescending ? query.OrderByDescending(b => b.AverageDailyTraffic) : query.OrderBy(b => b.AverageDailyTraffic),
            "County" => criteria.SortDescending ? query.OrderByDescending(b => b.CountyName) : query.OrderBy(b => b.CountyName),
            "Condition" => criteria.SortDescending
                ? query.OrderByDescending(b => new[] { b.DeckCondition, b.SuperstructureCondition, b.SubstructureCondition, b.CulvertCondition }.Min())
                : query.OrderBy(b => new[] { b.DeckCondition, b.SuperstructureCondition, b.SubstructureCondition, b.CulvertCondition }.Min()),
            _ => criteria.SortDescending ? query.OrderByDescending(b => b.FacilityCarried) : query.OrderBy(b => b.FacilityCarried)
        };

        // Paginate
        var bridges = await query
            .Skip((criteria.Page - 1) * criteria.PageSize)
            .Take(criteria.PageSize)
            .ToListAsync();

        return new BridgeSearchResult
        {
            Bridges = bridges,
            TotalCount = totalCount,
            AverageAge = avgAge,
            PercentStructurallyDeficient = pctSd,
            AverageAdt = avgAdt
        };
    }

    public async Task<Bridge?> GetBridgeAsync(string structureNumber)
    {
        using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Bridges.FirstOrDefaultAsync(b => b.StructureNumber == structureNumber);
    }

    public async Task<List<Bridge>> GetBridgesAsync(List<string> structureNumbers)
    {
        using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Bridges
            .Where(b => structureNumbers.Contains(b.StructureNumber))
            .ToListAsync();
    }

    public async Task<Dictionary<string, int>> GetCountyDistributionAsync()
    {
        using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Bridges
            .GroupBy(b => b.CountyName)
            .Select(g => new { County = g.Key, Count = g.Count() })
            .ToDictionaryAsync(g => g.County, g => g.Count);
    }

    public async Task<int> GetTotalBridgeCountAsync()
    {
        using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Bridges.CountAsync();
    }
}
