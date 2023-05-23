using Marten;
using SlugGenerators;

namespace JobsApi.Controllers;

public class UniqueIdChecker : ICheckForUniqueValues

{
    private readonly IDocumentStore _documentStore;

    public UniqueIdChecker(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }
    public async Task<bool> IsUniqueAsync(string attempt)
    {
        var session = _documentStore.LightweightSession();
        var alreadyThere = await session.Query<JobEntity>().Where(j => j.Slug == attempt).AnyAsync();
        return !alreadyThere;
    }
}