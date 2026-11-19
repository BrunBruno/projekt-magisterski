using ProjektMagisterski.Core.Entities;

namespace ProjektMagisterski.Application.Repositories;

public interface ISimulationRepository : IRepository<Simulation> {
	Task<IReadOnlyList<Simulation>> GetByInstrumentIdAsync(
		Guid instrumentId,
		CancellationToken cancellationToken = default);
}