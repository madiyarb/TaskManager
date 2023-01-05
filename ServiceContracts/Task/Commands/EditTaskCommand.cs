using Ardalis.Result;
using MediatR;
using ServiceContracts.Project.Models;

namespace ServiceContracts.Task.Commands;

public class EditTaskCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
    public int Priority { get; set; }
    public ProjectVm? Project { get; set; }
}