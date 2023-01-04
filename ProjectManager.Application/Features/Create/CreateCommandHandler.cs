using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using CommonStructures;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Application.Contracts;
using ProjectManager.Domain.Complementary;
using ProjectManager.Domain.Entities;
using ServiceContracts.Project.Commands;
using ServiceContracts.Project.Models;

namespace ProjectManager.Application.Features.Create;

public class CreateCommandHandler : IRequestHandler<CreateProjectCommand, Result<ProjectVm>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IValidator<CreateProjectCommand> _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCommandHandler> _logger;

    public CreateCommandHandler(IProjectRepository projectRepository, IValidator<CreateProjectCommand> validator, IMapper mapper, ILogger<CreateCommandHandler> logger)
    {
        _projectRepository = projectRepository;
        _validator = validator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<ProjectVm>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return Result.Invalid(validationResult.AsErrors());
        try
        {
            ProjectDbModel project = _mapper.Map<ProjectDbModel>(request);
            project.State = StateEnums.NotStarted;
            var result = await _projectRepository.AddAsync(project);
            return Result.Success(_mapper.Map<ProjectVm>(result));
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
        }
    }
}