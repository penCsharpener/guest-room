using GuestRoom.Domain;
using GuestRoom.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GuestRoom.Api.Controllers.Visitor;

public class CountVisitorRequest : IRequest<int>
{
    public Guid SessionId { get; set; }
}

public class CountVisitorRequestHandler : IRequestHandler<CountVisitorRequest, int>
{
    private readonly AppDbContext _context;
    private readonly IMemoryCache _memoryCache;

    public CountVisitorRequestHandler(AppDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }

    public async Task<int> Handle(CountVisitorRequest request, CancellationToken cancellationToken)
    {
        MemoryCacheEntryOptions options = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(14)
        };

        Counter count = null;

        if (!_memoryCache.TryGetValue(request.SessionId, out _))
        {
            count = await GetOrSetCountAsync(cancellationToken);
            count.VisitorCount++;

            await _context.SaveChangesAsync(cancellationToken);

            _memoryCache.Set(request.SessionId, request.SessionId, options);

            return count.VisitorCount;
        }

        if (count == null)
        {
            count = await GetOrSetCountAsync(cancellationToken);
        }

        return count.VisitorCount;
    }

    private async Task<Counter> GetOrSetCountAsync(CancellationToken cancellationToken)
    {
        var count = await _context.Counter.FirstOrDefaultAsync(c => c.Id == 1, cancellationToken);

        if (count == null)
        {
            count = new Counter() { VisitorCount = 0 };
            _context.Counter.Add(count);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return count;
    }
}
