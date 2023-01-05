using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServiceContracts.Task.Commands;
using ServiceContracts.Task.Models;
using ServiceContracts.Task.Queries;

namespace Gateway.API.Services.Inerfaces;

public interface ITaskService : IUseExtendedHttpClient<IProjectService>
{
    Task<DefaultResponseObject<bool>> CheckForNotCompletedFromProject(CheckForNotCompletedFromProjectQuery request);
    Task<DefaultResponseObject<TaskVm>> Create(CreateTaskCommand request);
    Task<DefaultResponseObject<string>> Delete(DeleteTaskCommand request);
    Task<DefaultResponseObject<string>> Edit(EditTaskCommand request);
    Task<DefaultResponseObject<AllTasksVm>> GetAll(GetAllTasksQuery request);
}