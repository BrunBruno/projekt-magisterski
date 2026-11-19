using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Infrastructure.Contexts;

namespace ProjektMagisterski.Infrastructure.Repositories;

public class SimulationEventRepository
	: Repository<SimulationEvent>, ISimulationEventRepository {
	public SimulationEventRepository(AppDbContext context) : base(context) { }

	public async Task<IReadOnlyList<SimulationEvent>> GetBySimulationIdAsync(
		Guid simulationId,
		CancellationToken cancellationToken = default) {
		return await DbSet.AsNoTracking()
			.Where(x => x.SimulationId == simulationId)
			.OrderBy(x => x.Timestamp)
			.ToListAsync(cancellationToken);
	}

	public async Task<IReadOnlyList<SimulationEvent>> GetByAgentIdAsync(
		Guid agentId,
		CancellationToken cancellationToken = default) {
		return await DbSet.AsNoTracking()
			.Where(x => x.SimulationAgentId == agentId)
			.OrderBy(x => x.Timestamp)
			.ToListAsync(cancellationToken);
	}
}
