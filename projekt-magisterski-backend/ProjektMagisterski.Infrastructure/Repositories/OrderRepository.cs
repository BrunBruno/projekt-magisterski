using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Infrastructure.Contexts;

namespace ProjektMagisterski.Infrastructure.Repositories;

public class OrderRepository
	: Repository<Order>, IOrderRepository {
	public OrderRepository(AppDbContext context) : base(context) { }

	public async Task<IReadOnlyList<Order>> GetByAgentIdAsync(
		Guid agentId,
		CancellationToken cancellationToken = default) {
		return await DbSet.AsNoTracking()
			.Where(x => x.SimulationAgentId == agentId)
			.OrderByDescending(x => x.CreatedAt)
			.ToListAsync(cancellationToken);
	}
}
