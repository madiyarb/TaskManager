using DataTransferLib.Models;
using ExtendedHttpClient;
using Gateway.API.Services.Inerfaces;
using ServiceContracts.Project.Commands;
using ServiceContracts.Project.Models;
using ServiceContracts.Project.Queries;
using ServiceContracts.Task.Models;

namespace Gateway.API.Services;

public class ProjectService : IProjectService
{
    public ProjectService(ExtendedHttpClient<IProjectService> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }

    public ExtendedHttpClient<IProjectService> ExtendedHttpClient { get; set; }
    public async Task<DefaultResponseObject<ProjectVm>> GetProject(GetProjectQuery request)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<ProjectVm>>($"/Project/GetProject?Id={request.Id}");
    }

    public async Task<DefaultResponseObject<string>> Close(CloseProjectCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<CloseProjectCommand, DefaultResponseObject<string>>(request, $"/Project/Close");
    }

    public async Task<DefaultResponseObject<ProjectVm>> Create(CreateProjectCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<CreateProjectCommand, DefaultResponseObject<ProjectVm>>(request, $"/Project/Create");
    }
}