namespace MessageContracts.JobsApi;

// Job is Created
public record JobCreated
{
    public static readonly string MessageId = "JobsApi.JobCreated";
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}

// Job is Retired
public record JobRetired
{
    public static readonly string MessageId = "JobsApi.JobRetired";
    public string Id { get; set; } = "";
}

