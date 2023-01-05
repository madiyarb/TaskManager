using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Project.Commands;
using ServiceContracts.Task.Commands;
using ServiceContracts.Task.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManager.API.Endpoints;

public class Delete : EndpointBaseAsync
    .WithRequest<DeleteTaskCommand>
    .WithActionResult<DefaultResponseObject<DefaultResponseObject<string>>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Delete(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Task/Delete")]
    [SwaggerOperation(
        Summary = "Task delete",
        Description = "It is necessary to pass the id in the request body",
        Tags = new[] { "Task" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<DefaultResponseObject<string>>>> HandleAsync([FromBody]DeleteTaskCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(result)); 
    }
}