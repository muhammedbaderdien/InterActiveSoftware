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
    public class ContactsController : ControllerBase
    {

	   private readonly InteractiveSoftwareAssessmentContext _context;

	   private readonly ILogger<ContactsController> _logger;

	   public ContactsController(ILogger<ContactsController> logger, InteractiveSoftwareAssessmentContext context)
	   {
		  _logger = logger;
		  _context = context;

		  if (_context.Contact.FirstOrDefault(x => x.Id == 1) == null)
		  {
			 var contact = new Contact
			 {
				Id = 1,
				FirstName = "Muhammed",
				LastName = "Baderdien",
				EmailAdress = "muhammedb@test.net",
				PhoneNumber = "+27833331234"
			 };

			 _context.Contact.Add(contact);
			 _context.SaveChanges();
		  }
	   }

	   // GET: Contacts
	   [HttpGet("Index")]
	   public Task<List<Contact>> Index()
	   {
		  var interactiveSoftwareAssessmentContext = _context.Contact;
		  var contacts = interactiveSoftwareAssessmentContext.ToList();
		  return Task.FromResult(contacts);
	   }

	   // GET: Contacts/Details/5
	   [HttpGet("Details/{id}")]
	   public async Task<Contact> Details(int? id)
	   {
		  if (id == null)
		  {
			 return null;
		  }

		  var contact = _context.Contact
			 .FirstOrDefault(m => m.Id == id);
		  if (contact == null)
		  {
			 return null;
		  }

		  return contact;
	   }


	   // POST: Contacts/Create
	   // To protect from overposting attacks, enable the specific properties you want to bind to.
	   // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	   [HttpPost("Create")]
	   [ValidateAntiForgeryToken]
	   public async Task<Contact> Create(Contact contact)
	   {
		  if (ModelState.IsValid)
		  {
			 _context.Add(contact);
			 await _context.SaveChangesAsync();
			 return contact;
		  }
		  else
		  {
			 return null;
		  }
	   }

	   // GET: Contacts/Edit/5
	   [HttpGet("Edit/{id}")]
	   public async Task<Contact> Edit(int? id)
	   {
		  if (id == null)
		  {
			 return null;
		  }

		  var contact = _context.Contact.FirstOrDefault(x => x.Id == id);
		  if (contact == null)
		  {
			 return null;
		  }
		  return contact;
	   }

	   // POST: Contacts/Edit/5
	   [HttpPost("Edit/{id}")]
	   public async Task<Contact> Edit(int id, Contact contact)
	   {
		  if (id != contact.Id)
		  {
			 return null;
		  }

		  if (ModelState.IsValid)
		  {
			 try
			 {
				_context.Update(contact);
				await _context.SaveChangesAsync();
			 }
			 catch (DbUpdateConcurrencyException)
			 {
				if (!ContactExists(contact.Id))
				{
				    return null;
				}
				else
				{
				    throw;
				}
			 }
			 return contact;
		  }
		  return null;
	   }

	   // GET: Contacts/Delete/5
	   [HttpGet("Delete/{id}")]
	   public async Task<Contact> Delete(int? id)
	   {
		  if (id == null)
		  {
			 return null;
		  }

		  var contact = _context.Contact
			 .FirstOrDefault(m => m.Id == id);
		  if (contact == null)
		  {
			 return null;
		  }

		  return contact;
	   }

	   // POST: Contacts/Delete/5
	   [HttpPost("Delete/{id}")]
	   public async Task<int> Delete(int id)
	   {
		  var contact = await _context.Contact.FindAsync(id);
		  _context.Contact.Remove(contact);
		  await _context.SaveChangesAsync();
		  return id;
	   }

	   private bool ContactExists(int id)
	   {
		  return _context.Contact.Any(e => e.Id == id);
	   }
    }
}
