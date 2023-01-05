using Ardalis.Result;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Application.Contracts;
using ProjectManager.Domain.Entities;
using ServiceContracts.Project.Models;
using ServiceContracts.Project.Queries;

namespace ProjectManager.Application.Features.GetProject;

public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, Result<ProjectVm>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IValidator<GetProjectQuery> _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<GetProjectQueryHandler> _logger;

    public GetProjectQueryHandler(IProjectRepository projectRepository, IValidator<GetProjectQuery> validator, IMapper mapper, ILogger<GetProjectQueryHandler> logger)
    {
        _projectRepository = projectRepository;
        _validator = validator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<ProjectVm>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        try
        {
            ProjectDbModel? project = await _projectRepository.GetFirstOrDefaultAsync(u => u.Id == request.Id);
            if (project == null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Id} Id");
                return Result.Error($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Id} Id");
            }
            ProjectVm result = _mapper.Map<ProjectVm>(project);
            return Result.Success(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}