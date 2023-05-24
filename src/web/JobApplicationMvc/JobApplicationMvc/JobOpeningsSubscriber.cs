using DotNetCore.CAP;
using JobApplicationMvc.Areas.Identity.Data;
using JobApplicationMvc.Data;
using DomainEvents = MessageContracts.JobListingsApi;

namespace JobApplicationMvc;

public class JobOpeningsSubscriber : ICapSubscribe
{
    private readonly JobApplicationsDataContext _context;

    public JobOpeningsSubscriber(JobApplicationsDataContext context)
    {
        _context = context;
    }

    [CapSubscribe("JobListings.JobListingCreated")]
    public async Task AddJobListingAsync(DomainEvents.JobListingCreated jobListingMessage)
    {
        var jobOpening = new JobOpeningEntity
        {
            Title = jobListingMessage.JobName,
            Department = "Any",
            Description = jobListingMessage.JobId,
            OpeningId = Guid.Parse(jobListingMessage.Id),
            ListedOn = new DateTimeOffset(DateTime.Now.ToUniversalTime()),
            SalaryBottomRange = jobListingMessage.SalaryRange.Min,
            SalaryTopRange = jobListingMessage.SalaryRange.Max,
        };
        _context.JobOpenings.Add(jobOpening);
        await _context.SaveChangesAsync();
    }
}
