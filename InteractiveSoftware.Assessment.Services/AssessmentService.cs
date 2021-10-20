using InteractiveSoftware.Assessment.API.Domain;
using InteractiveSoftware.Assessment.API.Domain.Infrastructure;
using InteractiveSoftware.Assessment.API.Domain.Models;
using InteractiveSoftware.Assessment.Services.ServiceClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Juta.WebAPI.Service
{
    public class AssessmentService : IAssessmentService
    {
	   private readonly IOptions<SettingsOptions> _options;

	   //private const string getSuburbsUrlOld = "api/suburbs/?q={0}";
	   private const string getCompaniesUrl = "api/companies/Index";
	   private readonly ClientAuthOptions apiKeyAuthentication;

	   public AssessmentService(IOptions<SettingsOptions> options)
	   {
		  _options = options;

		  if (string.IsNullOrWhiteSpace(_options.Value.AssessmentServiceBaseUrl))
		  {
			 throw new ArgumentOutOfRangeException(nameof(_options.Value.AssessmentServiceBaseUrl));
		  }

	   }

	   public async Task<List<Company>> GetCompanies(CancellationToken cancellationToken)
	   {
		  var companies = new List<Company>();
		  try
		  {
			 using (var serviceClient = new RestServiceClient(_options.Value.AssessmentServiceBaseUrl))
			 {
				var urlQueryString = string.Format(getCompaniesUrl);
				var rawResponse = await serviceClient.SendRequestAsync(urlQueryString);
				companies = JsonConvert.DeserializeObject<List<Company>>(rawResponse);
			 }

			 return companies.ToList();

		  }
		  catch (Exception ex)
		  {
			 Log.Error($"Companies API - error details: { ex.Message}\r\n\r\nStack Trace:\r\n{ ex}");
			 return new List<Company>();
		  }
	   }



	   private static Task<T> RunAsync<T>(Func<T> operation)
	   {
		  var tcs = new TaskCompletionSource<T>();

		  Task.Factory.StartNew(() =>
		  {
			 try
			 {
				var result = operation();

				tcs.SetResult(result);
			 }
			 catch (Exception ex)
			 {
				tcs.SetException(ex);
			 }
		  });

		  return tcs.Task;
	   }
    }
}
