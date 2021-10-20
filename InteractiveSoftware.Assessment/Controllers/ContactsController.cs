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
    public class ContactsController : Controller
    {
	   private readonly IAssessmentService _assessmentService;

	   public ContactsController(IAssessmentService assessmentService)
	   {
		  _assessmentService = assessmentService;
	   }

	   // GET: Contacts

	   public async Task<IActionResult> Index()
	   {
		  var contacts = await _assessmentService.GetContacts();
		  return View(contacts);
	   }

	   // GET: Contacts/Details/5
	   public async Task<IActionResult> Details(int? id)
	   {
		  if (id == null)
		  {
			 return NotFound();
		  }

		  var contact = await _assessmentService.GetContactById(id.Value);

		  if (contact == null)
		  {
			 return NotFound();
		  }

		  return View(contact);
	   }

	   // GET: Contacts/Create
	   public IActionResult Create()
	   {
		  return View();
	   }

	   // POST: Contacts/Create
	   // To protect from overposting attacks, enable the specific properties you want to bind to.
	   // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	   [HttpPost]
	   [ValidateAntiForgeryToken]
	   public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,PhoneNumber,EmailAdress")] Contact contact)
	   {
		  if (ModelState.IsValid)
		  {

			 await _assessmentService.CreateContact(contact);
			 return RedirectToAction(nameof(Index));
		  }
		  return View(contact);
	   }

	   // GET: Contacts/Edit/5
	   public async Task<IActionResult> Edit(int? id)
	   {
		  if (id == null)
		  {
			 return NotFound();
		  }

		  var contact = await _assessmentService.GetContactById(id.Value);
		  if (contact == null)
		  {
			 return NotFound();
		  }
		  return View(contact);
	   }

	   // POST: Contacts/Edit/5
	   // To protect from overposting attacks, enable the specific properties you want to bind to.
	   // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	   [HttpPost]
	   [ValidateAntiForgeryToken]
	   public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,PhoneNumber,EmailAdress")] Contact contact)
	   {
		  if (id != contact.Id)
		  {
			 return NotFound();
		  }
		  var updatedContact = new Contact();

		  if (ModelState.IsValid)
		  {
			 try
			 {
				updatedContact = await _assessmentService.UpdateContact(contact);
			 }
			 catch (DbUpdateConcurrencyException)
			 {
				if (!ContactExists(contact.Id))
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
		  return View(updatedContact);
	   }

	   // GET: Contacts/Delete/5
	   public async Task<IActionResult> Delete(int? id)
	   {
		  if (id == null)
		  {
			 return NotFound();
		  }
		  var contact = await _assessmentService.GetContactById(id.Value);

		  if (contact == null)
		  {
			 return NotFound();
		  }

		  return View(contact);
	   }

	   // POST: Contacts/Delete/5
	   [HttpPost, ActionName("Delete")]
	   [ValidateAntiForgeryToken]
	   public async Task<IActionResult> DeleteConfirmed(int id)
	   {
		  var contactId = await _assessmentService.DeleteContact(id);
		  return RedirectToAction(nameof(Index));
	   }

	   private bool ContactExists(int id)
	   {
		  var contact = _assessmentService.GetContactById(id).GetAwaiter().GetResult();
		  return contact.Id > 0;
	   }
    }
}
