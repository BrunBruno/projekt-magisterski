using MediatR;
using ProjektMagisterski.Application.Services;

namespace ProjektMagisterski.Application.Requests;

public record StartSimulationCommand(string ConnectionId, int Steps = 10) : IRequest<string>;

public class StartSimulationCommandHandler : IRequestHandler<StartSimulationCommand, string> {
	private readonly ISimulationQueue _queue;

	public StartSimulationCommandHandler(ISimulationQueue queue) {
		_queue = queue;
	}

	public async Task<string> Handle(StartSimulationCommand request, CancellationToken cancellationToken) {
		var simulationId = Guid.NewGuid().ToString("N");
		var job = new SimulationJob(simulationId, request.ConnectionId, request.Steps);

		await _queue.EnqueueAsync(job, cancellationToken);

		return simulationId;
	}
}
