namespace UserDirectory.Models
{
    public class Users
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }

        public string PostalAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string ZipCode { get; set; }
    }
}
