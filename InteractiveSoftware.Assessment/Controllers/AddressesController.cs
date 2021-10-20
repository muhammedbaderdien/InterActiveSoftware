using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InteractiveSoftware.Assessment.API.Domain;
using InteractiveSoftware.Assessment.API.Domain.Models;
using InteractiveSoftware.Assessment.API.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InteractiveSoftware.Assessment.Controllers
{
    public class AddressesController : Controller
    {
	   private readonly IAssessmentService _assessmentService;

	   public AddressesController(IAssessmentService assessmentService)
	   {
		  _assessmentService = assessmentService;
	   }

	   // GET: Addresses

	   public async Task<IActionResult> Index()
	   {
		  var companies = await _assessmentService.GetAddresses();
		  return View(companies);
	   }

	   // GET: Addresses/Details/5
	   public async Task<IActionResult> Details(int? id)
	   {
		  if (id == null)
		  {
			 return NotFound();
		  }

		  var address = await _assessmentService.GetAddressById(id.Value);

		  if (address == null)
		  {
			 return NotFound();
		  }

		  return View(address);
	   }

	   // GET: Addresses/Create
	   public IActionResult Create()
	   {
		  return View();
	   }

	   // POST: Addresses/Create
	   // To protect from overposting attacks, enable the specific properties you want to bind to.
	   // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	   [HttpPost]
	   [ValidateAntiForgeryToken]
	   public async Task<IActionResult> Create([Bind("Id,Line1,Line2,Suburb,Province,Postcode")] Address address)
	   {
		  if (ModelState.IsValid)
		  {
			 await _assessmentService.CreateAddress(address);
			 return RedirectToAction(nameof(Index));
		  }
		  return View(address);
	   }

	   // GET: Addresses/Edit/5
	   public async Task<IActionResult> Edit(int? id)
	   {
		  if (id == null)
		  {
			 return NotFound();
		  }

		  var address = await _assessmentService.GetAddressById(id.Value);
		  if (address == null)
		  {
			 return NotFound();
		  }
		  return View(address);
	   }

	   // POST: Addresses/Edit/5
	   // To protect from overposting attacks, enable the specific properties you want to bind to.
	   // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	   [HttpPost]
	   [ValidateAntiForgeryToken]
	   public async Task<IActionResult> Edit(int id, [Bind("Id,Line1,Line2,Suburb,Province,Postcode")] Address address)
	   {
		  if (id != address.Id)
		  {
			 return NotFound();
		  }
		  var updatedAddress = new Address();

		  if (ModelState.IsValid)
		  {
			 try
			 {
				updatedAddress = await _assessmentService.UpdateAddress(address);
			 }
			 catch (DbUpdateConcurrencyException)
			 {
				if (!AddressExists(address.Id))
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
		  return View(updatedAddress);
	   }

	   // GET: Addresses/Delete/5
	   public async Task<IActionResult> Delete(int? id)
	   {
		  if (id == null)
		  {
			 return NotFound();
		  }
		  var address = await _assessmentService.GetAddressById(id.Value);

		  if (address == null)
		  {
			 return NotFound();
		  }

		  return View(address);
	   }

	   // POST: Addresses/Delete/5
	   [HttpPost, ActionName("Delete")]
	   [ValidateAntiForgeryToken]
	   public async Task<IActionResult> DeleteConfirmed(int id)
	   {
		  var addressId = await _assessmentService.DeleteAddress(id);
		  return RedirectToAction(nameof(Index));
	   }

	   private bool AddressExists(int id)
	   {
		  var address = _assessmentService.GetAddressById(id).GetAwaiter().GetResult();
		  return address.Id > 0;
	   }
    }
}
