using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Infrastructure.Contexts;

namespace ProjektMagisterski.Infrastructure.Repositories;

public class SimulationConfigurationRepository
	: Repository<SimulationConfiguration>, ISimulationConfigurationRepository {
	public SimulationConfigurationRepository(AppDbContext context) : base(context) { }

	public Task<SimulationConfiguration?> GetBySimulationIdAsync(
		Guid simulationId,
		CancellationToken cancellationToken = default) {
		return DbSet.FirstOrDefaultAsync(
			x => x.SimulationId == simulationId,
			cancellationToken);
	}
}
