using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ServiceContracts.Task.Models;
using ServiceContracts.Task.Queries;
using TaskManager.Application.Contracts;

namespace TaskManager.Application.Features.GetAll;

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, Result<AllTasksVm>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IValidator<GetAllTasksQuery> _validator;
    private readonly ILogger<GetAllTasksQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetAllTasksQueryHandler(ITaskRepository taskRepository, IValidator<GetAllTasksQuery> validator, ILogger<GetAllTasksQueryHandler> logger, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _validator = validator;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<AllTasksVm>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return Result.Invalid(validationResult.AsErrors());

        try
        {
            var usersCount = _taskRepository.GetCountAsync();
            if (request.PageSize == 0)
            {
                request.PageNumber = 1;
                request.PageSize = await usersCount;
            }

            if (await usersCount == 0)
            {
                _logger.LogWarning($"{BussinesErrors.ListIsEmpty.ToString()}: Request list is empty");
                return Result.Error($"{BussinesErrors.ListIsEmpty.ToString()}: Request list is empty");
            }

            var data = await _taskRepository.GetFilteredBatchOfData(request.PageSize, request.PageNumber);

            var response = new AllTasksVm()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Tasks = _mapper.Map<List<TaskVm>>(data)
            };
            return Result.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
        }
    }
}