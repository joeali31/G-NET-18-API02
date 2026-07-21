using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.DTOs.Identity
{
    public class AddressDto
    {
        public AddressDto(string street, string city, string country, string firstName, string lastName)
        {
            Street = street;
            City = city;
            Country = country;
            FirstName = firstName;
            LastName = lastName;
        }

        //public int Id { get; set; }
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}
