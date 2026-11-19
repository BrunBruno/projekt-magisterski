using ProjektMagisterski.Application.Services;
using System.Threading.Channels;

namespace ProjektMagisterski.Infrastructure.Workers;

public class InMemorySimulationQueue : ISimulationQueue {
	private readonly Channel<SimulationJob> _queue = Channel.CreateUnbounded<SimulationJob>();

	public async ValueTask EnqueueAsync(SimulationJob job, CancellationToken cancellationToken = default) {
		await _queue.Writer.WriteAsync(job, cancellationToken);
	}

	public async ValueTask<SimulationJob> DequeueAsync(CancellationToken cancellationToken) {
		return await _queue.Reader.ReadAsync(cancellationToken);
	}
}
