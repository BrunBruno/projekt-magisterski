using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Infrastructure.Contexts;

namespace ProjektMagisterski.Infrastructure.Repositories;

public class PositionRepository
	: Repository<Position>, IPositionRepository {
	public PositionRepository(AppDbContext context) : base(context) { }

	public async Task<IReadOnlyList<Position>> GetByPortfolioIdAsync(
		Guid portfolioId,
		CancellationToken cancellationToken = default) {
		return await DbSet.AsNoTracking()
			.Where(x => x.PortfolioId == portfolioId)
			.ToListAsync(cancellationToken);
	}
}
