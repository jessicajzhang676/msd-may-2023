using System.ComponentModel.DataAnnotations;

namespace JobApplicationMvc.Adapters;

public class JobOpeningsApiAdapter
{
    private readonly HttpClient _client;

    public JobOpeningsApiAdapter(HttpClient client)
    {
        _client = client;
    }

    public async Task AddJobAsync(JobOpeningCreateModel request)
    {
        var requestData = new JobListingCreateModel
        {
            OpeningStartDate = request.OpeningStartDate,
            SalaryRange = new SalaryRangeModel
            {
                Min = request.MinimumSalary,
                Max = request.MaximumSalary,
            }
        };

        // /job-listings/{slug}/openings
        var response = await _client.PostAsJsonAsync($"/job-listings/{request.JobId}/openings", requestData);
        response.EnsureSuccessStatusCode();
    }
}

public record JobOpeningCreateModel
{
    public string JobId { get; set; } = "";
    public string OpeningStartDate { get; set; } = "";
    public decimal MinimumSalary { get; set; }
    public decimal MaximumSalary { get; set; }
}

public record JobListingCreateModel
{
    public string OpeningStartDate { get; set; } = "";
    public SalaryRangeModel SalaryRange { get; set; } = new();


}

public record SalaryRangeModel
{
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
}
