using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjektMagisterski.Application.Requests;

namespace ProjektMagisterski.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SimulationController : ControllerBase {
	private readonly IMediator _mediator;

	public SimulationController(IMediator mediator) {
		_mediator = mediator;
	}

	public record StartRequest(string ConnectionId, int Steps = 10);

	[HttpPost("start")]
	public async Task<IActionResult> Start([FromBody] StartRequest request) {
		if (string.IsNullOrWhiteSpace(request.ConnectionId))
			return BadRequest("Brak ConnectionId z SignalR.");

		var simulationId = await _mediator.Send(new StartSimulationCommand(request.ConnectionId, request.Steps));
		return Ok(new { SimulationId = simulationId });
	}
}