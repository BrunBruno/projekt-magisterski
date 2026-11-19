using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Infrastructure.Contexts;

namespace ProjektMagisterski.Infrastructure.Repositories;

public class StrategyParameterRepository
	: Repository<StrategyParameter>, IStrategyParameterRepository {
	public StrategyParameterRepository(AppDbContext context) : base(context) { }

	public async Task<IReadOnlyList<StrategyParameter>> GetByStrategyIdAsync(
		Guid strategyId,
		CancellationToken cancellationToken = default) {
		return await DbSet.AsNoTracking()
			.Where(x => x.TradingStrategyId == strategyId)
			.ToListAsync(cancellationToken);
	}
}
