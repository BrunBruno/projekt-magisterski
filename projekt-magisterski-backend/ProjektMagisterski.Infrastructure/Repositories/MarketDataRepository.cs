

using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Core.Enums;
using ProjektMagisterski.Infrastructure.Contexts;

namespace ProjektMagisterski.Infrastructure.Repositories;

public class MarketDataRepository
	: Repository<MarketData>, IMarketDataRepository {
	public MarketDataRepository(AppDbContext context) : base(context) { }

	public async Task<IReadOnlyList<MarketData>> GetByInstrumentAsync(
		Guid instrumentId,
		Timeframe timeframe,
		DateTimeOffset from,
		DateTimeOffset to,
		CancellationToken cancellationToken = default) {
		return await DbSet.AsNoTracking()
			.Where(x =>
				x.FinancialInstrumentId == instrumentId &&
				x.Timeframe == timeframe &&
				x.Timestamp >= from &&
				x.Timestamp <= to)
			.OrderBy(x => x.Timestamp)
			.ToListAsync(cancellationToken);
	}

	public Task<MarketData?> GetLatestAsync(
		Guid instrumentId,
		Timeframe timeframe,
		CancellationToken cancellationToken = default) {
		return DbSet.AsNoTracking()
			.Where(x =>
				x.FinancialInstrumentId == instrumentId &&
				x.Timeframe == timeframe)
			.OrderByDescending(x => x.Timestamp)
			.FirstOrDefaultAsync(cancellationToken);
	}
}