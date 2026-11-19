using ProjektMagisterski.Core.Entities;

namespace ProjektMagisterski.Application.Repositories;

public interface ISimulationConfigurationRepository : IRepository<SimulationConfiguration> {
	Task<SimulationConfiguration?> GetBySimulationIdAsync(
		Guid simulationId,
		CancellationToken cancellationToken = default);
}
