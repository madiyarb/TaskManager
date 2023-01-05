using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Project.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjectManager.API.Endpoints;

public class Delete : EndpointBaseAsync
    .WithRequest<DeleteProjectCommandHandler>
    .WithActionResult<DefaultResponseObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Delete(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Project/Delete")]
    [SwaggerOperation(
        Summary = "Project delete",
        Description = "It is necessary to pass the id in the request body",
        Tags = new[] { "Project" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<string>>> HandleAsync([FromBody]DeleteProjectCommandHandler request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(result));
    }
}