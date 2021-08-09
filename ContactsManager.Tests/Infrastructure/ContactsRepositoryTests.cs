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
using MockQueryable.EntityFrameworkCore;
using MockQueryable.Moq;
using MockQueryable.Core;
using FluentAssertions;
using System.Threading;

namespace ContactsManager.UnitTests.Infrastructure
{
    public class ContactsRepositoryTests
    {
        private Mock<IContactsDbContext> _mockDb;
        private IContactsRepository _contactsRepository;
        public ContactsRepositoryTests()
        {
            _mockDb = new Mock<IContactsDbContext>();
        }

        [Fact]
        public async void GetAllAsync_Returns_Contacts()
        {
            // Setup
            var contactList = new List<Contact> { new ContactBuilder().Build() };
            var dbSetMock = contactList.AsQueryable().BuildMockDbSet();
            _mockDb.Setup(X => X.Contact).Returns(dbSetMock.Object);
            _contactsRepository = new ContactsRepository(_mockDb.Object);

            // Execute 
            var result = await _contactsRepository.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }


        [Fact]
        public async void AddAsync_Adds_And_Saves()
        {
            // Setup
            var contactList = new List<Contact> { new ContactBuilder().Build() };
            _mockDb.Setup(x => x.Contact.AddAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()));
            _contactsRepository = new ContactsRepository(_mockDb.Object);

            // Execute 
            var result = await _contactsRepository.AddAsync(contactList[0]);

            // Assert
            result.Should().NotBeNull();
            _mockDb.Verify(x => x.Contact.AddAsync(It.IsAny<Contact>(), It.IsAny<CancellationToken>()));
            _mockDb.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}
