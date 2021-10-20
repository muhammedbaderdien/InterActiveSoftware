using System;

namespace InteractiveSoftware.Assessment.API.Domain.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAdress { get; set; }
    }
}
