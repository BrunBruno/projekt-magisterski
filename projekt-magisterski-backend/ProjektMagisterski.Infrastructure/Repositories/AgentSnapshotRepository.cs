using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Infrastructure.Contexts;

namespace ProjektMagisterski.Infrastructure.Repositories;

public class AgentSnapshotRepository
	: Repository<AgentSnapshot>, IAgentSnapshotRepository {
	public AgentSnapshotRepository(AppDbContext context) : base(context) { }

	public async Task<IReadOnlyList<AgentSnapshot>> GetByAgentIdAsync(
		Guid agentId,
		CancellationToken cancellationToken = default) {
		return await DbSet.AsNoTracking()
			.Where(x => x.SimulationAgentId == agentId)
			.OrderBy(x => x.StepNumber)
			.ToListAsync(cancellationToken);
	}

	public Task<AgentSnapshot?> GetLatestAsync(
		Guid agentId,
		CancellationToken cancellationToken = default) {
		return DbSet.AsNoTracking()
			.Where(x => x.SimulationAgentId == agentId)
			.OrderByDescending(x => x.StepNumber)
			.FirstOrDefaultAsync(cancellationToken);
	}
}
