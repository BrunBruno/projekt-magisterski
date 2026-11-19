using ProjektMagisterski.Application.Dtos;

namespace ProjektMagisterski.Application.Services;

public interface ISimulationNotifier {
	Task SendUpdateAsync(string connectionId, SimulationUpdateDto update, CancellationToken cancellationToken = default);
	Task SendCompletedAsync(string connectionId, string simulationId, CancellationToken cancellationToken = default);
}
