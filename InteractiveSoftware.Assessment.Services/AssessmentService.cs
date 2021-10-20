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
	   private const string ApplicationJsonContentType = "application/json";

	   private const string getCompaniesUrl = "api/companies/Index";
	   private const string getCompanyUrl = "api/companies/Details/{0}";
	   private const string editCompanyUrl = "api/companies/Edit/{0}";
	   private const string deleteCompanyUrl = "api/companies/Delete/{0}";
	   private const string createCompanyUrl = "api/companies/Create";

	   private const string getAddressesUrl = "api/addresses/Index";
	   private const string getAddressUrl = "api/addresses/Details/{0}";
	   private const string editAddressUrl = "api/addresses/Edit/{0}";
	   private const string deleteAddressUrl = "api/addresses/Delete/{0}";
	   private const string createAddressUrl = "api/addresses/Create";

	   private const string getContactsUrl = "api/contacts/Index";
	   private const string getContactUrl = "api/contacts/Details/{0}";
	   private const string editContactUrl = "api/contacts/Edit/{0}";
	   private const string deleteContactUrl = "api/contacts/Delete/{0}";
	   private const string createContactUrl = "api/contacts/Create";

	   public AssessmentService(IOptions<SettingsOptions> options)
	   {
		  _options = options;

		  if (string.IsNullOrWhiteSpace(_options.Value.AssessmentServiceBaseUrl))
		  {
			 throw new ArgumentOutOfRangeException(nameof(_options.Value.AssessmentServiceBaseUrl));
		  }

	   }

	   /////////////////////////////////////////////////////

	   public async Task<List<Company>> GetCompanies()
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


	   public async Task<Company> GetCompanyById(int id)
	   {
		  var company = new Company();
		  try
		  {
			 using (var serviceClient = new RestServiceClient(_options.Value.AssessmentServiceBaseUrl))
			 {
				var urlQueryString = string.Format(getCompanyUrl, id);
				var rawResponse = await serviceClient.SendRequestAsync(urlQueryString);
				company = JsonConvert.DeserializeObject<Company>(rawResponse);
			 }

			 return company;

		  }
		  catch (Exception ex)
		  {
			 Log.Error($"Companies API - error details: { ex.Message}\r\n\r\nStack Trace:\r\n{ ex}");
			 return new Company();
		  }
	   }

	   public async Task<Company> UpdateCompany(Company company)
	   {
		  var editCompanyResult = new Company();
		  try
		  {

			 var companyRequestJson = JsonConvert.SerializeObject(company);

			 var payload = new RestServiceClient.StringPayload(companyRequestJson, ApplicationJsonContentType, Encoding.UTF8);

			 using (var serviceClient = new RestServiceClient(_options.Value.AssessmentServiceBaseUrl))
			 {
				var urlQueryString = string.Format(editCompanyUrl, company.Id);

				var rawResponse = await serviceClient.SendRequestAsync(urlQueryString, RestServiceClient.RequestMethod.POST, payload);
				editCompanyResult = JsonConvert.DeserializeObject<Company>(rawResponse);
			 }

			 return editCompanyResult;

		  }
		  catch (Exception ex)
		  {
			 Log.Error($"Companies API - error details: { ex.Message}\r\n\r\nStack Trace:\r\n{ ex}");
			 return new Company();
		  }
	   }

	   public async Task<int> DeleteCompany(int id)
	   {
		  int deleteCompanyResult;
		  try
		  {

			 using (var serviceClient = new RestServiceClient(_options.Value.AssessmentServiceBaseUrl))
			 {
				var urlQueryString = string.Format(deleteCompanyUrl, id);

				var rawResponse = await serviceClient.SendRequestAsync(urlQueryString, RestServiceClient.RequestMethod.POST);
				deleteCompanyResult = JsonConvert.DeserializeObject<int>(rawResponse);
			 }

			 return deleteCompanyResult;

		  }
		  catch (Exception ex)
		  {
			 Log.Error($"Companies API - error details: { ex.Message}\r\n\r\nStack Trace:\r\n{ ex}");
			 return 0;
		  }
	   }

	   
	   public async Task<Company> CreateCompany(Company company)
	   {
		  var createCompanyResult = new Company();
		  try
		  {

			 var companyRequestJson = JsonConvert.SerializeObject(company);

			 var payload = new RestServiceClient.StringPayload(companyRequestJson, ApplicationJsonContentType, Encoding.UTF8);

			 using (var serviceClient = new RestServiceClient(_options.Value.AssessmentServiceBaseUrl))
			 {
				var urlQueryString = string.Format(createCompanyUrl);

				var rawResponse = await serviceClient.SendRequestAsync(urlQueryString, RestServiceClient.RequestMethod.POST, payload);
				createCompanyResult = JsonConvert.DeserializeObject<Company>(rawResponse);
			 }

			 return createCompanyResult;

		  }
		  catch (Exception ex)
		  {
			 Log.Error($"Companies API - error details: { ex.Message}\r\n\r\nStack Trace:\r\n{ ex}");
			 return new Company();
		  }
	   }

	   /////////////////////////////////////////////////////

	   public async Task<List<Address>> GetAddresses()
	   {
		  var companies = new List<Address>();
		  try
		  {
			 using (var serviceClient = new RestServiceClient(_options.Value.AssessmentServiceBaseUrl))
			 {
				var urlQueryString = string.Format(getAddressesUrl);
				var rawResponse = await serviceClient.SendRequestAsync(urlQueryString);
				companies = JsonConvert.DeserializeObject<List<Address>>(rawResponse);
			 }

			 return companies.ToList();

		  }
		  catch (Exception ex)
		  {
			 Log.Error($"Address API - error details: { ex.Message}\r\n\r\nStack Trace:\r\n{ ex}");
			 return new List<Address>();
		  }
	   }


	   public async Task<Address> GetAddressById(int id)
	   {
		  var company = new Address();
		  try
		  {
			 using (var serviceClient = new RestServiceClient(_options.Value.AssessmentServiceBaseUrl))
			 {
				var urlQueryString = string.Format(getAddressUrl, id);
				var rawResponse = await serviceClient.SendRequestAsync(urlQueryString);
				company = JsonConvert.DeserializeObject<Address>(rawResponse);
			 }

			 return company;

		  }
		  catch (Exception ex)
		  {
			 Log.Error($"Address API - error details: { ex.Message}\r\n\r\nStack Trace:\r\n{ ex}");
			 return new Address();
		  }
	   }

	   public async Task<Address> UpdateAddress(Address company)
	   {
		  var editAddressResult = new Address();
		  try
		  {

			 var companyRequestJson = JsonConvert.SerializeObject(company);

			 var payload = new RestServiceClient.StringPayload(companyRequestJson, ApplicationJsonContentType, Encoding.UTF8);

			 using (var serviceClient = new RestServiceClient(_options.Value.AssessmentServiceBaseUrl))
			 {
				var urlQueryString = string.Format(editAddressUrl, company.Id);

				var rawResponse = await serviceClient.SendRequestAsync(urlQueryString, RestServiceClient.RequestMethod.POST, payload);
				editAddressResult = JsonConvert.DeserializeObject<Address>(rawResponse);
			 }

			 return editAddressResult;

		  }
		  catch (Exception ex)
		  {
			 Log.Error($"Address API - error details: { ex.Message}\r\n\r\nStack Trace:\r\n{ ex}");
			 return new Address();
		  }
	   }

	   public async Task<int> DeleteAddress(int id)
	   {
		  int deleteAddressResult;
		  try
		  {

			 using (var serviceClient = new RestServiceClient(_options.Value.AssessmentServiceBaseUrl))
			 {
				var urlQueryString = string.Format(deleteAddressUrl, id);

				var rawResponse = await serviceClient.SendRequestAsync(urlQueryString, RestServiceClient.RequestMethod.POST);
				deleteAddressResult = JsonConvert.DeserializeObject<int>(rawResponse);
			 }

			 return deleteAddressResult;

		  }
		  catch (Exception ex)
		  {
			 Log.Error($"Address API - error details: { ex.Message}\r\n\r\nStack Trace:\r\n{ ex}");
			 return 0;
		  }
	   }


	   public async Task<Address> CreateAddress(Address company)
	   {
		  var createAddressResult = new Address();
		  try
		  {

			 var companyRequestJson = JsonConvert.SerializeObject(company);

			 var payload = new RestServiceClient.StringPayload(companyRequestJson, ApplicationJsonContentType, Encoding.UTF8);

			 using (var serviceClient = new RestServiceClient(_options.Value.AssessmentServiceBaseUrl))
			 {
				var urlQueryString = string.Format(createAddressUrl);

				var rawResponse = await serviceClient.SendRequestAsync(urlQueryString, RestServiceClient.RequestMethod.POST, payload);
				createAddressResult = JsonConvert.DeserializeObject<Address>(rawResponse);
			 }

			 return createAddressResult;

		  }
		  catch (Exception ex)
		  {
			 Log.Error($"Address API - error details: { ex.Message}\r\n\r\nStack Trace:\r\n{ ex}");
			 return new Address();
		  }
	   }

	   /////////////////////////////////////////////////////

	   public async Task<List<Contact>> GetContacts()
	   {
		  var companies = new List<Contact>();
		  try
		  {
			 using (var serviceClient = new RestServiceClient(_options.Value.AssessmentServiceBaseUrl))
			 {
				var urlQueryString = string.Format(getContactsUrl);
				var rawResponse = await serviceClient.SendRequestAsync(urlQueryString);
				companies = JsonConvert.DeserializeObject<List<Contact>>(rawResponse);
			 }

			 return companies.ToList();

		  }
		  catch (Exception ex)
		  {
			 Log.Error($"Contact API - error details: { ex.Message}\r\n\r\nStack Trace:\r\n{ ex}");
			 return new List<Contact>();
		  }
	   }


	   public async Task<Contact> GetContactById(int id)
	   {
		  var company = new Contact();
		  try
		  {
			 using (var serviceClient = new RestServiceClient(_options.Value.AssessmentServiceBaseUrl))
			 {
				var urlQueryString = string.Format(getContactUrl, id);
				var rawResponse = await serviceClient.SendRequestAsync(urlQueryString);
				company = JsonConvert.DeserializeObject<Contact>(rawResponse);
			 }

			 return company;

		  }
		  catch (Exception ex)
		  {
			 Log.Error($"Contact API - error details: { ex.Message}\r\n\r\nStack Trace:\r\n{ ex}");
			 return new Contact();
		  }
	   }

	   public async Task<Contact> UpdateContact(Contact company)
	   {
		  var editContactResult = new Contact();
		  try
		  {

			 var companyRequestJson = JsonConvert.SerializeObject(company);

			 var payload = new RestServiceClient.StringPayload(companyRequestJson, ApplicationJsonContentType, Encoding.UTF8);

			 using (var serviceClient = new RestServiceClient(_options.Value.AssessmentServiceBaseUrl))
			 {
				var urlQueryString = string.Format(editContactUrl, company.Id);

				var rawResponse = await serviceClient.SendRequestAsync(urlQueryString, RestServiceClient.RequestMethod.POST, payload);
				editContactResult = JsonConvert.DeserializeObject<Contact>(rawResponse);
			 }

			 return editContactResult;

		  }
		  catch (Exception ex)
		  {
			 Log.Error($"Contact API - error details: { ex.Message}\r\n\r\nStack Trace:\r\n{ ex}");
			 return new Contact();
		  }
	   }

	   public async Task<int> DeleteContact(int id)
	   {
		  int deleteContactResult;
		  try
		  {

			 using (var serviceClient = new RestServiceClient(_options.Value.AssessmentServiceBaseUrl))
			 {
				var urlQueryString = string.Format(deleteContactUrl, id);

				var rawResponse = await serviceClient.SendRequestAsync(urlQueryString, RestServiceClient.RequestMethod.POST);
				deleteContactResult = JsonConvert.DeserializeObject<int>(rawResponse);
			 }

			 return deleteContactResult;

		  }
		  catch (Exception ex)
		  {
			 Log.Error($"Contact API - error details: { ex.Message}\r\n\r\nStack Trace:\r\n{ ex}");
			 return 0;
		  }
	   }


	   public async Task<Contact> CreateContact(Contact company)
	   {
		  var createContactResult = new Contact();
		  try
		  {

			 var companyRequestJson = JsonConvert.SerializeObject(company);

			 var payload = new RestServiceClient.StringPayload(companyRequestJson, ApplicationJsonContentType, Encoding.UTF8);

			 using (var serviceClient = new RestServiceClient(_options.Value.AssessmentServiceBaseUrl))
			 {
				var urlQueryString = string.Format(createContactUrl);

				var rawResponse = await serviceClient.SendRequestAsync(urlQueryString, RestServiceClient.RequestMethod.POST, payload);
				createContactResult = JsonConvert.DeserializeObject<Contact>(rawResponse);
			 }

			 return createContactResult;

		  }
		  catch (Exception ex)
		  {
			 Log.Error($"Contact API - error details: { ex.Message}\r\n\r\nStack Trace:\r\n{ ex}");
			 return new Contact();
		  }
	   }
    }
}
