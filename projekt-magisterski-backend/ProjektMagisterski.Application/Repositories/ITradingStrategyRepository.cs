using ProjektMagisterski.Core.Entities;

namespace ProjektMagisterski.Application.Repositories;

public interface ITradingStrategyRepository : IRepository<TradingStrategy> {
	Task<TradingStrategy?> GetByCodeAsync(
		string code,
		CancellationToken cancellationToken = default);

	Task<IReadOnlyList<TradingStrategy>> GetActiveAsync(
		CancellationToken cancellationToken = default);
}
