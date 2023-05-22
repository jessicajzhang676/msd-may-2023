using SlugGenerators;

namespace JobsApi.Controllers;

public class UniqueIdChecker : ICheckForUniqueValues

{
    public Task<bool> IsUniqueAsync(string attempt)
    {
        // TODO: check the database, make sure it isn't used...
        return Task.FromResult(true);
    }
}