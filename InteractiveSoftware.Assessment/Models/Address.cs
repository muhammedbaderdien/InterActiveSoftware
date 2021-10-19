using System;

namespace InteractiveSoftware.Assessment.Models
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
