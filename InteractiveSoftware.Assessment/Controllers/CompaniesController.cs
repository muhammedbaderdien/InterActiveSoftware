using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InteractiveSoftware.Assessment.Models;
using InteractiveSoftware.Assessment.API.Domain.Models;
using InteractiveSoftware.Assessment.API.Persistance;
using InteractiveSoftware.Assessment.API.Domain;

namespace InteractiveSoftware.Assessment.Controllers
{
    public class CompaniesController : Controller
    {
	   private readonly InteractiveSoftwareAssessmentContext _context;

	   private readonly IAssessmentService _assessmentService;

	   public CompaniesController(InteractiveSoftwareAssessmentContext context, IAssessmentService assessmentService)
	   {
		  _context = context;
		  _assessmentService = assessmentService;

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
	   
	   public async Task<IActionResult> Index()
	   {
		  var companies = await _assessmentService.GetCompanies();
		  return View(companies);
	   }

	   // GET: Companies/Details/5
	   public async Task<IActionResult> Details(int? id)
	   {
		  if (id == null)
		  {
			 return NotFound();
		  }

		  var company = await _assessmentService.GetCompanyById(id.Value);
		  
		  if (company == null)
		  {
			 return NotFound();
		  }

		  return View(company);
	   }

	   // GET: Companies/Create
	   public IActionResult Create()
	   {
		  ViewData["AddressId"] = new SelectList(_assessmentService.GetAddresses().GetAwaiter().GetResult(), "Id", "Line1");
		  ViewData["ContactId"] = new SelectList(_assessmentService.GetContacts().GetAwaiter().GetResult(), "Id", "EmailAdress");
		  return View();
	   }

	   // POST: Companies/Create
	   // To protect from overposting attacks, enable the specific properties you want to bind to.
	   // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	   [HttpPost]
	   [ValidateAntiForgeryToken]
	   public async Task<IActionResult> Create([Bind("Id,Name,RegistrationNumber,AddressId,ContactId")] Company company)
	   {
		  if (ModelState.IsValid)
		  {
			 await _assessmentService.CreateCompany(company);
			 return RedirectToAction(nameof(Index));
		  }
		  ViewData["AddressId"] = new SelectList(_assessmentService.GetAddresses().GetAwaiter().GetResult(), "Id", "Line1", company.AddressId);
		  ViewData["ContactId"] = new SelectList(_assessmentService.GetContacts().GetAwaiter().GetResult(), "Id", "EmailAdress", company.ContactId);
		  return View(company);
	   }

	   // GET: Companies/Edit/5
	   public async Task<IActionResult> Edit(int? id)
	   {
		  if (id == null)
		  {
			 return NotFound();
		  }

		  var company = await _assessmentService.GetCompanyById(id.Value);
		  if (company == null)
		  {
			 return NotFound();
		  }
		  ViewData["AddressId"] = new SelectList(_assessmentService.GetAddresses().GetAwaiter().GetResult(), "Id", "Line1", company.AddressId);
		  ViewData["ContactId"] = new SelectList(_assessmentService.GetContacts().GetAwaiter().GetResult(), "Id", "EmailAdress", company.ContactId);
		  return View(company);
	   }

	   // POST: Companies/Edit/5
	   // To protect from overposting attacks, enable the specific properties you want to bind to.
	   // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	   [HttpPost]
	   [ValidateAntiForgeryToken]
	   public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RegistrationNumber,AddressId,ContactId")] Company company)
	   {
		  if (id != company.Id)
		  {
			 return NotFound();
		  }
		  var updatedCompany = new Company();

		  if (ModelState.IsValid)
		  {
			 try
			 {
				updatedCompany = await _assessmentService.UpdateCompany(company);
			 }
			 catch (DbUpdateConcurrencyException)
			 {
				if (!CompanyExists(company.Id))
				{
				    return NotFound();
				}
				else
				{
				    throw;
				}
			 }
			 return RedirectToAction(nameof(Index));
		  }
		  ViewData["AddressId"] = new SelectList(_assessmentService.GetAddresses().GetAwaiter().GetResult(), "Id", "Line1", updatedCompany.AddressId);
		  ViewData["ContactId"] = new SelectList(_assessmentService.GetContacts().GetAwaiter().GetResult(), "Id", "EmailAdress", updatedCompany.ContactId);
		  return View(updatedCompany);
	   }

	   // GET: Companies/Delete/5
	   public async Task<IActionResult> Delete(int? id)
	   {
		  if (id == null)
		  {
			 return NotFound();
		  }
		  var company = await _assessmentService.GetCompanyById(id.Value);

		  if (company == null)
		  {
			 return NotFound();
		  }

		  return View(company);
	   }

	   // POST: Companies/Delete/5
	   [HttpPost, ActionName("Delete")]
	   [ValidateAntiForgeryToken]
	   public async Task<IActionResult> DeleteConfirmed(int id)
	   {
		  var companyId = await _assessmentService.DeleteCompany(id);
		  return RedirectToAction(nameof(Index));
	   }

	   private bool CompanyExists(int id)
	   {
		  var company = _assessmentService.GetCompanyById(id).GetAwaiter().GetResult();
		  return company.Id > 0;
	   }
    }
}
