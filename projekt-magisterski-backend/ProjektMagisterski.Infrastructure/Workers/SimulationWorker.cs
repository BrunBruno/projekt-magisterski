
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjektMagisterski.Application.Dtos;
using ProjektMagisterski.Application.Services;

namespace ProjektMagisterski.Infrastructure.Workers;

public class SimulationWorker : BackgroundService {
	private readonly ISimulationQueue _queue;
	private readonly ISimulationNotifier _notifier;
	private readonly ILogger<SimulationWorker> _logger;

	public SimulationWorker(
		ISimulationQueue queue,
		ISimulationNotifier notifier,
		ILogger<SimulationWorker> logger) {
		_queue = queue;
		_notifier = notifier;
		_logger = logger;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
		while (!stoppingToken.IsCancellationRequested) {
			var job = await _queue.DequeueAsync(stoppingToken);

			// Uruchamiamy symulację jako osobne zadanie, aby worker mógł przyjmować kolejne
			_ = RunSimulationAsync(job, stoppingToken);
		}
	}

	private async Task RunSimulationAsync(SimulationJob job, CancellationToken ct) {
		_logger.LogInformation("Start symulacji {SimulationId}", job.SimulationId);

		for (int step = 1; step <= job.TotalSteps; step++) {
			if (ct.IsCancellationRequested)
				break;

			// Symulacja obliczeń trwających 1 sekundę
			await Task.Delay(1000, ct);
			double calculatedValue = Math.Round(Random.Shared.NextDouble() * 100, 2);

			var update = new SimulationUpdateDto(
				job.SimulationId,
				step,
				calculatedValue,
				$"Krok {step}/{job.TotalSteps}: Policzono wartość = {calculatedValue}",
				DateTime.UtcNow
			);

			await _notifier.SendUpdateAsync(job.ConnectionId, update, ct);
		}

		await _notifier.SendCompletedAsync(job.ConnectionId, job.SimulationId, ct);
		_logger.LogInformation("Zakończono symulację {SimulationId}", job.SimulationId);
	}
}
