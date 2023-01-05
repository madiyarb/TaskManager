using Ardalis.Result;
using MediatR;

namespace ServiceContracts.Project.Queries;

public class DeleteProjectCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
}