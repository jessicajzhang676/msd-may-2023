using JobApplicationMvc.Adapters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JobApplicationMvc.Pages.Admin;

public class JobsModel : PageModel
{
    private readonly JobsApiAdapter _adapter;

    public List<JobItemModel> Items { get; set; } = new();

    [BindProperty]
    public JobItemCreateModel CreateModel { get; set; } = new();
    public JobsModel(JobsApiAdapter adapter)
    {
        _adapter = adapter;
    }

    public async Task OnGetAsync()
    {
        Items = await _adapter.GetJobsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _adapter.AddJobAsync(CreateModel);
        return RedirectToPage("/admin/jobs");
    }
}

