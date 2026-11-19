using ProjektMagisterski.Core.Entities;

namespace ProjektMagisterski.Application.Repositories;

public interface IOrderRepository : IRepository<Order> {
	Task<IReadOnlyList<Order>> GetByAgentIdAsync(
		Guid agentId,
		CancellationToken cancellationToken = default);
}