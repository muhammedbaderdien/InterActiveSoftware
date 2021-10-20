using InteractiveSoftware.Assessment.API.Domain;
using InteractiveSoftware.Assessment.API.Domain.Models;
using InteractiveSoftware.Assessment.API.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace InteractiveSoftware.Assessment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {

	   private readonly InteractiveSoftwareAssessmentContext _context;

	   private readonly ILogger<CompaniesController> _logger;

	   public CompaniesController(ILogger<CompaniesController> logger, InteractiveSoftwareAssessmentContext context)
	   {
		  _logger = logger;
		  _context = context;

		  if (_context.Company.FirstOrDefault(x => x.Id == 1) == null)
		  {
			 var customer = new Company
			 {
				Id = 1,
				Name = "Interactive Software",
				RegistrationNumber = "TEST/12345",
				CompanyAddress = new Address
				{
				    Id = 1,
				    Line1 = "5 Test Avenue",
				    Line2 = "",
				    Suburb = "Discovery",
				    Province = "Gauteng",
				    Postcode = "1709"
				},
				CompanyContact = new Contact
				{
				    Id = 1,
				    FirstName = "Muhammed",
				    LastName = "Baderdien",
				    EmailAdress = "muhammedb@test.net",
				    PhoneNumber = "+27833331234"
				}
			 };

			 _context.Company.Add(customer);
			 _context.SaveChanges();
		  }
	   }

	   // GET: Companies
	   [HttpGet("Index")]
	   public Task<List<Company>> Index()
	   {
		  var interactiveSoftwareAssessmentContext = _context.Company.Include(c => c.CompanyAddress).Include(c => c.CompanyContact);
		  var companies = interactiveSoftwareAssessmentContext.ToList();
		  return Task.FromResult(companies);
	   }

	   // GET: Companies/Details/5
	   [HttpGet("Details/{id}")]
	   public async Task<Company> Details(int? id)
	   {
		  if (id == null)
		  {
			 return null;
		  }

		  var company = _context.Company
			 .Include(c => c.CompanyAddress)
			 .Include(c => c.CompanyContact)
			 .FirstOrDefault(m => m.Id == id);
		  if (company == null)
		  {
			 return null;
		  }

		  return company;
	   }


	   // POST: Companies/Create
	   // To protect from overposting attacks, enable the specific properties you want to bind to.
	   // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	   [HttpPost("Create")]
	   [ValidateAntiForgeryToken]
	   public async Task<Company> Create(Company company)
	   {
		  if (ModelState.IsValid)
		  {
			 _context.Add(company);
			 await _context.SaveChangesAsync();
			 return company;
		  }
		  else
		  {
			 return null;
		  }
	   }

	   // GET: Companies/Edit/5
	   [HttpGet("Edit/{id}")]
	   public async Task<Company> Edit(int? id)
	   {
		  if (id == null)
		  {
			 return null;
		  }

		  var company = _context.Company.Include(c => c.CompanyAddress).Include(c => c.CompanyContact).FirstOrDefault(x => x.Id == id);
		  if (company == null)
		  {
			 return null;
		  }
		  return company;
	   }

	   // POST: Companies/Edit/5
	   [HttpPost("Edit/{id}")]
	   public async Task<Company> Edit(int id, Company company)
	   {
		  if (id != company.Id)
		  {
			 return null;
		  }

		  if (ModelState.IsValid)
		  {
			 try
			 {
				_context.Update(company);
				await _context.SaveChangesAsync();
			 }
			 catch (DbUpdateConcurrencyException)
			 {
				if (!CompanyExists(company.Id))
				{
				    return null;
				}
				else
				{
				    throw;
				}
			 }
			 return company;
		  }
		  return null;
	   }

	   // GET: Companies/Delete/5
	   [HttpGet("Delete/{id}")]
	   public async Task<Company> Delete(int? id)
	   {
		  if (id == null)
		  {
			 return null;
		  }

		  var company = _context.Company
			 .Include(c => c.CompanyAddress)
			 .Include(c => c.CompanyContact)
			 .FirstOrDefault(m => m.Id == id);
		  if (company == null)
		  {
			 return null;
		  }

		  return company;
	   }

	   // POST: Companies/Delete/5
	   [HttpPost("Delete/{id}")]
	   public async Task<int> Delete(int id)
	   {
		  var company = await _context.Company.FindAsync(id);
		  _context.Company.Remove(company);
		  await _context.SaveChangesAsync();
		  return id;
	   }

	   private bool CompanyExists(int id)
	   {
		  return _context.Company.Any(e => e.Id == id);
	   }
    }
}
