
using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Infrastructure.Contexts;

namespace ProjektMagisterski.Infrastructure.Repositories;

public class FinancialInstrumentRepository
	: Repository<FinancialInstrument>, IFinancialInstrumentRepository {
	public FinancialInstrumentRepository(AppDbContext context) : base(context) { }

	public Task<FinancialInstrument?> GetBySymbolAsync(
		string symbol,
		CancellationToken cancellationToken = default) {
		return DbSet.FirstOrDefaultAsync(
			x => x.Symbol == symbol,
			cancellationToken);
	}
}
