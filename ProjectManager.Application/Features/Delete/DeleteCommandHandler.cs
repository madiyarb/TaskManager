using Ardalis.Result;
using AutoMapper;
using CommonStructures;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Application.Contracts;
using ProjectManager.Domain.Complementary;
using ProjectManager.Domain.Entities;
using ServiceContracts.Project.Models;
using ServiceContracts.Project.Queries;

namespace ProjectManager.Application.Features.Delete;

public class DeleteCommandHandler : IRequestHandler<DeleteProjectCommand, Result<string>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteCommandHandler> _logger;

    public DeleteCommandHandler(IProjectRepository projectRepository, IMapper mapper, ILogger<DeleteCommandHandler> logger)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ProjectDbModel? project = await _projectRepository.GetFirstOrDefaultAsync(t=>t.Id==request.Id);
            if (project is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Project not found with id {request.Id}");
                return Result.NotFound($"{BussinesErrors.NotFound.ToString()}: Project not found with id {request.Id}");
            }
            if (project.State != StateEnums.Completed)
            {
                _logger.LogWarning($"{BussinesErrors.ProjectNotCompleted.ToString()}: Project is not completed id {request.Id}");
                return Result.NotFound($"{BussinesErrors.ProjectNotCompleted.ToString()}: Project is not completed id {request.Id}");
            }
            await _projectRepository.DeleteAsync(project);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
        }
    }
}