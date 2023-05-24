using System.ComponentModel.DataAnnotations;

namespace JobApplicationMvc.Adapters;

public class JobsApiAdapter
{
    private readonly HttpClient _httpClient;

    public JobsApiAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<JobItemModel>> GetJobsAsync()
    {
        var response = await _httpClient.GetAsync("/jobs");
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<CollectionResponse<JobItemModel>>();

        if(data is null)
        {
            data = new CollectionResponse<JobItemModel>();
        }
        return data.Data;
    }

    public async Task AddJobAsync(JobItemCreateModel request)
    {
        var response = await _httpClient.PostAsJsonAsync("/jobs", request);
        response.EnsureSuccessStatusCode();

    }
}

public record CollectionResponse<T>
{
    public List<T> Data { get; set; } = new();
}

public record JobItemModel
{
    public string Id { get; init; } = "";
    public string Title { get; init; } = "";
    public string Description { get; init; } = "";
}

public record JobItemCreateModel
{
    [Required]
    public string Title { get; init; } = "";
    [Required]
    public string Description { get; init; } = "";
}