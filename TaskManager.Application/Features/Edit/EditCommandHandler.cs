using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Application.Contracts;
using ProjectManager.Domain.Entities;
using ServiceContracts.Task.Commands;
using TaskManager.Application.Contracts;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Features.Edit;

public class EditCommandHandler : IRequestHandler<EditTaskCommand, Result<string>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IValidator<EditTaskCommand> _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<EditCommandHandler> _logger;

    public EditCommandHandler(ITaskRepository taskRepository, IValidator<EditTaskCommand> validator, IMapper mapper, ILogger<EditCommandHandler> logger)
    {
        _taskRepository = taskRepository;
        _validator = validator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(EditTaskCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return Result.Invalid(validationResult.AsErrors());
        try
        {
            TaskDbModel? task = await _taskRepository.GetFirstOrDefaultAsync(t=>t.Id==request.Id);
            if (task is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Task not found with id {request.Id}");
                return Result.NotFound($"{BussinesErrors.NotFound.ToString()}: Task not found with id {request.Id}");
            }
            if (request.Project is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Project not found with id {request.ProjectId}");
                return Result.NotFound($"{BussinesErrors.NotFound.ToString()}: Project not found with id {request.ProjectId}");
            }
            task = _mapper.Map<TaskDbModel>(request);
            await _taskRepository.UpdateAsync(task);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
        }
    }
}