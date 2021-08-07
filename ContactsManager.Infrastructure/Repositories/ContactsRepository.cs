using ContactsManager.Domain.Entities;
using ContactsManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Infrastructure.Repositories
{
    public class ContactsRepository : IContactsRepository
    {
        public Task<Contact> AddAsync(Contact entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Contact entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Contact>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Contact> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Contact entity)
        {
            throw new NotImplementedException();
        }
    }
}
