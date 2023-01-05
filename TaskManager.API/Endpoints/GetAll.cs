using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Task.Models;
using ServiceContracts.Task.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskManager.API.Endpoints;

public class GetAll : EndpointBaseAsync
    .WithRequest<GetAllTasksQuery>
    .WithActionResult<DefaultResponseObject<AllTasksVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetAll(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Task/GetAll")]
    [SwaggerOperation(
        Summary = "Get project by id",
        Description = "Must be passed in the query string by id",
        Tags = new[] { "Task" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<AllTasksVm>>> HandleAsync(GetAllTasksQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<AllTasksVm>>(result));
    }
}