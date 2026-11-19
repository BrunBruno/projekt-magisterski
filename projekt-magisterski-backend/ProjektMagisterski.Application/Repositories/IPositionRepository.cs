using ProjektMagisterski.Core.Entities;

namespace ProjektMagisterski.Application.Repositories;

public interface IPositionRepository : IRepository<Position> {
	Task<IReadOnlyList<Position>> GetByPortfolioIdAsync(
		Guid portfolioId,
		CancellationToken cancellationToken = default);
}
