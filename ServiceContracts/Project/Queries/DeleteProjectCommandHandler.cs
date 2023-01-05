using Ardalis.Result;
using MediatR;

namespace ServiceContracts.Project.Queries;

public class DeleteProjectCommandHandler : IRequest<Result<string>>
{
    public int Id { get; set; }
}