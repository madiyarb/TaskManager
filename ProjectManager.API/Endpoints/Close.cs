using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Project.Commands;
using ServiceContracts.Project.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjectManager.API.Endpoints;

public class Close : EndpointBaseAsync
    .WithRequest<CloseProjectCommand>
    .WithActionResult<DefaultResponseObject<DefaultResponseObject<string>>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Close(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("/Project/Close")]
    [SwaggerOperation(
        Summary = "Close the project",
        Description = "Set CompletionDate field" +
                      "Do not populate property(object) <<NotComletedTasks>>",
        Tags = new[] { "Project" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<DefaultResponseObject<string>>>> HandleAsync([FromBody]CloseProjectCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(result));
    }
}