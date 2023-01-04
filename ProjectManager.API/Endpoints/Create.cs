﻿using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Project.Commands;
using ServiceContracts.Project.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ProjectManager.API.Endpoints;

public class Create : EndpointBaseAsync
    .WithRequest<CreateProjectCommand>
    .WithActionResult<DefaultResponseObject<ProjectVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Create(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Project/Create")]
    [SwaggerOperation(
        Summary = "Project creation",
        Description = "It is necessary to pass the name and description in the request body",
        Tags = new[] { "Project" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<ProjectVm>>> HandleAsync([FromBody]CreateProjectCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<ProjectVm>>(result));
    }
}