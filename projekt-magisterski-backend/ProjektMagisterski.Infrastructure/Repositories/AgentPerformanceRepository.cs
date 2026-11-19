
using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Infrastructure.Contexts;

namespace ProjektMagisterski.Infrastructure.Repositories;

public class AgentPerformanceRepository
	: Repository<AgentPerformance>, IAgentPerformanceRepository {
	public AgentPerformanceRepository(AppDbContext context) : base(context) { }

	public Task<AgentPerformance?> GetByAgentIdAsync(
		Guid agentId,
		CancellationToken cancellationToken = default) {
		return DbSet.FirstOrDefaultAsync(
			x => x.SimulationAgentId == agentId,
			cancellationToken);
	}
}