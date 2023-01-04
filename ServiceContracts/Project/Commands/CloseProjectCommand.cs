using Ardalis.Result;
using DataTransferLib.Models;
using MediatR;

namespace ServiceContracts.Project.Commands;

public class CloseProjectCommand : IRequest<Result<DefaultResponseObject<string>>>
{
    public int Id { get; set; }
}