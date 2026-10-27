using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProjektMagisterski.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Controller : ControllerBase 
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Controller(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
}