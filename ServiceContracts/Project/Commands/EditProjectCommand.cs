using Ardalis.Result;
using MediatR;
using ProjectManager.Domain.Complementary;

namespace ServiceContracts.Project.Commands;

public class EditProjectCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public StateEnums State { get; set; }
}