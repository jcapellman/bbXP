using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using bbxp.lib.Common;
using bbxp.lib.Database;
using bbxp.lib.Database.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VSDiagnostics;

namespace bbxp.web.api.Benchmarks;

[MemoryDiagnoser]
[CPUUsageDiagnoser]
public class PostsControllerBenchmarks
{
    private BbxpContext _dbContext = null!;
    private IMemoryCache _memoryCache = null!;
    private const int PostCount = 10;
    private const int Offset = 5;
    private const string TestCategory = "Technology";
    private const string TestUrl = "test-post-url";

    [GlobalSetup]
    public void Setup()
    {
        // Use in-memory database for benchmarking
        var options = new DbContextOptionsBuilder<BbxpContext>()
            .UseInMemoryDatabase(databaseName: $"BenchmarkDb_{Guid.NewGuid()}")
            .Options;
        _dbContext = new BbxpContext(options);
        _memoryCache = new MemoryCache(new MemoryCacheOptions());

        // Seed test data
        for (int i = 0; i < 100; i++)
        {
            _dbContext.Posts.Add(new Posts 
            { 
                Title = $"Test Post {i}", 
                Body = $"This is the body content for test post {i}. Lorem ipsum dolor sit amet.", 
                Category = i % 3 == 0 ? TestCategory : $"Category{i % 5}", 
                URL = i == 0 ? TestUrl : $"test-post-{i}", 
                PostDate = DateTime.Now.AddDays(-i), 
                Active = true 
            });
        }
        _dbContext.SaveChanges();
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _dbContext.Dispose();
        _memoryCache.Dispose();
    }

    [Benchmark(Description = "DB Query - Default Category with AsNoTracking")]
    public List<Posts> GetPosts_DefaultCategory_AsNoTracking()
    {
        return _dbContext.Posts
            .AsNoTracking()
            .Where(a => a.Active)
            .OrderByDescending(a => a.PostDate)
            .Take(PostCount)
            .ToList();
    }

    [Benchmark(Description = "OPTIMIZED - Compiled Query Default Category")]
    public async Task<List<Posts>> GetPosts_CompiledQuery()
    {
        return await BbxpContext.GetActivePostsAsync(_dbContext, PostCount).ToListAsync();
    }

    [Benchmark(Description = "DB Query - Specific Category with AsNoTracking")]
    public List<Posts> GetPosts_SpecificCategory_AsNoTracking()
    {
        return _dbContext.Posts
            .AsNoTracking()
            .Where(a => a.Active && a.Category == TestCategory)
            .OrderByDescending(a => a.PostDate)
            .ToList();
    }

    [Benchmark(Description = "OPTIMIZED - Compiled Query Category")]
    public async Task<List<Posts>> GetPosts_CompiledQuery_Category()
    {
        return await BbxpContext.GetPostsByCategoryAsync(_dbContext, TestCategory).ToListAsync();
    }

    [Benchmark(Description = "OPTIMIZED - Projected Summary (No Body)")]
    public async Task<List<bbxp.lib.DTOs.PostSummaryDto>> GetPosts_ProjectedSummary()
    {
        return await BbxpContext.GetPostSummariesAsync(_dbContext, PostCount).ToListAsync();
    }

    [Benchmark(Description = "Cache Hit - List Posts")]
    public List<Posts> CacheHit_ListPosts()
    {
        var cacheKey = "cached_posts";
        if (!_memoryCache.TryGetValue(cacheKey, out List<Posts>? result) || result is null)
        {
            result = _dbContext.Posts
                .AsNoTracking()
                .Where(a => a.Active)
                .OrderByDescending(a => a.PostDate)
                .Take(PostCount)
                .ToList();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(LibConstants.CACHE_HOUR_EXPIRATION));
            _memoryCache.Set(cacheKey, result, cacheEntryOptions);
        }
        return result;
    }

    [Benchmark(Description = "DB Query - Single Post FirstOrDefault")]
    public Posts? GetSinglePost_FirstOrDefault()
    {
        return _dbContext.Posts
            .AsNoTracking()
            .FirstOrDefault(a => a.Active && a.URL == TestUrl);
    }

    [Benchmark(Description = "Cache Hit - Single Post")]
    public Posts? CacheHit_SinglePost()
    {
        var cacheKey = TestUrl;
        if (!_memoryCache.TryGetValue(cacheKey, out Posts? result) || result is null)
        {
            result = _dbContext.Posts
                .AsNoTracking()
                .FirstOrDefault(a => a.Active && a.URL == TestUrl);
            if (result != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(LibConstants.CACHE_HOUR_EXPIRATION));
                _memoryCache.Set(cacheKey, result, cacheEntryOptions);
            }
        }
        return result;
    }

    [Benchmark(Description = "DB Query - Paged with Skip/Take")]
    public List<Posts> GetPosts_Paged_SkipTake()
    {
        return _dbContext.Posts
            .AsNoTracking()
            .Where(a => a.Active)
            .OrderByDescending(a => a.PostDate)
            .Skip(Offset)
            .Take(PostCount)
            .ToList();
    }

    [Benchmark(Description = "DB Query - Category Paged with Skip/Take")]
    public List<Posts> GetPosts_CategoryPaged_SkipTake()
    {
        return _dbContext.Posts
            .AsNoTracking()
            .Where(a => a.Active && a.Category == TestCategory)
            .OrderByDescending(a => a.PostDate)
            .Skip(Offset)
            .Take(PostCount)
            .ToList();
    }
}