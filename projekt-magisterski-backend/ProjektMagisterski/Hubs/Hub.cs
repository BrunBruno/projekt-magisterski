using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using ProjektMagisterski.Application.Hubs;
using SignalRSwaggerGen.Attributes;

namespace ProjektMagisterski.Api.Hubs;

[SignalRHub]
public class ApplicationHub : Hub<IApplicationHub>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ApplicationHub(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
}
