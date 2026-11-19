namespace ProjektMagisterski.Application.Services;

public record SimulationJob(string SimulationId, string ConnectionId, int TotalSteps);

public interface ISimulationQueue {
	ValueTask EnqueueAsync(SimulationJob job, CancellationToken cancellationToken = default);
	ValueTask<SimulationJob> DequeueAsync(CancellationToken cancellationToken);
}
