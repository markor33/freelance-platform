namespace FreelancerProfile.Application.Queries
{
    public class FreelancerViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ContactViewModel Contact { get; set; }
    }

    public class ContactViewModel
    {
        public AddressViewModel Address { get; set; }
        public string PhoneNumber { get; set; }
        public string TimeZoneId { get; set; }
    }

    public class AddressViewModel
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
    }
}
