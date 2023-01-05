using Ardalis.Result;
using MediatR;
using ServiceContracts.Task.Models;

namespace ServiceContracts.Task.Queries;

public class GetAllTasksFromProjectQuery : IRequest<Result<AllTasksVm>>
{
    public int ProjectId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}