using System;
using System.ComponentModel.DataAnnotations;

namespace InteractiveSoftware.Assessment.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Display(Name = "Company Name")]
        public string Name { get; set; }
        [Display(Name = "Company Registration Number")]
        public string RegistrationNumber { get; set; }
        public int AddressId { get; set; }
        public virtual Address CompanyAddress { get; set; }
        public int ContactId { get; set; }
        public virtual Contact CompanyContact { get; set; }
    }
}
