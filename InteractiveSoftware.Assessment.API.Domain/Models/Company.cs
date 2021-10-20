using InteractiveSoftware.Assessment.API.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace InteractiveSoftware.Assessment.API.Domain.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }
        [Display(Name = "Address")]
        public int AddressId { get; set; }
        [Display(Name = "Address")]
        public virtual Address CompanyAddress { get; set; }
        [Display(Name = "Contact")]
        public int ContactId { get; set; }
        [Display(Name = "Contact")]
        public virtual Contact CompanyContact { get; set; }
    }
}
