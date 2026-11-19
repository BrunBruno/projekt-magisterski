using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Infrastructure.Contexts;

namespace ProjektMagisterski.Infrastructure.Repositories;

public class TradeRepository
	: Repository<Trade>, ITradeRepository {
	public TradeRepository(AppDbContext context) : base(context) { }

	public async Task<IReadOnlyList<Trade>> GetByAgentIdAsync(
		Guid agentId,
		CancellationToken cancellationToken = default) {
		return await DbSet.AsNoTracking()
			.Where(x => x.SimulationAgentId == agentId)
			.OrderByDescending(x => x.ExecutedAt)
			.ToListAsync(cancellationToken);
	}

	public async Task<IReadOnlyList<Trade>> GetByOrderIdAsync(
		Guid orderId,
		CancellationToken cancellationToken = default) {
		return await DbSet.AsNoTracking()
			.Where(x => x.OrderId == orderId)
			.OrderBy(x => x.ExecutedAt)
			.ToListAsync(cancellationToken);
	}
}
