using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Infrastructure.Contexts;

namespace ProjektMagisterski.Infrastructure.Repositories;

public class PortfolioRepository
	: Repository<Portfolio>, IPortfolioRepository {
	public PortfolioRepository(AppDbContext context) : base(context) { }

	public Task<Portfolio?> GetByAgentIdAsync(
		Guid agentId,
		CancellationToken cancellationToken = default) {
		return DbSet.FirstOrDefaultAsync(
			x => x.SimulationAgentId == agentId,
			cancellationToken);
	}
}