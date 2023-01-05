using Ardalis.Result;
using MediatR;

namespace ServiceContracts.Task.Commands;

public class DeleteTaskCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
}