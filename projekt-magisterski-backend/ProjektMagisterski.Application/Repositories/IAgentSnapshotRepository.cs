using ProjektMagisterski.Core.Entities;

namespace ProjektMagisterski.Application.Repositories;

public interface IAgentSnapshotRepository : IRepository<AgentSnapshot> {
	Task<IReadOnlyList<AgentSnapshot>> GetByAgentIdAsync(
		Guid agentId,
		CancellationToken cancellationToken = default);

	Task<AgentSnapshot?> GetLatestAsync(
		Guid agentId,
		CancellationToken cancellationToken = default);
}
