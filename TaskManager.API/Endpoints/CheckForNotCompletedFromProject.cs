using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Project.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManager.API.Endpoints;

public class CheckForNotCompletedFromProject : EndpointBaseAsync
    .WithRequest<CheckForNotCompletedFromProjectQuery>
    .WithActionResult<DefaultResponseObject<bool>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CheckForNotCompletedFromProject(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Task/CheckForNotCompletedFromProject")]
    [SwaggerOperation(
        Summary = "Check for not completed tasks from project",
        Description = "need to pass the id in the query string",
        Tags = new[] { "Task" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<bool>>> HandleAsync(CheckForNotCompletedFromProjectQuery request,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<bool>>(result));
    }
}