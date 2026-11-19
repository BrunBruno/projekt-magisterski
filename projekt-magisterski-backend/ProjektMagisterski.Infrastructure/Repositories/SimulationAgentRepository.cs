using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Infrastructure.Contexts;

namespace ProjektMagisterski.Infrastructure.Repositories;

public class SimulationAgentRepository
	: Repository<SimulationAgent>, ISimulationAgentRepository {
	public SimulationAgentRepository(AppDbContext context) : base(context) { }

	public async Task<IReadOnlyList<SimulationAgent>> GetBySimulationIdAsync(
		Guid simulationId,
		CancellationToken cancellationToken = default) {
		return await DbSet.AsNoTracking()
			.Where(x => x.SimulationId == simulationId)
			.ToListAsync(cancellationToken);
	}
}
