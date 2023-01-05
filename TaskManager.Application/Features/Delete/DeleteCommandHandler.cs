using Ardalis.Result;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServiceContracts.Task.Commands;
using TaskManager.Application.Contracts;
using TaskManager.Application.Features.Edit;
using TaskManager.Domain.Complementary;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Features.Delete;

public class DeleteCommandHandler : IRequestHandler<DeleteTaskCommand, Result<string>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteCommandHandler> _logger;


    public DeleteCommandHandler(ITaskRepository taskRepository, IMapper mapper, ILogger<DeleteCommandHandler> logger)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            TaskDbModel? task = await _taskRepository.GetFirstOrDefaultAsync(t => t.Id == request.Id);
            if (task is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Task not found with id {request.Id}");
                return Result.NotFound($"{BussinesErrors.NotFound.ToString()}: Task not found with id {request.Id}");
            }

            if (task.TaskState != TaskStateEnums.Done)
            {
                _logger.LogWarning($"{BussinesErrors.StateNotDone.ToString()}: Task {task.Name} state is not done. Id: {task.Id}");
                return Result.NotFound($"{BussinesErrors.StateNotDone.ToString()}: Task {task.Name} state is not done. Id: {task.Id}");
            }

            await _taskRepository.DeleteAsync(task);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
        }
    }
}