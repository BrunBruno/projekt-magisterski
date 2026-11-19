using ProjektMagisterski.Core.Abstract;
using ProjektMagisterski.Core.Enums;

namespace ProjektMagisterski.Core.Entities;

public class TradingStrategy : BaseEntity {
	public string Code { get; private set; } = string.Empty;
	public string Name { get; private set; } = string.Empty;
	public string Description { get; private set; } = string.Empty;

	public StrategyCategory Category { get; private set; }

	public bool IsActive { get; private set; } = true;

	private TradingStrategy() { }

	public TradingStrategy(
		string code,
		string name,
		string description,
		StrategyCategory category) {
		if (string.IsNullOrWhiteSpace(code))
			throw new ArgumentException("Code is required.");

		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Name is required.");

		Code = code.Trim();
		Name = name.Trim();
		Description = description?.Trim() ?? string.Empty;
		Category = category;
	}

	public void Activate() => IsActive = true;

	public void Deactivate() => IsActive = false;
}
