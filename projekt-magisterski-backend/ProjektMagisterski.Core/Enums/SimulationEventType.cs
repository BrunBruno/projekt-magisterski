namespace ProjektMagisterski.Core.Enums;

public enum SimulationEventType {
	SimulationStarted,
	SimulationCompleted,
	SimulationFailed,
	SimulationCancelled,

	AgentStarted,
	AgentStopped,

	SignalGenerated,
	OrderCreated,
	OrderExecuted,
	OrderRejected,

	PositionOpened,
	PositionClosed,

	Error,
	Information
}
