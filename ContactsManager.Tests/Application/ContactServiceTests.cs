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

        [Fact]
        public async void UpdateAsync_Updates_Contact()
        {
            // Setup
            var contact = new ContactBuilder().Build();

            _contactsRepository.Setup(x => x.GetByIdAsync(3)).Returns(Task.FromResult(contact));
            _contactsRepository.Setup(x => x.UpdateAsync(It.IsAny<Contact>())).Returns(Task.FromResult(contact));
            _mockAuth.Setup(x => x.UserId).Returns("Nilesh");

            var contactDto = new ContactResultDTOBuilder().Build();
            _mapper.Setup(x => x.Map<ContactResultDTO>(It.IsAny<Contact>())).Returns(contactDto);


            // Execute

            var contactDTO = new ContactDTOBuilder().Build();
            var result = await _contactsService.UpdateAsync(3, contactDTO); ;

            // Assert
            result.Should().NotBeNull();
            _contactsRepository.Verify(x => x.GetByIdAsync(3));
            _contactsRepository.Verify(x => x.UpdateAsync(It.IsAny<Contact>()));
            _mockAuth.Verify(x => x.UserId);
        }

        [Fact]
        public async void UpdateAsync_ContactExists_ThrowsException()
        {
            // Setup
            Contact contact1 = null;
            _contactsRepository.Setup(x => x.GetByIdAsync(3)).Returns(Task.FromResult(contact1));

            var contactDto = new ContactResultDTOBuilder().Build();
            _mapper.Setup(x => x.Map<ContactResultDTO>(It.IsAny<Contact>())).Returns(contactDto);


            // Execute

            var contactDTO = new ContactDTOBuilder().Build();
            var result = await Assert.ThrowsAsync<ContactNotFoundException>(async () => await _contactsService.UpdateAsync(3, contactDTO));

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be("Contact with this Id [3] not found.");
        }


        [Fact]
        public async void AddAsync_Adds_Contact()
        {
            // Setup
            Contact contact = null;

            _contactsRepository.Setup(x => x.GetContactAsync(It.IsAny<Contact>())).ReturnsAsync(contact);
            _contactsRepository.Setup(x => x.AddAsync(It.IsAny<Contact>())).Returns(Task.FromResult(contact));
            _mockAuth.Setup(x => x.UserId).Returns("Nilesh");

            var contactDto = new ContactResultDTOBuilder().Build();
            var contactRep = new ContactBuilder().Build();
            _mapper.Setup(x => x.Map<Contact>(It.IsAny<ContactDTO>())).Returns(contactRep);
            _mapper.Setup(x => x.Map<ContactResultDTO>(It.IsAny<Contact>())).Returns(contactDto);


            // Execute

            var contactDTO = new ContactDTOBuilder().Build();
            var result = await _contactsService.AddAsync(contactDTO); ;

            // Assert
            result.Should().NotBeNull();
            _contactsRepository.Verify(x => x.GetContactAsync(It.IsAny<Contact>()));
            _contactsRepository.Verify(x => x.AddAsync(It.IsAny<Contact>()));
            _mockAuth.Verify(x => x.UserId);
        }

        [Fact]
        public async void AddAsync_ExistingEmail_ThrowsException()
        {
            // Setup
            var contactRep = new ContactBuilder().Build();

            _contactsRepository.Setup(x => x.GetContactAsync(It.IsAny<Contact>())).ReturnsAsync(contactRep);
            _contactsRepository.Setup(x => x.AddAsync(It.IsAny<Contact>())).Returns(Task.FromResult(contactRep));
            _mockAuth.Setup(x => x.UserId).Returns("Nilesh");

            var contactDto = new ContactResultDTOBuilder().Build();
           
            _mapper.Setup(x => x.Map<Contact>(It.IsAny<ContactDTO>())).Returns(contactRep);
            _mapper.Setup(x => x.Map<ContactResultDTO>(It.IsAny<Contact>())).Returns(contactDto);


            // Execute

            var contactDTO = new ContactDTOBuilder().Build();
            var result = await Assert.ThrowsAsync<ContactAlreadyExistsException>(async () => await _contactsService.AddAsync(contactDTO));

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be("Contact with phone number 8888888888 or Email Id nilesh@gmail.com, already exists.");
        }

        [Fact]
        public async void DeleteAsync_Deletes_Contact()
        {
            // Setup
            var contact = new ContactBuilder().Build();
            _contactsRepository.Setup(x => x.GetByIdAsync(3)).Returns(Task.FromResult(contact));
            _contactsRepository.Setup(x => x.DeleteAsync(It.IsAny<Contact>())).ReturnsAsync(true);
            var contactDto = new ContactResultDTOBuilder().Build();


            // Execute
            var result = await _contactsService.DeleteAsync(3);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void DeleteAsync_ContactDoesNotExists_ThrowsException()
        {
            // Setup
            Contact contact = null;
            _contactsRepository.Setup(x => x.GetByIdAsync(3)).ReturnsAsync(contact);
            _contactsRepository.Setup(x => x.DeleteAsync(It.IsAny<Contact>())).ReturnsAsync(true);
            var contactDto = new ContactResultDTOBuilder().Build();


            // Execute
            var result = await Assert.ThrowsAsync<ContactNotFoundException>(async () => await _contactsService.DeleteAsync(3));

            // Assert
            result.Should().NotBeNull();
            result.Message.Should().Be("Contact with this Id [3] not found.");
        }
    }
}
