using Identity.API.Constants;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Identity.API.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public Role Role { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Contact Contact { get; set; }
    }

    public class Contact
    {
        [Required]
        public string TimeZoneId { get; init; }
        [Required]
        public string PhoneNumber { get; init; }
        [Required]
        public Address Address { get; init; }

        public Contact()
        {
        }

        [JsonConstructor]
        public Contact(string timeZoneId, string phoneNumber, Address address)
        {
            TimeZoneId = timeZoneId;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }

    public class Address
    {
        [Required]
        public string Country { get; init; }
        [Required]
        public string City { get; init; }
        [Required]
        public string Street { get; init; }
        [Required]
        public string Number { get; init; }
        [Required]
        public string ZipCode { get; init; }

        public Address()
        {
        }

        [JsonConstructor]
        public Address(string country, string city, string street, string number, string zipCode)
        {
            Country = country;
            City = city;
            Street = street;
            Number = number;
            ZipCode = zipCode;
        }
    }
}
