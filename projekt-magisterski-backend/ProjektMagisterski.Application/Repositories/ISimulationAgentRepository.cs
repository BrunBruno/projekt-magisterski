using ProjektMagisterski.Core.Entities;

namespace ProjektMagisterski.Application.Repositories;

public interface ISimulationAgentRepository : IRepository<SimulationAgent> {
	Task<IReadOnlyList<SimulationAgent>> GetBySimulationIdAsync(
		Guid simulationId,
		CancellationToken cancellationToken = default);
}