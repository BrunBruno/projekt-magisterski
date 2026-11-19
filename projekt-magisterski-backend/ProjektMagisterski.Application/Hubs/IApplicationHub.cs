
using ProjektMagisterski.Application.Dtos;

namespace ProjektMagisterski.Application.Hubs;

public interface IApplicationHub
{
	Task ReceiveSimulationUpdate(SimulationUpdateDto update);
	Task SimulationCompleted(string simulationId);
}
