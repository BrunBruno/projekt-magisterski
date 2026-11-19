using ProjektMagisterski.Core.Abstract;
using ProjektMagisterski.Core.Enums;

namespace ProjektMagisterski.Core.Entities;

public class SimulationEvent : BaseEntity {
	public Guid SimulationId { get; private set; }

	public Guid? SimulationAgentId { get; private set; }

	public SimulationEventType EventType { get; private set; }

	public string Message { get; private set; } = string.Empty;

	public string? MetadataJson { get; private set; }

	public DateTimeOffset Timestamp { get; private set; }

	private SimulationEvent() { }

	public SimulationEvent(
		Guid simulationId,
		Guid? simulationAgentId,
		SimulationEventType eventType,
		string message,
		string? metadataJson = null) {
		if (simulationId == Guid.Empty)
			throw new ArgumentException("Simulation is required.");

		if (string.IsNullOrWhiteSpace(message))
			throw new ArgumentException("Message is required.");

		SimulationId = simulationId;
		SimulationAgentId = simulationAgentId;
		EventType = eventType;
		Message = message;
		MetadataJson = metadataJson;
		Timestamp = DateTimeOffset.UtcNow;
	}
}
