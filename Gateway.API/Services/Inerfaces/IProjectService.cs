using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServiceContracts.Project.Commands;
using ServiceContracts.Project.Models;
using ServiceContracts.Project.Queries;
using ServiceContracts.Task.Models;

namespace Gateway.API.Services.Inerfaces;

public interface IProjectService : IUseExtendedHttpClient<IProjectService>
{
    Task<DefaultResponseObject<ProjectVm>> GetProject(GetProjectQuery request);
    Task<DefaultResponseObject<string>> Close(CloseProjectCommand request);
    Task<DefaultResponseObject<ProjectVm>> Create(CreateProjectCommand request);
    Task<DefaultResponseObject<string>> Edit(EditProjectCommand request);
}