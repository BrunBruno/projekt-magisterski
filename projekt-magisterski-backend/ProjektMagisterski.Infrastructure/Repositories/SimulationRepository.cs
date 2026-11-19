using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Infrastructure.Contexts;

namespace ProjektMagisterski.Infrastructure.Repositories;

public class SimulationRepository
	: Repository<Simulation>, ISimulationRepository {
	public SimulationRepository(AppDbContext context) : base(context) { }

	public async Task<IReadOnlyList<Simulation>> GetByInstrumentIdAsync(
		Guid instrumentId,
		CancellationToken cancellationToken = default) {
		return await DbSet.AsNoTracking()
			.Where(x => x.FinancialInstrumentId == instrumentId)
			.OrderByDescending(x => x.CreatedAt)
			.ToListAsync(cancellationToken);
	}
}