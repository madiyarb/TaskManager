using Ardalis.Result;
using MediatR;
using ServiceContracts.Task.Models;

namespace ServiceContracts.Task.Queries;

public class GetAllTasksQuery : IRequest<Result<AllTasksVm>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string FilterString { get; set; }
}