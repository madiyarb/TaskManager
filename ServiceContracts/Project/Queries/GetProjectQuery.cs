using Ardalis.Result;
using MediatR;
using ServiceContracts.Project.Models;

namespace ServiceContracts.Project.Queries;

public class GetProjectQuery : IRequest<Result<ProjectVm>>
{
    public int Id { get; set; }
}