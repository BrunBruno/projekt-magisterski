

using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Infrastructure.Contexts;

namespace ProjektMagisterski.Infrastructure.Repositories;

public class TradingStrategyRepository
	: Repository<TradingStrategy>, ITradingStrategyRepository {
	public TradingStrategyRepository(AppDbContext context) : base(context) { }

	public Task<TradingStrategy?> GetByCodeAsync(
		string code,
		CancellationToken cancellationToken = default) {
		return DbSet.FirstOrDefaultAsync(
			x => x.Code == code,
			cancellationToken);
	}

	public async Task<IReadOnlyList<TradingStrategy>> GetActiveAsync(
		CancellationToken cancellationToken = default) {
		return await DbSet.AsNoTracking()
			.Where(x => x.IsActive)
			.ToListAsync(cancellationToken);
	}
}