using ContactsManager.Domain.Entities;
using ContactsManager.Domain.Interfaces;
using ContactsManager.Infrastructure.Persistence;
using ContactsManager.Infrastructure.Repositories;
using ContactsManager.UnitTests.Builders;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContactsManager.UnitTests.Infrastructure
{
    
    public class ContactsRepositoryTests
    {
        private IContactsRepository _contactsRepository;
        public ContactsRepositoryTests()
        { 

        }

        [Fact]
        public void GetAllAsync_Returns_Contacts()
        {
            var contactList = new List<Contact> { new ContactBuilder().Build() };
           // var dbSetMock = contactList.AsQueryable().BuildMock();
            
            //_contactsRepository = new ContactsRepository();

        }
    }
}
