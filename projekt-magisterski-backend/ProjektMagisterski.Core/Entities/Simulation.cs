using ProjektMagisterski.Core.Abstract;
using ProjektMagisterski.Core.Enums;

namespace ProjektMagisterski.Core.Entities;

public class Simulation : BaseEntity {
	public string Name { get; private set; } = string.Empty;

	public Guid FinancialInstrumentId { get; private set; }

	public DateTimeOffset StartDate { get; private set; }
	public DateTimeOffset EndDate { get; private set; }

	public Timeframe Timeframe { get; private set; }

	public decimal InitialCapital { get; private set; }

	public int RandomSeed { get; private set; }

	public SimulationStatus Status { get; private set; }

	public DateTimeOffset CreatedAt { get; private set; }

	public DateTimeOffset? StartedAt { get; private set; }
	public DateTimeOffset? CompletedAt { get; private set; }

	private Simulation() { }

	public Simulation(
		string name,
		Guid financialInstrumentId,
		DateTimeOffset startDate,
		DateTimeOffset endDate,
		Timeframe timeframe,
		decimal initialCapital,
		int randomSeed) {
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Name is required.");

		if (financialInstrumentId == Guid.Empty)
			throw new ArgumentException("Instrument is required.");

		if (startDate >= endDate)
			throw new ArgumentException("Invalid simulation date range.");

		if (initialCapital <= 0)
			throw new ArgumentException("Initial capital must be positive.");

		Name = name.Trim();
		FinancialInstrumentId = financialInstrumentId;
		StartDate = startDate.ToUniversalTime();
		EndDate = endDate.ToUniversalTime();
		Timeframe = timeframe;
		InitialCapital = initialCapital;
		RandomSeed = randomSeed;
		Status = SimulationStatus.Pending;
		CreatedAt = DateTimeOffset.UtcNow;
	}

	public void Start() {
		if (Status != SimulationStatus.Pending)
			throw new InvalidOperationException("Simulation cannot be started.");

		Status = SimulationStatus.Running;
		StartedAt = DateTimeOffset.UtcNow;
	}

	public void Complete() {
		if (Status != SimulationStatus.Running)
			throw new InvalidOperationException("Simulation is not running.");

		Status = SimulationStatus.Completed;
		CompletedAt = DateTimeOffset.UtcNow;
	}

	public void Cancel() {
		if (Status != SimulationStatus.Pending &&
			Status != SimulationStatus.Running)
			throw new InvalidOperationException("Simulation cannot be cancelled.");

		Status = SimulationStatus.Cancelled;
		CompletedAt = DateTimeOffset.UtcNow;
	}

	public void Fail() {
		if (Status != SimulationStatus.Running)
			throw new InvalidOperationException("Simulation is not running.");

		Status = SimulationStatus.Failed;
		CompletedAt = DateTimeOffset.UtcNow;
	}
}
