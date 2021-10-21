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
    public class AddressesController : ControllerBase
    {

	   private readonly InteractiveSoftwareAssessmentContext _context;

	   private readonly ILogger<AddressesController> _logger;

	   public AddressesController(ILogger<AddressesController> logger, InteractiveSoftwareAssessmentContext context)
	   {
		  _logger = logger;
		  _context = context;

		  if (_context.Address.FirstOrDefault(x => x.Id == 1) == null)
		  {
			 var address = new Address
			 {
				Id = 1,
				Line1 = "5 Test Avenue",
				Line2 = "",
				Suburb = "Discovery",
				Province = "Gauteng",
				Postcode = "1709"
			 };

			 _context.Address.Add(address);
			 _context.SaveChanges();
		  }
	   }

	   // GET: Addresses
	   [HttpGet("Index")]
	   public Task<List<Address>> Index()
	   {
		  var interactiveSoftwareAssessmentContext = _context.Address;
		  var addresses = interactiveSoftwareAssessmentContext.ToList();
		  return Task.FromResult(addresses);
	   }

	   // GET: Addresses/Details/5
	   [HttpGet("Details/{id}")]
	   public async Task<Address> Details(int? id)
	   {
		  if (id == null)
		  {
			 return null;
		  }

		  var address = _context.Address
			 .FirstOrDefault(m => m.Id == id);
		  if (address == null)
		  {
			 return null;
		  }

		  return address;
	   }


	   // POST: Addresses/Create
	   // To protect from overposting attacks, enable the specific properties you want to bind to.
	   // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	   [HttpPost("Create")]
	   public async Task<Address> Create(Address address)
	   {
		  if (ModelState.IsValid)
		  {
			 _context.Add(address);
			 await _context.SaveChangesAsync();
			 return address;
		  }
		  else
		  {
			 return null;
		  }
	   }

	   // GET: Addresses/Edit/5
	   [HttpGet("Edit/{id}")]
	   public async Task<Address> Edit(int? id)
	   {
		  if (id == null)
		  {
			 return null;
		  }

		  var address = _context.Address.FirstOrDefault(x => x.Id == id);
		  if (address == null)
		  {
			 return null;
		  }
		  return address;
	   }

	   // POST: Addresses/Edit/5
	   [HttpPost("Edit/{id}")]
	   public async Task<Address> Edit(int id, Address address)
	   {
		  if (id != address.Id)
		  {
			 return null;
		  }

		  if (ModelState.IsValid)
		  {
			 try
			 {
				_context.Update(address);
				await _context.SaveChangesAsync();
			 }
			 catch (DbUpdateConcurrencyException)
			 {
				if (!AddressExists(address.Id))
				{
				    return null;
				}
				else
				{
				    throw;
				}
			 }
			 return address;
		  }
		  return null;
	   }

	   // GET: Addresses/Delete/5
	   [HttpGet("Delete/{id}")]
	   public async Task<Address> Delete(int? id)
	   {
		  if (id == null)
		  {
			 return null;
		  }

		  var address = _context.Address
			 .FirstOrDefault(m => m.Id == id);
		  if (address == null)
		  {
			 return null;
		  }

		  return address;
	   }

	   // POST: Addresses/Delete/5
	   [HttpPost("Delete/{id}")]
	   public async Task<int> Delete(int id)
	   {
		  var address = await _context.Address.FindAsync(id);
		  _context.Address.Remove(address);
		  await _context.SaveChangesAsync();
		  return id;
	   }

	   private bool AddressExists(int id)
	   {
		  return _context.Address.Any(e => e.Id == id);
	   }
    }
}
