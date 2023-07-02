using System;

namespace TT.Deliveries.Data.Models
{
    public class Recipient
    {
        public Recipient(string name, string address, string email, string phoneNumber)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Address = address;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
