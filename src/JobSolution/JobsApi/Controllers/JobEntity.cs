namespace JobsApi.Controllers
{
    public class JobEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Slug { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsRetired { get; set; } = false;
    }
}
