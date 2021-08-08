using AutoMapper;
using ContactsManager.Application.Contracts;
using ContactsManager.Application.Implementation;
using ContactsManager.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ContactsManager.UnitTests.Builders;
using ContactsManager.Application.DTOs;
using ContactsManager.Domain.Entities;

namespace ContactsManager.UnitTests.Application
{
    public class ContactServiceTests
    {
        private IContactsService _contactsService;
        private Mock<IContactsRepository> _contactsRepository;
        private Mock<IMapper> _mapper;
        public ContactServiceTests()
        {
            _contactsRepository = new Mock<IContactsRepository>();
            _mapper = new Mock<IMapper>();
            _contactsService = new ContactsService(_contactsRepository.Object, _mapper.Object);
        }

        [Fact]
        public async void GetAllContactsAsync_Returns_Contacts()
        {
            // Setup
            var contactList = new List<Contact> { new ContactBuilder().Build() };
            _contactsRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(contactList));

            var contactDtoList = new List<ContactDTO> { new ContactDTOBuilder().Build() };
            _mapper.Setup(x => x.Map<List<ContactDTO>>(It.IsAny<List<Contact>>())).Returns(contactDtoList);
            
            // Execute
            var contacts = await _contactsService.GetAllContactsAsync();


            // Assert
            Assert.NotNull(contacts);
            contacts.Should().HaveCount(1);
        }

    }
}
