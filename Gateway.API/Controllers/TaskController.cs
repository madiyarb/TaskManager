using DataTransferLib.Models;
using Gateway.API.Services.Inerfaces;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Project.Queries;
using ServiceContracts.Task.Commands;
using ServiceContracts.Task.Models;
using ServiceContracts.Task.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.API.Controllers;

public class TaskController : Controller
{
    private readonly IProjectService _projectService;
    private readonly ITaskService _taskService;

    public TaskController(IProjectService projectService, ITaskService taskService)
    {
        _projectService = projectService;
        _taskService = taskService;
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Task creation",
        Description = "It is necessary to pass the name, description, projectid, priority in the request body" +
                      "Do not populate property(object) <<project>>"
    )]
    public async Task<ActionResult<DefaultResponseObject<TaskVm>>> Create([FromBody] CreateTaskCommand request)
    {
        var projectResponse = await _projectService.GetProject(new GetProjectQuery
        {
            Id = request.ProjectId
        });
        request.Project = projectResponse.Value;
        var response = await _taskService.Create(request);
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Task edit",
        Description = "It is necessary to pass the name, description, projectid, priority in the request body" +
                      "Do not populate property(object) <<project>>"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> Edit([FromBody] EditTaskCommand request)
    {
        var projectResponse = await _projectService.GetProject(new GetProjectQuery
        {
            Id = request.ProjectId
        });
        request.Project = projectResponse.Value;
        var response = await _taskService.Edit(request);
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Task delete",
        Description = "It is necessary to pass the id in the request body"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> Edit([FromBody] DeleteTaskCommand request)
    {
        var response = await _taskService.Delete(request);
        return Ok(response);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Check for not completed tasks from project",
        Description = "need to pass the id in the query string"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> CheckForNotCompletedFromProject([FromQuery] DeleteTaskCommand request)
    {
        var response = await _taskService.Delete(request);
        return Ok(response);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get project by id",
        Description = "Must be passed in the query string by id"
    )]
    public async Task<ActionResult<DefaultResponseObject<AllTasksVm>>> GetAll([FromQuery] GetAllTasksQuery request)
    {
        var response = await _taskService.GetAll(request);
        return Ok(response);
    }
}