namespace JobApplicationMvc.Data;

public class JobOpeningEntity
{
    public int Id { get; set; }
    public Guid OpeningId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public decimal SalaryBottomRange { get; set; }
    public decimal SalaryTopRange { get; set; }
    public DateTimeOffset ListedOn { get; set; }
    
}