using ProjektMagisterski.Core.Entities;

namespace ProjektMagisterski.Application.Repositories;

public interface IStrategyParameterRepository : IRepository<StrategyParameter> {
	Task<IReadOnlyList<StrategyParameter>> GetByStrategyIdAsync(
		Guid strategyId,
		CancellationToken cancellationToken = default);
}