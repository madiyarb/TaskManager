using Ardalis.Result;
using MediatR;

namespace ServiceContracts.Project.Queries;

public class CheckForNotCompletedFromProjectQuery : IRequest<Result<bool>>
{
    public int Id { get; set; }
}