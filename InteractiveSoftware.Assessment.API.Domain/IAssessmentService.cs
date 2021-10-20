using InteractiveSoftware.Assessment.API.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InteractiveSoftware.Assessment.API.Domain
{
    public interface IAssessmentService
    {
	   Task<List<Company>> GetCompanies(CancellationToken cancellationToken);
    }
}
