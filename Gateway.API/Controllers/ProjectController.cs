using DataTransferLib.Models;
using Gateway.API.Services.Inerfaces;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.Project.Commands;
using ServiceContracts.Project.Models;
using ServiceContracts.Project.Queries;
using ServiceContracts.Task.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Gateway.API.Controllers;

[Route("[controller]/[action]")]
public class ProjectController : Controller
{
    private readonly IProjectService _projectService;
    private readonly ITaskService _taskService;

    public ProjectController(IProjectService projectService, ITaskService taskService)
    {
        _projectService = projectService;
        _taskService = taskService;
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get project",
        Description = "Must be passed in the query string by id"
    )]
    public async Task<ActionResult<DefaultResponseObject<ProjectVm>>> Get([FromQuery] GetProjectQuery request)
    {
        var response = await _projectService.GetProject(request);
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create project",
        Description = "It is necessary to pass the name and description in the request body"
    )]
    public async Task<ActionResult<DefaultResponseObject<ProjectVm>>> Create([FromBody] CreateProjectCommand request)
    {
        var response = await _projectService.Create(request);
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Close the project",
        Description = "Set CompletionDate field" +
                      "Do not populate property(object) <<NotComletedTasks>>"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> Close([FromBody] CloseProjectCommand request)
    {
        var responseCheckTasks = await _taskService.CheckForNotCompletedFromProject
        (new CheckForNotCompletedFromProjectQuery
        {
            Id = request.Id
        });
        request.HaveNotComletedTasks = responseCheckTasks.Value;
        var response = await _projectService.Close(request);
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Edit the project",
        Description = "It is necessary to pass the name, state and description in the request body"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> Edit([FromBody] EditProjectCommand request)
    {
        var response = await _projectService.Edit(request);
        return Ok(response);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Project delete",
        Description = "It is necessary to pass the id in the request body"
    )]
    public async Task<ActionResult<DefaultResponseObject<string>>> Edit([FromBody] DeleteProjectCommand request)
    {
        var response = await _projectService.Delete(request);
        return Ok(response);
    }
}