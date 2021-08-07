using ContactsManager.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Application.Contracts
{
    public interface IContactsService
    {
        Task<List<ContactDTO>> GetAllContactsAsync();
        Task<ContactDTO> AddAsync(ContactDTO contactDTO);
        Task<ContactDTO> GetByIdAsync(int id);
        Task<ContactDTO> UpdateAsync(int id, ContactDTO contact);
        Task<bool> DeleteAsync(int id);
    }
}
