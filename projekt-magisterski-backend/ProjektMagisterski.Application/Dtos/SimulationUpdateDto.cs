
namespace ProjektMagisterski.Application.Dtos;

public record SimulationUpdateDto(
	string SimulationId,
	int Step,
	double CalculatedValue,
	string Message,
	DateTime Timestamp
);