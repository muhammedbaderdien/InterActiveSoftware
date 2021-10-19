using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InteractiveSoftware.Assessment.Models;

namespace InteractiveSoftware.Assessment.Data
{
    public class InteractiveSoftwareAssessmentContext : DbContext
    {
        public InteractiveSoftwareAssessmentContext (DbContextOptions<InteractiveSoftwareAssessmentContext> options)
            : base(options)
        {
        }

        public DbSet<InteractiveSoftware.Assessment.Models.Company> Company { get; set; }

        public DbSet<InteractiveSoftware.Assessment.Models.Address> Address { get; set; }

        public DbSet<InteractiveSoftware.Assessment.Models.Contact> Contact { get; set; }
    }
}
