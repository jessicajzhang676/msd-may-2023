using Microsoft.AspNetCore.Mvc;

namespace JobsApi.Controllers;

[Route("jobs")]
[ApiController]
public class JobsController : ControllerBase
{
    private readonly JobManager _jobManager;

    public JobsController(JobManager jobManager)
    {
        _jobManager = jobManager;
    }

    [HttpPost]
    public async Task<ActionResult> CreateJob([FromBody] JobCreateItem request)
    {
        JobItemModel response = await _jobManager.GetJobForAsync(request);
        return StatusCode(201, response);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllJobs()
    {
        var data = new List<JobItemModel>()
        {
            new JobItemModel { Id="developer-1", Title="Software Developer 1", Description = "Entry Level Software Developer"},
            new JobItemModel { Id = "qa-1", Title="Software Quality Assurance 1", Description ="Entry level QA"}
        };
        var response = new CollectionResponse<JobItemModel>()
        {
            Data = data
        };
        return Ok(response);
    }
}