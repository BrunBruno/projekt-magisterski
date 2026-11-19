using ProjektMagisterski.Core.Entities;

namespace ProjektMagisterski.Application.Repositories;

public interface ITradeRepository : IRepository<Trade> {
	Task<IReadOnlyList<Trade>> GetByAgentIdAsync(
		Guid agentId,
		CancellationToken cancellationToken = default);

	Task<IReadOnlyList<Trade>> GetByOrderIdAsync(
		Guid orderId,
		CancellationToken cancellationToken = default);
}
