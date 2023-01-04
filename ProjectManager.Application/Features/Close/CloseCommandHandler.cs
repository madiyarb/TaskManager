using Ardalis.Result;
using AutoMapper;
using CommonStructures;
using DataTransferLib.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using ProjectManager.Application.Contracts;
using ProjectManager.Application.Features.Create;
using ProjectManager.Domain.Entities;
using ServiceContracts.Project.Commands;

namespace ProjectManager.Application.Features.Close;

public class CloseCommandHandler : IRequestHandler<CloseProjectCommand, Result<DefaultResponseObject<string>>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CloseCommandHandler> _logger;

    public CloseCommandHandler(IProjectRepository projectRepository, IMapper mapper, ILogger<CloseCommandHandler> logger)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<DefaultResponseObject<string>>> Handle(CloseProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ProjectDbModel? project = await _projectRepository.GetFirstOrDefaultAsync(p=>p.Id==request.Id);
            if (project is null)
            {
                _logger.LogWarning($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Id} id");
                return Result.Error($"{BussinesErrors.NotFound.ToString()}: Not found with {request.Id} id");
            }
            project.CompletionDate = DateTime.Now;
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