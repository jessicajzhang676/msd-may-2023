using JobListingsApi.Adapters;
using JobListingsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobListingsApi.Controllers;

public class JobListingsRpcController : ControllerBase
{
    private readonly JobsApiHttpAdapter _adapter;

    public JobListingsRpcController(JobsApiHttpAdapter adapter)
    {
        _adapter = adapter;
    }
    
    [HttpPost("job-listings-rpc/{job}/openings")]
    public async Task<ActionResult> AddJobListing([FromRoute] string job, [FromBody] JobListingCreateModel request)
    {
        // the "model" is good with our validation, however, the job might not exist.
        // we need to make a call to the other API to check to see if that job exists.
        // do all the other stuff, but first see if the job exists.

        var jobExists = await _adapter.JobExistsAsync(job);
        if (jobExists)
        {
            return Ok("That Job Exists");
        }
        else
        {
            return NotFound("No Job with that title exists");
        }
    }
}
