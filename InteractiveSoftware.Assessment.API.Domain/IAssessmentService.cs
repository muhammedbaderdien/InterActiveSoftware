using InteractiveSoftware.Assessment.API.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InteractiveSoftware.Assessment.API.Domain
{
    public interface IAssessmentService
    {
	   Task<List<Company>> GetCompanies();
	   Task<Company> GetCompanyById(int id);
	   Task<Company> CreateCompany(Company company);
	   Task<Company> UpdateCompany(Company company);
	   Task<int> DeleteCompany(int id);

	   Task<List<Address>> GetAddresses();
	   Task<Address> GetAddressById(int id);
	   Task<Address> CreateAddress(Address address);
	   Task<Address> UpdateAddress(Address address);
	   Task<int> DeleteAddress(int id);

	   Task<List<Contact>> GetContacts();
	   Task<Contact> GetContactById(int id);
	   Task<Contact> CreateContact(Contact contact);
	   Task<Contact> UpdateContact(Contact contact);
	   Task<int> DeleteContact(int id);
    }
}
