using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Task.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManager.API.Endpoints;

public class Edit : EndpointBaseAsync
    .WithRequest<EditTaskCommand>
    .WithActionResult<DefaultResponseObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Edit(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Task/Edit")]
    [SwaggerOperation(
        Summary = "Task edit",
        Description = "It is necessary to pass the name, description, projectid, priority in the request body" +
                      "Do not populate property(object) <<project>>",
        Tags = new[] { "Task" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<string>>> HandleAsync([FromBody]EditTaskCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(result));    
    }
}