using DataTransferLib.Models;
using ExtendedHttpClient;
using Gateway.API.Services.Inerfaces;
using ServiceContracts.Task.Commands;
using ServiceContracts.Task.Models;
using ServiceContracts.Task.Queries;

namespace Gateway.API.Services;

public class TaskService : ITaskService
{
    public TaskService(ExtendedHttpClient<IProjectService> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }

    public ExtendedHttpClient<IProjectService> ExtendedHttpClient { get; set; }


    public async Task<DefaultResponseObject<bool>> CheckForNotCompletedFromProject(CheckForNotCompletedFromProjectQuery request)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<bool>>($"/Project/CheckForNotCompletedFromProject?Id={request.Id}");
    }

    public async Task<DefaultResponseObject<TaskVm>> Create(CreateTaskCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<CreateTaskCommand, DefaultResponseObject<TaskVm>>(request, $"/Task/Create");
    }

    public async Task<DefaultResponseObject<string>> Delete(DeleteTaskCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<DeleteTaskCommand, DefaultResponseObject<string>>(request, $"/Task/Delete");
    }

    public async Task<DefaultResponseObject<string>> Edit(EditTaskCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<EditTaskCommand, DefaultResponseObject<string>>(request, $"/Task/Edit");
    }

    public async Task<DefaultResponseObject<AllTasksVm>> GetAll(GetAllTasksQuery request)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<AllTasksVm>>($"/Task/GetAll?PageNumber={request.PageNumber}&PageSize={request.PageSize}&FilterString={request.FilterString}");
    }
}