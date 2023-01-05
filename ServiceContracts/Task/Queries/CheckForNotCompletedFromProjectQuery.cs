using Ardalis.Result;
using MediatR;

namespace ServiceContracts.Task.Queries;

public class CheckForNotCompletedFromProjectQuery : IRequest<Result<bool>>
{
    public int Id { get; set; }
}