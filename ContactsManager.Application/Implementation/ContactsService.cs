using AutoMapper;
using ContactsManager.Application.Contracts;
using ContactsManager.Application.DTOs;
using ContactsManager.Application.Exceptions;
using ContactsManager.Domain.Entities;
using ContactsManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Application.Implementation
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IMapper _mapper;
        public ContactsService(IContactsRepository contactsRepository, IMapper mapper)
        {
            _contactsRepository = contactsRepository;
            _mapper = mapper;
        }

        public async Task<ContactResultDTO> AddAsync(ContactDTO contactDTO)
        {
            var existingContact = await _contactsRepository.GetContactAsync(_mapper.Map<Contact>(contactDTO));

            if (existingContact != null)
            {
                throw new ContactAlreadyExistsException($"Contact with phone number {contactDTO.PhoneNumber} or Email Id {contactDTO.Email}, already exists.");
            }

            var contactEntity = _mapper.Map<Contact>(contactDTO);
            contactEntity.Created = DateTime.Now;
            contactEntity.CreatedBy = "user";

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

        public async Task<ContactDTO> UpdateAsync(int id, ContactDTO contact)
        {
            var result = await _contactsRepository.GetByIdAsync(id);

            if (result == null)
            {
                throw new ContactNotFoundException($"Contact with this Id [{id}] not found.");
            }

            result.Email = contact.Email;
            result.PhoneNumber = contact.PhoneNumber;
            result.FirstName = contact.FirstName;
            result.LastName = contact.LastName;
            result.Status = contact.Status;
            result.LastModified = DateTime.Now;
            result.LastModifiedBy = "user";

            var updated = await _contactsRepository.UpdateAsync(result);
            return _mapper.Map<ContactDTO>(updated);
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
    }
}
