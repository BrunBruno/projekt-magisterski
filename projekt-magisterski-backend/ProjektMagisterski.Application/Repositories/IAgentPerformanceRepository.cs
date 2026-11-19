using ProjektMagisterski.Core.Entities;

namespace ProjektMagisterski.Application.Repositories;

public interface IAgentPerformanceRepository : IRepository<AgentPerformance> {
	Task<AgentPerformance?> GetByAgentIdAsync(
		Guid agentId,
		CancellationToken cancellationToken = default);
}
