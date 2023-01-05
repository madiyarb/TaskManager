using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Application.Contracts;
using ProjectManager.Domain.Entities;
using ServiceContracts.Project.Commands;
using ServiceContracts.Project.Models;
using ServiceContracts.Task.Commands;
using ServiceContracts.Task.Models;
using TaskManager.Application.Contracts;
using TaskManager.Domain.Complementary;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Features.Create;

public class CreateCommandHandler : IRequestHandler<CreateTaskCommand, Result<TaskVm>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IValidator<CreateTaskCommand> _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCommandHandler> _logger;

    public CreateCommandHandler(ITaskRepository taskRepository, IValidator<CreateTaskCommand> validator, IMapper mapper, ILogger<CreateCommandHandler> logger)
    {
        _taskRepository = taskRepository;
        _validator = validator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<TaskVm>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return Result.Invalid(validationResult.AsErrors());
        try
        {
            if (request.Project is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Project not found with id {request.ProjectId}");
                return Result.NotFound($"{BussinesErrors.NotFound.ToString()}: Project not found with id {request.ProjectId}");
            }
            TaskDbModel task = _mapper.Map<TaskDbModel>(request);
            task.TaskState = TaskStateEnums.ToDo;
            var result = await _taskRepository.AddAsync(task);
            return Result.Success(_mapper.Map<TaskVm>(result));
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
        }
    }
}