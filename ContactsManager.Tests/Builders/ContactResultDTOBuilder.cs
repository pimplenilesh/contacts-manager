using ContactsManager.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.UnitTests.Builders
{
    public class ContactResultDTOBuilder
    {
        private ContactResultDTO _contactDTO;

        public ContactResultDTOBuilder()
        {
            _contactDTO = new ContactResultDTO
            {
                FirstName = "Nilesh",
                LastName = "Pimple",
                Email = "nilesh@gmail.com",
                PhoneNumber = "8888888888",
                Status = "Active"
            };
        }

        public ContactResultDTO Build()
        {
            return _contactDTO;
        }

    }
}
