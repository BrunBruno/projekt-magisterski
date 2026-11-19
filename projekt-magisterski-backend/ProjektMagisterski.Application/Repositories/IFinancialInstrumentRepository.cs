using ProjektMagisterski.Core.Entities;

namespace ProjektMagisterski.Application.Repositories;

public interface IFinancialInstrumentRepository : IRepository<FinancialInstrument> {
	Task<FinancialInstrument?> GetBySymbolAsync(
		string symbol,
		CancellationToken cancellationToken = default);
}
