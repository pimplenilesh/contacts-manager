﻿using AutoMapper;
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

        public async Task<ContactDTO> AddAsync(ContactDTO contactDTO)
        {
            var contactEntity = _mapper.Map<Contact>(contactDTO);
            contactEntity.Created = DateTime.Now;
            contactEntity.CreatedBy = "user";

            var contact = await _contactsRepository.AddAsync(contactEntity);
            return _mapper.Map<ContactDTO>(contact);
        }

        public async Task<ContactDTO> GetByIdAsync(int id)
        {
            var result = await _contactsRepository.GetByIdAsync(id);

            if(result == null)
            {
                throw new ContactNotFoundException($"Contact with this Id [{id}] not found.");
            }

            return _mapper.Map<ContactDTO>(result);
        }

        public async Task<List<ContactDTO>> GetAllContactsAsync()
        {
            var contacts = await _contactsRepository.GetAllAsync();
            return _mapper.Map<List<ContactDTO>>(contacts);
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
