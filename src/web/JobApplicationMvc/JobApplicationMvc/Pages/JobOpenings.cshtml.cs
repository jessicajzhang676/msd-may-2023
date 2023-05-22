using JobApplicationMvc.Areas.Identity.Data;
using JobApplicationMvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationMvc.Pages
{
    public class JobOpeningsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly JobApplicationsDataContext _context;

        public JobOpeningsModel(ILogger<IndexModel> logger, JobApplicationsDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<JobOpeningEntity> Openings { get; set; }
        public async Task OnGetAsync()
        {
            Openings = await _context.JobOpenings
                .OrderByDescending(j => j.ListedOn)
                .ToListAsync();
        }
    }
}
