using Microsoft.AspNetCore.SignalR;
using ProjektMagisterski.Api.Hubs;
using ProjektMagisterski.Application.Dtos;
using ProjektMagisterski.Application.Hubs;
using ProjektMagisterski.Application.Services;

namespace ProjektMagisterski.Infrastructure.Services;

public class SignalRSimulationNotifier : ISimulationNotifier {
	private readonly IHubContext<ApplicationHub, IApplicationHub> _hubContext;

	public SignalRSimulationNotifier(IHubContext<ApplicationHub, IApplicationHub> hubContext) {
		_hubContext = hubContext;
	}

	public async Task SendUpdateAsync(string connectionId, SimulationUpdateDto update, CancellationToken cancellationToken = default) {
		await _hubContext.Clients.Client(connectionId).ReceiveSimulationUpdate(update);
	}

	public async Task SendCompletedAsync(string connectionId, string simulationId, CancellationToken cancellationToken = default) {
		await _hubContext.Clients.Client(connectionId).SimulationCompleted(simulationId);
	}
}