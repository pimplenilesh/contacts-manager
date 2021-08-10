using AutoMapper;
using ContactsManager.Application.Contracts;
using ContactsManager.Application.DTOs;
using ContactsManager.Application.Exceptions;
using ContactsManager.Domain.Entities;
using ContactsManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsManager.Application.Implementation
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IMapper _mapper;
        public ContactsService(IContactsRepository contactsRepository, IAuthenticatedUserService authenticatedUserService, IMapper mapper)
        {
            _contactsRepository = contactsRepository;
            _authenticatedUserService = authenticatedUserService;
            _mapper = mapper;
        }

        public async Task<ContactResultDTO> AddAsync(ContactDTO contactDTO)
        {
            await CheckIfContactExists(contactDTO);

            var contactEntity = _mapper.Map<Contact>(contactDTO);
            contactEntity.Created = DateTime.Now;
            contactEntity.CreatedBy = _authenticatedUserService.UserId;

            var contact = await _contactsRepository.AddAsync(contactEntity);
            return _mapper.Map<ContactResultDTO>(contact);
        }

        public async Task<ContactResultDTO> GetByIdAsync(int id)
        {
            var result = await _contactsRepository.GetByIdAsync(id);

            if (result == null)
            {
                throw new ContactNotFoundException($"Contact with this Id [{id}] not found.");
            }

            return _mapper.Map<ContactResultDTO>(result);
        }

        public async Task<List<ContactResultDTO>> GetAllContactsAsync()
        {
            var contacts = await _contactsRepository.GetAllAsync();
            return _mapper.Map<List<ContactResultDTO>>(contacts);
        }

        public async Task<ContactResultDTO> UpdateAsync(int id, ContactDTO contact)
        {
            var result = await _contactsRepository.GetByIdAsync(id);

            if (result == null)
            {
                throw new ContactNotFoundException($"Contact with this Id [{id}] not found.");
            }

            await CheckIfCanUpdate(id, contact);

            _mapper.Map(contact, result);
            result.LastModified = DateTime.Now;
            result.LastModifiedBy = _authenticatedUserService.UserId;

            var updated = await _contactsRepository.UpdateAsync(result);
            return _mapper.Map<ContactResultDTO>(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _contactsRepository.GetByIdAsync(id);

            if (result == null)
            {
                throw new ContactNotFoundException($"Contact with this Id [{id}] not found.");
            }

            var deleteResult = await _contactsRepository.DeleteAsync(result);
            return deleteResult;
        }

        private async Task CheckIfContactExists(ContactDTO contactDTO)
        {
            var existingContact = await _contactsRepository.GetContactAsync(_mapper.Map<Contact>(contactDTO));

            if (existingContact != null)
            {
                throw new ContactAlreadyExistsException($"Contact with phone number {contactDTO.PhoneNumber} or Email Id {contactDTO.Email}, already exists.");
            }
        }

        private async Task CheckIfCanUpdate(int Id, ContactDTO contactDTO)
        {
            var existingContact = await _contactsRepository.GetContactAsync(_mapper.Map<Contact>(contactDTO));

            if (existingContact != null && existingContact.Id != Id)
            {
                throw new ContactAlreadyExistsException($"Contact with phone number {contactDTO.PhoneNumber} or Email Id {contactDTO.Email}, already exists.");
            }
        }
    }
}
