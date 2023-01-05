using Ardalis.Result;
using AutoMapper;
using CommonStructures;
using MediatR;
using Microsoft.Extensions.Logging;
using ServiceContracts.Project.Queries;
using TaskManager.Application.Contracts;

namespace TaskManager.Application.Features.CheckForNotCompletedFromProject;

public class CheckForNotCompletedFromProjectQueryHandler : IRequestHandler<CheckForNotCompletedFromProjectQuery, Result<bool>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<CheckForNotCompletedFromProjectQueryHandler> _logger;
    private readonly IMapper _mapper;

    public CheckForNotCompletedFromProjectQueryHandler(ITaskRepository taskRepository, ILogger<CheckForNotCompletedFromProjectQueryHandler> logger, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(CheckForNotCompletedFromProjectQuery request, CancellationToken cancellationToken)
    {
        try
        {
            bool result = await _taskRepository.CheckForNotCompletedFromProject(request.Id);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
        }
    }
}