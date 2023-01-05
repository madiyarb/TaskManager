using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Project.Models;
using ServiceContracts.Project.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjectManager.API.Endpoints;

public class GetProject : EndpointBaseAsync
    .WithRequest<GetProjectQuery>
    .WithActionResult<DefaultResponseObject<ProjectVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetProject(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/Project/GetProject")]
    [SwaggerOperation(
        Summary = "Get project by id",
        Description = "Must be passed in the query string by id",
        Tags = new[] { "Project" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<ProjectVm>>> HandleAsync([FromQuery]GetProjectQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<ProjectVm>>(result));
    }
}