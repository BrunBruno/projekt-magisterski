using ProjektMagisterski.Core.Entities;

namespace ProjektMagisterski.Application.Repositories;

public interface IPortfolioRepository : IRepository<Portfolio> {
	Task<Portfolio?> GetByAgentIdAsync(
		Guid agentId,
		CancellationToken cancellationToken = default);
}
