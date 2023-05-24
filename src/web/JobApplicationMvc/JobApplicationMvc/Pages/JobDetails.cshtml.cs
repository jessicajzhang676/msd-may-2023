using DotNetCore.CAP;
using JobApplicationMvc.Areas.Identity.Data;
using JobApplicationMvc.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DomainEvents = MessageContracts.WebMessages;

namespace JobApplicationMvc.Pages
{
    public class JobDetailsModel : PageModel
    {
        private readonly JobApplicationsDataContext _context;
        private readonly UserManager<JobApplicationMvcUser> _userManager;
        private readonly ICapPublisher _publisher;

        public JobDetailsModel(JobApplicationsDataContext context, UserManager<JobApplicationMvcUser> userManager, ICapPublisher publisher)
        {
            _context = context;
            _userManager = userManager;
            _publisher = publisher;
        }

        [BindProperty]
        public JobApplicationModel ApplicationModel { get; set; } = new();

        [BindProperty]
        public JobOpeningEntity? Job { get; set; }
        public JobApplicationMvcUser? Applicant { get; set; }
        [BindProperty]
        public string UserId { get; set; } = string.Empty;
        public string? Message { get; set; }
        public async Task OnGetAsync(int id)
        {
            var userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
            UserId = userId;
            var email = User?.Identity?.Name;
            Applicant = await _userManager.FindByIdAsync(userId);

            var opening = await _context.JobOpenings
                .Where(o => o.Id == id)
                .SingleOrDefaultAsync();
            if (opening is null)
            {
                Message = "Uh Oh! That Opening Doesn't Exist or has been filled!";
            }
            else
            {
                Job = opening;
                ApplicationModel.JobId = Job.OpeningId.ToString();
                ApplicationModel.UserId = Applicant.Id;

            }
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!;
            UserId = userId;
            var email = User?.Identity?.Name;
            Applicant = await _userManager.FindByIdAsync(userId);

            var opening = await _context.JobOpenings
            .Where(o => o.Id == id)
            .SingleOrDefaultAsync();
            if (opening is null)
            {
                Message = "Uh Oh! That Opening Doesn't Exist or has been filled!";
            }
            else
            {
                Job = opening;
                ApplicationModel.JobId = Job.OpeningId.ToString();
                ApplicationModel.UserId = Applicant.Id;
            }

            var eventToPublish = new DomainEvents.JobApplicationCreated
            {
                ApplicantId = UserId,
                JobOfferingId = opening.OpeningId.ToString()



            };
            await _publisher.PublishAsync(DomainEvents.JobApplicationCreated.MessageId, eventToPublish);
            return RedirectToPage("Index");
        }

    }
}
public record JobApplicationModel
{
    public string UserId { get; set; } = string.Empty;
    public string JobId { get; set; } = string.Empty;

}