using JobApplicationMvc.Adapters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobApplicationMvc.Pages.Admin;

public class OpeningsModel : PageModel
{
    private readonly JobsApiAdapter _jobsAdapter;
    private readonly JobOpeningsApiAdapter _jobOpeningsAdapter;

    [BindProperty]
    public JobOpeningCreateModel JobOpeningModel { get; set; } = new();
    public List<JobItemModel> Jobs { get; set; } = new();
    public OpeningsModel(JobsApiAdapter jobsAdapter, JobOpeningsApiAdapter jobOpeningsAdapter)
    {
        _jobsAdapter = jobsAdapter;
        _jobOpeningsAdapter = jobOpeningsAdapter;
    }

    public async Task OnGetAsync()
    {
        Jobs = await _jobsAdapter.GetJobsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _jobOpeningsAdapter.AddJobAsync(JobOpeningModel);
        ViewData["message"] = $"Added Opening for {JobOpeningModel.JobId}";
        return RedirectToPage("/admin/openings");
    }
}
