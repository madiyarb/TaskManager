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

namespace ProjectManager.Application.Features.Edit;

public class EditCommandHandler : IRequestHandler<EditProjectCommand, Result<string>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IValidator<EditProjectCommand> _validator;
    private readonly IMapper _mapper;
    private readonly ILogger<EditCommandHandler> _logger;

    public EditCommandHandler(IProjectRepository projectRepository, IValidator<EditProjectCommand> validator, IMapper mapper, ILogger<EditCommandHandler> logger)
    {
        _projectRepository = projectRepository;
        _validator = validator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(EditProjectCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return Result.Invalid(validationResult.AsErrors());
        try
        {
            ProjectDbModel? project = await _projectRepository.GetFirstOrDefaultAsync(t=>t.Id==request.Id);
            if (project is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Project not found with id {request.Id}");
                return Result.NotFound($"{BussinesErrors.NotFound.ToString()}: Project not found with id {request.Id}");
            }
            project = _mapper.Map<ProjectDbModel>(request);
            await _projectRepository.UpdateAsync(project);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
            return Result.Error($"{BussinesErrors.UnknownError.ToString()}: {ex.Message}");
        }
    }
}