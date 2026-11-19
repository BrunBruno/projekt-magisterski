using ProjektMagisterski.Core.Entities;
using ProjektMagisterski.Core.Enums;

namespace ProjektMagisterski.Application.Repositories;

public interface IMarketDataRepository : IRepository<MarketData> {
	Task<IReadOnlyList<MarketData>> GetByInstrumentAsync(
		Guid instrumentId,
		Timeframe timeframe,
		DateTimeOffset from,
		DateTimeOffset to,
		CancellationToken cancellationToken = default);

	Task<MarketData?> GetLatestAsync(
		Guid instrumentId,
		Timeframe timeframe,
		CancellationToken cancellationToken = default);
}