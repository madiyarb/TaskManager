using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Task.Commands;
using ServiceContracts.Task.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManager.API.Endpoints;

public class Create : EndpointBaseAsync
    .WithRequest<CreateTaskCommand>
    .WithActionResult<DefaultResponseObject<TaskVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Create(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Task/Create")]
    [SwaggerOperation(
        Summary = "Task creation",
        Description = "It is necessary to pass the name, description, projectid, priority in the request body" +
                      "Do not populate property(object) <<project>>",
        Tags = new[] { "Task" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<TaskVm>>> HandleAsync([FromBody]CreateTaskCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<TaskVm>>(result));    
    }
}