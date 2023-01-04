using Ardalis.Result;
using MediatR;
using ServiceContracts.Project.Models;

namespace ServiceContracts.Project.Commands;

public class CreateProjectCommand : IRequest<Result<ProjectVm>>
{
    public string Name { get; set; }
    public string Description { get; set; }
}