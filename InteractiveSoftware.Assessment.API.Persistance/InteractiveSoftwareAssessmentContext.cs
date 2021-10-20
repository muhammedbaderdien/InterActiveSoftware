using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InteractiveSoftware.Assessment.API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InteractiveSoftware.Assessment.API.Persistance
{
    public class InteractiveSoftwareAssessmentContext : DbContext
    {
	   public InteractiveSoftwareAssessmentContext(DbContextOptions<InteractiveSoftwareAssessmentContext> options)
		  : base(options)
	   {
	   }

	   public DbSet<Company> Company { get; set; }

	   public DbSet<Address> Address { get; set; }

	   public DbSet<Contact> Contact { get; set; }
    }
}
