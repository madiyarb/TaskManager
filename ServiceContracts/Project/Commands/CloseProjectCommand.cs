using Ardalis.Result;
using DataTransferLib.Models;
using MediatR;
using TaskManager.Domain.Entities;

namespace ServiceContracts.Project.Commands;

public class CloseProjectCommand : IRequest<Result<DefaultResponseObject<string>>>
{
    public int Id { get; set; }
    public bool HaveNotComletedTasks { get; set; }
}