using JobApplicationMvc.Data;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationMvc.Areas.Identity.Data;

public class JobApplicationsDataContext :DbContext
{
    public DbSet<JobOpeningEntity> JobOpenings { get; set; }

    public JobApplicationsDataContext(DbContextOptions<JobApplicationsDataContext> options):base(options)
    {
        
    }
}
