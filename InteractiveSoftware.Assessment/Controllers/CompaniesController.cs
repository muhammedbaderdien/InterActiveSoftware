﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InteractiveSoftware.Assessment.Data;
using InteractiveSoftware.Assessment.Models;

namespace InteractiveSoftware.Assessment.Controllers
{
    public class CompaniesController : Controller
    {
	   private readonly InteractiveSoftwareAssessmentContext _context;


	   public CompaniesController(InteractiveSoftwareAssessmentContext context)
	   {
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
	   public async Task<IActionResult> Index()
	   {
		  var interactiveSoftwareAssessmentContext = _context.Company.Include(c => c.CompanyAddress).Include(c => c.CompanyContact);
		  return View(await interactiveSoftwareAssessmentContext.ToListAsync());
	   }

	   // GET: Companies/Details/5
	   public async Task<IActionResult> Details(int? id)
	   {
		  if (id == null)
		  {
			 return NotFound();
		  }

		  var company = await _context.Company
			 .Include(c => c.CompanyAddress)
			 .Include(c => c.CompanyContact)
			 .FirstOrDefaultAsync(m => m.Id == id);
		  if (company == null)
		  {
			 return NotFound();
		  }

		  return View(company);
	   }

	   // GET: Companies/Create
	   public IActionResult Create()
	   {
		  ViewData["AddressId"] = new SelectList(_context.Set<Address>(), "Id", "Id");
		  ViewData["ContactId"] = new SelectList(_context.Set<Contact>(), "Id", "Id");
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
			 _context.Add(company);
			 await _context.SaveChangesAsync();
			 return RedirectToAction(nameof(Index));
		  }
		  ViewData["AddressId"] = new SelectList(_context.Set<Address>(), "Id", "Id", company.AddressId);
		  ViewData["ContactId"] = new SelectList(_context.Set<Contact>(), "Id", "Id", company.ContactId);
		  return View(company);
	   }

	   // GET: Companies/Edit/5
	   public async Task<IActionResult> Edit(int? id)
	   {
		  if (id == null)
		  {
			 return NotFound();
		  }

		  var company = await _context.Company.Include(c => c.CompanyAddress).Include(c => c.CompanyContact).FirstOrDefaultAsync(x => x.Id == id);
		  if (company == null)
		  {
			 return NotFound();
		  }
		  ViewData["AddressId"] = new SelectList(_context.Set<Address>(), "Id", "Id", company.AddressId);
		  ViewData["ContactId"] = new SelectList(_context.Set<Contact>(), "Id", "Id", company.ContactId);
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
				    return NotFound();
				}
				else
				{
				    throw;
				}
			 }
			 return RedirectToAction(nameof(Index));
		  }
		  ViewData["AddressId"] = new SelectList(_context.Set<Address>(), "Id", "Id", company.AddressId);
		  ViewData["ContactId"] = new SelectList(_context.Set<Contact>(), "Id", "Id", company.ContactId);
		  return View(company);
	   }

	   // GET: Companies/Delete/5
	   public async Task<IActionResult> Delete(int? id)
	   {
		  if (id == null)
		  {
			 return NotFound();
		  }

		  var company = await _context.Company
			 .Include(c => c.CompanyAddress)
			 .Include(c => c.CompanyContact)
			 .FirstOrDefaultAsync(m => m.Id == id);
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
		  var company = await _context.Company.FindAsync(id);
		  _context.Company.Remove(company);
		  await _context.SaveChangesAsync();
		  return RedirectToAction(nameof(Index));
	   }

	   private bool CompanyExists(int id)
	   {
		  return _context.Company.Any(e => e.Id == id);
	   }
    }
}
