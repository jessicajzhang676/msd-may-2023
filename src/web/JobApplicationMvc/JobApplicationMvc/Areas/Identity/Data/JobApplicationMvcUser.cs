using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace JobApplicationMvc.Areas.Identity.Data;

// Add profile data for application users by adding properties to the JobApplicationMvcUser class
public class JobApplicationMvcUser : IdentityUser
{
    [PersonalData]
    public string FirstName { get; set; } = string.Empty;
    [PersonalData]
    public string LastName { get; set; } = string.Empty;
    
    [PersonalData]
    public DateTimeOffset DateOfBirth { get; set; }
    
}

