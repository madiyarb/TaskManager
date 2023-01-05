using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Project.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjectManager.API.Endpoints;

public class Edit : EndpointBaseAsync
    .WithRequest<EditProjectCommand>
    .WithActionResult<DefaultResponseObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Edit(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Project/Edit")]
    [SwaggerOperation(
        Summary = "Edit creation",
        Description = "It is necessary to pass the name, state and description in the request body",
        Tags = new[] { "Project" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<string>>> HandleAsync(EditProjectCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(result));    
    }
}