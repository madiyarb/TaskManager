using Ardalis.Result;
using MediatR;
using ServiceContracts.Project.Models;
using ServiceContracts.Task.Models;

namespace ServiceContracts.Task.Commands;

public class CreateTaskCommand : IRequest<Result<TaskVm>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
    public int Priority { get; set; }
    public ProjectVm? Project { get; set; }
}