namespace MessageContracts.WebMessages;

public record ApplicationCreated
{
    public static readonly string MessageId = "WebMessages.ApplicantCreated";
    public string UserId { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateTimeOffset DateOfBirth { get; set; }
    public string EmailAddress { get; set; } = "";
}

public record JobApplicationCreated
{
    public static readonly string MessageId = "WebMessages.JobApplicationCreated";
    public string ApplicantId { get; set; } = "";
    public string JobOfferingId { get; set; } = "";
}
