using DotNetCore.CAP;
using JobListingsApi.Models;
using Marten;
using Microsoft.AspNetCore.Mvc;
using DomainEvents = MessageContracts.JobListingsApi;

namespace JobListingsApi.Controllers;

public class JobListingsController : ControllerBase
{
    private readonly IDocumentSession _documentSession;
    private readonly ICapPublisher _publisher;

    public JobListingsController(IDocumentSession documentSession, ICapPublisher publisher)
    {
        _documentSession = documentSession;
        _publisher = publisher;
    }

    [HttpPost("/job-listings/{slug}/openings")]
    public async Task<ActionResult> AddJobListing([FromRoute] string slug, [FromBody] JobListingCreateModel request)
    {
        var savedJob = await _documentSession.Query<JobModel>().Where(job => job.Id == slug).FirstOrDefaultAsync();

        if (savedJob != null)
        {
            var jobToAdd = new JobListingEntity
            {
                JobId = slug,
                JobName = savedJob.Title,
                OpeningStartDate = request.OpeningStartDate,
                SalaryRange = request.SalaryRange,

            };
            _documentSession.Insert(jobToAdd);
            await _documentSession.SaveChangesAsync();

            // publish the event that there is a new job listing;
            var domainEvent = new DomainEvents.JobListingCreated
            {
                Id = jobToAdd.Id.ToString(),
                JobId = jobToAdd.JobId,
                JobName = jobToAdd.JobName,
                OpeningStartDate = jobToAdd.OpeningStartDate,
                SalaryRange = new DomainEvents.Salaryrange
                {
                    Min = jobToAdd.SalaryRange.Min.Value,
                    Max = jobToAdd.SalaryRange.Max.Value
                }
            };
            await _publisher.PublishAsync(DomainEvents.JobListingCreated.MessageId, domainEvent);
            return StatusCode(201, jobToAdd);

        }
        else
        {
            return NotFound();
        }

    }
}