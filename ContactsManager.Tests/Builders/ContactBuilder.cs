using ContactsManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.UnitTests.Builders
{
    public class ContactBuilder
    {
        private Contact _contact;
        public ContactBuilder()
        {
            _contact = new Contact
            {
                FirstName = "Nilesh",
                LastName = "Pimple",
                Email = "nilesh@gmail.com",
                PhoneNumber = "8888888888",
                Status = "Active",
                Created = DateTime.Now,
                CreatedBy = "Nilesh",
                LastModified = DateTime.Now,
                LastModifiedBy = "Nilesh",
                Id = 1
            };
        }

        public ContactBuilder With(string firstName)
        {
            _contact.FirstName = firstName;
            return this;
        }

        public Contact Build()
        {
            return _contact;
        }
    }
}
