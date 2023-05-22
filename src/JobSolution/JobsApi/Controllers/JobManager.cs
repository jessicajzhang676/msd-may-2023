using SlugGenerators;

namespace JobsApi.Controllers;

public class JobManager
{
    private readonly SlugGenerator _slugGenerator;

    public JobManager(SlugGenerator slugGenerator)
    {
        _slugGenerator = slugGenerator;
    }

    public async Task<JobItemModel> GetJobForAsync(JobCreateItem request)
    {
        var response = new JobItemModel
        {
            Id = await _slugGenerator.GenerateSlugForAsync(request.Title),
            Title = request.Title,
            Description = request.Description,
        };
        return response;

    }
}