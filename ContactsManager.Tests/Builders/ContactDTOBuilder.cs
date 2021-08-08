using ContactsManager.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.UnitTests.Builders
{
    public class ContactDTOBuilder
    {
        private ContactDTO _contactDTO;

        public ContactDTOBuilder()
        {
            _contactDTO = new ContactDTO
            {
                FirstName = "Nilesh",
                LastName = "Pimple",
                Email = "nilesh@gmail.com",
                PhoneNumber = "8888888888",
                Status = "Active"
            };
        }

        public ContactDTO Build()
        {
            return _contactDTO;
        }

    }
}
