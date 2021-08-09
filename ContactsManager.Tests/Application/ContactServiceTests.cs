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
using ContactsManager.Application.Exceptions;

namespace ContactsManager.UnitTests.Application
{
    public class ContactServiceTests
    {
        private IContactsService _contactsService;
        private Mock<IContactsRepository> _contactsRepository;
        private Mock<IMapper> _mapper;
        private readonly Mock<IAuthenticatedUserService> _mockAuth;
        public ContactServiceTests()
        {
            _contactsRepository = new Mock<IContactsRepository>();
            _mapper = new Mock<IMapper>();
            _mockAuth = new Mock<IAuthenticatedUserService>();
            _contactsService = new ContactsService(_contactsRepository.Object, _mockAuth.Object, _mapper.Object);
        }

        [Fact]
        public async void GetAllContactsAsync_Returns_Contacts()
        {
            // Setup
            var contactList = new List<Contact> { new ContactBuilder().Build() };
            _contactsRepository.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(contactList));

            var contactDtoList = new List<ContactResultDTO> { new ContactResultDTOBuilder().Build() };
            _mapper.Setup(x => x.Map<List<ContactResultDTO>>(It.IsAny<List<Contact>>())).Returns(contactDtoList);
            
            // Execute
            var contacts = await _contactsService.GetAllContactsAsync();


            // Assert
            Assert.NotNull(contacts);
            contacts.Should().HaveCount(1);
        }

        [Fact]
        public async void GetByIdAsync_Returns_Contact()
        {
            // Setup
            var contact =  new ContactBuilder().Build();
            _contactsRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(contact));

            var contactDto =  new ContactResultDTOBuilder().Build();
            _mapper.Setup(x => x.Map<ContactResultDTO>(It.IsAny<Contact>())).Returns(contactDto);

            // Execute
            var result = await _contactsService.GetByIdAsync(1);


            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be("Nilesh");
        }

        [Fact]
        public async void GetByIdAsync_NotFound_ThrowsException()
        {
            // Setup
            var contact = new ContactBuilder().With(3).Build();

            _contactsRepository.Setup(x => x.GetByIdAsync(3)).Returns(Task.FromResult(contact));

            var contactDto = new ContactResultDTOBuilder().Build();
            _mapper.Setup(x => x.Map<ContactResultDTO>(It.IsAny<Contact>())).Returns(contactDto);

            // Execute
            var result = await Assert.ThrowsAsync<ContactNotFoundException>(async () => await _contactsService.GetByIdAsync(2));

            // Assert
            result.Message.Should().Be("Contact with this Id [2] not found.");
        }
    }
}
