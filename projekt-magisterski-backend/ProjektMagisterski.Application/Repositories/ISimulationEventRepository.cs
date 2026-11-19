using ProjektMagisterski.Core.Entities;

namespace ProjektMagisterski.Application.Repositories;

public interface ISimulationEventRepository : IRepository<SimulationEvent> {
	Task<IReadOnlyList<SimulationEvent>> GetBySimulationIdAsync(
		Guid simulationId,
		CancellationToken cancellationToken = default);

	Task<IReadOnlyList<SimulationEvent>> GetByAgentIdAsync(
		Guid agentId,
		CancellationToken cancellationToken = default);
}