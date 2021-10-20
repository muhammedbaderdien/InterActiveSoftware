using System;

namespace InteractiveSoftware.Assessment.API.Domain.Models
{
    public class Address
    {
	   public int Id { get; set; }
	   public string Line1 { get; set; }
	   public string Line2 { get; set; }
	   public string Suburb { get; set; }
	   public string Province { get; set; }
	   public string Postcode { get; set; }
    }
}
